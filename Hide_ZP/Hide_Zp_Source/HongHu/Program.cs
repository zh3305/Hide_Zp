
    using HongHu.UI;
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
namespace HongHu
{
    internal static class Program
    {

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HongHu.SysDataLog.SetDefutDemo();
            HongHu.DLL.CheckRun.checkeRun(new FormMoveRun());
        }

    }
}

