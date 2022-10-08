using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Population.SQLite.App.Api.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IMediator _mediator;
        protected ApiController(ILogger<ApiController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        protected IMediator Mediator
        {
            get
            {
                return _mediator ??= (IMediator)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IMediator));
            }
        }

        [NonAction]
        protected virtual IActionResult SendNotFoundResult(string message, string type)
        {
            var actionName = $"{_httpContextAccessor.HttpContext.Request.Method}_{type}";

            var response = new
            {
                Result = "Failure",
                Message = message
            };

            _logger.LogError($"Failed response for {actionName} : {response}");
            var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
            return new NotFoundObjectResult(json);
        }

        [NonAction]
        protected virtual IActionResult SendBadRequestObjectResult(string message, string type, ILogger logger)
        {
            var actionName = $"{_httpContextAccessor.HttpContext.Request.Method}_{type}";

            var response = new
            {
                Result = "Failure",
                Message = message
            };

            logger.LogError($"Failed response for {actionName} : {response}");
            var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
            return new BadRequestObjectResult(json);
        }

        [NonAction]
        protected virtual IActionResult SendSuccessfulGetResult(string type, object result)
        {
            var method = _httpContextAccessor.HttpContext.Request.Method;

            var actionName = $"{method}_{type}";

            _logger.LogInformation($"Successful response for {actionName} : {JsonConvert.SerializeObject(result)}");

            return new OkObjectResult(result);
        }
        [NonAction]
        protected virtual IActionResult SendSuccessfulGetResultDefault(string type, object result)
        {
            var method = _httpContextAccessor.HttpContext.Request.Method;

            var actionName = $"{method}_{type}";

            _logger.LogInformation($"Successful response for {actionName} : {JsonConvert.SerializeObject(result)}");
            var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });
            return new OkObjectResult(json);
        }
    }
}
