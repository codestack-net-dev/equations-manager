using System;
using System.Text.RegularExpressions;

namespace Xarial.Community.EqMgr.Core.Services
{
    public class VariablesProcessor : IVariablesProcessor
    {
        private const string VARIABLE_REG_EX = "{{ *(.+?) *}}";

        private IVariableResolver[] m_Resolvers;

        public VariablesProcessor(params IVariableResolver[] resolvers)
        {
            m_Resolvers = resolvers;
        }

        public string ResolveExpression(string expression)
        {
            expression = Regex.Replace(expression,
                        VARIABLE_REG_EX, m =>
                        {
                            IVariableResolver resolver;
                            var var = CreateVariableFromMatch(m, out resolver);
                            ValidateVariable(expression, m, var);

                            return resolver.Resolve(var);
                        });

            return expression;
        }

        public IVariable[] GetVariables(string expression)
        {
            var matches = Regex.Matches(expression, VARIABLE_REG_EX);

            var vars = new IVariable[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                IVariableResolver resolver;
                var var = CreateVariableFromMatch(match, out resolver);

                ValidateVariable(expression, match, var);

                vars[i] = var;
            }

            return vars;
        }

        private void ValidateVariable(string expression, Match match, IVariable var)
        {
            if (var == null)
            {
                throw new NullReferenceException($"Failed to create variable from the match '{match.Value}' in '{expression}'");
            }
        }

        private IVariable CreateVariableFromMatch(Match match, out IVariableResolver resolver)
        {
            if (match.Groups.Count != 2)
            {
                throw new InvalidOperationException($"Regular expression failed");
            }

            var content = match.Groups[1].Value;

            foreach (var res in m_Resolvers)
            {
                IVariable var;
                if (res.TryCreate(content, out var))
                {
                    resolver = res;
                    return var;
                }
            }

            resolver = null;
            return null;
        }
    }
}
