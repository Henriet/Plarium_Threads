using System.Windows.Forms;
using Threads.Domain;

namespace Threads.Services
{
    public class TreeEntryHandler : BaseEntryHanbler
    {
        private TreeView _tree;

        public TreeEntryHandler(TreeView tree)
        {
            _tree = tree;
        }

        protected override void WriteEntry(Entry entry)
        {
            //todo
        }
    }
}
