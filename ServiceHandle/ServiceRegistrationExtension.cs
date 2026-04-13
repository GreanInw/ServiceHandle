using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ServiceHandle;

public static class ServiceRegistrationExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddHandleServices(Assembly assembly)
        {
            services.RegisterServiceHandles(assembly);
            services.RegisterServiceHandleProcessor();
            return services;
        }

        private void RegisterServiceHandles(Assembly assembly)
        {
            var serviceImplementationTypes = ServiceRegistrationHelper.GetServiceImplementationTypes(assembly);
            foreach (var item in serviceImplementationTypes)
            {
                services.AddScoped(item.ServiceType, item.ImplementationType);
            }
        }

        private void RegisterServiceHandleProcessor()
        {
            services.AddScoped<IServiceHandleProcessor>(provider => new ServiceHandleProcessor(provider));
        }
    }
}
