namespace AM.DevAssessment.Api.Models;

public record IndividualTaxCalculationResponse(
    decimal TotalTaxPayable,
    string TaxCalculationType);