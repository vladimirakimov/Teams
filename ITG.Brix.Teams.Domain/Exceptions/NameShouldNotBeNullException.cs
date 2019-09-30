using ITG.Brix.Teams.Domain.Exceptions.Bases;
using ITG.Brix.Teams.Domain.Internal;

namespace ITG.Brix.Teams.Domain.Exceptions
{
    public sealed class NameShouldNotBeNullException : DomainException
    {
        internal NameShouldNotBeNullException() : base(Consts.DomainExceptionMessage.NameShouldNotBeNull) { }
    }
}
