using AM.DevAssessment.Api.Services.TaxCalculatorService.Individual;

namespace AM.DevAssessment.Api.Services.TaxCalculatorService;

public static class TaxCalculatorServiceExtensions
{
    public static void AddTaxCalculators(this IServiceCollection services)
    {
        services.AddTransient<FlatValueTaxCalculationService>();
        services.AddTransient<FlatRateTaxCalculationService>();
        services.AddTransient<ProgressiveTaxCalculationService>();
        services.AddScoped<TaxCalculatorServiceFactory>();
    }
}
