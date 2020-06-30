using IM.Model.Logs;
using IM.Model.Users;
using Org.BouncyCastle.Security;
using PusherServer;
using SP.StudioCore.API;
using SP.StudioCore.Gateway.Push;
using SP.StudioCore.Ioc;
using SP.StudioCore.Json;
using SP.StudioCore.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Agent
{
    /// <summary>
    /// 信息管理
    /// </summary>
    public sealed class MessageAgent : AgentBase<MessageAgent>
    {
        private IPush Pusher => IocCollection.GetService<IPush>();

        /// <summary>
        /// 发送群聊消息
        /// </summary>
        /// <returns></returns>
        public bool SendGroupMessage(int siteId, string userName, User.UserType type, string gid, string content)
        {
            int userId = UserAgent.Instance().GetUserID(siteId, userName, type);
            if (userId == 0) return this.FaildMessage("信息发送者不存在");

            //{
            //  username: "纸飞机" //消息来源用户名
            //  ,avatar: "http://tp1.sinaimg.cn/1571889140/180/40030060651/1" //消息来源用户头像
            //  ,id: "100000" //消息的来源ID（如果是私聊，则是用户id，如果是群聊，则是群组id）
            //  ,type: "friend" //聊天窗口来源类型，从发送消息传递的to里面获取
            //  ,content: "嗨，你好！本消息系离线消息。" //消息内容
            //  ,cid: 0 //消息id，可不传。除非你要对消息进行一些操作（如撤回）
            //  ,mine: false //是否我发送的消息，如果为true，则会显示在右方
            //  ,fromid: "100000" //消息的发送者id（比如群组中的某个消息发送者），可用于自动解决浏览器多窗口时的一些问题
            //  ,timestamp: 1467475443306 //服务端时间戳毫秒数。注意：如果你返回的是标准的 unix 时间戳，记得要 *1000
            //}

            User user = UserAgent.Instance().GetUserInfo(userId);

            string message = new
            {
                username = user.Name,
                avatar = user.Face.GetImage(),
                id = gid,
                type = "group",
                content,
                fromid = user.ID.ToString(),
                timestamp = WebAgent.GetTimestamps()
            }.ToJson();
            try
            {
                return Pusher.Send(message, gid);
            }
            finally
            {
                int groupId = UserAgent.Instance().GetGroupID(siteId, gid);
                this.WriteDB.Insert(new GroupMessage
                {
                    GroupID = groupId,
                    CreateAt = DateTime.Now,
                    Content = content,
                    SiteID = siteId,
                    UserID = userId
                });
            }
        }
    }
}
