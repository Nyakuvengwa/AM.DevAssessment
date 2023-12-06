namespace AM.DevAssessment.Api.Services.TaxCalculatorService;

public interface ITaxCalculatorService
{
    TaxCalculation CalculateTax(decimal annualIncome);
}

public record TaxCalculation(
    string Type,
    decimal TotalTaxPayable);