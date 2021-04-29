using LoanCalculationService;
using LoanCalculationService.Model;
using NUnit.Framework;

namespace LoanCalculationServiceTests
{
   
    public class LoanInterestsTest
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


        private decimal ExecuteTest(double amount,int period)
        {
            var calculator = new LoanInterestCalculator(_loanSettings);
            return calculator.CalcInterests(amount,period).TotalInterests;
        }

        [Test]
        public void Interest_For_500k_and_10y_Test()
        {
            var result = ExecuteTest(500_000, 10);
            Assert.That(result, Is.EqualTo(136_393.09));
        }
    }
}