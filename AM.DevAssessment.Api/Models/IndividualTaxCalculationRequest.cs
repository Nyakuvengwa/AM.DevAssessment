namespace AM.DevAssessment.Api.Models;

public record IndividualTaxCalculationRequest(
    decimal AnnualIncome,
    string PostalCode);
