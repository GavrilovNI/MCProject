using System.ComponentModel.DataAnnotations;

namespace MCProject.Data;

public class RegisterCredentials : ValidatableData
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string GameName { get; set; } = null!;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required, DataType(DataType.Password)]
    public string PasswordConfirm { get; set; } = null!;

    protected override string? ValidateFixAndGetError()
    {
        if(System.Net.Mail.MailAddress.TryCreate(Email, out var email))
            Email = email.Address;
        else
            return "Email is incorrect";

        if(Password != PasswordConfirm)
            return "Passwords are not equal";

        return null;
    }
}
