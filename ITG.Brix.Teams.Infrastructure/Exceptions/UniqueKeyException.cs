using System;

namespace ITG.Brix.Teams.Infrastructure.Exceptions
{
    public sealed class UniqueKeyException : Exception
    {
        public UniqueKeyException() : base("UniqueKey") { }

        internal UniqueKeyException(Exception ex) : base(ex.Message, ex) { }
    }
}
