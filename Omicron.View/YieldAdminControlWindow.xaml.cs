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

namespace Omicron.View
{
    /// <summary>
    /// YieldAdminControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class YieldAdminControlWindow
    {
        public YieldAdminControlWindow()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty QuitYieldAdminControlProperty =
            DependencyProperty.Register("QuitYieldAdminControl", typeof(bool), typeof(YieldAdminControlWindow), new PropertyMetadata(
            new PropertyChangedCallback((d, e) =>
            {
                var mYieldAdminControlWindow = d as YieldAdminControlWindow;
                if (mYieldAdminControlWindow.HasShow)
                {
                    mYieldAdminControlWindow.HasShow = false;
                    mYieldAdminControlWindow.Close();
                    mYieldAdminControlWindow = null;
                }
            })));
        public bool QuitYieldAdminControl
        {
            get { return (bool)GetValue(QuitYieldAdminControlProperty); }
            set { SetValue(QuitYieldAdminControlProperty, value); }
        }
        public bool HasShow { get; set; }
        protected override void OnClosed(EventArgs e)
        {
            HasShow = false;
            base.OnClosed(e);
        }
    }
}
