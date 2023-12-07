using AM.DevAssessment.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AM.DevAssessment.Pages;

public class SummaryModel : PageModel
{
    public List<TaxCalcuatationSummary> TaxCalculationSummaries { get; set; } = new List<TaxCalcuatationSummary>();
    public bool ShowResult = false;
    private readonly ILogger<SummaryModel> _logger;
    private readonly HttpClient _httpClient;

    public SummaryModel(ILogger<SummaryModel> logger, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        try
        {
            ShowResult = false;

            var responseMessage = 
                await _httpClient.GetFromJsonAsync<List<TaxCalcuatationSummary>>("http://am.devassessment.api/tax/individual");
            if (responseMessage is null)
            {
                return;
            }
            TaxCalculationSummaries = responseMessage;

            ShowResult = true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to process request");
        }
    }
}
