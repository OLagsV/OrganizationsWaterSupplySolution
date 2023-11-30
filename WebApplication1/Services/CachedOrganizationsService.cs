using OrganizationsWaterSupplyL4.Data;
using OrganizationsWaterSupplyL4.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OrganizationsWaterSupplyL4.Services
{
    public class CachedOrganizationsService : ICachedService<Organization>
    {
        private readonly OrganizationsWaterSupplyContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public CachedOrganizationsService(OrganizationsWaterSupplyContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }
        public IEnumerable<Organization> GetData(int rowsNumber = 20)
        {
            return _dbContext.Organizations.Take(rowsNumber).ToList();
        }

        public void AddData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Organization> organizations = _dbContext.Organizations.Take(rowsNumber).ToList();
            if (organizations != null)
            {
                _memoryCache.Set(cacheKey, organizations, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(270)
                });

            }

        }
        public IEnumerable<Organization> GetData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Organization> organizations;
            if (!_memoryCache.TryGetValue(cacheKey, out organizations))
            {
                organizations = _dbContext.Organizations.Take(rowsNumber).ToList();
                if (organizations != null)
                {
                    _memoryCache.Set(cacheKey, organizations,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(270)));
                }
            }
            return organizations;
        }
        public void UpdateData(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Organization> organizations;
            organizations = _dbContext.Organizations.Take(rowsNumber).ToList();
            _memoryCache.Set(cacheKey, organizations,
            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(270)));
        }
    }
}

