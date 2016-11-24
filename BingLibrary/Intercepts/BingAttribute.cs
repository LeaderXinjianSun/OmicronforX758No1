using Castle.DynamicProxy;
using System;

namespace BingLibrary.hjb.Intercepts
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BingAttribute : Attribute
    {
        public bool IsInnerInvoke { set; get; }
        public InvokeLocation Location { set; get; }

        public abstract void After(IInvocation invocation, Exception exp);

        public abstract void Middle(IInvocation invocation);

        public abstract void Before();
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class Initialize: Attribute
    {
        public Initialize(InitType InitType=InitType.InitializeAsync)
        { }
    }

    public enum InitType
    {
        InitializeNone=1,
        Initialize=2,
        InitializeAsync=4,
    }
}
