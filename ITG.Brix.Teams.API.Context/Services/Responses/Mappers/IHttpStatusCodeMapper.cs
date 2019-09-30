using ITG.Brix.Teams.API.Context.Bases;
using System.Net;

namespace ITG.Brix.Teams.API.Context.Services.Responses.Mappers
{
    public interface IHttpStatusCodeMapper
    {
        HttpStatusCode Map(ValidationResult validationResult);
    }
}
