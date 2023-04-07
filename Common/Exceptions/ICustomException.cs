


namespace Social_Network_API.Common.Exceptions
{
    interface ICustomException
    {
        public string View { get;}
        public Exception Origin { get;}
    }
}