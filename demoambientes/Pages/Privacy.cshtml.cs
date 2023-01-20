using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demoambientes.Pages;
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger, 
        IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        HostEnvironment = hostEnvironment;
    }

    public IHostEnvironment HostEnvironment { get; }

    public void OnGet()
    {
        if (HostEnvironment.IsProduction())
        {
            //...
        }

    }
}

