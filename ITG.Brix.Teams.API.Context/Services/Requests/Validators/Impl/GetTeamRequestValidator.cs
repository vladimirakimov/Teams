using ITG.Brix.Teams.API.Context.Bases;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Validators.Impl.Bases;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Validators.Impl
{
    public class GetTeamRequestValidator : AbstractRequestValidator<GetTeamRequest>
    {
        private readonly IRequestComponentValidator _requestComponentValidator;

        public GetTeamRequestValidator(IRequestComponentValidator requestComponentValidator)
        {
            _requestComponentValidator = requestComponentValidator ?? throw new ArgumentNullException(nameof(requestComponentValidator));
        }

        public override ValidationResult Validate<T>(T request)
        {
            var req = request as GetTeamRequest;
            ValidationResult result;

            result = _requestComponentValidator.RouteId(req.RouteId);

            if (result == null)
            {
                result = _requestComponentValidator.QueryApiVersionRequired(req.QueryApiVersion);
            }

            if (result == null)
            {
                result = _requestComponentValidator.QueryApiVersion(req.QueryApiVersion);
            }

            if (result == null)
            {
                result = new ValidationResult();
            }

            return result;
        }
    }
}
