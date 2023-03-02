using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffProject.Toolbox.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Get a value from an object without retroactively embeding the call inside a function. Similar to Linq.Select() but for objects.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TOutput Get<TInput,TOutput>(this TInput input, Func<TInput,TOutput> func)
        {
            return func(input);
        }
        /// <summary>
        /// Run an action with an object without retroactively embeding the call inside a function.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        public static void Do<TInput>(this TInput input, Action<TInput> func)
        {
            func(input);
        }

        /// <summary>
        /// Convert the IEnumerable to a joined string i.e. concatenates the members of a collection into a string. Shorthand for <c>string.Join(seperator,input)</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToStringConcat<T>(this IEnumerable<T> input, string separator = "")
        {
            return string.Join(separator, input.ToArray());
        }
    }
}
