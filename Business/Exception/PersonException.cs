using System.Net;

namespace Project.Provider.Exception
{
    public class PersonException : BaseCustomException
    {
        public PersonException(string message, HttpStatusCode httpStatusCode) : base(message, "USR", httpStatusCode) { }

        public class NotFound : PersonException
        {
            public NotFound() : base($"data not found.", HttpStatusCode.NotFound) { }
            public NotFound(int id = 0) : base($"data id ({id}) is not found.", HttpStatusCode.NotFound) { }
        }

        public class NotFoundByUserName : PersonException
        {
            public NotFoundByUserName(string userName) : base($"person name ({userName}) is not found.", HttpStatusCode.NotFound) { }
        }

    }
}
