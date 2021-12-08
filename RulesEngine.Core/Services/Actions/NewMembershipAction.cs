using RulesEngine.Core.Models;
using System;

namespace RulesEngine.Core.Services.Actions
{
    public class NewMembershipAction : IRuleTriggerAction
    {
        public string ActionName => "NewMembershipAction";

        public void DoAction(Payment payment)
        {
            Console.WriteLine("activate that membership.");
        }
    }
}
