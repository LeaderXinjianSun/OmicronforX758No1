using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;
using BingLibrary.hjb.Intercepts;
using System.ComponentModel.Composition;
using SxjLibrary;
using System.Windows;
using Omicron.Model;
using System.Windows.Forms;
using System.IO;
using ViewROI;
using HalconDotNet;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Threading;
//using MahApps.Metro.Controls.Dialogs;

namespace Omicron.ViewModel
{
    [BingAutoNotify]
    public class MainDataContext : DataSource
    {
        #region 属性绑定区域
        public virtual string AboutPageVisibility { set; get; } = "Collapsed";
        public virtual string HomePageVisibility { set; get; } = "Visible";
        public virtual string ParameterPageVisibility { set; get; } = "Collapsed";
        public virtual string CameraHcPageVisibility { set; get; } = "Collapsed";
        public virtual string ScanCameraPageVisibility { set; get; } = "Collapsed";
        public virtual string BarcodeDisplayPageVisibility { set; get; } = "Collapsed";
        public virtual string TesterParameterPageVisibility { set; get; } = "Collapsed";
        public virtual string TestRecordPageVisibility { set; get; } = "Collapsed";
        public virtual bool IsPLCConnect { set; get; } = false;
        public virtual bool IsTCPConnect { set; get; } = false;
        public virtual bool IsShieldTheDoor { set; get; } = false;
        public virtual bool IsOperateCiTie { set; get; } = true;
        public virtual string Msg { set; get; } = "";
        public virtual bool EpsonStatusAuto { set; get; } = false;
        public virtual bool EpsonStatusWarning { set; get; } = false;
        public virtual bool EpsonStatusSError { set; get; } = false;
        public virtual bool EpsonStatusSafeGuard { set; get; } = false;
        public virtual bool EpsonStatusEStop { set; get; } = false;
        public virtual bool EpsonStatusError { set; get; } = false;
        public virtual bool EpsonStatusPaused { set; get; } = false;
        public virtual bool EpsonStatusRunning { set; get; } = false;
        public virtual bool EpsonStatusReady { set; get; } = false;
        public virtual string SerialPortCom { set; get; }
        public virtual bool TestSendPortStatus { set; get; } = false;
        public virtual bool TestRevPortStatus { set; get; } = false;
        public virtual bool MsgRevPortStatus { set; get; } = false;
        public virtual bool CtrlPortStatus { set; get; } = false;
        public virtual string EpsonIp { set; get; } = "192.168.1.2";
        public virtual int EpsonTestSendPort { set; get; } = 2000;
        public virtual int EpsonTestReceivePort { set; get; } = 2001;
        public virtual int EpsonMsgReceivePort { set; get; } = 2002;
        public virtual int EpsonRemoteControlPort { set; get; } = 5000;
        public virtual string VisionScriptFileName { set; get; }
        public virtual string HcVisionScriptFileName { set; get; }        
        public virtual string ScanVisionScriptFileName { set; get; }
        public virtual HImage hImage { set; get; }
        public virtual ObservableCollection<HObject> hObjectList { set; get; }
        public virtual ObservableCollection<ROI> ROIList { set; get; } = new ObservableCollection<ROI>();
        public virtual int ActiveIndex { set; get; }
        public virtual bool Repaint { set; get; }

        public virtual HImage hImageScan { set; get; }
        public virtual ObservableCollection<HObject> hObjectListScan { set; get; }
        public virtual ObservableCollection<ROI> ROIListScan { set; get; } = new ObservableCollection<ROI>();
        public virtual int ActiveIndexScan { set; get; }
        public virtual bool RepaintScan { set; get; }

        public virtual string BarcodeDisplay { set; get; }

        public virtual bool FindFill1 { set; get; } = false;
        public virtual bool FindFill2 { set; get; } = false;
        public virtual bool FindFill3 { set; get; } = false;
        public virtual bool FindFill4 { set; get; } = false;
        public virtual bool FindFill5 { set; get; } = false;
        public virtual bool FindFill6 { set; get; } = false;

        public virtual string TestPcIPA { set; get; } = "192.168.1.101";
        public virtual string TestPcIPB { set; get; } = "192.168.1.102";
        public virtual int TestPcRemotePortA { set; get; } = 8000;
        public virtual int TestPcRemotePortB { set; get; } = 8000;
        public virtual string TestPcPathA { set; get; } = "/Users/zdt/Desktop/UIExplore_1.2.17_Flex.app";
        public virtual string TestPcPathB { set; get; } = "/Users/zdt/Desktop/UIExplore_1.2.17_Flex.app";
        public virtual bool TestCheckedAL { set; get; } = true;
        public virtual bool TestCheckedAR { set; get; } = true;
        public virtual bool TestCheckedBL { set; get; } = true;
        public virtual bool TestCheckedBR { set; get; } = true;

        public virtual string PickBracodeA { set; get; } = "Null";
        public virtual string TesterBracodeAL { set; get; } = "Null";
        public virtual string TesterBracodeAR { set; get; } = "Null";
        public virtual string TesterBracodeBL { set; get; } = "Null";
        public virtual string TesterBracodeBR { set; get; } = "Null";

        //public virtual DataTable TestRecodeDT { set; get; } = new DataTable();
        public virtual ObservableCollection<TestRecord> testRecord { set; get; } = new ObservableCollection<TestRecord>();

