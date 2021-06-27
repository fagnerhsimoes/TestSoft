using System.Threading;
using System.Threading.Tasks;
using TesteSoft.Taxa.Domain.Entities;

namespace TesteSoft.Taxa.Services.Services
{
    public class TaxaJurosApplicationService
	{
		public async Task<TaxaJuros> GetTaxaJuros(CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return new TaxaJuros();
		}
	}
}
