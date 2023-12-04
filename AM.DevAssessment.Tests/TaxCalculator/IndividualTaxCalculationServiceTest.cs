using AM.DevAssessment.Api.TaxCalculator;
using AM.DevAssessment.Api.TaxCalculator.Individual;

namespace AM.DevAssessment.Tests.TaxCalculator;

internal class IndividualTaxCalculationServiceTest
{
    private readonly ITaxCalculatorService _taxCalculatorService;

    public IndividualTaxCalculationServiceTest()
    {
        _taxCalculatorService = new IndividualTaxCalculationService();
    }

    //Postal Code Tax Calculation Type
    //7441 Progressive
    //A100 Flat Value
    //7000 Flat rate
    //1000 Progressive
    //The progressive tax is calculated based on this table (be sure you understand how a progressive
    //calculation works):
    //Rate From To
    //10% 0 8350
    //15% 8351 33950 (0 to 8350 at 10%)
    //25% 33951 82250 (8351 to 33950 - 15%)
    //28% 82251 171550 (33951 - 82250 25%)
    //33% 171551 372950 (82251 - 171550 28%)
    //35% 372951 - (171551-372950 33%)

    [TestCase(0, "7441", 0)]
    [TestCase(1, "7441", 0.1)]
    [TestCase(8349, "7441", 834.9)]
    [TestCase(33949, "7441", 5092.35)]
    [TestCase(82249, "7441", 20562.25)]
    [TestCase(171549, "7441", 48024.72)]
    [TestCase(372949, "7441", 122993.67)]
    [TestCase(372951, "7441", 130482.85)]
    public async Task CalculateTaxCalculationAsync_ShouldReturnTaxCalculationForValidAnnualIncome(decimal annualIncome, string postalCode, decimal expectedTaxPayable)
    {
        // Act
        TaxCalculation taxCalculation = await _taxCalculatorService.CalculateTaxCalculationAsync(annualIncome, postalCode);

        // Assert
        Assert.That(taxCalculation.TotalTaxPayable, Is.EqualTo(expectedTaxPayable));
        Assert.That(taxCalculation.Type, Is.EqualTo("Progressive"));
        Assert.That(taxCalculation.TaxRate, Is.EqualTo(0.1));
    }
}
