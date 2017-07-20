using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using CountriesMVC.Data.Objects;

namespace CountriesMVC.Data.Repositories
{
    public class XmlRepositorry : IRepository
    {
        private readonly string xmlPath;

        public XmlRepositorry(string xmlFolderPath)
        {
            this.xmlPath = xmlFolderPath + "\\country_storage.xml";
            if (!File.Exists(xmlPath))
            {
                InitializeXmlFile();
            }
        }

        public IEnumerable<Country> GetAll()
        {
            IEnumerable<Country> countries;
            var serializer = new XmlSerializer(typeof(List<Country>));
            using (var reader = XmlReader.Create(xmlPath))
            {
                countries = (List<Country>)serializer.Deserialize(reader);
            }
            return countries;
        }

        public Country GetCountry(string name)
        {
            return GetAll().FirstOrDefault(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public Country GetCountry(Guid id)
        {
            throw new NotImplementedException();
        }

        public void AddCountry(Country country)
        {
            XDocument xDocument = XDocument.Load(xmlPath);
            XElement root = xDocument.Element("ArrayOfCountry");
            IEnumerable<XElement> rows = root.Descendants("Country");
            XElement firstRow = rows.Last();

            firstRow.AddAfterSelf(
                new XElement("Country", 
                    new XElement("Id", country.Id),
                    new XElement("Name", country.Name),
                    new XElement("Capitol", country.Capitol),
                    new XElement("Population", country.Population))
            );

            xDocument.Save(xmlPath);
        }

        private void InitializeXmlFile()
        {
            List<Country> sampleData = new List<Country>
            {
                new Country {Id = Guid.NewGuid(), Name = "England", Capitol = "London", Population = 33434545},
                new Country {Id = Guid.NewGuid(), Name = "France", Capitol = "Paris", Population = 7878475}
            };

            var serializer = new XmlSerializer(sampleData.GetType());
            using (var writer = XmlWriter.Create(xmlPath))
            {
                serializer.Serialize(writer, sampleData);
            }
        }

        private XmlElement SerializeToXmlElement(object o)
        {
            XmlDocument doc = new XmlDocument();
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o);
            }
            return doc.DocumentElement;
        }
    }
}
