using AcmeCorpStockApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcmeCorpStockApp.Controllers
{
    [AllowAnonymous]
    [Route("Error")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("HttpStatusCodeHandler/{statusCode}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            _logger.LogWarning($"404-Error. Page Not Found. Path is: {statusCodeResult.OriginalPath} and " +
                $"Query String is: {statusCodeResult.OriginalQueryString}");

            ShowErrorDTO showErrorDTO = new ShowErrorDTO
            {
                StatusCode = statusCode,
            };
            return View(showErrorDTO);
        }

        [Route("ExceptionHandler")]
        public IActionResult ExceptionHandler()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The path {exceptionDetails.Path} threw an exception \n {exceptionDetails.Error.Message}. \n Error Stack Trace is \n{exceptionDetails.Error.StackTrace}");

            return View();
        }
    }
}
