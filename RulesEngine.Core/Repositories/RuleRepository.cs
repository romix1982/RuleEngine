using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RulesEngine.Core.Config;
using RulesEngine.Core.Models;
using RulesEngine.Infrastructure.Provider;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RulesEngine.Core.Repositories
{
    public interface IRuleRepository
    {
        Task<IEnumerable<Rule>> GetRulesAsync();
    }
    public class RuleRepository : IRuleRepository
    {
        private readonly RulesConfig _rulesConfig;
//        private const string ResourceName = "RulesEngine.Infrastructure.RulesDefinition.Rules.json";
        private readonly MemoryStream _streamRules;

        public RuleRepository(IOptions<RulesConfig> rulesConfig, IResourceLoader _resourceLoader)
        {
            _rulesConfig = rulesConfig.Value;
            var bodyBytes = _resourceLoader.GetResourceBytes(_rulesConfig.ResourceName);
            _streamRules = new MemoryStream(bodyBytes);
        }

        public async Task<IEnumerable<Rule>> GetRulesAsync()
        {
            var jsonRules = await new StreamReader(_streamRules).ReadToEndAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Rule>>(jsonRules);
        }
    }
}
