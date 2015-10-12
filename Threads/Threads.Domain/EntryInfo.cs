using System;
using System.Security.AccessControl;

namespace Threads.Domain
{
    public class EntryInfo
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastDataAccessTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public long Size { get; set; }
        public string Owner { get; set; }
        public FileSystemSecurity Permissions { get; set; }
        public EntryType EntryType { get; set; }
    }
}
