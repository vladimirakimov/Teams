using ITG.Brix.Teams.API.Context.Bases;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Validators.Impl.Bases;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Validators.Impl
{
    public class ListDriverWaitRequestValidator : AbstractRequestValidator<ListDriverWaitRequest>
    {
        private readonly IRequestComponentValidator _requestComponentValidator;

        public ListDriverWaitRequestValidator(IRequestComponentValidator requestComponentValidator)
        {
            _requestComponentValidator = requestComponentValidator ?? throw new ArgumentNullException(nameof(requestComponentValidator));
        }

        public override ValidationResult Validate<T>(T request)
        {
            var req = request as ListDriverWaitRequest;
            ValidationResult result;

            result = _requestComponentValidator.QueryApiVersionRequired(req.QueryApiVersion);

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
