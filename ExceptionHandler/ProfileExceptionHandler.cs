using System;
using System.Net.Http;

namespace ProfileMicroservice.ExceptionHandler
{
    public class ProfileExceptionHandler :   Exception
    {
        public readonly HttpResponseMessage HttpResponseMessage;
        public ProfileExceptionHandler(HttpResponseMessage responseMessage)
            : base(responseMessage.ReasonPhrase)
        {
            HttpResponseMessage = responseMessage;
        }
    }
}
