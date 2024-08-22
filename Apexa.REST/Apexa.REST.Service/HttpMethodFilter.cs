using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;


namespace Apexa.REST.Service
{
    // Declares a class that implements the IActionFilter interface to intercept actions in MVC.
    public class HttpMethodFilter : IActionFilter
    {
        public static string Error { get; private set; }

        // Private field to hold allowed HTTP methods.
        private readonly string[] _allowedMethods;

        // Constructor to initialize the filter with a list of allowed methods.
        public HttpMethodFilter(string[] allowedMethods)
        {
            // Stores the allowed HTTP methods passed during instantiation.
            _allowedMethods = allowedMethods;
        }

        // Method called before an action method executes.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the current request's HTTP method is not in the allowed list.
            if (!_allowedMethods.Contains(context.HttpContext.Request.Method))
            {
                // Creates an anonymous object to represent the custom error response.
                var customResponse = new
                {
                    Code = 405,  // HTTP status code for "Method Not Allowed".
                    Message = "HTTP Method not allowed"  // Custom error message.
                };

                // Setting the action result to an ObjectResult with the custom response and status code.
                context.Result = new ObjectResult(customResponse)
                {
                    StatusCode = 405  // Explicitly setting the HTTP status code to 405.
                };
            }
        }

        // Method called after an action method executes.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method is part of the IActionFilter interface but is not used here.
            // You can implement post-processing logic here if needed.
            // This method is intentionally left empty as no post-action processing is required
            Error = context?.Exception?.Message;
        }
    }    
}
