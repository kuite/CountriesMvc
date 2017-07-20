using System;

namespace CountriesMVC.Data.Objects
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Capitol { get; set; }
        public int Population { get; set; }
    }
}
