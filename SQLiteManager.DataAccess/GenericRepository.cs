//using Dapper;
//using SQLiteManager.Core;
//using SQLiteManager.Managers;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SQLite;
//using System.Diagnostics;
//using System.Text;
//using System.Threading.Tasks;

//namespace SQLiteManager.DataAccess
//{
//    public class GenericRepository<T> where T : class
//    {
//        #region Fields
//        string connectionString = StateManager.Instance.DatabasePath;
//        #endregion

//        #region Properties
//        public string TableName { get; private set; }
//        #endregion

//        #region Constructors

//        #endregion

//        #region CRUD Methods
//        public async Task<int> InsertAsync(T t)
//        {
//            var rowsAffected = 0;
//            var lastId = 0;
//            var insertQuery = SQLiteHelpers.GenerateInsertQuery(TableName, t);

//            using(IDbConnection conn = new SQLiteConnection(connectionString))
//            {
//                try
//                {
//                    rowsAffected = await conn.ExecuteAsync(insertQuery);
//                }
//                catch (SQLiteException sqle)
//                {
//                    Debug.WriteLine(sqle.Message);
//                }
//            }

//            return rowsAffected;
//        }

//        public async Task<IEnumerable<T>> GetAllAsync()
//        {
//            using (IDbConnection conn = new SQLiteConnection(connectionString))
//            {
//                return await conn.QueryAsync<T>($"SELECT * from { TableName }");
//            }
//        }
//        #endregion


//    }
//}
