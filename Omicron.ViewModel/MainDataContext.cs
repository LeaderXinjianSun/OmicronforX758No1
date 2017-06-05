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
using 臻鼎科技OraDB;
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
        public virtual string ScanPageVisibility { set; get; } = "Collapsed";
        public virtual string SampleTestPageVisibility { set; get; } = "Collapsed";
        public virtual string SampleTestPage1Visibility { set; get; } = "Collapsed";
        public virtual string AlarmRecordPageVisibility { set; get; } = "Collapsed";
        public virtual string HelpPageVisibility { set; get; } = "Collapsed";
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

        public virtual string BarcodeDisplay { set; get; } = "C4671110MYFGXCLBJ";

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
        public virtual ObservableCollection<AlarmRecord> alarmRecord { set; get; } = new ObservableCollection<AlarmRecord>();
        public virtual ObservableCollection<AlarmTableItem> alarmTableItems { set; get; } = new ObservableCollection<AlarmTableItem>();


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


        public virtual int TestCount0_Nomal { set; get; } = 0;
        public virtual int PassCount0_Nomal { set; get; } = 0;
        public virtual int FailCount0_Nomal { set; get; } = 0;
        public virtual double Yield0_Nomal { set; get; } = 0;


        public virtual int TestCount1_Nomal { set; get; } = 0;
        public virtual int PassCount1_Nomal { set; get; } = 0;
        public virtual int FailCount1_Nomal { set; get; } = 0;
        public virtual double Yield1_Nomal { set; get; } = 0;


        public virtual int TestCount2_Nomal { set; get; } = 0;
        public virtual int PassCount2_Nomal { set; get; } = 0;
        public virtual int FailCount2_Nomal { set; get; } = 0;
        public virtual double Yield2_Nomal { set; get; } = 0;


        public virtual int TestCount3_Nomal { set; get; } = 0;
        public virtual int PassCount3_Nomal { set; get; } = 0;
        public virtual int FailCount3_Nomal { set; get; } = 0;
        public virtual double Yield3_Nomal { set; get; } = 0;

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

        public virtual bool isGRRMode { set; get; } = false;

        public virtual bool AABReTest { set; get; } = false;

        public virtual int SingleTestTimes { set; get; } = 0;
        public virtual string SingleTestTimesVisibility { set; get; } = "Collapsed";
        public virtual string LoginButtonString { set; get; } = "登录";
        public virtual string LoginUserName { set; get; } = "Leader";
        public virtual string LoginPassword { set; get; } = "jsldr";
        public virtual bool isLogin { set; get; } = false;
        public virtual bool BarcodeMode { set; get; } = true;

        public virtual string LastChuiqiTimeStr { set; get; }

        public virtual bool IsTestersClean { set; get; }
        public virtual bool IsTestersSample { set; get; }

        public virtual bool AllowCleanActionCommand { set; get; } = false;
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

        public virtual bool _XYInDebug { set; get; }
        public virtual bool _FInDebug { set; get; }
        public virtual bool _TInDebug { set; get; }

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

        public virtual TwinCATCoil1 SaveButton { set; get; }
        public virtual TwinCATCoil1 SuckFailedFlag { set; get; }
        public virtual TwinCATCoil1 SuckAlarmRst { set; get; }
        public virtual bool _SuckFailedFlag { set; get; }

        public virtual TwinCATCoil1 M420 { set; get; }
        public virtual TwinCATCoil1 M1202 { set; get; }

        public virtual TwinCATCoil1 AlarmStr { set; get; }

        public virtual TwinCATCoil1 XYRDYtoDebug { set; get; }
        public virtual TwinCATCoil1 FRDYtoDebug { set; get; }
        public virtual TwinCATCoil1 TRDYtoDebug { set; get; }

        public virtual bool _XYRDYtoDebug { set; get; }
        public virtual bool _FRDYtoDebug { set; get; }
        public virtual bool _TRDYtoDebug { set; get; }

        public virtual TwinCATCoil1 XYDebugCMD { set; get; }
        public virtual TwinCATCoil1 FDebugCMD { set; get; }
        public virtual TwinCATCoil1 TDebugCMD { set; get; }

        public virtual TwinCATCoil1 XYDebugComplete { set; get; }
        public virtual TwinCATCoil1 FDebugComplete { set; get; }
        public virtual TwinCATCoil1 TDebugComplete { set; get; }

        public virtual TwinCATCoil1 M1202_1 { set; get; }

        public virtual TwinCATCoil1 XErrorCode { set; get; }
        public virtual TwinCATCoil1 YErrorCode { set; get; }
        public virtual TwinCATCoil1 FErrorCode { set; get; }
        public virtual TwinCATCoil1 TErrorCode { set; get; }

        public virtual TwinCATCoil1 BFI1 { set; get; }
        public virtual TwinCATCoil1 BFI2 { set; get; }
        public virtual TwinCATCoil1 BFI3 { set; get; }
        public virtual TwinCATCoil1 BFI4 { set; get; }
        public virtual TwinCATCoil1 BFI5 { set; get; }
        public virtual TwinCATCoil1 BFI6 { set; get; }
        public virtual TwinCATCoil1 BFI7 { set; get; }
        public virtual TwinCATCoil1 BFI8 { set; get; }
        public virtual TwinCATCoil1 BFI9 { set; get; }

        public virtual TwinCATCoil1 BFO1 { set; get; }
        public virtual TwinCATCoil1 BFO2 { set; get; }
        public virtual TwinCATCoil1 BFO3 { set; get; }

        public virtual string SQL_ora_server { set; get; }
        public virtual string SQL_ora_user { set; get; }
        public virtual string SQL_ora_pwd { set; get; }

        public virtual string Barsamuser_Uname { set; get; } = "ADMIN";
        public virtual string Barsamuser_Psw { set; get; }
        public virtual bool SamCheckinIsEnabled { set; get; }

        public virtual string Barsaminfo_Partnum { set; get; }
        public virtual string Barsaminfo_Barcode { set; get; }
        public virtual uint Barsaminfo_Stnum { set; get; } = 1000;
        public virtual uint Barsaminfo_Unum { set; get; } = 0;

        public virtual string DBSearch_Barcode { set; get; }

        public virtual string[] BarsamTableNames { set; get; } = new string[3] { "BARSAMINFO", "BARSAMREC", "FLUKE_DATA" };
        public virtual ushort BarsamTableIndex { set; get; } = 0;

        public virtual string[] SamNgItemsTableNames { set; get; } = new string[2] { "OK", "NG" };
        public virtual ushort SamNgItemsTableIndex { set; get; } = 0;

        public virtual DataTable SinglDt { set; get; }
        public virtual bool IsDBConnect { set; get; }

        public virtual bool SampleHave1 { set; get; }
        public virtual bool SampleHave2 { set; get; }
        public virtual bool SampleHave3 { set; get; }
        public virtual bool SampleHave4 { set; get; }
        public virtual bool SampleHave5 { set; get; }
        public virtual bool SampleHave6 { set; get; }
        public virtual bool SampleHave7 { set; get; }
        public virtual bool SampleHave8 { set; get; }


        public virtual string Barsamrec_Partnum { set; get; }
        public virtual string Barsamrec_Mno { set; get; }
        public virtual string Barsamrec_ID1 { set; get; }
        public virtual string Barsamrec_ID2 { set; get; }
        public virtual string Barsamrec_ID3 { set; get; }
        public virtual string Barsamrec_ID4 { set; get; }

        public virtual double SampleTimeElapse { set; get; }

        public virtual string LastSampleTestTimeStr { set; get; }

        public virtual string SampleRetestButtonVisibility { set; get; } = "Collapsed";

        public virtual ushort SampleNgitemsNum { set; get; } = 2;
        public virtual string OperateModeStr { set; get; } = "正常";
        public virtual ushort[] PcsGrrNeedNums { set; get; } = new ushort[20] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        public virtual ushort PcsGrrNeedNum { set; get; } = 0;
        public virtual ushort[] PcsGrrNeedCounts { set; get; } = new ushort[20] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        public virtual ushort PcsGrrNeedCount { set; get; } = 0;

        public virtual string SampleNgitem1 { set; get; }
        public virtual string SampleNgitem2 { set; get; }
        public virtual string SampleNgitem3 { set; get; }
        public virtual string SampleNgitem4 { set; get; }
        public virtual string SampleNgitem5 { set; get; }
        public virtual string SampleNgitem6 { set; get; }
        public virtual string SampleNgitem7 { set; get; }
        public virtual string SampleNgitem8 { set; get; }
        public virtual string SampleNgitem9 { set; get; }
        public virtual string SampleNgitem10 { set; get; }

        public virtual double PassMid { set; get; }
        public virtual double PassLowLimit { set; get; }

        public virtual double FlexTestTimeout { set; get; }

        public virtual string PassStatusDisplay1 { set; get; }
        public virtual string PassStatusDisplay2 { set; get; }
        public virtual string PassStatusDisplay3 { set; get; }
        public virtual string PassStatusDisplay4 { set; get; }

        public virtual string PassStatusColor1 { set; get; }
        public virtual string PassStatusColor2 { set; get; }
        public virtual string PassStatusColor3 { set; get; }
        public virtual string PassStatusColor4 { set; get; }

        #endregion
        #region 变量定义区域
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private string iniAlarmRecordPath = System.Environment.CurrentDirectory + "\\AlarmRecord.ini";
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
        Queue<AlarmRecord> myAlarmRecordQueue = new Queue<AlarmRecord>();
        public static DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool PLCNeedContinue = false;
        private DateTimeUtility.SYSTEMTIME lastchuiqi = new DateTimeUtility.SYSTEMTIME();

        private DateTimeUtility.SYSTEMTIME lastSample = new DateTimeUtility.SYSTEMTIME();

        TwinCATAds _TwinCATAds = new TwinCATAds();
        double DebugTargetX = 0;
        double DebugTargetY = 0;
        double DebugTargetF = 0;
        double DebugTargetT = 0;
        ushort fti = 1;

        bool EStop = false;

        bool isCheckined, isCheckinSuccessed;
        bool SampleAlarm_IsNeedCheckin = false, SampleAlarm_IsNeedCheckin_finish = false, NeedCheckin = false;

        DataTable SampleDt = new DataTable();

        List<AlarmTableItem> alarmTableItemsList = new List<AlarmTableItem>();

        int AlarmLastDayofYear = 0;
        bool Alarm_allowClean = true;
        string lastAlarmString = "";

        string AlarmLastDateNameStr = "";

        #endregion
        #region 构造函数
        public MainDataContext()
        {
            epsonRC90.ModelPrint += ModelPrintEventProcess;
            epsonRC90.EPSONCommTwincat += EPSONCommTwincatEventProcess;
            epsonRC90.EPSONDBSearch += EPSONDBSearchEventProcess;
            epsonRC90.EPSONSampleResult += EPSONSampleResultProcess;
            epsonRC90.EPSONSampleHave += EPSONSampleHaveProcess;
            epsonRC90.EPSONSelectSampleResultfromDt += EPSONSelectSampleResultfromDtProcess;
            epsonRC90.EPSONGRRTimesAsk += EPSONEPSONGRRTimesAskProcess;
            epsonRC90.EpsonStatusUpdate += EpsonStatusUpdateProcess;
            epsonRC90.ScanUpdate += ScanUpdateProcess;
            epsonRC90.ScanP3Update += ScanP3UpdateProcess;
            epsonRC90.ScanP3Update1 += ScanP3Update1Process;
            epsonRC90.TestFinished += StartUpdateProcess;
            dispatcherTimer.Tick += new EventHandler(DispatcherTimerTickUpdateUi);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            SampleDt.Columns.Add("PARTNUM", typeof(string));
            SampleDt.Columns.Add("SITEM", typeof(string));
            SampleDt.Columns.Add("BARCODE", typeof(string));
            SampleDt.Columns.Add("NGITEM", typeof(string));
            SampleDt.Columns.Add("TRES", typeof(string));
            SampleDt.Columns.Add("MNO", typeof(string));
            SampleDt.Columns.Add("CDATE", typeof(string));
            SampleDt.Columns.Add("CTIME", typeof(string));
            SampleDt.Columns.Add("SR01", typeof(string));

            alarmTableItemsList.Add(new AlarmTableItem("测试机穴1"));
            alarmTableItemsList.Add(new AlarmTableItem("测试机穴2"));
            alarmTableItemsList.Add(new AlarmTableItem("测试机穴3"));
            alarmTableItemsList.Add(new AlarmTableItem("测试机穴4"));
            alarmTableItemsList.Add(new AlarmTableItem("上料盘位1"));
            alarmTableItemsList.Add(new AlarmTableItem("上料盘位2"));
            alarmTableItemsList.Add(new AlarmTableItem("上料盘位3"));
            alarmTableItemsList.Add(new AlarmTableItem("上料盘位4"));
            alarmTableItemsList.Add(new AlarmTableItem("上料盘位5"));
            alarmTableItemsList.Add(new AlarmTableItem("上料盘位6"));

            ReadAlarmRecord();

            TwinCatVarInit();


            Async.RunFuncAsync(UpdateUI, null);
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
            TUnloadCompleted = new TwinCATCoil1(new TwinCATCoil("MAIN.TUnloadCompleted", typeof(bool), TwinCATCoil.Mode.Notice, 1), _TwinCATAds);

            ResetCMDComplete = new TwinCATCoil1(new TwinCATCoil("MAIN.ResetCMDComplete", typeof(bool), TwinCATCoil.Mode.Notice, 1), _TwinCATAds);
            ResetCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.ResetCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            PLCUnload = new TwinCATCoil1(new TwinCATCoil("MAIN.PLCUnload", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            WaitPLCUnload = new TwinCATCoil1(new TwinCATCoil("MAIN.WaitPLCUnload", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            SaveButton = new TwinCATCoil1(new TwinCATCoil("MAIN.SaveButton", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            SuckFailedFlag = new TwinCATCoil1(new TwinCATCoil("MAIN.SuckFailedFlag", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            SuckAlarmRst = new TwinCATCoil1(new TwinCATCoil("MAIN.SuckAlarmRst", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            M420 = new TwinCATCoil1(new TwinCATCoil("MAIN.M420", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            M1202 = new TwinCATCoil1(new TwinCATCoil("MAIN.M1202", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            AlarmStr = new TwinCATCoil1(new TwinCATCoil("MAIN.AlarmStr", typeof(string), TwinCATCoil.Mode.Notice), _TwinCATAds);

            XYRDYtoDebug = new TwinCATCoil1(new TwinCATCoil("MAIN.XYRDYtoDebug", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FRDYtoDebug = new TwinCATCoil1(new TwinCATCoil("MAIN.FRDYtoDebug", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TRDYtoDebug = new TwinCATCoil1(new TwinCATCoil("MAIN.TRDYtoDebug", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            XYDebugCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.XYDebugCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FDebugCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.FDebugCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TDebugCMD = new TwinCATCoil1(new TwinCATCoil("MAIN.TDebugCMD", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);


            XYDebugComplete = new TwinCATCoil1(new TwinCATCoil("MAIN.XYDebugComplete", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FDebugComplete = new TwinCATCoil1(new TwinCATCoil("MAIN.FDebugComplete", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TDebugComplete = new TwinCATCoil1(new TwinCATCoil("MAIN.TDebugComplete", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            M1202_1 = new TwinCATCoil1(new TwinCATCoil("MAIN.M1202_1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            XErrorCode = new TwinCATCoil1(new TwinCATCoil("MAIN.XErrorCode", typeof(uint), TwinCATCoil.Mode.Notice), _TwinCATAds);
            YErrorCode = new TwinCATCoil1(new TwinCATCoil("MAIN.YErrorCode", typeof(uint), TwinCATCoil.Mode.Notice), _TwinCATAds);
            FErrorCode = new TwinCATCoil1(new TwinCATCoil("MAIN.FErrorCode", typeof(uint), TwinCATCoil.Mode.Notice), _TwinCATAds);
            TErrorCode = new TwinCATCoil1(new TwinCATCoil("MAIN.TErrorCode", typeof(uint), TwinCATCoil.Mode.Notice), _TwinCATAds);

            BFI1 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI2 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI3 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI3", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI4 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI4", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI5 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI5", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI6 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI6", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI7 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI7", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI8 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI8", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFI9 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFI9", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

            BFO1 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFO1", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFO2 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFO2", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);
            BFO3 = new TwinCATCoil1(new TwinCATCoil("MAIN.BFO3", typeof(bool), TwinCATCoil.Mode.Notice), _TwinCATAds);

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
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
            SamCheckinIsEnabled = false;
            isLogin = false;
            Barsamuser_Psw = "";
            LoginButtonString = "登录";
            
        }
        public void ChoseAboutPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
            //MaopaoPaixu();
        }
        public void ChoseHelpPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Visible";
            //MaopaoPaixu();
        }
        public void ChoseParameterPage()
        {
            ParameterPageVisibility = "Visible";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseOperaterActionPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Visible";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseCameraPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseCameraHcPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Visible";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
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
        public void ChoseScanPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Visible";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
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
            ScanPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseBarcodeDisplayPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Visible";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseTestRecordPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Visible";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseAlarmRecordPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Visible";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseSampleTestPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Visible";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Collapsed";
            HelpPageVisibility = "Collapsed";
        }
        public void ChoseSampleTestPage1()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            ScanPageVisibility = "Collapsed";
            OperaterActionPageVisibility = "Collapsed";
            BarcodeDisplayPageVisibility = "Collapsed";
            TestRecordPageVisibility = "Collapsed";
            TwincatNcPageVisibility = "Collapsed";
            SampleTestPageVisibility = "Collapsed";
            AlarmRecordPageVisibility = "Collapsed";
            SampleTestPage1Visibility = "Visible";
            HelpPageVisibility = "Collapsed";
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
        private string MaopaoPaixu()
        {
            string str = "";
            double[] Array_A = new double[4];
            ushort[] Array_B = new ushort[4];

            for (ushort k = 0; k < 4; k++)
            {
                Array_A[k] = epsonRC90.testerwith4item[k / 2].Yield[k % 2];
                Array_B[k] = k;
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3 - i; j++)
                {
                    if (Array_A[j] < Array_A[j + 1])
                    {
                        var temp1 = Array_A[j];
                        var temp2 = Array_B[j];
                        Array_A[j] = Array_A[j + 1];
                        Array_B[j] = Array_B[j + 1];
                        Array_A[j + 1] = temp1;
                        Array_B[j + 1] = temp2;
                    }
                }
            }
            int i_index = 0;
            for (int m = 0; m < 4; m++)
            {
                if (Array_B[m] == 0)
                {
                    i_index = m;
                    break;
                }
            }
            if (i_index != 3)
            {
                var temp5 = Array_B[i_index];
                Array_B[i_index] = Array_B[3];
                Array_B[3] = temp5;
            }
            for (int l = 0; l < 4; l++)
            {
                str += Array_B[l].ToString() + ";";
            }
            return str;
        }
        public async void EpsonOpetate(object p)
        {
            string s = p.ToString();
            switch (s)
            {
                //启动
                case "1":
                    AlarmTextGridShow = "Collapsed";
                    if (epsonRC90.CtrlStatus && EpsonStatusReady && !EpsonStatusEStop)
                    {
                        Testerwith4item.IsInSampleMode = false;
                        if (isGRRMode)
                        {
                            await epsonRC90.CtrlNet.SendAsync("$start,3");
                            Msg = messagePrint.AddMessage("GRR模式");
                            OperateModeStr = "GRR";
                        }
                        else
                        {
                            string maopaostr = MaopaoPaixu();

                            await epsonRC90.CtrlNet.SendAsync("$start,2");
                            Msg = messagePrint.AddMessage("正常模式");
                            OperateModeStr = "正常";
                            AllowCleanActionCommand = true;
                            AllowSampleTestCommand = true;
                            await Task.Delay(200);
                            if (epsonRC90.TestSendStatus)
                            {
                                await epsonRC90.TestSentNet.SendAsync("IndexArray_i;" + maopaostr);
                            }
                            if (!IsTestersSample && epsonRC90.TestSendStatus)
                            {
                                await Task.Delay(200);
                                await epsonRC90.TestSentNet.SendAsync("GONOGOCancel");
                            }
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
                    if (r && epsonRC90.TestSendStatus && (EpsonStatusRunning || EpsonStatusPaused) && !isGRRMode)
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
                if (i < 4)
                {
                    epsonRC90.testerwith4item[i / 2].TestSpan[i % 2] = 0;
                    epsonRC90.testerwith4item[i / 2].PassCount[i % 2] = 0;
                    epsonRC90.testerwith4item[i / 2].FailCount[i % 2] = 0;
                    epsonRC90.testerwith4item[i / 2].TestCount[i % 2] = 0;
                    epsonRC90.testerwith4item[i / 2].Yield[i % 2] = 0;
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "TestSpan", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "PassCount", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "FailCount", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "TestCount", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + i.ToString(), "Yield", "0");
                    Msg = messagePrint.AddMessage("测试机 " + (i + 1).ToString() + " AAB 数据清空");
                }
                else
                {
                    epsonRC90.testerwith4item[(i - 4) / 2].TestSpan[(i - 4) % 2] = 0;
                    epsonRC90.testerwith4item[(i - 4) / 2].PassCount_Nomal[(i - 4) % 2] = 0;
                    epsonRC90.testerwith4item[(i - 4) / 2].FailCount_Nomal[(i - 4) % 2] = 0;
                    epsonRC90.testerwith4item[(i - 4) / 2].TestCount_Nomal[(i - 4) % 2] = 0;
                    epsonRC90.testerwith4item[(i - 4) / 2].Yield_Nomal[(i - 4) % 2] = 0;
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (i - 4).ToString(), "TestSpan", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (i - 4).ToString(), "PassCount_Nomal", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (i - 4).ToString(), "FailCount_Nomal", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (i - 4).ToString(), "TestCount_Nomal", "0");
                    Inifile.INIWriteValue(iniTesterResutPath, "Tester" + (i - 4).ToString(), "Yield_Nomal", "0");
                    Msg = messagePrint.AddMessage("测试机 " + (i - 4 + 1).ToString() + " 数据清空");
                }

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
            if (!Directory.Exists(TestRecordSavePath))
            {
                Directory.CreateDirectory(TestRecordSavePath);
            }
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
            //AlarmLastDayofYear = DateTime.Now.DayOfYear;
            if (AlarmLastDateNameStr != DateTime.Now.ToLongDateString() && (DateTime.Now.Hour >= 8 || (DateTime.Now.DayOfYear - AlarmLastDayofYear) * 24 + DateTime.Now.Hour > 24))
            {
                AlarmLastDateNameStr = DateTime.Now.ToLongDateString();
                Inifile.INIWriteValue(iniAlarmRecordPath, "Alarm", "AlarmLastDateNameStr", AlarmLastDateNameStr);
            }
            string Bancistr = DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20 ? "白班" : "夜班";
            string filepath = AlarmSavePath + "\\Alarm" + AlarmLastDateNameStr + Bancistr + ".csv";
            if (!Directory.Exists(AlarmSavePath))
            {
                Directory.CreateDirectory(AlarmSavePath);
            }
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
        private void SaveScanBarcodetoCSV(string bar)
        {
            if (!Directory.Exists(TestRecordSavePath + @"\Barcode\" + DateTime.Now.ToLongDateString().ToString()))
            {
                Directory.CreateDirectory(TestRecordSavePath + @"\Barcode\" + DateTime.Now.ToLongDateString().ToString());
            }
            string filepath = TestRecordSavePath + @"\Barcode\" + DateTime.Now.ToLongDateString().ToString() + @"\Scan" + (DateTime.Now.ToShortDateString()).Replace("/", "") + ".csv";
            try
            {
                if (!File.Exists(filepath))
                {
                    string[] heads = { "DateTime", "ScanBarcode" };
                    Csvfile.savetocsv(filepath, heads);
                }
                string[] conte = { System.DateTime.Now.ToString(), bar };
                Csvfile.savetocsv(filepath, conte);
            }
            catch (Exception ex)
            {
                Msg = messagePrint.AddMessage("写入CSV文件失败");
                Log.Default.Error("写入CSV文件失败", ex.Message);
            }
        }
        //private void addAlarm(string almstr)
        //{
        //    AlarmRecord alarmRecord = new AlarmRecord();
        //    alarmRecord.AlarmTime = System.DateTime.Now.ToString();
        //    alarmRecord.AlarmString = almstr;
        //    lock (this)
        //    {
        //        myAlarmRecordQueue.Enqueue(alarmRecord);
        //    }



        //}
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
                    if (epsonRC90.TestSendStatus && IsTestersClean && AllowCleanActionCommand)
                    {
                        await epsonRC90.TestSentNet.SendAsync("TestersCleanAction");
                        AllowCleanActionCommand = false;
                    }
                    break;
                case "5":
                    if (epsonRC90.TestSendStatus && IsTestersSample && AllowSampleTestCommand)
                    {
                        await epsonRC90.TestSentNet.SendAsync("GONOGOAction;" + SampleNgitemsNum.ToString());
                        AllowSampleTestCommand = false;
                    }
                    break;
                case "6":
                    if (epsonRC90.TestSendStatus && IsTestersSample)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SamRetest");
                        SampleRetestButtonVisibility = "Collapsed";
                    }
                    break;
                case "7":
                    //alarmRecord.Clear();
                    ClearAlarmRecord();
                    break;
                default:
                    break;
            }
        }
        private void ClearAlarmRecord()
        {
            foreach (var item in alarmTableItemsList)
            {
                item.产品没放好 = 0;
                item.吸取失败 = 0;
                item.测试机超时 = 0;
                //item.连续NG = 0;
            }
            WriteAlarmRecord();
    
            AlarmLastDayofYear = DateTime.Now.DayOfYear;
            Inifile.INIWriteValue(iniAlarmRecordPath, "Alarm", "AlarmLastDayofYear", AlarmLastDayofYear.ToString());
            Msg = messagePrint.AddMessage("清空报警数据");

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

                //lastSample

                Inifile.INIWriteValue(iniParameterPath, "Sample", "wDay", lastSample.wDay.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Sample", "wDayOfWeek", lastSample.wDayOfWeek.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Sample", "wHour", lastSample.wHour.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Sample", "wMilliseconds", lastSample.wMilliseconds.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Sample", "wMinute", lastSample.wMinute.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Sample", "wMonth", lastSample.wMonth.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Sample", "wSecond", lastSample.wSecond.ToString());
                Inifile.INIWriteValue(iniParameterPath, "Sample", "wYear", lastSample.wYear.ToString());

            }
            catch (Exception ex)
            {

                Log.Default.Error("SaveLastSamplTimetoIni", ex);
            }
        }
        public void ReadTwinCatDatafromIni()
        {
            try
            {
                ReleasePositionX1.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "ReleasePositionX1", "224.3008"));
                ReleasePositionY1.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "ReleasePositionY1", "320.7936"));
                ReleasePositionX2.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "ReleasePositionX2", "224.2896"));
                ReleasePositionY2.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "ReleasePositionY2", "0.2528"));
                ReleasePositionX3.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "ReleasePositionX3", "25.6064"));
                ReleasePositionY3.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "ReleasePositionY3", "319.9712"));
                PickPositionX.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "PickPositionX", "368.448"));
                PickPositionY.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "PickPositionY", "509.976"));
                WaitPositionX.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "WaitPositionX", "28.9152"));
                WaitPositionY.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "XY", "WaitPositionY", "509.4416"));
                FPosition1.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "F", "FPosition1", "466.234"));
                FPosition2.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "F", "FPosition2", "565.634"));
                FPosition3.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "F", "FPosition3", "293.882"));
                FPosition4.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "F", "FPosition4", "12.91"));
                FPosition5.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "F", "FPosition5", "12.91"));
                FPosition6.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "F", "FPosition6", "260.402"));
                FPosition7.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "F", "FPosition7", "170.004"));
                TPosition1.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "T", "TPosition1", "-28.4904"));
                TPosition2.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "T", "TPosition2", "-303.9456"));
                TPosition3.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "T", "TPosition3", "-565.848"));
                TPosition4.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "T", "TPosition4", "-565.848"));
                TPosition5.Value = double.Parse(Inifile.INIGetStringValue(TwincatParameterPath, "T", "TPosition5", "-1136.144"));
                Calc_Start.Value = true;
                SaveButton.Value = true;
                Msg = messagePrint.AddMessage("载入轴控参数完成");
            }
            catch (Exception ex)
            {

                Msg = messagePrint.AddMessage("载入轴控参数失败");
            }
        }
        public void SaveTwincatDataAction()
        {
            try
            {
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionX1", ReleasePositionX1.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionY1", ReleasePositionY1.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionX2", ReleasePositionX2.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionY2", ReleasePositionY2.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionX3", ReleasePositionX3.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "ReleasePositionY3", ReleasePositionY3.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "PickPositionX", PickPositionX.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "PickPositionY", PickPositionY.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "WaitPositionX", WaitPositionX.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "XY", "WaitPositionY", WaitPositionY.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "F", "FPosition1", FPosition1.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "F", "FPosition2", FPosition2.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "F", "FPosition3", FPosition3.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "F", "FPosition4", FPosition4.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "F", "FPosition5", FPosition5.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "F", "FPosition6", FPosition6.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "F", "FPosition7", FPosition7.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "T", "TPosition1", TPosition1.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "T", "TPosition2", TPosition2.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "T", "TPosition3", TPosition3.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "T", "TPosition4", TPosition4.Value.ToString());
                Inifile.INIWriteValue(TwincatParameterPath, "T", "TPosition5", TPosition5.Value.ToString());

                //TPosition1
                //FPosition1
                //PickPositionX
                //WaitPositionX
                SaveButton.Value = true;
                Msg = messagePrint.AddMessage("保存轴控参数完成");
            }
            catch
            {

                Msg = messagePrint.AddMessage("保存轴控参数失败");
            }
        }
        public void ScanAction()
        {
            Scan.GetBarCode(ScanActionCallback);
        }
        public void ScanActionCallback(string str)
        {
            BarcodeDisplay = str;
        }
        /// <summary>
        /// 样本录入用户登录
        /// </summary>
        public async void SamCheckinLoadAction()
        {
            if (samCheckinLoadAction(Barsamuser_Uname))
            {
                SamCheckinIsEnabled = true;
                await mydialog.showmessage("录入用户登录 成功");
            }
            else
            {
                SamCheckinIsEnabled = false;
                await mydialog.showmessage("录入用户登录 失败");

            }
        }
        public async void SamCheckinAction()
        {
            if (samCheckinAction())
            {
                await mydialog.showmessage("样本数据录入 成功");
            }
            else
            {
                await mydialog.showmessage("样本数据录入 失败");
            }
        }
        public async void SampleHaveUpdateAction(object p)
        {
            switch (p.ToString())
            {
                case "1":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;1;" + SampleHave1.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave1", SampleHave1.ToString());
                    break;
                case "2":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;2;" + SampleHave2.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave2", SampleHave2.ToString());
                    break;
                case "3":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;3;" + SampleHave3.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave3", SampleHave3.ToString());
                    break;
                case "4":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;4;" + SampleHave4.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave4", SampleHave4.ToString());
                    break;
                case "5":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;5;" + SampleHave4.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave5", SampleHave5.ToString());
                    break;
                case "6":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;6;" + SampleHave4.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave6", SampleHave6.ToString());
                    break;
                case "7":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;7;" + SampleHave4.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave7", SampleHave7.ToString());
                    break;
                case "8":
                    if (epsonRC90.TestSendStatus)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SampleHave;8;" + SampleHave4.ToString());
                    }
                    Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave8", SampleHave8.ToString());
                    break;
                default:
                    break;
            }
        }
        private void SaveSampleRecordLocal()
        {
            //TestRecordSavePath
            if (!Directory.Exists(TestRecordSavePath + @"\" + DateTime.Now.ToLongDateString().ToString()))
            {
                Directory.CreateDirectory(TestRecordSavePath + @"\" + DateTime.Now.ToLongDateString().ToString());
            }
            string filepath = TestRecordSavePath + @"\" + DateTime.Now.ToLongDateString().ToString() + @"\" + (DateTime.Now.ToShortDateString()).Replace("/", "") + (DateTime.Now.ToShortTimeString()).Replace(":", "") + ".csv";
            if (SampleDt.Rows.Count > 0)
            {
                Csvfile.dt2csv(SampleDt, filepath, "SampleTest", "PARTNUM,SITEM,BARCODE,NGITEM,TRES,MNO,CDATE,CTIME,SR01");
                SampleDt.Rows.Clear();
            }
        }
        private string[] PassStatusProcess(double f)
        {
            string[] strs = new string[2];
            if (f > PassMid)
            {
                strs[0] = "良率" + f.ToString() + "% 优秀";
                strs[1] = "Blue";
            }
            else
            {
                if (f > PassLowLimit)
                {
                    strs[0] = "良率" + f.ToString() + "% 正常";
                    strs[1] = "Green";
                }
                else
                {
                    if (f == 0)
                    {
                        strs[0] = "良率" + f.ToString() + "% 未知";
                        strs[1] = "Black";
                    }
                    else
                    {
                        strs[0] = "良率" + f.ToString() + "% 异常";
                        strs[1] = "Red";
                    }

                }
            }
            return strs;
        }
        #region 数据库
        private void setLocalTime(string strDateTime)
        {
            DateTimeUtility.SYSTEMTIME st = new DateTimeUtility.SYSTEMTIME();
            DateTime dt = Convert.ToDateTime(strDateTime);
            st.FromDateTime(dt);
            DateTimeUtility.SetLocalTime(ref st);
        }
        private void ConnectDBTest()
        {
            try
            {
                OraDB oraDB = new OraDB(SQL_ora_server, SQL_ora_user, SQL_ora_pwd);
                if (oraDB.isConnect())
                {
                    string dbtime = oraDB.sfc_getServerDateTime();
                    setLocalTime(dbtime);
                    Msg = messagePrint.AddMessage("获取数据库时间： " + dbtime);

                    IsDBConnect = true;
                }
                else
                {
                    Msg = messagePrint.AddMessage("数据库未连接");

                    IsDBConnect = false;
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                Msg = messagePrint.AddMessage("获取数据库时间失败");
                IsDBConnect = false;
            }
        }
        public void SQLGetBarcode(object p)
        {
            switch (p.ToString())
            {
                case "1":
                    Barsaminfo_Barcode = BarcodeDisplay;
                    break;
                case "2":
                    DBSearch_Barcode = BarcodeDisplay;
                    break;
                default:
                    break;
            }
        }
        public void SearchAction()
        {
            try
            {
                if (DBSearch_Barcode.Length > 10)
                {
                    LookforDt(DBSearch_Barcode.Replace(" ", ""), BarsamTableIndex);
                }
            }
            catch
            {


            }

        }
        private void SelectSampleResultfromDt()
        {
            string[] arrField = new string[2];
            string[] arrValue = new string[2];
            try
            {
                string tablename = "FLUKE_DATA";
                OraDB oraDB = new OraDB(SQL_ora_server, SQL_ora_user, SQL_ora_pwd);
                if (oraDB.isConnect())
                {
                    IsDBConnect = true;
                    foreach (DataRow item in SampleDt.Rows)
                    {
                        arrField[0] = "BARCODE";
                        arrValue[0] = (string)item["BARCODE"];
                        arrField[1] = "FL04";
                        arrValue[1] = (string)item["SR01"];
                        DataSet s = oraDB.selectSQLwithOrder(tablename.ToUpper(), arrField, arrValue);
                        SinglDt = s.Tables[0];
                        if (SinglDt.Rows.Count == 0)
                        {
                            Msg = messagePrint.AddMessage("未查询到 " + (string)item["BARCODE"] + "," + (string)item["SR01"] + " 信息");
                        }
                        else
                        {
                            item["TRES"] = (string)SinglDt.Rows[0]["FL01"];
                        }
                    }
                }
                else
                {
                    IsDBConnect = true;
                    Msg = messagePrint.AddMessage("数据库连接失败");
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                Log.Default.Error("SelectSampleResultfromDt", ex.Message);
            }

        }
        private bool LookforDt(string barcode,ushort index)
        {
            bool r = false;
            string[] arrField = new string[1];
            string[] arrValue = new string[1];
            try
            {
                string tablename = BarsamTableNames[index];
                OraDB oraDB = new OraDB(SQL_ora_server, SQL_ora_user, SQL_ora_pwd);
                if (oraDB.isConnect())
                {
                    IsDBConnect = true;
                    arrField[0] = "BARCODE";
                    arrValue[0] = barcode;
                    DataSet s = oraDB.selectSQL(tablename.ToUpper(), arrField, arrValue);
                    SinglDt = s.Tables[0];
                    if (SinglDt.Rows.Count == 0)
                    {
                        Msg = messagePrint.AddMessage("未查询到 " + barcode + " 信息");
                        r = false;

                    }
                    else
                    {
                        Msg = messagePrint.AddMessage("查询到 " + barcode + " 信息");
                        r = true;
                    }
                    

                }
                else
                {
                    IsDBConnect = true;
                    Msg = messagePrint.AddMessage("数据库连接失败");
                    
                    r = false;
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                r = false;
                Log.Default.Error("LookforDt", ex.Message);
            }
            return r;
        }
        private bool samCheckinLoadAction(string user)
        {
            bool r = false;
            string[] arrField = new string[1];
            string[] arrValue = new string[1];
            try
            {
                string tablename = "BARSAMUSER";
                OraDB oraDB = new OraDB(SQL_ora_server, SQL_ora_user, SQL_ora_pwd);
                if (oraDB.isConnect())
                {
                    IsDBConnect = true;
                    arrField[0] = "UNAME";
                    arrValue[0] = user.ToUpper();
                    DataSet s = oraDB.selectSQL(tablename.ToUpper(), arrField, arrValue);
                    DataTable singlDt = s.Tables[0];
                    if (singlDt.Rows.Count == 0)
                    {
                        Msg = messagePrint.AddMessage("未查询到 " + user + " 信息");
                        r = false;

                    }
                    else
                    {
                        Msg = messagePrint.AddMessage("查询到 " + user + " 信息");
                        if ((string)singlDt.Rows[0]["PSW"] == Barsamuser_Psw)
                        {
                            Msg = messagePrint.AddMessage("用户 " + user + " 登录成功");
                            SinglDt = singlDt;
                            r = true;
                        }
                        else
                        {
                            Msg = messagePrint.AddMessage("用户 " + user + " 登录失败");
                            r = false;
                        }
                    }


                }
                else
                {
                    IsDBConnect = true;
                    Msg = messagePrint.AddMessage("数据库连接失败");

                    r = false;
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                r = false;
                Log.Default.Error("samCheckinLoadAction", ex.Message);
            }
            return r;
        }
        private bool samUpdateAction1()
        {
            bool r = false;
            try
            {
                X758SamCheckinData x758SamCheckinData = new X758SamCheckinData();
                x758SamCheckinData.Partnum = Barsaminfo_Partnum.ToUpper();
                x758SamCheckinData.Barcode = Barsaminfo_Barcode.ToUpper();
                x758SamCheckinData.Stnum = Barsaminfo_Stnum;
                x758SamCheckinData.Unum = Barsaminfo_Unum;
                x758SamCheckinData.Ngitem = SamNgItemsTableNames[SamNgItemsTableIndex].ToUpper();
                string tablename = "BARSAMINFO";
                OraDB oraDB = new OraDB(SQL_ora_server, SQL_ora_user, SQL_ora_pwd);
                if (oraDB.isConnect())
                {
                    IsDBConnect = true;
                    string[,] arrFieldAndNewValue = { { "PARTNUM", x758SamCheckinData.Partnum } , { "SITEM", "FLUKE" }, { "STNUM", x758SamCheckinData.Stnum.ToString() }, { "UNUM", x758SamCheckinData.Unum.ToString() }, { "NGITEM", x758SamCheckinData.Ngitem } };
                    string[,] arrFieldAndOldValue = { { "BARCODE", x758SamCheckinData.Barcode } };
                    oraDB.updateSQL1(tablename.ToUpper(), arrFieldAndNewValue, arrFieldAndOldValue);
                    Msg = messagePrint.AddMessage("数据更新完成");
                    r = true;
                }
                else
                {
                    IsDBConnect = false;
                    Msg = messagePrint.AddMessage("数据库连接失败");
                    r = false;
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                r = false;
                Log.Default.Error("samUpdateAction1", ex.Message);
            }
            return r;
        }
        private bool samInsertAction1()
        {
            bool r = false;
            try
            {
                X758SamCheckinData x758SamCheckinData = new X758SamCheckinData();
                x758SamCheckinData.Partnum = Barsaminfo_Partnum.ToUpper();
                x758SamCheckinData.Barcode = Barsaminfo_Barcode.ToUpper();
                x758SamCheckinData.Stnum = Barsaminfo_Stnum;
                x758SamCheckinData.Unum = Barsaminfo_Unum;
                x758SamCheckinData.Ngitem = SamNgItemsTableNames[SamNgItemsTableIndex].ToUpper();
                string tablename = "BARSAMINFO";
                OraDB oraDB = new OraDB(SQL_ora_server, SQL_ora_user, SQL_ora_pwd);
                if (oraDB.isConnect())
                {
                    IsDBConnect = true;
                    string[] arrFieldAndNewValue = { "PARTNUM", "BARCODE", "SITEM", "STNUM", "UNUM", "NGITEM" };
                    string[] arrFieldAndOldValue = { x758SamCheckinData.Partnum, x758SamCheckinData.Barcode, "FLUKE", x758SamCheckinData.Stnum.ToString(), x758SamCheckinData.Unum.ToString(), x758SamCheckinData.Ngitem };
                    oraDB.insertSQL1(tablename.ToUpper(), arrFieldAndNewValue, arrFieldAndOldValue);
                    Msg = messagePrint.AddMessage("数据插入完成");
                    r = true;
                }
                else
                {
                    IsDBConnect = false;
                    Msg = messagePrint.AddMessage("数据库连接失败");
                    r = false;
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                r = false;
                Log.Default.Error("samUpdateAction1", ex.Message);
            }
            return r;
        }
        private bool samInsertAction2(X758SampleResultData x758SampleResultData)
        {
            bool r = false;
            try
            {
                //if (ngitem.Length > 20)
                //{
                //    ngitem = ngitem.Substring(0, 19);
                //}
                //if (tresult.Length > 20)
                //{
                //    tresult = tresult.Substring(0, 19);
                //}

                //X758SampleResultData x758SampleResultData = new X758SampleResultData();
                //x758SampleResultData.PARTNUM = Barsamrec_Partnum.ToUpper();
                //x758SampleResultData.SITEM = "FLUKE";
                //x758SampleResultData.BARCODE = bar.ToUpper();
                //x758SampleResultData.NGITEM = ngitem.ToUpper();
                //x758SampleResultData.TRES = tresult.ToUpper();
                //x758SampleResultData.MNO = mno.ToUpper();
                //x758SampleResultData.CDATE = (DateTime.Now.ToShortDateString()).Replace("/", "");
                //x758SampleResultData.CTIME = (DateTime.Now.ToShortTimeString()).Replace(":", "");
                //x758SampleResultData.SR01 = id;
                if (x758SampleResultData.TRES.Length > 20)
                {
                    x758SampleResultData.TRES = x758SampleResultData.TRES.Substring(0, 19);
                }
                string tablename = "BARSAMREC";
                OraDB oraDB = new OraDB(SQL_ora_server, SQL_ora_user, SQL_ora_pwd);
                if (oraDB.isConnect())
                {
                    IsDBConnect = true;
                    string[] arrFieldAndNewValue = { "PARTNUM", "SITEM", "BARCODE", "NGITEM", "TRES", "MNO", "CDATE", "CTIME", "SR01" };
                    string[] arrFieldAndOldValue = { x758SampleResultData.PARTNUM, x758SampleResultData.SITEM, x758SampleResultData.BARCODE, x758SampleResultData.NGITEM, x758SampleResultData.TRES, x758SampleResultData.MNO, x758SampleResultData.CDATE, x758SampleResultData.CTIME, x758SampleResultData.SR01 };
                    oraDB.insertSQL1(tablename.ToUpper(), arrFieldAndNewValue, arrFieldAndOldValue);
                    Msg = messagePrint.AddMessage("数据插入完成");
                    r = true;
                }
                else
                {
                    IsDBConnect = false;
                    Msg = messagePrint.AddMessage("数据库连接失败");
                    r = false;
                }
                oraDB.disconnect();
            }
            catch (Exception ex)
            {
                r = false;
                Log.Default.Error("samInsertAction2", ex.Message);
            }
            return r;
        }
        private bool samCheckinAction()
        {
            bool r = false;
            try
            {
                //Barsaminfo_Barcode
                if (LookforDt(Barsaminfo_Barcode, 0))
                {
                    //查询到条码信息。执行更新操作
                    r = samUpdateAction1();
                }
                else
                {
                    //未查询到条码信息。
                    if (Barsaminfo_Barcode.Length > 10)
                    {
                        //执行插入操作
                        r = samInsertAction1();
                    }
                    else
                    {
                        r = false;
                    }
                }
            }
            catch 
            {
                    
            }
            isCheckinSuccessed = r;
            isCheckined = true;
            return r;
        }
        private async Task<bool> WaitCheckinProcess()
        {
            bool r = false;
            isCheckined = false;
            isCheckinSuccessed = false;
            while (!isCheckined)
            {
                await Task.Delay(100);
            }
            r = isCheckinSuccessed;
            return r;
        }
        //private async Task WaitSampleAlarmIsNeedCheckinProcess()
        //{
            

        //    SampleAlarm_IsNeedCheckin_finish = false;
        //    while (!SampleAlarm_IsNeedCheckin_finish)
        //    {
        //        await Task.Delay(100);
        //    }
            
            
        //}
        #endregion
        #endregion
        #region BECKHOFF
        public void TwincatOperateAction(object p)
        {
            try
            {
                switch (p.ToString())
                {
                    case "1":
                        if ((bool)SuckFailedFlag.Value)
                        {
                            SuckAlarmRst.Value = true;
                        }
                        break;
                    case "2":
                        if ((bool)XYRDYtoDebug.Value)
                        {
                            XYDebugCMD.Value = true;
                        }
                        
                        
                        break;
                    case "3":
                        if ((bool)XYInDebug.Value)
                        {
                            XYDebugComplete.Value = true;
                        }
                        

                        break;
                    case "4":
                        if ((bool)FRDYtoDebug.Value)
                        {
                            FDebugCMD.Value = true;
                        }


                        break;
                    case "5":
                        if ((bool)FInDebug.Value)
                        {
                            FDebugComplete.Value = true;
                        }


                        break;
                    case "6":
                        if ((bool)TRDYtoDebug.Value)
                        {
                            TDebugCMD.Value = true;
                        }


                        break;
                    case "7":
                        if ((bool)TInDebug.Value)
                        {
                            TDebugComplete.Value = true;
                        }


                        break;
                    default:
                        break;
                }
            }
            catch 
            {

                
            }
        }
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
                        if (EStop)
                        {
                            break;
                        }
                    }
                    if (!EStop)
                    {
                        callback("FMOVE;" + s);
                    }
                    
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
                        if (EStop)
                        {
                            break;
                        }
                    }
                    if (!EStop)
                    {
                        callback();
                    }
                    
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
                        if (EStop)
                        {
                            break;
                        }
                    }
                    if (!EStop)
                    {
                        callback("TMOVE;" + s);
                    }
                    
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
                        if (EStop)
                        {
                            break;
                        }
                    }
                    if (!EStop)
                    {
                        callback("ULOAD");
                    }
                    
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
                    ReadTwinCatDatafromIni();

                    while (!(bool)ResetCMDComplete.Value)
                    {
                        await Task.Delay(100);
                        if (EStop)
                        {
                            break;
                        }
                    }
                    if (!EStop)
                    {
                        callback("ResetCMD");
                    }
                    
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
        private void EPSONSampleResultProcess(string str)
        {
            string ngitem = "", tresult = "";
            string[] strs = str.Split(',');
            ushort index = ushort.Parse(strs[1]);
            string bar = epsonRC90.testerwith4item[(index - 1) / 2].TesterBracode[(index - 1) % 2];
            switch (strs[2])
            {
                case "OK":
                    ngitem = SampleNgitem1;
                    break;
                case "NG":
                    ngitem = SampleNgitem2;
                    break;
                case "NG1":
                    ngitem = SampleNgitem3;
                    break;
                case "NG2":
                    ngitem = SampleNgitem4;
                    break;
                case "NG3":
                    ngitem = SampleNgitem5;
                    break;
                case "NG4":
                    ngitem = SampleNgitem6;
                    break;
                case "NG5":
                    ngitem = SampleNgitem7;
                    break;
                case "NG6":
                    ngitem = SampleNgitem8;
                    break;
                case "NG7":
                    ngitem = SampleNgitem9;
                    break;
                case "NG8":
                    ngitem = SampleNgitem10;
                    break;
                default:
                    
                    break;
            }
            switch (strs[3])
            {
                case "OK":
                    tresult = SampleNgitem1;
                    break;
                case "NG":
                    tresult = SampleNgitem2;
                    break;
                case "NG1":
                    tresult = SampleNgitem3;
                    break;
                case "NG2":
                    tresult = SampleNgitem4;
                    break;
                case "NG3":
                    tresult = SampleNgitem5;
                    break;
                case "NG4":
                    tresult = SampleNgitem6;
                    break;
                case "NG5":
                    tresult = SampleNgitem7;
                    break;
                case "NG6":
                    tresult = SampleNgitem8;
                    break;
                case "NG7":
                    tresult = SampleNgitem9;
                    break;
                case "NG8":
                    tresult = SampleNgitem10;
                    break;
                default:

                    break;
            }
            string bordid = "";
            switch (strs[1])
            {
                case "1":
                    bordid = Barsamrec_ID1;
                    break;
                case "2":
                    bordid = Barsamrec_ID2;
                    break;
                case "3":
                    bordid = Barsamrec_ID3;
                    break;
                case "4":
                    bordid = Barsamrec_ID4;
                    break;
                default:
                    break;
            }
            string mno = Barsamrec_Mno + "Flex" + strs[1];
            //samInsertAction2(bar, ngitem, tresult, mno);


            DataRow dr = SampleDt.NewRow();
            dr["PARTNUM"] = Barsamrec_Partnum.ToUpper();
            dr["SITEM"] = "FLUKE";
            dr["BARCODE"] = bar.ToUpper();
            dr["NGITEM"] = ngitem;
            dr["TRES"] = tresult;
            dr["MNO"] = mno.ToUpper();
            dr["CDATE"] = (DateTime.Now.ToShortDateString()).Replace("/", "");
            dr["CTIME"] = (DateTime.Now.ToShortTimeString()).Replace(":", "");
            dr["SR01"] = bordid.ToUpper();

            SampleDt.Rows.Add(dr);

        }
        private async void EPSONSelectSampleResultfromDtProcess(string str)
        {
            SelectSampleResultfromDt();
            foreach (DataRow item in SampleDt.Rows)
            {
                X758SampleResultData x758SampleResultData = new X758SampleResultData();
                x758SampleResultData.PARTNUM = (string)item["PARTNUM"];
                x758SampleResultData.SITEM = (string)item["SITEM"];
                x758SampleResultData.BARCODE = (string)item["BARCODE"];
                x758SampleResultData.NGITEM = (string)item["NGITEM"];
                x758SampleResultData.TRES = (string)item["TRES"];
                x758SampleResultData.MNO = (string)item["MNO"];
                x758SampleResultData.CDATE = (string)item["CDATE"];
                x758SampleResultData.CTIME = (string)item["CTIME"];
                x758SampleResultData.SR01 = (string)item["SR01"];
                //插入数据库
                samInsertAction2(x758SampleResultData);
                if ((string)item["TRES"] != (string)item["NGITEM"])
                {
                    await Task.Delay(100);
                    string NgItem = "Error";
                    if ((string)item["NGITEM"] == SampleNgitem1)
                    {
                        NgItem = "OK";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem2)
                    {
                        NgItem = "NG";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem3)
                    {
                        NgItem = "NG1";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem4)
                    {
                        NgItem = "NG2";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem5)
                    {
                        NgItem = "NG3";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem6)
                    {
                        NgItem = "NG4";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem7)
                    {
                        NgItem = "NG5";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem8)
                    {
                        NgItem = "NG6";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem9)
                    {
                        NgItem = "NG7";
                    }
                    if ((string)item["NGITEM"] == SampleNgitem10)
                    {
                        NgItem = "NG8";
                    }
                    ushort FlexNum = 0;
                    if ((string)item["MNO"] == (Barsamrec_Mno + "Flex").ToUpper() + "1")
                    {
                        FlexNum = 1;
                    }
                    if ((string)item["MNO"] == (Barsamrec_Mno + "Flex").ToUpper() + "2")
                    {
                        FlexNum = 2;
                    }
                    if ((string)item["MNO"] == (Barsamrec_Mno + "Flex").ToUpper() + "3")
                    {
                        FlexNum = 3;
                    }
                    if ((string)item["MNO"] == (Barsamrec_Mno + "Flex").ToUpper() + "4")
                    {
                        FlexNum = 4;
                    }
                    if (epsonRC90.TestSendStatus && FlexNum > 0)
                    {
                        await epsonRC90.TestSentNet.SendAsync("SelectSampleResultfromDt;"+ FlexNum.ToString() + ";" + NgItem);
                    }
                }
            }
            await Task.Delay(100);
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync("SelectSampleResultfromDtFinish");
            }
            //保存样本数据到本地
            SaveSampleRecordLocal();
        }
        private void EPSONSampleHaveProcess(string str)
        {
            string[] strs = str.Split(',');
            try
            {
                switch (strs[2])
                {
                    case "0":
                        SampleHave1 = bool.Parse(strs[1]);
                        Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave1", SampleHave1.ToString());
                        break;
                    case "1":
                        SampleHave2 = bool.Parse(strs[1]);
                        Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave2", SampleHave2.ToString());
                        break;
                    case "2":
                        SampleHave3 = bool.Parse(strs[1]);
                        Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave3", SampleHave3.ToString());
                        break;
                    case "3":
                        SampleHave4 = bool.Parse(strs[1]);
                        Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleHave4", SampleHave4.ToString());
                        break;
                    default:
                        break;
                }
            }
            catch 
            {

              
            }



        }
        private async void EPSONEPSONGRRTimesAskProcess(string str)
        {
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync("GRRTimesAsk;" + PcsGrrNeedNum.ToString() + ";" + PcsGrrNeedCount.ToString());
            }
        }
        private void ModelPrintEventProcess(string str)
        {
            Msg = messagePrint.AddMessage(str);
            switch (str)
            {
                case "MsgRev: 请确认，不得取走上料盘产品":
                    ShowAlarmTextGrid("请确认，\n不得取走上料盘产品");
                    break;
                case "MsgRev: 测试机1，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[0].吸取失败 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试机1，吸取失败");
                    }
                    ShowAlarmTextGrid("测试机1，吸取失败\n请将产品放回原位");
                    //addAlarm("测试机1，吸取失败");
                    break;
                case "MsgRev: 测试机2，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[1].吸取失败 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试机2，吸取失败");
                    }
                    ShowAlarmTextGrid("测试机2，吸取失败\n请将产品放回原位");
                    //addAlarm("测试机2，吸取失败");
                    break;
                case "MsgRev: 测试机3，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[2].吸取失败 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试机3，吸取失败");
                    }
                    ShowAlarmTextGrid("测试机3，吸取失败\n请将产品放回原位");
                    //addAlarm("测试机3，吸取失败");
                    break;
                case "MsgRev: 测试机4，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[3].吸取失败 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试机4，吸取失败");
                    }
                    ShowAlarmTextGrid("测试机4，吸取失败\n请将产品放回原位");
                    //addAlarm("测试机4，吸取失败");
                    break;
                case "MsgRev: 上料盘1，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        SaveCSVfileAlarm("上料盘1，吸取失败");
                        alarmTableItemsList[4].吸取失败 += 1;
                        WriteAlarmRecord();
                    }
                    ShowAlarmTextGrid("上料盘1，吸取失败\n请将产品放回原位");
                    //addAlarm("上料盘，吸取失败");
                    break;
                case "MsgRev: 上料盘2，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        SaveCSVfileAlarm("上料盘2，吸取失败");
                        alarmTableItemsList[5].吸取失败 += 1;
                        WriteAlarmRecord();
                    }
                    ShowAlarmTextGrid("上料盘2，吸取失败\n请将产品放回原位");
                    //addAlarm("上料盘，吸取失败");
                    break;
                case "MsgRev: 上料盘3，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        SaveCSVfileAlarm("上料盘3，吸取失败");
                        alarmTableItemsList[6].吸取失败 += 1;
                        WriteAlarmRecord();
                    }
                    ShowAlarmTextGrid("上料盘3，吸取失败\n请将产品放回原位");
                    //addAlarm("上料盘，吸取失败");
                    break;
                case "MsgRev: 上料盘4，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        SaveCSVfileAlarm("上料盘4，吸取失败");
                        alarmTableItemsList[7].吸取失败 += 1;
                        WriteAlarmRecord();
                    }
                    ShowAlarmTextGrid("上料盘4，吸取失败\n请将产品放回原位");
                    //addAlarm("上料盘，吸取失败");
                    break;
                case "MsgRev: 上料盘5，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        SaveCSVfileAlarm("上料盘5，吸取失败");
                        alarmTableItemsList[8].吸取失败 += 1;
                        WriteAlarmRecord();
                    }
                    ShowAlarmTextGrid("上料盘5，吸取失败\n请将产品放回原位");
                    //addAlarm("上料盘，吸取失败");
                    break;
                case "MsgRev: 上料盘6，吸取失败":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        SaveCSVfileAlarm("上料盘6，吸取失败");
                        alarmTableItemsList[9].吸取失败 += 1;
                        WriteAlarmRecord();
                    }
                    ShowAlarmTextGrid("上料盘6，吸取失败\n请将产品放回原位");
                    //addAlarm("上料盘，吸取失败");
                    break;
                //case "MsgRev: 蚀刻不良":
                //    ShowAlarmTextGrid("蚀刻不良");
                //    break;
                //case "MsgRev: 扫码不良":
                //    ShowAlarmTextGrid("扫码不良");
                //    break;
                case "MsgRev: 测试机1，测试超时":
                    ShowAlarmTextGrid("测试机1，测试超时");
                    //addAlarm("测试机1，测试超时");
                    alarmTableItemsList[0].测试机超时 += 1;
                    WriteAlarmRecord();
                    SaveCSVfileAlarm("测试机1，测试超时");
                    break;
                case "MsgRev: 测试机2，测试超时":
                    ShowAlarmTextGrid("测试机2，测试超时");
                    //addAlarm("测试机2，测试超时");
                    alarmTableItemsList[1].测试机超时 += 1;
                    WriteAlarmRecord();
                    SaveCSVfileAlarm("测试机2，测试超时");
                    break;
                case "MsgRev: 测试机3，测试超时":
                    ShowAlarmTextGrid("测试机3，测试超时");
                    //addAlarm("测试机3，测试超时");
                    alarmTableItemsList[2].测试机超时 += 1;
                    WriteAlarmRecord();
                    SaveCSVfileAlarm("测试机3，测试超时");
                    break;
                case "MsgRev: 测试机4，测试超时":
                    ShowAlarmTextGrid("测试机4，测试超时");
                    //addAlarm("测试机4，测试超时");
                    alarmTableItemsList[3].测试机超时 += 1;
                    WriteAlarmRecord();
                    SaveCSVfileAlarm("测试机4，测试超时");
                    break;
                case "MsgRev: 测试机1，连续NG":
                    ShowAlarmTextGrid("测试机1，连续NG");
                    //addAlarm("测试机1，连续NG");
                    //alarmTableItemsList[0].连续NG += 1;
                    //WriteAlarmRecord();
                    //SaveCSVfileAlarm("测试机1，连续NG");
                    break;
                case "MsgRev: 测试机2，连续NG":
                    ShowAlarmTextGrid("测试机2，连续NG");
                    //addAlarm("测试机2，连续NG");
                    //alarmTableItemsList[1].连续NG += 1;
                    //WriteAlarmRecord();
                    //SaveCSVfileAlarm("测试机2，连续NG");
                    break;
                case "MsgRev: 测试机3，连续NG":
                    ShowAlarmTextGrid("测试机3，连续NG");
                    //addAlarm("测试机3，连续NG");
                    //alarmTableItemsList[2].连续NG += 1;
                    //WriteAlarmRecord();
                    //SaveCSVfileAlarm("测试机3，连续NG");
                    break;
                case "MsgRev: 测试机4，连续NG":
                    ShowAlarmTextGrid("测试机4，连续NG");
                    //addAlarm("测试机4，连续NG");
                    //alarmTableItemsList[3].连续NG += 1;
                    //WriteAlarmRecord();
                    //SaveCSVfileAlarm("测试机4，连续NG");
                    break;
                case "MsgRev: 单穴测试，一次完成":
                    SingleTestTimes++;
                    break;
                case "MsgRev: 测试工位1，产品没放好":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[0].产品没放好 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试工位1，产品没放好");
                    }
                    ShowAlarmTextGrid("测试工位1，产品没放好");
                    //addAlarm("测试工位1，产品没放好");
                    break;
                case "MsgRev: 测试工位2，产品没放好":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[1].产品没放好 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试工位2，产品没放好");
                    }
                    ShowAlarmTextGrid("测试工位2，产品没放好");
                    //addAlarm("测试工位2，产品没放好");
                    break;
                case "MsgRev: 测试工位3，产品没放好":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[2].产品没放好 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试工位3，产品没放好");
                    }
                    ShowAlarmTextGrid("测试工位3，产品没放好");
                    //addAlarm("测试工位3，产品没放好");
                    break;
                case "MsgRev: 测试工位4，产品没放好":
                    if (lastAlarmString != str)
                    {
                        lastAlarmString = str;
                        alarmTableItemsList[3].产品没放好 += 1;
                        WriteAlarmRecord();
                        SaveCSVfileAlarm("测试工位4，产品没放好");
                    }
                    ShowAlarmTextGrid("测试工位4，产品没放好");
                    //addAlarm("测试工位4，产品没放好");
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
                    //addAlarm("A爪手掉料");
                    SaveCSVfileAlarm("A爪手掉料");
                    break;
                case "MsgRev: B爪手掉料":
                    ShowAlarmTextGrid("B爪手掉料");
                    //addAlarm("B爪手掉料");
                    SaveCSVfileAlarm("B爪手掉料");
                    break;
                case "MsgRev: 清洁操作，结束":
                    DateTimeUtility.GetLocalTime(ref lastchuiqi);
                    LastChuiqiTimeStr = lastchuiqi.ToDateTime().ToString();
                    SaveLastSamplTimetoIni();
                    AllowCleanActionCommand = true;
                    break;
                case "MsgRev: 样本测试，开始":
                    Testerwith4item.IsInSampleMode = true;
                    break;
                case "MsgRev: 样本测试，结束":
                    DateTimeUtility.GetLocalTime(ref lastSample);
                    LastSampleTestTimeStr = lastSample.ToDateTime().ToString();
                    SaveLastSamplTimetoIni();
                    AllowSampleTestCommand = true;
                    
                    Testerwith4item.IsInSampleMode = false;
                    break;
                case "MsgRev: 样本测试错误":
                    SampleRetestButtonVisibility = "Visible";
                    ShowAlarmTextGrid("样本测试错误");
                    //addAlarm("样本测试错误");
                    SaveCSVfileAlarm("样本测试错误");
                    //DateTimeUtility.GetLocalTime(ref lastSample);
                    //LastSampleTestTimeStr = lastSample.ToDateTime().ToString();
                    //SaveLastSamplTimetoIni();
                    //AllowSampleTestCommand = true;
                    //SaveSampleRecordLocal();
                    //Testerwith4item.IsInSampleMode = false;
                    break;
                case "MsgRev: 样本盘缺料":                 
                    ShowAlarmTextGrid("样本盘缺料");
                    //addAlarm("样本盘缺料");
                    SaveCSVfileAlarm("样本盘缺料");
                    break;
                case "MsgRev: 测试机有料，请清空":
                    ShowAlarmTextGrid("测试机有料，请清空");
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
            SaveScanBarcodetoCSV(bar);
        }
        private void StartUpdateProcess(int index)
        {
            TestRecord tr = new TestRecord(DateTime.Now.ToString(), epsonRC90.testerwith4item[index / 2].TesterBracode[index % 2], epsonRC90.testerwith4item[index / 2].testResult[index % 2].ToString(), epsonRC90.testerwith4item[index / 2].TestSpan[index % 2].ToString() + " s", (index + 1).ToString());
            lock (this)
            {
                myTestRecordQueue.Enqueue(tr);
            }
            //testRecord.Add(tr);
            SaveCSVfileRecord(tr);
            Msg = messagePrint.AddMessage("测试机 " + (index + 1).ToString() + " 测试完成");
        }
        private async void EPSONDBSearchEventProcess(string pickstr)
        {
            string NgItem = "Error";
            switch (pickstr)
            {
                case "A":
                    Barsaminfo_Barcode = epsonRC90.PickBracodeA;
                    DBSearch_Barcode = epsonRC90.PickBracodeA;
                    if (LookforDt(DBSearch_Barcode, 0))
                    {
                        //NgItem = (string)SinglDt.Rows[0]["NGITEM"];
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem1)
                        {
                            NgItem = "OK";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem2)
                        {
                            NgItem = "NG";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem3)
                        {
                            NgItem = "NG1";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem4)
                        {
                            NgItem = "NG2";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem5)
                        {
                            NgItem = "NG3";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem6)
                        {
                            NgItem = "NG4";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem7)
                        {
                            NgItem = "NG5";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem8)
                        {
                            NgItem = "NG6";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem9)
                        {
                            NgItem = "NG7";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem10)
                        {
                            NgItem = "NG8";
                        }
                        //switch ((string)SinglDt.Rows[0]["NGITEM"])
                        //{
                        //    case "PASS":
                        //        NgItem = "OK";
                        //        break;
                        //    case "X758FlexOpened":
                        //        NgItem = "NG";
                        //        break;
                        //    case "PixelOpenShortTest_OOS":
                        //        NgItem = "NG1";
                        //        break;
                        //    case "PowerTest_OOS":
                        //        NgItem = "NG2";
                        //        break;
                        //    case "NG3":
                        //        NgItem = "NG3";
                        //        break;
                        //    case "NG4":
                        //        NgItem = "NG4";
                        //        break;
                        //    case "NG5":
                        //        NgItem = "NG5";
                        //        break;
                        //    case "NG6":
                        //        NgItem = "NG6";
                        //        break;
                        //    case "NG7":
                        //        NgItem = "NG7";
                        //        break;
                        //    case "NG8":
                        //        NgItem = "NG8";
                        //        break;
                        //    default:
                        //        NgItem = "Error";
                        //        break;
                        //}
                        if (epsonRC90.TestSendStatus)
                        {
                            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;A;" + NgItem);
                        }

                    }
                    else
                    {
                        NgItem = "Error";
                        if (epsonRC90.TestSendStatus)
                        {
                            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;A;" + NgItem);
                        }
                        //bool r = await mydialog.showconfirm("样本数据库查询 失败。是否录入样本");
                        //SampleAlarm_IsNeedCheckin = true;
                        //await WaitSampleAlarmIsNeedCheckinProcess();
                        //if (NeedCheckin)
                        //{
                        //    //等待录入操作
                        //    bool rr = await WaitCheckinProcess();
                        //    if (rr)
                        //    {
                        //        NgItem = SamNgItemsTableNames[SamNgItemsTableIndex];
                        //        if (epsonRC90.TestSendStatus)
                        //        {
                        //            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;A;" + NgItem);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        NgItem = "Error";
                        //        if (epsonRC90.TestSendStatus)
                        //        {
                        //            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;A;" + NgItem);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    NgItem = "Error";
                        //    if (epsonRC90.TestSendStatus)
                        //    {
                        //        await epsonRC90.TestSentNet.SendAsync("SamDBSearch;A;" + NgItem);
                        //    }
                        //}
                    }
                    break;
                case "B":
                    Barsaminfo_Barcode = epsonRC90.PickBracodeB;
                    DBSearch_Barcode = epsonRC90.PickBracodeB;
                    if (LookforDt(DBSearch_Barcode, 0))
                    {
                        //NgItem = (string)SinglDt.Rows[0]["NGITEM"];
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem1)
                        {
                            NgItem = "OK";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem2)
                        {
                            NgItem = "NG";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem3)
                        {
                            NgItem = "NG1";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem4)
                        {
                            NgItem = "NG2";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem5)
                        {
                            NgItem = "NG3";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem6)
                        {
                            NgItem = "NG4";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem7)
                        {
                            NgItem = "NG5";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem8)
                        {
                            NgItem = "NG6";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem9)
                        {
                            NgItem = "NG7";
                        }
                        if ((string)SinglDt.Rows[0]["NGITEM"] == SampleNgitem10)
                        {
                            NgItem = "NG8";
                        }
                        //switch ((string)SinglDt.Rows[0]["NGITEM"])
                        //{
                        //    case "PASS":
                        //        NgItem = "OK";
                        //        break;
                        //    case "X758FlexOpened":
                        //        NgItem = "NG";
                        //        break;
                        //    case "PixelOpenShortTest_OOS":
                        //        NgItem = "NG1";
                        //        break;
                        //    case "PowerTest_OOS":
                        //        NgItem = "NG2";                             
                        //        break;
                        //    case "NG3":
                        //        NgItem = "NG3";
                        //        break;
                        //    case "NG4":
                        //        NgItem = "NG4";
                        //        break;
                        //    case "NG5":
                        //        NgItem = "NG5";
                        //        break;
                        //    case "NG6":
                        //        NgItem = "NG6";
                        //        break;
                        //    case "NG7":
                        //        NgItem = "NG7";
                        //        break;
                        //    case "NG8":
                        //        NgItem = "NG8";
                        //        break;
                        //    default:
                        //        NgItem = "Error";
                        //        break;
                        //}
                        if (epsonRC90.TestSendStatus)
                        {
                            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;B;" + NgItem);
                        }

                    }
                    else
                    {
                        NgItem = "Error";
                        if (epsonRC90.TestSendStatus)
                        {
                            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;B;" + NgItem);
                        }
                        //bool r = await mydialog.showconfirm("样本数据库查询 失败。是否录入样本");
                        //SampleAlarm_IsNeedCheckin = true;
                        //await WaitSampleAlarmIsNeedCheckinProcess();
                        //if (NeedCheckin)
                        //{
                        //    //等待录入操作
                        //    bool rr = await WaitCheckinProcess();
                        //    if (rr)
                        //    {
                        //        NgItem = SamNgItemsTableNames[SamNgItemsTableIndex];
                        //        if (epsonRC90.TestSendStatus)
                        //        {
                        //            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;B;" + NgItem);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        NgItem = "Error";
                        //        if (epsonRC90.TestSendStatus)
                        //        {
                        //            await epsonRC90.TestSentNet.SendAsync("SamDBSearch;B;" + NgItem);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    NgItem = "Error";
                        //    if (epsonRC90.TestSendStatus)
                        //    {
                        //        await epsonRC90.TestSentNet.SendAsync("SamDBSearch;B;" + NgItem);
                        //    }
                        //}
                    }
                    break;
                default:
                    break;
            }
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
            try
            {
                if (!Directory.Exists(@"E:\images"))
                {
                    Directory.CreateDirectory(@"E:\images");
                }
            }
            catch (Exception ex)
            {

                Log.Default.Error(@"CreateDirectory E:\images", ex.Message);
            }

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
                IsTestersSample = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "IsTestersSample", "False"));

                lastchuiqi.wDay = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wDay", "13"));
                lastchuiqi.wDayOfWeek = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wDayOfWeek", "0"));
                lastchuiqi.wHour = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wHour", "17"));
                lastchuiqi.wMilliseconds = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wMilliseconds", "273"));
                lastchuiqi.wMinute = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wMinute", "5"));
                lastchuiqi.wMonth = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wMonth", "11"));
                lastchuiqi.wSecond = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wSecond", "55"));
                lastchuiqi.wYear = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wYear", "2016"));
                LastChuiqiTimeStr = lastchuiqi.ToDateTime().ToString();

                lastSample.wDay = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "wDay", "13"));
                lastSample.wDayOfWeek = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "wDayOfWeek", "0"));
                lastSample.wHour = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "wHour", "17"));
                lastSample.wMilliseconds = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Chuiqi", "wMilliseconds", "273"));
                lastSample.wMinute = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "wMinute", "5"));
                lastSample.wMonth = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "wMonth", "11"));
                lastSample.wSecond = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "wSecond", "55"));
                lastSample.wYear = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "wYear", "2016"));
                LastSampleTestTimeStr = lastSample.ToDateTime().ToString();

                SQL_ora_server = Inifile.INIGetStringValue(iniParameterPath, "Oracle", "Server", "zdtdb");
                SQL_ora_user = Inifile.INIGetStringValue(iniParameterPath, "Oracle", "User", "ictdata");
                SQL_ora_pwd = Inifile.INIGetStringValue(iniParameterPath, "Oracle", "Passwold", "ictdata*168");

                SampleHave1 = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "SampleHave1", "False"));
                SampleHave2 = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "SampleHave2", "False"));
                SampleHave3 = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "SampleHave3", "False"));
                SampleHave4 = bool.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "SampleHave4", "False"));

                Barsaminfo_Partnum = Barsamrec_Partnum = Inifile.INIGetStringValue(iniParameterPath, "Sample", "PARTNUM", "CA9");
                Barsamrec_Mno = Inifile.INIGetStringValue(iniParameterPath, "Sample", "MNO", "L1");
                Barsamrec_ID1 = Inifile.INIGetStringValue(iniParameterPath, "Sample", "MNO1", "764099");
                Barsamrec_ID2 = Inifile.INIGetStringValue(iniParameterPath, "Sample", "MNO2", "764099");
                Barsamrec_ID3 = Inifile.INIGetStringValue(iniParameterPath, "Sample", "MNO3", "764099");
                Barsamrec_ID4 = Inifile.INIGetStringValue(iniParameterPath, "Sample", "MNO4", "764099");

                SampleTimeElapse = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "SampleTimeElapse","2"));
                //SampleNgitemsNum
                SampleNgitemsNum = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "Sample", "SampleNgitemsNum", "2"));
                PcsGrrNeedNum = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "GRR", "PcsGrrNeedNum", "4"));
                PcsGrrNeedCount = ushort.Parse(Inifile.INIGetStringValue(iniParameterPath, "GRR", "PcsGrrNeedCount", "4"));
                //SampleNgitem1
                SampleNgitem1 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem1", "PASS");
                SampleNgitem2 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem2", "X758FlexOpened");
                SampleNgitem3 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem3", "PixelOpenShortTest_OOS");
                SampleNgitem4 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem4", "PowerTest_OOS");
                SampleNgitem5 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem5", "NG3");
                SampleNgitem6 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem6", "NG4");
                SampleNgitem7 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem7", "NG5");
                SampleNgitem8 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem8", "NG6");
                SampleNgitem9 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem9", "NG7");
                SampleNgitem10 = Inifile.INIGetStringValue(iniParameterPath, "SampleNgitems", "SampleNgitem10", "NG8");

                PassMid = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "PassYield", "PassMid", "98"));
                PassLowLimit = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "PassYield", "PassLowLimit", "94"));

                FlexTestTimeout = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "FlexTest", "FlexTestTimeout", "100"));
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
                Inifile.INIWriteValue(iniParameterPath, "Sample", "IsTestersSample", IsTestersSample.ToString());

                Inifile.INIWriteValue(iniParameterPath, "Oracle", "Server", SQL_ora_server);
                Inifile.INIWriteValue(iniParameterPath, "Oracle", "User", SQL_ora_user);
                Inifile.INIWriteValue(iniParameterPath, "Oracle", "Passwold", SQL_ora_pwd);

                Inifile.INIWriteValue(iniParameterPath, "Sample", "PARTNUM", Barsamrec_Partnum);
                Inifile.INIWriteValue(iniParameterPath, "Sample", "MNO", Barsamrec_Mno);
                Inifile.INIWriteValue(iniParameterPath, "Sample", "MNO1", Barsamrec_ID1);
                Inifile.INIWriteValue(iniParameterPath, "Sample", "MNO2", Barsamrec_ID2);
                Inifile.INIWriteValue(iniParameterPath, "Sample", "MNO3", Barsamrec_ID3);
                Inifile.INIWriteValue(iniParameterPath, "Sample", "MNO4", Barsamrec_ID4);

                Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleTimeElapse", SampleTimeElapse.ToString());

                Inifile.INIWriteValue(iniParameterPath, "Sample", "SampleNgitemsNum", SampleNgitemsNum.ToString());
                Inifile.INIWriteValue(iniParameterPath, "GRR", "PcsGrrNeedNum", PcsGrrNeedNum.ToString());
                Inifile.INIWriteValue(iniParameterPath, "GRR", "PcsGrrNeedCount", PcsGrrNeedCount.ToString());
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem1", SampleNgitem1);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem2", SampleNgitem2);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem3", SampleNgitem3);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem4", SampleNgitem4);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem5", SampleNgitem5);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem6", SampleNgitem6);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem7", SampleNgitem7);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem8", SampleNgitem8);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem9", SampleNgitem9);
                Inifile.INIWriteValue(iniParameterPath, "SampleNgitems", "SampleNgitem10", SampleNgitem10);

                Inifile.INIWriteValue(iniParameterPath, "PassYield", "PassMid", PassMid.ToString());
                Inifile.INIWriteValue(iniParameterPath, "PassYield", "PassLowLimit", PassLowLimit.ToString());
                Inifile.INIWriteValue(iniParameterPath, "FlexTest", "FlexTestTimeout", FlexTestTimeout.ToString());

                return true;
            }
            catch (Exception ex)
            {
                Log.Default.Error("WriteParameter", ex);
                return false;
            }
        }
        private void ReadAlarmRecord()
        {
            //iniAlarmRecordPath
            try
            {
                AlarmLastDayofYear = int.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "Alarm", "AlarmLastDayofYear", "0"));
                AlarmLastDateNameStr = Inifile.INIGetStringValue(iniAlarmRecordPath, "Alarm", "AlarmLastDayofYear", "2017年5月5日");


                alarmTableItemsList[0].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴1", "吸取失败", "100"));
                alarmTableItemsList[0].产品没放好 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴1", "产品没放好", "0"));
                alarmTableItemsList[0].测试机超时 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴1", "测试机超时", "0"));
                //alarmTableItemsList[0].连续NG = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴1", "连续NG", "0"));

                alarmTableItemsList[1].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴2", "吸取失败", "0"));
                alarmTableItemsList[1].产品没放好 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴2", "产品没放好", "0"));
                alarmTableItemsList[1].测试机超时 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴2", "测试机超时", "0"));
                //alarmTableItemsList[1].连续NG = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴2", "连续NG", "0"));

                alarmTableItemsList[2].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴3", "吸取失败", "0"));
                alarmTableItemsList[2].产品没放好 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴3", "产品没放好", "0"));
                alarmTableItemsList[2].测试机超时 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴3", "测试机超时", "0"));
                //alarmTableItemsList[2].连续NG = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴3", "连续NG", "0"));

                alarmTableItemsList[3].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴4", "吸取失败", "0"));
                alarmTableItemsList[3].产品没放好 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴4", "产品没放好", "0"));
                alarmTableItemsList[3].测试机超时 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴4", "测试机超时", "0"));
                //alarmTableItemsList[3].连续NG = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "测试机穴4", "连续NG", "0"));

                alarmTableItemsList[4].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "上料盘位1", "吸取失败", "0"));
                alarmTableItemsList[5].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "上料盘位2", "吸取失败", "0"));
                alarmTableItemsList[6].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "上料盘位3", "吸取失败", "0"));
                alarmTableItemsList[7].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "上料盘位4", "吸取失败", "0"));
                alarmTableItemsList[8].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "上料盘位5", "吸取失败", "0"));
                alarmTableItemsList[9].吸取失败 = ushort.Parse(Inifile.INIGetStringValue(iniAlarmRecordPath, "上料盘位6", "吸取失败", "0"));
            }
            catch (Exception ex)
            {
                Log.Default.Error("ReadAlarmRecord", ex);
            }
 
        }
        private void WriteAlarmRecord()
        {
            try
            {
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴1", "吸取失败", alarmTableItemsList[0].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴1", "产品没放好", alarmTableItemsList[0].产品没放好.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴1", "测试机超时", alarmTableItemsList[0].测试机超时.ToString());
                //Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴1", "连续NG", alarmTableItemsList[0].连续NG.ToString());

                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴2", "吸取失败", alarmTableItemsList[1].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴2", "产品没放好", alarmTableItemsList[1].产品没放好.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴2", "测试机超时", alarmTableItemsList[1].测试机超时.ToString());
                //Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴2", "连续NG", alarmTableItemsList[1].连续NG.ToString());

                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴3", "吸取失败", alarmTableItemsList[2].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴3", "产品没放好", alarmTableItemsList[2].产品没放好.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴3", "测试机超时", alarmTableItemsList[2].测试机超时.ToString());
                //Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴3", "连续NG", alarmTableItemsList[2].连续NG.ToString());

                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴4", "吸取失败", alarmTableItemsList[3].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴4", "产品没放好", alarmTableItemsList[3].产品没放好.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴4", "测试机超时", alarmTableItemsList[3].测试机超时.ToString());
                //Inifile.INIWriteValue(iniAlarmRecordPath, "测试机穴4", "连续NG", alarmTableItemsList[3].连续NG.ToString());

                Inifile.INIWriteValue(iniAlarmRecordPath, "上料盘位1", "吸取失败", alarmTableItemsList[4].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "上料盘位2", "吸取失败", alarmTableItemsList[5].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "上料盘位3", "吸取失败", alarmTableItemsList[6].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "上料盘位4", "吸取失败", alarmTableItemsList[7].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "上料盘位5", "吸取失败", alarmTableItemsList[8].吸取失败.ToString());
                Inifile.INIWriteValue(iniAlarmRecordPath, "上料盘位6", "吸取失败", alarmTableItemsList[9].吸取失败.ToString());
            }
            catch (Exception ex)
            {
                Log.Default.Error("WriteAlarmRecord", ex);
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
            string _AlarmStr = "";
            //bool TwincatNeedAlarm = false;
            while (true)
            {
                await Task.Delay(200);
                TestSendPortStatus = epsonRC90.TestSendStatus;
                TestRevPortStatus = epsonRC90.TestReceiveStatus;
                MsgRevPortStatus = epsonRC90.MsgReceiveStatus;
                CtrlPortStatus = epsonRC90.CtrlStatus;
                IsTCPConnect = TestSendPortStatus & TestRevPortStatus & MsgRevPortStatus & CtrlPortStatus;

                try
                {
                    TestTime0 = epsonRC90.testerwith4item[0].TestSpan[0];
                    TestCount0 = epsonRC90.testerwith4item[0].TestCount[0];
                    PassCount0 = epsonRC90.testerwith4item[0].PassCount[0];
                    FailCount0 = epsonRC90.testerwith4item[0].FailCount[0];
                    Yield0 = epsonRC90.testerwith4item[0].Yield[0];
                    TesterBracodeAL = epsonRC90.testerwith4item[0].TesterBracode[0];

                    TestTime1 = epsonRC90.testerwith4item[0].TestSpan[1];
                    TestCount1 = epsonRC90.testerwith4item[0].TestCount[1];
                    PassCount1 = epsonRC90.testerwith4item[0].PassCount[1];
                    FailCount1 = epsonRC90.testerwith4item[0].FailCount[1];
                    Yield1 = epsonRC90.testerwith4item[0].Yield[1];
                    TesterBracodeAR = epsonRC90.testerwith4item[0].TesterBracode[1];

                    TestTime2 = epsonRC90.testerwith4item[1].TestSpan[0];
                    TestCount2 = epsonRC90.testerwith4item[1].TestCount[0];
                    PassCount2 = epsonRC90.testerwith4item[1].PassCount[0];
                    FailCount2 = epsonRC90.testerwith4item[1].FailCount[0];
                    Yield2 = epsonRC90.testerwith4item[1].Yield[0];
                    TesterBracodeBL = epsonRC90.testerwith4item[1].TesterBracode[0];

                    TestTime3 = epsonRC90.testerwith4item[1].TestSpan[1];
                    TestCount3 = epsonRC90.testerwith4item[1].TestCount[1];
                    PassCount3 = epsonRC90.testerwith4item[1].PassCount[1];
                    FailCount3 = epsonRC90.testerwith4item[1].FailCount[1];
                    Yield3 = epsonRC90.testerwith4item[1].Yield[1];
                    TesterBracodeBR = epsonRC90.testerwith4item[1].TesterBracode[1];

                    TestCount0_Nomal = epsonRC90.testerwith4item[0].TestCount_Nomal[0];
                    PassCount0_Nomal = epsonRC90.testerwith4item[0].PassCount_Nomal[0];
                    FailCount0_Nomal = epsonRC90.testerwith4item[0].FailCount_Nomal[0];
                    Yield0_Nomal = epsonRC90.testerwith4item[0].Yield_Nomal[0];

                    TestCount1_Nomal = epsonRC90.testerwith4item[0].TestCount_Nomal[1];
                    PassCount1_Nomal = epsonRC90.testerwith4item[0].PassCount_Nomal[1];
                    FailCount1_Nomal = epsonRC90.testerwith4item[0].FailCount_Nomal[1];
                    Yield1_Nomal = epsonRC90.testerwith4item[0].Yield_Nomal[1];

                    TestCount2_Nomal = epsonRC90.testerwith4item[1].TestCount_Nomal[0];
                    PassCount2_Nomal = epsonRC90.testerwith4item[1].PassCount_Nomal[0];
                    FailCount2_Nomal = epsonRC90.testerwith4item[1].FailCount_Nomal[0];
                    Yield2_Nomal = epsonRC90.testerwith4item[1].Yield_Nomal[0];

                    TestCount3_Nomal = epsonRC90.testerwith4item[1].TestCount_Nomal[1];
                    PassCount3_Nomal = epsonRC90.testerwith4item[1].PassCount_Nomal[1];
                    FailCount3_Nomal = epsonRC90.testerwith4item[1].FailCount_Nomal[1];
                    Yield3_Nomal = epsonRC90.testerwith4item[1].Yield_Nomal[1];

                    TesterResult0 = epsonRC90.testerwith4item[0].testResult[0].ToString();
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
                    TesterResult1 = epsonRC90.testerwith4item[0].testResult[1].ToString();
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
                    TesterResult2 = epsonRC90.testerwith4item[1].testResult[0].ToString();
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
                    TesterResult3 = epsonRC90.testerwith4item[1].testResult[1].ToString();
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
                    _SuckFailedFlag = (bool)SuckFailedFlag.Value;
                    //if (_AlarmStr != (string)AlarmStr.Value)
                    //{
                    //    _AlarmStr = (string)AlarmStr.Value;
                    //    if (_AlarmStr.Length > 0)
                    //    {
                    //        Msg = messagePrint.AddMessage(_AlarmStr);
                    //    }
                    //}
                    _XYInDebug = (bool)XYInDebug.Value;
                    _FInDebug = (bool)FInDebug.Value;
                    _TInDebug = (bool)TInDebug.Value;

                    
                    _XYRDYtoDebug = (bool)XYRDYtoDebug.Value;
                    _FRDYtoDebug = (bool)FRDYtoDebug.Value;
                    _TRDYtoDebug = (bool)TRDYtoDebug.Value;
                }
                catch 
                {

                    
                }
                
            }
        }
        private async void DispatcherTimerTickUpdateUi(Object sender, EventArgs e)
        {
            if ((DateTime.Now.DayOfYear - AlarmLastDayofYear)*24 + DateTime.Now.Hour > 24)
            {
                Alarm_allowClean = true;
                ClearAlarmRecord();              
            }
            else
            {
                if (Alarm_allowClean && (DateTime.Now.Hour == 8 || DateTime.Now.Hour == 20))
                {
                    Alarm_allowClean = false;
                    ClearAlarmRecord();
                }
                else
                {
                    if (DateTime.Now.Hour != 8 && DateTime.Now.Hour != 20)
                    {
                        Alarm_allowClean = true;
                    }
                }
            }

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


            alarmTableItems.Clear();
            foreach (var item in alarmTableItemsList)
            {
                alarmTableItems.Add(item);
            }

            //Text = "{Binding PassStatusDisplay1}" Foreground = "{Binding PassStatusColor1}"
            string[] Yieldstrs1 = PassStatusProcess(Yield0_Nomal);
            PassStatusDisplay1 = "测试机1" + Yieldstrs1[0];
            PassStatusColor1 = Yieldstrs1[1];

            string[] Yieldstrs2 = PassStatusProcess(Yield1_Nomal);
            PassStatusDisplay2 = "测试机2" + Yieldstrs2[0];
            PassStatusColor2 = Yieldstrs2[1];

            string[] Yieldstrs3 = PassStatusProcess(Yield2_Nomal);
            PassStatusDisplay3 = "测试机3" + Yieldstrs3[0];
            PassStatusColor3 = Yieldstrs3[1];

            string[] Yieldstrs4 = PassStatusProcess(Yield3_Nomal);
            PassStatusDisplay4 = "测试机4" + Yieldstrs4[0];
            PassStatusColor4 = Yieldstrs4[1];

            try
            {
                if (IsTestersClean && AllowCleanActionCommand)
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
                                AllowCleanActionCommand = false;
                            }

                        }
                    }
                }

                if (IsTestersSample && AllowSampleTestCommand)
                {
                    DateTimeUtility.SYSTEMTIME ds1 = new DateTimeUtility.SYSTEMTIME();
                    DateTimeUtility.GetLocalTime(ref ds1);
                    TimeSpan ts1 = ds1.ToDateTime() - lastSample.ToDateTime();
                    if (ts1.TotalHours > SampleTimeElapse)
                    {
                        if (IsTestersSample)
                        {
                            if (epsonRC90.TestSendStatus)
                            {
                                await epsonRC90.TestSentNet.SendAsync("GONOGOAction;" + SampleNgitemsNum.ToString());
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

            //if (SampleAlarm_IsNeedCheckin)
            //{
            //    SampleAlarm_IsNeedCheckin = false;
            //    NeedCheckin = await mydialog.showconfirm("样本数据库查询 失败。是否录入样本");
            //    SampleAlarm_IsNeedCheckin_finish = true;
            //}
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
        public void WindowLoaded()
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
            //await Task.Delay(100);
            System.Threading.Thread.Sleep(100);
            CameraHcInspect();
            Msg = messagePrint.AddMessage("检测相机初始化完成");
            ConnectDBTest();
            //epsonRC90.scanCameraInit();
            //await Task.Delay(100);
            //ScanCameraInspect();
            //scanCameraInit();
            //Msg = messagePrint.AddMessage("扫码相机初始化完成");
        }
        [Initialize]
        public void L91PLCWork()
        {
            bool TakePhoteFlage = false, _TakePhoteFlage = false;
            bool _IsShieldTheDoor = false;
            bool m207 = false, M207 = false;
            bool m1220 = false, M1220 = false;
            bool m263 = false, M263 = false;
            bool m514 = false, M514 = false;
            bool m911 = false, M911 = false;

            bool beckhoff_SuckFailedFlag = false;

            bool _PLCUnload = false;
            bool _EStop = false;
            //await Task.Delay(1000);
            System.Threading.Thread.Sleep(100);
            while (true)
            {
                //414,460
                //await Task.Delay(200);
                System.Threading.Thread.Sleep(200);
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
                    M207 = XinjiePLC.readM(207);
                    if (m207 != M207)
                    {
                        m207 = M207;
                        if (M207)
                        {
                            ShowAlarmTextGrid("上料，产品，吸取失败");
                        }                        
                    }

                    M263 = XinjiePLC.readM(263);
                    if (m263 != M263)
                    {
                        m263 = M263;
                        if (M263)
                        {
                            ShowAlarmTextGrid("上料，空盘吸取失败");
                        }
                    }

                    M514 = XinjiePLC.readM(514);
                    if (m514 != M514)
                    {
                        m514 = M514;
                        if (M514)
                        {
                            ShowAlarmTextGrid("拍照吸取失败");
                        }
                    }

                    M911= XinjiePLC.readM(911);
                    if (m911 != M911)
                    {
                        m911 = M911;
                        if (M911)
                        {
                            ShowAlarmTextGrid("下料，空盘吸取失败");
                        }
                    }

                    M1220 = XinjiePLC.readM(1220);
                    if (m1220 != M1220)
                    {
                        m1220 = M1220;
                        if (M1220)
                        {
                            AlarmTextGridShow = "Collapsed";
                        }                        
                    }

                    EStop = XinjiePLC.readM(1206);
                    if (_EStop != EStop)
                    {
                        _EStop = EStop;
                        if (_EStop)
                        {
                            Msg = messagePrint.AddMessage("急停按钮被按下");
                        }
                    }
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
                        if (beckhoff_SuckFailedFlag != (bool)SuckFailedFlag.Value)
                        {
                            beckhoff_SuckFailedFlag = (bool)SuckFailedFlag.Value;
                            if ((bool)SuckFailedFlag.Value)
                            {
                                XinjiePLC.setM(1201, true);//倍福吸取失败报警
                            }
                            else
                            {
                                XinjiePLC.setM(1201, false);
                            }
                        }

                        M420.Value = XinjiePLC.readM(420);
                        M1202.Value = XinjiePLC.readM(1202);

                        double deltaY = (double)YPos.Value - (double)WaitPositionY.Value;
                        bool UnloadYSafe = deltaY < 1 && deltaY > -1;
                        XinjiePLC.setM(1204, UnloadYSafe);

                        bool FSafePosition = (double)FPos.Value > 0 && (double)FPos.Value < (double)FPosition1.Value + 1;
                        XinjiePLC.setM(1207, FSafePosition);
                    }
                    catch (Exception ex)
                    {
                        Log.Default.Error("delta_YPos.Value", ex.Message);

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
    public class X758SamCheckinData
    {
        public string Partnum { set; get; }
        public string Barcode { set; get; }
        public uint Stnum { set; get; }
        public uint Unum { set; get; }
        public string Ngitem { set; get; }
    }
    public class X758SampleResultData
    {
        public string PARTNUM { set; get; }
        public string SITEM { set; get; }
        public string BARCODE { set; get; }
        public string NGITEM { set; get; }
        public string TRES { set; get; }
        public string MNO { set; get; }
        public string CDATE { set; get; }
        public string CTIME { set; get; }
        //控制板ID
        public string SR01 { set; get; }
    }
    public class AlarmRecord
    {
        public string AlarmTime { set; get; }
        public string AlarmString { set; get; }
    }
    public class AlarmTableItem
    {
        public string 工位 { set; get; }
        public ushort 吸取失败 { set; get; }
        public ushort 产品没放好 { set; get; }
        public ushort 测试机超时 { set; get; }
        //public ushort 连续NG { set; get; }
        public AlarmTableItem(string position)
        {
            工位 = position;
            吸取失败 = 0;
            产品没放好 = 0;
            测试机超时 = 0;
            //连续NG = 0;
        }
    }
}