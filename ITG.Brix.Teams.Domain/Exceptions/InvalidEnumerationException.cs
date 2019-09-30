using ITG.Brix.Teams.Domain.Exceptions.Bases;

namespace ITG.Brix.Teams.Domain.Exceptions
{
    public sealed class InvalidEnumerationException : DomainException
    {
        internal InvalidEnumerationException(string message) : base(message) { }
    }
}
