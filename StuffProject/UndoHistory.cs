using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuffProject
{
    public class UndoHistory<T>
    {
        private List<T> History = new List<T>();
     
        /// <summary>
        /// The number determining what number State is being used. The first state will be 0 and so on. Change the index to skip to that state.
        /// <br></br>
        /// WARNING: Changing the index is like multiple Undo's and Redo's to that point.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Write a new version of State.
        /// <br></br>
        /// WARNING: <c>ClearFuture()</c> will be invoked on the current state.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public T Write(T entry)
        {
            ClearFuture();
            History.Add(entry);
            Index++;
            return entry;
        }

        /// <summary>
        /// Create a blank UndoHistory object.
        /// <br></br>
        /// WARNING: No states will exist. <c>Write(T)</c> will have to be used first to use the object properly. OR Use the other constructor to generate a default state.
        /// </summary>
        public UndoHistory()
        {

        }

        /// <summary>
        /// Creates an UndoHistory object with a starting version.
        /// </summary>
        /// <param name="start">The state at Index 0.</param>
        public UndoHistory(T start)
        {
            Write(start);
        }

        /// <summary>
        /// Go back one state. Newers states not affected until <c>Write(T)</c>  or <c>ClearFuture()</c> are used.
        /// </summary>
        /// <exception cref="InvalidOperationException">The Index represents the earliest state, or no states exist.</exception>
        /// <returns></returns>
        public T Undo()
        {
            if (!CanUndo) throw new InvalidOperationException("Cannot undo further.");
            --Index;
            return State;
        }

        /// <summary>
        /// Go forward one state.
        /// </summary>
        /// <exception cref="InvalidOperationException">The Index represents the latest state, or no states exist.</exception>
        /// 
        /// <returns></returns>
        public T Redo()
        {
            if (!CanRedo) throw new InvalidOperationException("Cannot redo further.");
            ++Index;
            return State;
        }

        /// <summary>
        /// The current state at index Index.
        /// </summary>
        public T State
        {
            get{
                if (History.Count == 0)
                    return default;
                if (History.Count == 1)
                    return History[0];
                return History[Index-1];
            }
        }

        /// <summary>
        /// Wipes all states.
        /// <br></br>
        /// WARNING: No states will be left. <c>Write(T)</c> will have to be used first to use the object properly.
        /// </summary>
        public void Clear()
        {
            Index = 0;
            History.Clear();
        }
        /// <summary>
        /// Wipes all states except the current one.
        /// </summary>
        public void ClearHistory()
        {
            T state = State;
            Index = 0;
            History.Clear();
            Write(state);
        }

        /// <summary>
        /// Determines whether Undo() can be done, i.e. states exist and the current state is not the last state ever.
        /// </summary>
        public bool CanUndo => Index > 1;

        /// <summary>
        /// Determines whether Redo() can be done, i.e. states exist and the current state is not the first state ever.
        /// </summary>
        public bool CanRedo => Index < History.Count;

        /// <summary>
        /// Length of the history.
        /// </summary>
        public int Length => History.Count();

        /// <summary>
        /// Returns the history.
        /// </summary>
        /// <returns></returns>
        public T[] List()
        {
            return History.ToArray();
        }
        /// <summary>
        /// Returns the history up to the current value of Index with undone states taken out.
        /// </summary>
        /// <returns></returns>
        public T[] ListToIndex()
        {
            return History.Take(Index).ToArray();
        }

        /// <summary>
        /// Clears all undone states to the right of the current index.
        /// </summary>
        public void ClearFuture()
        {
            if (CanRedo)
                History.RemoveRange(Index, History.Count - Index);
        }
    }
}
