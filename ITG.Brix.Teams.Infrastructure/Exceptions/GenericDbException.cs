using System;

namespace ITG.Brix.Teams.Infrastructure.Exceptions
{
    public sealed class GenericDbException : Exception
    {
        public GenericDbException(Exception ex) : base("Generic", ex) { }
    }
}
