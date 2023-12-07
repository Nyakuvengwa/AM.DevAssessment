namespace AM.DevAssessment.Models;

public class IndividualTaxCalculationResponse
{
    public decimal TotalTaxPayable { get; set; }
    public string TaxCalculationType { get; set; } = string.Empty;
}
