using ITG.Brix.Teams.Domain.Exceptions.Bases;
using ITG.Brix.Teams.Domain.Internal;

namespace ITG.Brix.Teams.Domain.Exceptions
{
    public sealed class LayoutFieldShouldNotBeDefaultGuidException : DomainException
    {
        internal LayoutFieldShouldNotBeDefaultGuidException() : base(Consts.DomainExceptionMessage.LayoutFieldShouldNotBeDefaultGuid) { }
    }
}
