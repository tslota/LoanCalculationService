using System;
using System.Threading.Tasks;
using LoanCalculationService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanCalculationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
       
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanFeeCalculator _feeCalculator;
        private readonly ILoanInterestCalculator _interestCalculator;

        public LoanController(ILogger<LoanController> logger, ILoanFeeCalculator feeCalculator,ILoanInterestCalculator interestCalculator)
        {
           
            _logger = logger;
            _feeCalculator = feeCalculator;
            _interestCalculator = interestCalculator;
        }
        /// <summary>
        /// Method calculates a bases data for loan
        /// </summary>
        /// <param name="amount">loan amount</param>
        /// <param name="period">period in years </param>
        /// <returns></returns>
        // GET
        [HttpGet("{amount}/{period}")]

        public  IActionResult Get(decimal amount, int period)
        {
            try
            {
                var fee = _feeCalculator.CalcLoanFee(amount);
                var interest = _interestCalculator.CalcInterests(amount, period);

                return Ok(new
                {
                    MonthlyPayment = interest.MonthlyPayment,
                    AdministrationFee=fee,
                    TotalInterests=interest.TotalInterests,
                    NumberOfPayments = period*12,
                    LoanAmount = amount,
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Internal error.", e);
                return StatusCode(500);
            }
        }
    }
}