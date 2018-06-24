﻿using EInfrastructure.Core.Redis.Config;

namespace EInfrastructure.Core.Redis.Common
{
    /// <summary>
    /// 基于CsRedisCore的实现
    /// </summary>
    public partial class CsRedisHelper: QuickHelperBase
    {
        public static void InitializeConfiguration(RedisConfig redisConfig)
        {
            int port, poolsize, database;
            string ip, pass;
            port = redisConfig.Port;
            poolsize = redisConfig.PoolSize;
            database = redisConfig.DataBase;
            ip = redisConfig.Ip;
            pass = redisConfig.Password;
            Name = redisConfig.Name;
            Instance = new ConnectionPool(ip, port, poolsize);
            Instance.Connected += (s, o) => {
                RedisClient rc = s as RedisClient;
                if (!string.IsNullOrEmpty(pass)) rc.Auth(pass);
                if (database > 0) rc.Select(database);
            };
        }
    }
}
