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
    /// SampleTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SampleTestWindow 
    {
        public SampleTestWindow()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty QuitSampleTestProperty =
    DependencyProperty.Register("QuitSampleTest", typeof(bool), typeof(SampleTestWindow), new PropertyMetadata(
        new PropertyChangedCallback((d, e) =>
        {
            var mSampleTestWindow = d as SampleTestWindow;
            if (mSampleTestWindow.HasShow)
            {
                mSampleTestWindow.HasShow = false;
                mSampleTestWindow.Close();
                mSampleTestWindow = null;
            }
        })));
        public bool QuitSampleTest
        {
            get { return (bool)GetValue(QuitSampleTestProperty); }
            set { SetValue(QuitSampleTestProperty, value); }
        }
        public bool HasShow { get; set; }
        protected override void OnClosed(EventArgs e)
        {
            HasShow = false;
            base.OnClosed(e);
        }

    }
}
