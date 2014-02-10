using System;
using System.Runtime.Serialization;

namespace Hacapp.Core.Commands
{
    public class CommandExecutionException : Exception
    {
        public CommandExecutionException(string message) : base(message)
        {
        }

        public CommandExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}