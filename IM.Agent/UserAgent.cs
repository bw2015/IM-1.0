using IM.Model.Users;
using SP.StudioCore.Data;
using SP.StudioCore.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IM.Agent
{
    public class UserAgent : AgentBase<UserAgent>
    {
        #region ========  群组管理  ========

        /// <summary>
        /// 创建一个群组
        /// </summary>
        /// <returns></returns>
        public bool CreateGroup(UserGroup group)
        {
            group.SecretKey = Guid.NewGuid();
            return this.WriteDB.Insert(group);
        }

        /// <summary>
        /// 更新分组信息
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool UpdateGroupInfo(UserGroup group)
        {
            return this.WriteDB.Update(group, t => t.ID == group.ID && t.SiteID == group.SiteID, t => t.Name, t => t.Face) == 1;
        }

        /// <summary>
        /// 修改组内成员
        /// </summary>
        /// <param name="groupId">分组ID</param>
        /// <param name="siteId">商户ID</param>
        /// <param name="users">用户名</param>
        /// <returns></returns>
        public bool UpdateMember(int groupId, int siteId, string[] users)
        {
            UserGroup group = this.GetGroupInfo(groupId);
            if (group == null || group.SiteID != siteId) return this.FaildMessage("分组编号错误");
            IEnumerable<int> userlist = users.Select(t => this.GetUserID(siteId, t, group.Type)).Where(t => t != 0).Distinct();
            using (DbExecutor db = NewExecutor(IsolationLevel.ReadCommitted))
            {
                db.Delete<GroupMember>(t => t.GroupID == groupId);
                foreach (int userId in userlist)
                {
                    db.Insert(new GroupMember()
                    {
                        GroupID = groupId,
                        UserID = userId
                    });
                }
                db.Commit();
            }
            return true;
        }

        /// <summary>
        /// 获取群组内的用户列表
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IEnumerable<User> GetMembers(int siteId, int groupId)
        {
            return this.ReadDB.ReadList<User>($"EXISTS(SELECT 0 FROM usr_GroupMember WHERE usr_GroupMember.UserID = Users.UserID AND GroupID = { groupId } AND SiteID = { siteId })");
        }

        /// <summary>
        /// 获取群组信息
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public UserGroup GetGroupInfo(int siteId, int groupId)
        {
            return this.ReadDB.ReadInfo<UserGroup>(t => t.ID == groupId && t.SiteID == siteId);
        }


        /// <summary>
        /// 获取系统的群组
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserGroup> GetGroups(int siteId, User.UserType type)
        {
            return this.ReadDB.ReadList<UserGroup>(t => t.SiteID == siteId && t.Type == type);
        }

        /// <summary>
        /// 获取用户所在的群组
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="userName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<UserGroup> GetGroups(int userId)
        {
            return this.ReadDB.ReadList<UserGroup>($"EXISTS(SELECT 0 FROM { typeof(GroupMember).GetTableName() } t1 WHERE t1.GroupID = { typeof(UserGroup).GetTableName() }.GroupID AND UserID = {userId})");
        }

        /// <summary>
        /// 获取群组信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public UserGroup GetGroupInfo(int groupId)
        {
            return this.ReadDB.ReadInfo<UserGroup>(t => t.ID == groupId);
        }

        /// <summary>
        /// 获取分组ID
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public int GetGroupID(int siteId, string gid)
        {
            Guid secretKey = gid.GetValue<Guid>();
            if (secretKey == Guid.Empty) return 0;
            return this.ReadDB.ReadInfo<UserGroup, int>(t => t.ID, t => t.SiteID == siteId && t.SecretKey == secretKey);
        }

        /// <summary>
        /// 删除群组
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool DeleteGroup(int siteId, int groupId)
        {
            return this.WriteDB.Delete<UserGroup>(t => t.SiteID == siteId && t.ID == groupId) == 1;
        }


        #endregion

        #region ========  用户管理  ========

        /// <summary>
        /// 根据用户名得到用户ID
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserID(int siteId, string userName, User.UserType type)
        {
            return this.ReadDB.ReadInfo<User, int>(t => t.ID, t => t.SiteID == siteId && t.UserName == userName && t.Type == type);
        }

        /// <summary>
        /// 保存用户的信息
        /// </summary>
        /// <returns></returns>
        public bool SaveUser(int siteId, string userName, string nickName, string face, User.UserType type)
        {
            User user = new User
            {
                SiteID = siteId,
                UserName = userName,
                Name = string.IsNullOrEmpty(nickName) ? userName : nickName,
                Face = face,
                Type = type
            };
            using (DbExecutor db = NewExecutor(IsolationLevel.ReadCommitted))
            {
                if (db.Exists<User>(t => t.SiteID == siteId && t.UserName == userName && t.Type == type))
                {
                    db.Update(user, t => t.SiteID == siteId && t.UserName == userName && t.Type == type, t => t.Name, t => t.Face);
                }
                else
                {
                    db.Insert(user);
                }
                db.Commit();
            }
            return true;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserInfo(int userId)
        {
            return this.ReadDB.ReadInfo<User>(t => t.ID == userId);
        }

        #endregion

    }
}
