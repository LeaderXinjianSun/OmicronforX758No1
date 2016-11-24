using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BingLibrary.hjb.Intercepts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BingIntercept : Attribute, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {

            foreach (var attribute in invocation.MethodInvocationTarget.GetCustomAttributes(true))
            {
                if (attribute is BingAttribute)
                {
                    if ((attribute as BingAttribute).Location == InvokeLocation.Before
                        || (attribute as BingAttribute).Location == InvokeLocation.Both)
                    {
                        (attribute as BingAttribute).Before();
                    }
                }
            }

            if (Regex.IsMatch(invocation.Method.Name, @"[gs]et_") || invocation.MethodInvocationTarget.GetCustomAttributes(true).Count(p =>
            { return (p is BingAttribute && (p as BingAttribute).IsInnerInvoke == true); }) <= 0)
            {
                invocation.Proceed();
            }

            foreach (var attribute in invocation.MethodInvocationTarget.GetCustomAttributes(true))
            {
                if (attribute is BingAttribute)
                {
                    (attribute as BingAttribute).Middle(invocation);
                }
            }

            if (invocation.ReturnValue is Task)
            {
                (invocation.ReturnValue as Task).ContinueWith(delegate (Task t)
                {
                    AfterInvoke(invocation.MethodInvocationTarget.GetCustomAttributes(true), invocation, t.Exception);
                });
            }
            else
            {
                AfterInvoke(invocation.MethodInvocationTarget.GetCustomAttributes(true), invocation, null);
            }
        }

        private void AfterInvoke(object[] Attributes, IInvocation invocation, Exception exp)
        {
            foreach (var attribute in Attributes)
            {
                if (attribute is BingAttribute)
                {
                    if ((attribute as BingAttribute).Location == InvokeLocation.After
                        || (attribute as BingAttribute).Location == InvokeLocation.Both)
                    {
                        (attribute as BingAttribute).After(invocation, exp);
                    }
                }
            }
        }
    }
}