using RulesEngine.Core.Models;
using System;

namespace RulesEngine.Core.Services.Actions
{
    public class EmailMembershipOwnerAction : IRuleTriggerAction
    {
        public string ActionName => "EmailMembershipOwnerAction";

        public void DoAction(Payment payment)
        {
            Console.WriteLine("e-mail the owner and inform them of the activation / upgrade.");
        }
    }
}
