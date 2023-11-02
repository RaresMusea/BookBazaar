using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookBazaar.Models.CompanyModels;

public class Company
{
    [Key] public int Id { get; set; }

    [DisplayName("Company name")]
    [MaxLength(35)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Headquarters address")] public string HeadquartersAddress { get; set; } = string.Empty;

    [Range(1, uint.MaxValue, ErrorMessage = "The number of employees should be a positive value")]
    [DisplayName("Number of employees")]
    public int NumberOfEmployees { get; set; }

    [MinLength(3, ErrorMessage = "The company city should contain at least 3 characters!")]
    [MaxLength(25, ErrorMessage = "The company city should contain at most 25 characters!")]
    public string City { get; set; } = string.Empty;

    [MinLength(5, ErrorMessage = "The company country should contain at least 5 characters!")]
    [MaxLength(20, ErrorMessage = "The company country should contain at most 20 characters!")]
    public string Country { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    [DisplayName("Phone number")] public string Phone { get; set; } = string.Empty;

    [DisplayName("Date founded")] public DateTime IncorporationDate { get; set; }
}