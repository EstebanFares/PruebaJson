using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DataLayer
{
    [Serializable]
    public sealed class DatabaseNotDisposedException : Exception
    {
        [DebuggerNonUserCode()]
        public DatabaseNotDisposedException() { }
        [DebuggerNonUserCode()]
        public DatabaseNotDisposedException(string message) : base(message) { }
        [DebuggerNonUserCode()]
        public DatabaseNotDisposedException(string message, Exception inner) : base(message, inner) { }
        [DebuggerNonUserCode()]
        private DatabaseNotDisposedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
