using Household.SQLite.App.Application.Household.Queries.GetHousehold;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Population.SQLite.App.Application.Common.Helper;
using System;
using System.Threading.Tasks;

namespace Population.SQLite.App.Api.Controllers
{
    public class HouseholdController : ApiController
    {
        private const string Type = "Household";
        private readonly ILogger<ApiController> _logger;

        public HouseholdController(ILogger<ApiController> logger, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            _logger = logger;
        }

        [FunctionName("Household")]
        public async Task<IActionResult> Household([HttpTrigger(AuthorizationLevel.Anonymous, "GET",
            Route = "household")] HttpRequest request, ILogger log)
        {
            string states = request.Query["state"];
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd T HH:mm:ss.fff")} API endpoint called -/household?state={states}");

            var query = new GetHouseholdQuery()
            {
                States = states.QueryParamValidForIntArray()
            };

            var result = await Mediator.Send(query);

            if (result == null)
            {
                return SendNotFoundResult("Not Found", Type);
            }
            return SendSuccessfulGetResult(Type, result);
        }
    }
}
