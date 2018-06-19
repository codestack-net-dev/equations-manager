using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Registration;
using Xarial.Community.EqMgr.Core.Container.Exceptions;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core.Container
{
    public class EquationsManagerContainer
    {
        private UnityContainer m_Container;

        public EquationsManagerContainer(IContainerModule module)
        {
            m_Container = new UnityContainer();

            RegisterService<IVariableResolver>(r => module.RegisterVariableResolver(r));
            RegisterService<IExpressionEvaluator>(r => module.RegisterExpressionEvaluator(r));
            RegisterService<IValueSetter>(r => module.RegisterValueSetter(r));
            RegisterService<IVariablesMonitor>(r => module.RegisterVariablesMonitor(r));
            RegisterService<IVariablesProcessor>(r => module.RegisterVariablesProcessor(r));

            m_Container.RegisterType<EquationsManager>();
        }

        public EquationsManager GetEquationsManager()
        {
            var childCont = m_Container.CreateChildContainer();
            return childCont.Resolve<EquationsManager>();
        }

        public EquationsManager<TModel> GetEquationsManager<TModel>(TModel model)
        {
            var childCont = m_Container.CreateChildContainer();
            childCont.RegisterInstance(model);

            return childCont.Resolve<EquationsManager<TModel>>();
        }

        private void RegisterService<TService>(Action<ServiceResolver<TService>> action)
        {
            var resolver = new ServiceResolver<TService>(m_Container);

            action.Invoke(resolver);

            var regs = m_Container.Registrations.Where(r => r.RegisteredType == typeof(TService));

            if (!regs.Any())
            {
                throw new ServiceNotRegisteredException(typeof(TService));
            }
        }
    }
}
