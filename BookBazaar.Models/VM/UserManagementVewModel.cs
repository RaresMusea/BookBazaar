using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBazaar.Models.VM;

public class UserManagementVewModel
{
    public UserViewModel UserDetails { get; set; } = null!;
    public IEnumerable<SelectListItem> AvailableAppRoles { get; set; } = null!;
    public IEnumerable<SelectListItem> Companies { get; set; } = null!;
}