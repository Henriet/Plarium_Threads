using System.Collections.Generic;

namespace Threads.Domain
{
    public class Entry
    {
        public Entry Parent { get; set; }
        public List<Entry> Children { get; set; }
        public EntryInfo Info { get; set; }
        public bool IsRoot { get; set; }
        public bool HasChildren { get; set; }
    }
}