        public virtual double TestTime0 { set; get; } = 0;
        public virtual int TestCount0 { set; get; } = 0;
        public virtual int PassCount0 { set; get; } = 0;
        public virtual int FailCount0 { set; get; } = 0;
        public virtual double Yield0 { set; get; } = 0;

        public virtual double TestTime1 { set; get; } = 0;
        public virtual int TestCount1 { set; get; } = 0;
        public virtual int PassCount1 { set; get; } = 0;
        public virtual int FailCount1 { set; get; } = 0;
        public virtual double Yield1 { set; get; } = 0;

        public virtual double TestTime2 { set; get; } = 0;
        public virtual int TestCount2 { set; get; } = 0;
        public virtual int PassCount2 { set; get; } = 0;
        public virtual int FailCount2 { set; get; } = 0;
        public virtual double Yield2 { set; get; } = 0;

        public virtual double TestTime3 { set; get; } = 0;
        public virtual int TestCount3 { set; get; } = 0;
        public virtual int PassCount3 { set; get; } = 0;
        public virtual int FailCount3 { set; get; } = 0;
        public virtual double Yield3 { set; get; } = 0;

        public virtual string TesterResult0 { set; get; } = "Unknow";
        public virtual string TesterResult1 { set; get; } = "Unknow";
        public virtual string TesterResult2 { set; get; } = "Unknow";
        public virtual string TesterResult3 { set; get; } = "Unknow";

        public virtual string TesterStatusForeground0 { set; get; } = "Yellow";
        public virtual string TesterStatusForeground1 { set; get; } = "Yellow";
        public virtual string TesterStatusForeground2 { set; get; } = "Yellow";
        public virtual string TesterStatusForeground3 { set; get; } = "Yellow";

        public virtual string TesterStatusBackGround0 { set; get; } = "Gray";
        public virtual string TesterStatusBackGround1 { set; get; } = "Gray";
        public virtual string TesterStatusBackGround2 { set; get; } = "Gray";
        public virtual string TesterStatusBackGround3 { set; get; } = "Gray";

        public virtual string TestRecordSavePath { set; get; }
        public virtual string AlarmSavePath { set; get; }

        public virtual int NGContinueNum { set; get; }

        public virtual string AlarmTextString { set; get; }
        public virtual string AlarmTextGridShow { set; get; } = "Collapsed";
        public virtual bool PLCPause { set; get; } = false;
        #endregion
        #region 变量定义区域
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private string iniTesterResutPath = System.Environment.CurrentDirectory + "\\TesterResut.ini";
        private XinjiePlc XinjiePLC;
        private HdevEngine hdevEngine = new HdevEngine();
        private HdevEngine hdevScanEngine = new HdevEngine();
        private EpsonRC90 epsonRC90 = new EpsonRC90();
        private bool NeedNoiseReduce = false;
        private bool NeedLoadMaters = false;
        private bool NeedUnloadMaters = false;
        private string PreFeedFillStr = "FeedFill;0;0;0;0;0;0;";
        Queue<TestRecord> myTestRecordQueue = new Queue<TestRecord>();
        public static DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool PLCNeedContinue = false;
        #endregion
        #region 构造函数
        public MainDataContext()
        {
            epsonRC90.ModelPrint += ModelPrintEventProcess;
            epsonRC90.EpsonStatusUpdate += EpsonStatusUpdateProcess;
            epsonRC90.ScanUpdate += ScanUpdateProcess;
            epsonRC90.TestFinished += StartUpdateProcess;
            dispatcherTimer.Tick += new EventHandler(DispatcherTimerTickUpdateUi);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            Async.RunFuncAsync(UpdateUI,null);
        }
        #endregion
        #region 功能和方法
        public void ChoseHomePage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Visible";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseAboutPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseParameterPage()
        {
            ParameterPageVisibility = "Visible";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseTesterParameterPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Visible";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseCameraPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseCameraHcPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Visible";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseScanCameraPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Visible";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseBarcodeDisplayPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Visible";
            TestRecordPageVisibility = "Collapsed";
        }
        public void ChoseTestRecordPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Visible";
        }
        public async void ShieldDoorFunction()
        {
            
            if (!IsShieldTheDoor)
            {
                mydialog.changeaccent("red");
                var r = await mydialog.showconfirm("确定屏蔽安全门吗？请小心操作！");
                if (r)
                {
                    IsShieldTheDoor = !IsShieldTheDoor;
                }
                mydialog.changeaccent("blue");
            }
            else
            {
                IsShieldTheDoor = !IsShieldTheDoor;
            }
        }
        public async void OperateCiTieFunction()
        {

            if (IsOperateCiTie)
            {
                mydialog.changeaccent("red");
                var r = await mydialog.showconfirm("确定释放电磁铁吗？");
                if (r)
                {
                    IsOperateCiTie = !IsOperateCiTie;
                    Msg = messagePrint.AddMessage("电磁铁释放");
                }
                mydialog.changeaccent("blue");
            }
            else
            {
                IsOperateCiTie = !IsOperateCiTie;
            }
        }
        public async void EpsonOpetate(object p)
        {
            string s = p.ToString();
            switch (s)
            {
                //启动
                case "1":
                    AlarmTextGridShow = "Collapsed";
                    if (epsonRC90.CtrlStatus)
                    {
                        await epsonRC90.CtrlNet.SendAsync("$start,0");
                    }
                    break;
                //暂停
                case "2":
                    if (epsonRC90.CtrlStatus)
                    {
                        await epsonRC90.CtrlNet.SendAsync("$pause");
                    }
                    break;
                //继续
                case "3":
                    AlarmTextGridShow = "Collapsed";
                    if (epsonRC90.CtrlStatus)
                    {
                        await epsonRC90.CtrlNet.SendAsync("$continue");
                    }
                    if (PLCPause)
                    {
                        PLCNeedContinue = true;
                    }
                    break;
                //重启
                case "4":
                    AlarmTextGridShow = "Collapsed";
                    mydialog.changeaccent("red");
                    var r = await mydialog.showconfirm("确定进行停止机械手重启操作吗？");
                    if (r && epsonRC90.CtrlStatus)
                    {
                        await epsonRC90.CtrlNet.SendAsync("$stop");
                        await Task.Delay(300);
                        await epsonRC90.CtrlNet.SendAsync("$SetMotorOff,1");
                        await Task.Delay(400);
                        await epsonRC90.CtrlNet.SendAsync("$reset");
                    }
                    mydialog.changeaccent("blue");
                    break;
                //排料
                case "5":
                    mydialog.changeaccent("red");
                    r = await mydialog.showconfirm("确定进行排料操作吗？");
                    if (r && epsonRC90.CtrlStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("Discharge");
                    }
                    mydialog.changeaccent("blue");
                    break;
                //暂停
                case "6":
                    if (epsonRC90.CtrlStatus)
                    {
                        await epsonRC90.CtrlNet.SendAsync("$reset");
                    }
                    break;
                default:
                    break;
            }
        }
 
