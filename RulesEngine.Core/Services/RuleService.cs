using RulesEngine.Core.Models;
using RulesEngine.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesEngine.Core.Services
{
    public interface IRuleService
    {
        Task<IEnumerable<Rule>> GetRulesAsync();
    }

    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRespository;

        public RuleService(IRuleRepository ruleRespository)
        {
            _ruleRespository = ruleRespository;
        }

        public async Task<IEnumerable<Rule>> GetRulesAsync() => await _ruleRespository.GetRulesAsync();
    }
}
