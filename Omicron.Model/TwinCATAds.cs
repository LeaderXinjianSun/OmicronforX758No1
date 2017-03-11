using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using BingLibrary.hjb;

namespace Omicron.Model
{
    public class TwinCATAds
    {
        private TcAdsClient adsClient;
        List<TwinCATCoil> ToRead = new List<TwinCATCoil>();
        List<TwinCATCoil> ToNotice = new List<TwinCATCoil>();
        string netID = "192.168.1.17.1.1";
        int srvPort = 801;
        int timeout = 5000;
        public TwinCATAds()
        {
            try
            {
                TwinCATAdsConfig();
                adsClient = new TcAdsClient();
                adsClient.AdsNotificationEx += new AdsNotificationExEventHandler(adsClient_AdsNotificationEx);
                adsClient.Connect(netID, srvPort);
                adsClient.Timeout = timeout;
            }
            catch (Exception e)
            {
                Log.Default.Error("TwinCat连接失败",e);
            }
        }
        public void TwinCATAdsConfig()
        {
            StreamReader srd;
            try
            {
                srd = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\TwinCATAdsConfig.ini", Encoding.Default, true);
            }
            catch (Exception e)
            {
                Log.Default.Error("确认TwinCATAdsConfig.ini文件是否存在?", e);
                return; }
            try
            {
                netID = srd.ReadLine();
                srvPort = int.Parse(srd.ReadLine());
                timeout = int.Parse(srd.ReadLine());
            }
            catch (Exception e) { Log.Default.Error("读取TwinCATAdsConfig.ini失败", e); }
            srd.Close();
        }
        public void AddCoil(TwinCATCoil mTwinCATCoil)
        {
            if (mTwinCATCoil.CoilMode == TwinCATCoil.Mode.Read)
                ToRead.Add(mTwinCATCoil);
            else
                ToNotice.Add(mTwinCATCoil);
            try
            {
                mTwinCATCoil.CoilHandle = adsClient.CreateVariableHandle(mTwinCATCoil.CoilName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(System.DateTime.Now.ToString() + "    " + "变量" + mTwinCATCoil.CoilName+"失败");
                  Trace.Write(ex, "AddCoil");
            }
            mTwinCATCoil.CoilUpdate += mTwinCATCoil_CoilUpdate;
        }
        //往TwinCAT写入数值
        private void mTwinCATCoil_CoilUpdate(object sender, EventArgs e)
        {
            try
            {
                TwinCATCoil temp = sender as TwinCATCoil;
                if(temp.CoilHandle==0)
                    temp.CoilHandle = adsClient.CreateVariableHandle(temp.CoilName);
                if (temp.CoilGroup != 0)//如果是数组~
                {
                    //adsClient.WriteAny(temp.CoilHandle, a, new int[] { temp.CoilGroup });//这样不行~
                    dynamic dynamicObj = Convert.ChangeType(temp.CoilValue, temp.CoilType);
                    int size = 0;
                    if (dynamicObj is bool[])//否则得出结果是4字节
                        size = 1;
                    else
                        size = Marshal.SizeOf(dynamicObj[0]);
                    AdsStream datastream = new AdsStream(size * temp.CoilGroup); //每个元素占用位
                    System.IO.BinaryWriter binwrite = new System.IO.BinaryWriter(datastream);
                    datastream.Position = 0;
                    for (int i = 0; i < temp.CoilGroup; i++)
                    {
                        binwrite.Write(dynamicObj[i]);
                    }
                    adsClient.Write(temp.CoilHandle, datastream);
                }
                else
                    adsClient.WriteAny(temp.CoilHandle, Convert.ChangeType(temp.CoilValue, temp.CoilType));
            }
            catch(Exception ex)
            {
                if (sender is TwinCATCoil)
                    Trace.Write(ex, "Update:" + (sender as TwinCATCoil).CoilName);
                else
                    Trace.Write(ex, "Update");
            }
        }
        bool mLoop=false;
        bool mState=true;
        bool mStateChanged = false;
        public bool State
        {
            get
            {
                return mState;
            }
        }
        public event EventHandler StateChanged;
        public async void StartRead()
        {
            if (ToRead.Count == 0)
            {
                mLoop = false;
                return;
            }
            if (!mLoop)
                mLoop = true;
            else
                return;
            while (mLoop)
            {
                for (int i = 0; i < ToRead.Count; i++)
                {
                    Func<System.Threading.Tasks.Task> taskFunc = () =>
                    {
                        return System.Threading.Tasks.Task.Run(() =>
                        {
                            try
                            {
                                if (mLoop == false)
                                    return;
                                if(ToRead[i].CoilHandle==0)
                                    ToRead[i].CoilHandle = adsClient.CreateVariableHandle(ToRead[i].CoilName);
                                if (ToRead[i].CoilGroup!=0)
                                {
                                    ToRead[i].CoilValue = adsClient.ReadAny(ToRead[i].CoilHandle, ToRead[i].CoilType,new int[]{ToRead[i].CoilGroup});
                                }
                                else
                                    ToRead[i].CoilValue = adsClient.ReadAny(ToRead[i].CoilHandle, ToRead[i].CoilType);
                                if (mNotice == true&&mLoop&&mState==false)
                                {
                                    StopNotice();
                                    StartNotice();
                                }
                                if (!mState)
                                {
                                    mState = true;
                                    mStateChanged = true;
                                }
                            }
                            catch(Exception ex)
                            {
                                Trace.Write(ex, "Read:" + ToRead[i].CoilName);
                                if (mState)
                                {
                                    mState = false;
                                    mStateChanged = true;
                                }
                            }
                        });
                    };
                    await taskFunc();
                    if(mStateChanged)
                    {
                        mStateChanged = false;
                        if (StateChanged != null)
                            StateChanged(this, null);
                    }
                }
            }
        }
        public void StopRead()
        {
            mLoop = false;
        }
        bool mNotice = false;
        public void StartNotice()
        {
            if (ToNotice.Count == 0)
            {
                mNotice = false;
                return;
            }
            if (!mNotice)
                mNotice = true;
            else
                return;
            for (int i = 0; i < ToNotice.Count; i++)
            {
                try
                {
                    if (ToNotice[i].CoilGroup != 0)
                        ToNotice[i].CoilNoticeHandle = adsClient.AddDeviceNotificationEx(ToNotice[i].CoilName, AdsTransMode.OnChange, ToNotice[i].CycleTime, 0, ToNotice[i], ToNotice[i].CoilType,new int[]{ToNotice[i].CoilGroup});
                    else
                        ToNotice[i].CoilNoticeHandle = adsClient.AddDeviceNotificationEx(ToNotice[i].CoilName, AdsTransMode.OnChange, ToNotice[i].CycleTime, 0, ToNotice[i], ToNotice[i].CoilType);
                }
                catch (Exception ex)
                {
                    Trace.Write(ex, "StartNotice");
                }
            }
        }
        public void StopNotice()
        {
            mNotice = false;
            for (int i = 0; i < ToNotice.Count; i++)
            {
                try
                {
                    if (ToNotice[i].CoilNoticeHandle != 0)
                    {
                        adsClient.DeleteDeviceNotification(ToNotice[i].CoilNoticeHandle);
                        ToNotice[i].CoilNoticeHandle = 0;
                    }
                }
                catch (Exception ex)
                {
                    Trace.Write(ex, "StopNotice");
                }
            }
        }
        //Notice读取值
        private void adsClient_AdsNotificationEx(object sender, AdsNotificationExEventArgs e)
        {
            TwinCATCoil temp = (TwinCATCoil)e.UserData;
            temp.CoilValue = e.Value;
        }
    }
}
