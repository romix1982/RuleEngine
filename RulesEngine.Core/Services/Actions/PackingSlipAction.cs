using RulesEngine.Core.Models;
using System;

namespace RulesEngine.Core.Services.Actions
{
    public class PackingSlipAction : IRuleTriggerAction
    {
        public string ActionName => "PackingSlipAction";

        public void DoAction(Payment payment)
        {
            Console.WriteLine("Generate a packing slip for shipping.");
        }
    }
}
