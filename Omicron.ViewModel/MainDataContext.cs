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
        public virtual string OperaterActionPageVisibility { set; get; } = "Collapsed";
        public virtual string TestRecordPageVisibility { set; get; } = "Collapsed";
        public virtual string TwincatNcPageVisibility { set; get; } = "Collapsed";
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
        public virtual string ScanVisionScriptFileNameP3 { set; get; }
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
        public virtual string PickBracodeB { set; get; } = "Null";
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

        public virtual int SingleTestModeStageNum { set; get; } = 1;
        public virtual bool SingleTestMode { set; get; } = false;

        public virtual bool AABReTest { set; get; } = false;

        public virtual int SingleTestTimes { set; get; } = 0;
        public virtual string SingleTestTimesVisibility { set; get; } = "Collapsed";
        public virtual string LoginButtonString { set; get; } = "登录";
        public virtual string LoginUserName { set; get; } = "Leader";
        public virtual string LoginPassword { set; get; } = "jsldr";
        public virtual bool isLogin { set; get; } = false;
        public virtual bool BarcodeMode { set; get; } = true;

        public virtual string LastChuiqiTimeStr { set; get; } = "";
        public virtual bool IsTestersClean { set; get; }

        public virtual bool AllowSampleTestCommand { set; get; } = false;

        public virtual TwinCATCoil1 XPos { set; get; }
        public virtual TwinCATCoil1 YPos { set; get; }
        public virtual TwinCATCoil1 FPos { set; get; }
        public virtual TwinCATCoil1 TPos { set; get; }

        public virtual TwinCATCoil1 PickPositionX { set; get; }
        public virtual TwinCATCoil1 PickPositionY { set; get; }
        public virtual TwinCATCoil1 WaitPositionX { set; get; }
        public virtual TwinCATCoil1 WaitPositionY { set; get; }

        public virtual TwinCATCoil1 ReleasePositionX1 { set; get; }
        public virtual TwinCATCoil1 ReleasePositionY1 { set; get; }
        public virtual TwinCATCoil1 ReleasePositionX2 { set; get; }
        public virtual TwinCATCoil1 ReleasePositionY2 { set; get; }
        public virtual TwinCATCoil1 ReleasePositionX3 { set; get; }
        public virtual TwinCATCoil1 ReleasePositionY3 { set; get; }

        public virtual TwinCATCoil1 PowerOn1 { set; get; }
        public virtual TwinCATCoil1 PowerOn2 { set; get; }
        public virtual TwinCATCoil1 PowerOn3 { set; get; }
        public virtual TwinCATCoil1 PowerOn4 { set; get; }

        public virtual TwinCATCoil1 ServoRst1 { set; get; }
        public virtual TwinCATCoil1 ServoRst2 { set; get; }
        public virtual TwinCATCoil1 ServoRst3 { set; get; }
        public virtual TwinCATCoil1 ServoRst4 { set; get; }

        public virtual TwinCATCoil1 ServoSVN1 { set; get; }
        public virtual TwinCATCoil1 ServoSVN2 { set; get; }
        public virtual TwinCATCoil1 ServoSVN3 { set; get; }
        public virtual TwinCATCoil1 ServoSVN4 { set; get; }

        public virtual bool ServoHomed1 { set; get; }
        public virtual bool ServoHomed2 { set; get; }
        public virtual bool ServoHomed3 { set; get; }
        public virtual bool ServoHomed4 { set; get; }

        public virtual TwinCATCoil1 XRDY { set; get; }
        public virtual TwinCATCoil1 YRDY { set; get; }
        public virtual TwinCATCoil1 FRDY { set; get; }
        public virtual TwinCATCoil1 TRDY { set; get; }

        public virtual TwinCATCoil1 XYInDebug { set; get; }
        public virtual TwinCATCoil1 FInDebug { set; get; }
        public virtual TwinCATCoil1 TInDebug { set; get; }

        public virtual TwinCATCoil1 EF104 { set; get; }
        public virtual TwinCATCoil1 EF114 { set; get; }

        public virtual TwinCATCoil1 EF100 { set; get; }
        public virtual TwinCATCoil1 EF101 { set; get; }
        public virtual TwinCATCoil1 EF102 { set; get; }
        public virtual TwinCATCoil1 EF110 { set; get; }
        public virtual TwinCATCoil1 EF111 { set; get; }
        public virtual TwinCATCoil1 EF112 { set; get; }

        public virtual TwinCATCoil1 DebugXTargetPositon { set; get; }
        public virtual TwinCATCoil1 DebugYTargetPositon { set; get; }

        public virtual TwinCATCoil1 DebugFTargetPositon { set; get; }
        public virtual TwinCATCoil1 DebugTTargetPositon { set; get; }

        public virtual TwinCATCoil1 Calc_Start { set; get; }

        public virtual TwinCATCoil1 FPosition1 { set; get; }
        public virtual TwinCATCoil1 FPosition2 { set; get; }
        public virtual TwinCATCoil1 FPosition3 { set; get; }
        public virtual TwinCATCoil1 FPosition4 { set; get; }
        public virtual TwinCATCoil1 FPosition5 { set; get; }
        public virtual TwinCATCoil1 FPosition6 { set; get; }
        public virtual TwinCATCoil1 FPosition7 { set; get; }

        public virtual TwinCATCoil1 TPosition1 { set; get; }
        public virtual TwinCATCoil1 TPosition2 { set; get; }
        public virtual TwinCATCoil1 TPosition3 { set; get; }
        public virtual TwinCATCoil1 TPosition4 { set; get; }
        public virtual TwinCATCoil1 TPosition5 { set; get; }
        public virtual TwinCATCoil1 TPosition6 { set; get; }
        public virtual TwinCATCoil1 TPosition7 { set; get; }

        public virtual TwinCATCoil1 F200 { set; get; }
        public virtual TwinCATCoil1 F201 { set; get; }
        public virtual TwinCATCoil1 F202 { set; get; }
        public virtual TwinCATCoil1 F204 { set; get; }

        public virtual TwinCATCoil1 T200 { set; get; }
        public virtual TwinCATCoil1 T201 { set; get; }
        public virtual TwinCATCoil1 T202 { set; get; }
        public virtual TwinCATCoil1 T204 { set; get; }

        public virtual TwinCATCoil1 FCmdIndex { set; get; }
        public virtual TwinCATCoil1 FMoveCMD { set; get; }
        public virtual TwinCATCoil1 FMoveCompleted { set; get; }

        public virtual TwinCATCoil1 TCmdIndex { set; get; }
        public virtual TwinCATCoil1 TMoveCMD { set; get; }
        public virtual TwinCATCoil1 TMoveCompleted { set; get; }

        public virtual TwinCATCoil1 TUnloadCMD { set; get; }
        public virtual TwinCATCoil1 TUnloadCompleted { set; get; }

        public virtual TwinCATCoil1 ResetCMDComplete { set; get; }
        public virtual TwinCATCoil1 ResetCMD { set; get; }

        public virtual TwinCATCoil1 PLCUnload { set; get; }
        public virtual TwinCATCoil1 WaitPLCUnload { set; get; }
        #endregion
        #region 变量定义区域
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private string TwincatParameterPath = System.Environment.CurrentDirectory + "\\TwincatParameter.ini";
        private string iniTesterResutPath = System.Environment.CurrentDirectory + "\\TesterResut.ini";
        private XinjiePlc XinjiePLC;
        private HdevEngine hdevEngine = new HdevEngine();
        //private HdevEngine hdevScanEngine = new HdevEngine();
        private EpsonRC90 epsonRC90 = new EpsonRC90();
        private bool NeedNoiseReduce = false;
        private bool NeedLoadMaters = false;
        private bool NeedUnloadMaters = false;
        //private string PreFeedFillStr = "FeedFill;0;0;0;0;0;0;";
        Queue<TestRecord> myTestRecordQueue = new Queue<TestRecord>();
        public static DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool PLCNeedContinue = false;
        private DateTimeUtility.SYSTEMTIME lastchuiqi = new DateTimeUtility.SYSTEMTIME();

        TwinCATAds _TwinCATAds = new TwinCATAds();
        double DebugTargetX = 0;
        double DebugTargetY = 0;
        double DebugTargetF = 0;
        double DebugTargetT = 0;
        ushort fti = 1;

        #endregion
        #region 构造函数
        public MainDataContext()
        {
            epsonRC90.ModelPrint += ModelPrintEventProcess;
            epsonRC90.EPSONCommTwincat += EPSONCommTwincatEventProcess;
            epsonRC90.EpsonStatusUpdate += EpsonStatusUpdateProcess;
            epsonRC90.ScanUpdate += ScanUpdateProcess;
            epsonRC90.ScanP3Update += ScanP3UpdateProcess;
            epsonRC90.ScanP3Update1 += ScanP3Update1Process;
            epsonRC90.TestFinished += StartUpdateProcess;
            dispatcherTimer.Tick += new EventHandler(DispatcherTimerTickUpdateUi);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            TwinCatVarInit();


            Async.RunFuncAsync(UpdateUI,null);
        }
        private void TwinCatVarInit()
        {
            XPos = new TwinCATCoil1(new TwinCATCoil("MAIN.XPos", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            YPos = new TwinCATCoil1(new TwinCATCoil("MAIN.YPos", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FPos = new TwinCATCoil1(new TwinCATCoil("MAIN.FPos", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TPos = new TwinCATCoil1(new TwinCATCoil("MAIN.TPos", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);

            PickPositionX = new TwinCATCoil1(new TwinCATCoil("MAIN.PickPositionX", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            PickPositionY = new TwinCATCoil1(new TwinCATCoil("MAIN.PickPositionY", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);

            WaitPositionX = new TwinCATCoil1(new TwinCATCoil("MAIN.WaitPositionX", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            WaitPositionY = new TwinCATCoil1(new TwinCATCoil("MAIN.WaitPositionY", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);

            ReleasePositionX1 = new TwinCATCoil1(new TwinCATCoil("MAIN.ReleasePositionX1", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ReleasePositionY1 = new TwinCATCoil1(new TwinCATCoil("MAIN.ReleasePositionY1", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ReleasePositionX2 = new TwinCATCoil1(new TwinCATCoil("MAIN.ReleasePositionX2", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ReleasePositionY2 = new TwinCATCoil1(new TwinCATCoil("MAIN.ReleasePositionY2", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ReleasePositionX3 = new TwinCATCoil1(new TwinCATCoil("MAIN.ReleasePositionX3", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ReleasePositionY3 = new TwinCATCoil1(new TwinCATCoil("MAIN.ReleasePositionY3", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);

            PowerOn1 = new TwinCATCoil1(new TwinCATCoil("MAIN.PowerOn1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            PowerOn2 = new TwinCATCoil1(new TwinCATCoil("MAIN.PowerOn2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            PowerOn3 = new TwinCATCoil1(new TwinCATCoil("MAIN.PowerOn3", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            PowerOn4 = new TwinCATCoil1(new TwinCATCoil("MAIN.PowerOn4", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            ServoRst1 = new TwinCATCoil1(new TwinCATCoil("MAIN.E2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ServoRst2 = new TwinCATCoil1(new TwinCATCoil("MAIN.F2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ServoRst3 = new TwinCATCoil1(new TwinCATCoil("MAIN.G2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ServoRst4 = new TwinCATCoil1(new TwinCATCoil("MAIN.H2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            ServoSVN1 = new TwinCATCoil1(new TwinCATCoil("MAIN.E1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ServoSVN2 = new TwinCATCoil1(new TwinCATCoil("MAIN.F1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ServoSVN3 = new TwinCATCoil1(new TwinCATCoil("MAIN.G1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            ServoSVN4 = new TwinCATCoil1(new TwinCATCoil("MAIN.H1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            XRDY = new TwinCATCoil1(new TwinCATCoil("MAIN.XRDY", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            YRDY = new TwinCATCoil1(new TwinCATCoil("MAIN.YRDY", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FRDY = new TwinCATCoil1(new TwinCATCoil("MAIN.FRDY", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TRDY = new TwinCATCoil1(new TwinCATCoil("MAIN.TRDY", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            XYInDebug = new TwinCATCoil1(new TwinCATCoil("MAIN.XYInDebug", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FInDebug = new TwinCATCoil1(new TwinCATCoil("MAIN.FInDebug", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TInDebug = new TwinCATCoil1(new TwinCATCoil("MAIN.TInDebug", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            EF104 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF104", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            EF114 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF114", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            EF100 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF100", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            EF101 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF101", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            EF102 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF102", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            EF110 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF110", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            EF111 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF111", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            EF112 = new TwinCATCoil1(new TwinCATCoil("MAIN.EF112", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            DebugXTargetPositon = new TwinCATCoil1(new TwinCATCoil("MAIN.DebugXTargetPositon", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            DebugYTargetPositon = new TwinCATCoil1(new TwinCATCoil("MAIN.DebugYTargetPositon", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            DebugFTargetPositon = new TwinCATCoil1(new TwinCATCoil("MAIN.DebugFTargetPositon", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            DebugTTargetPositon = new TwinCATCoil1(new TwinCATCoil("MAIN.DebugTTargetPositon", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);

            Calc_Start = new TwinCATCoil1(new TwinCATCoil("MAIN.Calc_Start", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            FPosition1 = new TwinCATCoil1(new TwinCATCoil("MAIN.FPosition1", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FPosition2 = new TwinCATCoil1(new TwinCATCoil("MAIN.FPosition2", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FPosition3 = new TwinCATCoil1(new TwinCATCoil("MAIN.FPosition3", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FPosition4 = new TwinCATCoil1(new TwinCATCoil("MAIN.FPosition4", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FPosition5 = new TwinCATCoil1(new TwinCATCoil("MAIN.FPosition5", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FPosition6 = new TwinCATCoil1(new TwinCATCoil("MAIN.FPosition6", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FPosition7 = new TwinCATCoil1(new TwinCATCoil("MAIN.FPosition7", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);

            TPosition1 = new TwinCATCoil1(new TwinCATCoil("MAIN.TPosition1", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TPosition2 = new TwinCATCoil1(new TwinCATCoil("MAIN.TPosition2", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TPosition3 = new TwinCATCoil1(new TwinCATCoil("MAIN.TPosition3", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TPosition4 = new TwinCATCoil1(new TwinCATCoil("MAIN.TPosition4", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TPosition5 = new TwinCATCoil1(new TwinCATCoil("MAIN.TPosition5", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TPosition6 = new TwinCATCoil1(new TwinCATCoil("MAIN.TPosition6", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TPosition7 = new TwinCATCoil1(new TwinCATCoil("MAIN.TPosition7", typeof(double), TwinCATCoil.Mode.Notice), _TwinCATAds);

            F200 = new TwinCATCoil1(new TwinCATCoil("MAIN.F200", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            F201 = new TwinCATCoil1(new TwinCATCoil("MAIN.F201", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            F202 = new TwinCATCoil1(new TwinCATCoil("MAIN.F202", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            F204 = new TwinCATCoil1(new TwinCATCoil("MAIN.F204", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            T200 = new TwinCATCoil1(new TwinCATCoil("MAIN.T200", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            T201 = new TwinCATCoil1(new TwinCATCoil("MAIN.T201", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            T202 = new TwinCATCoil1(new TwinCATCoil("MAIN.T202", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            T204 = new TwinCATCoil1(new TwinCATCoil("MAIN.T204", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            FMoveCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.FMoveCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FMoveCompleted = new TwinCATCoil1(new TwinCATCoil("MAIN.FMoveCompleted", typeof(bool), TwinCATCoil.Mode.Notice, 1), _TwinCATAds);
            FCmdIndex = new TwinCATCoil1(new TwinCATCoil("MAIN.FCmdIndex", typeof(ushort), TwinCATCoil.Mode.Notice, 1), _TwinCATAds);

            TMoveCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.TMoveCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TMoveCompleted = new TwinCATCoil1(new TwinCATCoil("MAIN.TMoveCompleted", typeof(bool), TwinCATCoil.Mode.Notice, 1), _TwinCATAds);
            TCmdIndex = new TwinCATCoil1(new TwinCATCoil("MAIN.TCmdIndex", typeof(ushort), TwinCATCoil.Mode.Notice, 1), _TwinCATAds);

            TUnloadCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.TUnloadCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TUnloadCompleted = new TwinCATCoil1(new TwinCATCoil("MAIN.TUnloadCompleted", typeof(bool), TwinCATCoil.Mode.Notice,1), _TwinCATAds);

            ResetCMDComplete = new TwinCATCoil1(new TwinCATCoil("MAIN.ResetCMDComplete", typeof(bool), TwinCATCoil.Mode.Notice,1), _TwinCATAds);
            ResetCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.ResetCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            PLCUnload = new TwinCATCoil1(new TwinCATCoil("MAIN.PLCUnload", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            WaitPLCUnload = new TwinCATCoil1(new TwinCATCoil("MAIN.WaitPLCUnload", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            _TwinCATAds.StartNotice();
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
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
        }
        public void ChoseAboutPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
        }
        public void ChoseParameterPage()
        {
            ParameterPageVisibility = "Visible";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
        }
        public void ChoseOperaterActionPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Visible";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
        }
        public void ChoseCameraPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
        }
        public void ChoseCameraHcPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Visible";
            ScanCameraPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
        }
        //public void ChoseScanCameraPage()
        //{
        //    ParameterPageVisibility = "Collapsed";
        //    AboutPageVisibility = "Collapsed";
        //    HomePageVisibility = "Collapsed";
        //    CameraHcPageVisibility = "Collapsed";
        //    ScanCameraPageVisibility = "Visible";
        //    OperaterActionPageVisibility = "Collapsed";
        //    BarcodeDisplayPageVisibility = "Collapsed";
        //    TestRecordPageVisibility = "Collapsed";
        //}
        public void ChoseTwincatNcPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Visible";
            OperaterActionPageVisibility = "Collapsed";
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
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Visible";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
        }
        public void ChoseTestRecordPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanCameraPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Visible";
            TwincatNcPageVisibility = "Collapsed";
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
                        if (SingleTestMode)
                        {
                            SingleTestTimesVisibility = "Visible";
                            string str = "SingleTestModeStageNum;" + SingleTestModeStageNum.ToString();
                            if (epsonRC90.TestSendStatus)
                            {
                                await epsonRC90.TestSentNet.SendAsync(str);
                                Msg = messagePrint.AddMessage(str);
                            }
                            await epsonRC90.CtrlNet.SendAsync("$start,1");
                            SingleTestTimes = 0;
                            Msg = messagePrint.AddMessage("单穴反复测试模式");
                        }
                        else
                        {
                            SingleTestTimesVisibility = "Collapsed";
                            await epsonRC90.CtrlNet.SendAsync("$start,2");
                            Msg = messagePrint.AddMessage("正常模式");
                            AllowSampleTestCommand = true;
                        }

                        
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
                case "3":
                    dlg.Filter = "视觉文件(*.hdev)|*.hdev|所有文件(*.*)|*.*";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        ScanVisionScriptFileNameP3 = dlg.FileName;
                        Inifile.INIWriteValue(iniParameterPath, "Camera", "ScanVisionScriptFileNameP3", ScanVisionScriptFileNameP3);
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
            int num = 0;
            if (TestCheckedAL)
            {
                str += "1;";
                num++;
            }
            else
            {
                str += "0;";
                
            }

            if (TestCheckedAR)
            {
                str += "1;";
                num++;
            }
            else
            {
                str += "0;";
            }

            if (TestCheckedBL)
            {
                str += "1;";
                num++;
            }
            else
            {
                str += "0;";
            }

            if (TestCheckedBR)
            {
                str += "1;";
                num++;
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

            str = "BarcodeMode;" + BarcodeMode.ToString();
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync(str);
                Msg = messagePrint.AddMessage(str);
            }
            Inifile.INIWriteValue(iniParameterPath, "BarcodeMode", "BarcodeMode", BarcodeMode.ToString());

            if (num < 2)
            {
                AABReTest = false;
            }
            str = "AABReTest;" + AABReTest.ToString();
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync(str);
                Msg = messagePrint.AddMessage(str);
            }
            Inifile.INIWriteValue(iniParameterPath, "ReTest", "AABReTest", AABReTest.ToString());
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
        public async void LoginAction()
        {
            List<string> r;
            if (isLogin == false)
            {

                r = await mydialog.showlogin();
                if (r[0] == LoginUserName && r[1] == LoginPassword)
                {
                    isLogin = !isLogin;
                }

            }
            else
            {
                isLogin = !isLogin;
            }

            
            if (isLogin == true)
            {
                LoginButtonString = "登出";
            }
            else
            {
                LoginButtonString = "登录";
            }
        }
        public async void XQTActionFunction(object p)
        {
            string s = p.ToString();
            switch (s)
            {
                case "1":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("XQTAction;1");
                    }
                    break;
                case "2":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("XQTAction;2");
                    }
                    break;
                case "3":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("XQTAction;3");
                    }
                    break;
                case "4":
                    if (epsonRC90.TestSendStatus && IsTestersClean && AllowSampleTestCommand)
                    {
                        await epsonRC90.TestSentNet.SendAsync("TestersCleanAction");
                        AllowSampleTestCommand = false;
                    }
                    break;
                default:
                    break;
            }
        }
        private void SaveLastSamplTimetoIni()
        {
            try
            {
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wDay", lastchuiqi.wDay.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wDayOfWeek", lastchuiqi.wDayOfWeek.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wHour", lastchuiqi.wHour.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wMilliseconds", lastchuiqi.wMilliseconds.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wMinute", lastchuiqi.wMinute.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wMonth", lastchuiqi.wMonth.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wSecond", lastchuiqi.wSecond.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "wYear", lastchuiqi.wYear.ToString());

            }
            catch (Exception ex)
            {

                Log.Default.Error("SaveLastSamplTimetoIni", ex);
            }
        }
        public void SaveTwincatDateAction()
        {
            try
            {
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionX1", ReleasePositionX1.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionY1", ReleasePositionY1.Value.ToString());
                Msg = messagePrint.AddMessage("保存轴控参数完成");
            }
            catch 
            {

                Msg = messagePrint.AddMessage("保存轴控参数失败");
            }
        }
        #endregion
        #region BECKHOFF
        public void ServoResetAction(object p)
        {
            try
            {
                switch (p.ToString())
                {
                    case "1":
                        ServoRst1.Value = true;
                        break;
                    case "2":
                        ServoRst2.Value = true;
                        break;
                    case "3":
                        ServoRst3.Value = true;
                        break;
                    case "4":
                        ServoRst4.Value = true;
                        break;
                    default:
                        break;
                }
            }
            catch
            {

                
            }
            
        }
        public void ServoONAction(object p)
        {
            try
            {
                switch (p.ToString())
                {
                    case "1":
                        ServoSVN1.Value = !(bool)ServoSVN1.Value;
                        break;
                    case "2":
                        ServoSVN2.Value = !(bool)ServoSVN2.Value;
                        break;
                    case "3":
                        ServoSVN3.Value = !(bool)ServoSVN3.Value;
                        break;
                    case "4":
                        ServoSVN4.Value = !(bool)ServoSVN4.Value;
                        break;
                    default:
                        break;
                }
            }
            catch 
            {

                
            }
            
        }
        public void ServoHomeAction(object p)
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    switch (p.ToString())
                    {
                        case "1":
                            EF104.Value = true;
                            break;
                        case "2":
                            EF114.Value = true;
                            break;
                        //case "3":
                        //    ServoSVN3.Value = !(bool)ServoSVN3.Value;
                        //    break;
                        //case "4":
                        //    ServoSVN4.Value = !(bool)ServoSVN4.Value;
                            //break;
                        default:
                            break;
                    }
                }
                if ((bool)FInDebug.Value)
                {
                    switch (p.ToString())
                    {
                        case "3":
                            F204.Value = true;
                            break;
                        default:
                            break;
                    }
                }
                if ((bool)TInDebug.Value)
                {
                    switch (p.ToString())
                    {
                        case "4":
                            T204.Value = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch 
            {

                
            }

            
        }
        public void JogActionX_Plus()
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    EF100.Value = true;
                }
            }
            catch
            {

                
            }
        }
        public void JogActionX_Minus()
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    EF101.Value = true;
                }
            }
            catch
            {


            }
        }
        public void JogActionX_Stop()
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    EF100.Value = false;
                    EF101.Value = false;
                }
            }
            catch
            {


            }
        }
        public void JogActionY_Plus()
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    EF110.Value = true;
                }
            }
            catch
            {


            }
        }
        public void JogActionY_Minus()
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    EF111.Value = true;
                }
            }
            catch
            {


            }
        }
        public void JogActionY_Stop()
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    EF110.Value = false;
                    EF111.Value = false;
                }
            }
            catch
            {


            }
        }
        private void ServoPTPXY()
        {
            DebugXTargetPositon.Value = DebugTargetX;
            DebugYTargetPositon.Value = DebugTargetY;
            EF102.Value = true;
            EF112.Value = true;

        }
        public void JogActionF_Plus()
        {
            try
            {
                if ((bool)FInDebug.Value)
                {
                    F200.Value = true;
                }
            }
            catch
            {


            }
        }
        public void JogActionF_Minus()
        {
            try
            {
                if ((bool)FInDebug.Value)
                {
                    F201.Value = true;
                }
            }
            catch
            {


            }
        }
        public void JogActionF_Stop()
        {
            try
            {
                if ((bool)FInDebug.Value)
                {
                    F200.Value = false;
                    F201.Value = false;
                }
            }
            catch
            {


            }
        }
        private void ServoPTPF()
        {
            DebugFTargetPositon.Value = DebugTargetF;
            
            F202.Value = true;
            

        }

        public void JogActionT_Plus()
        {
            try
            {
                if ((bool)TInDebug.Value)
                {
                    T200.Value = true;
                }
            }
            catch
            {


            }
        }
        public void JogActionT_Minus()
        {
            try
            {
                if ((bool)TInDebug.Value)
                {
                    T201.Value = true;
                }
            }
            catch
            {


            }
        }
        public void JogActionT_Stop()
        {
            try
            {
                if ((bool)TInDebug.Value)
                {
                    T200.Value = false;
                    T201.Value = false;
                }
            }
            catch
            {


            }
        }
        private void ServoPTPT()
        {
            DebugTTargetPositon.Value = DebugTargetT;

            T202.Value = true;


        }

        public void MovetoPointAction(object p)
        {
            try
            {
                if ((bool)XYInDebug.Value)
                {
                    switch (p.ToString())
                    {
                        case "1":
                            DebugTargetX = (double)ReleasePositionX1.Value;
                            DebugTargetY = (double)ReleasePositionY1.Value;
                            ServoPTPXY();
                            break;
                        case "2":
                            DebugTargetX = (double)ReleasePositionX2.Value;
                            DebugTargetY = (double)ReleasePositionY2.Value;
                            ServoPTPXY();
                            break;
                        case "3":
                            DebugTargetX = (double)ReleasePositionX3.Value;
                            DebugTargetY = (double)ReleasePositionY3.Value;
                            ServoPTPXY();
                            break;
                        case "4":
                            DebugTargetX = (double)PickPositionX.Value;
                            DebugTargetY = (double)PickPositionY.Value;
                            ServoPTPXY();
                            break;
                        case "5":
                            DebugTargetX = (double)WaitPositionX.Value;
                            DebugTargetY = (double)WaitPositionY.Value;
                            ServoPTPXY();
                            break;
                        default:
                            break;
                    }
                }
                if ((bool)FInDebug.Value)
                {
                    switch (p.ToString())
                    {
                        case "6":
                            DebugTargetF = (double)FPosition1.Value;                            
                            ServoPTPF();
                            break;
                        case "7":
                            DebugTargetF = (double)FPosition2.Value;
                            ServoPTPF();
                            break;
                        case "8":
                            DebugTargetF = (double)FPosition3.Value;
                            ServoPTPF();
                            break;
                        case "9":
                            DebugTargetF = (double)FPosition4.Value;
                            ServoPTPF();
                            break;
                        case "10":
                            DebugTargetF = (double)FPosition5.Value;
                            ServoPTPF();
                            break;
                        case "11":
                            DebugTargetF = (double)FPosition6.Value;
                            ServoPTPF();
                            break;
                        case "12":
                            DebugTargetF = (double)FPosition7.Value;
                            ServoPTPF();
                            break;
                        default:
                            break;
                    }
                }

                if ((bool)TInDebug.Value)
                {
                    switch (p.ToString())
                    {
                        case "13":
                            DebugTargetT = (double)TPosition1.Value;
                            ServoPTPT();
                            break;
                        case "14":
                            DebugTargetT = (double)TPosition2.Value;
                            ServoPTPT();
                            break;
                        case "15":
                            DebugTargetT = (double)TPosition3.Value;
                            ServoPTPT();
                            break;
                        case "16":
                            DebugTargetT = (double)TPosition4.Value;
                            ServoPTPT();
                            break;
                        case "17":
                            DebugTargetT = (double)TPosition5.Value;
                            ServoPTPT();
                            break;
                       
                        default:
                            break;
                    }
                }
            }
            catch
            {


            }
        }
        public void GetCoord(object p)
        {
            try
            {
                switch (p.ToString())
                {
                    case "1":
                        ReleasePositionX1.Value = (double)XPos.Value;
                        ReleasePositionY1.Value = (double)YPos.Value;
                        Calc_Start.Value = true;
                        break;
                    case "2":
                        ReleasePositionX2.Value = (double)XPos.Value;
                        ReleasePositionY2.Value = (double)YPos.Value;
                        Calc_Start.Value = true;
                        break;
                    case "3":
                        ReleasePositionX3.Value = (double)XPos.Value;
                        ReleasePositionY3.Value = (double)YPos.Value;
                        Calc_Start.Value = true;
                        break;
                    case "4":
                        PickPositionX.Value = (double)XPos.Value;
                        PickPositionY.Value = (double)YPos.Value;
                        Calc_Start.Value = true;
                        break;
                    case "5":
                        WaitPositionX.Value = (double)XPos.Value;
                        WaitPositionY.Value = (double)YPos.Value;
                        Calc_Start.Value = true;
                        break;
                    case "6":
                        FPosition1.Value = (double)FPos.Value;
                        break;
                    case "7":
                        FPosition2.Value = (double)FPos.Value;
                        break;
                    case "8":
                        FPosition3.Value = (double)FPos.Value;
                        break;
                    case "9":
                        FPosition4.Value = (double)FPos.Value;
                        break;
                    case "10":
                        FPosition5.Value = (double)FPos.Value;
                        break;
                    case "11":
                        FPosition6.Value = (double)FPos.Value;
                        break;
                    case "12":
                        FPosition7.Value = (double)FPos.Value;
                        break;
                    case "13":
                        TPosition1.Value = (double)TPos.Value;
                        break;
                    case "14":
                        TPosition2.Value = (double)TPos.Value;
                        break;
                    case "15":
                        TPosition3.Value = (double)TPos.Value;
                        break;
                    case "16":
                        TPosition4.Value = (double)TPos.Value;
                        break;
                    case "17":
                        TPosition5.Value = (double)TPos.Value;
                        break;
                    default:
                        break;
                }
            }
            catch
            {

                
            }
        }
        public delegate void TwinCatProcessedDelegate(string s);
        public async void FMoveProcessStart(TwinCatProcessedDelegate callback,string s)
        {
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    FCmdIndex.Value = ushort.Parse(s);
                    await Task.Delay(100);
                    FMoveCMD.Value = true;
                    FMoveCompleted.Value = false;
                    
                    while (!(bool)FMoveCompleted.Value)
                    {
                        await Task.Delay(100);
                    }
                    callback("FMOVE;" + s);
                }
                );
            };
            await startTask();
        }
        public delegate void PLCUnLoadProcessedDelegate();
        public async void PLCUnLoadProcessStart(PLCUnLoadProcessedDelegate callback)
        {
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    
                    while (!XinjiePLC.readM(420))
                    {
                        await Task.Delay(100);
                    }
                    callback();
                }
                );
            };
            await Task.Delay(1000);
            await startTask();
        }
        public async void TMoveProcessStart(TwinCatProcessedDelegate callback, string s)
        {
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    TCmdIndex.Value = ushort.Parse(s);
                    await Task.Delay(100);
                    TMoveCMD.Value = true;
                    TMoveCompleted.Value = false;
                    
                    while (!(bool)TMoveCompleted.Value)
                    {
                        await Task.Delay(100);
                    }
                    callback("TMOVE;" + s);
                }
                );
            };
            await startTask();
        }
        public async void ULoadProcessStart(TwinCatProcessedDelegate callback)
        {
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    TUnloadCMD.Value = true;
                    TUnloadCompleted.Value = false;
                   
                    while (!(bool)TUnloadCompleted.Value)
                    {
                        await Task.Delay(100);
                    }
                    callback("ULOAD");
                }
                );
            };
            await startTask();
        }
        public async void ResetCMDProcessStart(TwinCatProcessedDelegate callback)
        {
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    ResetCMD.Value = true;
                    

                    while (!(bool)ResetCMDComplete.Value)
                    {
                        await Task.Delay(100);
                    }
                    callback("ResetCMD");
                }
                );
            };
            await startTask();
        }
        public async void TwinCatProcessStartCallback(string str)
        {
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync(str);
            }
        }
        #endregion
        #region 事件相应函数
        private void EPSONCommTwincatEventProcess(string str)
        {
            string[] strs = str.Split(',');
            switch (strs[0])
            {
                case "FMOVE":
                    FMoveProcessStart(TwinCatProcessStartCallback, strs[1]);
                    break;
                case "TMOVE":
                    TMoveProcessStart(TwinCatProcessStartCallback, strs[1]);
                    break;
                case "ULOAD":
                    ULoadProcessStart(TwinCatProcessStartCallback);
                    break;
                case "ResetCMD":
                    ResetCMDProcessStart(TwinCatProcessStartCallback);
                    break;
                default:
                    break;
            }
        }
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
                //case "MsgRev: 蚀刻不良":
                //    ShowAlarmTextGrid("蚀刻不良");
                //    break;
                //case "MsgRev: 扫码不良":
                //    ShowAlarmTextGrid("扫码不良");
                //    break;
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
                case "MsgRev: 单穴测试，一次完成":
                    SingleTestTimes++;
                    break;
                case "MsgRev: 测试工位1，产品没放好":
                    ShowAlarmTextGrid("测试工位1，产品没放好");
                    break;
                case "MsgRev: 测试工位2，产品没放好":
                    ShowAlarmTextGrid("测试工位2，产品没放好");
                    break;
                case "MsgRev: 测试工位3，产品没放好":
                    ShowAlarmTextGrid("测试工位3，产品没放好");
                    break;
                case "MsgRev: 测试工位4，产品没放好":
                    ShowAlarmTextGrid("测试工位4，产品没放好");
                    break;
                //case "MsgRev: 测试工位1，B爪手掉料":
                //    ShowAlarmTextGrid("测试工位1，B爪手掉料");
                //    break;
                //case "MsgRev: 测试工位2，B爪手掉料":
                //    ShowAlarmTextGrid("测试工位2，B爪手掉料");
                //    break;
                //case "MsgRev: 测试工位3，B爪手掉料":
                //    ShowAlarmTextGrid("测试工位3，B爪手掉料");
                //    break;
                //case "MsgRev: 测试工位4，B爪手掉料":
                //    ShowAlarmTextGrid("测试工位4，B爪手掉料");
                //    break;
                case "MsgRev: A爪手掉料":
                    ShowAlarmTextGrid("A爪手掉料");
                    break;
                case "MsgRev: B爪手掉料":
                    ShowAlarmTextGrid("B爪手掉料");
                    break;
                case "MsgRev: 清洁操作，结束":
                    DateTimeUtility.GetLocalTime(ref lastchuiqi);
                    LastChuiqiTimeStr = lastchuiqi.ToDateTime().ToString();
                    SaveLastSamplTimetoIni();
                    AllowSampleTestCommand = true;
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
        private void ScanP3UpdateProcess(string bar, HImage img, HObject hObject)
        {
            //ObservableCollection<HObject> objectList = new ObservableCollection<HObject>();
            hImageScan = img;
            BarcodeDisplay = bar;
            //objectList.Add(hObject);
            //hObjectListScan = objectList;
        }
        private void ScanP3Update1Process(string bar)
        {
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
        //public void ScanCameraInspect()
        //{
        //    Async.RunFuncAsync(epsonRC90.scanCameraInspect, null);
        //}
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
                ScanVisionScriptFileNameP3 = Inifile.INIGetStringValue(iniParameterPath, "Camera", "ScanVisionScriptFileNameP3", @"C:\test.hdev");
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
                PickBracodeB = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "PickBracodeB", "Null");
                TesterBracodeAL = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeAL", "Null");
                TesterBracodeAR = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeAR", "Null");
                TesterBracodeBL = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeBL", "Null");
                TesterBracodeBR = Inifile.INIGetStringValue(iniParameterPath, "Barcode", "TesterBracodeBR", "Null");
                TestRecordSavePath = Inifile.INIGetStringValue(iniParameterPath, "SavePath", "TestRecordSavePath", "C:\\");
                AlarmSavePath = Inifile.INIGetStringValue(iniParameterPath, "SavePath", "AlarmSavePath", "C:\\");
                NGContinueNum = int.Parse(Inifile.INIGetStringValue(iniParameterPath, "Tester", "NGContinueNum", "4"));
                BarcodeMode = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "BarcodeMode", "BarcodeMode", "True"));
                AABReTest = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "ReTest", "AABReTest", "False"));

                IsTestersClean = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "IsTestersClean", "False"));

                lastchuiqi.wDay = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wDay", "13"));
                lastchuiqi.wDayOfWeek = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wDayOfWeek", "0"));
                lastchuiqi.wHour = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wHour", "17"));
                lastchuiqi.wMilliseconds = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wMilliseconds", "273"));
                lastchuiqi.wMinute = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wMinute", "5"));
                lastchuiqi.wMonth = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wMonth", "11"));
                lastchuiqi.wSecond = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wSecond", "55"));
                lastchuiqi.wYear = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wYear", "2016"));
                LastChuiqiTimeStr = lastchuiqi.ToDateTime().ToString();

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
                Inifile.INIWriteValue(iniParameterPath, "BarcodeMode", "BarcodeMode", BarcodeMode.ToString());
                Inifile.INIWriteValue(iniParameterPath, "ReTest", "AABReTest", AABReTest.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Chuiqi", "IsTestersClean", IsTestersClean.ToString());
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
        public delegate void FuncTProcessedDelegate();
        public async void FuncTStart(FuncTProcessedDelegate callback)
        {
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    if (fti > 7)
                    {
                        fti = 1;
                    }
                    FCmdIndex.Value = fti++;
                    FMoveCMD.Value = true;
                    FMoveCompleted.Value = false;
                    while (!(bool)FMoveCompleted.Value)
                    {
                        await Task.Delay(100);
                    }
                    callback();
                }
                );
            };
            await startTask();
        }
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
            FuncTStart(FuncPrint);
        }
        public void FuncPrint()
        {
            Msg = messagePrint.AddMessage((fti-1).ToString() + " 完成");
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
                PickBracodeB = epsonRC90.PickBracodeB;
                //TesterBracodeAL = epsonRC90.TesterBracodeAL;
                //TesterBracodeAR = epsonRC90.TesterBracodeAR;
                //TesterBracodeBL = epsonRC90.TesterBracodeBL;
                //TesterBracodeBR = epsonRC90.TesterBracodeBR;

                try
                {
                    ServoHomed1 = (bool)XRDY.Value;
                    ServoHomed2 = (bool)YRDY.Value;
                    ServoHomed3 = (bool)FRDY.Value;
                    ServoHomed4 = (bool)TRDY.Value;
                }
                catch 
                {

                    
                }
                
            }
        }
        private async void DispatcherTimerTickUpdateUi(Object sender, EventArgs e)
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

            try
            {
                if (IsTestersClean && AllowSampleTestCommand)
                {
                    DateTimeUtility.SYSTEMTIME ds1 = new DateTimeUtility.SYSTEMTIME();
                    DateTimeUtility.GetLocalTime(ref ds1);
                    TimeSpan ts1 = ds1.ToDateTime() - lastchuiqi.ToDateTime();
                    if (ts1.TotalHours > 2)
                    {
                        if (IsTestersClean)
                        {
                            if (epsonRC90.TestSendStatus)
                            {
                                await epsonRC90.TestSentNet.SendAsync("TestersCleanAction");
                                AllowSampleTestCommand = false;
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Default.Error("DateTimeUtility.GetLocalTime(ref ds1)", ex);
            }
        }
        #endregion
        #region 导入导出
        //[Export(MEF.Contracts.ActionMessage)]
        //[ExportMetadata(MEF.Key, "winclose")]
        //public async void WindowClose()
        //{
        //    mydialog.changeaccent("Red");
            
        //    var r = await mydialog.showconfirm("确定要关闭程序吗？");
        //    if (r)
        //    {
        //        //epsonRC90.TestSentNet.client.Close();
        //        //epsonRC90.TestReceiveNet.client.Close();
        //        //epsonRC90.MsgReceiveNet.client.Close();
        //        //epsonRC90.CtrlNet.client.Close();
        //        System.Windows.Application.Current.Shutdown();
        //    }
        //    else
        //    {
        //        mydialog.changeaccent("Cobalt");
        //    }
        //}
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
            //epsonRC90.scanCameraInit();
            //await Task.Delay(100);
            //ScanCameraInspect();
            //scanCameraInit();
            //Msg = messagePrint.AddMessage("扫码相机初始化完成");
        }
        [Initialize]
        public async void L91PLCWork()
        {
            bool TakePhoteFlage = false, _TakePhoteFlage = false;
            bool _IsShieldTheDoor = false;



            bool _PLCUnload = false;

            while (true)
            {
                //414,460
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
                        IsPLCConnect = XinjiePLC.readM(36864);
                    }
                }
                else
                {
                    IsPLCConnect = XinjiePLC.readM(36864);
                    //拍照
                    TakePhoteFlage = XinjiePLC.readM(406);
                    if (_TakePhoteFlage != TakePhoteFlage)
                    {
                        _TakePhoteFlage = TakePhoteFlage;
                        if (TakePhoteFlage == true)
                        {
                            XinjiePLC.setM(406, false);
                            XinjiePLC.setM(407, false);
                            Async.RunFuncAsync(cameraHcInspect, PLCTakePhoteCallback);
                        }
                    }
                    //安全门
                    if (_IsShieldTheDoor != IsShieldTheDoor)
                    {
                        _IsShieldTheDoor = IsShieldTheDoor;
                        if (IsShieldTheDoor)
                        {
                            XinjiePLC.setM(1200, true);
                        }
                        else
                        {
                            XinjiePLC.setM(1200, false);
                        }
                    }
                    if (NeedNoiseReduce)
                    {
                        NeedNoiseReduce = false;
                        XinjiePLC.setM(1301, true);
                    }

                    try
                    {
                        if (_PLCUnload != (bool)PLCUnload.Value)
                        {
                            _PLCUnload = (bool)PLCUnload.Value;
                            if ((bool)PLCUnload.Value)
                            {
                                XinjiePLC.setM(901, true);
                                PLCUnLoadProcessStart(PLCUnLoadCallback);
                            }
                        }
                    }
                    catch 
                    {

                       
                    }






                }

            }
        }
        private void PLCUnLoadCallback()
        {
            WaitPLCUnload.Value = false;
        }
        private async void PLCTakePhoteCallback()
        {
            string str = "FeedFill";
            if (FindFill1)
            {
                XinjiePLC.setM(400, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill2)
            {
                XinjiePLC.setM(401, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill3)
            {
                XinjiePLC.setM(402, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill4)
            {
                XinjiePLC.setM(403, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill5)
            {
                XinjiePLC.setM(404, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            if (FindFill6)
            {
                XinjiePLC.setM(405, true);
                str += ";1";
            }
            else
            {
                str += ";0";
            }
            XinjiePLC.setM(407, true);
            await Task.Delay(1);
            Msg = messagePrint.AddMessage(str);
            //if (epsonRC90.TestSendStatus)
            //{
            //    await epsonRC90.TestSentNet.SendAsync(PreFeedFillStr);
            //}
            //PreFeedFillStr = str;
        }
        #endregion

    }
}