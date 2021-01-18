using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteManager.Core
{
    public enum ForeignKeyAction
    {
        NO_ACTION,
        RESTRICT,
        SET_NULL,
        SET_DEFAULT,
        CASCADE
    }
}
