using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Model.DbSetting
{
    /// <summary>
    /// 全局的参数配置
    /// </summary>
    public static class Setting
    {
        static Setting()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DbConnection = config.GetConnectionString("DbConnection");
            ReadConnection = config.GetConnectionString("ReadConnection");
            RedisConnection = config.GetConnectionString("RedisConnection");
            Pusher = config.GetConnectionString("Pusher");
        }

        /// <summary>
        /// 主数据库
        /// </summary>
        public static readonly string DbConnection;

        /// <summary>
        /// 读数据库
        /// </summary>
        public static readonly string ReadConnection;

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public static readonly string RedisConnection;

        /// <summary>
        /// Pusher 的连接字符串
        /// </summary>
        public static readonly string Pusher;
    }
}
