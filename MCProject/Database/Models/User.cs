using MCProject.Database.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MCProject.Database.Models;

public class User
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string GameName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public DateTime DateJoined { get; set; }

    public IQueryable<UserRoles> Roles { get; set; } = null!;

    public User()
    {

    }

    public User(string email, string gameName, string password)
    {
        Email = email;
        GameName = gameName;
        Password = password;
        DateJoined = DateTime.UtcNow;
    }

    public IQueryable<Role> GetRoles(MainContext context)
    {
        return context.UserRoles.Where(r => r.UserId == Id).Join(context.Roles, r => r.RoleId, r => r.Id, (_, role) => role);
    }
}
