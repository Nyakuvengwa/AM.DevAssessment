using AM.DevAssessment.Models;
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

        public string Error { get; set; } = string.Empty;
        [BindProperty]
        public bool IsCalculating { get; set; } = false;
        public IndividualTaxCalculationResponse? IndividualTaxCalculation { get; set; } = null;
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                IsCalculating = true;

                var responseMessage = await _httpClient.PostAsJsonAsync("http://am.devassessment.api/tax/individual/calculate", new IndividualTaxCalculationRequest(AnnualIncome, PostalCode));

                IsCalculating = false;
                if (responseMessage is null)
                {
                    return Page();
                }

                if(!responseMessage.IsSuccessStatusCode)
                {
                    var error = await responseMessage.Content.ReadFromJsonAsync<ProblemDetails>();

                    Error = $"{error?.Title} {string.Join(",",error?.Extensions?.Values?.Select(x => x?.ToString()))}";
                    return Page();
                }
                var response = await responseMessage.Content.ReadFromJsonAsync<IndividualTaxCalculationResponse>();
                if (response is null)
                {
                    return Page();
                }
                IndividualTaxCalculation = response;
            }
            catch (Exception e)
            {
                Error = e.Message;
                _logger.LogError(e, "Unable to process request");
            }

            return Page();
        }
    }
}