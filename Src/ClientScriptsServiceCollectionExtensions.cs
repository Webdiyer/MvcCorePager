using Microsoft.Extensions.DependencyInjection;

namespace Webdiyer.AspNetCore
{
    public static class ClientScriptsServiceCollectionExtensions
    {
        /// <summary>
        /// Use mvccorepager scripts service
        /// </summary>
        /// <param name="services"></param>
        public static void UseMvcCorePagerScripts(this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(ClientScriptsConfigurationOptions));
        }
    }
}
