using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer
{
    [Serializable]
    public sealed class TransactionInProgressException : Exception
    {
        public TransactionInProgressException() { }
        public TransactionInProgressException(string message) : base(message) { }
        public TransactionInProgressException(string message, Exception inner) : base(message, inner) { }
        private TransactionInProgressException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
