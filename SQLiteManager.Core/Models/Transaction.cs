using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteManager.Core
{
    public class BankTransaction
    {
        public int TransactionId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public bool Completed { get; set; }


        public override string ToString()
        {
            var completedString = Completed ? "Completed" : "Not Complete";
            return $"ID = {TransactionId}|| {completedString}: Time : {Timestamp}, {Amount}";
        }
    }
}
