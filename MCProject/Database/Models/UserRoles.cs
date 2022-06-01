using System.ComponentModel.DataAnnotations;

namespace MCProject.Database.Models;

public class UserRoles
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int RoleId { get; set; }

    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
