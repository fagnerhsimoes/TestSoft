using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TesteSoft.Taxa.Services.Services;

namespace TesteSoft.Taxa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxaJurosController : ControllerBase
    {
		private readonly TaxaJurosApplicationService _appService;

		public TaxaJurosController(TaxaJurosApplicationService appService) => _appService = appService;

		/// <summary>
		/// Busca pela Taxa de Juros.
		/// </summary>
		/// <param name="cancellationToken">Token de cancelamento da requisição</param>
		/// <returns>Taxa de Juros</returns>
		/// <response code="200">Busca realizada com sucesso.</response>
		/// <response code="404">Taxa de Juros não encontrada.</response>
		/// <response code="408">Requisição abortada.</response>
		[HttpGet]
		public async Task<IActionResult> GetTaxaJuros(CancellationToken cancellationToken)
		{
			var result = await _appService.GetTaxaJuros(cancellationToken);
			return Ok(result);
		}
	}
}
