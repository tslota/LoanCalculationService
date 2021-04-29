using System;
using LoanCalculationService.Model;

namespace LoanCalculationService
{
    public class LoanAPRCalculator
    {
        private readonly LoanSettings _loanSettings;

        public LoanAPRCalculator(LoanSettings loanSettings)
        {
            _loanSettings = loanSettings;
        }

        public LoanInterestModel CalcInterests(double amount, int periodInYears)
        {
            //q = 1 + r/m
            var q = 1 + (double)_loanSettings.InterestRate / 12;
            var n = periodInYears * 12;

            var monthly = amount * Math.Pow(q, periodInYears);

            var interests = monthly * n - amount;
            return new LoanInterestModel
            {
                MonthlyPayment = Math.Round(new decimal(monthly), 2),
                TotalInterests = Math.Round(new decimal(interests), 2)
            };
        }
    }
}