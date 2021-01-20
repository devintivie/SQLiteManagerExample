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

namespace SQLiteManager.DataAccess
{
    public static class BankAccountDataAccess
    {
        static string connectionString = StateManager.Instance.DatabasePath;
        public static int InsertAccount(BankAccount acct)
        {
            var rowsAffected = 0;
            var lastId = 0;
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    rowsAffected = connection.Execute("insert into BankAccount (NickName, AccountNumber, BankName) values (@NickName, @AccountNumber, @BankName)", acct);
                    lastId = connection.ExecuteScalar<int>("select max(AccountId) from BankAccount");
                }
                catch (SQLiteException sqle)
                {
                    Debug.WriteLine(sqle.Message);
                }
            }
            return rowsAffected;// new InsertResult(rowsAffected, lastId);
        }

        public static List<BankAccount> GetAccounts()
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var output = connection.Query<BankAccount>("select * from BankAccount");//, new DynamicParameters());
                return output.ToList();
            }
        }

        public static BankAccount GetAccount(string nickname)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var output = connection.Query<BankAccount>("select * from BankAccount where NickName = @Nickname", new { Nickname = nickname });
                return output.Single();
            }
        }

        public static int UpdateAccount(BankAccount acct)
        {
            var rowsAffected = 0;
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                rowsAffected = connection.Execute(@"UPDATE BankAccount SET 
                    Nickname = @Nickname,
                    AccountNumber = @AccountNumber,
                    BankName = @BankName
                    WHERE AccountId = @AccountId", acct);
            }
            return rowsAffected;
        }

        public static int DeleteAccount(int accountId)
        {
            var rowsAffected = 0;
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                rowsAffected = connection.Execute("delete from BankAccount where AccountId=@ID", new { ID = accountId });
            }
            return rowsAffected;
        }




    }

    
}
