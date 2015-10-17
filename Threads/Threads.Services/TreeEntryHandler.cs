using System.Linq;
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
            _tree.BeginInvoke((MethodInvoker)delegate
            {
                _tree.Nodes.Clear();
            });
        }

        protected override void WriteEntry(Entry entry)
        {
            _tree.BeginInvoke((MethodInvoker)delegate
            {
                if (entry.Parent != null)
                {
                    _tree.SelectedNode = _tree.Nodes.Find(entry.Parent.Info.FullName, true).FirstOrDefault();


                    if (_tree.SelectedNode != null)
                         _tree.SelectedNode.Nodes.Add(entry.Info.FullName, entry.Info.Name);
                    
                }
                else
                {
                    _tree.Nodes.Add(entry.Info.FullName, entry.Info.Name);
                    
                }
                _tree.ExpandAll();
            });

        }


    }
}
