using System;
using System.Collections.Generic;
using System.Linq;
using BurgerBackend.Finder.Service.ExternalServices.Models;
using BurgerBackend.Identity.Interface.Services.Models;

namespace BurgerBackend.Finder.Service.ExternalServices
{
    public interface IBurgerShopService
    {
        Guid Add(AddVenueRequest request, GetUserResponse user);
        BurgerShop GetById(Guid id);
        void AddOrUpdate(Guid id, UpdateVenueRequest request, GetUserResponse user);
        bool Delete(Guid id);
        int GetQuantity();
        IEnumerable<BurgerShop> GetShopListByZipCode(IEnumerable<string> zipCodes);
        IEnumerable<BurgerShop> GetAll(int top, int skip);
    }

    internal sealed class BurgerShopService : IBurgerShopService
    {
        private readonly List<BurgerShop> _burgerShopList;

        public BurgerShopService()
        {
            _burgerShopList = new List<BurgerShop>
            {
                new BurgerShop(Guid.NewGuid(), "Black Cab Burger", "Berzsenyi u. 1", "Budapest", "1191", new List<OpeningTime>{ new OpeningTime("MON-SUN", 9, 23) }),
                new BurgerShop(Guid.NewGuid(), "Zing Burger Shopmark", "Üllői út 201", "Budapest", "1191", new List<OpeningTime>{ new OpeningTime("MON-SUN", 9, 23) }),
                new BurgerShop(Guid.NewGuid(), "Zing Burger", "Pillangó u. 12", "Budapest", "1149", new List<OpeningTime>{ new OpeningTime("MON-SUN", 9, 23) }),
                new BurgerShop(Guid.NewGuid(), "Burger Market Árkád", "Örs vezér tere 25/A", "Budapest", "1106", new List<OpeningTime>{ new OpeningTime("MON-SUN", 9, 23) }),
                new BurgerShop(Guid.NewGuid(), "CheChe Bistrocafe", "Teréz krt. 56", "Budapest", "1066", new List<OpeningTime>{ new OpeningTime("MON-SUN", 9, 23) })
            };
        }

        public Guid Add(AddVenueRequest request, GetUserResponse user)
        {
            var newShop = new BurgerShop
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                Zip = request.Zip,
                OpeningTimes = request.OpeningTimes.ToList(),
                CreationUserId = user.Id,
                LastUpdateUserId = user.Id
            };

            _burgerShopList.Add(newShop);
            return newShop.Id;
        }

        public BurgerShop GetById(Guid id) => _burgerShopList.FirstOrDefault(b => b.Id == id);

        public void AddOrUpdate(Guid id, UpdateVenueRequest request, GetUserResponse user)
        {
            var shop = _burgerShopList.FirstOrDefault(b => b.Id == id);

            if (shop == null)
            {
                shop = new BurgerShop
                {
                    Id = Guid.NewGuid(),
                    CreationUserId = user.Id
                };

                _burgerShopList.Add(shop);
            }

            if (!string.IsNullOrWhiteSpace(request.Name)) shop.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Address)) shop.Address = request.Address;
            if (!string.IsNullOrWhiteSpace(request.City)) shop.City = request.City;
            if (!string.IsNullOrWhiteSpace(request.Zip)) shop.Zip = request.Zip;
            if (request.OpeningTimes?.Any() == true) shop.OpeningTimes = request.OpeningTimes.ToList();

            shop.LastUpdateUserId = user.Id;
        }

        public bool Delete(Guid id)
        {
            var shop = _burgerShopList.FirstOrDefault(b => b.Id == id);

            if (shop != null)
            {
                _burgerShopList.Remove(shop);
                return true;
            }

            return false;
        }

        public IEnumerable<BurgerShop> GetShopListByZipCode(IEnumerable<string> zipCodes)
            => _burgerShopList.Where(b => zipCodes.Contains(b.Zip, StringComparer.OrdinalIgnoreCase));

        public IEnumerable<BurgerShop> GetAll(int top, int skip) => _burgerShopList.OrderBy(b => b.Name).Skip(skip).Take(top);

        public int GetQuantity() => _burgerShopList.Count;
    }
}
