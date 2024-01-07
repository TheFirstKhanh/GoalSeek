using GoalSeek.Controllers;
using GoalSeek.Models;
using GoalSeek.Services;
using GoalSeek.Services.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoakSeek.Tests
{
    public class GoalSeekControllerTests
    {
        [Fact]
        public async Task GoalSeekC_Execute_ReturnResult()
        {
            // Assign
            Mock<ILogger<GoalSeekController>> _mockLogger = new Mock<ILogger<GoalSeekController>>();
            Mock<ICacheManager> _mockCacheManager = new Mock<ICacheManager>();
            IScriptEngine scriptEngine =  new ScriptEngine();

            _mockCacheManager.Setup(x => x.GetAsync(It.IsAny<GoalSeekCacheKeyGenerator>(), It.IsAny<Func<GoalSeekResult>>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new GoalSeekResult(1000,0.1M,10,true,1000)));

            // Act
            GoalSeekController goalSeekController = new GoalSeekController(_mockLogger.Object, scriptEngine, _mockCacheManager.Object);

            // Assert
            GoalSeekRequest goalSeekRequest = new GoalSeekRequest();
            goalSeekRequest.Result = "(input + 0.62)/2";
            goalSeekRequest.InitialResult = 0.81M;
            goalSeekRequest.TargetResult = 0.8M;
            goalSeekRequest.MaximumIterations = 10;

            ActionResult<GoalSeekResponse> actionResult =  await goalSeekController.Post(goalSeekRequest);
            GoalSeekResponse goalSeekResponse = (GoalSeekResponse)((OkObjectResult)actionResult.Result).Value;

            Assert.Equal(goalSeekResponse.TargetInput, 1000);

        }
    }
}