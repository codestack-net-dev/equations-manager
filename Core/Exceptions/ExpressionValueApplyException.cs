using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xarial.Community.EqMgr.Core.Exceptions
{
    public class ExpressionValueApplyException : Exception
    {
        public ExpressionValueApplyException(string name, string value) : base($"Failed to apply value '{value}' to '{name}'")
        {
        }
    }
}
