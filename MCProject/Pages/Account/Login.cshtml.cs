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

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginCredentials Credentials { get; set; } = null!;

    private readonly MainContext _context;
    private readonly IPasswordHasher _passwordHasher = new PasswordHasher();

    public LoginModel(MainContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
    }


    private List<Claim> CreateClaims(User user)
    {
        List<Claim> claims = new();
        claims.Add(new Claim(ClaimTypes.Name, user.GameName));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));

        foreach(var role in user.GetRoles(_context))
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        
        return claims;
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

        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Credentials.Email);

        bool foundAndVerified = user != null &&
            _passwordHasher.Test(Credentials.Password, user.Password);

        if(foundAndVerified == false)
        {
            Credentials.Error = "User not found";
            return Page();
        }

        var claims = CreateClaims(user);

        ClaimsIdentity identity = new(claims, Startup.CookieName);
        ClaimsPrincipal principal = new(identity);

        AuthenticationProperties authProperties = new()
        {
            IsPersistent = Credentials.RememberMe
        };

        await HttpContext.SignInAsync(Startup.CookieName, principal, authProperties);

        return RedirectToPage(String.IsNullOrWhiteSpace(returnUrl) ? "/Index" : returnUrl);
    }
}
