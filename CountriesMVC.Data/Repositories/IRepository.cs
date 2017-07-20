using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountriesMVC.Data.Objects;

namespace CountriesMVC.Data.Repositories
{
    public interface IRepository //instances using diffrent ways of xml reads
    {
        IEnumerable<Country> GetAll();
        Country GetCountry(string name);
        Country GetCountry(Guid id);
        void AddCountry(Country country);
    }
}
