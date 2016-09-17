using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    [Serializable]
    public sealed class TransactionNotDisposedException : Exception
    {
        public TransactionNotDisposedException() { }
        public TransactionNotDisposedException(string message) : base(message) { }
        public TransactionNotDisposedException(string message, System.Exception inner) : base(message, inner) { }
        private TransactionNotDisposedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
