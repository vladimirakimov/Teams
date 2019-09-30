using System;

namespace ITG.Brix.Teams.Infrastructure.Exceptions
{
    public sealed class FilterODataException : Exception
    {
        public FilterODataException() : base("FilterOData") { }

        internal FilterODataException(Exception ex) : base(ex.Message, ex) { }

        internal FilterODataException(string message) : base(message) { }
    }
}
