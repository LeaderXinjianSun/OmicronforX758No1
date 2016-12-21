using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.IO;
using System.Reflection;

namespace BingLibrary.hjb
{
   public class SingletonApplication:Application
    {
        Mutex mutex;
        string appName = Path.GetFileName(Assembly.GetEntryAssembly().GetName().Name);

        public SingletonApplication()
        {
             mutex = new Mutex(false, appName);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (!mutex.WaitOne(0))
            {
                MessageBox.Show("已有一个实例在运行！");
                Environment.Exit(0);
            }

        }
        
    }
}
