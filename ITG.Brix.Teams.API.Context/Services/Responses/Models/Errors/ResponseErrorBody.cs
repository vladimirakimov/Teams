using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors
{
    public class ResponseErrorBody
    {
        /// <summary>
        /// Gets or sets service-defined error code. This code serves as a sub-status for the HTTP error code specified in the response.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets human-readable representation of the error.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the list of invalid fields send in request, in case of validation error.
        /// </summary>
        [JsonProperty(PropertyName = "details")]
        public IList<ResponseErrorField> Details { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            bool result = false;

            var other = obj as ResponseErrorBody;

            if (other != null)
            {
                if (other.Code != this.Code)
                {
                    return false;
                }

                if (other.Message != this.Message)
                {
                    return false;
                }

                IList<ResponseErrorField> list = other.Details;
                if (list == null && this.Details == null)
                {
                    return true;
                }

                if (list == null || this.Details == null)
                {
                    return false;
                }

                if (list.Count != this.Details.Count)
                {
                    return false;
                }

                foreach (var item in this.Details)
                {
                    result = list.Any(x => x.Equals(item));
                    if (!result)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode += this.Code.GetHashCode();
                hashCode += this.Message.GetHashCode();
                foreach (var item in this.Details)
                {
                    hashCode += item.GetHashCode();
                }
                return hashCode;
            }
        }
    }
}
