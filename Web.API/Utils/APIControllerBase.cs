using IM.Model.Sites;
using Microsoft.AspNetCore.Mvc;
using SP.StudioCore.Http;
using SP.StudioCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Utils
{
    /// <summary>
    /// 控制层基类
    /// </summary>
    public abstract class APIControllerBase : MvcControllerBase
    {
        protected Site SiteInfo
        {
            get
            {
                return this.context.GetItem<Site>();
            }
        }
    }
}
