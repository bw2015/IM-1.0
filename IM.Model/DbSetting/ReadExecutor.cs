using SP.StudioCore.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IM.Model.DbSetting
{
    /// <summary>
    /// 只读库操作对象
    /// </summary>
    public sealed class ReadExecutor : DbExecutor
    {
        public ReadExecutor() : 
            base(Setting.ReadConnection, DatabaseType.SqlServer)
        {
        }
    }
}
