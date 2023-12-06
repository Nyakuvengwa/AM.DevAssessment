using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AM.DevAssessment.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required]
        public decimal AnnualIncome { get; set; }
        [BindProperty]
        [Required, MinLength(3)]
        public string PostalCode { get; set; } = string.Empty;

        private readonly HttpClient _httpClient;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(HttpClient httpClient, ILogger<IndexModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("{AnnualIncome}-{PostalCode}", AnnualIncome, PostalCode);
                try
                {
                var result = await _httpClient.PostAsJsonAsync("https://am.devassessment.api/tax/individual/calculate", new IndividualTaxCalculationRequest(AnnualIncome, PostalCode));

                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unable to process request");
                }
            }

            return Page();
        }
    }
}
public record IndividualTaxCalculationRequest(
    decimal AnnualIncome,
    string PostalCode);