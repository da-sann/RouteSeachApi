using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RouteSeachApi.Mapper.Profiles;
using RouteSeachApi.Modules;

namespace RouteSeachApi {
    public class Startup {
        private readonly string[] DefaultRoutes = new[] { "/" };

        public Startup(IHostEnvironment env) {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                string basePath = AppContext.BaseDirectory;
                string xmlPath = System.IO.Path.Combine(basePath, @"RouteSeachApi.xml");
                if (System.IO.File.Exists(xmlPath))
                    options.IncludeXmlComments(xmlPath);
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGenNewtonsoftSupport();
            ConfigureCompression(services);
        }

        public void ConfigureCompression(IServiceCollection services) {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        public void ConfigureContainer(ContainerBuilder builder) {
            builder.RegisterModule<RequestHandlersModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ProviderServiceModule>();
            builder.RegisterModule(new ShardDbContextModule(Configuration));
            builder.RegisterModule(new AutoMapperModule(cfg =>
            {
                cfg.AddProfile<ModelToModelProfile>();
            }));
            builder.RegisterModule<ValidationModule>();
            builder.RegisterModule(new MigrationModule(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                if (DefaultRoutes.Contains(context.Request.Path.Value.ToLowerInvariant())) {
                    context.Response.Redirect("/api/index.html");
                    return;
                }
                await next();
            });
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> {
                        new OpenApiServer {
                            Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/"
                        }
                    };
                });
            });
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions.OrderByDescending(m => m.ApiVersion.MajorVersion)) {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                c.DisplayOperationId();
                c.DocumentTitle = "RouteSeach API";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
