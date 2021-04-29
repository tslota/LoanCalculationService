using System;

namespace LoanCalculationService.Model
{
    public class LoanFeeSettings
    {
        public decimal MaxFeeAmount { get; set; } = 10000;
        public decimal FeeRate { get; set; } = new Decimal(0.01);
    }
}