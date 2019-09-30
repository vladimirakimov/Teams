using FluentValidation;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Extensions;
using ITG.Brix.Teams.Application.Resources;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Validators
{
    public class CreateOperatorCommandValidator : AbstractValidator<CreateOperatorCommand>
    {
        public CreateOperatorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().ValidationFault(ValidationFailures.OperatorIdMandatory);
            RuleFor(x => x.Login).NotEmpty().ValidationFault(ValidationFailures.OperatorLoginMandatory);
            RuleFor(x => x.FirstName).NotEmpty().ValidationFault(ValidationFailures.OperatorFirstNameMandatory);
            RuleFor(x => x.LastName).NotEmpty().ValidationFault(ValidationFailures.OperatorLastNameMandatory);
        }
    }
}
