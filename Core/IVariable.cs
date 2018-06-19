namespace Xarial.Community.EqMgr.Core
{
    /// <summary>
    /// Represents the variable which can be used in <see cref="Expression"/> and whose value might change
    /// </summary>
    /// <remarks>Variables are compared by pointers.
    /// It is required to cache variables or overwrite <see cref="object.Equals(object)"/> and == operator
    /// to properly represent variables when returned from <see cref="Services.IVariablesProcessor.GetVariables(string)"/> method</remarks>
    public interface IVariable
    {
        void Load(string content);
    }
}
