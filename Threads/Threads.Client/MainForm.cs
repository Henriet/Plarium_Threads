using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Threads.Client.Properties;

namespace Threads.Client
{
    public partial class MainForm : Form
    {
        private bool _fileSelected;
        private bool _directorySelected;
        private Thread _thread;

        public MainForm()
        {
            InitializeComponent();
        }

        private void StartClick(object sender, EventArgs e)
        {
            if (_directorySelected && _fileSelected)
            {
                StopThread();

                var selectedPath = FolderBrowser.SelectedPath;
                progressBar.Maximum = Helpers.GetCountOfEntries(selectedPath);
                progressBar.Value = 0;
                try
                {
                    var scanner = new DirectoryScanner(selectedPath, treeView, progressBar, CurrentFileNameLabel,
                        SelectedFileNameLabel.Text, EnabledButtons);

                    EnabledButtons(false);
                    _thread = new Thread(() => scanner.Scan());
                    _thread.Start();
                }
                catch (ArgumentException)
                {
                    MessageBox.Show(String.Format(Resources.MainForm_DirectoryDoes_not_exsist, selectedPath));
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show(String.Format(Resources.Access_denided_For, saveFileDialog.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(Resources.Error_message, ex.InnerException.Message));
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

        private void EnabledButtons(bool enabled)
        {
            StartButton.Enabled = enabled;
            ChangeDirectoryButton.Enabled = enabled;
            ChangeFileButton.Enabled = enabled;
            StopButton.Enabled = !enabled;
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
             StopThread();
             EnabledButtons(true);
        }

        private void StopThread()
        {
            if (_thread != null && _thread.IsAlive)
                _thread.Abort();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopThread();
            Application.Exit();
        }
    }
}
