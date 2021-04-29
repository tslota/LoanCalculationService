namespace LoanCalculationService.Interfaces
{
    public interface ILoanInterestCalculator
    {
        LoanInterestModel CalcInterests(decimal amount, int periodInYears);
    }
}