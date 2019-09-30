using ITG.Brix.Teams.Application.Enums;

namespace ITG.Brix.Teams.Application.Bases
{
    public class CustomFault : Failure
    {
        public ErrorType Type
        {
            get
            {
                return ErrorType.CustomError;
            }
        }
    }
}
