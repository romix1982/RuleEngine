using RulesEngine.Core.Models;
using System.Collections.Generic;
using System.Linq;
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
            return (from rule in rules
                    where rule.IsMatch(payment)
                    select rule.Action).ToList();
        }
    }
}
