using Autofac;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using MediatR;
using System.Reflection;

namespace ITG.Brix.Teams.DependencyResolver.AutofacModules
{
    public class CommandHandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateTeamCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}
