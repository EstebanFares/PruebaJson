using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    internal enum TransactionAction
    {
        DataOperation,
        Commit,
        Rollback,
        Dispose
    }
}
