using System;

namespace ITG.Brix.Teams.Infrastructure.Exceptions
{
    public sealed class EntityVersionDbException : Exception
    {
        public EntityVersionDbException() : base("EntityVersion") { }
    }
}
