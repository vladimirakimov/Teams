using FluentValidation;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Extensions;
using ITG.Brix.Teams.Application.Resources;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Validators
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().ValidationFault(ValidationFailures.TeamNameMandatory);
        }
    }
}
