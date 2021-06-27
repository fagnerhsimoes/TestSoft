using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TesteSoft.Calculo.Common.Exceptions;
using TesteSoft.Calculo.Services.DTO;

namespace TesteSoft.Calculo.Common.Helpers
{
    public static class HttpClientHelper
    {
        /// <summary>
        /// Verifica por erros na resposta HTTP.
        /// </summary>
        /// <param name="response">Resposta de requisição HTTP</param>
        public static async Task HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;

            string content = await response.Content.ReadAsStringAsync();
            var exception = JsonConvert.DeserializeObject<ExceptionDto>(content, new JsonSerializerSettings
            {
                Error = (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args) =>
                {
                    args.ErrorContext.Handled = true;
                    throw new TesteSoftIntegrationException(response);
                }
            });

            throw new DomainException(exception?.Mensagem);
        }
    }
}
