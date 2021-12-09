using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;

namespace RulesEngine.Core.Models
{
    public class Rule
    {
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Action { get; set; }

        public async Task<bool> IsMatchAsync(Payment input)
        {
            var payment = new Globals { Payment = input };
            return await CSharpScript.EvaluateAsync<bool>(Expression, globals: payment);
        }
    }

    public class Globals
    {
        public Payment Payment;
    }
}
