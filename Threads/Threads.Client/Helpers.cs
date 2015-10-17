using System;
using System.IO;
using System.Linq;
using Threads.Domain;

namespace Threads.Client
{
    public class Helpers
    {
        public static EntryInfo GetEntryInfo(string path)
        {
            Path = path;
            FileSystemInfo entry = IsDirectory(path)
               ? (FileSystemInfo)new DirectoryInfo(path)
               : new FileInfo(path);

            var entryInfo = new EntryInfo
            {
                EntryType = IsDirectory(path) ? EntryType.Directory : EntryType.File,
                Name = entry.Name,
                FullName = entry.FullName,
                CreationTime = entry.CreationTime,
                LastDataAccessTime = entry.LastAccessTime,
                ModificationTime = entry.LastWriteTime,
                Owner = _isDirectory
                    ? Directory.GetAccessControl(path).GetOwner(typeof (System.Security.Principal.NTAccount)).ToString()
                    : File.GetAccessControl(path).GetOwner(typeof (System.Security.Principal.NTAccount)).ToString()
            };



            if (_isDirectory)
            {
                var directory = (DirectoryInfo) entry;
                var directories = directory.GetFiles();

                entryInfo.Size = directories.Select(fileInfo => fileInfo.Length).Sum();
            }
            else
            {
                var file = (FileInfo) entry;
                entryInfo.Size = file.Length;
            }

            return entryInfo;

        }

        public static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            return (attr & FileAttributes.Directory) == FileAttributes.Directory && Directory.Exists(path);
        }


        private static string Path { get; set; }
        private static bool _isDirectory { get { return IsDirectory(Path); } }

        public static int GetCountOfEntries(string path)
        {
            if(!Directory.Exists(path))
                throw new ArgumentException();

            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFileSystemInfos().Length;
        }
    }
}