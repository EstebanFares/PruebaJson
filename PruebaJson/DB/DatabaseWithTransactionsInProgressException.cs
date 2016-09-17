using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DataLayer
{
    [Serializable]
    public sealed class DatabaseWithTransactionsInProgressException : Exception
    {
        [DebuggerNonUserCode()]
        public DatabaseWithTransactionsInProgressException() { }
        [DebuggerNonUserCode()]
        public DatabaseWithTransactionsInProgressException(string message) : base(message) { }
        [DebuggerNonUserCode()]
        public DatabaseWithTransactionsInProgressException(string message, Exception inner) : base(message, inner) { }
        [DebuggerNonUserCode()]
        private DatabaseWithTransactionsInProgressException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
