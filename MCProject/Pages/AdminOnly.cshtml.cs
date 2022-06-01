using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MCProject.Pages;

[Authorize(Roles = "Admin")]
public class AdminOnlyModel : PageModel
{
    private readonly ILogger<AdminOnlyModel> _logger;

    public AdminOnlyModel(ILogger<AdminOnlyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
