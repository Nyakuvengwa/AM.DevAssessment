namespace AM.DevAssessment.Models;

public class IndividualTaxCalculationRequest
{
    public decimal AnnualIncome { get; set; }
    public string PostalCode { get; set; }
    public IndividualTaxCalculationRequest(decimal annualIncome, string postalCode)
    {
        AnnualIncome = annualIncome;
        PostalCode = postalCode;
    }
}