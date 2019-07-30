using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ThatConference.Fn.Config;

namespace ThatConference.Fn.Functions
{
    public class HealthFunctions
    {
        #region Constructor

        public HealthFunctions(IOptions<AppConfig> config, ILogger<HealthFunctions> logger)
        {
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Private Fields

        private ILogger<HealthFunctions> _logger;
        private readonly AppConfig _config;

        #endregion

        #region Health Endpoints

        [FunctionName("Ping")]
        public IActionResult Ping([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ping")]
            HttpRequest req)
        {
            return new OkObjectResult("pong");
        }

        [FunctionName("Health")]
        public IActionResult Health([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")]
            HttpRequest req)
        {
            if (string.IsNullOrEmpty(_config?.ConnectionString))
                return new StatusCodeResult((int) HttpStatusCode.ServiceUnavailable);

            return new OkObjectResult("ok");
        }

        #endregion
    }
}