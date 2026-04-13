using System.Reflection;

namespace ServiceHandle;

internal sealed class ServiceRegistrationHelper
{
    public static IEnumerable<ServiceImplementationType> GetServiceImplementationTypes(Assembly assembly)
    {
        static bool interfaceNotNull(Type w)
           => w.GetInterface(nameof(IRequestServiceHandle)) is not null;

        var types = assembly.GetTypes().Where(interfaceNotNull);
        var implementationTypes = types.Where(w => w.IsClass && !w.IsAbstract);

        var items = new List<ServiceImplementationType>();
        foreach (var implementationType in implementationTypes)
        {
            var interfaceTypes = implementationType.GetInterfaces()
                .Where(f => f.Name.Contains(nameof(IRequestServiceHandle))
                    && f != typeof(IRequestServiceHandle)
                );

            foreach (var serviceType in interfaceTypes)
            {
                items.Add(new(serviceType, implementationType));
            }
        }

        return items;
    }

    internal class ServiceImplementationType
    {
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }

        public ServiceImplementationType() { }

        public ServiceImplementationType(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }
    }
}