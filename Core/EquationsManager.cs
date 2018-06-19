using System;
using System.Collections.Generic;
using Xarial.Community.EqMgr.Core.Exceptions;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core
{
    /// <summary>
    /// Interface responsible for managing and applying the equations
    /// </summary>
    /// <remarks>This class will handle the variable changes and update the corresponding expression values</remarks>
    public interface IEquationsManager
    {
        void Load(ExpressionCollection expressions);
    }

    /// <summary>
    /// Controlling the equation manager for one entity (i.e. document) with an ability to inject the pointer to the entity
    /// </summary>
    /// <typeparam name="TModel">Injectable entity model</typeparam>
    public class EquationsManager<TModel> : EquationsManager
    {
        public TModel Model { get; private set; }

        public EquationsManager(IVariablesProcessor varsProcessor, IExpressionEvaluator evaluator,
            IValueSetter valSetter, IVariablesMonitor varsMonitor, TModel model)
            : base(varsProcessor, evaluator, valSetter, varsMonitor)
        {
            Model = model;
        }
    }

    /// <summary>
    /// Controlling the equation service for one entity (i.e. document)
    /// </summary>
    public class EquationsManager : IEquationsManager
    {
        private readonly IVariablesProcessor m_VarsProcessor;
        private readonly IExpressionEvaluator m_Evaluator;
        private readonly IVariablesMonitor m_VarsMonitor;
        private readonly IValueSetter m_ValSetter;

        private Dictionary<IVariable, List<Expression>> m_ExpDeps;

        public EquationsManager(
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

        public IVariablesProcessor VarsProcessor
        {
            get
            {
                return m_VarsProcessor;
            }
        }

        public IExpressionEvaluator Evaluator
        {
            get
            {
                return m_Evaluator;
            }
        }

        public IVariablesMonitor VarsMonitor
        {
            get
            {
                return m_VarsMonitor;
            }
        }

        public IValueSetter ValSetter
        {
            get
            {
                return m_ValSetter;
            }
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
