using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesteSoft.Taxa.Test.TaxaJuros
{
    [TestClass]
    public class TaxaJurosTest
    {
        [TestMethod]
        public void RetornaErroQuandoValorTaxaInvalido()
        {
            double taxa = 123;
            bool TaxaValido = taxa == Domain.Entities.TaxaJuros.taxaAtual;
            Assert.IsTrue(!TaxaValido);
        }

        [TestMethod]
        public void RetornaSucessoQuandoValorTaxaValido()
        {
            double taxa = 0.01;
            bool TaxaValido = taxa == Domain.Entities.TaxaJuros.taxaAtual;
            Assert.IsTrue(TaxaValido);
        }
    }
}
