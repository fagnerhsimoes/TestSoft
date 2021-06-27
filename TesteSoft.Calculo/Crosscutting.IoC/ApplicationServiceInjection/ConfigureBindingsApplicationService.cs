using Microsoft.Extensions.DependencyInjection;
using TesteSoft.Calculo.Services.Services;

namespace TesteSoft.Calculo.Crosscutting.IoC.ApplicationServiceInjection
{
    public static class ConfigureBindingsApplicationService
    {
        public static void RegisterBindings(IServiceCollection services)
        {
            services.AddScoped<CalculoApplicationService, CalculoApplicationService>();
        }
    }
}
