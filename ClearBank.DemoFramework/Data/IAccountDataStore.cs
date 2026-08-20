using ClearBank.DemoFramework.Types;

namespace ClearBank.DemoFramework.Data;

public interface IAccountDataStore
{
    Account? GetAccount(string accountNumber);
    void UpdateAccount(Account account);
}
