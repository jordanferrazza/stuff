using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuffProject;

namespace StuffTester
{
    class Program
    {
        static void Main(string[] args)
        {
            char c = ' ';
            var e = new ExceptionSandbox<Exception>(
                () => Console.WriteLine("\nEntered ExceptionSandbox"),
                (x) =>
            {
                Console.WriteLine("ExceptionSandbox caught an error!");
                ConsoleExt.WriteLine(x,ConsoleColor.Red);
            }, () => Console.WriteLine("Exited ExceptionSandbox"));
            char d;
            Console.WriteLine(@"Welcome to StuffTester 
StuffProject, StuffTester by Jordan Ferrazza

Classes not included due to being tested in tester app itself:
ExceptionSandbox, ConsoleExt interface");
            while (c != 'q')
            {

                if (c == 'c')
                {
                    Console.Clear();
                }
                if (c == 'u')
                {
                    Console.WriteLine(@"
Instructions:
w = Write
u = Undo
r = Redo
i = State
q = Back");
                    var undo = new UndoHistory<int>();
                    var ind = 0;
                    while ((d = Console.ReadKey().KeyChar) != 'q')
                    {
                        e.Run(() =>
                        {
                            if (d == 'w')
                            {
                                Console.WriteLine("Write() = " + undo.Write(ind++));
                            }
                            else if (d == 'u')
                            {
                                Console.WriteLine("Undo() = " + undo.Undo());
                            }
                            else if (d == 'r')
                            {
                                Console.WriteLine("Redo() = " + undo.Redo());
                            }
                            else if (d == 'i')
                            {

                            }
                            else return;
                            Console.WriteLine("ListToIndex = " + string.Join(",", undo.ListToIndex()));
                            Console.WriteLine("List  =       " + string.Join(",", undo.List()));
                            Console.WriteLine("State =       " + undo.State());
                            Console.WriteLine("Index =       " + undo.Index);
                            Console.WriteLine("ind =         " + ind);
                        });
                    }


                }



                Console.WriteLine(@"

MAIN MENU:
u = Test UndoHistory

c = Clear
q = Quit");
                c = Console.ReadKey().KeyChar;
            }


        }
    }
}
