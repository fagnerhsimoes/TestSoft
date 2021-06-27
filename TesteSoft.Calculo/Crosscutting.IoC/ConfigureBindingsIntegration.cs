using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using TesteSoft.Calculo.Services.Services;

namespace TesteSoft.Calculo.Crosscutting.IoC
{
    public static class ConfigureBindingsIntegration
	{
		public static void RegisterBindings(IServiceCollection services)
		{

			services.AddHttpClient<CalculoApplicationService>()
				.AddPolicyHandler(GetRetryPolicy())
				.SetHandlerLifetime(TimeSpan.FromMinutes(5));

		}

		private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
			HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
	}
}
