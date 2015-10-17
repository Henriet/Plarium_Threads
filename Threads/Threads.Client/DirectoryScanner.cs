using System.IO;
using System.Threading;
using System.Windows.Forms;
using Threads.Domain;
using Threads.Services;

namespace Threads.Client
{
    public class DirectoryScanner
    {
        private string Path { get; set; }
        private TreeView Tree { get; set; }
        private readonly XmlEntryHandler _xmlEntryHandler;
        private readonly TreeEntryHandler _treeEntryHandler;
        private readonly Thread _treeWrittingThread;
        private readonly Thread _xmlWrittingThread;
        private readonly ProgressBar _progressBar;
        private readonly Label _label;

        
        public DirectoryScanner(string path, TreeView tree, ProgressBar progressBar, Label currentSystemInfoLabel)
        {
            Path = path;
            Tree = tree;

            _xmlEntryHandler = new XmlEntryHandler();
            _treeEntryHandler = new TreeEntryHandler(Tree);

            _treeWrittingThread = new Thread(_treeEntryHandler.Write);
            _xmlWrittingThread = new Thread(_xmlEntryHandler.Write);
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
            var fileSystemInfos = directoryInfo.GetFileSystemInfos();
            
            foreach (var fileSystemInfo in fileSystemInfos)
            {
                Path = fileSystemInfo.FullName;
                GetEntry(entry);
            }

        }

        private void PassEntry(Entry entry)
        {
            PassEntryToHandlers(entry);
            UpdateProgress(entry);
        }

        private void PassEntryToHandlers(Entry entry)
        {
            _treeEntryHandler.AddEntryToQueue(entry);
            _xmlEntryHandler.AddEntryToQueue(entry);
            
        }

        private void UpdateProgress(Entry entry)
        {
            _label.BeginInvoke((MethodInvoker)delegate
            {
                _label.Text = entry.Info.FullName;
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
                _label.Text = "";
            });
            
            _treeEntryHandler.Stop();
            _xmlEntryHandler.Stop();
        }
    }
}
