using BingLibrary.hjb.Intercepts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace BingLibrary.hjb
{
    [Serializable]
    public class DataSource : INotifyPropertyChanged
    {
        public static T New<T>() where T : class
        {
            return BingInterceptClassFactory.GetInterceptClass<T>();
        }

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //属性名称,属性内容
        public void setProperty<T>(string PropertyName, T value)
        {
            if (GetType().GetProperty(PropertyName) == null)
            {
                Debug.WriteLine("■■■未找到属性：" + PropertyName);
            }
            else
            {
                var oldValue = this.GetType().GetProperty(PropertyName).GetValue(this);
                this.GetType().GetProperty(PropertyName).SetValue(this, value);
                this.NotifyPropertyChanged(PropertyName);
            }
        }
    }

    internal class RefelectionExtension
    {
        public class Funcs
        {
            public void RunFunc()
            {
                Func();
            }

            public Func<object> Func { set; get; }

            public void RunFuncWithParam(object obj)
            {
                FuncWithParam(obj);
            }

            public Func<object, object> FuncWithParam { set; get; }
        }

        public static Dictionary<string, object> getMethods(object obj)
        {
            if (obj == null)
                return null;
            Dictionary<string, object> tempd = new Dictionary<string, object>();

            foreach (var m in obj.GetType().GetMethods())
            {
                if ((!new Regex(RegexStr1).Match(m.DeclaringType.Name).Success) && (!new Regex(RegexStr2).Match(m.DeclaringType.Name).Success) && m.ToString().Substring(0, 4) == RegexStr3 && Regex.IsMatch(m.Name[0].ToString(), "[A-Z]"))
                {
                    if (m.GetParameters().Length == 0)
                    {
                        Funcs funcs1 = new Funcs();

                        funcs1.Func = () =>
                         {
                             m.Invoke(obj, null);
                             return null;
                         };

                        foreach (var ca in m.CustomAttributes)
                        {
                            if (ca.AttributeType.Name == "Initialize")
                            {
                                if (ca.ConstructorArguments.Count == 1)
                                {
                                    if ((int)(ca.ConstructorArguments[0].Value) == 1)
                                    {
                                        //Do Nothing
                                    }
                                    else if ((int)(ca.ConstructorArguments[0].Value) == 2)
                                    {
                                        funcs1.RunFunc();
                                    }
                                    else if ((int)(ca.ConstructorArguments[0].Value) == 4)
                                    {
                                        Async.RunFuncAsync(funcs1.RunFunc, null);
                                    }
                                }
                                
                                break;
                            }
                        }

                        if (tempd.ContainsKey(m.Name))
                            Debug.WriteLine("■■■找到重复键：" + m.Name + "。已采用默认值。");
                        else
                            tempd.Add(m.Name, new Action(funcs1.RunFunc));
                    }
                    else
                    {
                        Funcs funcs2 = new Funcs();
                        object param = m.GetParameters()[0];

                        funcs2.FuncWithParam = (pam) =>
                          {
                              object[] objs = new object[1];
                              objs[0] = pam;
                              m.Invoke(obj, objs);
                              return null;
                          };
                        if (tempd.ContainsKey(m.Name))
                            Debug.WriteLine("■■■找到重复键：" + m.Name + "。已采用默认值。");
                        else

                            tempd.Add(m.Name, new Action<object>(funcs2.RunFuncWithParam));
                    }
                }
            }
            return tempd;
        }
        private const string RegexStr1 = "DataSource";
        private const string RegexStr2 = "Proxy";
        private const string RegexStr3 = "Void";

    }

    internal class DataSourceExt
    {
        [ImportMany(MEF.Contracts.Data)]
        public IEnumerable<Lazy<object, IDictionary<string, object>>> AllData { set; get; }

        public object data(object key)
        {
            return MEF.lookup(this.AllData, key, null);
        }
    }

    internal static class DataModule
    {
        public static DataSourceExt allDatas = (DataSourceExt)MEF.compose(new DataSourceExt());
        public static Dictionary<string, object> allMethods = new Dictionary<string, object>();
        public static List<string> keys = new List<string>();
    }

    public class DataExtension : MarkupExtension
    {
        private string Key;

        private object getData(object key)
        {
            return DataModule.allDatas.data(key);
        }

        public DataExtension(object key)
        {
            this.Key = key.ToString();
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            if (DataModule.allDatas.data(Key) == null)
                return null;
            if (!DataModule.keys.Contains(Key))
            {
                DataModule.keys.Add(Key);
                foreach (var temp in RefelectionExtension.getMethods(DataModule.allDatas.data(Key)))
                {
                    if (DataModule.allMethods.ContainsKey(temp.Key))
                    {
                        Debug.WriteLine("■■■找到重复键：" + temp.Key + "。已采用默认值。");
                    }
                    else
                    {
                        DataModule.allMethods.Add(temp.Key, temp.Value);
                    }
                }
            }

            return DataModule.allDatas.data(Key);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _excuteMethod;

        public Action<object> ExcuteMethod
        {
            get
            {
                return _excuteMethod;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            ExcuteMethod(parameter);
        }

        public RelayCommand(Action<object> excuteMethod)
        {
            if (excuteMethod == null)
            {
                throw new Exception();
            }
            _excuteMethod = excuteMethod;
        }
    }

    internal class ExtensionTool
    {
        public static object getDataContext(IServiceProvider provider)
        {
            return typeof(FrameworkElement).IsAssignableFrom(((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetObject.GetType()) ? ((FrameworkElement)((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetObject).DataContext : null;
        }

        public static Type getDestinationType(IServiceProvider provider)
        {
            switch (((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty.GetType().Name)
            {
                case "PropertyInfo": return (((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as PropertyInfo).PropertyType;
                case "DependencyProperty": return (((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as DependencyProperty).PropertyType;
                case "EventInfo": return (((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as EventInfo).EventHandlerType;
                case "RuntimeEventInfo": return (((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as EventInfo).EventHandlerType;
                case "MethodInfo":
                    if (new Regex("[Add|Remove].+Handler").IsMatch((((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as MethodInfo).Name))
                    {
                        if ((((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as MethodInfo).GetParameters().Length == 2)
                        {
                            if (typeof(MulticastDelegate).IsAssignableFrom((((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as MethodInfo).GetParameters()[1].ParameterType))
                            {
                                return (((IProvideValueTarget)provider.GetService(typeof(IProvideValueTarget))).TargetProperty as MethodInfo).GetParameters()[1].ParameterType;
                            }
                            else
                                return null;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
            }

            return null;
        }

        public static object createDelegate(Type handlerType, Action<object, object> handler)
        {
            if (handlerType == null)
            {
                return null;
            }
            List<ParameterExpression> pe = new List<ParameterExpression>();
            for (int i = 0; i < handlerType.GetMethod("Invoke").GetParameters().Length; i++)
            {
                pe.Add(System.Linq.Expressions.Expression.Parameter(handlerType.GetMethod("Invoke").GetParameters()[i].ParameterType));
            }
            List<System.Linq.Expressions.Expression> exp = new List<System.Linq.Expressions.Expression>();
            exp.Add(pe[0]);
            exp.Add(System.Linq.Expressions.Expression.Convert(pe[1], typeof(object)));
            return Delegate.CreateDelegate(handlerType, System.Linq.Expressions.Expression.Lambda(System.Linq.Expressions.Expression.Call(System.Linq.Expressions.Expression.Constant(new Action<object, object>(handler)), typeof(Action<object, object>).GetMethod("Invoke"), exp), pe).Compile(), "Invoke");
        }

        public static object createAction(IServiceProvider provider, Action method)
        {
            string s = ExtensionTool.getDestinationType(provider).Name;
            switch (ExtensionTool.getDestinationType(provider).Name)
            {
                case "ICommand":
                    return (object)(new RelayCommand(param => method()));

                case "Delegate":
                    return ExtensionTool.createDelegate(ExtensionTool.getDestinationType(provider), (object sender, object args) => method());

                case "EventHandler":
                    return ExtensionTool.createDelegate(ExtensionTool.getDestinationType(provider), (object sender, object args) => method());

                case "MouseButtonEventHandler":
                    return ExtensionTool.createDelegate(ExtensionTool.getDestinationType(provider), (object sender, object args) => method());

                case "RoutedEventHandler":
                    return ExtensionTool.createDelegate(ExtensionTool.getDestinationType(provider), (object sender, object args) => method());

                default:
                    return null;
            }
        }

        public static object createActionWithParam(IServiceProvider provider, Action<object> method)
        {
            switch (ExtensionTool.getDestinationType(provider).Name)
            {
                case "ICommand":
                    return (object)(new RelayCommand(param => method(param)));

                default:
                    return null;
            }
        }

        public static object createEventAction(IServiceProvider provider, Action<object> method)
        {
            switch (ExtensionTool.getDestinationType(provider).Name)
            {
                case "ICommand":
                    return null;

                case "Delegate":
                    return ExtensionTool.createDelegate(ExtensionTool.getDestinationType(provider), (object sender, object args) => method(args));

                default:
                    return null;
            }
        }

        private static ActionExt rslt = (ActionExt)MEF.compose(new ActionExt());
        private static bool isexedinit = false;

        public static ActionExt actions()
        {
            if (!isexedinit)
            {
                isexedinit = true;
                foreach (var import in rslt.Initializes)
                {
                    if (((object)import.Value) == null)
                    {
                        Debug.WriteLine("■■■Initialize导出项为null。请尝试为该导出设置独立的__Export导出类。");
                    }
                    else
                    {
                        import.Value();
                    }
                }
            }
            else
            { }
            return rslt;
        }
    }

    internal class ActionExt
    {
        [ImportMany(MEF.Contracts.Initialize)]
        public IEnumerable<Lazy<Action, IDictionary<string, object>>> Initializes { set; get; }

        [ImportMany(MEF.Contracts.Execute)]
        public IEnumerable<Lazy<Action, IDictionary<string, object>>> Executes { set; get; }

        public Action execute(object key)
        {
            return (Action)MEF.lookup(this.Executes, key, null);
        }

        [ImportMany(MEF.Contracts.ExecuteWithParam)]
        public IEnumerable<Lazy<Action<object>, IDictionary<string, object>>> ExecutesWithParam { set; get; }

        public Action<object> executeWithParam(object key)
        {
            return (Action<object>)MEF.lookup(this.ExecutesWithParam, key, null);
        }
    }

    public class ActionAutoExtension : MarkupExtension
    {
        private string Key;

        public ActionAutoExtension(object key)
        {
            this.Key = key.ToString();
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            Action execute;
            if (DataModule.allMethods.ContainsKey(Key))
            {
                execute = (Action)DataModule.allMethods[Key];
            }
            else
            {
                Debug.WriteLine("■■■未找到键：" + Key + "。已采用默认值。");
                execute = null;
            }

            if (execute == null)
                return null;
            return ExtensionTool.createAction(provider, execute);
        }
    }

    public class ActionAutoWithParamExtension : MarkupExtension
    {
        private string Key;

        public ActionAutoWithParamExtension(object key)
        {
            this.Key = key.ToString();
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            Action<object> executeWithParam;
            if (DataModule.allMethods.ContainsKey(Key))
            {
                executeWithParam = (Action<object>)DataModule.allMethods[Key];
            }
            else
            {
                Debug.WriteLine("■■■未找到键：" + Key + "。已采用默认值。");
                executeWithParam = null;
            }

            if (executeWithParam == null)
                return null;
            return ExtensionTool.createActionWithParam(provider, executeWithParam);
        }
    }

    public class ActionExtension : MarkupExtension
    {
        private string Key;

        public ActionExtension(object key)
        {
            this.Key = key.ToString();
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            var execute = ExtensionTool.actions().execute(Key);

            if (execute == null)
                return null;
            return ExtensionTool.createAction(provider, execute);
        }
    }

    public class ActionWithParamExtension : MarkupExtension
    {
        private string Key;

        public ActionWithParamExtension(object key)
        {
            this.Key = key.ToString();
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            var executeWithParam = ExtensionTool.actions().executeWithParam(Key);
            if (executeWithParam == null)
                return null;
            return ExtensionTool.createActionWithParam(provider, executeWithParam);
        }
    }

    internal class ActionMessageExtensionTool
    {
        public static ActionMessageExt rslt = (ActionMessageExt)MEF.compose(new ActionMessageExt());
    }

    internal class ActionMessageExt
    {
        [ImportMany(MEF.Contracts.ActionMessage)]
        public IEnumerable<Lazy<Action, IDictionary<string, object>>> Executes { set; get; }

        public Action execute(object key)
        {
            return (Action)MEF.lookup(this.Executes, key, null);
        }
    }

    public static class ActionMessages
    {
        public static object GetAction(string key)
        {
            var execute = ActionMessageExtensionTool.rslt.execute(key);
            return execute ?? null;
        }

        public static void ExecuteAction(string key)
        {
            ActionMessageExtensionTool.rslt.execute(key)?.Invoke();
        }
    }
}