using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace democonfiguracion.Pages;
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;
    private readonly IConfiguration configuration;

    public string NombreCurso { get; private set; }

    public PrivacyModel(ILogger<PrivacyModel> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        this.configuration = configuration;
    }

    public void OnGet()
    {
        NombreCurso = configuration.GetValue<string>("Curso");
    }
}

