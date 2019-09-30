using FluentValidation;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Extensions;
using ITG.Brix.Teams.Application.Resources;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Validators
{
    public class GetTeamQueryValidator : AbstractValidator<GetTeamQuery>
    {
        public GetTeamQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.TeamNotFound);
        }
    }
}
