using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.OrderModels;

public class Order
{
    [Key] public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    [ValidateNever] [ForeignKey("UserId")] public AppUser User { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public double GrandTotal { get; set; }

    public string? Status { get; set; } = string.Empty;

    public string? TransactionState { get; set; } = string.Empty;

    public string? Awb { get; set; } = string.Empty;

    public string? ShippingProvider { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public DateTime PaymentDueDate { get; set; }

    public string? TransactionId { get; set; } = string.Empty;

    [Required]
    [DisplayName("Recipient name")]
    public string Name { get; set; } = string.Empty;

    [DisplayName("Phone number")]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [DisplayName("Address line")]
    [Required]
    public string Address { get; set; } = string.Empty;

    [Required] public string City { get; set; } = string.Empty;

    [Required] public string Country { get; set; } = string.Empty;
}