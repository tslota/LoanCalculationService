using System;
using LoanCalculationService.Interfaces;
using LoanCalculationService.Model;

namespace LoanCalculationService
{
    public class LoanFeeCalculator : ILoanFeeCalculator
    {
        private readonly LoanFeeSettings _feeSettings;

        public LoanFeeCalculator(LoanFeeSettings feeSettings)
        {
            _feeSettings = feeSettings;
        }
        public Decimal CalcLoanFee(decimal loanAmount)
        {
            var feeAmount = loanAmount * _feeSettings.FeeRate;
            if (feeAmount > _feeSettings.MaxFeeAmount)
                return _feeSettings.MaxFeeAmount;
            return Math.Round(feeAmount,2);
        }
    }
}