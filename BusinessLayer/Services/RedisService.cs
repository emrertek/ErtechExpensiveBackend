using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;

        public RedisService(IConfiguration configuration)
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]);
            _database = redis.GetDatabase();
        }

        public async Task SetAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }

        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.HasValue ? value.ToString() : null;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
    }
}
