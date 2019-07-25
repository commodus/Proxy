using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using ProxyEngine;
using ProxyEngine.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProxyTest
{
    public static class ProxyInstaller
    {
        public static void ProxyInstall(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IProxyFactory), typeof(ProxyFactory));

            services.RegisterAllTypes<IProxy>();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = from a in GetReferencingAssemblies()
                                      from t in a.GetTypes()
                                      where t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IProxy)) && t.BaseType != (typeof(Object))
                                      select t;

            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (Exception ex)
                { }
            }
            return assemblies;
        }
    }
}
