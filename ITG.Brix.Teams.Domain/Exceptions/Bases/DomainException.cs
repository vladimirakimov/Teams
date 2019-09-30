using System;

namespace ITG.Brix.Teams.Domain.Exceptions.Bases
{
    public class DomainException : Exception
    {
        internal DomainException(string message) : base(message) { }
    }
}
