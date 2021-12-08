using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using RulesEngine.Core.Models;
using RulesEngine.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesEngine.UnitTest.Services
{
    [TestFixture]
    public class RulesValidatorServiceTest
    {
        private RulesValidatorService _rulesValidatorService;
        private Mock<IRuleService> _ruleService;

        [SetUp]
        public void SetUp()
        {
            _ruleService = new Mock<IRuleService>();
            _rulesValidatorService = new RulesValidatorService(_ruleService.Object);
        }

        [Test]
        public async Task ApplyRulesAsync_should_return_list_of_actionsAsync()
        {
            //Arrenge
            _ruleService.Setup(x => x.GetRulesAsync()).ReturnsAsync(GetDummyRules());
            var payment = new Payment { Concept = "Book" };

            //Act
            var result = await _rulesValidatorService.ApplyRulesAsync(payment);

            //Assert
            CollectionAssert.IsNotEmpty(result);
            CollectionAssert.Contains(result, "BookAction");
            CollectionAssert.Contains(result, "CommissionPaymentAction");
        }

        private static IEnumerable<Rule> GetDummyRules()
        {
            return Builder<Rule>.CreateListOfSize(3)
                .All()
                .Random(1)
                .With(x => x.Expression = "Payment.Concept == \"Book\"")
                .With(x => x.Action = "BookAction")
                .Random(1)
                .With(x => x.Expression = "Payment.Concept == \"Book\" || Payment.Concept == \"physical product\"")
                .With(x => x.Action = "CommissionPaymentAction")
                .Random(1)
                .With(x => x.Expression = "Payment.Concept == \"Flowers\"")
                .With(x => x.Action = "CommissionPaymentAction")
                .Build();
        }
    }
}
