using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;

namespace IM.Model.Sites
{
    [Table("Site")]
    public partial class Site
    {

        #region  ========  構造函數  ========
        public Site() { }

        public Site(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "SiteID":
                        this.ID = (int)reader[i];
                        break;
                    case "SiteName":
                        this.Name = (string)reader[i];
                        break;
                    case "SecretKey":
                        this.SecretKey = (Guid)reader[i];
                        break;
                }
            }
        }


        public Site(DataRow dr)
        {
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                switch (dr.Table.Columns[i].ColumnName)
                {
                    case "SiteID":
                        this.ID = (int)dr[i];
                        break;
                    case "SiteName":
                        this.Name = (string)dr[i];
                        break;
                    case "SecretKey":
                        this.SecretKey = (Guid)dr[i];
                        break;
                }
            }
        }

        #endregion

        #region  ========  数据库字段  ========

        /// <summary>
        /// 商户ID
        /// </summary>
        [Column("SiteID"), Key]
        public int ID { get; set; }


        /// <summary>
        /// 商户名
        /// </summary>
        [Column("SiteName")]
        public string Name { get; set; }


        /// <summary>
        /// 密钥
        /// </summary>
        [Column("SecretKey")]
        public Guid SecretKey { get; set; }

        #endregion


        #region  ========  扩展方法  ========


        #endregion

    }

}
