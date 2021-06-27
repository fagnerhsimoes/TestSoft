using System;

namespace TesteSoft.Calculo.Services.DTO
{
    public class ExceptionDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Mensagem { get; set; }

        public DateTime DataErro { get; set; }
    }
}
