using SP.StudioCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMClient.Models
{
    /// <summary>
    /// GRPC客户端的参数配置
    /// </summary>
    public class IMClientSetting : ISetting
    {
        public IMClientSetting(string queryString) : base(queryString)
        {
        }

        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }
    }
}
