using System;
using System.Data;
using Xarial.Community.EqMgr.Core.Exceptions;

namespace Xarial.Community.EqMgr.Core.Services
{
    public class DataTableExpressionEvaluator : IExpressionEvaluator
    {
        private DataTable m_Evaluator;

        public DataTableExpressionEvaluator()
        {
            m_Evaluator = new DataTable();
        }

        public string Evaluate(string expr)
        {
            try
            {
                return m_Evaluator.Compute(expr, "")?.ToString();
            }
            catch (SyntaxErrorException ex)
            {
                throw new InvalidSyntaxException(ex.Message, ex);
            }
            catch (EvaluateException ex)
            {
                throw new EvaluationErrorException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new GenericEvaluatorException(ex.Message, ex);
            }
        }
    }
}
