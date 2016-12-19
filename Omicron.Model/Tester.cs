using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SxjLibrary;
using System.IO;
using BingLibrary.hjb;
using System.Diagnostics;

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
        public static bool IsInSampleMode { set; get; } = false;
        #region 属性定义
        public string TestPcIP { set; get; } = "192.168.1.100";
        public int TestPcRemotePort { set; get; } = 8000;
        public string TesterBracode { set; get; } = "Null";
        public int TestTimeout { set; get; } 
        public int Index { set; get; } 
        public int PassCount { set; get; }
        public int FailCount { set; get; }
        public int TestCount { set; get; }
        public double Yield { set; get; }

        public double TestSpan { set; get; } = 0;
        public TestResult testResult { set; get; } = TestResult.Unknow;
        public TestStatus testStatus { set; get; } = TestStatus.PreTest;
        #region Mac命令
        public string StartStr { set; get; }
        public string BarcodeStr { set; get; }
        public string PassCountStr { set; get; }
        public string FailCountStr { set; get; }
        public string AppPathStr { set; get; }
        #endregion
        public Udp udp;
        private string iniTesterResutPath = System.Environment.CurrentDirectory + "\\TesterResut.ini";
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
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

            TestTimeout = 100000;

            try
            {
                TestSpan = double.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + Index.ToString(), "TestSpan", "0"));
                PassCount = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + Index.ToString(), "PassCount", "0"));
                FailCount = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + Index.ToString(), "FailCount", "0"));
                TestCount = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + Index.ToString(), "TestCount", "0"));
                Yield = float.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + Index.ToString(), "Yield", "0"));
                string str;
                switch (Index)
                {
                    case 0:
                        str = "TesterBracodeAL";
                        break;
                    case 1:
                        str = "TesterBracodeAR";
                        break;
                    case 2:
                        str = "TesterBracodeBL";
                        break;
                    case 3:
                        str = "TesterBracodeBR";
                        break;
                    default:
                        str = "";
                        break;
                }
                //TesterBracode = Inifile.INIGetStringValue(iniParameterPath, "Barcode", str, "Null");
                TesterBracode = "Z6BS000UHP192ZM3";
            }
            catch
            {

            }
        }
        public delegate void StartProcessedDelegate(int index);
        public async void Start(StartProcessedDelegate callback)
        {
            Stopwatch sw = new Stopwatch();
            int mResult = -2;
            int prePassCount = 0;
            int preFailCount = 0;
            string s;
            string[] ss;
            bool isRestarted = false;
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    try
                    {
                        //s = udp.UdpSendthenReceive(CHKUploadStr);
                        //ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //await Task.Delay(100);
                        //if (ss[0] != "停止上传")
                        //{
                        //    s = udp.UdpSendthenReceive(CHKUploadStr);
                        //    ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //    await Task.Delay(100);
                        //    if (ss[0] != "停止上传")
                        //    {
                        //        TesterNoticEventArgs te = new TesterNoticEventArgs(Index);
                        //        OnTesterNotic(te);
                        //    }
                        //}
                        sw.Start();
                        testResult = TestResult.Unknow;
                        testStatus = TestStatus.Testing;
                        //await Task.Delay(20);
                        //mResult = 1;
                        //读当前PASS
                        s = udp.UdpSendthenReceive(PassCountStr);
                        ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        await Task.Delay(100);
                        while (ss[0] == "nil" || ss[0] == "Udp 发送或接收错误")
                        {
                            TestSpan = Math.Round(sw.Elapsed.TotalSeconds,2);
                            if (mResult == 2)
                            {
                                return;
                            }

                            s = udp.UdpSendthenReceive(PassCountStr);
                            ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            await Task.Delay(100);
                        }

                        try
                        {
                            prePassCount = PassCount = int.Parse(ss[0]);
                        }
                        catch (Exception e)
                        {
                            Log.Default.Error("Pass转码错误", e.Message);
                        }
                        //读当前FAIL
                        s = udp.UdpSendthenReceive(FailCountStr);
                        ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        await Task.Delay(100);
                        while (ss[0] == "nil" || ss[0] == "Udp 发送或接收错误")
                        {
                            TestSpan = Math.Round(sw.Elapsed.TotalSeconds, 2);
                            if (mResult == 2)
                            {
                                return;
                            }

                            s = udp.UdpSendthenReceive(FailCountStr);
                            ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            await Task.Delay(100);
                        }

                        try
                        {
                            preFailCount = FailCount = int.Parse(ss[0]);
                        }
                        catch (Exception e)
                        {
                            Log.Default.Error("Fail转码错误", e.Message);
                        }
                        //写条码
                        while (udp.UdpSendthenReceive("setvalue:(AXValue:" + TesterBracode + ")" + BarcodeStr) != "SetValue Success")
                        {
                            TestSpan = Math.Round(sw.Elapsed.TotalSeconds, 2);
                            if (mResult == 2)
                            {
                                return;
                            }
                            await Task.Delay(200);
                        }
                        await Task.Delay(50);
                        //按开始按钮
                        while (udp.UdpSendthenReceive(StartStr) != "Action Success")
                        {
                            TestSpan = Math.Round(sw.Elapsed.TotalSeconds, 2);
                            if (mResult == 2)
                            {
                                return;
                            }
                            await Task.Delay(200);
                        }
                        await Task.Delay(200);
                        while (!(PassCount == prePassCount + 1 || FailCount == preFailCount + 1))
                        {
                            if (mResult == 2)
                            {
                                return;
                            }
                            //读PASS
                            s = udp.UdpSendthenReceive(PassCountStr);
                            ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            await Task.Delay(100);
                            while (ss[0] == "nil" || ss[0] == "Udp 发送或接收错误")
                            {
                                TestSpan = Math.Round(sw.Elapsed.TotalSeconds, 2);
                                if (mResult == 2)
                                {
                                    return;
                                }

                                s = udp.UdpSendthenReceive(PassCountStr);
                                ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                await Task.Delay(100);
                            }

                            try
                            {
                                PassCount = int.Parse(ss[0]);
                            }
                            catch (Exception e)
                            {
                                Log.Default.Error("Pass转码错误", e.Message);
                            }

                            //if (PassCount < prePassCount && PassCount == 0)
                            //{
                            //    prePassCount = PassCount;
                            //}

                            //读FAIL
                            s = udp.UdpSendthenReceive(FailCountStr);
                            ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            await Task.Delay(100);
                            while (ss[0] == "nil" || ss[0] == "Udp 发送或接收错误")
                            {
                                TestSpan = Math.Round(sw.Elapsed.TotalSeconds, 2);
                                if (mResult == 2)
                                {
                                    return;
                                }

                                s = udp.UdpSendthenReceive(FailCountStr);
                                ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                await Task.Delay(100);
                            }

                            try
                            {
                                FailCount = int.Parse(ss[0]);
                            }
                            catch (Exception e)
                            {
                                Log.Default.Error("Fail转码错误", e.Message);
                            }



                            //if (FailCount < preFailCount && FailCount == 0)
                            //{
                            //    preFailCount = FailCount;
                            //}
                            TestSpan = Math.Round(sw.Elapsed.TotalSeconds, 2);

                        }
                        if (PassCount == prePassCount + 1)
                        {
                            mResult = 1;

                        }
                        else
                        {
                            if (FailCount == preFailCount + 1)
                            {
                                mResult = 0;

                            }
                        }
                    }
                    catch
                    {
                        mResult = -1;//出错
                    }
                }
                    );
            };
            //label_reStart:
            Task taskDelay = Task.Delay(TestTimeout);
            var completeTask = await Task.WhenAny(startTask(), taskDelay);

            if (completeTask == taskDelay)
            {
                //超时退出
                mResult = 2;
                if (isRestarted == false)
                {
                    //isRestarted = true;
                    //await Task.Delay(200);

                    //if (await ReStartApp())
                    //{
                    //    AppReStartEventArgs e = new AppReStartEventArgs(true, Index);
                    //    OnAppReStarted(e);
                    //    mResult = -2;
                    //    goto label_reStart;


                    //}
                    //else
                    //{
                    //    AppReStartEventArgs e = new AppReStartEventArgs(false, Index);
                    //    OnAppReStarted(e);
                    //}

                }
                Log.Default.Error("测试机"+ Index.ToString() + "测试超时");
            }
            UpdateTester(mResult);
            callback(Index);
        }
        private void UpdateTester(int rst)
        {
        /*result = 0 -> Ng
        * result = 1 -> Pass
        * result = 2 -> Timeout
        */
            switch (rst)
            {
                case 0:
                    testStatus = TestStatus.Tested;
                    testResult = TestResult.Ng;
                    if (!IsInSampleMode)
                    {
                        FailCount++;
                    }
                    break;
                case 1:
                    testStatus = TestStatus.Tested;
                    testResult = TestResult.Pass;
                    if (!IsInSampleMode)
                    {
                        PassCount++;
                    }
                    break;
                case 2:
                    testStatus = TestStatus.Tested;
                    testResult = TestResult.TimeOut;
                    break;
                default:
                    testStatus = TestStatus.Err;
                    testResult = TestResult.TimeOut;
                    break;
            }
            if (!IsInSampleMode)
            {
                TestCount++;
            }
            Yield = Math.Round((float)PassCount / (PassCount + FailCount) * 100, 2);
            try
            {
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + Index.ToString(), "TestSpan", TestSpan.ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + Index.ToString(), "PassCount", PassCount.ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + Index.ToString(), "FailCount", FailCount.ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + Index.ToString(), "TestCount", TestCount.ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + Index.ToString(), "Yield", Yield.ToString());
            }
            catch
            {


            }

        }
    }
    public class TestRecord
    {
        private string testTime;
        public string TestTime
        {
            get { return testTime; }
            set { this.testTime = value; }
        }
        private string barcode;
        public string Barcode
        {
            get { return barcode; }
            set { this.barcode = value; }
        }
        private string testResult;
        public string TestResult
        {
            get { return testResult; }
            set { this.testResult = value; }
        }
        private string testCycleTime;
        public string TestCycleTime
        {
            get { return testCycleTime; }
            set { this.testCycleTime = value; }
        }
        private string index;
        public string Index
        {
            get { return index; }
            set { index = value; }
        }
    }
}
