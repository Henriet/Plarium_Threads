using System;
using System.Linq;
using System.Windows.Forms;
using Threads.Services.Properties;

namespace Threads.Services
{
    public class TreeEntryService : BaseEntryService
    {
        private readonly TreeView _tree;
        private readonly Helpers _helpers;

        public TreeEntryService(TreeView tree, TextBox erroLogTextBox)
        {
            _helpers = new Helpers(String.Empty, erroLogTextBox);
            _tree = tree;
            try
            {
                _tree.BeginInvoke((MethodInvoker) delegate
                {
                    _tree.Nodes.Clear();
                });
            }
            catch (Exception ex)
            {
                _helpers.WriteToLog(Resources.Error, ex.Message);
            }
        }

        protected override void WriteEntry()
        {
            try
            {
                _tree.Invoke((MethodInvoker) delegate
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
            catch (Exception ex)
            {
                _helpers.WriteToLog(Resources.Error, ex.Message);
            }
        }
    }
}
