using IM.Model.Users;
using SP.StudioCore.API;
using SP.StudioCore.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IM.Agent
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class ConfigAgent : AgentBase<ConfigAgent>
    {
        /// <summary>
        /// 用户的初始化配置
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string Init(int siteId, string userName, User.UserType type)
        {
            int userId = UserAgent.Instance().GetUserID(siteId, userName, type);
            if (userId == 0) return new
            {
                code = 1,
                msg = "用户不存在"
            }.ToJson();

            User user = UserAgent.Instance().GetUserInfo(userId);
            IEnumerable<UserGroup> groups = UserAgent.Instance().GetGroups(userId);
            return new
            {
                code = 0,
                msg = string.Empty,
                data = new
                {
                    mine = new
                    {
                        username = user.Name,
                        id = user.ID.ToString(),
                        status = "online",
                        sign = "",
                        avatar = user.Face.GetImage()
                    },
                    friend = new object[] { },
                    group = groups.Select(t => new
                    {
                        groupname = t.Name,
                        id = t.SecretKey.ToString("N"),
                        avatar = t.Face.GetImage()
                    })
                }
            }.ToJson();
        }
    }
}
