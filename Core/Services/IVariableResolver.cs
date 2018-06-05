using System;

namespace Xarial.Community.EqMgr.Core.Services
{
    public interface IVariableResolver
    {
        Type VariableType { get; }
        string Resolve(IVariable var);
        bool TryCreate(string content, out IVariable var);
    }
}
