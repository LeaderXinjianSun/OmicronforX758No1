using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace BingLibrary.hjb
{
    public static class LanguageSwitch
    {
        public static LanguageType Local = (LanguageType)int.Parse(Inifile.INIGetStringValue(System.Environment.CurrentDirectory + "\\Language.config", "Language", "Value", "0"));

        public static void SetLocalLanguage(LanguageType local)
        {
            Local = local;
            Inifile.INIWriteValue(System.Environment.CurrentDirectory + "\\Language.config", "Language", "Value", ((int)LanguageSwitch.Local).ToString());
        }
    }

    public static class LanguagePackage
    {
        public static Dictionary<LanguageType, List<string>> Packages = new Dictionary<LanguageType, List<string>>();
        public static string[] LanguageNames = Enum.GetNames(typeof(LanguageType));

        public static void addLanguagePackages()
        {
            ActionMessages.ExecuteAction("addLanguagePackages");
        }
    }

    public enum LanguageType
    {
        简体中文 = 0,
        English = 1,
        繁体中文 = 2,
        日文 = 3,
    }

    public class LanguageExtension : MarkupExtension
    {
        private string _designName;

        public LanguageExtension(string designName)
        {
            _designName = designName;
            if (LanguagePackage.Packages.Count == 0)
                LanguagePackage.addLanguagePackages();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            int index = LanguagePackage.Packages[LanguageType.简体中文].FindIndex(x => x == _designName);
            return LanguagePackage.Packages[LanguageSwitch.Local][index];
        }
    }
}