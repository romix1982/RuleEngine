using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RulesEngine.Core.Config;
using RulesEngine.Core.Models;
using RulesEngine.Core.Repositories;
using RulesEngine.Infrastructure.Provider;
using RulesEngine.IntegrationTest.DummyProvider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesEngine.IntegrationTest
{
    [TestFixture]
    public class RuleRepositoryTest
    {
        private Mock<IOptions<RulesConfig>> _rulesConfig;
        private IResourceLoader _resourceLoader; 
        private IRuleRepository _ruleRepository;
        private const string DummyResourceName = "RulesEngine.IntegrationTest.Resources.Rules.json";

        [SetUp]
        public void Setup()
        {
            _resourceLoader = new DummyResourceLoader();
            _rulesConfig = new Mock<IOptions<RulesConfig>>();
            _rulesConfig.Setup(x => x.Value).Returns(new RulesConfig { ResourceName = DummyResourceName });
            _ruleRepository = new RuleRepository(_rulesConfig.Object, _resourceLoader);
        }

        [Test]
        public async Task GetRulesAsync_should_return_Rules()
        {
            //Arrenge
            //Act
            var result = await _ruleRepository.GetRulesAsync();

            //Assert
            CollectionAssert.IsNotEmpty(result);
            Assert.IsInstanceOf<IEnumerable<Rule>>(result);
        }
    }
}