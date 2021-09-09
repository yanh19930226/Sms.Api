using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Sms.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Sms.Api
{
    public static class DependenceExtension
    {
        public static List<Type> GetTypeOfISerice(this AssemblyName assemblyName)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName).ExportedTypes.Where(b => b.GetInterfaces().Contains(typeof(IService)) && !b.IsAbstract).ToList();
        }

        public static void AddService(this IServiceCollection services)
        {
            var defaultAssemblyNames = DependencyContext.Default.GetDefaultAssemblyNames().Where(a => a.FullName.Contains("Sms.Api.Service")).ToList();

            var assemblies = defaultAssemblyNames.SelectMany(a => a.GetTypeOfISerice()).ToList();

            assemblies.ForEach(assembliy =>
            {
                services.AddScoped(assembliy);
            });
        }
    }
}
