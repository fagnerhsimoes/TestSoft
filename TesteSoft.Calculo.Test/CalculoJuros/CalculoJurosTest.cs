using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TesteSoft.Calculo.Test.CalculoJuros
{

    [TestClass]
    public class CalculoJurosTest
    {
        [TestMethod]
        public void RetornaErroQuandoResultadoCalculoInvalido()
        {
            string resultadoTeste = "105,09";
            var ResultadoCalculo = "105,10";

            bool ResultadoValido = ResultadoCalculo == resultadoTeste;
            Assert.IsTrue(!ResultadoValido);
        }

        [TestMethod]
        public void RetornaSucessoQuandoResultadoCalculoValido()
        {
            string resultadoTeste = "105,10";
            var ResultadoCalculo = "105,10";

            bool ResultadoValido = ResultadoCalculo == resultadoTeste;
            Assert.IsTrue(ResultadoValido);
        }
    }
}
