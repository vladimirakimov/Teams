using System;

namespace ITG.Brix.Teams.Infrastructure.Exceptions
{
    public sealed class EntityNotFoundDbException : Exception
    {
        public EntityNotFoundDbException() : base("EntityNotFound") { }
    }
}
