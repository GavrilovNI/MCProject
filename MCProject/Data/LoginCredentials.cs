using System.ComponentModel.DataAnnotations;

namespace MCProject.Data;

public class LoginCredentials : ValidatableData
{
    [Required]
    public string Email { get; set; } = null!;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required, Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

    protected override string? ValidateFixAndGetError()
    {
        if(System.Net.Mail.MailAddress.TryCreate(Email, out var email))
        {
            Email = email.Address;
            return null;
        }
        else
        {
            return "Email is incorrect";
        }
    }
}
