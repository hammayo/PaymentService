﻿using ClearBank.DemoFramework.Types;

namespace ClearBank.DemoFramework.Data
{
    public class BackupAccountDataStore : IAccountDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Access backup data base to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
