using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProfileMicroservice.HttpService;
using ProfileMicroservice.Filters;
using ProfileMicroservice.Repository;
using Microsoft.Extensions.Logging;
using System;

namespace ProfileMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(config =>
            {
                config.Filters.Add(typeof(ProfileAPIExceptionAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
             .AddJsonFormatters();

            services.AddMemoryCache();
            //services.AddApplicationInsightsTelemetry(Configuration);
            services.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddSwaggerGen(options =>
            {
                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

            });

            services.Scan(scan => scan.FromAssemblies(ProfileMicroservice.Assembly)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddHttpClient<IHttpClientContext, HttpClientContext>();
            services.AddScoped<IProfileServiceRepository, ProfileServiceRepository>();
            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(
                  options =>
                  {
                      foreach (var description in provider.ApiVersionDescriptions)
                      {
                          options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                      }
                  });
        }

        private static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"Profileservice {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "Profileservice with Swagger, Swashbuckle, and API versioning.",
            };

            if (description.IsDeprecated)
            {
                info.Description = "Profileservice with Swagger, Swashbuckle, Version" + description.ApiVersion.ToString() + " has been deprecated.";
            }

            return info;
        }
    }
}
