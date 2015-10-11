using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ThreadsTest
{
    public class DirectoryInvestigator
    {
        private const int CountOfThreads = 2;
        public string Path { get; private set; }

        public DirectoryInvestigator(string path)
        {
            Path = path;
        }

        public void Investigate(TreeView tree)
        {
            var entries = Directory.GetFileSystemEntries(Path);
            var xmlEventHandler = new XmlEntryHandler();
            var treeEventHandler = new TreeEntryHandler(tree);

            foreach (var entry in entries)
            {
                WaitHandle[] waitHandles = new WaitHandle[1];
                waitHandles[0] = StartNewThread(xmlEventHandler, entry);
                //waitHandles[1] = StartNewThread(treeEventHandler, entry);
                WaitHandle.WaitAll(waitHandles);
            }
        }

        private AutoResetEvent StartNewThread(IEntryHandler handler, string entry)
        {
            var thread = new Thread(handler.AddNewEntry);
            var autoEvent = new AutoResetEvent(false);
            thread.Start(new EntryHandlerParameters(entry, autoEvent));
            return autoEvent;
        }
    }
}
