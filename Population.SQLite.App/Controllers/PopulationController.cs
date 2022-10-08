using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Population.SQLite.App.Application.Common.Helper;
using Population.SQLite.App.Application.Population.Queries.GetPopulation;
using System;
using System.Threading.Tasks;

namespace Population.SQLite.App.Api.Controllers
{
    public class PopulationController : ApiController
    {
        private readonly ILogger<ApiController> _logger;
        private const string Type = "Comments";

        public PopulationController(ILogger<ApiController> logger, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            _logger = logger;
        }

        [FunctionName("Population")]
        public async Task<IActionResult> Population([HttpTrigger(AuthorizationLevel.Anonymous, "GET",
            Route = "population")] HttpRequest request, ILogger log)
        {
            string states = request.Query["state"];
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd T HH:mm:ss.fff")} API endpoint called -/population?state={states}");

            var query = new GetPopulationQuery()
            {
                States = states.QueryParamValidForIntArray()
            };

            var result = await Mediator.Send(query);

            if (result == null)
            {
                return SendNotFoundResult("Not Found",Type);
            }
            return SendSuccessfulGetResult(Type, result);
        }
    }
}
