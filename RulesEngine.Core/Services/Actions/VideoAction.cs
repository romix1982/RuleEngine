using RulesEngine.Core.Models;
using System;

namespace RulesEngine.Core.Services.Actions
{
    public class VideoAction : IRuleTriggerAction
    {
        public string ActionName => "VideoAction";

        public void DoAction(Payment payment)
        {
            Console.WriteLine("Add a free “First Aid” video to the packing slip.");
        }
    }
}
