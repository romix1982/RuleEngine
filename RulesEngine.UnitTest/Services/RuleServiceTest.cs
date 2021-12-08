using Moq;
using NUnit.Framework;
using RulesEngine.Core.Models;
using RulesEngine.Core.Repositories;
using RulesEngine.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesEngine.UnitTest.Services
{
    [TestFixture]
    public class RuleServiceTest
    {
        private Mock<IRuleRepository> _ruleRepository;
        private IRuleService _ruleService;

        [SetUp]
        public void Setup()
        {
            _ruleRepository = new Mock<IRuleRepository>();
            _ruleService = new RuleService(_ruleRepository.Object);
        }

        [Test]
        public async Task GetRulesAsync_should_call_repository()
        {
            //Arrenge
            _ruleRepository.Setup(x => x.GetRulesAsync()).ReturnsAsync(new List<Rule>());
            //Act
            var result = await _ruleService.GetRulesAsync();

            //Assert
            _ruleRepository.Verify(x => x.GetRulesAsync(), Times.Once);
        }
    }
}
