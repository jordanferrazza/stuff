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
        public int Index { get; set; }

        public T Write(T entry)
        {
            ClearFuture();
            History.Add(entry);
            Index++;
            return entry;
        }

        public T Undo()
        {
            if (!CanUndo()) throw new InvalidOperationException("Cannot undo further.");
            --Index;
            return State();
        }
        public T Redo()
        {
            if (!CanRedo()) throw new InvalidOperationException("Cannot redo further.");
            ++Index;
            return State();
        }

        public T State()
        {
            if (History.Count == 0)
                return default;
            if (History.Count == 1)
                return History[0];
            return History[Index - 1];
        }

        public void Clear()
        {
            Index = 0;
            History.Clear();
        }

        public bool CanUndo()
        {
            return Index > 1;
        }
        public bool CanRedo()
        {
            return Index < History.Count;
        }
        public int Length()
        {
            return History.Count();
        }

        public T[] List()
        {
            return History.ToArray();
        }
        public T[] ListToIndex()
        {
            return History.Take(Index).ToArray();
        }

        public void ClearFuture()
        {
            if (CanRedo())
                History.RemoveRange(Index, History.Count - Index);
        }
    }
}
