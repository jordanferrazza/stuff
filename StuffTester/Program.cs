using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuffProject;
using StuffProject.ConsoleExt;
using StuffProject.Toolbox;
using StuffProject.Toolbox.Extensions;

namespace StuffTester
{
    class TestObject<T>
    {
        public TestObject(string name, T value)
        {
            Name = name;
            Value = value;

        }

        public override string ToString()
        {
            return $"[{Name}, {Value}]";
        }

        public string Name { get; set; }

        public T Value { get; set; }
    }

    class Program
    {

        static ExceptionSandbox<Exception> ES = new ExceptionSandbox<Exception>(
               () => ConsoleExt.WriteLine("\n:) Entered ExceptionSandbox", ConsoleColor.Yellow),
                (x) =>
            {
                ConsoleExt.WriteLine("xO ExceptionSandbox caught an error!", ConsoleColor.Yellow);
                ConsoleExt.WriteLine(x, ConsoleColor.Red);
            }, () => ConsoleExt.WriteLine(":D Exited ExceptionSandbox", ConsoleColor.Yellow));
        static void Main(string[] args)
        {
            ReadMe();

            while (true)
            {
                switch (ConsoleMenu.Show("MAIN MENU", "TEST...", "ABOUT", "EXIT"))
                {
                    case 0:
                        switch (ConsoleMenu.Show("TEST...", "<<", "UndoHistory", "ConsoleMenu","Listonary"))
                        {
                            case 1:
                                testUndoHistory();
                                break;
                            case 2:
                                ES.Run(() =>
                                {
                                    Console.WriteLine(ConsoleMenu.ShowInline("Test menu", "Option 1", "Option 2", "Option 3"));
                                    Console.WriteLine(ConsoleMenu.ShowInlineSimple("Test menu", "Option 1", "Option 2", "Option 3"));

                                });
                                ConsoleExt.Pause();
                                break;
                            case 3:
                                testListonary();
                                break;
                        }
                        break;
                    case 1:
                        ReadMe();
                        break;
                    default:
                        Console.CursorTop = (Console.WindowHeight / 2) - 7;
                        if (ConsoleMenu.ShowInline("Are you sure you want to quit?", "No", "Yes") == 1)
                            return;
                        break;
                }





            }


        }

        private static void testListonary()
        {
            var o = new Listonary<string, TestObject<string>>((x) => x.Name);
            o.Add(new TestObject<string>("Hello", "hello world"));
            o.Add(new TestObject<string>("Fox", "the quick brown fox jumped over the lazy dog"));
            o.Add(new TestObject<string>("Foobar", "foo bar"));
            o.Add(new TestObject<string>("Bye", "bye"));

            ES.Run(() =>
            {
                Console.WriteLine("[Fox] = " + o["Fox"]);
                Console.WriteLine("ToDictionary() = " + o.ToDictionary().ToStringConcat(","));
                Console.WriteLine("Keys = " + o.Keys.ToStringConcat(","));
                Console.WriteLine("Values = " + o.Values.ToStringConcat(","));
                Console.WriteLine("ContainsKey() = " + o.ContainsKey("Hello"));
                Console.WriteLine("ContainsKey() = " + o.ContainsKey("Dog"));
            });

            ConsoleExt.Pause();
        }

        static void ReadMe()
        {
            Console.Clear();
            ConsoleExt.WriteLine(@"
Welcome to StuffTester 
StuffProject, StuffTester by Jordan Ferrazza (C) 2023
", ConsoleColor.White);
            ConsoleExt.Separator(false);
            Console.WriteLine(@"
Classes not or less included due to being tested in tester app itself:
ExceptionSandbox, ConsoleExt interface
");
            ConsoleExt.Separator(false);
            ConsoleExt.Pause();
        }
        static void testUndoHistory()
        {
            char c;

            ConsoleExt.WriteLine(@"
Instructions:
w = Write - Write to history, ovewriting undone actions
u = Undo - Undo
r = Redo - Redo
i = State - Current latest value
q = Back", ConsoleColor.White);
            var undo = new UndoHistory<int>();
            var ind = 0;
            while ((c = Console.ReadKey().KeyChar) != 'q')
            {
                ES.Run(() =>
                {
                    if (c == 'w')
                    {
                        Console.WriteLine("Write() = " + undo.Write(ind++));
                    }
                    else if (c == 'u')
                    {
                        Console.WriteLine("Undo() = " + undo.Undo());
                    }
                    else if (c == 'r')
                    {
                        Console.WriteLine("Redo() = " + undo.Redo());
                    }
                    else if (c == 'i')
                    {

                    }
                    else return;
                    Console.WriteLine("ind =         " + ind);
                    Console.WriteLine("ListToIndex = " + undo.ListToIndex().ToStringConcat(","));
                    Console.WriteLine("List  =       " + undo.List().ToStringConcat(","));
                    Console.WriteLine("State =       " + undo.State);
                    Console.WriteLine("Index =       " + undo.Index);
                    Console.WriteLine("CanUndo =     " + undo.CanUndo);
                    Console.WriteLine("CanRedo =     " + undo.CanRedo);
                });
            }
        }
    }
}
