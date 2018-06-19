namespace Xarial.Community.EqMgr.Core.Services
{
    /// <summary>
    /// Service evaluating the text expression into the final value
    /// </summary>
    /// <remarks>The expression has variables already resolved</remarks>
    /// <example>Expression (2+3)*5 should return 25</example>
    public interface IExpressionEvaluator
    {
        string Evaluate(string expr);
    }
}
