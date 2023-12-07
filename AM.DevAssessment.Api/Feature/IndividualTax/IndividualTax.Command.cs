using AM.DevAssessment.Api.Data;
using AM.DevAssessment.Api.Data.Entities;
using AM.DevAssessment.Api.Models;
using AM.DevAssessment.Api.Services.TaxCalculatorService;
using MediatR;

namespace AM.DevAssessment.Api.Feature.IndividualTax;

public sealed record IndividualTaxCommand(decimal AnnualIncome, string PostalCode) : IRequest<IndividualTaxCalculationResponse>;

public sealed class IndividualTaxCommandHandler : IRequestHandler<IndividualTaxCommand, IndividualTaxCalculationResponse>
{
    private readonly TaxCalculatorServiceFactory _taxCalculatorServiceFactory;
    private readonly TaxDbContext _taxDbContext;

    public IndividualTaxCommandHandler(TaxCalculatorServiceFactory taxCalculatorServiceFactory, TaxDbContext taxDbContext)
    {
        _taxCalculatorServiceFactory = taxCalculatorServiceFactory;
        _taxDbContext = taxDbContext;
    }

    public async Task<IndividualTaxCalculationResponse> Handle(IndividualTaxCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        await Task.Delay(1000, cancellationToken);

        var calculationType = request.PostalCode switch
        {
            "7441" => "Progressive",
            "A100" => "Flat Value",
            "7000" => "Flat rate",
            "1000" => "Progressive",
            _ => throw new ArgumentException($"Invalid postal code : {request.PostalCode}"),
        };

        if (string.IsNullOrEmpty(calculationType))
        {
            throw new ArgumentException($"Invalid postal code : {request.PostalCode}");
        }

        var taxCalculator = _taxCalculatorServiceFactory.CreateTaxCalculatorService(calculationType)
            ?? throw new ArgumentException($"Invalid tax calculator type");

        var tax = taxCalculator.CalculateTax(request.AnnualIncome)
            ?? throw new Exception("Unable to calculate tax");

        await _taxDbContext.CalculatedTaxAudits.AddAsync(new CalculatedTaxAudit
        {
            AnnualIncome = request.AnnualIncome,
            CreatedDate = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid(),
            PostalCode = request.PostalCode,
            TaxCalculationType = tax.Type,
            TotalTaxPayable = tax.TotalTaxPayable
        }, cancellationToken);

        await _taxDbContext.SaveChangesAsync(cancellationToken);

        return new IndividualTaxCalculationResponse(tax.TotalTaxPayable, tax.Type);
    }
}

