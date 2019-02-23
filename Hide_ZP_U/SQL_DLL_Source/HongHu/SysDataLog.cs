namespace HongHu
{
    using log4net;
    using System;
    using System.Reflection;

    public static class SysDataLog
    {
        public static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}

