namespace BookBazaar.Models.VM;

public class UserViewModel
{
    public AppUser.AppUser User { get; set; } = null!;
    public string? Role { get; set; } = string.Empty;
}