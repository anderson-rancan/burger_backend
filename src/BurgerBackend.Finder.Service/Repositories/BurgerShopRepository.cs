using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BurgerBackend.Finder.Service.ExternalServices.Models;

namespace BurgerBackend.Finder.Service.Repositories
{
    public interface IBurgerShopRepository
    {
        bool Add(BurgerShop newShop);
        BurgerShop Get(Guid id);
        bool Delete(Guid id);
        int GetAproxQuantity();
        IEnumerable<BurgerShop> GetAll(int top, int skip);
        IEnumerable<BurgerShop> GetAllFromZip(IEnumerable<string> zipCodes, int top, int skip);
    }

    internal sealed class BurgerShopRepository : IBurgerShopRepository
    {
        private readonly ConcurrentDictionary<Guid, BurgerShop> _burgerShopDictionary = new();

        public BurgerShopRepository()
        {
            var id = Guid.NewGuid();
            _burgerShopDictionary.TryAdd(id, new BurgerShop(id, "Black Cab Burger", "Berzsenyi u. 1", "Budapest", "1191", new List<OpeningTime> { new OpeningTime("MON-SUN", 9, 23) }));

            id = Guid.NewGuid();
            _burgerShopDictionary.TryAdd(id, new BurgerShop(id, "Zing Burger Shopmark", "Üllői út 201", "Budapest", "1191", new List<OpeningTime> { new OpeningTime("MON-SUN", 9, 23) }));

            id = Guid.NewGuid();
            _burgerShopDictionary.TryAdd(id, new BurgerShop(id, "Zing Burger", "Pillangó u. 12", "Budapest", "1149", new List<OpeningTime> { new OpeningTime("MON-SUN", 9, 23) }));

            id = Guid.NewGuid();
            _burgerShopDictionary.TryAdd(id, new BurgerShop(id, "Burger Market Árkád", "Örs vezér tere 25/A", "Budapest", "1106", new List<OpeningTime> { new OpeningTime("MON-SUN", 9, 23) }));

            id = Guid.NewGuid();
            _burgerShopDictionary.TryAdd(id, new BurgerShop(id, "CheChe Bistrocafe", "Teréz krt. 56", "Budapest", "1066", new List<OpeningTime> { new OpeningTime("MON-SUN", 9, 23) }));
        }

        public bool Add(BurgerShop newShop) => _burgerShopDictionary.TryAdd(newShop.Id, newShop);

        public bool Delete(Guid id) => _burgerShopDictionary.TryRemove(id, out _);

        public BurgerShop Get(Guid id) => _burgerShopDictionary.TryGetValue(id, out var shop) ? shop : null;

        public IEnumerable<BurgerShop> GetAll(int top, int skip) => _burgerShopDictionary.Values
            .OrderBy(v => v.Name)
            .Skip(skip)
            .Take(top);

        public IEnumerable<BurgerShop> GetAllFromZip(IEnumerable<string> zipCodes, int top, int skip) => _burgerShopDictionary.Values
            .OrderBy(v => v.Name)
            .Where(v => zipCodes.Contains(v.Zip, StringComparer.OrdinalIgnoreCase))
            .Skip(skip)
            .Take(top);

        public int GetAproxQuantity() => _burgerShopDictionary.Count;
    }
}
