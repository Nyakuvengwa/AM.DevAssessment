using AM.DevAssessment.Api.Data;
using AM.DevAssessment.Api.Data.Entities;
using AM.DevAssessment.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AM.DevAssessment.Api.Feature.IndividualTax;

public record GetIndividualTaxCalculationsQuery() : IRequest<List<CalculatedTaxAuditModel>>;

public class GetIndividualTaxCalculationsQueryHandler : IRequestHandler<GetIndividualTaxCalculationsQuery, List<CalculatedTaxAuditModel>>
{
    public readonly TaxDbContext _taxDbContext;

    public GetIndividualTaxCalculationsQueryHandler(TaxDbContext taxDbContext)
    {
        _taxDbContext = taxDbContext;
    }

    public async Task<List<CalculatedTaxAuditModel>> Handle(GetIndividualTaxCalculationsQuery request, CancellationToken cancellationToken)
    {
        return await _taxDbContext.CalculatedTaxAudits
            .AsNoTracking()
            .OrderBy(x => x.CreatedDate)
            .Take(25)
            .Select(x => new CalculatedTaxAuditModel
            {
                Id = x.Id,
                AnnualIncome = x.AnnualIncome,
                CreatedDate = x.CreatedDate,
                PostalCode = x.PostalCode,
                TaxCalculationType = x.TaxCalculationType,
                TotalTaxPayable = x.TotalTaxPayable,
            })
            .ToListAsync(cancellationToken);
    }
}

