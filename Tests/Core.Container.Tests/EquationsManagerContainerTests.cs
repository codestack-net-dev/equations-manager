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

        public class ModelMock
        {
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
        public void GetEquationsManagerTest()
        {
            var cont = new EquationsManagerContainer(new ContainerModuleMock());
            
            var m1 = cont.GetEquationsManager();
            var m2 = cont.GetEquationsManager();

            Assert.AreNotEqual(m1, m2);
            Assert.AreEqual(m1.Evaluator, m2.Evaluator);
            Assert.AreEqual(m1.VarsProcessor, m2.VarsProcessor);
            Assert.AreNotEqual(m1.ValSetter, m2.ValSetter);
            Assert.AreNotEqual(m1.VarsMonitor, m2.VarsMonitor);
        }

        [TestMethod]
        public void GetEquationsManagerWithModelTest()
        {
            var cont = new EquationsManagerContainer(new ContainerModuleMock());

            var m1 = cont.GetEquationsManager<ModelMock>(new ModelMock());
            var m2 = cont.GetEquationsManager<ModelMock>(new ModelMock());

            Assert.AreNotEqual(m1, m2);
            Assert.AreEqual(m1.Evaluator, m2.Evaluator);
            Assert.AreEqual(m1.VarsProcessor, m2.VarsProcessor);
            Assert.AreNotEqual(m1.ValSetter, m2.ValSetter);
            Assert.AreNotEqual(m1.VarsMonitor, m2.VarsMonitor);
        }
    }
}
