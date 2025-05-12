using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrastructure.Exceptions
{
    public class DatabaseValidateException : Exception
    {
        public DatabaseValidateException()
        {
        }

        public DatabaseValidateException(string? message) : base(message)
        {
        }

        public DatabaseValidateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DatabaseValidateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
