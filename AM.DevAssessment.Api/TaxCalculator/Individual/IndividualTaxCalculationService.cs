
namespace AM.DevAssessment.Api.TaxCalculator.Individual;

public class IndividualTaxCalculationService : ITaxCalculatorService
{
    public Task<TaxCalculation> CalculateTaxCalculationAsync(decimal annualIncome, string postalCode)
    {
        TaxCalculation result = new ("Progressive", annualIncome * 0.1m, 0.1m);
        return Task.FromResult(result);
    }
}
