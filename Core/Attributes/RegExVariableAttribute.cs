using System;

namespace Xarial.Community.EqMgr.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RegExVariableAttribute : Attribute
    {
        public string RegexPattern { get; private set; }

        public RegExVariableAttribute(string regexPattern)
        {
            RegexPattern = regexPattern;
        }
    }
}
