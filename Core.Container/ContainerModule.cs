using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core.Container
{
    public abstract class BaseContainerModule : IContainerModule
    {
        public virtual void RegisterExpressionEvaluator(ServiceResolver<IExpressionEvaluator> resolver)
        {
            resolver.RegisterSingleton<DataTableExpressionEvaluator>();
        }

        public virtual void RegisterVariablesProcessor(ServiceResolver<IVariablesProcessor> resolver)
        {
            resolver.RegisterSingleton<VariablesProcessor>();
        }

        public abstract void RegisterVariableResolver(ServiceResolver<IVariableResolver> resolver);
        public abstract void RegisterValueSetter(ServiceResolver<IValueSetter> resolver);
        public abstract void RegisterVariablesMonitor(ServiceResolver<IVariablesMonitor> resolver);
    }
}
