using System.IO;
using System.Linq;
using Threads.Domain;

namespace Threads.Client
{
    public class Helpers
    {
        public EntryInfo EntryInfo { get; set; }

        private void GetEntryInfo(string path)
        {
            EntryInfo.EntryType = IsDirectory(path) ? EntryType.Directory : EntryType.File;
            FileSystemInfo entry = IsDirectory(path)
                ? (FileSystemInfo) new DirectoryInfo(path)
                : new FileInfo(path);

            EntryInfo.Name = entry.Name;
            EntryInfo.CreationTime = entry.CreationTime;
            EntryInfo.LastDataAccessTime = entry.LastAccessTime;
            EntryInfo.ModificationTime = entry.LastWriteTime;

            EntryInfo.Owner = _isDirectory
                ? Directory.GetAccessControl(path).GetOwner(typeof (System.Security.Principal.NTAccount)).ToString()
                : File.GetAccessControl(path).GetOwner(typeof (System.Security.Principal.NTAccount)).ToString();
            if (_isDirectory)
            {
                var directory = (DirectoryInfo) entry;
                var directories = directory.GetFiles();

                EntryInfo.Size = directories.Select(fileInfo => fileInfo.Length).Sum();
            }
            else
            {
                var file = (FileInfo) entry;
                EntryInfo.Size = file.Length;
            }

            //todo
            
        }

        public static bool IsDirectory(string path)
        {
            return Directory.Exists(path);//todo
        }

        private bool _isDirectory { get;
            set;
        }
    }
}