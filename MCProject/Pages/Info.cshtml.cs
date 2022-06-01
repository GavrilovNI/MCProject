using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MCProject.Pages;

[Authorize]
public class InfoModel : PageModel
{
    private readonly ILogger<InfoModel> _logger;

    public InfoModel(ILogger<InfoModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
