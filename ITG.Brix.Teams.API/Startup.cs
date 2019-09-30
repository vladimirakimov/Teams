using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using ITG.Brix.Teams.API.Constants;
using ITG.Brix.Teams.API.Middleware;
using ITG.Brix.Teams.DependencyResolver;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NServiceBus;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.API
{
    public class Startup
    {
        IEndpointInstance endpoint;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                     .AddJsonOptions(options =>
                     {
                         options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                     })
                     .AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Teams API", Version = "v1" });
            });

            services.AddCors();

            var connectionString = Configuration[Consts.Configuration.ConnectionString];

            var builder = Resolver.BuildServiceProvider(services, connectionString);

            builder.Register(c => endpoint)
                .As<IMessageSession>()
                .SingleInstance();

            var container = builder.Build();
            endpoint = BootstrapNServiceBusForMessaging(container);

            return new AutofacServiceProvider(container);
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            app.UseDeveloperExceptionPage();
            Configure(app, env, appLifetime);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                                    .AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teams API v1");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();

            app.Run(context =>
            {
                context.Response.Redirect("swagger");
                return Task.CompletedTask;
            });

            appLifetime.ApplicationStopped.Register(() => endpoint.Stop().GetAwaiter().GetResult());
        }

        IEndpointInstance BootstrapNServiceBusForMessaging(IContainer container)
        {
            var endpointConfiguration = new EndpointConfiguration("Teams.API");

            endpointConfiguration.UseContainer<AutofacBuilder>(customizations: customizations =>
                                                              {
                                                                  customizations.ExistingLifetimeScope(container);
                                                              });

            var isRabbitMQEnabled = Configuration[Consts.Configuration.RabbitMQEnabled] == "true";

            if (isRabbitMQEnabled)
            {
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                var connectionString = Configuration[Consts.Configuration.RabbitMQConnectionString];
                transport.ConnectionString(connectionString);
                transport.UsePublisherConfirms(true);
                transport.UseDirectRoutingTopology();
            }

            var isAzureServiceBusEnabled = Configuration[Consts.Configuration.AzureServiceBusEnabled] == "true";

            if (isAzureServiceBusEnabled)
            {
                var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
                var connectionString = Configuration[Consts.Configuration.AzureServiceBusConnectionString];
                transport.ConnectionString(connectionString);
            }

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var serialization = endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            serialization.Settings(settings);

            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.EnableInstallers();

            return Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
