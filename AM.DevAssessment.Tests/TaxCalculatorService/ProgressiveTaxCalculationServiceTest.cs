namespace AM.DevAssessment.Tests.TaxCalculatorService;

public class ProgressiveTaxCalculationServiceTest
{
    private readonly ITaxCalculatorService _taxCalculatorService;

    public ProgressiveTaxCalculationServiceTest()
    {
        _taxCalculatorService = new ProgressiveTaxCalculationService();
    }

    [TestCase(-8349, 0)]
    [TestCase(0, 0)]
    [TestCase(1, 0.1)]
    [TestCase(8349, 834.9)]
    [TestCase(33949, 4674.70)]
    [TestCase(82249, 16749.35)]
    [TestCase(171549, 41753.04)]
    [TestCase(372949, 108214.66)]
    [TestCase(372951, 108214.99)]
    public void CalculateTax_ShouldReturnTaxCalculationForValidAnnualIncome(decimal annualIncome, decimal expectedTaxPayable)
    {
        // Act
        TaxCalculation taxCalculation = _taxCalculatorService.CalculateTax(annualIncome);

        // Assert
        Assert.That(taxCalculation.Type, Is.EqualTo("Progressive"));
        Assert.That(taxCalculation.TotalTaxPayable, Is.EqualTo(expectedTaxPayable));
    }
}
