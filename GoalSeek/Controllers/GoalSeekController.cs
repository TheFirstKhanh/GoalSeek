using GoalSeek.Models;
using GoalSeek.Services;
using GoalSeek.Services.Caching;
using Microsoft.AspNetCore.Mvc;

namespace GoalSeek.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalSeekController : ControllerBase
    {

        private readonly ILogger<GoalSeekController> _logger;
        private readonly IScriptEngine _scriptEngine;
        private readonly ICacheManager _cacheManager;
        public GoalSeekController(ILogger<GoalSeekController> logger,
            IScriptEngine scriptEngine,
            ICacheManager cacheManager)
        {
            _logger = logger;
            _scriptEngine = scriptEngine;
            _cacheManager = cacheManager;
        }

        [HttpGet]
        public string Get()
        {
            return "Success";
        }
        [HttpPost]
        public async Task<ActionResult<GoalSeekResponse>> Post([FromBody] GoalSeekRequest goalSeekRequest)
        {
            try
            {
                if (goalSeekRequest == null || string.IsNullOrEmpty(goalSeekRequest.Result) || goalSeekRequest.TargetResult == 0)
                {
                    _logger.LogError($"{nameof(Post)}_Invalid Input");
                    return StatusCode(400, string.Empty);
                }

                string requestMd5Code = goalSeekRequest.GetMd5Code();

                GoalSeekResult goalSeekResult = await _cacheManager.GetAsync<GoalSeekResult>(
                    new GoalSeekCacheKeyGenerator(requestMd5Code),
                    () => Services.GoalSeek.TrySeek(
                    Calculate,
                    targetValue: goalSeekRequest.TargetResult,
                    maxIterations: goalSeekRequest.MaximumIterations,
                    guess: goalSeekRequest.InitialResult,
                    formula: goalSeekRequest.Result),
                    new TimeSpan(5));


                return Ok(new GoalSeekResponse(goalSeekResult?.ClosestValue ?? 0));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, string.Empty);
            }
        }



        private decimal Calculate(decimal value, string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                _logger.LogError($"{nameof(Calculate)}_Invalid Input");
                throw new ArgumentException();
            }

            string newformula = formula.Replace("input", value.ToString());
            return _scriptEngine.Compute(newformula);

        }



    }
}