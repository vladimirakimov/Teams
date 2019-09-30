using ITG.Brix.Teams.API.Context.Bases;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Validators.Impl.Bases;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Validators.Impl
{
    public class DeleteTeamRequestValidator : AbstractRequestValidator<DeleteTeamRequest>
    {
        private readonly IRequestComponentValidator _requestComponentValidator;

        public DeleteTeamRequestValidator(IRequestComponentValidator requestComponentValidator)
        {
            _requestComponentValidator = requestComponentValidator ?? throw new ArgumentNullException(nameof(requestComponentValidator));
        }
        public override ValidationResult Validate<T>(T request)
        {
            var req = request as DeleteTeamRequest;
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
                result = _requestComponentValidator.HeaderIfMatchRequired(req.HeaderIfMatch);
            }

            if (result == null)
            {
                result = _requestComponentValidator.HeaderIfMatch(req.HeaderIfMatch);
            }

            if (result == null)
            {
                result = new ValidationResult();
            }

            return result;
        }
    }
}
