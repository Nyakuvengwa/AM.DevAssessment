using AM.DevAssessment.Api.Models;
using FluentValidation;

namespace AM.DevAssessment.Api.Validations;

public class IndividualTaxCalculationRequestValidator : AbstractValidator<IndividualTaxCalculationRequest>
{
    public IndividualTaxCalculationRequestValidator()
    {
        RuleFor(x => x).NotNull();

        RuleFor(x => x.AnnualIncome).
            GreaterThanOrEqualTo(0);

        RuleFor(x => x.PostalCode)
            .NotNull()
            .NotEmpty()
            .Must(BeInPostalCodeList).WithMessage("Invalid postal code provided.");

        static bool BeInPostalCodeList(string postalCode)
        {           
            return new string[] { "7441", "A100", "7000", "1000" }.Contains(postalCode);
        }
    }
}
