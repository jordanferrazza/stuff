using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffProject.Toolbox.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Get a value from an object without retroactively embeding the call inside a function. Similar to Linq.Select() but for single objects.
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TOutput Get<TInput, TOutput>(this TInput input, Func<TInput, TOutput> func)
        {
            return func(input);
        }
        /// <summary>
        /// Run an action with an object without retroactively embeding the call inside a function. Use Get() to return another value.
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
        /// <summary>
        /// Quickly cast an object by suffixing a generic rather than prefixing a type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T CastTo<T>(this object input)
        {
            return (T)input;
        }

        /// <summary>
        /// Gets the index of the first match in a collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static int IndexOfMatch<T>(this IEnumerable<T> input, Predicate<T> rule)
        {
            return input.ToList().IndexOf(input.ToList().Find(x => rule(x)));
        }
        /// <summary>
        /// Adds the co-ordinates of another point to the co-ordinates.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Point Translate(this Point input, Point f)
        {
            return new Point(input.X + f.X, input.Y + f.Y);
        }
        /// <summary>
        /// Adds a constant value to the co-ordinates.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Point Translate(this Point input, int z)
        {
            return new Point(input.X + z, input.Y + z);
        }
        /// <summary>
        /// Adds values to the co-ordinates.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point Translate(this Point input, int x, int y)
        {
            return new Point(input.X + x, input.Y + y);
        }

        /// <summary>
        /// Subtracts the co-ordinates from zero.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Point Negative(this Point input)
        {
            return input.Translate(x => -x);
        }
        /// <summary>
        /// Swaps the co-ordinates of the point.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Point Inverse(this Point input)
        {
            return new Point(input.Y, input.X);
        }
        /// <summary>
        /// Subtracts the co-ordinates of another point from the co-ordinates.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point Difference(this Point input, Point y)
        {
            return input.Translate(Negative(y));
        }
        /// <summary>
        /// Changes the co-ordinates according to a function on the co-ordinate.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Point Translate(this Point input, Func<int, int> z)
        {
            return new Point(z(input.X), z(input.Y));
        }
        /// <summary>
        /// Changes the co-ordinates according to functions on the co-ordinates.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point Translate(this Point input, Func<int, int> x, Func<int, int> y)
        {
            return new Point(x(input.X), y(input.Y));
        }

        /// <summary>
        /// Checks if both coordinates match an expression.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsTrueAnd(this Point input, Func<int, bool> x)
        {
            return x(input.X) && x(input.Y);
        }
        /// <summary>
        /// Compares two points and returns a pair of directional integers indicating whether each coordinate is greather than, equal to or less than each other.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Point Compare(this Point input, Point x)
        {
            return new Point((input.X > x.X ? 1 : input.X < x.X ? -1 : 0),
                (input.Y > x.Y ? 1 : input.Y < x.Y ? -1 : 0));
        }

        /// <summary>
        /// If a value is equal to another value, return a different value instead, else return the original value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="illegal"></param>
        /// <param name="alternative"></param>
        /// <returns></returns>
        public static T MakeNever<T>(this T condition, T illegal, T alternative)
        {
            return condition.Equals(illegal) ? alternative : condition;
        }
        /// <summary>
        /// If a value is equal to another value, keep it, or else make it equal another value. Almost the opposite of MakeNever().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="ignore"></param>
        /// <param name="then"></param>
        /// <returns></returns>
        public static T FormatUnless<T>(this T condition, T ignore, Func<T,T> then)
        {
            return condition.Equals(ignore) ? ignore : then(condition);
        }

    }
}
