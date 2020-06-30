using Grpc.Core;
using GrpcGreeter;
using IMClient.Models;
using SP.StudioCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IMClient
{
    /// <summary>
    /// GRPC 客户端
    /// </summary>
    public sealed class GrpcClient
    {
        private readonly Channel channel;

        public readonly IMClientSetting Setting;

        public Greeter.GreeterClient Client => new Greeter.GreeterClient(channel);

        public GrpcClient(string clientSetting)
        {
            this.Setting = new IMClientSetting(clientSetting);

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            this.channel = new Channel(Setting.Host, Setting.Port, ChannelCredentials.Insecure);
        }

        public static implicit operator Greeter.GreeterClient(GrpcClient client)
        {
            return client.Client;
        }
    }
}
