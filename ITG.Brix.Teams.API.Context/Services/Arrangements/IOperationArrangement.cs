using ITG.Brix.Teams.API.Context.Services.Arrangements.Bases;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.API.Context.Services.Arrangements
{
    public interface IOperationArrangement
    {
        Task<IActionResult> Process(ListTeamRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(GetTeamRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(CreateTeamRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(UpdateTeamRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(DeleteTeamRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListOperatorRequest request, IValidatorActionResult validatorActionResult);
        Task<IActionResult> Process(ListDriverWaitRequest request, IValidatorActionResult validatorActionResult);
    }
}
