using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteManager.Core
{
    public interface ITableAttribute
    {
        string ToDbString();
    }
}
