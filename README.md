# SvelteMiddleware

[![](https://img.shields.io/nuget/v/SvelteMiddleware.svg)](https://www.nuget.org/packages/SvelteMiddleware/)

A helper for building single-page applications on ASP.NET MVC Core using Svelte/Sapper.

## Usage Example

```csharp
    using SvelteMiddleware;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
           services.AddMvc(); // etc

           // Need to register ISpaStaticFileProvider for UseSpaStaticFiles middleware to work
           services.AddSpaStaticFiles(configuration =>
           {
               configuration.RootPath = "ClientApp/dist";
           });
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           // your config opts...

           // add static files from SPA (/dist)
          app.UseSpaStaticFiles();

          app.UseMvc(routes => /* configure*/ );

          app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
#if DEBUG
                if (env.IsDevelopment())
                {
                    spa.UseSvelte(npmScript: "serve", port: 8080); // optional port
                }
#endif
            });
        }
    }
```

## History

This was adapted from [VueCliMiddleware](https://github.com/EEParker/aspnetcore-vueclimiddleware).
