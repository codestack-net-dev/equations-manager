using Xarial.Community.EqMgr.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ViewTests
{
    /// <summary>
    /// Interaction logic for ExpressionEditorWindow.xaml
    /// </summary>
    public partial class ExpressionEditorWindow : Window
    {
        public ExpressionEditorWindow()
        {
            InitializeComponent();
        }

        public ExpressionsEditor Editor
        {
            get
            {
                return editor;
            }
        }
    }
}
