using System.IO;
using System.Windows.Forms;
using Threads.Domain;
using Threads.Services;

namespace Threads.Client
{
    public class DirectoryInvestigator
    {
        private string Path { get; set; }
        private TreeView Tree { get; set; }
        private XmlEntryHandler _xmlEventHandler;
        private TreeEntryHandler _treeEventHandler;

        public DirectoryInvestigator(string path, TreeView tree)
        {
            Path = path;
            Tree = tree;
        }

        public void Investigate()
        {
            _xmlEventHandler = new XmlEntryHandler();
            _treeEventHandler = new TreeEntryHandler(Tree);
            Entry root = new Entry { IsRoot = true };

            GetEntry(root);
        }

        private Entry GetEntry(Entry info)
        {
            var entry = new Entry();
            if (!Directory.Exists(Path) && !File.Exists(Path))
                return entry;
            entry.Info = Helpers.GetEntryInfo(Path);

            if (!info.IsRoot)
            {
                info.Children.Add(entry);
                entry.Parent = info;
            }

            if (!Helpers.IsDirectory(Path))
            {
                //todo pass entry for threads
                return entry;
            }

            var directoryInfo = new DirectoryInfo(Path);
            var fileSystemInfos = directoryInfo.GetFileSystemInfos();

            foreach (var fileSystemInfo in fileSystemInfos)
            {
                //todo pass entry for threads
                Path = fileSystemInfo.FullName;
                GetEntry(entry);
            }

            return entry;
        }
    }
}
