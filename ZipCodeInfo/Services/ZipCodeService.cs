using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using ZipCodeInfo.Models;

namespace ZipCodeInfo.Services
{
    /// <summary>
    /// Stores zip codes in-memory but can be changed to use a different data
    /// store.
    /// </summary>
    public class ZipCodeService : IZipCodeService
    {
        private const string CacheKey = "PreviousEntries";

        private readonly IMemoryCache _cache;

        public ZipCodeService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Add(GeographicDataModel zipCode)
        {
            if (!_cache.TryGetValue(CacheKey, out ICollection<GeographicDataModel> entries))
            {
                entries = new List<GeographicDataModel>();
            }

            entries.Add(zipCode);

            _cache.Set(CacheKey, entries);
        }

        public IEnumerable<GeographicDataModel> GetAll()
        {
            return _cache.GetOrCreate(CacheKey, _ => { return new List<GeographicDataModel>(); });
        }
    }
}
