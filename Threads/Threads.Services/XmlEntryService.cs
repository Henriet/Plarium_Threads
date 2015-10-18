using System.Globalization;
using System.Xml;
using Threads.Domain;
using Threads.Services.Properties;

namespace Threads.Services
{
    public class XmlEntryService : BaseEntryService
    {
        private readonly XmlDocument _xmlDocument;
        private readonly Helpers _helpers;

        public XmlEntryService(string path)
        {
            _xmlDocument = new XmlDocument();
            var entryNode = _xmlDocument.CreateElement(Resources.Service_Entries);
            _xmlDocument.AppendChild(entryNode);
            _helpers = new Helpers(path);
            _helpers.ClearFile();
        }

        protected override void WriteEntry()
        {
            var node = AddNewEntry(CurrentEntry);
            _helpers.WriteToFile(node.OuterXml); 
        }

        private XmlElement AddNewEntry(Entry entry)
        {
            var entryNode = _xmlDocument.CreateElement(Resources.Service_entry);

            AddEntryInfoNode(Resources.Service_FullName, entry.Info.FullName, entryNode);
            AddEntryInfoNode(Resources.Service_Creation_Time, entry.Info.CreationTime.ToString(CultureInfo.InvariantCulture), entryNode);
            AddEntryInfoNode(Resources.Service_Last_Data_Access_Time, entry.Info.LastDataAccessTime.ToString(CultureInfo.InvariantCulture), entryNode);
            AddEntryInfoNode(Resources.Services_Modification_Time, entry.Info.ModificationTime.ToString(CultureInfo.InvariantCulture), entryNode);
            AddEntryInfoNode(Resources.Services_Size, entry.Info.Size, entryNode);
            AddEntryInfoNode(Resources.Services_Owner, entry.Info.Owner, entryNode);
            return entryNode;
        }

        private void AddEntryInfoNode(string nodeName, string value, XmlElement parentElement)
        {
            XmlElement newElement = _xmlDocument.CreateElement(nodeName);
            newElement.InnerText = value;
            parentElement.AppendChild(newElement);
        }
    }

}
