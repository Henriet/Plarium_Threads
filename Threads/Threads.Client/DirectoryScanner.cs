using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Threads.Client.Properties;
using Threads.Domain;
using Threads.Services;

namespace Threads.Client
{
    public class DirectoryScanner
    {
        private string Path { get; set; }

        private readonly XmlEntryService _xmlEntryService;
        private readonly TreeEntryService _treeEntryService;
        private  Thread _treeWrittingThread;
        private  Thread _xmlWrittingThread;
        private readonly StatusUpdater _statusUpdater;

        public DirectoryScanner(string path, TreeView tree, string filePath, StatusUpdater updater, TextBox erroLogTextBox)
        {
            Path = path;
            _xmlEntryService = new XmlEntryService(filePath, erroLogTextBox);
            _treeEntryService = new TreeEntryService(tree, erroLogTextBox); 
            _statusUpdater = updater;
        }

        public void Scan()
        {
            try
            {
                _treeWrittingThread = new Thread(_treeEntryService.Write);
                _xmlWrittingThread = new Thread(_xmlEntryService.Write);
                _treeWrittingThread.Start();
                _xmlWrittingThread.Start();

                var root = new Entry { IsRoot = true };
                GetEntry(root);
                FinishScanning();
            }
            catch (Exception ex)
            {
                Helpers.WriteToLog(Resources.Error_message, ex.Message);
            }
        }

        private void GetEntry(Entry info)
        {
            if (!Directory.Exists(Path) && !File.Exists(Path) || info == null)
                return;
            var entry = new Entry { Info = Helpers.GetEntryInfo(Path) };

            if (!info.IsRoot)
            {
                info.Children.Add(entry);
                entry.Parent = info;
            }
            PassEntry(entry);

            //if entry is directory - get files and subdirectories for it and recursively call this method for each of them
            if (!Helpers.IsDirectory(Path))
            {
                return;
            }

            var directoryInfo = new DirectoryInfo(Path);
            try
            {
                var fileSystemInfos = directoryInfo.GetFileSystemInfos();
                foreach (var fileSystemInfo in fileSystemInfos)
                {
                    Path = fileSystemInfo.FullName;
                    GetEntry(entry);
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteToLog(Resources.Error_message, ex.Message);
            }
        }

        private void PassEntry(Entry entry)
        {
            PassEntryToServices(entry);
            _statusUpdater.UpdateProgress(entry.Info.FullName);
        }

        private void PassEntryToServices(Entry entry)
        {
            try
            {
                _treeEntryService.AddEntryToQueue(entry);
                _xmlEntryService.AddEntryToQueue(entry);
            }
            catch (ThreadStateException ex)
            {
                Helpers.WriteToLog(Resources.Error_message, ex.Message);
            }
        }


        public void FinishScanning()
        {
            try
            {
                _treeEntryService.Stop();
                _xmlEntryService.Stop();
                _statusUpdater.Finish();
            }
            catch (Exception ex)
            {
                Helpers.WriteToLog(Resources.Error_message, ex.Message);
            }
            finally
            {
                if(_treeEntryService.Working)
                    _treeEntryService.Stop();
                if(_xmlEntryService.Working)
                    _xmlEntryService.Stop();
            }
        }
    }
}
