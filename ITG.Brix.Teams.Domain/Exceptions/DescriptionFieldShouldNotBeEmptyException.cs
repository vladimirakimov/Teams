using ITG.Brix.Teams.Domain.Exceptions.Bases;
using ITG.Brix.Teams.Domain.Internal;

namespace ITG.Brix.Teams.Domain.Exceptions
{
    public sealed class DescriptionFieldShouldNotBeEmptyException : DomainException
    {
        internal DescriptionFieldShouldNotBeEmptyException() : base(Consts.DomainExceptionMessage.DescriptionFieldShouldNotBeEmpty) { }
    }
}
