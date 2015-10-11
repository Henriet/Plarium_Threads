using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;

namespace ThreadsTest
{
    public class EntryHandlerParameters
    {
        public AutoResetEvent AutoResetEvent { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime LastDataAccessTime { get; private set; }
        public DateTime ModificationTime { get; private set; }
        public long Size { get; private set; }
        public string Owner { get; private set; }
        public FileSystemSecurity Permissions { get; private set; }

        public EntryType EntryType { get; private set; }

        public EntryHandlerParameters(string entry, AutoResetEvent autoResetEvent)
        {
            AutoResetEvent = autoResetEvent;

            if (File.Exists(entry))
                GetFileHandlerParameters(entry);
            if (Directory.Exists(entry))
                GetDirectoryHandlerParameters(entry);
        }

        private void GetFileHandlerParameters(string entry)
        {
            EntryType = EntryType.File;
            var info = new FileInfo(entry);
            Init(info);
            Owner = File.GetAccessControl(entry).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            Size = info.Length;
            Permissions = info.GetAccessControl();
        }

        private void GetDirectoryHandlerParameters(string entry)
        {
            EntryType = EntryType.Directory;
            var info = new DirectoryInfo(entry);
            Init(info);
            Owner = Directory.GetAccessControl(entry).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            var directories = info.GetFiles();
            Size = directories.Select(fileInfo => fileInfo.Length).Sum();
            Permissions = info.GetAccessControl();
        }

        private void Init(FileSystemInfo info)
        {
            Name = info.Name;
            CreationTime = info.CreationTime;
            LastDataAccessTime = info.LastAccessTime;
            ModificationTime = info.LastWriteTime;
        }
    }
}