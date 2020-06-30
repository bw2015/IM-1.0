using Grpc.Core;
using Grpc.Core.Interceptors;
using IM.Agent;
using IM.Model.DbSetting;
using IM.Model.Sites;
using Microsoft.AspNetCore.Http;
using SP.StudioCore.Http;
using SP.StudioCore.Model;
using SP.StudioCore.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcGreeter.Utils
{
    /// <summary>
    /// GRPC过滤器
    /// </summary>
    public class GrpcAuthorizationInterceptors : Interceptor
    {
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            HttpContext httpContext = context.GetHttpContext();

            if (!httpContext.GetAuth(out string name, out string password)) return default;

            int siteId = name.GetValue<int>();
            Guid secretKey = password.GetValue<Guid>();
            if (siteId == 0 || secretKey == Guid.Empty) return default;

            Site site = SiteAgent.Instance().GetSiteInfo(siteId);
            if (site == null || site.SecretKey != secretKey) return default;

            httpContext.SetItem(site);

            return continuation(request, context);
        }
    }
}
