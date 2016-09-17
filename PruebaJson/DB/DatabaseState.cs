using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public enum DatabaseState
    {
        Closed,
        Opened,
        WithTransactionsInProgress,
        Disposed
    }
}
