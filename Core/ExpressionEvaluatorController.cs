using System;
using System.Collections.Generic;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core
{
    public interface IExpressionEvaluatorController
    {
        void Load(ExpressionCollection expressions, IVariablesMonitor varsMonitor);
    }

    public class ExpressionEvaluatorController : IExpressionEvaluatorController
    {
        private IVariablesProcessor m_VarsProcessor;
        private IExpressionEvaluator m_Evaluator;

        private Dictionary<IVariable, List<Expression>> m_ExpDeps;
        private IVariablesMonitor m_VarsMonitor;
        private IValueSetter m_ValSetter;

        public ExpressionEvaluatorController(IVariablesProcessor varsProcessor, IExpressionEvaluator evaluator, IValueSetter valSetter)
        {
            m_VarsProcessor = varsProcessor;
            m_Evaluator = evaluator;
            m_ValSetter = valSetter;
        }

        public void Load(ExpressionCollection expressions, IVariablesMonitor varsMonitor)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }

            if (varsMonitor == null)
            {
                throw new ArgumentNullException(nameof(varsMonitor));
            }

            if (m_VarsMonitor != null)
            {
                m_VarsMonitor.VariableChanged -= OnVariableChanged;
            }

            m_VarsMonitor = varsMonitor;
            m_VarsMonitor.VariableChanged += OnVariableChanged;

            m_ExpDeps = new Dictionary<IVariable, List<Expression>>();
            IndexVariables(expressions);
        }

        private void IndexVariables(ExpressionCollection expressions)
        {
            foreach (var exp in expressions)
            {
                var vars = m_VarsProcessor.GetVariables(exp.Formula);

                foreach (var var in vars)
                {
                    List<Expression> expList;

                    if (!m_ExpDeps.TryGetValue(var, out expList))
                    {
                        expList = new List<Expression>();
                        m_ExpDeps.Add(var, expList);
                    }

                    expList.Add(exp);
                }
            }
        }

        private void OnVariableChanged(IVariable variable)
        {
            List<Expression> exps;

            if (m_ExpDeps.TryGetValue(variable, out exps))
            {
                foreach (var exp in exps)
                {
                    var resForm = m_VarsProcessor.ResolveExpression(exp.Formula);
                    var evaluatedValue = m_Evaluator.Evaluate(resForm);

                    if (!m_ValSetter.SetValue(exp.Name, evaluatedValue))
                    {
                        //TODO: throw exception
                    }
                }
            }
        }
    }
}
