using ITG.Brix.Teams.API.Context.Bases;

namespace ITG.Brix.Teams.API.Context.Services.Requests
{
    public interface IApiRequest
    {
        ValidationResult Validate<T>(T request);
    }
}
