using Microsoft.AspNetCore.Identity;

namespace CafeWithCats.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}