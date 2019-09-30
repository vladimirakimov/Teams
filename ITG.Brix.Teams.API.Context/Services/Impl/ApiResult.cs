using ITG.Brix.Teams.API.Context.Services.Arrangements;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.API.Context.Services.Impl
{
    public class ApiResult : IApiResult
    {
        private readonly IValidationArrangement _validationArrangement;
        private readonly IOperationArrangement _operationArrangement;

        public ApiResult(IValidationArrangement validationArrangement,
                         IOperationArrangement operationArrangement)
        {
            _validationArrangement = validationArrangement ?? throw new ArgumentNullException(nameof(validationArrangement));
            _operationArrangement = operationArrangement ?? throw new ArgumentNullException(nameof(operationArrangement));
        }

        public async Task<IActionResult> Produce(ListTeamRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(GetTeamRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(CreateTeamRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(DeleteTeamRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(UpdateTeamRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(ListOperatorRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }

        public async Task<IActionResult> Produce(ListDriverWaitRequest request)
        {
            var validatorActionResult = await _validationArrangement.Validate(request);
            var actionResult = await _operationArrangement.Process(request, validatorActionResult);

            return actionResult;
        }
    }
}
