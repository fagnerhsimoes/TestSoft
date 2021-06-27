using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TesteSoft.Calculo.Services.DTO;
using TesteSoft.Calculo.Services.Services;

namespace TesteSoft.Calculo.Controllers
{
    [ApiController]
	[Route("calculajuros")]
	public class CalculoJurosController : ControllerBase
	{
		private readonly CalculoApplicationService _appService;

		public CalculoJurosController(CalculoApplicationService appService) => _appService = appService;

		/// <summary>
		/// Retorna o valor total com juros após o cálculo.
		/// </summary>
		/// <param name="cancellationToken">Token de cancelamento da requisição</param>
		/// <returns>Resultado do Cálculo</returns>
		/// <response code="200">Cálculo realizado com sucesso.</response>
		/// <response code="408">Requisição abortada.</response>
		/// <response code="500">Não foi possível efetuar o cálculo de juros.</response>
		[HttpPost]
		public async Task<IActionResult> ExecuteCalculoJuros([FromBody] CalculoDto dto, CancellationToken cancellationToken)
		{
			var result = await _appService.ExecuteCalculoJuros(dto, cancellationToken);
			return Ok(result);
		}
	}
}
