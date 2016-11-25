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

namespace Omicron.ViewModel
{
    [BingAutoNotify]
    public class MainDataContext : DataSource
    {
        #region 属性绑定区域
        public virtual string AboutPageVisibility { set; get; } = "Collapsed";
        public virtual string HomePageVisibility { set; get; } = "Visible";
        public virtual string ParameterPageVisibility { set; get; } = "Collapsed";
        public virtual string CameraPageVisibility { set; get; } = "Collapsed";
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
        public virtual bool EllipseTestSend { set; get; } = false;
        public virtual bool EllipseTestRev { set; get; } = false;
        public virtual bool EllipseMsgRev { set; get; } = false;
        public virtual bool EllipseCtrl { set; get; } = false;
        public virtual string EpsonIp { set; get; } = "192.168.1.2";
        public virtual int EpsonTestSendPort { set; get; } = 2000;
        public virtual int EpsonTestReceivePort { set; get; } = 2001;
        public virtual int EpsonMsgReceivePort { set; get; } = 2002;
        public virtual int EpsonRemoteControlPort { set; get; } = 5000;
        public virtual VisionImage Img { set; get; } = new VisionImage();
        public virtual string VisionScriptFileName { set; get; }
        #endregion
        #region 变量定义区域
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        private string iniParameterPath = System.Environment.CurrentDirectory + "\\Parameter.ini";
        private XinjiePlc XinjiePLC;
        private VBAIClass vBAIClass = new VBAIClass();
        #endregion
        #region 功能和方法
        public void ChoseHomePage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Visible";
            CameraPageVisibility = "Collapsed";
            //Msg = messagePrint.AddMessage("111");
        }
        public void ChoseAboutPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
        }
        public void ChoseParameterPage()
        {
            ParameterPageVisibility = "Visible";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Collapsed";
        }
        public void ChoseCameraPage()
        {
            ParameterPageVisibility = "Collapsed";
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Collapsed";
            CameraPageVisibility = "Visible";
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
        public void Selectfile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = System.Environment.CurrentDirectory;
            dlg.Filter = "视觉文件(*.vbai)|*.vbai|所有文件(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                VisionScriptFileName = dlg.FileName;
                Inifile.INIWriteValue(iniParameterPath, "Camera", "VisionScriptFileName", VisionScriptFileName);
            }
            dlg.Dispose();
        }
        #endregion
        #region 视觉
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
        }
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