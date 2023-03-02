using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffProject.ConsoleExt
{
    public static class ConsoleMenu
    {
        public static int Show(string msg, params string[] args)
        {

            var ii = 0;
            var i2 = 0;
            while (true)
            {
                Console.Clear();
                var o = new List<string>();
                Console.Write($"~({ii + 1}-");
                for (int i = ii; i < args.Length && i < ii + 8; i++)
                {
                    i2 = i;
                    o.Add(args[i]);
                }
                Console.Write($"{ i2 + 1}/{args.Length})~");
                var left = Console.CursorLeft;
                Console.Write(new string('~', Console.WindowWidth - left - 1));
                Console.CursorLeft = left + 1 + ((Console.WindowWidth - left - 1 - msg.Length) / 2);
                if (i2 >= ii + 7) o.Add("(NEXT PAGE)");
                else if (i2 >= args.Length - 1 && ii != 0) o.Add("(FIRST PAGE)");
                var p = ShowInline(msg, 2, o.ToArray());
                Console.Clear();

                if (p == Math.Min(8, args.Length - ii))
                    if (o[p] == "(FIRST PAGE)") ii = 0;
                    else ii += 8;
                else
                {
                    Console.WriteLine($"{msg} > {args[p + ii]}");
                    return p + ii;
                }
            }

        }
        public static int ShowInline(string msg, params string[] args)
        {
            return ShowInline(msg, 1, args);
        }
        public static int ShowInlineSimple(string msg, params string[] args)
        {
            return ShowInline(msg, 0, args);
        }
        private static int ShowInline(string msg, int decor, params string[] args)
        {
            var l = 0;

            if (args.Length == 0 || args.Length > 10) throw new ArgumentException("No or too many args. Show() has functionality to filter by page.");
            if (decor == 1) ConsoleExt.Separator();

            Console.WriteLine(msg);
            while (true)
            {
                var top = Console.CursorTop;
                if (decor == 1) ConsoleExt.Separator();

                var i = 0;
                Console.WriteLine();
                foreach (var item in args)
                {
                    ConsoleExt.WriteLine($"{i + 1}{(l == i ? "> " : "  ")}{item}", l == i ? ConsoleColor.Yellow : Console.ForegroundColor);
                    i++;
                }
                var top2 = Console.CursorTop;
                if (decor == 2) Console.CursorTop = Console.WindowHeight - 2;
                else Console.WriteLine();
                Console.WriteLine("[W] UP  [S] DOWN  [SPACEBAR]/[ENTER] SELECT  [1-9] QUICK SELECT");
                if (decor == 1) ConsoleExt.Separator();
                var key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case 'w':
                        l = Math.Max(0, l - 1);
                        break;
                    case 's':
                        l = Math.Min(args.Length - 1, l + 1);
                        break;
                    case ' ':
                        return l;
                    case '\r':
                        return l;
                    default:
                        if (!int.TryParse(key.ToString(), out int r) || r <= 0 || r > args.Length)
                            break;

                        else
                            return r - 1;
                }
                Console.CursorTop = top;
            }

        }
    }

    public static class ConsoleExt
    {

        static ConsoleColor oldColour;

        public static Action<ConsoleColor> startWrite = (c) =>
        {
            oldColour = Console.ForegroundColor;
            Console.ForegroundColor = c;
        };
        public static Action<ConsoleColor> endWrite = (c) =>
        {
            Console.ForegroundColor = oldColour;
        };

        public static void Pause(bool plain = false)
        {
            ConsoleExt.RunInColor(plain ? Console.ForegroundColor : ConsoleColor.Yellow, () =>
            {

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            });

        }

        public static void Separator(bool plain = true)
        {
            ConsoleExt.RunInColor(plain ? Console.ForegroundColor : ConsoleColor.Yellow, () =>
            {
                Console.Write(new string('~', Console.WindowWidth));
            });
        }

        public static void Write(object value, ConsoleColor color)
        {
            RunInColor(color, () => Console.Write(value));
        }
        public static void WriteLine(object value, ConsoleColor color)
        {
            RunInColor(color, () => Console.WriteLine(value));

        }

        public static void RunInColor(ConsoleColor color, Action func)
        {
            try
            {
                startWrite(color);
                func();
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
