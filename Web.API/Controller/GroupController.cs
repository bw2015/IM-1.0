using IM.Agent;
using IM.Model.Users;
using Microsoft.AspNetCore.Mvc;
using SP.StudioCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Utils;

namespace Web.API.Controller
{
    public class GroupController : APIControllerBase
    {
        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <param name="face">头像</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public ContentResult Create([FromForm]string name, [FromForm]string face, [FromForm]User.UserType type)
        {
            return this.GetResultContent(UserAgent.Instance().CreateGroup(new UserGroup()
            {
                Name = name,
                SiteID = this.SiteInfo.ID,
                Type = type
            }));
        }

        /// <summary>
        /// 修改群组信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="name"></param>
        /// <param name="face"></param>
        /// <param name="users">群组内成员</param>
        /// <returns></returns>
        public ContentResult Update([FromForm]int groupId, [FromForm]string name, [FromForm]string face)
        {
            return this.GetResultContent(UserAgent.Instance().UpdateGroupInfo(new UserGroup()
            {
                ID = groupId,
                SiteID = this.SiteInfo.ID,
                Name = name,
                Face = face
            }));
        }

        /// <summary>
        /// 修改组内成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public ContentResult UpdateMember([FromForm]int groupId, [FromForm]string users)
        {
            return this.GetResultContent(UserAgent.Instance().UpdateMember(groupId, this.SiteInfo.ID, users.Split(',')));
        }
    }
}
