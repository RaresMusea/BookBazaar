using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookBazaar.Models.CompanyModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models;

public class AppUser : IdentityUser
{
    [Required] public string Name { get; set; } = string.Empty;

    public int? CompanyId { get; set; }

    [ForeignKey("CompanyId")]
    [ValidateNever]
    Company Company { get; set; }

    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}