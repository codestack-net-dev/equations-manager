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
        void RegisterExpressionEvaluator(ServiceResolver<IExpressionEvaluator> resolver);
        void RegisterValueSetter(ServiceResolver<IValueSetter> resolver);
        void RegisterVariableResolver(ServiceResolver<IVariableResolver> resolver);
        void RegisterVariablesMonitor(ServiceResolver<IVariablesMonitor> resolver);
        void RegisterVariablesProcessor(ServiceResolver<IVariablesProcessor> resolver);
    }
}
