using System;
using System.Runtime.Caching;

namespace Tool
{
    /// <summary>
    /// 記憶體快取
    /// </summary>
    public static class InMemoryCache
    {
        /// <summary>
        /// 從Catch取資料,無資料時執行CallBack
        /// </summary>
        /// <typeparam name="T">資料型別</typeparam>
        /// <param name="cacheKey">索引</param>
        /// <param name="getItemCallback">無資料時CalbackFunction</param>
        /// <param name="timeOut">暫存秒數</param>
        /// <returns>資料物件</returns>
        /// 

        public static T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, int timeOut = 10*60) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddSeconds(timeOut));
            }
            return item;
        }
        /// <summary>
        /// 從Catch取資料
        /// </summary>
        /// <typeparam name="T">資料型別</typeparam>
        /// <param name="cacheKey">索引</param>
        /// <returns></returns>
        public static T Get<T>(string cacheKey) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            return item ?? null;
        }
        /// <summary>
        /// 寫入Catch資料
        /// </summary>
        /// <typeparam name="T">資料型別</typeparam>
        /// <param name="cacheKey">索引鍵</param>
        /// <param name="item">存入快取的物件</param>
        /// <param name="timeOut">暫存秒數</param>
        /// <returns>資料</returns>
        public static T Set<T>(string cacheKey, T item,int timeOut = 10*60) where T : class
        {
            if (item != null)
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddSeconds(timeOut));

            return item;
        }
        /// <summary>
        /// 刪除快取
        /// </summary>
        /// <param name="cacheKey">索引</param>
        public static void Remove(string cacheKey)
        {
            if (cacheKey != null)
                MemoryCache.Default.Remove(cacheKey);
        }
    }
}