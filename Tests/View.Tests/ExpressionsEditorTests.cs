using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xarial.Community.EqMgr.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewTests;

namespace Xarial.Community.EqMgr.View.Tests
{
    [TestClass]
    public class ExpressionsEditorTests
    {
        [TestMethod]
        public void ExpressionsEditorTest()
        {
            var expEditor = new ExpressionEditorWindow();

            //expEditor.Show();
            //expEditor.ShowDialog();
            //expEditor.Close();

            var exp = expEditor.Editor.Expressions;

            Assert.IsNotNull(exp);
        }
    }
}