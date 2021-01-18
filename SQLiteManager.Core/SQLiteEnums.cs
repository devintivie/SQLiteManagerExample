using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SQLiteManager.Core
{
    public enum SQLiteDataType
    {
        [Description("INTEGER")]
        Int,
        [Description("VARCHAR(20)")]
        VarChar,

    }

    public struct InsertResult
    {
        public int RowsAffected { get; set; }
        public int LastId { get; set; }

        public InsertResult(int rows, int id)
        {
            RowsAffected = rows;
            LastId = id;
        }

    }
}
