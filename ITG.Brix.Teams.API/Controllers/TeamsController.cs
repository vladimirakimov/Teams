using ITG.Brix.Teams.API.Context.Services;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.Teams.API.Extensions;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.API.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly IApiResult _apiResult;

        public TeamsController(IApiResult apiResult)
        {
            _apiResult = apiResult ?? throw new ArgumentNullException(nameof(apiResult));
        }

        [HttpGet]
        [ProducesResponseType(typeof(TeamsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List([FromQuery] ListTeamFromQuery query)
        {
            var request = new ListTeamRequest(query);
            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TeamModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetTeamFromRoute route,
                                             [FromQuery] GetTeamFromQuery query)
        {

            var request = new GetTeamRequest(route, query);
            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromQuery] CreateTeamFromQuery query,
                                                [FromBody] CreateTeamFromBody body)
        {
            var request = new CreateTeamRequest(query, body);
            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Delete([FromRoute] DeleteTeamFromRoute route,
                                                [FromQuery] DeleteTeamFromQuery query,
                                                [FromHeader] DeleteTeamFromHeader header)
        {
            var request = new DeleteTeamRequest(route, query, header);
            var result = await _apiResult.Produce(request);

            return result;

        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> Update([FromRoute] UpdateTeamFromRoute route,
                                                [FromQuery] UpdateTeamFromQuery query,
                                                [FromHeader] UpdateTeamFromHeader header)
        {
            var bodyString = await Request.GetRawBodyStringAsync();
            var body = JsonConvert.DeserializeObject<UpdateTeamFromBody>(bodyString);
            body.Put = bodyString;
            var request = new UpdateTeamRequest(header, query, route, body);
            var result = await _apiResult.Produce(request);

            return result;
        }

        [HttpGet("DriverWait")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDriverWait([FromQuery]ListDriverWaitFromQuery query)
        {
            var request = new ListDriverWaitRequest(query);
            var result = await _apiResult.Produce(request);

            return result;
        }
    }
}