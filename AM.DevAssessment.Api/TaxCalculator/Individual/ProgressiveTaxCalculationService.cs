
namespace AM.DevAssessment.Api.TaxCalculator.Individual;

public class ProgressiveTaxCalculationService : ITaxCalculatorService
{
    public TaxCalculation CalculateTax(decimal annualIncome)
    {
        decimal tax = 0m;
        List<TaxBracket> taxBrackets = new()
        {
            new() { Rate = 0.1m, From = 0, To = 8350 }, // 835 + 3839.7
            new() { Rate = 0.15m, From = 8351, To = 33950 },
            new() { Rate = 0.25m, From = 33951, To = 82250 },
            new() { Rate = 0.28m, From = 82251, To = 171550 },
            new() { Rate = 0.33m, From = 171551, To = 372950 },
            new() { Rate = 0.35m, From = 372951, To = decimal.MaxValue } // For incomes exceeding the highest bracket
        };

        foreach (var taxBracket in taxBrackets)
        {
            if (annualIncome < 0)
            {
                break;
            }

            if (annualIncome > taxBracket.From)
            {
                decimal taxableAmountInBracket = Math.Min(annualIncome - taxBracket.From, taxBracket.To - taxBracket.From);
                tax += taxableAmountInBracket * taxBracket.Rate;
                continue;
            }

        }

        return new(Type, tax);
    }

    public const string Type = "Progressive";
}

public class TaxBracket
{
    public decimal Rate { get; set; }
    public decimal From { get; set; }
    public decimal To { get; set; }
}