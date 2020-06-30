using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace IM.Model.Users
{
    /// <summary>
    /// 注册的会员
    /// </summary>
    [Table("Users")]
    public partial class User
    {

        #region  ========  構造函數  ========
        public User() { }

        public User(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "UserID":
                        this.ID = (int)reader[i];
                        break;
                    case "SiteID":
                        this.SiteID = (int)reader[i];
                        break;
                    case "UserName":
                        this.UserName = (string)reader[i];
                        break;
                    case "NickName":
                        this.Name = (string)reader[i];
                        break;
                    case "Face":
                        this.Face = (string)reader[i];
                        break;
                    case "Type":
                        this.Type = (UserType)reader[i];
                        break;
                }
            }
        }


        public User(DataRow dr)
        {
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                switch (dr.Table.Columns[i].ColumnName)
                {
                    case "UserID":
                        this.ID = (int)dr[i];
                        break;
                    case "SiteID":
                        this.SiteID = (int)dr[i];
                        break;
                    case "UserName":
                        this.UserName = (string)dr[i];
                        break;
                    case "NickName":
                        this.Name = (string)dr[i];
                        break;
                    case "Face":
                        this.Face = (string)dr[i];
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
        /// 会员ID
        /// </summary>
        [Column("UserID"),DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int ID { get; set; }


        /// <summary>
        /// 所属商户
        /// </summary>
        [Column("SiteID")]
        public int SiteID { get; set; }


        /// <summary>
        /// 会员名
        /// </summary>
        [Column("UserName")]
        public string UserName { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        [Column("NickName")]
        public string Name { get; set; }


        /// <summary>
        /// 头像
        /// </summary>
        [Column("Face")]
        public string Face { get; set; }


        /// <summary>
        /// 用户类型 1:Member 2:Admin
        /// </summary>
        [Column("Type")]
        public UserType Type { get; set; }

        #endregion


#region  ========  扩展方法  ========

        public enum UserType : byte
        {
            [Description("会员")]
            Member = 1,
            [Description("管理员")]
            Admin = 2
        }
        #endregion

    }

}
