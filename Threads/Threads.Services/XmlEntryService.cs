using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using Threads.Domain;
using Threads.Services.Properties;

namespace Threads.Services
{
    public class XmlEntryService : BaseEntryService
    {
        private readonly XmlDocument _xmlDocument;
        private readonly Helpers _helpers;

        public XmlEntryService(string path, TextBox erroLogTextBox)
        {
            _xmlDocument = new XmlDocument();
            var entryNode = _xmlDocument.CreateElement(Resources.Service_Entries);
            _xmlDocument.AppendChild(entryNode);
            _helpers = new Helpers(path, erroLogTextBox);
            _helpers.ClearFile();
        }

        protected override void WriteEntry()
        {
            var node = AddNewEntry(CurrentEntry.Info);
            try
            {
                _helpers.WriteToFile(node.OuterXml);
            }
            catch (UnauthorizedAccessException)
            {
                Stop();
            }
        }

        private XmlElement AddNewEntry(EntryInfo entry) 
        {
            var parentNode = _xmlDocument.CreateElement(Resources.Service_entry);
            Dictionary<string, string> entryDictionary = new Dictionary<string, string>
            {
                {Resources.Service_FullName, entry.FullName},
                {Resources.Service_Creation_Time, entry.CreationTime.ToString(CultureInfo.InvariantCulture)},
                {Resources.Service_Last_Data_Access_Time, entry.LastDataAccessTime.ToString(CultureInfo.InvariantCulture)},
                {Resources.Services_Modification_Time, entry.ModificationTime.ToString(CultureInfo.InvariantCulture)},
                {Resources.Services_Size, entry.Size},
                {Resources.Services_Owner, entry.Owner},
                {"Permissions", entry.Permissions.ToString()}
            };
            try
            {
                foreach (var entryElement in entryDictionary)
                {
                    XmlElement newElement = _xmlDocument.CreateElement(entryElement.Key);
                    newElement.InnerText = entryElement.Value;
                    parentNode.AppendChild(newElement);
                }
                
            }
            catch (Exception ex)
            {
                _helpers.WriteToLog(Resources.Error, ex.Message);
            }
            return parentNode;
        }
    }
}
