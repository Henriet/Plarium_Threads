using System;
using System.IO;

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
            using (StreamWriter fs = new StreamWriter(_path, true))
            {
                fs.WriteLine(text);
            }
        }

        public void ClearFile()
        {
            using (StreamWriter fs = new StreamWriter(_path, false))
            {
                fs.Write(String.Empty);
            }
        }
    }
}
