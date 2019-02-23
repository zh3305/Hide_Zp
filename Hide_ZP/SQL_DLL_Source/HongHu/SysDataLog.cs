namespace HongHu
{
    using log4net;
    using System;
    using System.Reflection;
using System.Windows.Forms;

    public static class SysDataLog
    {
        
        public static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Winform 捕获全局异常 SamWang
        /// </summary>
        public static void CustomExceptionHandler()
        {
            //捕获未处理异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HongHu.SysDataLog.log.Error(e.Exception);
            //throw new Exception("线程未知异常", e.Exception);
            MessageBox.Show(e.Exception.Message, "线程异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            HongHu.SysDataLog.log.Error(ex);
            MessageBox.Show(ex.Message, "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}

