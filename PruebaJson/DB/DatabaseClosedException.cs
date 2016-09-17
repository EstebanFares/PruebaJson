using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DataLayer
{
    [Serializable]
    public sealed class DatabaseClosedException : Exception
    {
        [DebuggerNonUserCode()]
        public DatabaseClosedException() { }
        [DebuggerNonUserCode()]
        public DatabaseClosedException(string message) : base(message) { }
        [DebuggerNonUserCode()]
        public DatabaseClosedException(string message, Exception inner) : base(message, inner) { }
        [DebuggerNonUserCode()]
        private DatabaseClosedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
