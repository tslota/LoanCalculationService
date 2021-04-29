using System.Runtime.InteropServices.ComTypes;
using LoanCalculationService;
using LoanCalculationService.Model;
using NUnit.Framework;

namespace LoanCalculationServiceTests
{
    public class LoanFeeTest
    {
        private LoanFeeSettings _loanSettings;

        [SetUp]
        public void Setup()
        {
            _loanSettings = new LoanFeeSettings
            {
                FeeRate = new decimal(0.01),
                MaxFeeAmount = new decimal(10_000)
            };
        }

        private decimal ExecuteTest(decimal amount)
        {
            var calculator = new LoanFeeCalculator(_loanSettings);
            return calculator.CalcLoanFee(amount);
        }
        [Test]
        public void loan_fee_lower_then_max_test()
        {
            var ret = ExecuteTest(500_000);
            Assert.That(ret,Is.EqualTo(5_000));

        }
        [Test]
        public void loan_fee_higher_then_max_test()
        {
            var ret = ExecuteTest(1_500_000);
            Assert.That(ret, Is.EqualTo(10_000));

        }

        [Test]
        public void loan_fee_equal_then_max_test()
        {
            var ret = ExecuteTest(1_500_000);
            Assert.That(ret, Is.EqualTo(10_000));

        }

        [Test]
        public void loan_amount_equal_0()
        {
            var ret = ExecuteTest(0);
            Assert.That(ret, Is.EqualTo(0));

        }
    }
}