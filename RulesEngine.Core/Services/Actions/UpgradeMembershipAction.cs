using RulesEngine.Core.Models;
using System;

namespace RulesEngine.Core.Services.Actions
{
    public class UpgradeMembershipAction : IRuleTriggerAction
    {
        public string ActionName => "UpgradeMembershipAction";

        public void DoAction(Payment payment)
        {
            Console.WriteLine("Apply the upgrade."); 
        }
    }
}
