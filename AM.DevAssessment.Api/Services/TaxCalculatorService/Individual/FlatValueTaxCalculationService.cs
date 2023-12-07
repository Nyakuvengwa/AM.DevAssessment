using AM.DevAssessment.Api.Models;

namespace AM.DevAssessment.Api.Services.TaxCalculatorService.Individual;

public class FlatValueTaxCalculationService : ITaxCalculatorService
{
    public const string Type = "Flat Value";
    private const decimal _flatValuePercentage = 0.05m;

    public TaxCalculation CalculateTax(decimal annualIncome)
    {
        if (annualIncome < 0) { return new(Type, 0); }
        return new(Type, annualIncome < 200_000 ? annualIncome * _flatValuePercentage : 10_000);
    }
}