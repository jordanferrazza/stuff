using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffProject.Toolbox.Extensions
{
    public static class Extensions
    {

        public static TOutput Get<TInput,TOutput>(this TInput input, Func<TInput,TOutput> func)
        {
            return func(input);
        }
        public static void Do<TInput>(this TInput input, Action<TInput> func)
        {
            func(input);
        }
    }
}
