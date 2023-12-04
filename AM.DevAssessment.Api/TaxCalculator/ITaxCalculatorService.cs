namespace AM.DevAssessment.Api.TaxCalculator;

public interface ITaxCalculatorService
{
    Task<TaxCalculation> CalculateTaxCalculationAsync(decimal annualIncome, string postalCode);
}

public record TaxCalculation(
    string Type,
    decimal TotalTaxPayable,
    decimal TaxRate);