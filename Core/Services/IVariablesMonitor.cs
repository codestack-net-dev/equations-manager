using System;

namespace Xarial.Community.EqMgr.Core.Services
{
    public delegate void VariableChangedDelegate(IVariable variable);

    public interface IVariablesMonitor : IDisposable
    {
        event VariableChangedDelegate VariableChanged;
    }
}
