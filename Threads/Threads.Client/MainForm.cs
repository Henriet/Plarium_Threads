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
                var selectedPath = FolderBrowser.SelectedPath;
                progressBar.Maximum = Helpers.GetCountOfEntries(selectedPath);
                progressBar.Value = 0;
                try
                {
                    var scanner = new DirectoryScanner(selectedPath, treeView, progressBar, CurrentFileNameLabel,
                        SelectedFileNameLabel.Text);

                    var thread = new Thread(() => scanner.Scan());
                    thread.Start();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(String.Format("Directory {0} doesn't exist", selectedPath));
                }
            }
            else
            {
                MessageBox.Show(Resources.MainForm_StartClick_Please__select_file_and_folder, Resources.MainForm_Error, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeDirectoryButtonClick(object sender, EventArgs e)
        {
            ChangeClick(true, ref _directorySelected, SelectedDirectoryNameLabel);
        }

        private void ChangeFileButtonClick(object sender, EventArgs e)
        {
            ChangeClick(false, ref _fileSelected, SelectedFileNameLabel);
        }

        private void ChangeClick(bool directoryChanged, ref bool changedFlag, Label label)
        {
            var result = directoryChanged ? FolderBrowser.ShowDialog() : saveFileDialog.ShowDialog();

            if (result != DialogResult.OK) return;

            label.Text = directoryChanged ? FolderBrowser.SelectedPath : saveFileDialog.FileName;
            changedFlag = true;

            if (_directorySelected && _fileSelected)
                StartButton.Enabled = true;
        }
    }
}
