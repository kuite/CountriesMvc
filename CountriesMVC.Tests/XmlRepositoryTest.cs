using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountriesMVC.Data.Objects;
using CountriesMVC.Data.Repositories;
using NUnit.Framework;

namespace CountriesMVC.Tests
{
    public class XmlRepositoryTest
    {
        private IRepository xmlRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            xmlRepository = new XmlRepositorry(ConfigurationManager.AppSettings["xmlPathTest"]);
        }

        [Test]
        public void CreateCountry()
        {
            var id = Guid.NewGuid();
            var name = "Russia";
            var country = new Country { Id = id, Name = name, Capitol = "Moskow", Population = 33888994};
            xmlRepository.AddCountry(country);
            Country saved = xmlRepository.GetCountry(name);

            Assert.True(saved != null);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            File.Delete(ConfigurationManager.AppSettings["xmlPathTest"] + "\\country_storage.xml");
        }
    }
}
