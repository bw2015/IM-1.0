using Grpc.Core;
using GrpcGreeter;
using IMClient.Models;
using SP.StudioCore.Http;
using SP.StudioCore.Ioc;
using SP.StudioCore.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMClient
{
    public static class IMClientHelper
    {
        private static Greeter.GreeterClient Client => IocCollection.GetService<GrpcClient>();

        private static CallOptions CallOptions => new CallOptions(new Metadata
        {
             { "Authorization", IocCollection.GetService<GrpcClient>().Setting.SecretKey.ToBasicAuth() }
        });

        private static Result ToResult(this ResultReply reply)
        {
            return new Result(reply.Success, reply.Message, reply.Info);
        }

        #region ========  用户接口  ========

        /// <summary>
        /// 保存用户（新建或者更新）
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="nickName"></param>
        /// <param name="face"></param>
        /// <returns></returns>
        public static Result SaveUser(string userName, string nickName, string face, MemberType type)
        {
            ResultReply result = Client.SaveUser(new UserInfoRequest
            {
                UserName = userName,
                NickName = nickName,
                Face = face,
                Type = type.ToString()
            }, CallOptions);
            return new Result(result.Success, result.Message);
        }

        /// <summary>
        /// layim的初始化配置内容
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Init(string userName, MemberType type)
        {
            return Client.Init(new UserInfoRequest
            {
                UserName = userName,
                Type = type.ToString()
            }, CallOptions).Info;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="userName"></param>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Result SendMessage(string targetId, string userName, MemberType type, string content)
        {
            return Client.SendMessage(new MessageContentRequest
            {
                ID = targetId,
                Content = content,
                Type = type.ToString(),
                UserName = userName
            }, CallOptions).ToResult();
        }

        #endregion

        #region ========  群组接口  ========

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="name"></param>
        /// <param name="face"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Result CreateGroup(string name, string face, MemberType type)
        {
            ResultReply result = Client.CreateGroup(new GroupInfoRequest
            {
                Name = name,
                Face = face,
                Type = type.ToString()
            }, CallOptions);
            return new Result(result.Success, result.Message);
        }

        /// <summary>
        /// 修改群组信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Result UpdateGroup(int groupId, string name, string face)
        {
            ResultReply result = Client.UpdateGroup(new GroupInfoRequest
            {
                GroupID = groupId,
                Name = name,
                Face = face
            }, CallOptions);
            return new Result(result.Success, result.Message);
        }

        /// <summary>
        /// 修改群组成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public static Result UpdateGroupMember(int groupId, string[] users)
        {
            return Client.UpdateGroupMember(new GroupInfoRequest
            {
                GroupID = groupId,
                Member = string.Join(",", users)
            }, CallOptions).ToResult();
        }

        /// <summary>
        /// 获取群组列表
        /// </summary>
        /// <returns></returns>
        public static Result GetGroupList(MemberType type)
        {
            return Client.GetGroupList(new GroupListRequest
            {
                Type = type.ToString()
            }, CallOptions).ToResult();
        }

        /// <summary>
        /// 删除群组
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Result DeleteGroup(int groupId)
        {
            return Client.DeleteGroup(new GroupIDRequest
            {
                GroupID = groupId
            }).ToResult();
        }

        /// <summary>
        /// 获取群组信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Result GetGroupInfo(int groupId)
        {
            return Client.GetGroupInfo(new GroupIDRequest
            {
                GroupID = groupId
            }).ToResult();
        }



        #endregion
    }
}
