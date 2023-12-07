using AM.DevAssessment.Api.Models;

namespace AM.DevAssessment.Tests.TaxCalculatorService;

public class FlatRateTaxCalculationServiceTest
{
    private readonly ITaxCalculatorService _taxCalculatorService;
    public FlatRateTaxCalculationServiceTest()
    {
        _taxCalculatorService = new FlatRateTaxCalculationService();
    }

    [TestCase(-960, 0)]
    [TestCase(0, 0)]
    [TestCase(1, 0.175)]
    [TestCase(1_000, 175)]
    [TestCase(9_999, 1749.825)]
    [TestCase(200_000, 35_000)]
    [TestCase(2_000_000, 350_000)]
    public void FlatRateTaxCalculationService_CalculateTax_ShouldReturnTaxCalculationForValidAnnualIncome(decimal annualIncome, decimal expectedTaxPayable)
    {
        // Act
        TaxCalculation taxCalculation = _taxCalculatorService.CalculateTax(annualIncome);

        // Assert
        Assert.That(taxCalculation.Type, Is.EqualTo("Flat Rate"));
        Assert.That(taxCalculation.TotalTaxPayable, Is.EqualTo(expectedTaxPayable));
    }
}
