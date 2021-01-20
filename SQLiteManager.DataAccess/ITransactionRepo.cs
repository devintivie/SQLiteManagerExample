using SQLiteManager.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SQLiteManager.DataAccess
{
    public interface ITransactionRepo : IDataAccessRepo
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods
        Task<InsertResult> InsertTransactionAsync(BankTransaction tran);
        Task<List<BankTransaction>> GetAllTransactionsAsync();
        Task<BankTransaction> GetTransactionAsync(int id);
        Task<List<BankTransaction>> GetTransactionsBetweenDates(DateTime start, DateTime end);
        Task<int> DeleteTransactionAsync(int accountId);
        Task<int> UpdateTransactionAsync(BankTransaction tran);
        #endregion

    }
}
