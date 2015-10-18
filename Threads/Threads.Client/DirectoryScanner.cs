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
        private TreeView Tree { get; set; }
        private readonly XmlEntryService _xmlEntryService;
        private readonly TreeEntryService _treeEntryService;
        private readonly Thread _treeWrittingThread;
        private readonly Thread _xmlWrittingThread;
        private readonly ProgressBar _progressBar;
        private readonly Label _label;

        private const int MaxLableLength = 45;

        
        public DirectoryScanner(string path, TreeView tree, ProgressBar progressBar, Label currentSystemInfoLabel, string filePath)
        {
            Path = path;
            Tree = tree;

            _xmlEntryService = new XmlEntryService(filePath);
            _treeEntryService = new TreeEntryService(Tree);

            _treeWrittingThread = new Thread(_treeEntryService.Write);
            _xmlWrittingThread = new Thread(_xmlEntryService.Write);
            _progressBar = progressBar;
            _label = currentSystemInfoLabel;
        }

        public void Scan()
        {
            _treeWrittingThread.Start();
            _xmlWrittingThread.Start();
            
            var root = new Entry { IsRoot = true };

            GetEntry(root);

            FinishScanning();
        }

        private void GetEntry(Entry info)
        {
            var entry = new Entry();
            if (!Directory.Exists(Path) && !File.Exists(Path))
                return;
            entry.Info = Helpers.GetEntryInfo(Path);

            if (!info.IsRoot)
            {
                info.Children.Add(entry);
                entry.Parent = info;
            }
            PassEntry(entry);

            if (!Helpers.IsDirectory(Path))
            {
                return;
            }

            var directoryInfo = new DirectoryInfo(Path);
            FileSystemInfo[] fileSystemInfos;
            try
            {
                fileSystemInfos = directoryInfo.GetFileSystemInfos();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(String.Format(Resources.MainForm_AccessDenided, Path));
                return;
            }
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                Path = fileSystemInfo.FullName;
                GetEntry(entry);
            }
        }

        private void PassEntry(Entry entry)
        {
            PassEntryToServices(entry);
            UpdateProgress(entry);
        }

        private void PassEntryToServices(Entry entry)
        {
            _treeEntryService.AddEntryToQueue(entry);
            _xmlEntryService.AddEntryToQueue(entry);
            
        }

        private void UpdateProgress(Entry entry)
        {
            _label.BeginInvoke((MethodInvoker)delegate
            {
                string fullName = entry.Info.FullName;
                string text = fullName.Length < MaxLableLength ? fullName :  String.Format("...{0}",fullName.Substring(fullName.Length - MaxLableLength));

                _label.Text = text;
            });

            _progressBar.BeginInvoke((MethodInvoker)delegate
            {
                if(_progressBar.Value < _progressBar.Maximum)
                     _progressBar.Value++;
            });
        }

        private void FinishScanning()
        {
            _label.BeginInvoke((MethodInvoker)delegate
            {
                _label.Text = String.Empty;
            });
            
            _treeEntryService.Stop();
            _xmlEntryService.Stop();
        }
    }
}
