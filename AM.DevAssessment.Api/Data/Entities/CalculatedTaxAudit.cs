using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DevAssessment.Api.Data.Entities;

[Table(nameof(CalculatedTaxAudit))]
public class CalculatedTaxAudit
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public decimal AnnualIncome { get; set; }
    [Required]
    public string PostalCode { get; set; } = string.Empty;
    [Required]
    public DateTimeOffset CreatedDate { get; set; }
    [Required]
    public decimal TotalTaxPayable { get; set; }
    [Required]
    public string TaxCalculationType { get; set; } = string.Empty;
}