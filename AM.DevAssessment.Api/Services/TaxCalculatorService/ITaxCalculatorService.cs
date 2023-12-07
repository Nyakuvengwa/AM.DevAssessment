using AM.DevAssessment.Api.Models;

namespace AM.DevAssessment.Api.Services.TaxCalculatorService;

public interface ITaxCalculatorService
{
    TaxCalculation CalculateTax(decimal annualIncome);
}