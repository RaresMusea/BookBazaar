namespace BookBazaar.Misc.Orders_Payments;

public static class PaymentStatus
{
    public static readonly string Pending = "Pending";
    public static readonly string Approved = "Approved";
    public static readonly string BusinessDelayed = "Approved for delayed payment";
    public static readonly string Rejected = "Rejected";
    public static readonly string Refunded = "Refunded";
}