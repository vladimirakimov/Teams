using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.API.Context.Services
{
    public interface IApiResult
    {
        Task<IActionResult> Produce(ListTeamRequest request);
        Task<IActionResult> Produce(GetTeamRequest request);
        Task<IActionResult> Produce(CreateTeamRequest request);
        Task<IActionResult> Produce(DeleteTeamRequest reuqest);
        Task<IActionResult> Produce(UpdateTeamRequest request);
        Task<IActionResult> Produce(ListOperatorRequest request);
        Task<IActionResult> Produce(ListDriverWaitRequest request);
    }
}
