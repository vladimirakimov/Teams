using ITG.Brix.Teams.API.Context.Bases;
using ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors;

namespace ITG.Brix.Teams.API.Context.Services.Responses.Mappers
{
    public interface IErrorMapper
    {
        ResponseError Map(ValidationResult validationResult);
    }
}
