using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Threads.Domain;

namespace Threads.Services
{
    public class XmlEntryHandler : BaseEntryHanbler
    {
        private static int count;
        private XDocument _xmlDocument;

        public XmlEntryHandler()
        {
            _xmlDocument = new XDocument();
            
        }
/*
        public void AddNewEntry(object param)
        {
            var xmlParams = (EntryHandlerParameters)param;
            AddNewNode(xmlParams);
           

            var fileName = @"C:\Users\aleon_000\Desktop\test.txt";
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(GetXml());
            }

            xmlParams.AutoResetEvent.Set();
        }

        string GetXml()
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                _xmlDocument.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        void AddNewNode(EntryHandlerParameters param)
        {
            XElement entryNode = new XElement(param.EntryType.ToString(),
                new XAttribute("Name", param.Name),
                new XElement("Creation_time", param.CreationTime.ToString()),
                new XElement("Size", param.Size.ToString())
                );
            if (_xmlDocument.Root != null) _xmlDocument.Root.Add(entryNode);
        }*/
        protected override void WriteEntry(Entry entry)
        {
            //todo
        }
    }
 
}
