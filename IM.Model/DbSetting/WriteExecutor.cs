using SP.StudioCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Model.DbSetting
{
    /// <summary>
    /// 主库操作对象（可写）
    /// </summary>
    public sealed class WriteExecutor : DbExecutor
    {
        public WriteExecutor() : base(Setting.DbConnection, DatabaseType.SqlServer) { }
    }
}
