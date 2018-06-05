using System;

namespace Xarial.Community.EqMgr.Core.Exceptions
{
    public class GenericEvaluatorException : Exception
    {
        public GenericEvaluatorException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }
}
