using System.Management.Automation;

namespace GoalSeek.Services
{
    public class ScriptEngine: IScriptEngine
    {
        public ScriptEngine() { }

        public decimal Compute(string formula)
        {
            if (string.IsNullOrEmpty(formula))
                throw new ArgumentNullException();
          
            using (PowerShell ps = PowerShell.Create())
            {
                string? computedValue = ps.AddScript(formula).Invoke().Select(x => x.BaseObject)?.FirstOrDefault()?.ToString();

                if (string.IsNullOrEmpty(computedValue))
                    throw new ArgumentNullException();

                decimal result;
                
                if(decimal.TryParse(computedValue, out result))
                    return result;
            }

            return default;
        }
    }
}
