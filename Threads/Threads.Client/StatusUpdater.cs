using System;
using System.Windows.Forms;
using Threads.Client.Properties;

namespace Threads.Client
{
    public class StatusUpdater
    {
        public ProgressBar ProgressBar { get; set; }
        public Label Label { get; set; }

        //delegate for informating main thread about finishing scaning
        public delegate void FinishScan(bool enabled);
        private readonly FinishScan _formFinishScanDelegate;

        public StatusUpdater(FinishScan formFinishScanDelegate)
        {
            _formFinishScanDelegate = formFinishScanDelegate;
        }

        private const int MaxLableLength = 45;

        public void UpdateProgress(string entryName)
        {
            try
            {
                if(entryName == null || Label == null || ProgressBar == null)
                    return;

                Label.BeginInvoke((MethodInvoker)delegate
                {
                    string fullName = entryName;
                    int fullNameLength = fullName.Length;
                    if(fullNameLength > MaxLableLength)
                        fullName = String.Format("...{0}", fullName.Substring(fullName.Length - MaxLableLength));

                    Label.Text = fullName;
                });

                ProgressBar.BeginInvoke((MethodInvoker)delegate
                {
                    ProgressBar.PerformStep();
                });
            }
            catch (Exception ex)
            {
                Helpers.WriteToLog(Resources.Error_message, ex.Message);
            }
        }

        public void Finish()
        {
            try
            {
                if(Label == null)
                    return;
                Label.BeginInvoke((MethodInvoker)delegate
                {
                    Label.Text = String.Empty;
                    _formFinishScanDelegate(true);
                });
            }
            catch (Exception ex)
            {
                Helpers.WriteToLog(Resources.Error_message, ex.Message);
            }
        }
    }
}
