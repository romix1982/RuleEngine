using RulesEngine.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesEngine.Core.Services
{
    public interface IRulesValidatorService
    {
        Task<IEnumerable<string>> ApplyRulesAsync(Payment payment);
    }

    public class RulesValidatorService : IRulesValidatorService
    {
        private readonly IRuleService _ruleService;

        public RulesValidatorService(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        public async Task<IEnumerable<string>> ApplyRulesAsync(Payment payment)
        {
            var rules = await _ruleService.GetRulesAsync();
            var matchList = new List<string>();
            foreach (var rule in rules)
            {
                if (await rule.IsMatchAsync(payment))
                    matchList.Add(rule.Action);
            }

            return matchList;
        }
    }
}
