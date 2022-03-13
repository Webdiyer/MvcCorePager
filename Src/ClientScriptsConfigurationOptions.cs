using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;

#if NETSTANDARD2_0
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
#else
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
#endif

namespace Webdiyer.AspNetCore
{
    internal class ClientScriptsConfigurationOptions : IPostConfigureOptions<StaticFileOptions>
    {
        public ClientScriptsConfigurationOptions(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public IWebHostEnvironment Environment { get; }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;
            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "wwwroot");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}
