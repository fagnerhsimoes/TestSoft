using System;
using System.Net.Http;

namespace TesteSoft.Calculo.Common.Exceptions
{
    public class TesteSoftIntegrationException : Exception
    {
        private readonly HttpResponseMessage _response;

        public TesteSoftIntegrationException(HttpResponseMessage response)
            : base(WriteMessage(response))
        {
            _response = response;
        }

        private static string WriteMessage(HttpResponseMessage response)
        {
            var msg = "Chamada de Integração para " +
                response.RequestMessage.RequestUri +
                " retornou status code " +
                response.StatusCode +
                " .";

            msg = WriteJson(response, msg);

            return msg;
        }

        private static string WriteJson(HttpResponseMessage response, string msg)
        {
            var json = response.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrEmpty(json))
            {
                msg += " Conteudo retornado na requisição: " + json;
            }
            return msg;
        }

        public HttpResponseMessage Response
        {
            get { return _response; }
        }
    }
}
