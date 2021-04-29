using LoanCalculationService;
using LoanCalculationService.Model;
using NUnit.Framework;

namespace LoanCalculationServiceTests
{
    [TestFixture]
    public class LoanMonthlyPaymentTest
    {
        private LoanSettings _loanSettings;

        [SetUp]
        public void Setup()
        {
            _loanSettings = new LoanSettings()
            {
                InterestRate = new decimal(0.05)
            };
        }


        private decimal ExecuteTest(double amount, int period)
        {
            var calculator = new LoanAPRCalculator(_loanSettings);
            return calculator.CalcInterests(amount, period).MonthlyPayment;
        }

        [Test]
        public void Monthly_Payment_For_500k_and_10y_Test()
        {
            var result = ExecuteTest(500_000, 10);
            Assert.That(result, Is.EqualTo(5_303.28));
        }
    }
}