using Xarial.Community.EqMgr.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xarial.Community.EqMgr.Core.Services;

namespace Xarial.Community.EqMgr.Core.Tests
{
    [TestClass]
    public class DataTableExpressionEvaluatorTests
    {
        private DataTableExpressionEvaluator m_Eval;

        [TestInitialize]
        public void Init()
        {
            m_Eval = new DataTableExpressionEvaluator();
        }

        [TestMethod]
        public void FormulasEvaluation()
        {
            var r1 = m_Eval.Evaluate("1+1");
            var r2 = m_Eval.Evaluate("(1 + 1) / 4");
            var r3 = m_Eval.Evaluate("10*3");
            var r4 = m_Eval.Evaluate("25%9");
            var r5 = m_Eval.Evaluate("10.5-6.4");

            Assert.AreEqual("2", r1);
            Assert.AreEqual("0.5", r2);
            Assert.AreEqual("30", r3);
            Assert.AreEqual("7", r4);
            Assert.AreEqual("4.1", r5);

            Assert.ThrowsException<InvalidSyntaxException>(() => m_Eval.Evaluate("10k1"));
        }

        [TestMethod]
        public void LogicalEvaluation()
        {
            var r1 = m_Eval.Evaluate("IIF(10>2,'A','B')");
            var r2 = m_Eval.Evaluate("'ABCD' LIKE 'AB*'");
            var r3 = m_Eval.Evaluate("'ABC'<>'ABC'");
            var r4 = m_Eval.Evaluate("10>=10");
            var r5 = m_Eval.Evaluate("10 in (2,3,4,5,6,10,11)");
            var r6 = m_Eval.Evaluate("5=5 OR 'A'='B' AND (10+5)>1");

            Assert.AreEqual("A", r1);
            Assert.AreEqual("True", r2);
            Assert.AreEqual("False", r3);
            Assert.AreEqual("True", r4);
            Assert.AreEqual("True", r5);
            Assert.AreEqual("True", r6);

            Assert.ThrowsException<InvalidSyntaxException>(() => m_Eval.Evaluate("'A'='B"));
            Assert.ThrowsException<EvaluationErrorException>(() => m_Eval.Evaluate("A=1"));
        }

        [TestMethod]
        public void StringOperationsEvaluation()
        {
            var r1 = m_Eval.Evaluate("substring('abcdef',2,2)");
            var r2 = m_Eval.Evaluate("'A' + 'B' + 'C'");
            var r3 = m_Eval.Evaluate("TRIM('  ABC ')");
            var r4 = m_Eval.Evaluate("LEN('ABC')");

            Assert.AreEqual("bc", r1);
            Assert.AreEqual("ABC", r2);
            Assert.AreEqual("ABC", r3);
            Assert.AreEqual("3", r4);
        }
    }
}
