using System;

namespace ClearBank.DemoFramework.Types;

public record MakePaymentRequest
{
    public required string CreditorAccountNumber { get; init; }
    public required string DebtorAccountNumber { get; init; }
    public decimal Amount { get; init; }
    public PaymentScheme PaymentScheme { get; init; }
}
