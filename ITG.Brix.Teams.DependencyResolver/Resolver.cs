using Autofac;
using Autofac.Extensions.DependencyInjection;
using ITG.Brix.Teams.DependencyResolver.AutofacModules;
using ITG.Brix.Teams.Domain;
using Microsoft.AspNet.OData.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;

namespace ITG.Brix.Teams.DependencyResolver
{
    public static class Resolver
    {
        public static ContainerBuilder BuildServiceProvider(IServiceCollection services, string connectionString)
        {
            services
                .AutoMapper()
                .Providers()
                .Persistence(connectionString)
                .ApiServices()
                .RequestValidators();

            var containerBuilder = BuildContainer(services);
            return containerBuilder;
        }

        private static ContainerBuilder BuildContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder
                .RegisterModule(new MediatorModule())
                .RegisterModule(new CommandHandlerModule())
                .RegisterModule(new ValidatorModule())
                .RegisterModule(new BehaviorModule());

            return builder;
        }

        public static IEdmModel ConfigureOData()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Team>("Teams").EntityType.HasKey(x => x.Id);

            var model = builder.GetEdmModel();

            return model;
        }
    }
}
