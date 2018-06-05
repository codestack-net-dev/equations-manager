using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xarial.Community.EqMgr.Core;

namespace Xarial.Community.EqMgr.View
{
    public class ExpressionsEditor : DataGrid
    {
        private class ColumnNames
        {
            internal const string NameColumn = "NameColumn";
            internal const string FormulaColumn = "FormulaColumn";
            internal const string EvaluatedValueColumn = "EvaluatedValueColumn";
        }

        private ObservableCollection<ExpressionVM> m_ExpressionsViewModel;

        static ExpressionsEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExpressionsEditor), 
                new FrameworkPropertyMetadata(typeof(ExpressionsEditor)));
        }

        public ExpressionsEditor()
        {
            this.Columns.Add(FindResource<DataGridColumn>(ColumnNames.NameColumn));
            this.Columns.Add(FindResource<DataGridColumn>(ColumnNames.FormulaColumn));
            this.Columns.Add(FindResource<DataGridColumn>(ColumnNames.EvaluatedValueColumn));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SetSource();
        }

        private T FindResource<T>(string resName)
            where T : class
        {
            var resDic = new ResourceDictionary();
            resDic.Source =
                new Uri($"/{typeof(ExpressionsEditor).Assembly.GetName().Name};component/Themes/Generic.xaml",
                        UriKind.RelativeOrAbsolute);

            return resDic[resName] as T;
        }
        
        public ExpressionCollection Expressions
        {
            get { return (ExpressionCollection)GetValue(ExpressionsProperty); }
            set { SetValue(ExpressionsProperty, value); }
        }

        public static readonly DependencyProperty ExpressionsProperty =
            DependencyProperty.Register(nameof(Expressions), typeof(ExpressionCollection),
                typeof(ExpressionsEditor),
                new PropertyMetadata(new ExpressionCollection(), OnExpressionPropertyChanged));

        private static void OnExpressionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ExpressionsEditor).SetSource();
        }

        private void SetSource()
        {
            if (m_ExpressionsViewModel != null)
            {
                m_ExpressionsViewModel.CollectionChanged -= OnExpressionsCollectionChanged;
            }

            m_ExpressionsViewModel = new ObservableCollection<ExpressionVM>(
                Expressions.Select(e => new ExpressionVM(e)));

            m_ExpressionsViewModel.CollectionChanged += OnExpressionsCollectionChanged;

            this.ItemsSource = m_ExpressionsViewModel;
        }

        private void OnExpressionsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach(ExpressionVM exp in e.NewItems)
                {
                    if (!Expressions.Contains(exp.Expression))
                    {
                        Expressions.Add(exp.Expression);
                    }
                    else
                    {
                        Debug.Assert(false, "Error in sync");
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (ExpressionVM exp in e.OldItems)
                {
                    if (Expressions.Contains(exp.Expression))
                    {
                        Expressions.Remove(exp.Expression);
                    }
                    else
                    {
                        Debug.Assert(false, "Error in sync");
                    }
                }
            }
        }
    }
}
