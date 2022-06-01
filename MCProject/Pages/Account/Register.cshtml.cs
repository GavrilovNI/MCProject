using MCProject.Data;
using MCProject.Database.Data;
using MCProject.Database.Models;
using MCProject.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MCProject.Pages.Account;

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterCredentials Credentials { get; set; } = null!;

    private readonly MainContext _context;
    private readonly IPasswordHasher _passwordHasher = new PasswordHasher();

    public RegisterModel(MainContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
    }

    public async Task<bool> TryRegister(RegisterCredentials credentials)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == credentials.Email || u.GameName == credentials.GameName);

        if(user == null)
        {
            var hash = _passwordHasher.Hash(Credentials.Password);
            user = new(credentials.Email, credentials.GameName, hash);
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        if(ModelState.IsValid == false)
        {
            Credentials.Error = "Form not filled correctly";
            return Page();
        }

        if(Credentials.ValidateAndFix() == false)
            return Page();

        bool registered = await TryRegister(Credentials);

        if(registered)
        {
            return RedirectToPage("/Account/Login");
        }
        else
        {
            Credentials.Error = "User with same email or game name already exists";
            return Page();
        }
    }
}
