using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xarial.Community.EqMgr.Core;

namespace Xarial.Community.EqMgr.View
{
    public class ExpressionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal Expression Expression { get; private set; }

        private string m_EvaluatedValue;

        //required for the view to create new rows with expression
        public ExpressionVM() : this(new Expression())
        {
        }

        internal ExpressionVM(Expression exp)
        {
            Expression = exp;
        }

        public string Name
        {
            get
            {
                return Expression.Name;
            }
            set
            {
                Expression.Name = value;
                this.NotifyChanged();
            }
        }

        public string Formula
        {
            get
            {
                return Expression.Formula;
            }
            set
            {
                Expression.Formula = value;
                this.NotifyChanged();
            }
        }

        public string EvaluatedValue
        {
            get
            {
                return m_EvaluatedValue;
            }
            set
            {
                m_EvaluatedValue = value;
                this.NotifyChanged();
            }
        }

        private void NotifyChanged([CallerMemberName]string prpName = "")
        {
            if (string.IsNullOrEmpty(prpName))
            {
                throw new NullReferenceException("Property name is not specified");
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prpName));
        }
    }
}
