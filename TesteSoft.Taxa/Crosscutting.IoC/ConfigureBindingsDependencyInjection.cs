using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteSoft.Taxa.Crosscutting.IoC.ApplicationServiceInjection;

namespace TesteSoft.Taxa.Crosscutting.IoC
{
    public static class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureBindingsApplicationService.RegisterBindings(services);
        }
    }
}
