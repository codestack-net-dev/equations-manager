using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xarial.Community.EqMgr.Core.Attributes;

namespace Xarial.Community.EqMgr.Core.Services
{
    public class VariableResolver<TVar> : IVariableResolver
            where TVar : class, IVariable, new()
    {
        private Func<TVar, string> m_Resolver;

        public Type VariableType => typeof(TVar);

        public VariableResolver(Func<TVar, string> resolver)
        {
            m_Resolver = resolver;
        }

        public string Resolve(TVar var)
        {
            return m_Resolver.Invoke(var);
        }

        string IVariableResolver.Resolve(IVariable var)
        {
            return Resolve(var as TVar);
        }

        public bool TryCreate(string content, out IVariable var)
        {
            var = null;

            var regexAtt = VariableType.GetCustomAttributes(
                typeof(RegExVariableAttribute), true)?.FirstOrDefault() as RegExVariableAttribute;

            if (regexAtt != null)
            {
                if (Regex.IsMatch(content, regexAtt.RegexPattern))
                {
                    var = new TVar();
                    var.Load(content);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new NotSupportedException($"Variable type {VariableType.FullName} must be attributed with {typeof(RegExVariableAttribute).FullName}. Other types are not supported yet");
            }
        }
    }
}
