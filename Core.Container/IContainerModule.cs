using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core.Container
{
    public interface IContainerModule
    {
        IServiceResolver<IExpressionEvaluator> RegisterExpressionEvaluator();
        IServiceResolver<IValueSetter> RegisterValueSetter();
        IServiceResolver<IVariableResolver>[] RegisterVariableResolvers();
        IServiceResolver<IVariablesMonitor> RegisterVariablesMonitor();
        IServiceResolver<IVariablesProcessor> RegisterVariablesProcessor();
    }
}
