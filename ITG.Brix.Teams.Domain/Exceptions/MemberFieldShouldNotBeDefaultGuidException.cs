using ITG.Brix.Teams.Domain.Exceptions.Bases;
using ITG.Brix.Teams.Domain.Internal;

namespace ITG.Brix.Teams.Domain.Exceptions
{
    public sealed class MemberFieldShouldNotBeDefaultGuidException : DomainException
    {
        internal MemberFieldShouldNotBeDefaultGuidException() : base(Consts.DomainExceptionMessage.MemberFieldShouldNotBeDefaultGuid) { }
    }
}
