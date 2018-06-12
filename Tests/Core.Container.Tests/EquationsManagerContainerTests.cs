using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xarial.Community.EqMgr.Core;
using Xarial.Community.EqMgr.Core.Container;
using Xarial.Community.EqMgr.Core.Services;

namespace Core.Container.Tests
{
    [TestClass]
    public class EquationsManagerContainerTests
    {
        #region Mocks

        public class ValueSetterMock1 : IValueSetter
        {
            public bool SetValue(string name, string value)
            {
                throw new NotImplementedException();
            }
        }

        public class VariableResolverMock1 : IVariableResolver
        {
            public Type VariableType => throw new NotImplementedException();

            public string Resolve(IVariable var)
            {
                throw new NotImplementedException();
            }

            public bool TryCreate(string content, out IVariable var)
            {
                throw new NotImplementedException();
            }
        }

        public class VariableResolverMock2 : IVariableResolver
        {
            public Type VariableType => throw new NotImplementedException();

            public string Resolve(IVariable var)
            {
                throw new NotImplementedException();
            }

            public bool TryCreate(string content, out IVariable var)
            {
                throw new NotImplementedException();
            }
        }

        public class VariablesMonitorMock : IVariablesMonitor
        {
            public event VariableChangedDelegate VariableChanged;

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        public class ContainerModuleMock : BaseContainerModule
        {
            public override void RegisterValueSetter(ServiceResolver<IValueSetter> resolver)
            {
                resolver.RegisterType<ValueSetterMock1>();
            }

            public override void RegisterVariableResolver(ServiceResolver<IVariableResolver> resolver)
            {
                resolver.RegisterMultipleType<VariableResolverMock1>();
            }

            public override void RegisterVariablesMonitor(ServiceResolver<IVariablesMonitor> resolver)
            {
                resolver.RegisterType<VariablesMonitorMock>();
            }
        }

        #endregion

        [TestMethod]
        public void TestLifecycle()
        {
            var cont = new EquationsManagerContainer(new ContainerModuleMock());
            
            var mgr = cont.GetEquationsManager();
        }
    }
}
