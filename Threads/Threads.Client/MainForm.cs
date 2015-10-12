using System;
using System.Threading;
using System.Windows.Forms;
using Threads.Client.Properties;

namespace Threads.Client
{
    public partial class MainForm : Form
    {
        private bool _fileSelected;
        private bool _directorySelected;
        public MainForm()
        {
            InitializeComponent();
        }

        private void StartClick(object sender, EventArgs e)
        {
            if (_directorySelected && _fileSelected)
            {
                var investigator = new DirectoryInvestigator(FolderBrowser.SelectedPath, treeView);
                //progressBar.Step = investigator.GetCount(); todo move count to helpers
                var thread = new Thread(() => investigator.Investigate());

                thread.Start();
            }
            else
            {
                MessageBox.Show(Resources.MainForm_StartClick_Please__select_file_and_folder);
            }
        }


        private void ChangeDirectoryButtonClick(object sender, EventArgs e)
        {
            var result = FolderBrowser.ShowDialog();
            if (result != DialogResult.OK) return;

            _directorySelected = true;
            SelectedDirectoryNameLabel.Text = FolderBrowser.SelectedPath;
        }

        private void ChangeFileButtonClick(object sender, EventArgs e)
        {
            var result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            _fileSelected = true;
            SelectedFileNameLabel.Text = saveFileDialog.FileName;
        }

        private void AddNewNode(string name)
        {
            progressBar.Value++;
            CurrentFileNameLabel.Text = name;
        }

    }
}
