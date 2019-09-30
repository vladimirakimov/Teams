using Autofac;
using FluentValidation;
using ITG.Brix.Teams.Application.Cqs.Commands.Validators;
using System.Reflection;

namespace ITG.Brix.Teams.DependencyResolver.AutofacModules
{
    public class ValidatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(CreateTeamCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        }
    }
}
