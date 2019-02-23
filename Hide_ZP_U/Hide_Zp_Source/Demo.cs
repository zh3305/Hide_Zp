using System;
namespace HongHu
{
    #region enum Runfs 软件运行模式
    [EnumDescription("软件运行模式")]
    public enum Runfs
    {
        /// <summary>
        /// 未隐藏
        /// </summary>
        [EnumDescription("未隐藏")]
        None = 0,
        /// <summary>
        /// 登录隐藏
        /// </summary>
        [EnumDescription("登录隐藏")]
        Login = 1,
        /// <summary>
        /// 系统隐藏
        /// </summary>
        [EnumDescription("系统隐藏")]
        System = 2,
        /// <summary>
        /// 分离隐藏
        /// </summary>
        [EnumDescription("分离隐藏")]
        Detach = 3,
        /// <summary>
        /// 移动隐藏
        /// </summary>
        [EnumDescription("移动隐藏")]
        Move = 4,        
        /// <summary>
        /// 移动隐藏
        /// </summary>
        [EnumDescription("移动隐藏")]
        RunOnU=5
    }

    #endregion Runfs 软件运行模式

    /// <summary>
    /// 多线程参数
    /// </summary>
    public class HongHuWorkArgs
    {
        #region 构造函数
        public HongHuWorkArgs() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ewk">WorkDoEnumWork</param>
        /// <param name="e">相关参数</param>
        public HongHuWorkArgs(EnumWork Ewk, object e)
        {
            WorkDoEnumWork = Ewk;
            Args = e;
        }
        #endregion  构造函数

        #region 操作枚举

        /// <summary>
        /// 操作枚举
        /// </summary>
        public enum EnumWork
        {
            /// <summary>
            /// 刷新帐套信息
            /// </summary>
            SXZT,
            /// <summary>
            /// 显示隐藏操作
            /// </summary>
            XSYC
        }

        #endregion 操作枚举

        #region 属性
        /// <summary>
        /// 相关参数
        /// </summary>
        public object Args;
        /// <summary>
        /// WorkDoEnumWork
        /// </summary>
        public EnumWork WorkDoEnumWork;
        #endregion 属性
    }
}

