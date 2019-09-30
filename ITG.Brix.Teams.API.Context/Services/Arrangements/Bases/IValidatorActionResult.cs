using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.Teams.API.Context.Services.Arrangements.Bases
{
    public interface IValidatorActionResult
    {
        IActionResult Result { get; }
    }
}
