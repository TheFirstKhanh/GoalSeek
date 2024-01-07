using GoalSeek.Common;

namespace GoalSeek.Models
{
    public class GoalSeekRequest: BaseRequest
    {
        public string Result { get; set; }
        public decimal InitialResult { get; set; }
        public decimal TargetResult { get; set; }
        public int MaximumIterations { get; set; }

        public override string GetMd5Code()
        {
            return ($"{Result}_{InitialResult}_{TargetResult}_{MaximumIterations}").CreateMD5();
        }
    }
}