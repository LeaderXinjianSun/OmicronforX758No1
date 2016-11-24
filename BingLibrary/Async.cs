using System;
using System.Threading.Tasks;

namespace BingLibrary.hjb
{
    public static class Async
    {
        public static async void RunFuncAsync(Action function, Action callback)
        {
            await ((Func<Task>)(() =>
            {
                return Task.Run(() =>
                {
                    function();
                });
            }))();
            callback?.Invoke();
        }

        public static async void RunFuncAsync<TResult>(Func<TResult> function, Action<TResult> callback)
        {
            TResult rlt = await ((Func<Task<TResult>>)(() =>
            {
                return Task.Run(() =>
                {
                    return function();
                });
            }))();
            callback?.Invoke(rlt);
        }

        public static T RunFuncAsync<T>(T v, object p)
        {
            throw new NotImplementedException();
        }
    }
}