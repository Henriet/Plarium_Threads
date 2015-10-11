using System.Windows.Forms;

namespace ThreadsTest
{
    public class TreeEntryHandler : IEntryHandler
    {
        private TreeView _tree;
        public TreeEntryHandler(TreeView tree)
        {
            _tree = tree;
        }
        public void AddNewEntry(object param)
        {
            
        }
    }
}
