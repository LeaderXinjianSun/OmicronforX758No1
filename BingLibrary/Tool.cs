using System;
using System.Collections.Generic;
using System.IO;

namespace BingLibrary.hjb
{
    public static class Tool
    {
        public static string CurrentPath()
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var fileInfo = new FileInfo(assemblyLocation);
            return fileInfo.DirectoryName;
        }

        public static string DateTimeFormart(int Index)
        {
            return dic.ContainsKey(Index) ? dic[Index].Invoke() : DateTime.Now.ToString();
        }

        private static Dictionary<int, Func<string>> dic = new Dictionary<int, Func<string>>
       {
           { 0, ()=> DateTime.Now.ToString() },
           { 1, ()=> DateTime.Now.ToString("yyyy-MM-dd") },//2016-11-5
           { 2, ()=> DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") },//2016-11-5 14:23:23
           { 3, ()=> DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff")  },//2016年11月5日 14:23:23.1234
           { 4, ()=> DateTime.Now.ToString("yyyy年MM月dd日") },//2016年11月5日
           { 5, ()=> DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss") },//2016年11月5日 14:23:23
           { 6, ()=> DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss.ffff")  },//2016年11月5日 14:23:23.1234
        };

        public static string ConvertBase(string Value, int From, int To)
        {
            //2,8,10,16四种进制间转换
            try
            {
                int intValue = Convert.ToInt32(Value, From);  //先转成from进制
                string result = Convert.ToString(intValue, To);  //再转成to进制
                if (To == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7: result = "0" + result; break;
                        case 6: result = "00" + result; break;
                        case 5: result = "000" + result; break;
                        case 4: result = "0000" + result; break;
                        case 3: result = "00000" + result; break;
                    }
                }
                return result;
            }
            catch
            {
                return "0";
            }
        }
    }
}