namespace AM.DevAssessment.Api.TaxCalculator;

public interface ITaxCalculatorService
{
    TaxCalculation CalculateTax(decimal annualIncome);
}

public record TaxCalculation(
    string Type,
    decimal TotalTaxPayable);