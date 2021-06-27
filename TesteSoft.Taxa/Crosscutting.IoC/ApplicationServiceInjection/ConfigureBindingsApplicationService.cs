using Microsoft.Extensions.DependencyInjection;
using TesteSoft.Taxa.Services.Services;

namespace TesteSoft.Taxa.Crosscutting.IoC.ApplicationServiceInjection
{
    public static class ConfigureBindingsApplicationService
    {
        public static void RegisterBindings(IServiceCollection services)
        {
            services.AddScoped<TaxaJurosApplicationService, TaxaJurosApplicationService>();
        }
    }
}
