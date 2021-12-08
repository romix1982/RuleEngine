using NUnit.Framework;
using RulesEngine.Core.Services.Actions;
using RulesEngine.Core.Services;
using Moq;
using RulesEngine.Core.Models;
using System.Collections.Generic;

namespace RulesEngine.UnitTest.Services
{
    [TestFixture]
    public class RulesActionCoordinatorServiceTest
    {
        private Mock<IRuleTriggerAction> _ruleTriggerAction;
        private Mock<IRulesValidatorService> _rulesValidatorService;
        private IRulesActionCoordinatorService _rulesActionCoordinatorService;

        [SetUp]
        public void SetUp()
        {
            _ruleTriggerAction = new Mock<IRuleTriggerAction>();
            _rulesValidatorService = new Mock<IRulesValidatorService>();
            _rulesActionCoordinatorService = new RulesActionCoordinatorService(_rulesValidatorService.Object, new[] { _ruleTriggerAction.Object });
        }

        [Test]
        public void ApplyActionsAsync_should_call_DoAction_twice()
        {
            //Arrenge
            var payment = new Payment { Concept = "Book"};
            _rulesValidatorService.Setup(x => x.ApplyRulesAsync(It.IsAny<Payment>())).ReturnsAsync(new List<string> { "BookAction", "CommissionPaymentAction" });
            _ruleTriggerAction.SetupSequence(x => x.ActionName).Returns("BookAction").Returns("CommissionPaymentAction");

            //Act
            _rulesActionCoordinatorService.ApplyActionsAsync(payment);

            //Assert
            _rulesValidatorService.Verify(x => x.ApplyRulesAsync(It.IsAny<Payment>()), Times.Once);
            _ruleTriggerAction.Verify(x => x.ActionName, Times.Exactly(2));
            _ruleTriggerAction.Verify(x => x.DoAction(It.IsAny<Payment>()), Times.Exactly(2));
        }

        [Test]
        public void ApplyActionsAsync_should_call_DoAction_once()
        {
            //Arrenge
            var payment = new Payment { Concept = "Book" };
            _rulesValidatorService.Setup(x => x.ApplyRulesAsync(It.IsAny<Payment>())).ReturnsAsync(new List<string> { "BookAction"});
            _ruleTriggerAction.Setup(x => x.ActionName).Returns("BookAction");

            //Act
            _rulesActionCoordinatorService.ApplyActionsAsync(payment);

            //Assert
            _rulesValidatorService.Verify(x => x.ApplyRulesAsync(It.IsAny<Payment>()), Times.Once);
            _ruleTriggerAction.Verify(x => x.ActionName, Times.Once);
            _ruleTriggerAction.Verify(x => x.DoAction(It.IsAny<Payment>()), Times.Once);
        }

        [Test]
        public void ApplyActionsAsync_should_not_call_DoAction()
        {
            //Arrenge
            var payment = new Payment { Concept = "Book" };
            _rulesValidatorService.Setup(x => x.ApplyRulesAsync(It.IsAny<Payment>())).ReturnsAsync(new List<string> { "BookAction" });
            _ruleTriggerAction.Setup(x => x.ActionName).Returns("CommissionPaymentAction");

            //Act
            _rulesActionCoordinatorService.ApplyActionsAsync(payment);

            //Assert
            _rulesValidatorService.Verify(x => x.ApplyRulesAsync(It.IsAny<Payment>()), Times.Once);
            _ruleTriggerAction.Verify(x => x.ActionName, Times.Once);
            _ruleTriggerAction.Verify(x => x.DoAction(It.IsAny<Payment>()), Times.Never);
        }
    }
}
