using RulesEngine.Core.Models;

namespace RulesEngine.Core.Services.Actions
{
    public interface IRuleTriggerAction
    {
        string ActionName { get;}
        void DoAction(Payment payment);
    }
}
