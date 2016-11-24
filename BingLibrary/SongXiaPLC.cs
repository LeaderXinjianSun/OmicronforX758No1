using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace BingLibrary.hjb
{
    //发送数据长度超过128时%改成<。
    public class SongXiaPLC
    {
        public SerialPort SerialPort1;
        private bool mClose = false;
        private System.Object SerialLock = new System.Object();
        private static readonly object obj = new object();

        public SongXiaPLC(String PortName, int BaudRate, Parity Parity, int DataBits, StopBits StopBits)
        {
            SerialPort1 = new SerialPort();
            SerialPort1.PortName = PortName;
            SerialPort1.BaudRate = BaudRate;
            SerialPort1.Parity = Parity;
            SerialPort1.DataBits = DataBits;
            SerialPort1.StopBits = StopBits;
            SerialPort1.DataReceived += SerialPort1_DataReceived;
        }

        private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = SerialPort1.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            try
            {
                SerialPort1.Read(buf, 0, n);//读取缓冲数据
            }
            catch { }
            builder.Append(Encoding.ASCII.GetString(buf));
        }

        public bool Connect()
        {
            try
            {
                if (SerialPort1.PortName != "" && SerialPort1 != null && SerialPort1.IsOpen == false)
                {
                    SerialPort1.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }

        private int mTime = 100;

        public bool ReConnect()
        {
            while (SerialPort1.PortName != "" && SerialPort1.IsOpen == false && mClose == false)
            {
                try
                {
                    SerialPort1.Open();
                }
                catch (Exception ex)
                {
                    Thread.Sleep(mTime);
                    Debug.Print("ReConnect:" + ex.Message + mTime);
                    mTime = mTime < 1000 ? mTime + 50 : 1000;
                }
            }
            return true;
        }

        public void Closed()
        {
            try
            {
                mClose = true;
                if (SerialPort1 != null)
                    SerialPort1.Close();
            }
            catch { }
        }

        public bool SetM(int mAdds, string mData, string mIndex = "01")
        {
            string madd = mAdds.ToString("X4");
            string sendData = "%" + mIndex + "#WCSR" + madd + mData;
            string bcccode = ChkBcc(sendData);
            sendData += bcccode;
            lock (obj)
            {
                try
                {
                    SerialPort1.ReadExisting();
                    SerialPort1.Write(sendData + "\r");
                    Thread.Sleep(30);
                    string received = builder.ToString();
                    builder.Clear();
                    string sComm = received.Substring(4, 2);
                    if (sComm == "WC")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
        }

        public bool ReadM(int mAdds, string mIndex = "01")
        {
            string madd = mAdds.ToString("X4");
            string sendData = "%" + mIndex + "#RCSR" + madd;
            string bcccode = ChkBcc(sendData);
            sendData += bcccode;
            lock (SerialLock)
            {
                try
                {
                    SerialPort1.ReadExisting();
                    SerialPort1.Write(sendData + "\r");
                    Thread.Sleep(30);

                    // string received = SerialPort1.ReadLine();
                    string received = builder.ToString();
                    builder.Clear();
                    string sComm = received.Substring(4, 2);
                    if (sComm == "RC")
                    {
                        if (received.Substring(6, 1) == "1")
                        {
                            return true;
                        }
                        else
                        { return false; }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
        }

        /// <summary>
        /// 批量写M
        /// </summary>
        /// <param name="mStartAdds">起始地址，十进制</param>
        /// <param name="mEndAdds">结束地址，十进制</param>
        /// <param name="mData">写入值，如M0~M15,一次则为"0000111100001111"</param>
        /// <param name="mIndex"></param>
        /// <returns></returns>
        public bool SetMS(int mStartAdds, int mEndAdds, string mData, string mIndex = "01")
        {
            string temp = "";
            string finaldata = "";
            try
            {
                for (int i = 0; i < mData.Length / 16; i++)
                {
                    temp = mData.Substring(16 * i, 16);
                    temp = ArrayReverse(temp);
                    string temp1 = temp.Substring(0 * 4, 4);
                    string temp2 = temp.Substring(1 * 4, 4);
                    string temp3 = temp.Substring(2 * 4, 4);
                    string temp4 = temp.Substring(3 * 4, 4);
                    finaldata += Convert2To16(temp3) +
                        Convert2To16(temp4) +
                        Convert2To16(temp1) +
                        Convert2To16(temp2);
                }
            }
            catch { }

            string startAdd = mStartAdds.ToString("X4");
            string endAdd = mEndAdds.ToString("X4");
            string sendData = "%" + mIndex + "#WCCR" + startAdd + endAdd + finaldata;
            string bcccode = ChkBcc(sendData);
            sendData += bcccode;
            lock (SerialLock)
            {
                try
                {
                    SerialPort1.ReadExisting();
                    SerialPort1.Write(sendData + "\r");
                    Thread.Sleep(20);

                    string received = builder.ToString();
                    builder.Clear();
                    string sComm = received.Substring(4, 2);
                    if (sComm == "WC")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
        }

        private string ChkBcc(string chkstring)
        {
            int chksum = 0;
            string chksums = "";
            int k;
            for (k = 0; k < chkstring.Length; k++)
            {
                chksum = chksum ^ Asc(chkstring.Substring(k, 1));
            }
            chksums = Convert.ToString(chksum, 16);
            if (chksums.Length == 1)
                chksums = "0" + chksums;
            return chksums.Substring(chksums.Length - 2, 2).ToUpper();
        }

        private int Asc(string c)
        {
            if (c.Length == 1)
            {
                System.Text.ASCIIEncoding asciiE = new ASCIIEncoding();
                int ascc = (int)asciiE.GetBytes(c)[0];
                return ascc;
            }
            else
            {
                return 0;
            }
        }

        private string Convert2To16(string s)
        {
            return string.Format("{0:x}", Convert.ToInt32(s, 2)).ToUpper();
        }

        private string ArrayReverse(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}