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
using NationalInstruments.Vision;
using NationalInstruments.VBAI;
using NationalInstruments.VBAI.Structures;
using NationalInstruments.VBAI.Enums;
using NationalInstruments.Vision.WindowsForms;
using Omicron.Model;
using System.Windows.Forms;
using System.IO;
using ViewROI;
using HalconDotNet;
using System.Collections.ObjectModel;
using MathNet.Numerics.LinearAlgebra;

namespace Omicron.ViewModel
{
    [BingAutoNotify]
    public class MainDataContext : DataSource
    {
        #region 属性绑定区域
        public virtual string AboutPageVisibility { set; get; } = "Collapsed";
        public virtual string HomePageVisibility { set; get; } = "Collapsed";
        public virtual string ParameterPageVisibility { set; get; } = "Collapsed";
        public virtual string CameraPageVisibility { set; get; } = "Collapsed";
        public virtual string CameraHcPageVisibility { set; get; } = "Collapsed";
        public virtual string TesterParameterPageVisibility { set; get; } = "Collapsed";
        public virtual string HalconScriptPageVisibility { set; get; } = "Collapsed";
        public virtual string CalibPageVisibility { set; get; } = "Visible";
        public virtual bool IsPLCConnect { set; get; } = false;
        public virtual bool IsTCPConnect { set; get; } = false;
        public virtual bool IsShieldTheDoor { set; get; } = true;
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
        public virtual VisionImage Img { set; get; } = new VisionImage();
        public virtual string VisionScriptFileName { set; get; }
        public virtual string HcVisionScriptFileName { set; get; }
        public virtual HImage hImage { set; get; }
        public virtual ObservableCollection<HObject> hObjectList { set; get; }
        public virtual ObservableCollection<ROI> ROIList { set; get; } = new ObservableCollection<ROI>();
        public virtual int ActiveIndex { set; get; }
        public virtual bool Repaint { set; get; }

        public virtual HImage hScriptImage { set; get; }
        public virtual ObservableCollection<HObject> hScriptObjectList { set; get; }
        public virtual ObservableCollection<ROI> ScriptROIList { set; get; } = new ObservableCollection<ROI>();
        public virtual int ScriptActiveIndex { set; get; }
        public virtual bool ScriptRepaint { set; get; }

        public virtual ObservableCollection<double> ToolCoordX1 { set; get; } = new ObservableCollection<double>();
        public virtual ObservableCollection<double> ToolCoordY1 { set; get; } = new ObservableCollection<double>();
        public virtual ObservableCollection<double> PixCoordX1 { set; get; } = new ObservableCollection<double>();
        public virtual ObservableCollection<double> PixCoordY1 { set; get; } = new ObservableCollection<double>();

        public virtual double CalibXA { set; get; } = 0;
        public virtual double CalibXB { set; get; } = 0;
        public virtual double CalibXC { set; get; } = 0;
        public virtual double CalibXD { set; get; } = 0;
        public virtual double CalibXE { set; get; } = 0;
        public virtual double CalibXF { set; get; } = 0;

        public virtual double CalibYA { set; get; } = 0;
        public virtual double CalibYB { set; get; } = 0;
        public virtual double CalibYC { set; get; } = 0;
        public virtual double CalibYD { set; get; } = 0;
        public virtual double CalibYE { set; get; } = 0;
        public virtual double CalibYF { set; get; } = 0;

        public virtual double CalcPixCoordX1 { set; get; } = 0;
        public virtual double CalcPixCoordY1 { set; get; } = 0;
        public virtual double CalcToolCoordX1 { set; get; } = 0;
        public virtual double CalcToolCoordY1 { set; get; } = 0;

