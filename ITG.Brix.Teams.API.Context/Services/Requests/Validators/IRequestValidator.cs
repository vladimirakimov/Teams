using ITG.Brix.Teams.API.Context.Bases;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Validators
{
    public interface IRequestValidator
    {
        ValidationResult Validate<T>(T request);

        Type Type { get; }
    }
}
