using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;  //这个肯定要的 

namespace HongHu
{
    public class win32API
    {

        public struct CWPSTRUCT
        {
            public int lParam;
            public int wParam;
            public int Msg;
            public int hWnd;
        }
        [DllImport("kernel32")]
        public static extern void RtlMoveMemory(ref CWPSTRUCT dst,
         IntPtr Source, Int32 len);
        public readonly int MOUSEEVENTF_LEFTDOWN = 0x2;
        public readonly int MOUSEEVENTF_LEFTUP = 0x4;
        public const int OPEN_PROCESS_ALL = 2035711;
        public const int PAGE_READWRITE = 4;
        public const int PROCESS_CREATE_THREAD = 2;
        public const int PROCESS_HEAP_ENTRY_BUSY = 4;
        public const int PROCESS_VM_OPERATION = 8;
        public const int PROCESS_VM_READ = 256;
        public const int PROCESS_VM_WRITE = 32;
        public const int PAGE_EXECUTE_READWRITE = 0x4;
        public const int MEM_COMMIT = 4096;
        public const uint BM_CLICK = 0xF5;
        public const int MEM_RELEASE = 0x8000;
        public const int MEM_DECOMMIT = 0x4000;
        public const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_CHAR = 0x0102;  
      public    const int WM_GETTEXT = 0x000D;
      public  const int WM_SETTEXT = 0x000C;
      public const int WM_CLICK = 0x00F5;
      public const uint WM_LBUTTONDOWN = 0x201;
      [DllImport("user32.dll")]
      public static extern int GetMenuState(int hMenu, int uId, int uFlags);
      //安装钩子 
      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern int SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid); 
      public const uint WM_LBUTTONUP = 0x202;
        public const uint WM_RBUTTONDOWN = 0x204;
        public const uint WM_RBUTTONUP = 0x205;
        [DllImport("user32.dll")]
        public static extern int GetMenuItemID(int hMenu, int nPos);
        [DllImport("user32.dll")]
      public  static extern int CallWindowProc(int lpPrevWndFunc, int hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
       public static extern int DefWindowProc(int hWnd, int uMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
      public  static extern IntPtr GetProp(IntPtr hWnd, string lpString);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        public const int WM_CREATE = 1;
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetPropA(IntPtr hWnd, string lpString, int hData);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, 菜单窗口_消息处理 dwNewLong);
        [DllImport("user32.dll")]
       public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }
        [DllImport("user32.dll", SetLastError = true)]
     public   static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
     public   static extern bool UnhookWindowsHookEx(IntPtr hhk);

