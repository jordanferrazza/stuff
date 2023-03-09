using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StuffProject.Toolbox.Special
{
    public class ExplodeString
    {
        /// <summary>
        /// An escaped character(s) used to open a variation group.
        /// </summary>
        public string OpenSequence { get; private set; }
        /// <summary>
        /// An escaped character(s) used to close a variation group.
        /// </summary>
        public string CloseSequence { get; private set; }
        /// <summary>
        /// An escaped character(s) used to seperate variations.
        /// </summary>
        public string Seperator { get; private set; }
        /// <summary>
        /// An escaped character(s) used to escape the object's sequences.
        /// </summary>
        public string EscapeSequence { get; private set; }


        /// <summary>
        /// Used to find where a group closes (and anchor a substring to, pick a preceeding variation, and recursively work out possibilities from the substring after it).
        /// </summary>
        Regex CloseSplitter;
        /// <summary>
        /// Used to find where a group opens (and its 'contents' to the end of a string).
        /// </summary>
        Regex OpenSplitter;
        /// <summary>
        /// Used to split the variations in a group.
        /// </summary>
        Regex GroupSplitter;

        /// <summary>
        /// Creates an ExplodeString object to explode strings into different possibile combinations.
        /// <para>For example inputting "{" "}", ";", "\" and running on "A{A;B}B" will return "AAB" and "ABB". Putting the \ in front of a character will ignore the character but make sure that the string is still syntactically correct to prevent unexpected or illegal behavour.</para>
        /// <para>DO NOT ESCAPE THE PARAMETERS. THEY WILL ALSO BE ESCAPED ON ENTRY, so the inputs may not match the properties. Use Regex.Unescape to unescape a property using Regex escape characters, and ExplodeString.Unescape to unescape a string using the escape sequence.</para>
        /// </summary>
        /// <param name="open">The (unescaped) character(s) used to open a variation group.</param>
        /// <param name="close">The (unescaped) character(s) used to close a variation group.</param>
        /// <param name="sep">The (unescaped) character(s) used to sepearate variations.</param>
        /// <param name="esc">The (unescaped) character(s) used to escape aformentioned sequences.</param>
        public ExplodeString(string open, string close, string sep, string esc)
        {
            OpenSequence = Regex.Escape(open);
            CloseSequence = Regex.Escape(close);
            Seperator = Regex.Escape(sep);
            EscapeSequence = Regex.Escape(esc);
            CloseSplitter = new Regex($"(?<!{EscapeSequence}){CloseSequence}");
            OpenSplitter = new Regex($"(?<=(?<!{EscapeSequence}){OpenSequence}).*");
            GroupSplitter = new Regex($"(?<!{EscapeSequence}){Seperator}");
        }
        /// <summary>
        /// Remove the escape sequenece from the front of the object's sequences in string except the escape sequenece itself and predecessing escape sequence instances, without running the object.
        /// <para></para> THIS DOES NOT UNESCAPE THE SEQUENCES THEMSLEVES. Use Regex.Unescape to unescape the sequences themselves.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <returns></returns>
        public string Unescape(string input)
        {
            return input.Replace($"{Regex.Unescape(EscapeSequence)}{Regex.Unescape(OpenSequence)}", Regex.Unescape(OpenSequence))
                .Replace($"{Regex.Unescape(EscapeSequence)}{Regex.Unescape(CloseSequence)}", Regex.Unescape(CloseSequence))
                .Replace($"{Regex.Unescape(EscapeSequence)}{Regex.Unescape(Seperator)}", Regex.Unescape(Seperator))
                ;
        }

        /// <summary>
        /// Run the object on a string and return all its possibilities. The possibilities are generated from out of variations left to right but out of groups right to left.
        /// </summary>
        /// <param name="input">The string. It should contain the mentioned clauses in OpenSequence, CloseSequence and Seperator ("variation groups"). Use EscapeSequence in front of said clauses to ignore them but they will be automatically removed if the sequence is being used directly next to another sequence (Use Unescape to do this alone).</param>
        /// <returns></returns>
        public List<string> Run(string input)
        {
            //Init anchors and ouput
            var o = new List<string>(); //Output

            //If this input has no groups, just add it to the ouput
            if (!OpenSplitter.IsMatch(input))
            {
                o.Add(Unescape(input));
            }
            else//If this input has groups, try to get a group from it
            {
                // Get the first occuring group interval (others are not used until recusively called below)
                string clause = CloseSplitter.Split(input)[0];
                // If there are groups in this interval
                if (OpenSplitter.IsMatch(clause))
                {
                    //Find the group
                    Match group = OpenSplitter.Match(clause);
                    //For each variation in the group
                    foreach (var variation in GroupSplitter.Split(group.Value))
                    {
                        //Find the constant preceeding the group in the old input
                        string prefix = clause.Remove(group.Index - Regex.Unescape(OpenSequence).Length);
                        //Select the other group data following the group in the old input
                        string suffix = input.Substring(clause.Length + Regex.Unescape(CloseSequence).Length);
                        //Create a new input made of the prefix, suffix and selected variation
                        string result = prefix + variation + suffix;
                        //Get the entire branch from this new string and add it to the ouput
                        o.AddRange(Run(result));
                    }

                }
                // If there are no groups, ignore it the interval
            }


            //Return the list
            return o;
        }

    }

}
