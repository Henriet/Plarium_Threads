using System;
using System.Security.AccessControl;

namespace Threads.Domain
{
    public class EntryInfo
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastDataAccessTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public string Size { get; set; }
        public string Owner { get; set; }
    }
}