        public  delegate int 菜单窗口_消息处理(int hWnd, int Msg, int wParam, int lParam);
      public  delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
      [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall )]
     public  static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadID);
        [DllImport("user32.dll")]
      public static extern int GetMenuString(IntPtr hMenu, int uIDItem, [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder lpString, int nMaxCount, uint uFlag);
      [DllImport("user32")]
      public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo); 
      [DllImport("User32.dll", EntryPoint = "SendMessage")]
      public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
      [return: MarshalAs(UnmanagedType.Bool)]
      [DllImport("user32.dll", SetLastError = true)]
      public static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
      [System.Runtime.InteropServices.DllImport("user32.dll")]
      public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
      [System.Runtime.InteropServices.DllImport("user32.dll")]
      public static extern int GetMenuItemCount(IntPtr hMenu);
      [System.Runtime.InteropServices.DllImport("user32.dll")]
      public static extern bool DrawMenuBar(IntPtr hWnd);
      [System.Runtime.InteropServices.DllImport("user32.dll")]
      public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);
      public const Int32 MF_BYPOSITION = 0x400;
      public const Int32 MF_REMOVE = 0x1000;
      [DllImport("user32.dll", SetLastError = true)]
     public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
      public void PostMessageSafe(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam)
      {
          bool returnValue = PostMessage(hWnd, msg, wParam, lParam);
          if (!returnValue)
          {
              // An error occured
              throw new Win32Exception(Marshal.GetLastWin32Error());
          }
      }
      [DllImport("user32.dll")]
     public static extern bool GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);
      [StructLayout(LayoutKind.Sequential)]
      public struct MENUITEMINFO
      {
          public uint cbSize;
          public uint fMask;
          public uint fType;
          public uint fState;
          public int wID;
          public int hSubMenu;
          public int hbmpChecked;
          public int hbmpUnchecked;
          public int dwItemData;
          public string dwTypeData;
          public uint cch;
          public int hbmpItem;
      }
      // Values for the fMask parameter
      //From winuser.h
      public const UInt32 MIM_MAXHEIGHT = 0x00000001;
      public const UInt32 MIM_BACKGROUND = 0x00000002;
      public const UInt32 MIM_HELPID = 0x00000004;
      public const UInt32 MIM_MENUDATA = 0x00000008;
      public const UInt32 MIM_STYLE = 0x00000010;
     public const UInt32 MIM_APPLYTOSUBMENUS = 0x80000000;
      [DllImport("user32.dll")]
      public static extern IntPtr GetMenu(IntPtr hWnd);
      [DllImport("user32.dll")]
      public static extern UInt32 SendInput(UInt32 nInputs, Input[] pInputs, int cbSize);
      [StructLayout(LayoutKind.Explicit)]

      public struct Input
      {
          [FieldOffset(0)]
          public Int32 type;
          [FieldOffset(4)]
          public MouseInput mi;
          [FieldOffset(4)]
          public tagKEYBDINPUT ki;
          [FieldOffset(4)]
          public tagHARDWAREINPUT hi;
      }
      [StructLayout(LayoutKind.Sequential)]
      public struct MouseInput
      {
          public Int32 dx;
          public Int32 dy;
          public Int32 Mousedata;
          public Int32 dwFlag;
          public Int32 time;
          public IntPtr dwExtraInfo;
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct tagKEYBDINPUT
      {
          Int16 wVk;
          Int16 wScan;
          Int32 dwFlags;
          Int32 time;
          IntPtr dwExtraInfo;
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct tagHARDWAREINPUT
      {
          Int32 uMsg;
          Int16 wParamL;
          Int16 wParamH;
      }

      [DllImport("user32.dll", EntryPoint = "keybd_event")]
      public static extern void keybd_event(
      byte bVk,
      byte bScan,
      int dwFlags,
      int dwExtraInfo
      );
      public const int MouseEvent_Absolute = 0x8000;
      public const int MouserEvent_Hwheel = 0x01000;
      public const int MouseEvent_Move = 0x0001;
      public const int MouseEvent_Move_noCoalesce = 0x2000;
      public const int MouseEvent_LeftDown = 0x0002;
      public const int MouseEvent_LeftUp = 0x0004;
      public const int MouseEvent_MiddleDown = 0x0020;
      public const int MouseEvent_MiddleUp = 0x0040;
      public const int MouseEvent_RightDown = 0x0008;
      public const int MouseEvent_RightUp = 0x0010;
      public const int MouseEvent_Wheel = 0x0800;
      public const int MousseEvent_XUp = 0x0100;
      public const int MousseEvent_XDown = 0x0080;
      public const int KEYEVENTF_EXTENDEDKEY = 0x1;
      public const int KEYEVENTF_KEYUP = 0x2;
      public const int KEYEVENTF_KEYDOWN = 0x00;
      public const int VK_RETURN = 0x0D;
      public const int KEY_A = 0x41;
      public const int KEY_S = 0x53;
      public const int KEY_D = 0x44;
      public const int KEY_W = 0x57;

      public const int KEY_J = 0x4A;
      public const int KEY_K = 0x4B;
      public const int KEY_L = 0x4C;

      public const int KEY_U = 0x55;
      public const int KEY_I = 0x49;
      public const int KEY_O = 0x4F;   
      [DllImport("user32.dll")]
      public static extern IntPtr SetFocus(IntPtr hWnd);
      [DllImport("user32.dll")]
      public static extern IntPtr SetCapture(IntPtr hWnd);
      [DllImport("user32.dll")]
      public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);
      [DllImport("user32.dll", SetLastError = true)]
      public static extern IntPtr SetActiveWindow(IntPtr hWnd);
       // For Windows Mobile, replace user32.dll with coredll.dll 
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
     public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///  取得类名称
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpClassName"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
       public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
        /// <summary>
        /// 取父窗口句丙
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        /// <summary>
        /// 设置窗口标题
        /// </summary>
        /// <param name="lpClassName">窗口句柄</param>
        /// <param name="lpWindowName">窗口标题</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SetWindowTextA")]
        public extern static int SetWindowTextA(
            int lpClassName,
            string lpWindowName
            );
        /// <summary>
        /// 查找窗体
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(
            string lpClassName,
            string lpWindowName
            );
        /// <summary>
        /// 该函数获得一个窗口的句柄，该窗口的类名和窗口名与给定的字符串相匹配。这个函数查找子窗口，从排在给定的子窗口后面的下一个子窗口开始。在查找时不区分大小写。
        /// </summary>
        /// <param name="hwndParent">要查找子窗口的父窗口句柄。如果hwnjParent为NULL，则函数以桌面窗口为父窗口，查找桌面窗口的所有子窗口。</param>
        /// <param name="hwndChildAfter">子窗口句柄。查找从在Z序中的下一个子窗口开始。子窗口必须为hwndPareRt窗口的直接子窗口而非后代窗口。如果HwndChildAfter为NULL，查找从hwndParent的第一个子窗口开始。如果hwndParent 和 hwndChildAfter同时为NULL，则函数查找所有的顶层窗口及消息窗口。</param>
        /// <param name="lpszClass">指向一个指定了类名的空结束字符串，或一个标识类名字符串的成员的指针。如果该参数为一个成员，则它必须为前次调用theGlobaIAddAtom函数产生的全局成员。该成员为16位，必须位于lpClassName的低16位，高位必须为0。</param>
        /// <param name="lpszWindow">指向一个指定了窗口名（窗口标题）的空结束字符串。如果该参数为 NULL，则为所有窗口全匹配。返回值：如果函数成功，返回值为具有指定类名和窗口名的窗口句柄。如果函数失败，返回值为NULL。</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        /// <summary>
        /// 得到目标进程句柄的函数
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(
            int hwnd,
            ref int lpdwProcessId
            );
        /// <summary>
        /// 得到目标进程句柄的函数
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        public extern static int GetWindowThreadProcessId(
            IntPtr hwnd,
            ref int lpdwProcessId
            );
        /// <summary>
        /// 打开进程
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public extern static int OpenProcess(
            int dwDesiredAccess,
            int bInheritHandle,
            int dwProcessId
            );
        /// <summary>
        /// 打开进程
        /// </summary>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="bInheritHandle"></param>
        /// <param name="dwProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public extern static IntPtr OpenProcess(
            uint dwDesiredAccess,
            int bInheritHandle,
            uint dwProcessId
            );

        /// <summary>
        /// 关闭句柄的函数
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
        public static extern int CloseHandle(
            int hObject
            );
        /// <summary>
        /// 读内存
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [In, Out] byte[] buffer,
            int size,
            out IntPtr lpNumberOfBytesWritten
            );
        /// <summary>
        /// 读内存
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="lpBaseAddress"></param>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="lpNumberOfBytesWritten"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(
            int hProcess,
            int lpBaseAddress,
            ref int buffer,
            //byte[] buffer,
            int size,
            int lpNumberOfBytesWritten
            );
        [DllImport("Kernel32.dll ")]
        public static extern Int32 ReadProcessMemory(
            int hProcess,
            int lpBaseAddress,
            byte[] buffer,
            int size,
            int lpNumberOfBytesWritten
            );
        //写内存
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [In, Out] byte[] buffer,
            int size,
            out IntPtr lpNumberOfBytesWritten
            );
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(
            int hProcess,
            int lpBaseAddress,
            byte[] buffer,
            int size,
            int lpNumberOfBytesWritten
            );
        //创建线程
        [DllImport("kernel32", EntryPoint = "CreateRemoteThread")]
        public static extern int CreateRemoteThread(
            int hProcess,
            int lpThreadAttributes,
            int dwStackSize,
            int lpStartAddress,
            int lpParameter,
            int dwCreationFlags,
            ref int lpThreadId
            );
        //开辟指定进程的内存空间
        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualAllocEx(
         System.IntPtr hProcess,
         System.Int32 lpAddress,
         System.Int32 dwSize,
         System.Int16 flAllocationType,
         System.Int16 flProtect
         );
        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualAllocEx(
        int hProcess,
        int lpAddress,
        int dwSize,
        int flAllocationType,
        int flProtect
        );
        //释放内存空间
        [DllImport("Kernel32.dll")]
        public static extern System.Int32 VirtualFreeEx(
        int hProcess,
        int lpAddress,
        int dwSize,
        int flAllocationType
        );

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        /// <summary>
        /// 返回一个子窗口的列表
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>窗口子名单</returns>
        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        /// <summary>
        /// 回调方法来枚举窗口时使用。
        /// </summary>
        /// <param name="handle">下一个窗口的句柄</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        public static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        /// <summary>
        /// 委派的EnumChildWindows函数方法
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);
    }
}