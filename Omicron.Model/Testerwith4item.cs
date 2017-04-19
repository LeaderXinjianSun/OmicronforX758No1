﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SxjLibrary;
using System.IO;
using BingLibrary.hjb;
using System.Diagnostics;
using BingLibrary.hjb.Intercepts;

namespace Omicron.Model
{
    public class Testerwith4item
    {
        public static bool IsInSampleMode { set; get; } = false;
        #region 属性定义
        public string TestPcIP { set; get; } = "192.168.1.100";
        public int TestPcRemotePort { set; get; } = 8000;
        public string[] TesterBracode { set; get; } = new string[4] { "Null", "Null", "Null", "Null" };
        public int TestTimeout { set; get; }
        public int Index { set; get; }
        public int[] PassCount { set; get; } = new int[4];
        public int[] FailCount { set; get; } = new int[4];
        public int[] TestCount { set; get; } = new int[4];
        public double[] Yield { set; get; } = new double[4];

        public int[] PassCount_Nomal { set; get; }= new int[4];
        public int[] FailCount_Nomal { set; get; }= new int[4];
        public int[] TestCount_Nomal { set; get; } = new int[4];
        public double[] Yield_Nomal { set; get; } = new double[4];

        public double[] TestSpan { set; get; } = new double[4] { 0, 0, 0, 0 };
        public TestResult[] testResult { set; get; } = new TestResult[4] { TestResult.Unknow, TestResult.Unknow, TestResult.Unknow, TestResult.Unknow };
        public TestStatus[] testStatus { set; get; } = new TestStatus[4] { TestStatus.PreTest, TestStatus.PreTest, TestStatus.PreTest, TestStatus.PreTest };
        private bool[] TestActionSwitch = new bool[4];
        private short[] StepFlag = new short[4];

        private string iniTesterResutPath = System.Environment.CurrentDirectory + "\\TesterResut.ini";
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";

        public Udp udp;

        int[] prePassCount = new int[4] { 0, 0, 0, 0 };
        int[] preFailCount = new int[4] { 0, 0, 0, 0 };
        int[] pc = new int[4] { 0, 0, 0, 0 };
        int[] fc = new int[4] { 0, 0, 0, 0 };

