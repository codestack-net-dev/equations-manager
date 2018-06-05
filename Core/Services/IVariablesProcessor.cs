namespace Xarial.Community.EqMgr.Core.Services
{
    public interface IVariablesProcessor
    {
        IVariable[] GetVariables(string expression);
        string ResolveExpression(string expression);
    }
}
