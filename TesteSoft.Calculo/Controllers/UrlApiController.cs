using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TesteSoft.Calculo.Crosscutting.IoC;

namespace TesteSoft.Calculo.Controllers
{
    [ApiController]
	[Route("showmethecode")]
	public class UrlApiController : ControllerBase
	{
		/// <summary>
		/// Retorna a url do local que está o código fonte da API de cálculo de juros.
		/// </summary>
		/// <param name="cancellationToken">Token de cancelamento da requisição</param>
		/// <returns>URL da API do Cálculo de Juros</returns>
		/// <response code="200">Consulta realizada com sucesso.</response>
		/// <response code="408">Requisição abortada.</response>
		/// <response code="500">Não foi possível obter a URL da API de cálculo de juros.</response>
		[HttpGet]
	    public async Task<IActionResult> GetUrlApiCalculoJuros(CancellationToken ct)
		{
			ct.ThrowIfCancellationRequested();
			var result = Constants.Urls.UrlApi;
			return Ok(result);
		}
	}
}
