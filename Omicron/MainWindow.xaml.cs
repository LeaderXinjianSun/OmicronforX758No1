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
using MahApps.Metro.Controls;
using BingLibrary.hjb;
using SxjLibrary;

namespace Omicron
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private dialog mydialog = new dialog();
        public MainWindow()
        {
            InitializeComponent();
            //if (System.Environment.CurrentDirectory != @"C:\Debug")
            //{
            //    System.Windows.MessageBox.Show("软件安装目录必须为C:\\Debug");
            //    System.Windows.Application.Current.Shutdown();
            //}
            //else
            //{
                #region 判断系统是否已启动

                System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcessesByName("Omicron");//获取指定的进程名   
                if (myProcesses.Length > 1) //如果可以获取到知道的进程名则说明已经启动
                {
                    System.Windows.MessageBox.Show("不允许重复打开软件");
                    System.Windows.Application.Current.Shutdown();
                }


                #endregion
            //}

        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            //ActionMessages.ExecuteAction("winclose");
            WindowClose();
        }
        public async void WindowClose()
        {
            try
            {
                mydialog.changeaccent("Red");

                var r = await mydialog.showconfirm("确定要关闭程序吗？");
                if (r)
                {
                    System.Windows.Application.Current.Shutdown();
                }
                else
                {
                    mydialog.changeaccent("Cobalt");
                }
            }
            catch 
            {

               
            }
            
        }
    }
}
