using Newtonsoft.Json;
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

        public static T ReadJson<T>(string jsonFileName)
        {
            FileInfo fileInfo = new FileInfo(jsonFileName);
            if (fileInfo.Exists)
            {
                using (var fs = new FileStream(jsonFileName, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var content = sr.ReadToEnd();
                        var rslt = JsonConvert.DeserializeObject<T>(content);
                        return rslt;
                    }
                }
            }
            else
                throw new FileNotFoundException();
        }

        public static void WriteJson(object objectToSerialize, string jsonFileName)
        {
            using (var fs = new FileStream(jsonFileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0L);
                using (var sw = new StreamWriter(fs))
                {
                    var jsonStr = JsonConvert.SerializeObject(objectToSerialize, Formatting.Indented);
                    sw.Write(jsonStr);
                }
            }
        }
    }
}