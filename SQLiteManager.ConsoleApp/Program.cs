using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteManager.Core;
using SQLiteManager.DataAccess;
using SQLiteManager.Managers;

//using static SQLiteManager.DataAccess.BankAccountDataAccess;

namespace SQLiteLManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestCreateTable();
            //TestFacadeAccess();
            TestDateCases();
            //TestSeparatedAccess();
            Console.ReadLine();
        }

        static async void TestDateCases()
        {
            StateManager.Instance.DatabaseName = "date_test";
            var database = new BudgetDatabase();
            database.Initialize();

            var transactions = await database.TransactionAccess.GetAllTransactionsAsync();
            Console.WriteLine("before write");
            foreach (var item in transactions)
            {
                Console.WriteLine(item);
            }


            var date1 = new DateTime(2021, 1, 5);
            var date2 = new DateTime(2021, 1, 10);
            var date3 = new DateTime(2021, 1, 15);
            var date4 = new DateTime(2021, 1, 20);

            var insertTrans1 = new BankTransaction
            {
                Timestamp = date1,
                Completed = false,
                Amount = 300.00m
            };
            var insertTrans2 = new BankTransaction
            {
                Timestamp = date2,
                Completed = true,
                Amount = 250.00m
            };
            var insertTrans3 = new BankTransaction
            {
                Timestamp = date3,
                Completed = false,
                Amount = 150.00m
            };
            var insertTrans4 = new BankTransaction
            {
                Timestamp = date4,
                Completed = false,
                Amount = 500.00m
            };

            await database.TransactionAccess.InsertTransactionAsync(insertTrans1);
            await database.TransactionAccess.InsertTransactionAsync(insertTrans2);
            await database.TransactionAccess.InsertTransactionAsync(insertTrans3);
            await database.TransactionAccess.InsertTransactionAsync(insertTrans4);


            var filtered = await database.TransactionAccess.GetTransactionsBetweenDates(date3, date4);
            Console.WriteLine("\nfiltered");
            foreach (var item in filtered)
            {
                Console.WriteLine(item);
            }
            transactions = await database.TransactionAccess.GetAllTransactionsAsync();
            Console.WriteLine("\nafter write");
            foreach (var item in transactions)
            {
                Console.WriteLine(item);
                await database.TransactionAccess.DeleteTransactionAsync(item.TransactionId);
            }



            transactions = await database.TransactionAccess.GetAllTransactionsAsync();
            Console.WriteLine("\nafter delete");
            foreach (var item in transactions)
            {
                Console.WriteLine(item);
            }







        }

        static async void TestFacadeAccess()
        {
            StateManager.Instance.DatabaseName = "test3";
            var database = new BudgetDatabase();
            database.Initialize();

            var recv = await database.AccountAccess.GetAllAccountsAsync();

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
            var added = await database.AccountAccess.InsertAccountAsync(newAcct);

            //var dbAcct = await database.AccountAccess.GetAccountAsync(added.Nickname);
            var bal = new Balance
            {
                Amount = 200.00m,
                Timestamp = DateTime.Now,
                AccountId = added.LastId 
                //BankAccount = dbAcct
            };


            var balAdded = await database.BalanceAccess.InsertBalanceAsync(bal);
            Console.WriteLine($"Balances added = {balAdded.RowsAffected}");

            var balances = await database.BalanceAccess.GetAllBalancesAsync();

            Console.WriteLine("Balances.....");
            foreach (var balance in balances)
            {
                Console.WriteLine(balance);
            }

            Console.WriteLine($"{added} entries added");

            recv = await database.AccountAccess.GetAllAccountsAsync();
            Console.WriteLine($"second pass count = {recv.Count}");
            foreach (var item in recv)
            {
                Console.WriteLine(item);
            }

            var toUpdate = await database.AccountAccess.GetAccountAsync(newAcct.Nickname);
            Console.WriteLine("db.GetAccount");
            Console.WriteLine(toUpdate);

            toUpdate.AccountNumber = "157849";

            await database.AccountAccess.UpdateAccountAsync(toUpdate);
            recv = await database.AccountAccess.GetAllAccountsAsync();
            Console.WriteLine($"update pass count = {recv.Count}");
            foreach (var item in recv)
            {
                Console.WriteLine(item);
            }

            //var balance = new Balance()



            var deleted = await database.AccountAccess.DeleteAccountAsync(recv.Single().AccountID);
            Console.WriteLine($"{deleted} entries deleted");

            recv = await database.AccountAccess.GetAllAccountsAsync();

            Console.WriteLine($"second pass count = {recv.Count}");
            foreach (var item in recv)
            {
                Console.WriteLine(item);
            }
        }

        //static void TestSeparatedAccess()
        //{
        //    StateManager.Instance.DatabaseName = "test2";
        //    var database = new BudgetDatabase();
        //    database.Initialize();

        //    var recv = GetAccounts();

        //    Console.WriteLine($"first pass count = {recv.Count}");
        //    foreach (var item in recv)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    var newAcct = new BankAccount
        //    {
        //        Nickname = "MainAccount",
        //        AccountNumber = "12345678",
        //        BankName = "Main Street Bank"
        //    };
        //    var added = InsertAccount(newAcct);

        //    Console.WriteLine($"{added} entries added");

        //    recv = GetAccounts();
        //    Console.WriteLine($"second pass count = {recv.Count}");
        //    foreach (var item in recv)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    var toUpdate = GetAccount(newAcct.Nickname);
        //    Console.WriteLine("db.GetAccount");
        //    Console.WriteLine(toUpdate);

        //    toUpdate.AccountNumber = "157849";

        //    UpdateAccount(toUpdate);
        //    recv = GetAccounts();
        //    Console.WriteLine($"update pass count = {recv.Count}");
        //    foreach (var item in recv)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    //var balance = new Balance()



        //    var deleted = DeleteAccount(recv.FirstOrDefault().AccountID);
        //    Console.WriteLine($"{deleted} entries deleted");

        //    recv = GetAccounts();

        //    Console.WriteLine($"second pass count = {recv.Count}");
        //    foreach (var item in recv)
        //    {
        //        Console.WriteLine(item);
        //    }
        //}
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
