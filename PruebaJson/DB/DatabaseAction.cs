using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    internal enum DatabaseAction
    {
        Open,
        Close,
        Dispose,
        DataOperation,
        BeginsFirstTransaction,
        EndsLastTransaction
    }
}
