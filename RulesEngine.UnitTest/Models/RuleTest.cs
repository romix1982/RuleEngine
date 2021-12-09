using NUnit.Framework;
using RulesEngine.Core.Models;
using System.Threading.Tasks;

namespace RulesEngine.UnitTest.Models
{
    [TestFixture]
    public class RuleTest
    {
        [TestCase("Payment.Concept == \"Physical product\"", "Physical product")]
        [TestCase("Payment.Concept == \"Book\"", "Book")]
        [TestCase("Payment.Concept == \"Book\" || Payment.Concept == \"Physical product\"", "Physical product")]
        [TestCase("Payment.Concept == \"Book\" || Payment.Concept == \"Physical product\"", "Book")]
        [TestCase("Payment.Concept == \"New Membership\"", "New Membership")]
        [TestCase("Payment.Concept == \"Upgrade Membership\"", "Upgrade Membership")]
        [TestCase("Payment.Concept == \"New Membership\" || Payment.Concept == \"Upgrade Membership\"", "Upgrade Membership")]
        [TestCase("Payment.Concept == \"New Membership\" || Payment.Concept == \"Upgrade Membership\"", "New Membership")]
        [TestCase("Payment.Concept == \"Video\"", "Video")]
        [TestCase("Payment.Concept != \"New Membership\"", "Book")]
        public async Task IsMatch_should_return_true_when_expression_match_with_payment_concept(string expression, string concept)
        {
            //Arrange
            var rule = new Rule { Name = "TestRule", Action = "TestAction", Expression = expression };
            var payment = new Payment { Concept = concept };

            //Act
            var result = await rule.IsMatchAsync(payment);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsMatch_should_return_false_when_expression_does_not_match_with_payment_concept()
        {
            //Arrange
            var rule = new Rule { Name = "TestRule", Action = "TestAction", Expression = "Payment.Concept == \"Physical product\"" };
            var payment = new Payment { Concept = "Book" };

            //Act
            var result = await rule.IsMatchAsync(payment);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
