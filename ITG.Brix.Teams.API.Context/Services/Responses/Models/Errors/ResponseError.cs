using Newtonsoft.Json;
using System.Collections.Generic;

namespace ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors
{
    public class ResponseError
    {
        /// <summary>
        /// Gets or sets property level error code.
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public ResponseErrorBody Error { get; set; }

        public override bool Equals(object obj)
        {
            var error = obj as ResponseError;
            return error != null &&
                   EqualityComparer<ResponseErrorBody>.Default.Equals(Error, error.Error);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return this.Error.GetHashCode();
            }
        }
    }
}
