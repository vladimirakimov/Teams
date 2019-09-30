using Newtonsoft.Json;

namespace ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors
{
    public class ResponseErrorField
    {
        /// <summary>
        /// Gets or sets property level error code.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets human-readable representation of property-level error.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets property name.
        /// </summary>
        [JsonProperty(PropertyName = "target")]
        public string Target { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ResponseErrorField;
            return other != null &&
                   Code == other.Code &&
                   Message == other.Message &&
                   Target == other.Target;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode += this.Code.GetHashCode();
                hashCode += this.Message.GetHashCode();
                hashCode += this.Target.GetHashCode();
                return hashCode;
            }
        }
    }
}
