namespace AM.DevAssessment.Models;

public class TaxCalcuatationSummary
{
    public Guid Id { get; set; }
    public decimal AnnualIncome { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; }
    public decimal TotalTaxPayable { get; set; }
    public string TaxCalculationType { get; set; } = string.Empty;
}