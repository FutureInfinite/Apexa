using Caliburn.Micro;
using Microsoft.AspNetCore.Mvc;

namespace Apexa.REST.Service
{
    public class ErrorResult
    {
        #region Propteis&Attributes
        public Exception Exception { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
        #endregion Propteis&Attributes
    }


    public class ErrorActionResult : IActionResult
    {
        #region Propteis&Attributes
        private readonly ErrorResult _result;
        #endregion Propteis&Attributes

        #region Operations
        public ErrorActionResult(ErrorResult result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result.Exception ?? _result.Data)
            {
                StatusCode = _result.Exception != null
                    ? StatusCodes.Status500InternalServerError
                    : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
        #endregion Operations
    }
}
