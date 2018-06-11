using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Registration;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core.Container
{
    public static class EquationsManagerContainer
    {
        private static UnityContainer m_Container;

        public static void Init(IContainerModule module)
        {
            m_Container = new UnityContainer();

            foreach (var varResolverType in module.RegisterVariableResolvers())
            {
                var specType = varResolverType.GetType();

                m_Container.RegisterType(
                    typeof(IVariableResolver),
                    varResolverType.GetType(),
                    specType.FullName,
                    new ContainerControlledLifetimeManager(),
                    null);
            }

            m_Container.RegisterType(
                typeof(IExpressionEvaluator),
                module.RegisterExpressionEvaluator().GetType(),
                new ContainerControlledLifetimeManager());

            m_Container.RegisterType(
                typeof(IValueSetter),
                module.RegisterValueSetter().GetType(),
                new ContainerControlledLifetimeManager());

            m_Container.RegisterType(
                typeof(IVariablesMonitor),
                module.RegisterVariablesMonitor().GetType(),
                new ContainerControlledLifetimeManager());

            m_Container.RegisterType(
                typeof(IVariablesProcessor),
                module.RegisterVariablesProcessor().GetType(),
                new ContainerControlledLifetimeManager());

            m_Container.RegisterType<ExpressionEvaluatorController>();
        }

        /// <summary>
        /// Returns the controller instance per model
        /// </summary>
        public static ExpressionEvaluatorController CreateContoller()
        {
            return m_Container.Resolve<ExpressionEvaluatorController>();
        }
    }
}
