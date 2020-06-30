using IM.Model.DbSetting;
using SP.StudioCore.Data;
using SP.StudioCore.Data.Repository;
using SP.StudioCore.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Agent
{
    public abstract class AgentBase<T> : DbAgent<T> where T : class, new()
    {
        protected AgentBase() : base(Setting.DbConnection, DatabaseType.SqlServer)
        {
        }
    }
}
