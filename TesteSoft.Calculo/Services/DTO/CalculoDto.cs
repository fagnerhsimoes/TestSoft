using System.ComponentModel.DataAnnotations;

namespace TesteSoft.Calculo.Services.DTO
{
    public class CalculoDto
    {
        public double ValorInicial { get; set; }

        [Required(ErrorMessage = "A Quantidade de meses é Obrigatória")]
        public int Meses { get; set; }
    }
}
