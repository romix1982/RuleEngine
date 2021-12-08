using RulesEngine.Core.Models;
using System;

namespace RulesEngine.Core.Services.Actions
{
    public class CommissionPaymentAction : IRuleTriggerAction
    {
        public string ActionName => "CommissionPaymentAction";

        public void DoAction(Payment payment)
        {
            Console.WriteLine("Generate a commission payment to the agent.");
        }
    }
}
