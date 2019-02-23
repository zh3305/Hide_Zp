namespace HongHu
{
    using System;
    using System.Collections.Generic;
    
/// <summary>
/// 程序配置
/// </summary>
    public class HideConfigItem
    {
        /// <summary>
        /// 是否显示进度条
        /// </summary>
        public bool Jdt = true;
        /// <summary>
      /// 移动路径
        /// </summary>
        public string Movetarget = "";
        /// <summary>
        /// 隐藏方式
        /// </summary>
        public Runfs Rf = Runfs.Login;
        /// <summary>
        /// 快捷键
        /// </summary>
        public List<kuaijijian> UHotkeys = new List<kuaijijian>();
        /// <summary>
        /// 快捷键显示帐套
        /// </summary>
        public kuaijijian Xszt;
        /// <summary>
        /// 是否显示小图标
        /// </summary>
        public bool Xtb = true;
        /// <summary>
        /// 快捷键隐藏帐套
        /// </summary>
        public kuaijijian Yczt;
        /// <summary>
        /// 
        /// </summary>
        public kuaijijian Zjm;
        /// <summary>
        /// 是否显示自运行
        /// </summary>
        public bool ZRun = false;
    }
}

