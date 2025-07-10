using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRedisService
    {
        Task SetAsync(string key, string value);
        Task<string?> GetAsync(string key);
        Task<bool> RemoveAsync(string key);
    }
}
