using ClearBank.DemoFramework.Types;

namespace ClearBank.DemoFramework.Services;

public interface IAccountService
{
    Account? GetAccount(string accountNumber);
    void UpdateAccount(Account account, MakePaymentRequest request);
}
