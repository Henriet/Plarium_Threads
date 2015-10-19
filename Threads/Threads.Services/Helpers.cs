using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Threads.Services.Properties;

namespace Threads.Services
{
    class Helpers
    {
        private readonly string _path;
        private readonly TextBox _erroLogTextBox;

        public Helpers(string path, TextBox erroLogTextBox)
        {
            _path = path;
            _erroLogTextBox = erroLogTextBox;
        }

        public void WriteToFile(string text)
        {
            try
            {
                using (StreamWriter fs = new StreamWriter(_path, true))
                {
                    fs.Write(text);
                }
            }
            catch (IOException)
            {
                WriteToLog(Resources.File_Is_Currently_In_Use, String.Empty);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException();
                WriteToLog(Resources.Have_No_Write_Permissions, String.Empty);
            }
            catch (Exception)
            {
                WriteToLog(Resources.Services_CannotWriteToTheFile, String.Empty);
            }
        }

        public void ClearFile()
        {
            try
            {
                using (StreamWriter fs = new StreamWriter(_path, false))
                {
                    fs.Write(String.Empty);
                }
            }
            catch (Exception)
            {
                WriteToLog(Resources.Services_CannotWriteToTheFile, String.Empty);
            }
        }

        public void WriteToLog(string text, string error)
        {
            try
            {
                _erroLogTextBox.AppendText(String.Format(text, error) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(text, error));
            }
        }
    }
}
