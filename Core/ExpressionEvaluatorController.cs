using System;
using System.Collections.Generic;
using Xarial.Community.EqMgr.Core.Exceptions;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core
{
    public interface IExpressionEvaluatorController
    {
        void Load(ExpressionCollection expressions);
    }

    public class ExpressionEvaluatorController<TModel> : ExpressionEvaluatorController
    {
        public TModel Model { get; private set; }

        public ExpressionEvaluatorController(IVariablesProcessor varsProcessor, IExpressionEvaluator evaluator,
            IValueSetter valSetter, IVariablesMonitor varsMonitor, TModel model) 
            : base(varsProcessor, evaluator, valSetter, varsMonitor)
        {
            Model = model;
        }
    }

    public class ExpressionEvaluatorController : IExpressionEvaluatorController
    {
        private readonly IVariablesProcessor m_VarsProcessor;
        private readonly IExpressionEvaluator m_Evaluator;
        private readonly IVariablesMonitor m_VarsMonitor;
        private readonly IValueSetter m_ValSetter;

        private Dictionary<IVariable, List<Expression>> m_ExpDeps;

        public ExpressionEvaluatorController(
            IVariablesProcessor varsProcessor, IExpressionEvaluator evaluator, IValueSetter valSetter,
            IVariablesMonitor varsMonitor)
        {
            m_VarsProcessor = varsProcessor;
            m_Evaluator = evaluator;
            m_ValSetter = valSetter;
            m_VarsMonitor = varsMonitor;

            m_VarsMonitor.VariableChanged += OnVariableChanged;
        }

        public void Load(ExpressionCollection expressions)
        {
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
                        throw new ExpressionValueApplyException(exp.Name, evaluatedValue);
                    }
                }
            }
        }
    }
}
