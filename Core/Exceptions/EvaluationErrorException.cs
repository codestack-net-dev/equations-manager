using System;

namespace Xarial.Community.EqMgr.Core.Exceptions
{
    public class EvaluationErrorException : Exception
    {
        public EvaluationErrorException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }
}
