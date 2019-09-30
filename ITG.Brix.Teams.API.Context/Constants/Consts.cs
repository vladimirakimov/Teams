namespace ITG.Brix.Teams.API.Context.Constants
{
    public static class Consts
    {
        public static class Failure
        {
            public static class ServiceErrorCode
            {
                public const string None = "None";
                public const string UnsupportedMediaType = "UnsupportedMediaType";
                public const string AuthenticationFailed = "AuthenticationFailed";
                public const string InvalidInput = "InvalidInput";
                public const string InvalidQueryParameterValue = "InvalidQueryParameterValue";
                public const string InvalidHeaderValue = "InvalidHeaderValue";
                public const string InvalidRequestBodyValue = "InvalidRequestBodyValue";
                public const string ResourceNotFound = "ResourceNotFound";
                public const string ResourceAlreadyExists = "ResourceAlreadyExists";
                public const string ConditionNotMet = "ConditionNotMet";
                public const string MissingRequiredQueryParameter = "MissingRequiredQueryParameter";
                public const string MissingRequiredHeader = "MissingRequiredHeader";
                public const string MissingRequiredRequestBody = "MissingRequiredRequestBody";
            }

            public static class Detail
            {
                public static class Code
                {
                    public const string Unsupported = "unsupported";
                    public const string NotFound = "not-found";
                    public const string Invalid = "invalid";
                    public const string Missing = "missing";
                    public const string InvalidQueryFilter = "invalid-query-filter";
                    public const string InvalidQueryTop = "invalid-query-top";
                    public const string InvalidQuerySkip = "invalid-query-skip";
                }

                public static class Target
                {
                    public const string ContentType = "content-type";
                    public const string ApiVersion = "api-version";
                    public const string Id = "id";
                    public const string IfMatch = "if-match";
                    public const string RequestBody = "request-body";
                    public const string QueryFilter = "$filter";
                    public const string QueryTop = "$top";
                    public const string QuerySkip = "$skip";
                }
            }
        }
    }
}
