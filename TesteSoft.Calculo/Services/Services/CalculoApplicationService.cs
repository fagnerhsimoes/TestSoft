using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TesteSoft.Calculo.Common.Configuration;
using TesteSoft.Calculo.Common.Helpers;
using TesteSoft.Calculo.Common.Infrastructure.Integrations;
using TesteSoft.Calculo.Domain.Entities;
using TesteSoft.Calculo.Services.DTO;

namespace TesteSoft.Calculo.Services.Services
{
    public class CalculoApplicationService : RestfulIntegration
    {
        private readonly string _url;

        public CalculoApplicationService(HttpClient httpClient, IOptions<AppSettings> options) : base(httpClient)
        {
            httpClient.DefaultRequestHeaders.Clear();
            _url = options.Value.Get("TaxaJurosDomain");
        }

        public async Task<Resultado> ExecuteCalculoJuros(CalculoDto dto, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var valorCalculado = await CalculaValorTotalComJuros(dto, ct);
            return new Resultado(valorCalculado);
        }

        private async Task<double> GetTaxaJuros(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var response = await GetAsync($"{_url}/TaxaJuros", ct);
            await HttpClientHelper.HandleResponse(response);
            var result = JsonConvert.DeserializeObject<TaxaJurosDto>(await response.Content.ReadAsStringAsync());
            return result.TaxadeJuros;
        }


        private async Task<string> CalculaValorTotalComJuros(CalculoDto dto, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var TaxaJuros =  await GetTaxaJuros(ct);
            var ValorToralCalculado = dto.ValorInicial * Math.Pow(TaxaJuros + 1, dto.Meses);
            return FormataValorCom2CasasDecimais(ValorToralCalculado);
        }

        private string FormataValorCom2CasasDecimais(double valor)
        {
            decimal valorTotal = Convert.ToDecimal(valor);
            String ValorFormatado = String.Format("{0:N2}", (valorTotal - (valorTotal % 0.01M)));
            return ValorFormatado;
        }

    }
}
