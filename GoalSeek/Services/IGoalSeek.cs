using GoalSeek.Common;

namespace GoalSeek.Services
{
    public interface IGoalSeek
    {
        decimal Calculate(Func<decimal, string, decimal> func,
            string formula = "",
            decimal accuracyLevel = GoalSeekConstants.DefaultAccuracyLevel,
            decimal targetValue = 0,
            decimal guess = 0,
            int maxIterations = GoalSeekConstants.DefaultMaxIterations,
            bool resultRoundOff = GoalSeekConstants.DefaultResultRoundOff);
    }
}
