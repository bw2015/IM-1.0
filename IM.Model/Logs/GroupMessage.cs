using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;

namespace IM.Model.Logs
{
    /// <summary>
    /// 群聊聊天记录
    /// </summary>
    [Table("log_GroupMessage")]
    public partial class GroupMessage
    {

        #region  ========  構造函數  ========
        public GroupMessage() { }

        public GroupMessage(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "LogID":
                        this.ID = (int)reader[i];
                        break;
                    case "SiteID":
                        this.SiteID = (int)reader[i];
                        break;
                    case "GroupID":
                        this.GroupID = (int)reader[i];
                        break;
                    case "UserID":
                        this.UserID = (int)reader[i];
                        break;
                    case "CreateAt":
                        this.CreateAt = (DateTime)reader[i];
                        break;
                    case "Content":
                        this.Content = (string)reader[i];
                        break;
                }
            }
        }


        public GroupMessage(DataRow dr)
        {
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                switch (dr.Table.Columns[i].ColumnName)
                {
                    case "LogID":
                        this.ID = (int)dr[i];
                        break;
                    case "SiteID":
                        this.SiteID = (int)dr[i];
                        break;
                    case "GroupID":
                        this.GroupID = (int)dr[i];
                        break;
                    case "UserID":
                        this.UserID = (int)dr[i];
                        break;
                    case "CreateAt":
                        this.CreateAt = (DateTime)dr[i];
                        break;
                    case "Content":
                        this.Content = (string)dr[i];
                        break;
                }
            }
        }

        #endregion

        #region  ========  数据库字段  ========

        [Column("LogID"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ID { get; set; }


        [Column("SiteID")]
        public int SiteID { get; set; }


        /// <summary>
        /// 所属群聊
        /// </summary>
        [Column("GroupID")]
        public int GroupID { get; set; }


        /// <summary>
        /// 信息的发送者
        /// </summary>
        [Column("UserID")]
        public int UserID { get; set; }


        /// <summary>
        /// 信息提交时间
        /// </summary>
        [Column("CreateAt")]
        public DateTime CreateAt { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        [Column("Content")]
        public string Content { get; set; }

        #endregion


        #region  ========  扩展方法  ========


        #endregion

    }

}
