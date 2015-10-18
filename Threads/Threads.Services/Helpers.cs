using System;
using System.IO;
using System.Windows.Forms;
using Threads.Services.Properties;

namespace Threads.Services
{
    class Helpers
    {
        private readonly string _path;
        public Helpers(string path)
        {
            _path = path;
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
                MessageBox.Show(Resources.File_Is_Currently_In_Use);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(Resources.Have_No_Write_Permissions);
                throw new UnauthorizedAccessException();
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.Services_CannotWriteToTheFile);
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
                MessageBox.Show(Resources.Services_CannotWriteToTheFile);
            }
        }
    }
}
