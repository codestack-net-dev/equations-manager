using Xarial.Community.EqMgr.Core.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core.Tests
{
    [TestClass]
    public class VariablesProcessorTests
    {
        #region Mock

        [RegExVariable("-var1-")]
        public class Var1 : IVariable
        {
            public string Content { get; private set; }

            public void Load(string content)
            {
                Content = content;
            }
        }

        [RegExVariable("-var2-")]
        public class Var2 : IVariable
        {
            public string Content { get; private set; }

            public void Load(string content)
            {
                Content = content;
            }
        }

        #endregion

        private VariablesProcessor m_Prc;

        private IVariableResolver m_Var1Resolver;
        private IVariableResolver m_Var2Resolver;

        [TestInitialize]
        public void Init()
        {
            m_Var1Resolver = new VariableResolver<Var1>(v => $"_{v.Content}_");
            m_Var2Resolver = new VariableResolver<Var2>(v => $"+{v.Content}+");

            m_Prc = new VariablesProcessor(m_Var1Resolver, m_Var2Resolver);
        }

        [TestMethod]
        public void ResolveExpressionTest()
        {
            var exp1 = "({{-var1-}} + 10) * 2";
            var exp2 = "(50*{{-var1-}} + 10) * 2{{-var1-}}";
            var exp3 = "10 + {{-var2-}} + 20 + {{-var1-}}";

            var res1 = m_Prc.ResolveExpression(exp1);
            var res2 = m_Prc.ResolveExpression(exp2);
            var res3 = m_Prc.ResolveExpression(exp3);

            Assert.AreEqual("(_-var1-_ + 10) * 2", res1);
            Assert.AreEqual("(50*_-var1-_ + 10) * 2_-var1-_", res2);
            Assert.AreEqual("10 + +-var2-+ + 20 + _-var1-_", res3);
        }

        [TestMethod]
        public void GetVariablesTest()
        {
            var exp1 = "({{-var1-}} + 10) * 2";
            var exp2 = "(50*{{-var1-}} + 10) * 2{{-var1-}}";
            var exp3 = "10 + {{-var2-}} + 20 + {{-var1-}}";
            var exp4 = "10 + {{ -var2-}} + 20 + {{-var1-  }} + {{ -var1- }}";
            var exp5 = "10 + {{-var2-}} + 20 + {{not-set}}";

            var v1 = m_Prc.GetVariables(exp1);
            var v2 = m_Prc.GetVariables(exp2);
            var v3 = m_Prc.GetVariables(exp3);
            var v4 = m_Prc.GetVariables(exp4);

            Assert.AreEqual(1, v1.Length);
            Assert.IsTrue(v1.OfType<Var1>().Count() == 1);
            Assert.AreEqual(2, v2.Length);
            Assert.IsTrue(v2.OfType<Var1>().Count() == 2);
            Assert.AreEqual(2, v3.Length);
            Assert.IsTrue(v3.OfType<Var1>().Count() == 1);
            Assert.IsTrue(v3.OfType<Var2>().Count() == 1);
            Assert.AreEqual(3, v4.Length);
            Assert.IsTrue(v4.OfType<Var1>().Count() == 2);
            Assert.IsTrue(v4.OfType<Var2>().Count() == 1);

            Assert.ThrowsException<NullReferenceException>(() => { var v5 = m_Prc.GetVariables(exp5); });
        }
    } 
}