        #region Mac命令
        public string[] StartStr { set; get; } = new string[4] { "", "", "", "" };
        public string[] BarcodeStr { set; get; } = new string[4] { "", "", "", "" };
        public string[] PassCountStr { set; get; } = new string[4] { "", "", "", "" };
        public string[] FailCountStr { set; get; } = new string[4] { "", "", "", "" };
        public string[] AppPathStr { set; get; } = new string[4] { "", "", "", "" };
        #endregion
        #endregion
        #region 构造函数
        public Testerwith4item(string ip, int port, int index)
        {
            TestPcIP = ip;
            TestPcRemotePort = port;
            Index = index;
            udp = new Udp(TestPcIP, TestPcRemotePort, 12000 + Index, 5000);
            TestCommandStringReadline();
            TestTimeout = 100000;
            TesterStatusInit();
            RunLoop();
        }
        #endregion
        #region 功能函数
        private void TestCommandStringReadline()
        {
            StreamReader srd;
            try
            {
                srd = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\FlexTestConfig.ini", Encoding.Default, true);
            }
            catch { return; }
            for (int i = 0; i < 2; i++)
            {
                BarcodeStr[i] = srd.ReadLine();
                StartStr[i] = srd.ReadLine();
                PassCountStr[i] = srd.ReadLine();
                FailCountStr[i] = srd.ReadLine();
                AppPathStr[i] = srd.ReadLine();
            }
            
        }
        private void TesterStatusInit()
        {
            for (int i = 0; i < 2; i++)
            {
                TestSpan[i] = double.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "TestSpan", "0"));
                PassCount[i] = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "PassCount", "0"));
                PassCount_Nomal[i] = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "PassCount_Nomal", "0"));
                FailCount[i] = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "FailCount", "0"));
                FailCount_Nomal[i] = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "FailCount_Nomal", "0"));
                TestCount[i] = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "TestCount", "0"));
                TestCount_Nomal[i] = int.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "TestCount_Nomal", "0"));
                Yield[i] = double.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "Yield", "0"));
                Yield_Nomal[i] = double.Parse(Inifile.INIGetStringValue(iniTesterResutPath, "Tester" + (Index * 2 + i).ToString(), "Yield_Nomal", "0"));
                string str = "";
                switch (Index)
                {
                    case 0:
                        switch (i)
                        {
                            case 0:
                                str = "TesterBracodeAL";
                                break;
                            case 1:
                                str = "TesterBracodeAR";
                                break;
                            default:
                                str = "";
                                break;
                        }
                        break;
                    case 1:
                        switch (i)
                        {
                            case 0:
                                str = "TesterBracodeBL";
                                break;
                            case 1:
                                str = "TesterBracodeBR";
                                break;
                            default:
                                str = "";
                                break;
                        }
                        break;
                    default:
                        break;
                }
                TesterBracode[i] = Inifile.INIGetStringValue(iniParameterPath, "Barcode", str, "Null");
            }
        }
        /// <数据更新保存AAB>
        /// 
        /// </summary>
        /// <param name="rst"></param>
        /// <param name="index_i"></param>
        public void UpdateTester1(int rst, int index_i)
        {
            /*result = 0 -> Ng
                   * result = 1 -> Pass
                   * result = 2 -> Timeout
                   */
            switch (rst)
            {
                case 0:

                        FailCount[index_i]++;

                    break;
                case 1:
   
                        PassCount[index_i]++;
                    
                    break;
                default:

                    break;
            }

                TestCount[index_i]++;
 
            Yield[index_i] = Math.Round((double)PassCount[index_i] / (PassCount[index_i] + FailCount[index_i]) * 100, 2);
            try
            {
                //Inifile.INIWriteValue(iniTesterResutPath, "Tester" + Index.ToString(), "TestSpan", TestSpan.ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "PassCount", PassCount[index_i].ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "FailCount", FailCount[index_i].ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "TestCount", TestCount[index_i].ToString());
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "Yield", Yield[index_i].ToString());
            }
            catch
            {


            }
        }
        /// <数据更新保存>
        /// 
        /// </数据更新保存>
        /// <param name="rst">结果</param>
        /// <param name="index_i">穴位索引</param>
        private void UpdateTester(int rst,int index_i)
        {
            /*result = 0 -> Ng
            * result = 1 -> Pass
            * result = 2 -> Timeout
            */
            switch (rst)
            {
                case 0:
                    testStatus[index_i] = TestStatus.Tested;
                    testResult[index_i] = TestResult.Ng;
                    if (!IsInSampleMode)
                    {
                        FailCount_Nomal[index_i]++;
                    }
                    
                    break;
                case 1:
                    testStatus[index_i] = TestStatus.Tested;
                    testResult[index_i] = TestResult.Pass;
                    if (!IsInSampleMode)
                    {
                        PassCount_Nomal[index_i]++;
                    }
                    
                    break;
                case 2:
                    testStatus[index_i] = TestStatus.Tested;
                    testResult[index_i] = TestResult.TimeOut;
                    break;
                default:
                    testStatus[index_i] = TestStatus.Err;
                    testResult[index_i] = TestResult.TimeOut;
                    break;
            }
            if (!IsInSampleMode)
            {
                TestCount_Nomal[index_i]++;
            }
            if (!IsInSampleMode)
            {
                Yield_Nomal[index_i] = Math.Round((double)PassCount_Nomal[index_i] / (PassCount_Nomal[index_i] + FailCount_Nomal[index_i]) * 100, 2);
            }
            
            try
            {
                if (!IsInSampleMode)
                {
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "TestSpan", TestSpan[index_i].ToString());
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "PassCount_Nomal", PassCount_Nomal[index_i].ToString());
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "FailCount_Nomal", FailCount_Nomal[index_i].ToString());
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "TestCount_Nomal", TestCount_Nomal[index_i].ToString());
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (Index * 2 + index_i).ToString(), "Yield_Nomal", Yield_Nomal[index_i].ToString());
                }

            }
            catch
            {


            }

        }
        public delegate void StartProcessedDelegate(int i);
        public async void Start1(StartProcessedDelegate callback)
        {
            Stopwatch sw = new Stopwatch();
            int mResult = -2;
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    //开始动作
                    StepFlag[0] = 0;
                    TestActionSwitch[0] = true;
                    
                    sw.Start();
                    testResult[0] = TestResult.Unknow;
                    testStatus[0] = TestStatus.Testing;
                    while (StepFlag[0] != 5)
                    {
                        if (mResult == 2)
                        {
                            TestActionSwitch[0] = false;
                            return;
                        }
                        TestSpan[0] = Math.Round(sw.Elapsed.TotalSeconds, 2);
                        await Task.Delay(50);
                    }
                    TestActionSwitch[0] = false;


                    if (pc[0] == prePassCount[0] + 1)
                    {
                        mResult = 1;
                    }
                    else
                    {
                        if (fc[0] == preFailCount[0] + 1)
                        {
                            mResult = 0;
                        }
                    }

                });
            };
            Task taskDelay = Task.Delay(TestTimeout);
            var completeTask = await Task.WhenAny(startTask(), taskDelay);
            if (completeTask == taskDelay)
            {
                //超时退出
                mResult = 2;
            }
            UpdateTester(mResult, 0);
            callback(Index * 2 + 0);
        }
        public async void Start2(StartProcessedDelegate callback)
        {
            Stopwatch sw = new Stopwatch();
            int mResult = -2;
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    //开始动作
                    StepFlag[1] = 0;
                    TestActionSwitch[1] = true;
                    
                    sw.Start();
                    testResult[1] = TestResult.Unknow;
                    testStatus[1] = TestStatus.Testing;
                    while (StepFlag[1] != 5)
                    {
                        if (mResult == 2)
                        {
                            TestActionSwitch[1] = false;                           
                            return;
                        }
                        TestSpan[1] = Math.Round(sw.Elapsed.TotalSeconds, 2);
                        await Task.Delay(50);
                    }
                    TestActionSwitch[1] = false;


                    if (pc[1] == prePassCount[1] + 1)
                    {
                        mResult = 1;
                    }
                    else
                    {
                        if (fc[1] == preFailCount[1] + 1)
                        {
                            mResult = 0;
                        }
                    }

                });
            };
            Task taskDelay = Task.Delay(TestTimeout);
            var completeTask = await Task.WhenAny(startTask(), taskDelay);
            if (completeTask == taskDelay)
            {
                //超时退出
                mResult = 2;
            }
            UpdateTester(mResult, 1);
            callback(Index * 2 + 1);
        }
        /// <扫描周期>
        /// 100ms
        /// </扫描周期>
        private async void RunLoop()
        {
            string s;
            string[] ss;
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (TestActionSwitch[i])
                    {
                        switch (StepFlag[i])
                        {
                            case 0://初始化
                                pc[i] = 0;
                                fc[i] = 0;
                                prePassCount[i] = 0;
                                preFailCount[i] = 0;
                                StepFlag[i] = 1;
                                break;
                            case 1://读PrePass；读PreNg
                                bool isPrePassSuccuss = false;
                                bool isPreNgSuccuss = false;
                                await Task.Delay(100);
                                s = udp.UdpSendthenReceive(PassCountStr[i]);
                                ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                if (ss[0] != "nil" && ss[0] != "Udp 发送或接收错误")
                                {
                                    try
                                    {
                                        prePassCount[i] = pc[i] = int.Parse(ss[0]);
                                        isPrePassSuccuss = true;
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Default.Error("Pass转码错误", e.Message);
                                    }
                                }
                                await Task.Delay(100);
                                s = udp.UdpSendthenReceive(FailCountStr[i]);
                                ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                if (ss[0] != "nil" && ss[0] != "Udp 发送或接收错误")
                                {
                                    try
                                    {
                                        preFailCount[i] = fc[i] = int.Parse(ss[0]);
                                        isPreNgSuccuss = true;
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Default.Error("Fail转码错误", e.Message);
                                    }
                                }
                                if (isPrePassSuccuss && isPreNgSuccuss)
                                {
                                    StepFlag[i] = 2;
                                }
                                break;
                            case 2://写条码
                                await Task.Delay(100);
                                s = udp.UdpSendthenReceive("setvalue:(AXValue:" + TesterBracode[i] + ")" + BarcodeStr[i]);
                                if (s == "SetValue Success")
                                {
                                    StepFlag[i] = 3;
                                }
                                break;
                            case 3://写按钮
                                await Task.Delay(100);
                                s = udp.UdpSendthenReceive(StartStr[i]);
                                if (s == "Action Success")
                                {
                                    StepFlag[i] = 4;
                                }
                                break;
                            case 4://读Pass；读Ng
                                await Task.Delay(100);
                                s = udp.UdpSendthenReceive(PassCountStr[i]);
                                ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                if (ss[0] != "nil" && ss[0] != "Udp 发送或接收错误")
                                {
                                    try
                                    {
                                        pc[i] = int.Parse(ss[0]);
                                        
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Default.Error("Pass转码错误", e.Message);
                                    }
                                }
                                await Task.Delay(100);
                                s = udp.UdpSendthenReceive(FailCountStr[i]);
                                ss = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                if (ss[0] != "nil" && ss[0] != "Udp 发送或接收错误")
                                {
                                    try
                                    {
                                        fc[i] = int.Parse(ss[0]);
                                        
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Default.Error("Fail转码错误", e.Message);
                                    }
                                }
                                if (pc[i] == prePassCount[i] + 1 || fc[0] == preFailCount[0] + 1)
                                {
                                    StepFlag[i] = 5;
                                }
                                break;
                            case 5://完成
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        StepFlag[i] = -1;

                    }
                }
                await Task.Delay(100);
            }
        }
        #endregion
    }
}