using Microsoft.Extensions.Logging;
using ClearBank.DemoFramework.Services;
using ClearBank.DemoFramework.Types;
using Moq;
using Xunit;

namespace ClearBank.DemoFramework.Tests.Services;

public class PaymentServiceTests
{
    private readonly Mock<IAccountService> _accountServiceMock;
    private readonly Mock<ILogger<PaymentService>> _loggerMock;
    private readonly PaymentService _sut;

    public PaymentServiceTests()
    {
        _accountServiceMock = new Mock<IAccountService>();
        _loggerMock = new Mock<ILogger<PaymentService>>();
        _sut = new PaymentService(_accountServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void MakePayment_WithNullRequest_ReturnsFalse()
    {
        // Act
        var result = _sut.MakePayment(null);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void MakePayment_WithValidFasterPaymentRequest_ReturnsTrue()
    {
        // Arrange
        var request = new MakePaymentRequest 
        { 
            PaymentScheme = PaymentScheme.FasterPayments,
            Amount = 100,
            DebtorAccountNumber = "12345",
            CreditorAccountNumber = "67890"
        };

        var account = new Account
        {
            AccountNumber = "12345",
            Balance = 200,
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Status = AccountStatus.Live
        };

        _accountServiceMock
            .Setup(x => x.GetAccount(request.DebtorAccountNumber))
            .Returns(account);

        // Act
        var result = _sut.MakePayment(request);

        // Assert
        Assert.True(result.Success);
        _accountServiceMock.Verify(x => x.UpdateAccount(account, request), Times.Once);
    }
}
