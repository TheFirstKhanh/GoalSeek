using GoalSeek.Common;
using GoalSeek.Models;

namespace GoalSeek.Services
{
    public class GoalSeek
    {
        //https://github.com/tobony/GoalSeekExample/blob/master/GoalSeek.cs
        // I couldn't work out an optimal alorithm for Goal Seek

        public static GoalSeekResult TrySeek(
            Func<decimal, string, decimal> func,
            string formula = "",
            decimal accuracyLevel = GoalSeekConstants.DefaultAccuracyLevel,
            decimal targetValue = 0,
            decimal guess = 0,
            int maxIterations = GoalSeekConstants.DefaultMaxIterations,
            bool resultRoundOff = GoalSeekConstants.DefaultResultRoundOff)
        {
            try
            {
                const decimal delta = 0.0001m;

                var iterations = 0;

                var result1 = func(guess, formula) - targetValue;
                while (Math.Abs(result1) > accuracyLevel && iterations++ < maxIterations)
                {
                    var newGuess = guess + delta;
                    var result2 = func(newGuess, formula) - targetValue;
                    if (result2 - result1 != 0)
                        guess -= result1 * (newGuess - guess) / (result2 - result1);
                    else
                        break;

                    result1 = func(guess, formula) - targetValue;
                }

                if (iterations > maxIterations)
                    iterations = maxIterations;

                if (resultRoundOff)
                    guess = Math.Round(guess, accuracyLevel.ToString().Length - (accuracyLevel.ToString().IndexOf('.') + 1));
                return new GoalSeekResult(targetValue: targetValue, accucracyLevel: accuracyLevel, iterations: iterations, isGoalReached: Math.Abs(result1) <= accuracyLevel, closestValue: guess);
            }
            catch (Exception ex) 
            {
                throw;
            }

 
        }
    }
}
