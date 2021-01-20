using SQLiteManager.Core;
using SQLiteManager.Managers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace SQLiteManager.DataAccess
{
    public class BudgetDatabase
    {
        #region Fields

        #endregion

        #region Properties
        public IAccountRepo AccountAccess { get; private set; }
        public ITransactionRepo TransactionAccess { get; set; }
        //public IBillRepo BillRepo { get; private set; }
        public IBalanceRepo BalanceAccess { get; private set; }
        public string ConnectionString => StateManager.Instance.DatabasePath;

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public void Initialize()
        {
            var accountTableName = "BankAccount";
            var balanceTableName = "Balance";
            var billTableName = "Bill";
            var transactionTableName = "BankTransaction";

            var accountTable = BuildBankAccountTable(accountTableName);
            var balanceTable = BuildBalanceTable(balanceTableName, accountTable);
            var billTable = BuildBillTable(billTableName, accountTable);
            var transactionTable = BuildTransactionTable(transactionTableName);

            CreateTable(accountTable);
            CreateTable(balanceTable);
            CreateTable(billTable);
            CreateTable(transactionTable);

            AccountAccess = new SQLiteAccountAccess(accountTableName);
            BalanceAccess = new SQLiteBalanceAccess(balanceTableName);
            TransactionAccess = new SQLiteTransactionAccess(transactionTableName);
        }

        private void CreateTable(SQLiteTable table)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand(table.BuildTableScript(), connection);
                cmd.ExecuteNonQuery();
            }
        }

        private SQLiteTable BuildBankAccountTable(string tableName)
        {
            //Build BankAccount Table
            var accountTable = new SQLiteTable(tableName);
            accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
            accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
            accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

            return accountTable;
        }

        private SQLiteTable BuildBalanceTable(string tableName, SQLiteTable accountTable)
        {
            //Build Balance Table
            var balanceTable = new SQLiteTable(tableName);
            balanceTable.AddColumn(new SQLiteColumn("BalanceId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            balanceTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            balanceTable.AddColumn(new SQLiteColumn("Timestamp").WithDatatype("TEXT"));
            balanceTable.AddColumn(new SQLiteColumn("AccountId"));
            balanceTable.AddForeignKey(new ForeignKey("FK_BalanceToBank", "AccountId", accountTable.TableName, "AccountId")
                .HasUpdateAction(ForeignKeyAction.CASCADE)
                .HasDeleteAction(ForeignKeyAction.CASCADE));
            return balanceTable;
        }

        private SQLiteTable BuildBillTable(string tableName, SQLiteTable accountTable)
        {
            //Build Bill Table
            var billTable = new SQLiteTable(tableName);
            billTable.AddColumn(new SQLiteColumn("BillId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            billTable.AddColumn(new SQLiteColumn("Date").WithDatatype("TEXT"));
            billTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            billTable.AddColumn(new SQLiteColumn("Payee").WithDatatype("TEXT"));
            billTable.AddColumn(new SQLiteColumn("IsPaid"));
            billTable.AddColumn(new SQLiteColumn("IsAuto"));
            billTable.AddColumn(new SQLiteColumn("AccountId"));
            billTable.AddForeignKey(new ForeignKey("FK_BillToBank", "AccountId", accountTable.TableName)
                .HasUpdateAction(ForeignKeyAction.CASCADE)
                .HasDeleteAction(ForeignKeyAction.SET_DEFAULT));

            return billTable;
        }

        private SQLiteTable BuildTransactionTable(string tableName)
        {
            //Build BankTransaction Table
            var transactionTable = new SQLiteTable(tableName);
            transactionTable.AddColumn(new SQLiteColumn("TransactionId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            transactionTable.AddColumn(new SQLiteColumn("Timestamp").WithDatatype("TEXT").IsUnique());
            transactionTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            transactionTable.AddColumn(new SQLiteColumn("Completed").WithDatatype("NUMERIC"));
            return transactionTable;
        }




        #endregion
    }
}
