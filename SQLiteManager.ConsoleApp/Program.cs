using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteManager.Core;

namespace SQLiteLManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCreateTable();
            Console.ReadLine();
        }

        static void TestCreateTable()
        {
            //var manager = new SQLiteHelper("test1");
            //var id = new SQLiteColumn("Id").AsNullable(false).AsPrimaryKey(true).WithAutoIncrement(true).IsUnique(true);
            //var firstName = new SQLiteColumn("FirstName").WithDatatype("TEXT").AsNullable(false);
            //var lastName = new SQLiteColumn("LastName").WithDatatype("TEXT");

            //var columns = new List<SQLiteColumn> { id, firstName, lastName };
            //var tableName = "Person";

            //var people = manager.LoadPeople();

            //foreach (var p in people)
            //{
            //    Console.WriteLine(p.FullName);
            //}

            var accountTable = new SQLiteTable("BankAccount");
            accountTable.AddColumn(new SQLiteColumn("AccountId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            accountTable.AddColumn(new SQLiteColumn("NickName").WithDatatype("TEXT").IsUnique());
            accountTable.AddColumn(new SQLiteColumn("AccountNumber").WithDatatype("TEXT").AsNullable(true));
            accountTable.AddColumn(new SQLiteColumn("BankName").WithDatatype("TEXT").AsNullable(true));

            var balanceTable = new SQLiteTable("Balance");
            balanceTable.AddColumn(new SQLiteColumn("BalanceId").AsPrimaryKey().WithAutoIncrement().IsUnique());
            balanceTable.AddColumn(new SQLiteColumn("Amount").WithDatatype("FLOAT"));
            balanceTable.AddColumn(new SQLiteColumn("Timestamp").WithDatatype("TEXT"));
            balanceTable.AddColumn(new SQLiteColumn("AccountId"));
            balanceTable.AddForeignKey(new ForeignKey("FK_BalanceToBank", "AccountId", accountTable.TableName, "AccountId")
                .HasUpdateAction(ForeignKeyAction.CASCADE)
                .HasDeleteAction(ForeignKeyAction.CASCADE));

            var billTable = new SQLiteTable("Bill");
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

            var db = new SQLiteHelper("test1");
            db.CreateTable(accountTable);
            db.CreateTable(balanceTable);
            db.CreateTable(billTable);

            var recv = db.GetAccounts();

            Console.WriteLine($"first pass count = {recv.Count}");
            foreach (var item in recv)
            {
                Console.WriteLine(item);
            }

            var newAcct = new BankAccount
            {
                Nickname = "MainAccount",
                AccountNumber = "12345678",
                BankName = "Main Street Bank"
            };
            var added = db.CreateAccount(newAcct);

            Console.WriteLine($"{added.RowsAffected} entries added");
            
            recv = db.GetAccounts();
            Console.WriteLine($"second pass count = {recv.Count}");
            foreach (var item in recv)
            {
                Console.WriteLine(item);
            }

            var toUpdate = db.GetAccount(newAcct.Nickname);
            Console.WriteLine("db.GetAccount");
            Console.WriteLine(toUpdate);

            toUpdate.AccountNumber = "157849";

            db.UpdateAccount(toUpdate);
            recv = db.GetAccounts();
            Console.WriteLine($"update pass count = {recv.Count}");
            foreach (var item in recv)
            {
                Console.WriteLine(item);
            }

            //var balance = new Balance()



            var deleted = db.DeleteAccount(recv.FirstOrDefault().AccountID);
            Console.WriteLine($"{deleted} entries deleted");

            recv = db.GetAccounts();

            Console.WriteLine($"second pass count = {recv.Count}");
            foreach (var item in recv)
            {
                Console.WriteLine(item);
            }


            //manager.CreateTable(tableName, columns);
            //var person1 = new PersonModel("Devin", "Ivie");
            //manager.SavePerson(person1);

            //var people = manager.GetPeople()

            //Console.WriteLine(id.ToDbString());
            //Console.WriteLine(firstName.ToDbString());
            //Console.WriteLine(lastName.ToDbString());
            //manager.CreateTable
        }
    }
}
