using AM.DevAssessment.Api.Models;

namespace AM.DevAssessment.Tests.TaxCalculatorService;

public class FlatValueTaxCalculationServiceTest
{
    private readonly ITaxCalculatorService _taxCalculatorService;
    public FlatValueTaxCalculationServiceTest()
    {
        _taxCalculatorService = new FlatValueTaxCalculationService();
    }

    [TestCase(-240, 0)]
    [TestCase(0, 0)]
    [TestCase(1, 0.05)]
    [TestCase(1_000, 50)]
    [TestCase(9_999, 499.95)]
    [TestCase(200_000, 10_000)]
    [TestCase(2_000_000, 10_000)]
    public void FlatValueTaxCalculationService_CalculateTax_ShouldReturnTaxCalculationForValidAnnualIncome(decimal annualIncome, decimal expectedTaxPayable)
    {
        // Act
        TaxCalculation taxCalculation = _taxCalculatorService.CalculateTax(annualIncome);

        // Assert
        Assert.That(taxCalculation.Type, Is.EqualTo("Flat Value"));
        Assert.That(taxCalculation.TotalTaxPayable, Is.EqualTo(expectedTaxPayable));
    }
}
