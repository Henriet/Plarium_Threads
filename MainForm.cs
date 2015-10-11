using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadsTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void StartClick(object sender, EventArgs e)
        {

            var result = FolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                var investigator = new DirectoryInvestigator(FolderBrowser.SelectedPath);
                investigator.Investigate(treeView);
            }
        }
    }
}
