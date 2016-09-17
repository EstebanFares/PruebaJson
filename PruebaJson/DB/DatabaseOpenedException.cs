using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DataLayer
{
    [Serializable]
    public sealed class DatabaseOpenedException : Exception
    {
        [DebuggerNonUserCode()]
        public DatabaseOpenedException() { }
        [DebuggerNonUserCode()]
        public DatabaseOpenedException(string message) : base(message) { }
        [DebuggerNonUserCode()]
        public DatabaseOpenedException(string message, Exception inner) : base(message, inner) { }
        [DebuggerNonUserCode()]
        private DatabaseOpenedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
