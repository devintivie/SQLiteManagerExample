//using SQLiteManager.Core;
//using SQLiteManager.Managers;
//using System;
//using System.Data.SQLite;

//namespace SQLiteManager.DataAccess
//{
//    public static class SQLiteInit
//    {
//        public static void Initialize()
//        {
//            //Build BankAccount Table
//            var accountTable = new SQLiteTable("BankAccount");
//            accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
//            accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
//            accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
//            accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

//            //Build Balance Table
//            var balanceTable = new SQLiteTable("Balance");
//            balanceTable.AddColumn(new SQLiteColumn("BalanceId").AsPrimaryKey().WithAutoIncrement().IsUnique());
//            balanceTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
//            balanceTable.AddColumn(new SQLiteColumn("Timestamp").WithDatatype("TEXT"));
//            balanceTable.AddColumn(new SQLiteColumn("AccountId"));
//            balanceTable.AddForeignKey(new ForeignKey("FK_BalanceToBank", "AccountId", accountTable.TableName, "AccountId")
//                .HasUpdateAction(ForeignKeyAction.CASCADE)
//                .HasDeleteAction(ForeignKeyAction.CASCADE));

//            //Build Bill Table
//            var billTable = new SQLiteTable("Bill");
//            billTable.AddColumn(new SQLiteColumn("BillId").AsPrimaryKey().WithAutoIncrement().IsUnique());
//            billTable.AddColumn(new SQLiteColumn("Date").WithDatatype("TEXT"));
//            billTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
//            billTable.AddColumn(new SQLiteColumn("Payee").WithDatatype("TEXT"));
//            billTable.AddColumn(new SQLiteColumn("IsPaid"));
//            billTable.AddColumn(new SQLiteColumn("IsAuto"));
//            billTable.AddColumn(new SQLiteColumn("AccountId"));
//            billTable.AddForeignKey(new ForeignKey("FK_BillToBank", "AccountId", accountTable.TableName)
//                .HasUpdateAction(ForeignKeyAction.CASCADE)
//                .HasDeleteAction(ForeignKeyAction.SET_DEFAULT));

//            CreateTable(accountTable);
//            CreateTable(balanceTable);
//            CreateTable(billTable);
//        }

//        private static void CreateTable(SQLiteTable table)
//        {
//            using (var connection = new SQLiteConnection(StateManager.Instance.DatabasePath))
//            {
//                connection.Open();
//                var cmd = new SQLiteCommand(table.BuildTableScript(), connection);
//                cmd.ExecuteNonQuery();
//            }
//        }
//    }
//}
