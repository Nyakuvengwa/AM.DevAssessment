
namespace AM.DevAssessment.Api.TaxCalculator.Individual;

public class ProgressiveTaxCalculationService : ITaxCalculatorService
{
    public TaxCalculation CalculateTax(decimal annualIncome)
    {
        decimal tax = 0m;
        List<TaxBracket> taxBrackets = new()
        {
            new(0.1m, 0, 8350 ), // 835 + 3839.7
            new(0.15m, 8351, 33950 ),
            new(0.25m,  33951, 82250 ),
            new(0.28m,  82251, 171550 ),
            new(0.33m,  171551, 372950 ),
            new(0.35m,  372951, decimal.MaxValue) 
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
    public decimal Rate { get; }

    public decimal From { get; }
    public decimal To { get; }

    public TaxBracket(decimal rate, decimal from, decimal to)
    {
        Rate = rate;
        From = from;
        To = to;
    }

}