using Microsoft.Extensions.DependencyInjection;
using ProxyEngine.Contract;
using ProxyEngine.Contract.Dtos;
using System;

namespace ProxyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ProxyInstall();
            serviceCollection.LDAPInstall();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var proxyFactory = serviceProvider.GetService<IProxyFactory>();

            var proxy = proxyFactory.GetProxy((int)1, new ProxyRequest());
            var proxyResponse = proxy.Authenticate(new ProxyAuthenticateRequest
            {
                User = new ProxyUser
                {
                    Username = "",
                    Password = ""
                },
                TrackId = Guid.NewGuid()
            });
        }
    }
}
