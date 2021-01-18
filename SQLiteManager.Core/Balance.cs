using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteManager.Core
{
    public class Balance
    {
        public int BalanceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int AccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public Balance()
        {
            Timestamp = DateTime.Now;
        }
    }
}
