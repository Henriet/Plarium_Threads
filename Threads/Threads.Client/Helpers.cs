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
                try
                {
                    var directories = directory.GetFiles();

                    entryInfo.Size = directories.Select(fileInfo => fileInfo.Length).Sum().ToString();
                }
                catch (UnauthorizedAccessException)
                {
                    entryInfo.Size = "Acess denided";
                }
            }
            else
            {
                var file = (FileInfo) entry;
                entryInfo.Size = file.Length.ToString();
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
            int count = 0;
            GetCountOfFiles(ref count, directoryInfo);
            return count;
        }

        private static void GetCountOfFiles(ref int count, DirectoryInfo info)
        {
            try
            {
                var directories = info.GetDirectories();

                foreach (var directoryInfo in directories)
                {
                    GetCountOfFiles(ref count, directoryInfo);
                    count += directoryInfo.GetFiles().Length;
                }
            }
            catch(UnauthorizedAccessException) {  }
        }
    }
}