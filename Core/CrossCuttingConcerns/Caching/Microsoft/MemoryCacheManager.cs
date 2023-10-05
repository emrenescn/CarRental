using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            _memoryCache=ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key,value,TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
           return _memoryCache.Get<T>(key);

        }

        public object Get(string key)
        {
            return _memoryCache.Get<object>(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key,out _);
        }

        public void Remove(string key)
        {
             _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            dynamic cacheEntriesCollection = null;
            var cacheEntriesFieldCollectionDefinition = typeof(MemoryCache).GetField("_coherentState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (cacheEntriesFieldCollectionDefinition != null)
            {
                var coherentStateValueCollection = cacheEntriesFieldCollectionDefinition.GetValue(_memoryCache);
                //bu kodda cachelenen elemanların toplantığı EntriesCollection yerini bul diyoruz
                var entriesCollectionValueCollection = coherentStateValueCollection.GetType().GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                //entriescollection içerisindeki elemanları yukarıda null olan cacheEntriesCollection içerisine valueları atadık
                cacheEntriesCollection = entriesCollectionValueCollection.GetValue(coherentStateValueCollection);
            }

            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();//aşağıda patterne uyan kodları hepsini bu listede topladık foreach ile aşağıda sileceğiz


            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);//patternin nasıl olacağını ayarladık
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            //liste içinde patterne uyan elemanları foreach ile teker teker sildik 
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
