using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using Threads.Client.Properties;
using Threads.Domain;

namespace Threads.Client
{
    public class Helpers
    {
        public static TextBox ErrorLogTextBox;

        public static EntryInfo GetEntryInfo(string path)
        {
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
                Owner = IsDirectory(path)
                    ? Directory.GetAccessControl(path).GetOwner(typeof(NTAccount)).ToString()
                    : File.GetAccessControl(path).GetOwner(typeof(NTAccount)).ToString()
            };

            try
            {
                FileSystemSecurity security;
                if (IsDirectory(path))
                {
                    var directory = (DirectoryInfo)entry;
                    var directories = directory.GetFiles();
                    var filesLength = directories.Select(fileInfo => fileInfo.Length);
                    entryInfo.Size = filesLength.Sum().ToString();

                    security = directory.GetAccessControl();
                }
                else
                {
                    var file = (FileInfo)entry;
                    entryInfo.Size = file.Length.ToString();
                    security = file.GetAccessControl();
                }
                foreach (FileSystemAccessRule rule in
                             security.GetAccessRules(true, true, typeof(NTAccount)))
                {
                    entryInfo.Permissions = rule.FileSystemRights;
                    return entryInfo;
                }
            }
            catch (UnauthorizedAccessException)
            {
                entryInfo.Size = Resources.Access_Denided;
            }
            catch (Exception ex)
            {
                WriteToLog(Resources.Error_message, ex.Message);
            }
            return entryInfo;
        }

        public static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            bool isDirectory = (attr & FileAttributes.Directory) == FileAttributes.Directory && Directory.Exists(path);
            return isDirectory;
        }
        
        public static int GetCountOfEntries(string path)
        {
            if (!Directory.Exists(path))
                return 0;

            var directoryInfo = new DirectoryInfo(path);
            int count = 0;
            GetLengthOfFiles(ref count, directoryInfo);
            return count;
        }

        private static void GetLengthOfFiles(ref int count, DirectoryInfo info)
        {
            try
            {
                var directories = info.GetDirectories();

                foreach (var directoryInfo in directories)
                {
                    GetLengthOfFiles(ref count, directoryInfo);
                    count += directoryInfo.GetFiles().Length;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                 WriteToLog(Resources.Error_message, ex.Message);
            }
        }


        public static void WriteToLog(string text, string error)
        {
            try
            {
                ErrorLogTextBox.AppendText(String.Format(text, error) + Environment.NewLine);
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format(text, error));
            }
        }
    }
}