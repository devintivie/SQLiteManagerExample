using SQLiteManager.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLiteManager.DataAccess
{
    public interface IDataAccessRepo
    {
        #region Fields

        #endregion

        #region Properties
        string ConnectionString { get; }
        string TableName { get; }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion


    }
}