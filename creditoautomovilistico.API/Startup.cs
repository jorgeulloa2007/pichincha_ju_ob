using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using Microsoft.OpenApi.Models;
using creditoautomovilistico.Infrastructure.Context;
using creditoautomovilistico.Repository.Repositories;
using nombremicroservicio.Domain.Interfaces;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.API.Mapper;
using creditoautomovilistico.Domain.Services;

namespace nombremicroservicio.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region OPENTELEMETRY
            string otelServer = Environment.GetEnvironmentVariable("OTEL_SERVER");
            var serviceName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            services.AddHttpContextAccessor();
            if (!String.IsNullOrEmpty(otelServer))
            {
                services.AddOpenTelemetryTracing(builder =>
                   builder
                  .AddAspNetCoreInstrumentation()
                  .AddHttpClientInstrumentation()
                  .AddConsoleExporter()
                  .AddZipkinExporter(options =>
                  {
                      options.Endpoint = new Uri($"http://{otelServer}:9411/api/v2/spans");
                  })
                  .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                );
            }

            services.AddLogging(logging =>
            {
                logging.AddOpenTelemetry(options =>
                {
                    options.AddConsoleExporter();
                });
            });

            #endregion OPENTELEMETRY

            #region INFRASTRUCTURE

            services.AddDbContext<DatabaseContext>();

            #endregion INFRASTRUCTURE

            #region COMPATIBILITY

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #endregion COMPATIBILITY

            #region HANDLING API VERSIONS
            services.AddApiVersioning(options => options.UseApiBehavior = true);
            services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);
            #endregion HANDLING API VERSIONS

            #region POLICY FOR CROSS DOMAIN
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                   .AllowAnyHeader()));
            #endregion POLICY FOR CROSS DOMAIN

            services.AddControllers();
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "creditoautomovilistico",
                    Description = "Registro de solicitudes de cr�dito",
                    Contact = new OpenApiContact
                    {
                        Name = "Banco Pichincha",
                        Email = "devops@pichincha.com",
                        Url = new Uri("https://www.pichincha.com"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            #endregion Swagger

            services.AddAutoMapper(
                                    typeof(APIMappingProfile),
                                    typeof(RepositoryMappingProfile)
                                   );

            services.AddScoped<IPatioRepository, PatioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IVehiculoRepository, VehiculoRepository>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVehiculoService, VehiculoService>();
            services.AddScoped<IPatioService, PatioService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region SwaggerUI
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "creditoautomovilistico API");
                c.RoutePrefix = "swagger";
                c.InjectStylesheet("/swagger/custom.css");
            });
            #endregion Swagger

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dataContext.Seed();
        }
    }
}
