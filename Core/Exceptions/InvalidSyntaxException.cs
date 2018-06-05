using System;

namespace Xarial.Community.EqMgr.Core.Exceptions
{
    public class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }
}
