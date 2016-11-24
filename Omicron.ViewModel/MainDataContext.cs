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

namespace Omicron.ViewModel
{
    [BingAutoNotify]
    public class MainDataContext : DataSource
    {
        public virtual string AboutPageVisibility { set; get; } = "Collapsed";
        public virtual string HomePageVisibility { set; get; } = "Visible";
        public virtual bool IsPLCConnect { set; get; } = false;
        public virtual bool IsTCPConnect { set; get; } = false;
        public virtual bool IsShieldTheDoor { set; get; } = true;
        public virtual string Msg { set; get; } = "";
        public virtual bool EpsonStatusAuto { set; get; } = true;
        public virtual bool EpsonStatusWarning { set; get; } = true;
        public virtual bool EpsonStatusSError { set; get; } = true;
        public virtual bool EpsonStatusSafeGuard { set; get; } = true;
        public virtual bool EpsonStatusEStop { set; get; } = true;
        public virtual bool EpsonStatusError { set; get; } = true;
        public virtual bool EpsonStatusPaused { set; get; } = true;
        public virtual bool EpsonStatusRunning { set; get; } = true;
        public virtual bool EpsonStatusReady { set; get; } = true;
        private MessagePrint messagePrint = new MessagePrint();
        private dialog mydialog = new dialog();
        public void ChoseHomePage()
        {
            AboutPageVisibility = "Collapsed";
            HomePageVisibility = "Visible";
            //Msg = messagePrint.AddMessage("111");
        }
        public void ChoseAboutPage()
        {
            AboutPageVisibility = "Visible";
            HomePageVisibility = "Collapsed";
        }
        public void FunctionTest()
        {

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
        [Export(MEF.Contracts.ActionMessage)]
        [ExportMetadata(MEF.Key, "winclose")]
        public async void WindowClose()
        {
            mydialog.changeaccent("Red");
            var r = await mydialog.showconfirm("确定要关闭程序吗？");
            if (r)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                mydialog.changeaccent("Cobalt");
            }
        }
    }
}