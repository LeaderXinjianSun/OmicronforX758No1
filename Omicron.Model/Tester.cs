using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SxjLibrary;
using System.IO;
using BingLibrary.hjb;

namespace Omicron.Model
{
    public enum TestStatus
    {
        Testing, PreTest, Tested, Err
    }
    public enum TestResult
    {
        Unknow, Pass, Ng, TimeOut

    }
    public class Tester
    {
        #region 属性定义
        public string TestPcIP { set; get; } = "192.168.1.100";
        public int TestPcRemotePort { set; get; } = 8000;
        public string TesterBracode { set; get; } = "Null";
        public int Index { set; get; }
        public int PassCount { set; get; }
        public int FailCount { set; get; }
        public int TestCount { set; get; }
        #region Mac命令
        public string StartStr { set; get; }
        public string BarcodeStr { set; get; }
        public string PassCountStr { set; get; }
        public string FailCountStr { set; get; }
        public string AppPathStr { set; get; }
        #endregion
        public Udp udp;
        #endregion


        public Tester(string ip,int port,int index)
        {
            TestPcIP = ip;
            TestPcRemotePort = port;
            Index = index;
            udp = new Udp(TestPcIP, TestPcRemotePort, 12000 + Index, 5000);
            StreamReader srd;
            try
            {
                srd = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\FlexTestConfig.ini", Encoding.Default, true);
            }
            catch { return; }
            for (int i = 0; i < Index % 2 * 5; i++)
            {
                string str = srd.ReadLine();
            }
            BarcodeStr = srd.ReadLine();
            StartStr = srd.ReadLine();
            PassCountStr = srd.ReadLine();
            FailCountStr = srd.ReadLine();
            AppPathStr = srd.ReadLine();
        }
    }
}
