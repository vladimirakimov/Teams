using Autofac;
using ITG.Brix.Teams.Application.Behaviors;
using MediatR;

namespace ITG.Brix.Teams.DependencyResolver.AutofacModules
{
    public class BehaviorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
