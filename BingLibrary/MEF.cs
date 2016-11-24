using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
namespace BingLibrary.hjb
{
    public class MEF
    {
        public static class Contracts
        {
            public const string Data = "261B7242-5D50-4E7D-80F6-9C753C115B54";

            public const string Execute = "46D960C6-075A-4018-B3E9-0F8B2883770D";

            public const string ExecuteWithParam = "08091F57-B82F-40B3-9F55-CAA036D23045";

            public const string Initialize = "ED9F3DE0-75E4-494F-B051-A3C3D4BDD61C";

            public const string ActionMessage = "A0CAAFD5-C4CA-4206-A0F1-288541B4289A";

            //public const string ActionMessageWithParam = "72359118-F146-4DE3-9E3C-A7C9CB3F9D61";

        }

        public const string Key = "B919F2D4-2942-4BAC-99A4-715A7BBA387D";

        public static object lookup<T>(IEnumerable<Lazy<T, IDictionary<string, object>>> imports, object key, Action<object> defaultValue)
        {
            string keyStr = (key == null) ? "" : key.ToString();
            List<object> finds = new List<object>();
            foreach (var import in imports)
            {
                if (import != null && import.Metadata.ContainsKey(MEF.Key) && import.Metadata[MEF.Key].ToString() == key.ToString())
                {
                    finds.Add(import.Value);
                }
            }

            if (finds.Count == 0)
            {
                if (keyStr != null)
                    Debug.WriteLine("■■■未找到键：" + keyStr + "。已采用默认值。");
                return defaultValue;
            }
            else if (finds.Count == 1)
            {
                return finds[0];
            }
            else
            {
                Debug.WriteLine("■■■找到重复键：" + keyStr + "。已采用默认值。");
                return defaultValue;
            }
        }

        public static CompositionContainer container()
        {

            var fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var catalog = new DirectoryCatalog(fi.DirectoryName);
            return new CompositionContainer(catalog);
        }

        public static object compose(object part)
        {
            container().ComposeParts(part);
            return part;
        }
    }
}
