using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuffProject;
using StuffProject.ConsoleExt;
using StuffProject.Toolbox;

namespace StuffTester
{
    class Program
    {
        static ExceptionSandbox<Exception> ES = new ExceptionSandbox<Exception>(
               () => ConsoleExt.WriteLine("\nEntered ExceptionSandbox", ConsoleColor.Yellow),
                (x) =>
            {
                ConsoleExt.WriteLine("ExceptionSandbox caught an error!", ConsoleColor.Yellow);
                ConsoleExt.WriteLine(x, ConsoleColor.Red);
            }, () => ConsoleExt.WriteLine("Exited ExceptionSandbox", ConsoleColor.Yellow));
        static void Main(string[] args)
        {
            ReadMe();
            
            while (true)
            {
                switch (ConsoleMenu.Show("MAIN MENU", "TEST...", "ABOUT", "EXIT"))
                {
                    case 0:
                        switch (ConsoleMenu.Show("TEST...", "<<", "UndoHistory"))
                        {
                            case 1:
                                testUndoHistory();
                                break;
                        }
                        break;
                    case 1:
                        ReadMe();
                        break;                 
                    default:
                        return;
                }






            }


        }
        static void ReadMe()
        {
            Console.Clear();
            ConsoleExt.WriteLine(@"
Welcome to StuffTester 
StuffProject, StuffTester by Jordan Ferrazza (C) 2023
",ConsoleColor.White);
            ConsoleExt.Separator();
            Console.WriteLine(@"
Classes not included due to being tested in tester app itself:
ExceptionSandbox, ConsoleExt interface
");
            ConsoleExt.Separator();
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
                    Console.WriteLine("ListToIndex = " + string.Join(",", undo.ListToIndex()));
                    Console.WriteLine("List  =       " + string.Join(",", undo.List()));
                    Console.WriteLine("State =       " + undo.State);
                    Console.WriteLine("Index =       " + undo.Index);
                    Console.WriteLine("CanUndo =     " + undo.CanUndo);
                    Console.WriteLine("CanRedo =     " + undo.CanRedo);
                });
            }
        }
    }
}
