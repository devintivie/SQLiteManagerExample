using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteManager.Core
{
    public class BankAccount
    {
        #region Fields

        #endregion

        #region Properties
        public int AccountID { get; set; }
        public string Nickname { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }

        #endregion

        #region Constructors

        #endregion

        #region Override Methods
        public override string ToString()
        {
            return $"ID = {AccountID}: {Nickname}, Acct# = {AccountNumber} at '{BankName}'";
        }
        #endregion

        #region Methods

        #endregion

    }
}
