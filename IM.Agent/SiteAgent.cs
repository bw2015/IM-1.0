using IM.Model.Sites;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Agent
{
    public sealed class SiteAgent : AgentBase<SiteAgent>
    {

        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public Site GetSiteInfo(int siteId)
        {
            return this.ReadDB.ReadInfo<Site>(t => t.ID == siteId);
        }
    }
}
