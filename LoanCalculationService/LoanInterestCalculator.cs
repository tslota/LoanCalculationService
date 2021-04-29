using System;
using LoanCalculationService.Interfaces;
using LoanCalculationService.Model;

namespace LoanCalculationService
{
    public class LoanInterestCalculator : ILoanInterestCalculator
    {
        private readonly LoanSettings _loanSettings;

        public LoanInterestCalculator(LoanSettings loanSettings)
        {
            _loanSettings = loanSettings;
        }

        public LoanInterestModel CalcInterests(decimal amount, int periodInYears)
        {
            //q = 1 + r/m
            var q = 1 + (double)_loanSettings.InterestRate / 12;
            var n = periodInYears * 12;

            var monthly =(double)amount * Math.Pow(q, n) * ((q - 1) /( Math.Pow(q, n) - 1));

            var interests = monthly * n - (double)amount;
            return new LoanInterestModel
            {
                MonthlyPayment = Math.Round(new decimal(monthly),2),
                TotalInterests = Math.Round(new decimal(interests), 2)
            };
        }
    }
}