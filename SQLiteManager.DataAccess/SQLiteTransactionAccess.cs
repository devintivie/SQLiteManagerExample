using Dapper;
using SQLiteManager.Core;
using SQLiteManager.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteManager.DataAccess
{
    public class SQLiteTransactionAccess : ITransactionRepo
    {
        #region Fields

        #endregion

        #region Properties
        public string TableName { get; private set; }
        public string ConnectionString => StateManager.Instance.DatabasePath;
        #endregion

        #region Constructors
        public SQLiteTransactionAccess(string name)
        {
            TableName = name;
        }
        #endregion

        #region Methods

        
        public async Task<InsertResult> InsertTransactionAsync(BankTransaction tran)
        {
            var rowsAffected = 0;
            var lastId = 0;
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    rowsAffected = await conn.ExecuteAsync($"insert into {TableName} (Timestamp, Amount, Completed) values (@Timestamp, @Amount, @Completed)", tran);
                    lastId = conn.ExecuteScalar<int>($"select max(TransactionId) from {TableName}");
                }
                catch (SQLiteException sqle)
                {
                    Debug.WriteLine(sqle.Message);
                }
            }
            return new InsertResult(rowsAffected, lastId);
        }


        public async Task<int> DeleteTransactionAsync(int transactionId)
        {
            var rowsAffected = 0;
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                rowsAffected = await conn.ExecuteAsync($"delete from {TableName} where TransactionId=@ID", new { ID = transactionId });
            }
            return rowsAffected;
        }

        public async Task<List<BankTransaction>> GetAllTransactionsAsync()
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                var output = await conn.QueryAsync<BankTransaction>($"SELECT * from { TableName }");
                return output.ToList();
            }
        }

        public async Task<BankTransaction> GetTransactionAsync(int id)
        {
            throw new NotImplementedException();
        }

        
        public async Task<int> UpdateTransactionAsync(BankTransaction tran)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BankTransaction>> GetTransactionsBetweenDates(DateTime start, DateTime end)
        {
            using (IDbConnection conn = new SQLiteConnection(ConnectionString))
            {
                var p = new
                {
                    Completed = false,
                    Start = start,
                    End = end
                };

                //WHERE IsPaid = @IsPaid OR
                var query = $@"SELECT * FROM {TableName} 
                        WHERE Completed = @Completed 
                        OR Timestamp BETWEEN @Start AND @End";
                var output = await conn.QueryAsync<BankTransaction>(query, p);
                return output.ToList();
            }
        }
        #endregion
    }
}
