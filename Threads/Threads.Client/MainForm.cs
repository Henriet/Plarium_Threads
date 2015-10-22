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
        private Thread _thread;
        private DirectoryScanner _scanner;

        public MainForm()
        {
            InitializeComponent();
            Helpers.ErrorLogTextBox = ErrorLogTextBox;
        }

        private void StartClick(object sender, EventArgs e)
        {
            if (_directorySelected && _fileSelected)
            {
                StopThread();
                var selectedPath = FolderBrowser.SelectedPath;
               
                try
                {
                    int countOfEntries = 0;
                    EventWaitHandle eventWaitHandle = new AutoResetEvent(false);
                    var helperThread = new Thread( () => Helpers.SetMaximumForProgressBar(selectedPath, ref countOfEntries, eventWaitHandle));
                    helperThread.Start();
                    eventWaitHandle.WaitOne();

                    progressBar.Maximum = countOfEntries;
                    progressBar.Value = 0;
                    var statusUpdater = new StatusUpdater(EnabledButtons){Label = CurrentFileNameLabel, ProgressBar = progressBar};
                    _scanner = new DirectoryScanner(selectedPath, treeView, saveFileDialog.FileName, statusUpdater, ErrorLogTextBox);

                    EnabledButtons(false);
                    _thread = new Thread(() => _scanner.Scan());
                    _thread.Start();
                }
                catch (ArgumentException)
                {
                    Helpers.WriteToLog(Resources.MainForm_DirectoryDoes_not_exsist, selectedPath);
                }
                catch (UnauthorizedAccessException)
                {
                    Helpers.WriteToLog(Resources.Access_denided_For, saveFileDialog.FileName);
                }
                catch (ThreadAbortException)
                { }
                catch (Exception ex)
                {
                    Helpers.WriteToLog(Resources.Error_message, ex.Message);
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

        private void ChangeClick(bool directoryChanged, ref bool selectedFlag, Label label)
        {
            var result = directoryChanged ? FolderBrowser.ShowDialog() : saveFileDialog.ShowDialog();

            if (result != DialogResult.OK) return;

            label.Text = directoryChanged ? FolderBrowser.SelectedPath : saveFileDialog.FileName;
            selectedFlag = true;

            StartButton.Enabled = _directorySelected && _fileSelected;
        }

        public void EnabledButtons(bool enabled)
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
            if(_scanner != null)
                _scanner.FinishScanning();
            if (_thread != null && _thread.IsAlive)
                _thread.Abort();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopThread();
            Application.Exit();
        }
    }
}
