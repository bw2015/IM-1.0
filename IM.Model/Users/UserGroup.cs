using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;
using static IM.Model.Users.User;

namespace IM.Model.Users
{
    /// <summary>
    /// 分组
    /// </summary>
    [Table("usr_Group")]
    public partial class UserGroup
    {

        #region  ========  構造函數  ========
        public UserGroup() { }

        public UserGroup(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "GroupID":
                        this.ID = (int)reader[i];
                        break;
                    case "SiteID":
                        this.SiteID = (int)reader[i];
                        break;
                    case "SecretKey":
                        this.SecretKey = (Guid)reader[i];
                        break;
                    case "GroupName":
                        this.Name = (string)reader[i];
                        break;
                    case "Face":
                        this.Face = (string)reader[i];
                        break;
                    case "Users":
                        this.Users = (string)reader[i];
                        break;
                    case "Type":
                        this.Type = (UserType)reader[i];
                        break;
                }
            }
        }


        public UserGroup(DataRow dr)
        {
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                switch (dr.Table.Columns[i].ColumnName)
                {
                    case "GroupID":
                        this.ID = (int)dr[i];
                        break;
                    case "SiteID":
                        this.SiteID = (int)dr[i];
                        break;
                    case "SecretKey":
                        this.SecretKey = (Guid)dr[i];
                        break;
                    case "GroupName":
                        this.Name = (string)dr[i];
                        break;
                    case "Face":
                        this.Face = (string)dr[i];
                        break;
                    case "Users":
                        this.Users = (string)dr[i];
                        break;
                    case "Type":
                        this.Type = (UserType)dr[i];
                        break;
                }
            }
        }

        #endregion

        #region  ========  数据库字段  ========

        /// <summary>
        /// 分组编号
        /// </summary>
        [Column("GroupID"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ID { get; set; }


        /// <summary>
        /// 所属站点
        /// </summary>
        [Column("SiteID")]
        public int SiteID { get; set; }


        /// <summary>
        /// 分组的编码
        /// </summary>
        [Column("SecretKey")]
        public Guid SecretKey { get; set; }


        /// <summary>
        /// 分组名称
        /// </summary>
        [Column("GroupName")]
        public string Name { get; set; }


        [Column("Face")]
        public string Face { get; set; }


        /// <summary>
        /// 组内成员
        /// </summary>
        [Column("Users")]
        public string Users { get; set; }


        /// <summary>
        /// 群组类型
        /// </summary>
        [Column("Type")]
        public UserType Type { get; set; }

        #endregion


        #region  ========  扩展方法  ========


        #endregion

    }

}
