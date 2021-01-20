using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteManager.Core
{
    public class Balance
    {
        #region Fields

        #endregion

        #region Properties
        public int BalanceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int AccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        #endregion

        #region Constructors
        public Balance()
        {
            Timestamp = DateTime.Now;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            //bankid = 0;
            return $"ID: {BalanceId}, Amount = {Amount}, timestamp = {Timestamp} bankid = {AccountId}";
        }
        #endregion

        #region Methods

        #endregion




    }
}
