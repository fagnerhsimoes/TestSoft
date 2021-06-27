namespace TesteSoft.Calculo.Domain.Entities
{
    public class Resultado
    {
        public Resultado(string valorTotalComJuros)
        {
            ValorTotalComJurosJuros = valorTotalComJuros;
        }

        public string ValorTotalComJurosJuros { get; private set; }
    }
}
