using ITG.Brix.Teams.API.Context.Services.Arrangements.Bases;
using ITG.Brix.Teams.API.Context.Services.Requests.Mappers;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Responses;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.API.Context.Services.Arrangements.Impl
{
    public class OperationArrangement : IOperationArrangement
    {
        private readonly IMediator _mediator;
        private readonly IApiResponse _apiResponse;
        private readonly ICqsMapper _cqsMapper;

        public OperationArrangement(IMediator mediator,
                                    IApiResponse apiResponse,
                                    ICqsMapper cqsMapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _apiResponse = apiResponse ?? throw new ArgumentNullException(nameof(apiResponse));
            _cqsMapper = cqsMapper ?? throw new ArgumentNullException(nameof(cqsMapper));
        }

        public async Task<IActionResult> Process(ListTeamRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                request.UpdateFilter(fromToSet: new Dictionary<string, string>{
                        { "image", "dissalowed"},
                        { "description", "dissalowed"},
                        { "layout", "dissalowed"},
                        { "filterContent", "dissalowed"},
                        { "version", "dissalowed"},
                    });

                var query = _cqsMapper.Map(request);
                var result = await _mediator.Send(query);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<TeamsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(GetTeamRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var query = _cqsMapper.Map(request);
                var result = await _mediator.Send(query);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<TeamModel>)result).Value, ((Result<TeamModel>)result).Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(CreateTeamRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var command = _cqsMapper.Map(request);
                var result = await _mediator.Send(command);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Created($"/api/teams/{((Result<Guid>)result).Value}", result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(UpdateTeamRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var command = _cqsMapper.Map(request);
                var result = await _mediator.Send(command);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Updated(result.Version.ToString());
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(DeleteTeamRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var command = _cqsMapper.Map(request);
                var result = await _mediator.Send(command);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Deleted();
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(ListOperatorRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var query = _cqsMapper.Map(request);
                var result = await _mediator.Send(query);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<OperatorsModel>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }

        public async Task<IActionResult> Process(ListDriverWaitRequest request, IValidatorActionResult validatorActionResult)
        {
            IActionResult actionResult;

            if (validatorActionResult.Result == null)
            {
                var query = _cqsMapper.Map(request);
                var result = await _mediator.Send(query);

                actionResult = result.IsFailure ? _apiResponse.Fail(result)
                                                : _apiResponse.Ok(((Result<IEnumerable<string>>)result).Value);
            }
            else
            {
                actionResult = validatorActionResult.Result;
            }

            return actionResult;
        }
    }
}
