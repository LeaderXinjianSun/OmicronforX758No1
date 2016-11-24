using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BingLibrary.hjb
{
    public class Serialize
    {
        public static void writefile(object o, string filename)
        {
            try
            {
                BinaryFormatter formater = new BinaryFormatter();
                Stream stream = new FileStream(System.Environment.CurrentDirectory + @"\" + filename, FileMode.Create, FileAccess.Write, FileShare.None);
                formater.Serialize(stream, o);
                stream.Close();
            }
            catch { }
        }

        public static object readfile(string filename)
        {
            BinaryFormatter formater = new BinaryFormatter();
            Stream stream = new FileStream(System.Environment.CurrentDirectory + @"\" + filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            object me = formater.Deserialize(stream);
            stream.Close();
            return me;
        }
    }
}