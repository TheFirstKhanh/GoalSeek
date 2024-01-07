namespace GoalSeek.Models
{
    public class GoalSeekResponse
    {
        public decimal TargetInput { get; }

        public GoalSeekResponse(decimal targetInput)
        { 
            TargetInput = targetInput;
        }
    }
}
