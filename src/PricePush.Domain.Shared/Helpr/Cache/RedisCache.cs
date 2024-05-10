using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PricePush.Helpr.Cache
{
    public class RedisCache
    {
        static ConnectionMultiplexer redis; 
        static IDatabase m_RedisDb;
        public RedisCache(string connect,int database)
        {
            redis = ConnectionMultiplexer.Connect(connect);
            m_RedisDb = redis.GetDatabase(database);
        }
        #region String
        /// <summary>
        /// 获取单个字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? StringGet(string key)
        {
            if (m_RedisDb.KeyExists(key) == false)
            {
                return null;
            }
            return m_RedisDb.StringGet(key)!;
        }
        /// <summary>
        /// 写入单个字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="effectiveTime">有效期s 默认不失效</param>
        public static void StringSet(string key, string value, int effectiveTime = 0)
        {
            m_RedisDb.StringSet(key, value);
            ModifyKeyExpire(key, effectiveTime);
        }

        /// <summary>
        /// 获取多个字符串类型
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static List<string>? StringGet(List<string> keys)
        {
            var redisKeys = Array.ConvertAll(keys.ToArray(), x => (RedisKey)x);

            var results = m_RedisDb.StringGet(redisKeys);
            if (results == null || results.Any() == false)
            {
                return null;
            }
            var output = Array.ConvertAll(results, x => (string)x!);

            return output.ToList();
        }
        /// <summary>
        /// 写入多个字符串类型
        /// </summary>
        /// <param name="effectiveTime">有效期s 默认不失效</param>
        public static void StringSet(Dictionary<string, string> values, int effectiveTime = 0)
        {
            KeyValuePair<RedisKey, RedisValue>[] redisEntries = values
            .Select(kv => new KeyValuePair<RedisKey, RedisValue>(kv.Key, kv.Value))
            .ToArray();

            m_RedisDb.StringSet(redisEntries);
            foreach (var item in values)
            {
                ModifyKeyExpire(item.Key, effectiveTime);
            }
        }

        #endregion

        #region List
        /// <summary>
        /// 写入list的value，如key已存在 value会叠加
        /// </summary>
        /// <typeparam name="T">单个对象</typeparam>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <param name="effectiveTime">有效期/s</param>
        public static void ListSet<T>(string key, List<T> list, int effectiveTime = 0)
        {
            RedisValue[] values = Array.ConvertAll(list.ToArray(), x => (RedisValue)JsonConvert.SerializeObject(x));
            m_RedisDb.ListRightPush(key, values);

            ModifyKeyExpire(key, effectiveTime);
        }
        /// <summary>
        /// 获取list类型的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T>? ListGet<T>(string key)
        {

            if (m_RedisDb.KeyExists(key) == false)
            {
                return null;
            }
            RedisValue[] results = m_RedisDb.ListRange(key);

            List<T> output = new List<T>();
            foreach (RedisValue result in results)
            {
                var item = JsonConvert.DeserializeObject<T>(result.ToString());
                output.Add(item!);
            }
            return output;
        }
        #endregion

        #region Hash
        /// <summary>
        /// 读取整个key的hash内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, T>? HashGetAll<T>(string key)
        {
            if (m_RedisDb.KeyExists(key) == false)
            {
                return null;
            }
            HashEntry[] results = m_RedisDb.HashGetAll(key);

            Dictionary<string, T> output = new Dictionary<string, T>();
            foreach (var result in results)
            {
                var item = JsonConvert.DeserializeObject<T>(result.Value!);
                output.Add(result.Name!, item!);
            }
            return output;
        }
        /// <summary>
        /// 写入整个key的hash类型，value已存在会覆盖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hash"></param>
        /// <param name="effectiveTime"></param>
        public static void HashSetAll<T>(string key, Dictionary<string, T> hash, int effectiveTime = 0)
        {
            HashEntry[] entries = new HashEntry[hash.Count];
            int i = 0;
            foreach (var pair in hash)
            {
                entries[i] = new HashEntry(pair.Key, (RedisValue)JsonConvert.SerializeObject(pair.Value));
                i++;
            }
            m_RedisDb.HashSet(key, entries);

            ModifyKeyExpire(key, effectiveTime);

        }
        /// <summary>
        /// 删除整个hash类型的key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashKey"></param>
        public static void HashKeyDelete(string key, string hashKey)
        {
            if (m_RedisDb.KeyExists(key) == false || m_RedisDb.HashExists(hashKey, hashKey) == false)
            {
                return;
            }
            m_RedisDb.HashDelete(key, hashKey);
        }
        /// <summary>
        /// 删除多个hash类型的key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashKeys"></param>
        public static void HashKeyDelete(string key, string[] hashKeys)
        {
            if (m_RedisDb.KeyExists(key) == false)
            {
                return;
            }
            var hashFields = Array.ConvertAll(hashKeys.ToArray(), x => (RedisValue)x);
            m_RedisDb.HashDelete(key, hashFields);
        }
        /// <summary>
        /// 获取hash类型下的单个key 的其中一个hashField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashKey"></param>
        /// <returns></returns>
        public static T? HashKeyGet<T>(string key, string hashKey)
        {
            if (m_RedisDb.KeyExists(key) == false)
            {
                return default(T);
            }
            RedisValue result = m_RedisDb.HashGet(key, hashKey);

            var output = JsonConvert.DeserializeObject<T>(result.ToString());

            return output;
        }
        /// <summary>
        /// 获取hash类型下的单个key 的其中多个hashField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashKeys"></param>
        /// <returns></returns>
        public static List<T>? HashKeyGet<T>(string key, string[] hashKeys)
        {
            if (m_RedisDb.KeyExists(key) == false)
            {
                return default(List<T>);
            }
            var output = new List<T>();
            var hashFields = Array.ConvertAll(hashKeys.ToArray(), x => (RedisValue)x);
            RedisValue[] results = m_RedisDb.HashGet(key, hashFields);
            foreach (var result in results)
            {
                var item = JsonConvert.DeserializeObject<T>(result.ToString());
                output.Add(item!);
            }
            return output;
        }
        /// <summary>
        /// 向hash类型的key中写入一个hashField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashKey"></param>
        /// <param name="value"></param>
        public static void HashKeyAdd<T>(string key, string hashKey, T value)
        {
            m_RedisDb.HashSet(key, hashKey, (RedisValue)JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 向hash类型的key中写入多个hashField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashKeyValue"></param>
        public static void HashKeyAdd<T>(string key, Dictionary<string, T> hashKeyValue)
        {
            var hashEntries = new List<HashEntry>();
            foreach (var keyEntry in hashKeyValue)
            {
                var hashEntrie = new HashEntry(keyEntry.Key, JsonConvert.SerializeObject(keyEntry.Value));
                hashEntries.Add(hashEntrie);
            }
            m_RedisDb.HashSet(key, hashEntries.ToArray());
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除指定key
        /// </summary>
        /// <param name="key"></param>
        public static void DeleteKey(string key)
        {
            if (m_RedisDb.KeyExists(key) == false)
            {
                return;
            }
            m_RedisDb.KeyDelete(key);
        }
        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys"></param>
        public static void DeleteKey(List<string> keys)
        {
            var redisKeys = Array.ConvertAll(keys.ToArray(), x => (RedisKey)x);

            m_RedisDb.KeyDelete(redisKeys);
        }
        /// <summary>
        /// 删除当前链接库所有key
        /// </summary>
        public static void DeleteFlush()
        {
            m_RedisDb.Execute("FLUSHDB");
        }
        #endregion

        #region Expire
        /// <summary>
        /// 修改key的有效期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="effectiveTime"></param>
        public static void ModifyKeyExpire(string key, int effectiveTime = 0)
        {
            m_RedisDb.KeyExpire(key, effectiveTime <= 0 ? null : TimeSpan.FromSeconds(effectiveTime));
        }
        /// <summary>
        /// 获取key的有有效期
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetkeyExpire(string key)
        {
            TimeSpan? expiration = m_RedisDb.KeyTimeToLive(key);
            if (expiration.HasValue)
            {
                return DateTime.Now.Add(expiration.Value);
            }
            return DateTime.MinValue;
        }
        #endregion

        #region Key
        /// <summary>
        /// 搜索key "user*"
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string>? SearchKeys(string? key)
        {
            var keys = m_RedisDb.Multiplexer.GetServer("127.0.0.1:6379").Keys(pattern: key);
            if (keys == null || keys.Any() == false)
            {
                return null;
            }
            var output = Array.ConvertAll(keys.ToArray(), x => (string)x!);
            return output.ToList();
        }
        /// <summary>
        /// 获取指定key的数据类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static RedisType GetKeyType(string key)
        {
            return m_RedisDb.KeyType(key);
        }
        #endregion
    }
}
