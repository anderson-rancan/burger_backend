using System.Collections.Generic;

namespace BurgerBackend.Finder.Service.ExternalServices.Models
{
    public sealed class AddVenueRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public IEnumerable<OpeningTime> OpeningTimes { get; set; }
    }
}
