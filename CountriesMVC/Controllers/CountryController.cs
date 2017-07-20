using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CountriesMVC.Data;
using CountriesMVC.Data.Objects;
using CountriesMVC.Data.Repositories;

namespace CountriesMVC.Controllers
{
    public class CountryController : Controller
    {
        private readonly IRepository xmlRepository;

        public CountryController(IRepository xmlRepository)
        {
            this.xmlRepository = xmlRepository;
        }

        public ActionResult Index()
        {
            List<Country> countries = new List<Country> {new Country {Name = "Norway", Capitol = "Oslo"} };
            countries = xmlRepository.GetAll().ToList();
            return View("Index", countries);
        }

        public ActionResult Details(string name)
        {
            Country detailedCountry = xmlRepository.GetCountry(name);
            return View(detailedCountry);
        }

        public ActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult AddCountry(Country country)
        {
            country.Id = Guid.NewGuid();
            xmlRepository.AddCountry(country);
            return Index();
        }
    }
}