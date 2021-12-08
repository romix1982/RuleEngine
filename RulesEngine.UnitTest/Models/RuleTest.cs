using NUnit.Framework;
using RulesEngine.Core.Models;

namespace RulesEngine.UnitTest.Models
{
    [TestFixture]
    public class RuleTest
    {
        [TestCase("Payment.Concept == \"physical product\"", "physical product")]
        [TestCase("Payment.Concept == \"Book\"", "Book")]
        [TestCase("Payment.Concept == \"Book\" || Payment.Concept == \"physical product\"", "physical product")]
        [TestCase("Payment.Concept == \"Book\" || Payment.Concept == \"physical product\"", "Book")]
        [TestCase("Payment.Concept == \"New Membership\"", "New Membership")]
        [TestCase("Payment.Concept == \"Upgrade Membership\"", "Upgrade Membership")]
        [TestCase("Payment.Concept == \"New Membership\" || Payment.Concept == \"Upgrade Membership\"", "Upgrade Membership")]
        [TestCase("Payment.Concept == \"New Membership\" || Payment.Concept == \"Upgrade Membership\"", "New Membership")]
        [TestCase("Payment.Concept == \"Video\"", "Video")]
        public void IsMatch_should_return_true_when_expression_match_with_payment_concept(string expression, string concept)
        {
            //Arrange
            var rule = new Rule { Name = "TestRule", Action = "TestAction", Expression = expression };
            var payment = new Payment { Concept = concept };

            //Act
            var result = rule.IsMatch(payment);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsMatch_should_return_false_when_expression_does_not_match_with_payment_concept()
        {
            //Arrange
            var rule = new Rule { Name = "TestRule", Action = "TestAction", Expression = "Payment.Concept == \"physical product\"" };
            var payment = new Payment { Concept = "Book" };

            //Act
            var result = rule.IsMatch(payment);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
