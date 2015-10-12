using System.IO;
using System.Windows.Forms;
using Threads.Domain;
using Threads.Services;

namespace Threads.Client
{
    public class DirectoryInvestigator
    {
        public string Path { get; private set; }
        public TreeView Tree { get; private set; }

        public DirectoryInvestigator(string path, TreeView tree)
        {
            Path = path;
            Tree = tree;
        }

        public void Investigate()
        {
            Entry root = new Entry {IsRoot = true};
            var xmlEventHandler = new XmlEntryHandler();
            var treeEventHandler = new TreeEntryHandler(Tree);
            GetEntry(root);
        }

        private Entry GetEntry(Entry info)
        {
            Entry entry = new Entry();
            if (Directory.Exists(Path) || File.Exists(Path)) //todo сделать путь
                return entry;
            entry.Parent = info;
            info.Children.Add(entry);
            //todo entryinfo

            //todo pass entry for threads

            return info;
        }
    }
}
