using ITG.Brix.Teams.Domain.Exceptions.Bases;

namespace ITG.Brix.Teams.Domain.Exceptions
{
    public sealed class InvalidIdentityException : DomainException
    {
        internal InvalidIdentityException(string message) : base(message) { }
    }
}
