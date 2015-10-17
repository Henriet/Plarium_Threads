namespace Threads.Client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.treeView = new System.Windows.Forms.TreeView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDirectoryToScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFileForXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectedFileLabel = new System.Windows.Forms.Label();
            this.CurrentFileLabel = new System.Windows.Forms.Label();
            this.SelectedFileNameLabel = new System.Windows.Forms.Label();
            this.CurrentFileNameLabel = new System.Windows.Forms.Label();
            this.SelectedDirecoryLabel = new System.Windows.Forms.Label();
            this.SelectedDirectoryNameLabel = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ChangeDirectoryButton = new System.Windows.Forms.Button();
            this.ChangeFileButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(16, 163);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(473, 262);
            this.treeView.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(16, 474);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(473, 40);
            this.progressBar.TabIndex = 1;
            // 
            // Menu
            // 
            this.Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(524, 28);
            this.Menu.TabIndex = 2;
            this.Menu.Text = "Menu";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectDirectoryToScanToolStripMenuItem,
            this.selectFileForXmlToolStripMenuItem,
            this.scanToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // selectDirectoryToScanToolStripMenuItem
            // 
            this.selectDirectoryToScanToolStripMenuItem.Name = "selectDirectoryToScanToolStripMenuItem";
            this.selectDirectoryToScanToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.selectDirectoryToScanToolStripMenuItem.Text = "Select directory to scan";
            this.selectDirectoryToScanToolStripMenuItem.Click += new System.EventHandler(this.ChangeDirectoryButtonClick);
            // 
            // selectFileForXmlToolStripMenuItem
            // 
            this.selectFileForXmlToolStripMenuItem.Name = "selectFileForXmlToolStripMenuItem";
            this.selectFileForXmlToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.selectFileForXmlToolStripMenuItem.Text = "Select file for xml";
            this.selectFileForXmlToolStripMenuItem.Click += new System.EventHandler(this.ChangeFileButtonClick);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.scanToolStripMenuItem.Text = "Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.StartClick);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ApplictionExit);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showHelpToolStripMenuItem
            // 
            this.showHelpToolStripMenuItem.Name = "showHelpToolStripMenuItem";
            this.showHelpToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.showHelpToolStripMenuItem.Text = "Show help";
            // 
            // SelectedFileLabel
            // 
            this.SelectedFileLabel.AutoSize = true;
            this.SelectedFileLabel.Location = new System.Drawing.Point(12, 77);
            this.SelectedFileLabel.Name = "SelectedFileLabel";
            this.SelectedFileLabel.Size = new System.Drawing.Size(93, 17);
            this.SelectedFileLabel.TabIndex = 3;
            this.SelectedFileLabel.Text = "Selected file: ";
            // 
            // CurrentFileLabel
            // 
            this.CurrentFileLabel.AutoSize = true;
            this.CurrentFileLabel.Location = new System.Drawing.Point(13, 440);
            this.CurrentFileLabel.Name = "CurrentFileLabel";
            this.CurrentFileLabel.Size = new System.Drawing.Size(85, 17);
            this.CurrentFileLabel.TabIndex = 4;
            this.CurrentFileLabel.Text = "Current file: ";
            // 
            // SelectedFileNameLabel
            // 
            this.SelectedFileNameLabel.AutoSize = true;
            this.SelectedFileNameLabel.Location = new System.Drawing.Point(159, 77);
            this.SelectedFileNameLabel.Name = "SelectedFileNameLabel";
            this.SelectedFileNameLabel.Size = new System.Drawing.Size(28, 17);
            this.SelectedFileNameLabel.TabIndex = 5;
            this.SelectedFileNameLabel.Text = ". . .";
            // 
            // CurrentFileNameLabel
            // 
            this.CurrentFileNameLabel.AutoSize = true;
            this.CurrentFileNameLabel.Location = new System.Drawing.Point(98, 440);
            this.CurrentFileNameLabel.Name = "CurrentFileNameLabel";
            this.CurrentFileNameLabel.Size = new System.Drawing.Size(0, 17);
            this.CurrentFileNameLabel.TabIndex = 6;
            // 
            // SelectedDirecoryLabel
            // 
            this.SelectedDirecoryLabel.AutoSize = true;
            this.SelectedDirecoryLabel.Location = new System.Drawing.Point(13, 43);
            this.SelectedDirecoryLabel.Name = "SelectedDirecoryLabel";
            this.SelectedDirecoryLabel.Size = new System.Drawing.Size(130, 17);
            this.SelectedDirecoryLabel.TabIndex = 8;
            this.SelectedDirecoryLabel.Text = "Selected directory: ";
            // 
            // SelectedDirectoryNameLabel
            // 
            this.SelectedDirectoryNameLabel.AutoSize = true;
            this.SelectedDirectoryNameLabel.Location = new System.Drawing.Point(159, 43);
            this.SelectedDirectoryNameLabel.Name = "SelectedDirectoryNameLabel";
            this.SelectedDirectoryNameLabel.Size = new System.Drawing.Size(28, 17);
            this.SelectedDirectoryNameLabel.TabIndex = 9;
            this.SelectedDirectoryNameLabel.Text = ". . .";
            // 
            // ChangeDirectoryButton
            // 
            this.ChangeDirectoryButton.Location = new System.Drawing.Point(414, 37);
            this.ChangeDirectoryButton.Name = "ChangeDirectoryButton";
            this.ChangeDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeDirectoryButton.TabIndex = 10;
            this.ChangeDirectoryButton.Text = "Change";
            this.ChangeDirectoryButton.UseVisualStyleBackColor = true;
            this.ChangeDirectoryButton.Click += new System.EventHandler(this.ChangeDirectoryButtonClick);
            // 
            // ChangeFileButton
            // 
            this.ChangeFileButton.Location = new System.Drawing.Point(414, 71);
            this.ChangeFileButton.Name = "ChangeFileButton";
            this.ChangeFileButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeFileButton.TabIndex = 11;
            this.ChangeFileButton.Text = "Change";
            this.ChangeFileButton.UseVisualStyleBackColor = true;
            this.ChangeFileButton.Click += new System.EventHandler(this.ChangeFileButtonClick);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(16, 119);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(473, 38);
            this.StartButton.TabIndex = 12;
            this.StartButton.Text = "Scan";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 536);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ChangeFileButton);
            this.Controls.Add(this.ChangeDirectoryButton);
            this.Controls.Add(this.SelectedDirectoryNameLabel);
            this.Controls.Add(this.SelectedDirecoryLabel);
            this.Controls.Add(this.CurrentFileNameLabel);
            this.Controls.Add(this.SelectedFileNameLabel);
            this.Controls.Add(this.CurrentFileLabel);
            this.Controls.Add(this.SelectedFileLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "MainForm";
            this.Text = "Information about catalog";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label SelectedFileLabel;
        private System.Windows.Forms.Label CurrentFileLabel;
        private System.Windows.Forms.Label SelectedFileNameLabel;
        private System.Windows.Forms.Label CurrentFileNameLabel;
        private System.Windows.Forms.Label SelectedDirecoryLabel;
        private System.Windows.Forms.Label SelectedDirectoryNameLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem selectDirectoryToScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectFileForXmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.Button ChangeDirectoryButton;
        private System.Windows.Forms.Button ChangeFileButton;
        private System.Windows.Forms.Button StartButton;
    }
}

