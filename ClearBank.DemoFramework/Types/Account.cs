namespace ClearBank.DemoFramework.Types;

public record Account
{
    public required string AccountNumber { get; init; }
    public decimal Balance { get; set; }
    public AccountStatus Status { get; init; }
    public AllowedPaymentSchemes AllowedPaymentSchemes { get; init; }
}
