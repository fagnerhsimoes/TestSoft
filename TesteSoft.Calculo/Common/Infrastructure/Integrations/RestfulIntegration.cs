using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TesteSoft.Calculo.Common.Exceptions;

namespace TesteSoft.Calculo.Common.Infrastructure.Integrations
{
    public abstract class RestfulIntegration
    {
        protected readonly HttpClient Client;

        public object AuthorizationHelper { get; private set; }

        public RestfulIntegration(HttpClient client)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            Client = client;
            Client.Timeout = TimeSpan.FromMinutes(4);
        }

        protected virtual async Task<HttpResponseMessage> GetAsync(string endpointWithParameters, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            return await Client.GetAsync(endpointWithParameters, ct);
        }

        protected virtual async Task<HttpResponseMessage> DeleteAsync(string endpointWithParameters, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            return await Client.DeleteAsync(endpointWithParameters, ct);
        }

        protected virtual async Task<HttpResponseMessage> PatchAsync(string endpointWithParameters, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var request = new HttpRequestMessage();

            request.Method = new HttpMethod("PATCH");
            request.RequestUri = new Uri(endpointWithParameters);

            return await Client.SendAsync(request, ct);
        }

        protected virtual async Task<HttpResponseMessage> PostAsync<TRequest>(string endpoint, TRequest request, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var jsonRequest = SerializeObject(request);

            var contentString = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await Client.PostAsync(endpoint, contentString, ct);
        }

        protected virtual async Task<HttpResponseMessage> PostAsJsonAsync(string endpoint, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            return await this.PostAsJsonAsync<dynamic>(endpoint, null, ct);
        }

        protected virtual async Task<HttpResponseMessage> PostAsJsonAsync<TRequest>(string endpoint, TRequest request, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            JObject jsonRequest = null;

            if (request != null)
                jsonRequest = JObject.FromObject(request);

            return await Client.PostAsJsonAsync(endpoint, jsonRequest, ct);
        }

        protected virtual async Task<HttpResponseMessage> PutAsync<TRequest>(string endpoint, TRequest request, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var jsonRequest = SerializeObject(request);

            var contentString = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await Client.PutAsync(endpoint, contentString, ct);

        }

        protected virtual async Task<HttpResponseMessage> PostAsMultipartFileBytesAsync(string endpoint, string fileName, byte[] fileBytes, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            using (var content = new MultipartFormDataContent())
            {
                Client.DefaultRequestHeaders.TransferEncodingChunked = true;

                using var fileContent = new ByteArrayContent(fileBytes);

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = fileName,
                    Name = "file"
                };

                fileContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Pdf);

                content.Add(fileContent);

                return await Client.PostAsync(endpoint, content, ct);
            }
        }

        protected virtual async Task<HttpResponseMessage> PostAsMultipartNamedFileFolder(string endpoint,
                                                                                         string originFilePath,
                                                                                         string targetFilePath,
                                                                                         CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            using (var content = new MultipartFormDataContent())
            {
                Client.DefaultRequestHeaders.TransferEncodingChunked = true;
                var fileName = Path.GetFileName(originFilePath);

                using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(originFilePath, ct));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = fileName,
                    Name = "file"
                };
                content.Add(fileContent);

                var file = Path.HasExtension(targetFilePath) ? Path.GetFileNameWithoutExtension(targetFilePath) : Path.GetFileNameWithoutExtension(fileName);

                var isFolder = !Path.HasExtension(targetFilePath);

                var folder = isFolder ? targetFilePath : Path.GetDirectoryName(targetFilePath);

                var response = await Client.PostAsync($"{endpoint}?fileName={file}&folderName={folder}", content, ct);

                return response;
            }
        }

        protected virtual async Task<T> HttpResponseMessageHandlerAsync<T>(HttpResponseMessage response,
                                                                           bool showSimpleException,
                                                                           CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (!response.IsSuccessStatusCode)
            {
                if (showSimpleException)
                {
                    string message;

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                        message = response.ReasonPhrase;
                    else
                    {
                        var aux = await response.Content.ReadAsAsync<dynamic>(ct);
                        message = aux.Message.ToString();
                    }

                    throw new TesteSoftException(message, response.StatusCode);
                }
                else
                {
                    throw new TesteSoftIntegrationException(response);
                }
            }
            return await response.Content.ReadAsAsync<T>(ct);
        }

        protected virtual async Task<byte[]> HttpResponseMessageHandlerByteAsync(HttpResponseMessage response,
                                                                                 bool showSimpleException,
                                                                                 CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (!response.IsSuccessStatusCode)
            {
                if (showSimpleException)
                {
                    throw new TesteSoftException(response.ReasonPhrase, response.StatusCode);
                }
                else
                {
                    throw new TesteSoftIntegrationException(response);
                }
            }
            return await response.Content.ReadAsByteArrayAsync();
        }

        protected virtual T DeserializeObject<T>(string json)
        {
            try
            {
                var settings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                var deserializedObject = JsonConvert.DeserializeObject<T>(json, settings);

                return deserializedObject;
            }
            catch (Exception ex)
            {
                var exception = new Exception($"Erro ao deserializar retorno da API: {ex.Message}", ex);
                throw exception;
            }
        }

        protected virtual string SerializeObject<T>(T request)
        {
            try
            {
                var settings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                var serializedObject = JsonConvert.SerializeObject(request, Formatting.None, settings);

                return serializedObject;
            }
            catch (Exception ex)
            {
                var exception = new Exception($"Erro ao serializar request da API: {ex.Message}", ex);
                throw exception;
            }
        }

        private void SetAuthorizationHeader(string token)
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

    }
}
