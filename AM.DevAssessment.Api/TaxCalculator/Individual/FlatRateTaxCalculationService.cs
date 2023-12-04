namespace AM.DevAssessment.Api.TaxCalculator.Individual;

public class FlatRateTaxCalculationService : ITaxCalculatorService
{
    private const string Type = "Flat Rate";
    private const decimal _flatRatePercentage = 0.175m;

    public TaxCalculation CalculateTax(decimal annualIncome)
    {
        if (annualIncome < 0) { return new(Type, 0); }
        return new(Type, annualIncome * _flatRatePercentage);
    }
}