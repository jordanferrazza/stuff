using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffTester
{
    public static class ConsoleExt
    {
        static ConsoleColor c1;

        public static Action<ConsoleColor> startWrite = (c) =>
        {
            c1 = Console.ForegroundColor;
            Console.ForegroundColor = c;
        };
        public static Action<ConsoleColor> endWrite = (c) =>
        {
            Console.ForegroundColor = c1;
        };
        public static void Write(object value, ConsoleColor color)
        {
            try
            {
                startWrite(color);
                Console.Write(value);
                endWrite(color);
            }
            catch
            {
                endWrite(color);
                throw;
            }
        }
        public static void WriteLine(object value, ConsoleColor color)
        {
            try
            {
                startWrite(color);
                Console.WriteLine(value);
                endWrite(color);
            }
            catch
            {
                endWrite(color);
                throw;
            }
        }
    }
}
