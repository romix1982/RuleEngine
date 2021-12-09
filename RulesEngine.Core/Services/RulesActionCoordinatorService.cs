using RulesEngine.Core.Models;
using RulesEngine.Core.Services.Actions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesEngine.Core.Services
{
    public interface IRulesActionCoordinatorService
    {
        Task ApplyActionsAsync(Payment payment);
    }

    public class RulesActionCoordinatorService : IRulesActionCoordinatorService
    {
        private readonly IEnumerable<IRuleTriggerAction> _ruleTriggerActions;
        private readonly IRulesValidatorService _rulesValidatorService;

        public RulesActionCoordinatorService(IRulesValidatorService rulesValidatorService, IEnumerable<IRuleTriggerAction> ruleTriggerActions)
        {
            _rulesValidatorService = rulesValidatorService;
            _ruleTriggerActions = ruleTriggerActions;
        }

        public async Task ApplyActionsAsync(Payment payment)
        {
            var actions = await _rulesValidatorService.ApplyRulesAsync(payment);

            foreach (var action in actions)
            {
                _ruleTriggerActions.First(x => x.ActionName == action).DoAction(payment);
            }
        }
    }
}
