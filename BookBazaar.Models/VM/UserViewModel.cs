namespace BookBazaar.Models.VM;

public class UserViewModel
{
    public AppUser.AppUser User { get; init; } = null!;
    public string? Role { get; init; } = string.Empty;
}