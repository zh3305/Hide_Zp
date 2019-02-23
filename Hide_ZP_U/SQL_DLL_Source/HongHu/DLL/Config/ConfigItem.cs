using System;
using System.Collections.Generic;
using System.Text;

namespace HongHu.DLL.Config
{
    public class ConfigItem
    {

        /// <summary>
        /// 要连接到的数据库
        /// </summary>
       DataSourceType SourceType = DataSourceType.SQL;

        /// <summary>
        /// 可用数据库类型
        /// </summary>
        public enum DataSourceType
        {
            SQL = 1,
            Acces = 2
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnStr = "";
        //SQL连接事例 连接字符串通过 udl文件获得




        /// <summary>
        /// 
        /// </summary
        //string 备份文件路径;
        /// <summary>
        /// 
        /// </summary>
        //string 快捷键;

        #region 保留
        //public class Conn
        //{
        //    /// <summary>
        //    /// sql连接方式
        //    /// </summary>
        //     public enum SQLLinkMode
        //    {
        //        SQL = 1,
        //        SSPI = 3,
        //        WIN = 2
        //    }
        //    public SQLLinkMode LinkMode = SQLLinkMode.SQL;
        //}
        /// <summary>
        /// 数据库类型
        /// </summary>
        #endregion
    }
}
