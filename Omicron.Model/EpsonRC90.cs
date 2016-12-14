using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;
using SxjLibrary;
using ViewROI;
using HalconDotNet;
using System.IO;

namespace Omicron.Model
{
    public class EpsonRC90
    {
        #region 属性
        public string IP { set; get; } = "192.168.1.2";
        public int TestSentPort { set; get; } = 2000;
        public int TestReceivePort { set; get; } = 2001;
        public int MsgReceivePort { set; get; } = 2002;
        public int CtrlPort { set; get; } = 5000;
        public bool TestSendStatus { get; set; } = false;
        public bool TestReceiveStatus { get; set; } = false;
        public bool MsgReceiveStatus { get; set; } = false;
        public bool CtrlStatus { set; get; } = false;
        public double Coord_X { set; get; } = 0;
        public double Coord_Y { set; get; } = 0;
        public double Coord_Z { set; get; } = 0;
        public double Coord_U { set; get; } = 0;
        public string ScanVisionScriptFileName { set; get; }
        #endregion
        #region 变量
        public HdevEngine hdevScanEngine = new HdevEngine();
        public TCPIPConnect TestSentNet = new TCPIPConnect();
        public TCPIPConnect TestReceiveNet = new TCPIPConnect();
        public TCPIPConnect MsgReceiveNet = new TCPIPConnect();
        public TCPIPConnect CtrlNet = new TCPIPConnect();
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private bool isLogined = false;
        //private string barcodeString = "";

        #endregion
        #region 事件定义
        public delegate void PrintEventHandler(string ModelMessageStr);
        public event PrintEventHandler ModelPrint;
        public delegate void EpsonStatusEventHandler(string EpsonStatusString);
        public event EpsonStatusEventHandler EpsonStatusUpdate;
        public delegate void ScanEventHandler(string bar, HImage img);
        public event ScanEventHandler ScanUpdate;
        #endregion
        #region 构造函数
        public EpsonRC90()
        {
            try
            {
                IP = Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonIp", "192.168.1.2");
                TestSentPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestSendPort", "2000"));
                TestReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestReceivePort", "2001"));
                MsgReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonMsgReceivePort", "2002"));
                CtrlPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonRemoteControlPort", "5000"));

                ScanVisionScriptFileName = Inifile.INIGetStringValue(iniParameterPath, "Camera", "ScanVisionScriptFileName", @"C:\test.hdev");

                Async.RunFuncAsync(checkCtrlNet, null);
                Async.RunFuncAsync(checkTestSentNet, null);
                Async.RunFuncAsync(checkTestReceiveNet, null);
                Async.RunFuncAsync(checkMsgReceiveNet, null);

                Async.RunFuncAsync(GetStatus, null);
                Async.RunFuncAsync(TestRevAnalysis,null);
            }
            catch (Exception ex)
            {
                Log.Default.Error("EpsonRC90.EpsonRC90()", ex.Message);
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
                            if (statuss[1].Length == 10)
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
                await Task.Delay(200);
            }
        }
        private async void TestRevAnalysis()
        {
            while (true)
            {
                await Task.Delay(100);
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
                    }
                    else
                    {
                        ModelPrint("TestRev: " + s);
                        try
                        {
                            string[] strs = s.Split(',');
                            switch (strs[0])
                            {
                                case "Coord":
                                    Coord_X = double.Parse(strs[1]);
                                    Coord_Y = double.Parse(strs[2]);
                                    Coord_Z = double.Parse(strs[3]);
                                    Coord_U = double.Parse(strs[4]);
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
        public void scanCameraInspect()
        {
            hdevScanEngine.inspectengine();
            var hImageScan = hdevScanEngine.getImage("Image");
            var aa = hdevScanEngine.getmeasurements("DecodedDataStrings");
            var barcodeString = aa.ToString();
            ScanUpdate(barcodeString, hImageScan);
        }

        #endregion

    }
}
