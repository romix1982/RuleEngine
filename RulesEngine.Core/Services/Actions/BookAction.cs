using RulesEngine.Core.Models;
using System;

namespace RulesEngine.Core.Services.Actions
{
    public class BookAction : IRuleTriggerAction
    {
        public string ActionName => "BookAction";

        public void DoAction(Payment payment)
        {
            Console.WriteLine("Create a duplicate packing slip for the royalty department.");
        }
    }
}
