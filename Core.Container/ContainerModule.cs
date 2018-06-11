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
        public virtual IServiceResolver<IExpressionEvaluator> RegisterExpressionEvaluator()
        {
            return ServiceResolver<IExpressionEvaluator, DataTableExpressionEvaluator>.Create();
        }

        public virtual IServiceResolver<IVariablesProcessor> RegisterVariablesProcessor()
        {
            return ServiceResolver<IVariablesProcessor, VariablesProcessor>.Create();
        }

        public abstract IServiceResolver<IValueSetter> RegisterValueSetter();
        public abstract IServiceResolver<IVariablesMonitor> RegisterVariablesMonitor();
        public abstract IServiceResolver<IVariableResolver>[] RegisterVariableResolvers();
    }

    public class BaseContainerModule<TValueSetterImp, TVariablesMonitorImp> : BaseContainerModule
        where TValueSetterImp : class, IValueSetter
        where TVariablesMonitorImp : class, IVariablesMonitor
    {
        public override IServiceResolver<IValueSetter> RegisterValueSetter()
        {
            return ServiceResolver<IValueSetter, TValueSetterImp>.Create();
        }

        public override IServiceResolver<IVariablesMonitor> RegisterVariablesMonitor()
        {
            return ServiceResolver<IVariablesMonitor, TVariablesMonitorImp>.Create();
        }

        public override IServiceResolver<IVariableResolver>[] RegisterVariableResolvers()
        {
            throw new NotImplementedException();
        }

        public BaseContainerModule(params IVariableResolver[] resolvers)
        {
            //TODO: implement assigning of resolvers
        }
    }
}
