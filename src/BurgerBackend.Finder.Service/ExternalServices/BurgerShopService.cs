using System;
using System.Collections.Generic;
using System.Linq;
using BurgerBackend.Finder.Service.ExternalServices.Models;
using BurgerBackend.Finder.Service.Repositories;
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
        IEnumerable<BurgerShop> GetShopListByZipCode(IEnumerable<string> zipCodes, int top, int skip);
        IEnumerable<BurgerShop> GetAll(int top, int skip);
    }

    internal sealed class BurgerShopService : IBurgerShopService
    {
        private readonly IBurgerShopRepository _burgerShopRepository;

        public BurgerShopService(IBurgerShopRepository burgerShopRepository)
        {
            _burgerShopRepository = burgerShopRepository;
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

            _burgerShopRepository.Add(newShop);
            return newShop.Id;
        }

        public BurgerShop GetById(Guid id) => _burgerShopRepository.Get(id);

        public void AddOrUpdate(Guid id, UpdateVenueRequest request, GetUserResponse user)
        {
            var shop = _burgerShopRepository.Get(id);

            if (shop == null)
            {
                shop = new BurgerShop
                {
                    Id = Guid.NewGuid(),
                    CreationUserId = user.Id
                };

                _burgerShopRepository.Add(shop);
            }

            if (!string.IsNullOrWhiteSpace(request.Name)) shop.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Address)) shop.Address = request.Address;
            if (!string.IsNullOrWhiteSpace(request.City)) shop.City = request.City;
            if (!string.IsNullOrWhiteSpace(request.Zip)) shop.Zip = request.Zip;
            if (request.OpeningTimes?.Any() == true) shop.OpeningTimes = request.OpeningTimes.ToList();

            shop.LastUpdateUserId = user.Id;
        }

        public bool Delete(Guid id) => _burgerShopRepository.Delete(id);

        public IEnumerable<BurgerShop> GetShopListByZipCode(IEnumerable<string> zipCodes, int top, int skip)
            => _burgerShopRepository.GetAllFromZip(zipCodes, top, skip);

        public IEnumerable<BurgerShop> GetAll(int top, int skip) => _burgerShopRepository.GetAll(top, skip);

        public int GetQuantity() => _burgerShopRepository.GetAproxQuantity();
    }
}
