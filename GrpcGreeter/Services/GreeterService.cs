using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using IM.Agent;
using IM.Model.Sites;
using IM.Model.Users;
using Microsoft.Extensions.Logging;
using SP.StudioCore.API;
using SP.StudioCore.Data;
using SP.StudioCore.Enums;
using SP.StudioCore.Http;
using SP.StudioCore.Ioc;
using SP.StudioCore.Json;
using SP.StudioCore.Model;
using SP.StudioCore.Types;

namespace GrpcGreeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        #region ========  Ⱥ�����  ========

        public override Task<ResultReply> GetGroupList(GroupListRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = true,
                Message = "����ɹ�",
                Info = UserAgent.Instance().GetGroups(site.ID, request.Type.ToEnum<User.UserType>()).Select(t => new
                {
                    t.ID,
                    t.Name,
                    t.Face
                }).ToJson()
            });
        }

        public override Task<ResultReply> CreateGroup(GroupInfoRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = UserAgent.Instance().CreateGroup(new UserGroup()
                {
                    SiteID = site.ID,
                    Name = request.Name,
                    Face = request.Face,
                    Type = request.Type.ToEnum<User.UserType>()
                })
            });
        }

        public override Task<ResultReply> UpdateGroup(GroupInfoRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = UserAgent.Instance().UpdateGroupInfo(new UserGroup
                {
                    ID = request.GroupID,
                    SiteID = site.ID,
                    Name = request.Name,
                    Face = request.Face
                }),
                Message = IocCollection.GetService<MessageResult>().ToString()
            });
        }

        public override Task<ResultReply> UpdateGroupMember(GroupInfoRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = UserAgent.Instance().UpdateMember(request.GroupID, site.ID, request.Member.Split(',')),
                Message = IocCollection.GetService<MessageResult>().ToString()
            });
        }

        public override Task<ResultReply> GetGroupMember(GroupIDRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = true,
                Message = string.Empty,
                Info = UserAgent.Instance().GetMembers(site.ID, request.GroupID).Select(t => new
                {
                    t.UserName,
                    t.Name,
                    Face = t.Face.GetImage(),
                    t.Type
                }).ToJson()
            });
        }

        /// <summary>
        /// ��ȡȺ����Ϣ
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ResultReply> GetGroupInfo(GroupIDRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultReply
            {
                Success = true,
                Message = string.Empty,
                Info = UserAgent.Instance().GetGroupInfo(request.GroupID).ToJson()
            });
        }

        /// <summary>
        /// ɾ��Ⱥ��
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ResultReply> DeleteGroup(GroupIDRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = UserAgent.Instance().DeleteGroup(site.ID, request.GroupID)
            });
        }

        #endregion

        #region ========  �û�����  ========

        /// <summary>
        /// �½����߱����û�
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ResultReply> SaveUser(UserInfoRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = UserAgent.Instance().SaveUser(site.ID, request.UserName, request.NickName, request.Face, request.Type.ToEnum<User.UserType>()),
                Message = IocCollection.GetService<MessageResult>()
            });
        }

        /// <summary>
        /// ��ȡ��ʼ������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ResultReply> Init(UserInfoRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = true,
                Message = "��ȡ�ɹ�",
                Info = ConfigAgent.Instance().Init(site.ID, request.UserName, request.Type.ToEnum<User.UserType>())
            });
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ResultReply> SendMessage(MessageContentRequest request, ServerCallContext context)
        {
            Site site = context.GetHttpContext().GetItem<Site>();
            return Task.FromResult(new ResultReply
            {
                Success = MessageAgent.Instance().SendGroupMessage(site.ID, request.UserName, request.Type.ToEnum<User.UserType>(), request.ID, request.Content),
                Message = "������Ϣ"
            });
        }

        #endregion
    }
}
