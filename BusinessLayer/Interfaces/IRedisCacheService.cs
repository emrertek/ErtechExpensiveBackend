using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRedisCacheService
    {
        void Set<T>(string key, T value, TimeSpan expiration);
        bool TryGet<T>(string key, out T value);
        void Remove(string key);
    }

}
