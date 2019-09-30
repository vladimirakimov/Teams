using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Internal;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Domain
{
    public class Description : ValueObject<Description>
    {
        private readonly string _text;

        public Description(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw Error.DescriptionFieldShouldNotBeEmpty();
            }

            _text = text;
        }

        public static explicit operator Description(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : new Description(text);
        }

        public static implicit operator string(Description description)
        {
            return description?._text;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _text;
        }
    }
}
