using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteSoft.Calculo.Crosscutting.IoC.ApplicationServiceInjection;

namespace TesteSoft.Calculo.Crosscutting.IoC
{
    public static class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureBindingsIntegration.RegisterBindings(services);
            ConfigureBindingsApplicationService.RegisterBindings(services);
        }
    }
}
