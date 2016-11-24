using Castle.DynamicProxy;
using System.Linq;

namespace BingLibrary.hjb.Intercepts
{
    public class BingInterceptClassFactory
    {
        public static T GetInterceptClass<T>(params object[] args) where T : class
        {
            return new ProxyGenerator().CreateClassProxy(typeof(T), args,
                typeof(T).GetCustomAttributes(typeof(IInterceptor), true).Cast<IInterceptor>().ToArray()) as T;
        }
    }
}