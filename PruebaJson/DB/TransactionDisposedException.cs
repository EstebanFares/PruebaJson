using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    [Serializable]
    public sealed class TransactionDisposedException : Exception
    {
        public TransactionDisposedException() { }
        public TransactionDisposedException(string message) : base(message) { }
        public TransactionDisposedException(string message, Exception inner) : base(message, inner) { }
        private TransactionDisposedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
