using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Internal;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Domain
{
    public class Name : ValueObject<Name>
    {
        private readonly string _text;

        public Name(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw Error.NameFieldShouldNotBeEmpty();
            }

            _text = text;
        }

        public static explicit operator Name(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : new Name(text);
        }

        public static implicit operator string(Name name)
        {
            return name?._text;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _text;
        }
    }
}
