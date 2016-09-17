using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    [Serializable]
    public sealed class TransactionEndedException : Exception
    {
        public TransactionEndedException() { }
        public TransactionEndedException(string message) : base(message) { }
        public TransactionEndedException(string message, Exception inner) : base(message, inner) { }
        private TransactionEndedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
