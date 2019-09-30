using ITG.Brix.Teams.Application.Enums;

namespace ITG.Brix.Teams.Application.Bases
{
    public class ValidationFault : Failure
    {
        public ErrorType Type
        {
            get
            {
                return ErrorType.ValidationError;
            }
        }
    }
}
