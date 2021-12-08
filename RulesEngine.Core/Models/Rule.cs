using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace RulesEngine.Core.Models
{
    public class Rule
    {
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Action { get; set; }

        public bool IsMatch(Payment input)
        {
            var payment = new Globals { Payment = input };
            return CSharpScript.EvaluateAsync<bool>(Expression, globals: payment).Result;
        }
    }

    public class Globals
    {
        public Payment Payment;
    }
}
