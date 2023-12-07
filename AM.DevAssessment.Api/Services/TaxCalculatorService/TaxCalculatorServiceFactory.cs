using AM.DevAssessment.Api.Services.TaxCalculatorService.Individual;

namespace AM.DevAssessment.Api.Services.TaxCalculatorService;

public class TaxCalculatorServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TaxCalculatorServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITaxCalculatorService CreateTaxCalculatorService(string taxCalculatorType) =>
        // You can use the provided type string to create the appropriate service.
        taxCalculatorType switch
        {
            "Flat Value" => _serviceProvider.GetRequiredService<FlatValueTaxCalculationService>(),
            "Flat Rate" => _serviceProvider.GetRequiredService<FlatRateTaxCalculationService>(),
            "Progressive" => _serviceProvider.GetRequiredService<ProgressiveTaxCalculationService>(),
            _ => throw new ArgumentException($"Invalid tax calculator type: {taxCalculatorType}"),
        };
}