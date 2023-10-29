using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookBazaar.Models;

public class AppUser : IdentityUser
{
    [Required] public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}