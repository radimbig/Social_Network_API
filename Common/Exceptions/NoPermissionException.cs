namespace Social_Network_API.Common.Exceptions
{
    public class NoPermissionException : Exception, ICustomException
    {
        public NoPermissionException():base("Access denied") { }
        public string View => "Access denied";

        public int StatusCode => StatusCodes.Status403Forbidden;
    }
}
