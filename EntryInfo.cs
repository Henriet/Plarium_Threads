using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;

namespace ThreadsTest
{
    [Serializable]
    public class EntryInfo
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastDataAccessTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public long Size { get;  set; }
        public string Owner { get;  set; }
        //public FileSystemSecurity Permissions { get; set; }

        public EntryType EntryType { get; set; }

        private EntryInfo() { }
        public EntryInfo(string entry)
        {

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
           // Permissions = info.GetAccessControl();
        }

        private void GetDirectoryHandlerParameters(string entry)
        {
            EntryType = EntryType.Directory;
            var info = new DirectoryInfo(entry);
            Init(info);
            Owner = Directory.GetAccessControl(entry).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
            var directories = info.GetFiles();
            Size = directories.Select(fileInfo => fileInfo.Length).Sum();
            //Permissions = info.GetAccessControl();
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