using Castle.DynamicProxy;
using System;

namespace BingLibrary.hjb.Intercepts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BingAutoNotify : Attribute, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (invocation.Method.Name.StartsWith("set_"))
            {
                (invocation.InvocationTarget as DataSource).NotifyPropertyChanged(invocation.Method.Name.Substring(4));
            }
        }
    }

   
}