        public virtual double RealToolCoorX { set; get; } = 0;
        public virtual double RealToolCoorY { set; get; } = 0;
        #endregion
        #region 变量定义区域
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private XinjiePlc XinjiePLC;
        private VBAIClass vBAIClass = new VBAIClass();
        private HdevEngine hdevEngine = new HdevEngine();
        private HalconScript halconScript = new HalconScript();
        private bool isHalconScriptLoop = false;
        private EpsonRC90 epsonRC90 = new EpsonRC90();
        private double[] _ToolCoordX1 = new double[6] { 0, 0, 0, 0, 0, 0 };
        private double[] _ToolCoordY1 = new double[6] { 0, 0, 0, 0, 0, 0 };
        private double[] _PixCoordX1 = new double[6] { 0, 0, 0, 0, 0, 0 };
        private double[] _PixCoordY1 = new double[6] { 0, 0, 0, 0, 0, 0 };
        private double PixX = 0;
        private double PixY = 0;
        private double[] CalbX = new double[6] { 0, 0, 0, 0, 0, 0 };
        private double[] CalbY = new double[6] { 0, 0, 0, 0, 0, 0 };
        #endregion
        #region 构造函数
        public MainDataContext()
        {
            epsonRC90.ModelPrint += ModelPrintEventProcess;
            epsonRC90.EpsonStatusUpdate += EpsonStatusUpdateProcess;

            for (int i = 0; i < 6; i++)
            {
                ToolCoordX1.Add(_ToolCoordX1[i]);
                ToolCoordY1.Add(_ToolCoordY1[i]);
                PixCoordX1.Add(_PixCoordX1[i]);
                PixCoordY1.Add(_PixCoordY1[i]);
            }

            Async.RunFuncAsync(UpdateUI,null);
        }
        #endregion
        #region 功能和方法
        public void ChoseHomePage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Visible";
            CameraPageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            HalconScriptPageVisibility = "Collapsed";
            CalibPageVisibility = "Collapsed";
            //Msg = messagePrint.AddMessage("111");
        }
        public void ChoseAboutPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            HalconScriptPageVisibility = "Collapsed";
            CalibPageVisibility = "Collapsed";
        }
        public void ChoseParameterPage()
        {
            ParameterPageVisibility = "Visible";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            HalconScriptPageVisibility = "Collapsed";
            CalibPageVisibility = "Collapsed";
        }
        public void ChoseTesterParameterPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Visible";
            HalconScriptPageVisibility = "Collapsed";
            CalibPageVisibility = "Collapsed";
        }
        public void ChoseCameraPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Visible";
            CameraHcPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            HalconScriptPageVisibility = "Collapsed";
            CalibPageVisibility = "Collapsed";
        }
        public void ChoseCameraHcPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
            CameraHcPageVisibility = "Visible";
            TesterParameterPageVisibility = "Collapsed";
            HalconScriptPageVisibility = "Collapsed";
            CalibPageVisibility = "Collapsed";
        }
        public void ChoseHalconScriptPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            HalconScriptPageVisibility = "Visible";
            CalibPageVisibility = "Collapsed";
        }
        public void ChoseCalibPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
            CameraHcPageVisibility = "Collapsed";
            TesterParameterPageVisibility = "Collapsed";
            HalconScriptPageVisibility = "Collapsed";
            CalibPageVisibility = "Visible";
        }
        public void ShieldDoorFunction()
        {
            IsShieldTheDoor = !IsShieldTheDoor;
        }
        public void EpsonOpetate(object p)
        {

        }
        public void NoiseReduce()
        {

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
                default:
                    break;
            }
            //dlg.InitialDirectory = System.Environment.CurrentDirectory;

            dlg.Dispose();
        }
        #endregion
        #region 事件相应函数
        private void ModelPrintEventProcess(string str)
        {
            Msg = messagePrint.AddMessage(str);
        }
        private void EpsonStatusUpdateProcess(string str)
        {
            EpsonStatusAuto = str[1] == '1';
            EpsonStatusWarning = str[2] == '1';
            EpsonStatusSError = str[3] == '1';
            EpsonStatusSafeGuard = str[4] == '1';
            EpsonStatusEStop = str[5] == '1';
            EpsonStatusError = str[6] == '1';
            EpsonStatusPaused = str[7] == '1';
            EpsonStatusRunning = str[8] == '1';
            EpsonStatusReady = str[9] == '1';
        }
        #endregion
        #region 视觉
        #region VBAI
        public void CameraInit()
        {
            vBAIClass.vbaipath = VisionScriptFileName;
            Async.RunFuncAsync<bool>(vBAIClass.OpenEngine, CameraInitCallBack);
        }
        public void CameraInitCallBack(bool rst)
        {
            if (rst)
            {
                Msg = messagePrint.AddMessage("初始化相机成功");
            }
            else
            {
                Msg = messagePrint.AddMessage("初始化相机失败");
            }
        }
        public void CameraInspect()
        {
            Async.RunFuncAsync<List<StepMeasurements>>(vBAIClass.InspectEngine, CameraInspectCallBack);
        }
        public void CameraInspectCallBack(List<StepMeasurements> ms)
        {
            Img = vBAIClass.VBAIImage;
            foreach (StepMeasurements item in ms)
            {
                if (item.displayName == "Cor_X")
                {
                    PixX = item.measurement.numData;
                    Msg = messagePrint.AddMessage(PixX.ToString());
                }
                if (item.displayName == "Cor_Y")
                {
                    PixY = item.measurement.numData;
                    Msg = messagePrint.AddMessage(PixY.ToString());
                }
            }
        }
        #endregion
        #region Halcon
        public void CameraHcInit()
        {
            Async.RunFuncAsync(cameraHcInit, CameraHcInitCallBack);
        }
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
        public void CameraHcInitCallBack()
        {
            Msg = messagePrint.AddMessage("Hc相机初始化完成");
        }
        public void CameraHcInspect()
        {
            Async.RunFuncAsync(cameraHcInspect,null);
        }
        public void cameraHcInspect()
        {
            ObservableCollection<HObject> hl = new ObservableCollection<HObject>();
            hdevEngine.inspectengine();
            hImage = hdevEngine.getImage("Image");
            hl.Add(hdevEngine.getRegion("Rectangle1"));
            hl.Add(hdevEngine.getRegion("Rectangle2"));
            hObjectList = hl;           
            //roilist.Add(hdevEngine.getRegion("Rectangle1"));
        }
        #region HalconScript
        public void HcScriptInit()
        {
            halconScript.HalconScriptInit();
        }
        public void HcScriptInspectOnce()
        {
            if (!isHalconScriptLoop)
            {
                halconScript.HalconScriptGrabImage();

                hScriptImage = HalconScript.HObjectToHImage(halconScript.ho_Image);
            }
        }
        public void HcScriptInspectLoop()
        {
            if (!isHalconScriptLoop)
            {
                Async.RunFuncAsync(hcScriptInspectLoop, null);
            }
            isHalconScriptLoop = !isHalconScriptLoop;
        }
        private async void hcScriptInspectLoop()
        {
            while (true)
            {

                await Task.Delay(100);
                halconScript.HalconScriptGrabImage();

                hScriptImage = HalconScript.HObjectToHImage(halconScript.ho_Image);
                if (!isHalconScriptLoop)
                {
                    break;
                }
            }
        }
        public void HcScriptAddROI()
        {
            ROICircle rOICircle = new ROICircle();
            rOICircle.createROI(100,100,50);
            ScriptROIList.Add(rOICircle);
            ScriptActiveIndex = ScriptROIList.Count - 1;
            ScriptRepaint = !ScriptRepaint;
        }
        public void HcScriptDeleteROI()
        {
            if (ScriptActiveIndex == -1)
            {
                return;
            }
            ScriptROIList.RemoveAt(ScriptActiveIndex);
            ScriptActiveIndex = ScriptROIList.Count - 1;
            ScriptRepaint = !ScriptRepaint;
        }

        #endregion

        #endregion
        #region 标定
        public async void GetCoor(object b)
        {
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync("Coord");
            }
            await Task.Delay(100);
            int i = int.Parse(b.ToString());
            _ToolCoordX1[i] = epsonRC90.Coord_X;
            _ToolCoordY1[i] = epsonRC90.Coord_Y;
            ToolCoordX1.Clear();
            ToolCoordY1.Clear();
            for (int ii = 0; ii < 6; ii++)
            {
                ToolCoordX1.Add(_ToolCoordX1[ii]);
                ToolCoordY1.Add(_ToolCoordY1[ii]);
            }

            CameraInspect();
            await Task.Delay(500);
            _PixCoordX1[i] = PixX;
            _PixCoordY1[i] = PixY;
            PixCoordX1.Clear();
            PixCoordY1.Clear();
            for (int j = 0; j < 6; j++)
            {
                PixCoordX1.Add(_PixCoordX1[j]);
                PixCoordY1.Add(_PixCoordY1[j]);
            }
        }
        public void CameraCalib(object p)
        {
            CoorConvertParam();
        }
        private void CoorConvertParam()
        {
            var mb = Matrix<double>.Build;
            var vb = Vector<double>.Build;
            double[,] A = new double[6, 6];
            double[] B = new double[6];
            //计算X参数
            for (int i = 0; i < 6; i++)
            {
                A[i, 0] = _PixCoordX1[i];
                A[i, 1] = _PixCoordY1[i];
                A[i, 2] = Math.Pow(_PixCoordX1[i], 2);
                A[i, 3] = Math.Pow(_PixCoordY1[i], 2);
                A[i, 4] = _PixCoordX1[i] * _PixCoordY1[i];
                A[i, 5] = 1;

                B[i] = _ToolCoordX1[i];
                Console.WriteLine(B.ToString());
            }
            var mbfromArray = mb.DenseOfArray(A);
            var vbfromArray = vb.DenseOfArray(B);
            Console.WriteLine(mbfromArray.ToString());
            Console.WriteLine(vbfromArray.ToString());
            var result = mbfromArray.Inverse() * vbfromArray;
            Console.WriteLine(result.ToString());
            for (int i = 0; i < 6; i++)
            {
                CalbX[i] = result[i];
            }
            CalibXA = CalbX[0];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "XA", CalibXA.ToString());
            CalibXB = CalbX[1];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "XB", CalibXB.ToString());
            CalibXC = CalbX[2];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "XC", CalibXC.ToString());
            CalibXD = CalbX[3];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "XD", CalibXD.ToString());
            CalibXE = CalbX[4];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "XE", CalibXE.ToString());
            CalibXF = CalbX[5];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "XF", CalibXF.ToString());
            //计算Y参数
            for (int i = 0; i < 6; i++)
            {
                A[i, 0] = _PixCoordX1[i];
                A[i, 1] = _PixCoordY1[i];
                A[i, 2] = Math.Pow(_PixCoordX1[i], 2);
                A[i, 3] = Math.Pow(_PixCoordY1[i], 2);
                A[i, 4] = _PixCoordX1[i] * _PixCoordY1[i];
                A[i, 5] = 1;
                B[i] = _ToolCoordY1[i];
            }
            mbfromArray = mb.DenseOfArray(A);
            vbfromArray = vb.DenseOfArray(B);
            Console.WriteLine(mbfromArray.ToString());
            Console.WriteLine(vbfromArray.ToString());
            result = mbfromArray.Inverse() * vbfromArray;
            Console.WriteLine(result.ToString());
            for (int i = 0; i < 6; i++)
            {
                CalbY[i] = result[i];
            }
            CalibYA = CalbY[0];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "YA", CalibYA.ToString());
            CalibYB = CalbY[1];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "YB", CalibYB.ToString());
            CalibYC = CalbY[2];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "YC", CalibYC.ToString());
            CalibYD = CalbY[3];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "YD", CalibYD.ToString());
            CalibYE = CalbY[4];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "YE", CalibYE.ToString());
            CalibYF = CalbY[5];
            Inifile.INIWriteValue(iniParameterPath, "Calib1", "YF", CalibYF.ToString());
        }
        public async void CalcGetCoor(object p)
        {
            if (epsonRC90.TestSendStatus)
            {
                await epsonRC90.TestSentNet.SendAsync("Coord");
            }

            CameraInspect();
            await Task.Delay(500);
            CalcPixCoordX1 = PixX;
            CalcPixCoordY1 = PixY;
            CalcToolCoordX1 = CalibXA * CalcPixCoordX1
                + CalibXB * CalcPixCoordY1 + CalibXC * Math.Pow(CalcPixCoordX1, 2)
                + CalibXD * Math.Pow(CalcPixCoordY1, 2) + CalibXE * CalcPixCoordX1 * CalcPixCoordY1
                + CalibXF;
            CalcToolCoordY1 = CalibYA * CalcPixCoordX1
                + CalibYB * CalcPixCoordY1 + CalibYC * Math.Pow(CalcPixCoordX1, 2)
                + CalibYD * Math.Pow(CalcPixCoordY1, 2) + CalibYE * CalcPixCoordX1 * CalcPixCoordY1
                + CalibYF;

            RealToolCoorX = epsonRC90.Coord_X;
            RealToolCoorY = epsonRC90.Coord_Y;
        }
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

                CalibXA = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "XA", "0"));
                CalibXB = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "XB", "0"));
                CalibXC = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "XC", "0"));
                CalibXD = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "XD", "0"));
                CalibXE = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "XE", "0"));
                CalibXF = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "XF", "0"));

                CalibYA = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "YA", "0"));
                CalibYB = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "YB", "0"));
                CalibYC = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "YC", "0"));
                CalibYD = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "YD", "0"));
                CalibYE = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "YE", "0"));
                CalibYF = double.Parse(Inifile.INIGetStringValue(iniParameterPath, "Calib1", "YF", "0"));

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


            //ROIRectangle1 CurROI = new ROIRectangle1();
            //double x = 50; double y = 40;
            //double w = 40; double h = 40;
            //CurROI.row1 = y;
            //CurROI.col1 = x;
            //CurROI.row2 = y + h;
            //CurROI.col2 = x + w;
            //CurROI.midC = x + w / 2;
            //CurROI.midR = y + h / 2;
            ROICircle CurROI = new ROICircle();
            CurROI.createROI(100,100);
            ROIList.Add(CurROI);
            ActiveIndex = ROIList.Count - 1;
            Repaint = !Repaint;

        
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
                //var r1 = WriteParameter();
                //if (r1)
                //{
                //    Msg = messagePrint.AddMessage("写入参数成功");
                //}
                //else
                //{
                //    Msg = messagePrint.AddMessage("写入参数成功");
                //}
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
            await Task.Delay(10);
        }
        [Initialize]
        public async void L91PLCWork()
        {
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
                }
            }
        }

        #endregion

    }
}