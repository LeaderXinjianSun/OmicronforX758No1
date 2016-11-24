using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingLibrary.hjb;
using BingLibrary.hjb.Intercepts;
using System.ComponentModel.Composition;

namespace Omicron.ViewModel
{
    public class MessagePrint
    {
        public string MessageStr { set; get; }
        private int limt;
        public MessagePrint()
        {
            limt = 1000;
            MessageStr = "";
        }
        public MessagePrint(int Limit)
        {
            limt = Limit;
            MessageStr = "";
        }
        /// <summary>
        /// 返回所有的信息的字符串
        /// </summary>
        /// <param name="str">添加的信息</param>
        /// <returns>整合好的字符串</returns>
        public string AddMessage(string str)
        {
            string[] s = MessageStr.Split('\n');
            if (s.Length > limt)
            {
                MessageStr = "";
            }
            MessageStr += System.DateTime.Now.ToString() + " " + str + "\n";
            return MessageStr;
        }
    }
}
