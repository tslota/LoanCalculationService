using System;

namespace LoanCalculationService.Interfaces
{
    public interface ILoanFeeCalculator
    {
        Decimal CalcLoanFee(decimal loanAmount);
    }
}