using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;

namespace IM.Model.Users
{
    /// <summary>
    /// 组内成员
    /// </summary>
    [Table("usr_GroupMember")]
    public partial class GroupMember
    {

        #region  ========  構造函數  ========
        public GroupMember() { }

        public GroupMember(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "GroupID":
                        this.GroupID = (int)reader[i];
                        break;
                    case "UserID":
                        this.UserID = (int)reader[i];
                        break;
                    case "Flag":
                        this.Flag = (int)reader[i];
                        break;
                }
            }
        }


        public GroupMember(DataRow dr)
        {
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                switch (dr.Table.Columns[i].ColumnName)
                {
                    case "GroupID":
                        this.GroupID = (int)dr[i];
                        break;
                    case "UserID":
                        this.UserID = (int)dr[i];
                        break;
                    case "Flag":
                        this.Flag = (int)dr[i];
                        break;
                }
            }
        }

        #endregion

        #region  ========  数据库字段  ========

        /// <summary>
        /// 所属分组
        /// </summary>
        [Column("GroupID"),Key]
        public int GroupID { get; set; }


        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("UserID"),Key]
        public int UserID { get; set; }


        [Column("Flag")]
        public int Flag { get; set; }

        #endregion


        #region  ========  扩展方法  ========

        #endregion

    }

}
