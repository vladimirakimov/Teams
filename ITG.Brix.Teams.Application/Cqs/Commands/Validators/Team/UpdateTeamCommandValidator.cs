using FluentValidation;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Extensions;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using System.Linq;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Validators
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.TeamNotFound);
            RuleFor(x => x.Name).NotEmpty().ValidationFault(ValidationFailures.TeamNameMandatory);

            RuleFor(x => x.DriverWait).Custom((elem, context) =>
            {
                var driverWait = DriverWait.List().SingleOrDefault(x => x.Name == elem);
                if (driverWait == null)
                {
                    context.AddValidationFault("DriverWait", ValidationFailures.TeamDriverWaitWrongValue);
                }
            });
        }
    }
}