        public void NoiseReduce()
        {
            NeedNoiseReduce = true;
        }
        public void LoadMaters()
        {
            NeedLoadMaters = true;
        }
        public void UnLoadMaters()
        {
            NeedUnloadMaters = true;
        }
        public void SaveParameter()
        {
            var r1 = WriteParameter();
            if (r1)
            {
                Msg = messagePrint.AddMessage("写入参数成功");
            }
            else
            {
                Msg = messagePrint.AddMessage("写入参数成功");
            }
        }
        public void Selectfile(object p)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            switch (p.ToString())
            {
                case "0":
                    dlg.Filter = "视觉文件(*.vbai)|*.vbai|所有文件(*.*)|*.*";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        VisionScriptFileName = dlg.FileName;
                        Inifile.INIWriteValue(iniParameterPath, "Camera", "VisionScriptFileName", VisionScriptFileName);
                    }
                    break;
                case "1":
                    dlg.Filter = "视觉文件(*.hdev)|*.hdev|所有文件(*.*)|*.*";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        HcVisionScriptFileName = dlg.FileName;
                        Inifile.INIWriteValue(iniParameterPath, "Camera", "HcVisionScriptFileName", HcVisionScriptFileName);
                    }
                    break;
                case "2":
                    dlg.Filter = "视觉文件(*.hdev)|*.hdev|所有文件(*.*)|*.*";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        ScanVisionScriptFileName = dlg.FileName;
                        Inifile.INIWriteValue(iniParameterPath, "Camera", "ScanVisionScriptFileName", ScanVisionScriptFileName);
                    }
                    break;
                default:
                    break;
            }
            //dlg.InitialDirectory = System.Environment.CurrentDirectory;

            dlg.Dispose();
        }

        public async void UpdateSelectFlexer()
        {
            string str = "Select;";

            if (TestCheckedAL)
            {
                str += "1;";
            }
            else
            {
                str += "0;";
            }

            if (TestCheckedAR)
            {
                str += "1;";
            }
            else
            {
                str += "0;";
            }

            if (TestCheckedBL)
            {
                str += "1;";
            }
            else
            {
                str += "0;";
            }

            if (TestCheckedBR)
            {
                str += "1;";
            }
            else
            {
                str += "0;";
            }
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync(str + "123");
                Msg = messagePrint.AddMessage(str);
            }
            

            Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedAL", TestCheckedAL.ToString());
            Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedAR", TestCheckedAR.ToString());
            Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedBL", TestCheckedBL.ToString());
            Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedBR", TestCheckedBR.ToString());

            str = "NGContinueNum;" + NGContinueNum.ToString();
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync(str);
                Msg = messagePrint.AddMessage(str);
            }

            Inifile.INIWriteValue(iniParameterPath, "Tester", "NGContinueNum", NGContinueNum.ToString());

        }
        public async void ClearFlexer()
        {
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync("Clear;123");
                Msg = messagePrint.AddMessage("Clear;");
            }
            
        }
        public void CleantoZero(object p)
        {
            string s = p.ToString();
            int i = int.Parse(s);
            try
            {
                epsonRC90.tester[i].TestSpan = 0;
                epsonRC90.tester[i].PassCount = 0;
                epsonRC90.tester[i].FailCount = 0;
                epsonRC90.tester[i].TestCount = 0;
                epsonRC90.tester[i].Yield = 0;
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "TestSpan", "0");
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "PassCount", "0");
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "FailCount", "0");
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "TestCount", "0");
                Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "Yield", "0");
                Msg = messagePrint.AddMessage("测试机 " + (i+ 1).ToString() + " 数据清空");
            }
            catch
            {

            }
        }

        public void SelectSavePath(object p)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "请选择文件路径";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                switch (p.ToString())
                {
                    case "0":
                        TestRecordSavePath = dlg.SelectedPath;
                        Inifile.INIWriteValue(iniParameterPath, "SavePath", "TestRecordSavePath", TestRecordSavePath);

                        break;
                    case "1":
                        AlarmSavePath = dlg.SelectedPath;
                        Inifile.INIWriteValue(iniParameterPath, "SavePath", "AlarmSavePath", AlarmSavePath);
                        break;
                    default:
                        break;
                }                             
            }
        }
        private void SaveCSVfileRecord(TestRecord tr)
        {
            string filepath = TestRecordSavePath + "\\" + DateTime.Now.ToLongDateString().ToString() + ".csv";
            try
            {

                if (!File.Exists(filepath))
                {
                    string[] heads = { "Time", "Barcode", "Result", "Cycle", "Index" };
                    Csvfile.savetocsv(filepath, heads);
                }
                string[] conte = { tr.TestTime, tr.Barcode, tr.TestResult, tr.TestCycleTime, tr.Index };
                Csvfile.savetocsv(filepath, conte);
            }
            catch (Exception ex)
            {
                Msg = messagePrint.AddMessage("写入CSV文件失败");     
                Log.Default.Error("写入CSV文件失败", ex.Message);
            }
        }
        private void SaveCSVfileAlarm(string str)
        {
            string filepath = AlarmSavePath + "\\Alarm" + DateTime.Now.ToLongDateString().ToString() + ".csv";
            try
            {
                if (!File.Exists(filepath))
                {
                    string[] heads = { "DateTime", "Contant" };
                    Csvfile.savetocsv(filepath, heads);
                }
                string[] conte = { System.DateTime.Now.ToString(), str };
                Csvfile.savetocsv(filepath, conte);
            }
            catch (Exception ex)
            {
                Msg = messagePrint.AddMessage("写入CSV文件失败");
                Log.Default.Error("写入CSV文件失败", ex.Message);
            }
        }
        private void ShowAlarmTextGrid(string str)
        {
            AlarmTextString = str;
            AlarmTextGridShow = "Visible";
        }
        #endregion
        #region 事件相应函数
        private void ModelPrintEventProcess(string str)
        {
            Msg = messagePrint.AddMessage(str);
            switch (str)
            {
                case "MsgRev: 测试机1，吸取失败":
                    ShowAlarmTextGrid("测试机1，吸取失败");
                    break;
                case "MsgRev: 测试机2，吸取失败":
                    ShowAlarmTextGrid("测试机2，吸取失败");
                    break;
                case "MsgRev: 测试机3，吸取失败":
                    ShowAlarmTextGrid("测试机3，吸取失败");
                    break;
                case "MsgRev: 测试机4，吸取失败":
                    ShowAlarmTextGrid("测试机4，吸取失败");
                    break;
                case "MsgRev: 上料盘，吸取失败":
                    ShowAlarmTextGrid("上料盘，吸取失败");
                    break;
                case "MsgRev: 蚀刻不良":
                    ShowAlarmTextGrid("蚀刻不良");
                    break;
                case "MsgRev: 测试机1，测试超时":
                    ShowAlarmTextGrid("测试机1，测试超时");
                    break;
                case "MsgRev: 测试机2，测试超时":
                    ShowAlarmTextGrid("测试机2，测试超时");
                    break;
                case "MsgRev: 测试机3，测试超时":
                    ShowAlarmTextGrid("测试机3，测试超时");
                    break;
                case "MsgRev: 测试机4，测试超时":
                    ShowAlarmTextGrid("测试机4，测试超时");
                    break;
                case "MsgRev: 测试机1，连续NG":
                    ShowAlarmTextGrid("测试机1，连续NG");
                    break;
                case "MsgRev: 测试机2，连续NG":
                    ShowAlarmTextGrid("测试机2，连续NG");
                    break;
                case "MsgRev: 测试机3，连续NG":
                    ShowAlarmTextGrid("测试机3，连续NG");
                    break;
                case "MsgRev: 测试机4，连续NG":
                    ShowAlarmTextGrid("测试机4，连续NG");
                    break;
                default:
                    break;
            }
        }
        private void EpsonStatusUpdateProcess(string str)
        {
            EpsonStatusAuto = str[2] == '1';
            EpsonStatusWarning = str[3] == '1';
            EpsonStatusSError = str[4] == '1';
            EpsonStatusSafeGuard = str[5] == '1';
            EpsonStatusEStop = str[6] == '1';
            EpsonStatusError = str[7] == '1';
            EpsonStatusPaused = str[8] == '1';
            EpsonStatusRunning = str[9] == '1';
            EpsonStatusReady = str[10] == '1';
        }
        private void ScanUpdateProcess(string bar, HImage img)
        {
            hImageScan = img;
            BarcodeDisplay = bar;
        }
        private void StartUpdateProcess(int index)
        {
            TestRecord tr = new TestRecord(DateTime.Now.ToString(), epsonRC90.tester[index].TesterBracode, epsonRC90.tester[index].testResult.ToString(), epsonRC90.tester[index].TestSpan.ToString() + " s", (index + 1).ToString());
            lock (this)
            {
                myTestRecordQueue.Enqueue(tr);
            }
            //testRecord.Add(tr);
            SaveCSVfileRecord(tr);
            Msg = messagePrint.AddMessage("测试机 " + (index + 1).ToString() + " 测试完成");
        }
        #endregion
        #region 视觉
        #region Halcon
        public void cameraHcInit()
        {
            string filename = System.IO.Path.GetFileName(HcVisionScriptFileName);
            string fullfilename = System.Environment.CurrentDirectory + @"\" + filename;
            if (!(File.Exists(fullfilename)))
            {
                File.Copy(HcVisionScriptFileName, fullfilename);
            }
            else
            {
                FileInfo fileinfo1 = new FileInfo(HcVisionScriptFileName);
                FileInfo fileinfo2 = new FileInfo(fullfilename);
                TimeSpan ts = fileinfo1.LastWriteTime - fileinfo2.LastWriteTime;
                if (ts.TotalMilliseconds > 0)
                {
                    File.Copy(HcVisionScriptFileName, fullfilename,true);
                }
            }
            hdevEngine.initialengine(System.IO.Path.GetFileNameWithoutExtension(fullfilename));
            hdevEngine.loadengine();
        }
        public void CameraHcInspect()
        {
            Async.RunFuncAsync(cameraHcInspect,null);
        }
        public void cameraHcInspect()
        {
            ObservableCollection<HObject> objectList = new ObservableCollection<HObject>();
            hdevEngine.inspectengine();
            hImage = hdevEngine.getImage("Image");
            var fill1 = hdevEngine.getmeasurements("fill1");
            var fill2 = hdevEngine.getmeasurements("fill2");
            var fill3 = hdevEngine.getmeasurements("fill3");
            var fill4 = hdevEngine.getmeasurements("fill4");
            var fill5 = hdevEngine.getmeasurements("fill5");
            var fill6 = hdevEngine.getmeasurements("fill6");
            FindFill1 = fill1.ToString() == "1";
            FindFill2 = fill2.ToString() == "1";
            FindFill3 = fill3.ToString() == "1";
            FindFill4 = fill4.ToString() == "1";
            FindFill5 = fill5.ToString() == "1";
            FindFill6 = fill6.ToString() == "1";
            objectList.Add(hdevEngine.getRegion("Regions1"));
            objectList.Add(hdevEngine.getRegion("Regions2"));
            objectList.Add(hdevEngine.getRegion("Regions3"));
            objectList.Add(hdevEngine.getRegion("Regions4"));
            objectList.Add(hdevEngine.getRegion("Regions5"));
            objectList.Add(hdevEngine.getRegion("Regions6"));
            hObjectList = objectList;
        }

        #endregion
        #region Scan
        //public void scanCameraInit()
        //{
        //    string filename = System.IO.Path.GetFileName(ScanVisionScriptFileName);
        //    string fullfilename = System.Environment.CurrentDirectory + @"\" + filename;
        //    if (!(File.Exists(fullfilename)))
        //    {
        //        File.Copy(ScanVisionScriptFileName, fullfilename);
        //    }
        //    else
        //    {
        //        FileInfo fileinfo1 = new FileInfo(ScanVisionScriptFileName);
        //        FileInfo fileinfo2 = new FileInfo(fullfilename);
        //        TimeSpan ts = fileinfo1.LastWriteTime - fileinfo2.LastWriteTime;
        //        if (ts.TotalMilliseconds > 0)
        //        {
        //            File.Copy(ScanVisionScriptFileName, fullfilename, true);
        //        }
        //    }
        //    hdevScanEngine.initialengine(System.IO.Path.GetFileNameWithoutExtension(fullfilename));
        //    hdevScanEngine.loadengine();
        //}
        public void ScanCameraInspect()
        {
            Async.RunFuncAsync(epsonRC90.scanCameraInspect, null);
        }
        //public void scanCameraInspect()
        //{
        //    hdevScanEngine.inspectengine();
        //    hImageScan = hdevScanEngine.getImage("Image");
        //    var aa = hdevScanEngine.getmeasurements("DecodedDataStrings");
        //    BarcodeDisplay = aa.ToString();
        //}

        #endregion
        #endregion
        #region 读写操作
        private bool ReadParameter()
        {
            try
            {
                SerialPortCom = Inifile.INIGetStringValue(iniParameterPath, "SerialPort","Com","COM1");
                EpsonIp = Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonIp", "192.168.1.2");
                EpsonTestSendPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestSendPort", "2000"));
                EpsonTestReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonTestReceivePort", "2001"));
                EpsonMsgReceivePort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonMsgReceivePort", "2002"));
                EpsonRemoteControlPort = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Epson", "EpsonRemoteControlPort", "5000"));
                VisionScriptFileName = Inifile.INIGetStringValue(iniParameterPath, "Camera", "VisionScriptFileName", @"C:\test.vbai");
                HcVisionScriptFileName = Inifile.INIGetStringValue(iniParameterPath, "Camera", "HcVisionScriptFileName", @"C:\test.hdev");
                ScanVisionScriptFileName = Inifile.INIGetStringValue(iniParameterPath, "Camera", "ScanVisionScriptFileName", @"C:\test.hdev");
                TestPcIPA = Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcIPA", "192.168.1.101");
                TestPcIPB = Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcIPB", "192.168.1.102");
                TestPcRemotePortA = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcRemotePortA", "8000"));
                TestPcRemotePortB = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcRemotePortB", "8000"));
                TestPcPathA = Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcPathA", "/Users/zdt/Desktop/UIExplore_1.2.17_Flex.app");
                TestPcPathB = Inifile.INIGetStringValue(iniParameterPath, "Mac", "TestPcPathB", "/Users/zdt/Desktop/UIExplore_1.2.17_Flex.app");
                TestCheckedAL = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedAL", "True"));
                TestCheckedAR = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedAR", "True"));
                TestCheckedBL = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedBL", "True"));
                TestCheckedBR = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "TestCheckedBR", "True"));
                PickBracodeA = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "PickBracodeA", "Null");
                TesterBracodeAL = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeAL", "Null");
                TesterBracodeAR = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeAR", "Null");
                TesterBracodeBL = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeBL", "Null");
                TesterBracodeBR = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeBR", "Null");
                TestRecordSavePath = Inifile.INIGetStringValue(iniParameterPath, "SavePath", "TestRecordSavePath", "C:\\");
                AlarmSavePath = Inifile.INIGetStringValue(iniParameterPath, "SavePath", "AlarmSavePath", "C:\\");
                NGContinueNum = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "NGContinueNum", "4"));

                return true;
            }
            catch (Exception ex)
            {
                Log.Default.Error("ReadParameter",ex);
                return false;
            }          
        }
        private bool WriteParameter()
        {
            try
            {
                Inifile.INIWriteValue(iniParameterPath, "SerialPort", "Com", SerialPortCom);
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonIp", EpsonIp);
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonTestSendPort", EpsonTestSendPort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonTestReceivePort", EpsonTestReceivePort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonMsgReceivePort", EpsonMsgReceivePort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Epson", "EpsonRemoteControlPort", EpsonRemoteControlPort.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Mac", "TestPcIPA", TestPcIPA);
                Inifile.INIWriteValue(iniParameterPath, "Mac", "TestPcIPB", TestPcIPB);
                Inifile.INIWriteValue(iniParameterPath, "Mac", "TestPcRemotePortA", TestPcRemotePortA.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Mac", "TestPcRemotePortB", TestPcRemotePortB.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Mac", "TestPcPathA", TestPcPathA);
                Inifile.INIWriteValue(iniParameterPath, "Mac", "TestPcPathB", TestPcPathB);
                Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedAL", TestCheckedAL.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedAR", TestCheckedAR.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedBL", TestCheckedBL.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Tester", "TestCheckedBR", TestCheckedBR.ToString());

                //NGContinueNum
                Inifile.INIWriteValue(iniParameterPath, "Tester", "NGContinueNum", NGContinueNum.ToString());
                //Inifile.INIWriteValue(iniParameterPath, "Barcode", "PickBracodeA", PickBracodeA);
                //Inifile.INIWriteValue(iniParameterPath, "Barcode", "TesterBracodeAL", TesterBracodeAL);
                //Inifile.INIWriteValue(iniParameterPath, "Barcode", "TesterBracodeAR", TesterBracodeAR);
                //Inifile.INIWriteValue(iniParameterPath, "Barcode", "TesterBracodeBL", TesterBracodeBL);
                //Inifile.INIWriteValue(iniParameterPath, "Barcode", "TesterBracodeBR", TesterBracodeBR);
                return true;
            }
            catch (Exception ex)
            {
                Log.Default.Error("WriteParameter", ex);
                return false;
            }
        }
        #endregion
        #region FunctionTest
        public void FunctionTest()
        {
            //DataRow dr = TestRecodeDT.NewRow();
            //dr["Time"] = DateTime.Now.ToString();
            //dr["Barcode"] = "123";
            //dr["Result"] = TestResult.Pass.ToString();
            //dr["Cycle"] = 50.2;
            //dr["Index"] = 3;
            //TestRecodeDT.Rows.Add(dr);
            //epsonRC90.tester[1].Start(epsonRC90.StartProcess);
            //ShowAlarmTextGrid("测试机2，吸取失败");
            //TestRecord tr = new TestRecord(DateTime.Now.ToString(), "bar", "f", "11.1 s", "1");
            //SaveCSVfileRecord(tr);
        }
        #endregion
        #region UI更新
        private async void UpdateUI()
        {
            while (true)
            {
                await Task.Delay(100);
                TestSendPortStatus = epsonRC90.TestSendStatus;
                TestRevPortStatus = epsonRC90.TestReceiveStatus;
                MsgRevPortStatus = epsonRC90.MsgReceiveStatus;
                CtrlPortStatus = epsonRC90.CtrlStatus;
                IsTCPConnect = TestSendPortStatus & TestRevPortStatus & MsgRevPortStatus & CtrlPortStatus;

                try
                {
                    TestTime0 = epsonRC90.tester[0].TestSpan;
                    TestCount0 = epsonRC90.tester[0].TestCount;
                    PassCount0 = epsonRC90.tester[0].PassCount;
                    FailCount0 = epsonRC90.tester[0].FailCount;
                    Yield0 = epsonRC90.tester[0].Yield;
                    TesterBracodeAL = epsonRC90.tester[0].TesterBracode;

                    TestTime1 = epsonRC90.tester[1].TestSpan;
                    TestCount1 = epsonRC90.tester[1].TestCount;
                    PassCount1 = epsonRC90.tester[1].PassCount;
                    FailCount1 = epsonRC90.tester[1].FailCount;
                    Yield1 = epsonRC90.tester[1].Yield;
                    TesterBracodeAR = epsonRC90.tester[1].TesterBracode;

                    TestTime2 = epsonRC90.tester[2].TestSpan;
                    TestCount2 = epsonRC90.tester[2].TestCount;
                    PassCount2 = epsonRC90.tester[2].PassCount;
                    FailCount2 = epsonRC90.tester[2].FailCount;
                    Yield2 = epsonRC90.tester[2].Yield;
                    TesterBracodeBL = epsonRC90.tester[2].TesterBracode;

                    TestTime3 = epsonRC90.tester[3].TestSpan;
                    TestCount3 = epsonRC90.tester[3].TestCount;
                    PassCount3 = epsonRC90.tester[3].PassCount;
                    FailCount3 = epsonRC90.tester[3].FailCount;
                    Yield3 = epsonRC90.tester[3].Yield;
                    TesterBracodeBR = epsonRC90.tester[3].TesterBracode;

                    TesterResult0 = epsonRC90.tester[0].testResult.ToString();
                    switch (TesterResult0)
                    {
                        case "Ng":
                            TesterStatusBackGround0 = "Red";
                            TesterStatusForeground0 = "White";
                            break;
                        case "Pass":
                            TesterStatusBackGround0 = "Green";
                            TesterStatusForeground0 = "White";
                            break;
                        case "Unknow":
                            TesterStatusBackGround0 = "Wheat";
                            TesterStatusForeground0 = "Yellow";
                            break;
                        case "TimeOut":
                            TesterStatusBackGround0 = "Wheat";
                            TesterStatusForeground0 = "Maroon";
                            break;
                        default:
                            break;
                    }
                    TesterResult1 = epsonRC90.tester[1].testResult.ToString();
                    switch (TesterResult1)
                    {
                        case "Ng":
                            TesterStatusBackGround1 = "Red";
                            TesterStatusForeground1 = "White";
                            break;
                        case "Pass":
                            TesterStatusBackGround1 = "Green";
                            TesterStatusForeground1 = "White";
                            break;
                        case "Unknow":
                            TesterStatusBackGround1 = "Wheat";
                            TesterStatusForeground1 = "Yellow";
                            break;
                        case "TimeOut":
                            TesterStatusBackGround1 = "Wheat";
                            TesterStatusForeground1 = "Maroon";
                            break;
                        default:
                            break;
                    }
                    TesterResult2 = epsonRC90.tester[2].testResult.ToString();
                    switch (TesterResult2)
                    {
                        case "Ng":
                            TesterStatusBackGround2 = "Red";
                            TesterStatusForeground2 = "White";
                            break;
                        case "Pass":
                            TesterStatusBackGround2 = "Green";
                            TesterStatusForeground2 = "White";
                            break;
                        case "Unknow":
                            TesterStatusBackGround2 = "Wheat";
                            TesterStatusForeground2 = "Yellow";
                            break;
                        case "TimeOut":
                            TesterStatusBackGround2 = "Wheat";
                            TesterStatusForeground2 = "Maroon";
                            break;
                        default:
                            break;
                    }
                    TesterResult3 = epsonRC90.tester[3].testResult.ToString();
                    switch (TesterResult3)
                    {
                        case "Ng":
                            TesterStatusBackGround3 = "Red";
                            TesterStatusForeground3 = "White";
                            break;
                        case "Pass":
                            TesterStatusBackGround3 = "Green";
                            TesterStatusForeground3 = "White";
                            break;
                        case "Unknow":
                            TesterStatusBackGround3 = "Wheat";
                            TesterStatusForeground3 = "Yellow";
                            break;
                        case "TimeOut":
                            TesterStatusBackGround3 = "Wheat";
                            TesterStatusForeground3 = "Maroon";
                            break;
                        default:
                            break;
                    }
                }
                catch 
                {

                    
                }
                

                PickBracodeA = epsonRC90.PickBracodeA;

                //TesterBracodeAL = epsonRC90.TesterBracodeAL;
                //TesterBracodeAR = epsonRC90.TesterBracodeAR;
                //TesterBracodeBL = epsonRC90.TesterBracodeBL;
                //TesterBracodeBR = epsonRC90.TesterBracodeBR;
            }
        }
        private void DispatcherTimerTickUpdateUi(Object sender, EventArgs e)
        {
            if (myTestRecordQueue.Count > 0)
            {
                lock (this)
                {
                    foreach (TestRecord item in myTestRecordQueue)
                    {
                        testRecord.Add(item);
                    }
                    myTestRecordQueue.Clear();
                }
            }
        }
        #endregion
        #region 导入导出
        [Export(MEF.Contracts.ActionMessage)]
        [ExportMetadata(MEF.Key, "winclose")]
        public async void WindowClose()
        {
            mydialog.changeaccent("Red");
            
            var r = await mydialog.showconfirm("确定要关闭程序吗？");
            if (r)
            {
                //epsonRC90.TestSentNet.client.Close();
                //epsonRC90.TestReceiveNet.client.Close();
                //epsonRC90.MsgReceiveNet.client.Close();
                //epsonRC90.CtrlNet.client.Close();
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                mydialog.changeaccent("Cobalt");
            }
        }
        #endregion
        #region 初始化
        [Initialize]
        public async void WindowLoaded()
        {
            var r = ReadParameter();
            if (r)
            {
                Msg = messagePrint.AddMessage("读取参数成功");
            }
            else
            {
                Msg = messagePrint.AddMessage("读取参数失败");
            }
            string filepath = TestRecordSavePath + "\\" + DateTime.Now.ToLongDateString().ToString() + ".csv";
            DataTable dt = new DataTable();
            DataTable dt1;
            dt.Columns.Add("Time", typeof(string));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Result", typeof(string));
            dt.Columns.Add("Cycle", typeof(string));
            dt.Columns.Add("Index", typeof(string));
            try
            {
                if (File.Exists(filepath))
                {
                    dt1 = Csvfile.csv2dt(filepath, 1, dt);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt1.Rows)
                        {
                            TestRecord tr = new TestRecord(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString());
                            lock (this)
                            {
                                myTestRecordQueue.Enqueue(tr);
                            }
                        }
                        Msg = messagePrint.AddMessage("读取测试记录完成");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Default.Error("WindowLoadedcsv2dt", ex.Message);
            }
            cameraHcInit();
            await Task.Delay(100);
            CameraHcInspect();
            Msg = messagePrint.AddMessage("检测相机初始化完成");
            epsonRC90.scanCameraInit();
            await Task.Delay(100);
            ScanCameraInspect();
            //scanCameraInit();
            //Msg = messagePrint.AddMessage("扫码相机初始化完成");
        }
        [Initialize]
        public async void L91PLCWork()
        {
            bool TakePhoteFlage = false, _TakePhoteFlage = false;
            bool _IsShieldTheDoor = false;
            bool _IsOperateCiTie = false;
            bool _PLCPause = false;
            while (true)
            {
                await Task.Delay(200);
                if (!IsPLCConnect)
                {
                    if (XinjiePLC != null)
                    {
                        XinjiePLC.Closed();
                    }
                    try
                    {
                        XinjiePLC = new XinjiePlc(SerialPortCom, 19200, System.IO.Ports.Parity.Even, 8, System.IO.Ports.StopBits.One);
                        IsPLCConnect = XinjiePLC.Connect();
                    }
                    catch
                    {

                    }
                    if (IsPLCConnect)
                    {
                        IsPLCConnect = XinjiePLC.readM(24576);
                    }
                }
                else
                {
                    IsPLCConnect = XinjiePLC.readM(24576);
                    //拍照
                    TakePhoteFlage = XinjiePLC.readM(2100);
                    if (_TakePhoteFlage != TakePhoteFlage)
                    {
                        _TakePhoteFlage = TakePhoteFlage;
                        if (TakePhoteFlage == true)
                        {
                            XinjiePLC.setM(2100, false);
                            Async.RunFuncAsync(cameraHcInspect, PLCTakePhoteCallback);
                        }
                    }
                    //安全门
                    if (_IsShieldTheDoor != IsShieldTheDoor)
                    {
                        _IsShieldTheDoor = IsShieldTheDoor;
                        if (IsShieldTheDoor)
                        {
                            XinjiePLC.setM(1000, true);
                        }
                        else
                        {
                            XinjiePLC.setM(1000, false);
                        }
                    }
                    //电磁铁
                    if (_IsOperateCiTie != IsOperateCiTie)
                    {
                        _IsOperateCiTie = IsOperateCiTie;
                        if (IsOperateCiTie)
                        {
                            XinjiePLC.setM(1001, true);
                        }
                        else
                        {
                            XinjiePLC.setM(1001, false);
                        }
                    }
                    //消音
                    if (NeedNoiseReduce)
                    {
                        NeedNoiseReduce = false;
                        XinjiePLC.setM(1003, true);
                    }
                    //上料
                    if (NeedLoadMaters)
                    {
                        NeedLoadMaters = false;
                        XinjiePLC.setM(472, true);
                    }
                    //下料
                    if (NeedUnloadMaters)
                    {
                        NeedUnloadMaters = false;
                        XinjiePLC.setM(473, true);
                    }
                    //暂停
                    if (PLCNeedContinue == true)
                    {
                        PLCNeedContinue = false;
                        XinjiePLC.setM(1005, true);
                    }

                    PLCPause = XinjiePLC.readM(500);
                    if (_PLCPause != PLCPause)
                    {
                        _PLCPause = PLCPause;
                        if (PLCPause == true)
                        {
                            ShowAlarmTextGrid("PLC暂停");
                            Msg = messagePrint.AddMessage("PLC暂停");
                        }
                    }

                }

            }
        }
        private async void PLCTakePhoteCallback()
        {
            string str = "FeedFill;";
            if (FindFill1)
            {
                XinjiePLC.setM(2000, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill2)
            {
                XinjiePLC.setM(2001, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill3)
            {
                XinjiePLC.setM(2002, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill4)
            {
                XinjiePLC.setM(2003, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill5)
            {
                XinjiePLC.setM(2004, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill6)
            {
                XinjiePLC.setM(2005, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            XinjiePLC.setM(2006, true);
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync(PreFeedFillStr);
            }
            PreFeedFillStr = str;
        }
        #endregion

    }
}