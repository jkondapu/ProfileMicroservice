using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfileMicroservice.Logging;
using ProfileMicroservice.ExceptionHandler;

namespace ProfileMicroservice.Filters
{
    public class ProfileAPIExceptionAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ProfileAPIExceptionAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(0, context.Exception, context.Exception.Message);
            if (context.Exception is ProfileExceptionHandler apiException)
            {
                context.Result = new StatusCodeResult((int)apiException.HttpResponseMessage.StatusCode);
            }
        }
    }
}