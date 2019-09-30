using ITG.Brix.Teams.Domain.Exceptions.Bases;
using ITG.Brix.Teams.Domain.Internal;

namespace ITG.Brix.Teams.Domain.Exceptions
{
    public sealed class NameFieldShouldNotBeEmptyException : DomainException
    {
        internal NameFieldShouldNotBeEmptyException() : base(Consts.DomainExceptionMessage.NameFieldShouldNotBeEmpty) { }
    }
}
