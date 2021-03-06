﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;
using SxjLibrary;
using ViewROI;
using HalconDotNet;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace Omicron.Model
{
    public class EpsonRC90
    {
        #region 属性
        public string IP { set; get; } = "192.168.1.2";
        public int TestSentPort { set; get; } = 2000;
        public int TestReceivePort { set; get; } = 2001;
        public int TestSentFlexPort { set; get; } = 2004;
        public int TestReceiveFlexPort { set; get; } = 2005;
        public int MsgReceivePort { set; get; } = 2002;
        public int CtrlPort { set; get; } = 5000;
        public bool TestSendStatus { get; set; } = false;
        public bool TestSendFlexStatus { get; set; } = false;

        public bool TestReceiveStatus { get; set; } = false;
        public bool TestReceiveFlexStatus { get; set; } = false;
        public bool MsgReceiveStatus { get; set; } = false;
        public bool CtrlStatus { set; get; } = false;
        public double Coord_X { set; get; } = 0;
        public double Coord_Y { set; get; } = 0;
        public double Coord_Z { set; get; } = 0;
        public double Coord_U { set; get; } = 0;
        public string ScanVisionScriptFileName { set; get; }
        public virtual bool BarcodeMode { set; get; } = true;

        public bool TestCheckedAL { set; get; } = true;
        public bool TestCheckedAR { set; get; } = true;
        public bool TestCheckedBL { set; get; } = true;
        public bool TestCheckedBR { set; get; } = true;

        public string TestPcIPA { set; get; } = "192.168.1.101";
        public string TestPcIPB { set; get; } = "192.168.1.102";
        public int TestPcRemotePortA { set; get; } = 8000;
        public int TestPcRemotePortB { set; get; } = 8000;

        public string PickBracodeA { set; get; }
        public string PickBracodeB { set; get; }

        public string TesterBracodeAL { set; get; } = "Null";
        public string TesterBracodeAR { set; get; } = "Null";
        public string TesterBracodeBL { set; get; } = "Null";
        public string TesterBracodeBR { set; get; } = "Null";



        #endregion
        #region 变量
        public HdevEngine hdevScanEngine = new HdevEngine();
        public TCPIPConnect TestSentNet = new TCPIPConnect();
        public TCPIPConnect TestReceiveNet = new TCPIPConnect();
        public TCPIPConnect TestSentFlexNet = new TCPIPConnect();
        public TCPIPConnect TestReceiveFlexNet = new TCPIPConnect();
        public TCPIPConnect MsgReceiveNet = new TCPIPConnect();
        public TCPIPConnect CtrlNet = new TCPIPConnect();
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private bool isLogined = false;
        //public Tester[] tester = new Tester[4];
        public Testerwith4item[] testerwith4item = new Testerwith4item[2];
        //private string barcodeString = "";
        private string BarcodeString = "";
        private string TestRecordSavePath = "";
        public UploadSoftwareStatus[] uploadSoftwareStatus = new UploadSoftwareStatus[4];
        private bool isCheckUpload = false;
        double PassLowLimitStop;
        int PassLowLimitStopNum;
        bool IsPassLowLimitStop;
        bool IsCheckINI;
        public int[] AdminAddNum = new int[4] { 0, 0, 0, 0 };
        private string ScanCheckPickStr = "";
        #endregion
        #region 事件定义
        public delegate void PrintEventHandler(string ModelMessageStr);
        public event PrintEventHandler ModelPrint;
        public event PrintEventHandler EPSONCommTwincat;
        public event PrintEventHandler EPSONDBSearch;
        public event PrintEventHandler EPSONSampleResult;
        public event PrintEventHandler EPSONSampleHave;
        public event PrintEventHandler EPSONSelectSampleResultfromDt;
        public event PrintEventHandler EPSONGRRTimesAsk;
        public delegate void EpsonStatusEventHandler(string EpsonStatusString);
        public event EpsonStatusEventHandler EpsonStatusUpdate;
        public delegate void ScanEventHandler(string bar, HImage img);
        public delegate void ScanP3EventHandler(string bar, HImage img, HObject hObject);
        public delegate void ScanP3EventHandler1(string bar);
        public event ScanEventHandler ScanUpdate;
        public event ScanP3EventHandler ScanP3Update;
        public event ScanP3EventHandler1 ScanP3Update1;
        public delegate void TestFinishedHandler(int index);
        public event TestFinishedHandler TestFinished;
       
        #endregion
        #region 构造函数
        public EpsonRC90()
        {
            try
            {
                Scan.ini("COM1");
                IP = Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonIp", "192.168.1.2");
                TestSentPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestSendPort", "2000"));
                TestSentFlexPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestSendFlexPort", "2004"));
                TestReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestReceivePort", "2001"));
                TestReceiveFlexPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestReceiveFlexPort", "2005"));
                MsgReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonMsgReceivePort", "2002"));
                CtrlPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonRemoteControlPort", "5000"));
                BarcodeMode = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "BarcodeMode", "BarcodeMode", "True"));
                if (BarcodeMode)
                {
                    ScanVisionScriptFileName = Inifile.INIGetStringValue(iniParameterPath, "Camera", "ScanVisionScriptFileName", @"C:\test.hdev");
                }
                else
                {
                    ScanVisionScriptFileName = Inifile.INIGetStringValue(iniParameterPath, "Camera", "ScanVisionScriptFileNameP3", @"C:\test.hdev");
                }

                

                TestPcIPA = Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcIPA", "192.168.1.101");
                TestPcIPB = Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcIPB", "192.168.1.102");
                TestPcRemotePortA = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcRemotePortA", "8000"));
                TestPcRemotePortB = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcRemotePortB", "8000"));

                TestCheckedAL = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedAL", "True"));
                TestCheckedAR = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedAR", "True"));
                TestCheckedBL = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedBL", "True"));
                TestCheckedBR = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedBR", "True"));

                for (int i = 0; i < 2; i++)
                {
                    if (i < 1)
                    {
                        testerwith4item[i] = new Testerwith4item(TestPcIPA, TestPcRemotePortA, i);
                    }
                    else
                    {
                        testerwith4item[i] = new Testerwith4item(TestPcIPB, TestPcRemotePortB, i);
                    }
                    
                }
                TestRecordSavePath = Inifile.INIGetStringValue(iniParameterPath, "SavePath", "TestRecordSavePath", "C:\\");
                isCheckUpload = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Upload", "IsCheckUploadStatus", "False"));
                PassLowLimitStop = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "PassYield", "PassLowLimitStop", "85"));
                PassLowLimitStopNum = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "PassYield", "PassLowLimitStopNum", "100"));
                IsPassLowLimitStop = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "PassYield", "IsPassLowLimitStop", "False"));
                IsCheckINI = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "CheckINI", "IsCheckINI", "False"));
                //isCheckUpload = true;
                for (int i = 0; i < 4; i++)
                {
                    uploadSoftwareStatus[i] = new UploadSoftwareStatus(i);
                }
                Async.RunFuncAsync(checkCtrlNet, null);
                Async.RunFuncAsync(checkTestSentNet, null);
                Async.RunFuncAsync(checkTestReceiveNet, null);
                Async.RunFuncAsync(checkTestSentFlexNet, null);
                Async.RunFuncAsync(checkTestReceiveFlexNet, null);
                Async.RunFuncAsync(checkMsgReceiveNet, null);

                Async.RunFuncAsync(GetStatus, null);
                Async.RunFuncAsync(TestRevAnalysis,null);
                Async.RunFuncAsync(TestRevFlexAnalysis, null);
                Async.RunFuncAsync(MsgRevAnalysis, null);
                Async.RunFuncAsync(EpsonRC90Init, null);
            }
            catch (Exception ex)
            {
                Log.Default.Error("EpsonRC90.EpsonRC90()", ex.Message);
            }
        }
        public void InitBarcodeList()
        {
            string filepath = TestRecordSavePath + "\\" + DateTime.Now.ToLongDateString().ToString() + ".csv";
            if (File.Exists(filepath))
            {
                DataTable dt = new DataTable();
                DataTable dt1;
                dt.Columns.Add("Time", typeof(string));
                dt.Columns.Add("Barcode", typeof(string));
                dt.Columns.Add("Result", typeof(string));
                dt.Columns.Add("Cycle", typeof(string));
                dt.Columns.Add("Index", typeof(string));
                try
                {
                    dt1 = Csvfile.csv2dt(filepath, 1, dt);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt1.Rows)
                        {
                            Testerwith4item.BarcodeList.Add(item["Barcode"].ToString());
                        }
                        ModelPrint("读取本地条码记录完成");
                    }
                    else
                    {
                        ModelPrint("本地文件无条码记录");
                    }
                }
                catch (Exception ex)
                {
                    Log.Default.Error("InitBarcodeList", ex.Message);
                }
            }
            else
            {
                ModelPrint("无当日本地记录文件");
            }
        }
        #endregion
        #region CheckNet
        public async void checkCtrlNet()
        {            
            while (true)
            {
                CtrlNet.IsOnline();
                if (!CtrlNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    CtrlNet.IsOnline();
                    if (!CtrlNet.tcpConnected)
                    {
                        isLogined = false;
                        bool r1 = await CtrlNet.Connect(IP, CtrlPort);
                        if (r1)
                        {
                            CtrlStatus = true;
                            ModelPrint("机械手CtrlNet连接");
                        }
                        else
                            CtrlStatus = false;
                    }
                }
                if (!isLogined && CtrlStatus)
                {
                    await CtrlNet.SendAsync("$login,123");
                    string s = await CtrlNet.ReceiveAsync();
                    if (s.Contains("#login,0"))
                        isLogined = true;
                    await Task.Delay(400);
                }
                else
                {
                    await Task.Delay(3000);
                }
            }
        }
        public async void checkTestSentNet()
        {
            while (true)
            {
                TestSentNet.IsOnline();
                await Task.Delay(400);
                if (!TestSentNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestSentNet.IsOnline();
                    if (!TestSentNet.tcpConnected)
                    {
                        bool r1 = await TestSentNet.Connect(IP, TestSentPort);
                        if (r1)
                        {
                            TestSendStatus = true;
                            ModelPrint("机械手TestSentNet连接");

                        }
                        else
                            TestSendStatus = false;
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkTestSentFlexNet()
        {
            while (true)
            {
                TestSentFlexNet.IsOnline();
                await Task.Delay(400);
                if (!TestSentFlexNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestSentFlexNet.IsOnline();
                    if (!TestSentFlexNet.tcpConnected)
                    {
                        bool r1 = await TestSentFlexNet.Connect(IP, TestSentFlexPort);
                        if (r1)
                        {
                            TestSendFlexStatus = true;
                            ModelPrint("机械手TestSentFlexNet连接");

                        }
                        else
                            TestSendFlexStatus = false;
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkTestReceiveNet()
        {
            while (true)
            {
                TestReceiveNet.IsOnline();
                await Task.Delay(400);
                if (!TestReceiveNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestReceiveNet.IsOnline();
                    if (!TestReceiveNet.tcpConnected)
                    {
                        bool r1 = await TestReceiveNet.Connect(IP, TestReceivePort);
                        if (r1)
                        {
                            TestReceiveStatus = true;
                            ModelPrint("机械手TestReceiveNet连接");

                        }
                        else
                            TestReceiveStatus = false;
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkTestReceiveFlexNet()
        {
            while (true)
            {
                TestReceiveFlexNet.IsOnline();
                await Task.Delay(400);
                if (!TestReceiveFlexNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    TestReceiveFlexNet.IsOnline();
                    if (!TestReceiveFlexNet.tcpConnected)
                    {
                        bool r1 = await TestReceiveFlexNet.Connect(IP, TestReceiveFlexPort);
                        if (r1)
                        {
                            TestReceiveFlexStatus = true;
                            ModelPrint("机械手TestReceiveFlexNet连接");

                        }
                        else
                            TestReceiveFlexStatus = false;
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        public async void checkMsgReceiveNet()
        {
            while (true)
            {
                MsgReceiveNet.IsOnline();
                await Task.Delay(400);
                if (!MsgReceiveNet.tcpConnected)
                {
                    await Task.Delay(1000);
                    MsgReceiveNet.IsOnline();
                    if (!MsgReceiveNet.tcpConnected)
                    {
                        bool r1 = await MsgReceiveNet.Connect(IP, MsgReceivePort);
                        if (r1)
                        {
                            MsgReceiveStatus = true;
                            ModelPrint("机械手MsgReceiveNet连接");

                        }
                        else
                            MsgReceiveStatus = false;
                    }
                }
                else
                { await Task.Delay(15000); }
            }
        }
        #endregion
        #region 工作函数
        private async void GetStatus()
        {
            string status = "";
            while (true)
            {
                if (isLogined == true)
                {
                    try
                    {
                        status = CtrlNet.SendThenReceive("$getstatus");
                        string[] statuss = status.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (statuss[0] == "#getstatus")
                        {
                            if (statuss[1].Length == 11)
                            {
                                EpsonStatusUpdate(statuss[1]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Default.Error("EpsonRC90.GetStatus", ex.Message);
                    }
                }
                await Task.Delay(1000);
            }
        }
        private async void TestRevAnalysis()
        {
            while (true)
            {
                //await Task.Delay(100);
                if (TestReceiveStatus == true)
                {
                    string s = await TestReceiveNet.ReceiveAsync();

                    string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        s = ss[0];
                    }
                    catch
                    {
                        s = "error";
                    }

                    if (s == "error")
                    {
                        TestReceiveNet.tcpConnected = false;
                        TestReceiveStatus = false;
                        ModelPrint("机械手TestReceiveNet断开");
                    }
                    else
                    {
                        ModelPrint("TestRev: " + s);
                        try
                        {
                            string[] strs = s.Split(',');
                            switch (strs[0])
                            {
                                case "Scan":
                                    //EpsonScanAction(strs[1], BacodeProcess);
                                    R750Inspect();
                                    break;
                                case "ScanP3":
                                    //EpsonScanAction(strs[1], BacodeProcess);
                                    PickBracodeA = "Null";
                                    PickBracodeB = "Null";
                                    R750Inspect();
                                    break;
                                case "ScanCheck":
                                    ScanCheckPickStr = strs[1];
                                    R750Inspect1();
                                    break;
                                case "SaveBarcode":
                                    switch (strs[2])
                                    {
                                        case "A":
                                            PickBracodeA = testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2];
                                            if (!Testerwith4item.IsInSampleMode)
                                            {
                                                testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2] = "Null";
                                            }
                                            
                                            break;
                                        case "B":
                                            PickBracodeB = testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2];
                                            if (!Testerwith4item.IsInSampleMode)
                                            {
                                                testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2] = "Null";
                                            }
                                                
                                            break;
                                        default:
                                            break;
                                    }
                                    if (!Testerwith4item.IsInSampleMode)
                                    {
                                        string barstr1;
                                        switch (int.Parse(strs[1]) - 1)
                                        {
                                            case 0:
                                                barstr1 = "TesterBracodeAL";
                                                TesterBracodeAL = "Null";
                                                break;
                                            case 1:
                                                barstr1 = "TesterBracodeAR";
                                                TesterBracodeAR = "Null";
                                                break;
                                            case 2:
                                                barstr1 = "TesterBracodeBL";
                                                TesterBracodeBL = "Null";
                                                break;
                                            case 3:
                                                barstr1 = "TesterBracodeBR";
                                                TesterBracodeBR = "Null";
                                                break;
                                            default:
                                                barstr1 = "";
                                                break;
                                        }
                                        Inifile.INIWriteValue(iniParameterPath, "Barcode", barstr1, "Null");
                                    }

                                    break;
                                case "Start":
                                    switch (strs[2])
                                    {
                                        case "A":
                                            //Tester.IsInSampleMode = false;
                                            testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2] = PickBracodeA;
                                            
                                            string barstr;
                                            switch (int.Parse(strs[1]) - 1)
                                            {
                                                case 0:
                                                    barstr = "TesterBracodeAL";
                                                    TesterBracodeAL = PickBracodeA;
                                                    break;
                                                case 1:
                                                    barstr = "TesterBracodeAR";
                                                    TesterBracodeAR = PickBracodeA;
                                                    break;
                                                case 2:
                                                    barstr = "TesterBracodeBL";
                                                    TesterBracodeBL = PickBracodeA;
                                                    break;
                                                case 3:
                                                    barstr = "TesterBracodeBR";
                                                    TesterBracodeBR = PickBracodeA;
                                                    break;
                                                default:
                                                    barstr = "";
                                                    break;
                                            }
                                            Inifile.INIWriteValue(iniParameterPath, "Barcode", barstr, PickBracodeA);
                                            if (!Testerwith4item.IsInSampleMode)
                                            {
                                                PickBracodeA = "Null";
                                            }
                                            
                                            //tester[int.Parse(strs[1]) - 1].Start(StartProcess);
                                            switch ((int.Parse(strs[1]) - 1) % 2)
                                            {
                                                case 0:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start1(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                case 1:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start2(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                default:
                                                    
                                                    break;
                                            }
                                            break;
                                        case "B":
                                            //Tester.IsInSampleMode = false;

                                            testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2] = PickBracodeB;
                                            
                                            //SaveStartBarcodetoCSV(PickBracodeB, int.Parse(strs[1]));
                                            switch (int.Parse(strs[1]) - 1)
                                            {
                                                case 0:
                                                    barstr = "TesterBracodeAL";
                                                    TesterBracodeAL = PickBracodeB;
                                                    break;
                                                case 1:
                                                    barstr = "TesterBracodeAR";
                                                    TesterBracodeAR = PickBracodeB;
                                                    break;
                                                case 2:
                                                    barstr = "TesterBracodeBL";
                                                    TesterBracodeBL = PickBracodeB;
                                                    break;
                                                case 3:
                                                    barstr = "TesterBracodeBR";
                                                    TesterBracodeBR = PickBracodeB;
                                                    break;
                                                default:
                                                    barstr = "";
                                                    break;
                                            }
                                            Inifile.INIWriteValue(iniParameterPath, "Barcode", barstr, PickBracodeB);
                                            if (!Testerwith4item.IsInSampleMode)
                                            {
                                                PickBracodeB = "Null";
                                            }
                                            
                                            switch ((int.Parse(strs[1]) - 1) % 2)
                                            {
                                                case 0:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start1(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                case 1:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start2(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                default:

                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "SamDBSearch":
                                    switch (strs[1])
                                    {
                                        case "A":
                                            EPSONDBSearch("A");
                                            break;
                                        case "B":
                                            EPSONDBSearch("B");
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "TestResultCount":
                                    switch (strs[1])
                                    {
                                        case "OK":
                                            testerwith4item[(int.Parse(strs[2]) - 1) / 2].UpdateTester1(1, (int.Parse(strs[2]) - 1) % 2);
                                            if (IsCheckINI)
                                            {
                                                switch (strs[3])
                                                {
                                                    case "A":
                                                        CheckiniAction(PickBracodeA, int.Parse(strs[2]) - 1);
                                                        break;
                                                    case "B":
                                                        CheckiniAction(PickBracodeB, int.Parse(strs[2]) - 1);
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                
                                            }
                                            break;
                                        case "NG":
                                            testerwith4item[(int.Parse(strs[2]) - 1) / 2].UpdateTester1(0, (int.Parse(strs[2]) - 1) % 2);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                case "SampleResult":
                                    EPSONSampleResult(s);
                                    break;
                                case "SamPanelHave":
                                    EPSONSampleHave(s);
                                    break;
                                case "SelectSampleResultfromDt":
                                    EPSONSelectSampleResultfromDt("");
                                    break;
                                    
                                case "GRRTimesAsk":
                                    EPSONGRRTimesAsk("GRRTimesAsk");
                                    break;
                                case "FMOVE":
                                    EPSONCommTwincat(s);
                                    break;
                                case "TMOVE":
                                    EPSONCommTwincat(s);
                                    break;
                                case "ULOAD":
                                    EPSONCommTwincat(s);
                                    break;
                                case "ResetCMD":
                                    EPSONCommTwincat(s);
                                    break;
                                case "StatusOfUpload":
                                    AnswerStatusOfUpload();
                                    break;
                                case "StatusOfYield":
                                    AnswerStatusOfYield();
                                    break;
                                default:
                                    ModelPrint("无效指令： " + s);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelPrint("监听机械手命令出错");
                            Log.Default.Error("EpsonRC90.TestRevAnalysis", ex.Message);
                        }

                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
        private async void TestRevFlexAnalysis()
        {
            while (true)
            {
                //await Task.Delay(100);
                if (TestReceiveFlexStatus == true)
                {
                    string s = await TestReceiveFlexNet.ReceiveAsync();

                    string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        s = ss[0];
                    }
                    catch
                    {
                        s = "error";
                    }

                    if (s == "error")
                    {
                        TestReceiveFlexNet.tcpConnected = false;
                        TestReceiveFlexStatus = false;
                        ModelPrint("机械手TestReceiveFlexNet断开");
                    }
                    else
                    {
                        ModelPrint("TestRevFlex: " + s);
                        try
                        {
                            string[] strs = s.Split(',');
                            switch (strs[0])
                            {
                                
                                case "Start":
                                    switch (strs[2])
                                    {
                                        case "A":
                                            //Tester.IsInSampleMode = false;
                                            testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2] = PickBracodeA;

                                            string barstr;
                                            switch (int.Parse(strs[1]) - 1)
                                            {
                                                case 0:
                                                    barstr = "TesterBracodeAL";
                                                    TesterBracodeAL = PickBracodeA;
                                                    break;
                                                case 1:
                                                    barstr = "TesterBracodeAR";
                                                    TesterBracodeAR = PickBracodeA;
                                                    break;
                                                case 2:
                                                    barstr = "TesterBracodeBL";
                                                    TesterBracodeBL = PickBracodeA;
                                                    break;
                                                case 3:
                                                    barstr = "TesterBracodeBR";
                                                    TesterBracodeBR = PickBracodeA;
                                                    break;
                                                default:
                                                    barstr = "";
                                                    break;
                                            }
                                            Inifile.INIWriteValue(iniParameterPath, "Barcode", barstr, PickBracodeA);
                                            PickBracodeA = "Null";
                                            //tester[int.Parse(strs[1]) - 1].Start(StartProcess);
                                            switch ((int.Parse(strs[1]) - 1) % 2)
                                            {
                                                case 0:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start1(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                case 1:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start2(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                default:

                                                    break;
                                            }
                                            break;
                                        case "B":
                                            //Tester.IsInSampleMode = false;

                                            testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2] = PickBracodeB;
                                            //SaveStartBarcodetoCSV(PickBracodeB, int.Parse(strs[1]));
                                            switch (int.Parse(strs[1]) - 1)
                                            {
                                                case 0:
                                                    barstr = "TesterBracodeAL";
                                                    TesterBracodeAL = PickBracodeB;
                                                    break;
                                                case 1:
                                                    barstr = "TesterBracodeAR";
                                                    TesterBracodeAR = PickBracodeB;
                                                    break;
                                                case 2:
                                                    barstr = "TesterBracodeBL";
                                                    TesterBracodeBL = PickBracodeB;
                                                    break;
                                                case 3:
                                                    barstr = "TesterBracodeBR";
                                                    TesterBracodeBR = PickBracodeB;
                                                    break;
                                                default:
                                                    barstr = "";
                                                    break;
                                            }
                                            Inifile.INIWriteValue(iniParameterPath, "Barcode", barstr, PickBracodeB);
                                            PickBracodeB = "Null";
                                            switch ((int.Parse(strs[1]) - 1) % 2)
                                            {
                                                case 0:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start1(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                case 1:
                                                    testerwith4item[(int.Parse(strs[1]) - 1) / 2].Start2(StartProcess);
                                                    SaveStartBarcodetoCSV(testerwith4item[(int.Parse(strs[1]) - 1) / 2].TesterBracode[(int.Parse(strs[1]) - 1) % 2], int.Parse(strs[1]));
                                                    break;
                                                default:

                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                               
                                default:
                                    ModelPrint("无效指令： " + s);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelPrint("监听机械手Flex命令出错");
                            Log.Default.Error("EpsonRC90.TestRevFlexAnalysis", ex.Message);
                        }

                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
        private async void AnswerStatusOfUpload()
        {
            string str = "StatusOfUpload";
            for (int i = 0; i < 4; i++)
            {
                if (uploadSoftwareStatus[i].status || !isCheckUpload)
                {
                    str += ";1";
                }
                else
                {
                    str += ";0";
                }
            }

            if (TestSendStatus)
            {
                await TestSentNet.SendAsync(str);
            }
        }
        private async void AnswerStatusOfYield()
        {
            string str = "StatusOfYield";

            for (int i = 0; i < 4; i++)
            {
                if (testerwith4item[i / 2].Yield_Nomal[i % 2] >= PassLowLimitStop || !IsPassLowLimitStop || testerwith4item[i / 2].TestCount_Nomal[i % 2] < PassLowLimitStopNum + AdminAddNum[i])
                {
                    str += ";1";
                }
                else
                {
                    str += ";0";
                }
            }

            if (TestSendStatus)
            {
                await TestSentNet.SendAsync(str);
            }
        }
        private async void CheckiniAction(string barcode, int index)
        {
            string iniFilepath = @"d:\test.ini";
            string sectionName = "";
            Stopwatch sw = new Stopwatch();
            switch (index)
            {
                case 0:
                    sectionName = "A";
                    break;
                case 1:
                    sectionName = "B";
                    break;
                case 2:
                    sectionName = "C";
                    break;
                case 3:
                    sectionName = "D";
                    break;
                default:
                    break;
            }
            sw.Start();
            await ((Func<Task>)(() =>
            {
                return Task.Run(() =>
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        ModelPrint((index+1).ToString() + "条码比对中 " + Math.Round(sw.Elapsed.TotalSeconds, 1).ToString());
                        string bar1 = Inifile.INIGetStringValue(iniFilepath, sectionName, "bar", "999");
                        string rst = Inifile.INIGetStringValue(iniFilepath, sectionName, "result", "999"); 
                        if ((bar1 == barcode && rst == "OK") || sw.Elapsed.TotalSeconds > 30 || !IsCheckINI)
                        {
                            
                            break;
                        }
                        else
                        {
                            if (bar1 == barcode)
                            {
                                ModelPrint("条码 " + barcode +" 匹配成功，但结果异常");
                            }
                        }
                    }
                });
            }))();
            sw.Stop();
            if (sw.Elapsed.TotalSeconds > 30)
            {
                await TestSentNet.SendAsync("CheckBarcodeResult;2");
            }
            else
            {
                await TestSentNet.SendAsync("CheckBarcodeResult;1");
            }
            
        }
        public async void StartProcess(int index)
        {
            TestFinished(index);
            if (testerwith4item[index / 2].testStatus[index % 2] == TestStatus.Err)
            {
                Log.Default.Error("测试机 " + (index + 1).ToString() + " 测试过程出错");
                ModelPrint("测试机 " + (index + 1).ToString() + " 测试过程出错");
            }
            else
            {
                if (testerwith4item[index / 2].testStatus[index % 2] == TestStatus.Tested)
                {
                    ModelPrint("测试机 " + (index + 1).ToString() + " 测试完成 " + testerwith4item[index / 2].testResult[index % 2].ToString() + ";" + testerwith4item[index / 2].testRemarks[index % 2]);
                    if (testerwith4item[index / 2].testResult[index % 2] == TestResult.Pass && isCheckUpload)
                    {
                        Async.RunFuncAsync(uploadSoftwareStatus[index].StartCommand, null);
                    }
                    string r = await TestSentFlexNet.SendAsync("TestResult;" + testerwith4item[index / 2].testResult[index % 2].ToString() + ";" + (index + 1).ToString() + ";" + testerwith4item[index / 2].testRemarks[index % 2]);
                    if (r == "error")
                    {
                        Log.Default.Error("TestSentFlex网络出错");
                        ModelPrint("TestSentFlex网络出错");
                        TestSentFlexNet.tcpConnected = false;
                        TestSendFlexStatus = false;
                    }
                }
            }
        }
        private void SaveStartBarcodetoCSV(string bar, int index_ii)
        {
            if (!Directory.Exists(TestRecordSavePath + @"\Barcode\" + DateTime.Now.ToLongDateString().ToString()))
            {
                Directory.CreateDirectory(TestRecordSavePath + @"\Barcode\" + DateTime.Now.ToLongDateString().ToString());
            }
            string filepath = TestRecordSavePath + @"\Barcode\" + DateTime.Now.ToLongDateString().ToString() + @"\Tester" + index_ii.ToString() + (DateTime.Now.ToShortDateString()).Replace("/", "") + ".csv";
            try
            {
                if (!File.Exists(filepath))
                {
                    string[] heads = { "DateTime", "TesterBarcode" };
                    Csvfile.savetocsv(filepath, heads);
                }
                string[] conte = { System.DateTime.Now.ToString(), bar };
                Csvfile.savetocsv(filepath, conte);
            }
            catch (Exception ex)
            {
                //Msg = messagePrint.AddMessage("写入CSV文件失败");
                Log.Default.Error("写入CSV文件失败", ex.Message);
            }
        }
        private async void MsgRevAnalysis()
        {
            while (true)
            {
                //await Task.Delay(100);
                if (MsgReceiveStatus == true)
                {
                    string s = await MsgReceiveNet.ReceiveAsync();

                    string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        s = ss[0];
                    }
                    catch
                    {
                        s = "error";
                    }

                    if (s == "error")
                    {
                        MsgReceiveNet.tcpConnected = false;
                        MsgReceiveStatus = false;
                        ModelPrint("机械手MsgReceiveNet断开");
                    }
                    else
                    {
                        ModelPrint("MsgRev: " + s);
                    }
                }
                else
                {
                    await Task.Delay(100);
                }
            }
        }
        private async void EpsonRC90Init()
        {
            while (!TestSendStatus)
            {
                await Task.Delay(1000);
            }
            await TestSentNet.SendAsync("InitPar;123");
            ModelPrint("机械手控制器，初始化完成");
        }
        private delegate void EpsonScanProcessedDelegate(string bar,string pick);
        private void EpsonScanAction(string pick, EpsonScanProcessedDelegate callback)
        {
            callback(scanCameraInspect(), pick);
        }
        private async void BacodeProcess(string barcode, string pick)
        {
            HTuple Cfind;
            if (BarcodeMode)
            {
                if (barcode == "")
                {
                    Cfind = "-1";
                }
                else
                {
                    Cfind = "1";
                }
                    
            }
            else
            {
                //Cfind = hdevScanEngine.getmeasurements("Cfind");
                Cfind = "1";
            }
                
            switch (pick)
            {
                case "A":
                    PickBracodeA = barcode;
                    Inifile.INIWriteValue(iniParameterPath, "Barcode", "PickBracodeA", PickBracodeA);
                    break;
                case "B":
                    PickBracodeB = barcode;
                    Inifile.INIWriteValue(iniParameterPath, "Barcode", "PickBracodeB", PickBracodeB);
                    break;
                default:
                    break;
            }
            if (Cfind.ToString() != "1")
            {
                ModelPrint("蚀刻不良");
                if (TestSendStatus)
                {
                    await TestSentNet.SendAsync("ScanResult;ShikeNg;" + pick);
                }
            }
            else
            {
                if (barcode == "")
                {

                    ModelPrint("扫码不良");
                    if (TestSendStatus)
                    {
                        await TestSentNet.SendAsync("ScanResult;Ng;" + pick);
                    }                   
                }
                else
                {
                    ModelPrint("扫码成功 " + barcode);
                    if (TestSendStatus)
                    {
                        await TestSentNet.SendAsync("ScanResult;Pass;" + pick);
                    }
                }
            }

        }
        #endregion
        #region Scan
        public void scanCameraInit()
        {
            string filename = System.IO.Path.GetFileName(ScanVisionScriptFileName);
            string fullfilename = System.Environment.CurrentDirectory + @"\" + filename;
            if (!(File.Exists(fullfilename)))
            {
                File.Copy(ScanVisionScriptFileName, fullfilename);
            }
            else
            {
                FileInfo fileinfo1 = new FileInfo(ScanVisionScriptFileName);
                FileInfo fileinfo2 = new FileInfo(fullfilename);
                TimeSpan ts = fileinfo1.LastWriteTime - fileinfo2.LastWriteTime;
                if (ts.TotalMilliseconds > 0)
                {
                    File.Copy(ScanVisionScriptFileName, fullfilename, true);
                }
            }
            hdevScanEngine.initialengine(System.IO.Path.GetFileNameWithoutExtension(fullfilename));
            hdevScanEngine.loadengine();
            ModelPrint("扫码相机初始化完成");
        }
        public void ScanCameraInspect()
        {
            Async.RunFuncAsync(scanCameraInspect, null);
        }
        public string scanCameraInspect()
        {
            hdevScanEngine.inspectengine();
            var hImageScan = hdevScanEngine.getImage("Image");
            var aa = hdevScanEngine.getmeasurements("DecodedDataStrings");

            var barcodeString = aa.ToString();
            if (BarcodeMode)
            {
                ScanUpdate(barcodeString, hImageScan);
            }
            else
            {
                //var hObject = hdevScanEngine.getRegion("Rectangle_FindConnecter");
                ScanP3Update(barcodeString, hImageScan, null);
            }

            return barcodeString;
            //return "Z71A0HB2HP192Z" + getString(2);
        }
        public void R750Inspect()
        {
            Scan.GetBarCode(R750InspectCallback);
            //R750InspectCallback("123");

            //return barcodeString;
            //return "Z71A0HB2HP192Z" + getString(2);
        }
        public void R750Inspect1()
        {
            Scan.GetBarCode(ScanCheckCallback);
        }
        private async void R750InspectCallback(string barcode)
        {
            PickBracodeA = barcode;
            Inifile.INIWriteValue(iniParameterPath, "Barcode", "PickBracodeA", PickBracodeA);
            if (barcode == "Error")
            {

                ModelPrint("扫码不良");
                if (TestSendStatus)
                {
                    await TestSentNet.SendAsync("ScanResult;Ng;A");
                }
            }
            else
            {
                ModelPrint("扫码成功 " + barcode);
                if (TestSendStatus)
                {
                    await TestSentNet.SendAsync("ScanResult;Pass;A");
                }
            }
            ScanP3Update1(barcode);
        }
        private async void ScanCheckCallback(string barcode)
        {
            string pickbarcode = "";
            switch (ScanCheckPickStr)
            {
                case "A":
                    pickbarcode = PickBracodeA;
                    break;
                case "B":
                    pickbarcode = PickBracodeB;
                    break;
                default:
                    break;
            }
            
            
            if (barcode == "Error")
            {

                ModelPrint("扫码不良");
                if (TestSendStatus)
                {
                    await TestSentNet.SendAsync("CheckScanResult;Ng");
                }
            }
            else
            {
                ModelPrint("扫码成功 " + barcode);
                if (pickbarcode == barcode)
                {
                    ModelPrint("条码比对成功 " + barcode);
                    if (TestSendStatus)
                    {
                        await TestSentNet.SendAsync("CheckScanResult;Pass");
                    }
                }
                else
                {
                    ModelPrint("条码比对失败 " + barcode);
                    if (TestSendStatus)
                    {
                        await TestSentNet.SendAsync("CheckScanResult;Ng");
                    }
                }
            }
            //ScanP3Update1(barcode);
        }
        public string getString(int count)
        {
            int number;
            string checkCode = String.Empty;
            System.Random myRandom = new Random();
            for (int i = 0; i < count; i++)
            {
                number = myRandom.Next();
                number = number % 36;
                if (number < 10)
                {
                    number += 48;
                }
                else
                {
                    number += 55;
                }
                checkCode += ((char)number).ToString();
            }
            return checkCode;
        }
        #endregion

    }
}
