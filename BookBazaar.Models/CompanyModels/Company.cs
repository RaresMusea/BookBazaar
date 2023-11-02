using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookBazaar.Models.CompanyModels;

public class Company
{
    [Key] public int Id { get; set; }

    [DisplayName("Company name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Headquarters address")] public string HeadquartersAddress { get; set; } = string.Empty;

    [Range(1, uint.MaxValue, ErrorMessage = "The number of employees should be a positive value")]
    [DisplayName("Number of employees")]
    public int NumberOfEmployees { get; set; }

    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    [DisplayName("Phone number")] public string Phone { get; set; } = string.Empty;

    [DisplayName("Date founded")] public DateOnly IncorporationDate { get; set; }
}