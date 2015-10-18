using System.Linq;
using System.Windows.Forms;

namespace Threads.Services
{
    public class TreeEntryService : BaseEntryService
    {
        private readonly TreeView _tree;


        public TreeEntryService(TreeView tree)
        {
            _tree = tree;
            _tree.BeginInvoke((MethodInvoker)delegate
            {
                _tree.Nodes.Clear();
            });
        }

        protected override void WriteEntry()
        {
            
            _tree.Invoke((MethodInvoker)delegate
            {
                if (CurrentEntry.Parent != null)
                {
                    _tree.SelectedNode = _tree.Nodes.Find(CurrentEntry.Parent.Info.FullName, true).FirstOrDefault();

                    if (_tree.SelectedNode != null)
                    {
                        _tree.SelectedNode.Nodes.Add(CurrentEntry.Info.FullName, CurrentEntry.Info.Name);
                    }
                }
                else
                {
                    _tree.Nodes.Add(CurrentEntry.Info.FullName, CurrentEntry.Info.Name);

                }
            });
        }
    }
}
