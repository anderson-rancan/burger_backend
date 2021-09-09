using System;
using System.Collections.Generic;
using System.Linq;

namespace BurgerBackend.Finder.Service.ExternalServices.Models
{
    public sealed class BurgerShop
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public List<OpeningTime> OpeningTimes { get; set; }
        public Guid CreationUserId { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public BurgerShop()
        {
        }

        public BurgerShop(Guid id, string name, string address, string city, string zip, IEnumerable<OpeningTime> openingTimes)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            Zip = zip;
            OpeningTimes = openingTimes.ToList();
        }
    }
}
