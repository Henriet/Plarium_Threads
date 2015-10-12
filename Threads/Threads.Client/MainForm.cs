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
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = Resources.MainForm_TextFilesExtension;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = Resources.MainForm_Text_file_extension;

            FolderBrowser.Description = Resources.MainForm_FolderBrowserDialog_Description_Select_folder_to_scan;
        }

        private void StartClick(object sender, EventArgs e)
        {
            if (_directorySelected && _fileSelected)
            {
                var selectedPath = FolderBrowser.SelectedPath;

                var investigator = new DirectoryInvestigator(selectedPath, treeView);
                progressBar.Step = Helpers.GetCountOfEntries(selectedPath);

                var thread = new Thread(() => investigator.Investigate());
                thread.Start();

            }
            else
            {
                MessageBox.Show(Resources.MainForm_StartClick_Please__select_file_and_folder, Resources.MainForm_Error, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ApplictionExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
