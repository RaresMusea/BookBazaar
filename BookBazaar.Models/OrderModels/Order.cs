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

    [DisplayName("Order date")] public DateTime OrderDate { get; set; }

    [DisplayName("Delivery date")] public DateTime DeliveryDate { get; set; }

    public double GrandTotal { get; set; }

    [DisplayName("Order state")] public string? OrderState { get; set; } = string.Empty;

    [DisplayName("Transaction state")] public string? TransactionState { get; set; } = string.Empty;

    [DisplayName("Tracking number (AWB)")] public string? Awb { get; set; } = string.Empty;

    [DisplayName("Shipping provider")] public string? ShippingProvider { get; set; } = string.Empty;

    [DisplayName("Payment date")] public DateTime PaymentDate { get; set; }

    [DisplayName("Payment due date")] public DateTime PaymentDueDate { get; set; }

    public string? SessionId { get; set; } = string.Empty;

    [DisplayName("Transaction identifier")]
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