using ITG.Brix.Teams.API.Context.Services;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.API.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OperatorsController : Controller
    {
        private readonly IApiResult _apiResult;

        public OperatorsController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperatorsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListOperatorFromQuery query)
        {
            var request = new ListOperatorRequest(query);
            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}
