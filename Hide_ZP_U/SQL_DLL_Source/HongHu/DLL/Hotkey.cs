using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HongHu.DLL
{
    public delegate void HotkeyEventHandler(int HotKeyID);

    public class Hotkey : IMessageFilter       //继承这个接口，才能用AddMessageFilter接收消息
    {

        System.Collections.Hashtable keyIDs = new System.Collections.Hashtable();

        IntPtr hWnd;

        public event HotkeyEventHandler OnHotkey;  //方便对快捷键进行处理

        public const int MOD_ALT = 0x1;

        public const int MOD_CONTROL = 0x2;

        public const int MOD_SHIFT = 0x4;

        public const int MOD_WIN = 0x8;

        public const int WM_HOTKEY = 0x312;    //按下快捷键消息的ID


        /// <summary>
        /// 注册快捷键
        /// </summary>
        /// <param name="hWnd">用于接收消息的句柄，一般是this</param>
        /// <param name="id">被注册的快捷键的ID，可以设置个ID,但不能重复，推荐用System.Guid.NewGuid()方法获得一个不易被重复的ID.</param>
        /// <param name="fsModifiers">
        /// 快捷键的修饰键（如：Ctrl,Alt或Ctrl+Alt等等），可以使用下面的一些值：
        /// public const int MOD_ALT = 0x1;        //Alt键，值为1
        /// public const int MOD_CONTROL = 0x2;    //Ctrl键值为2，如果要用Ctrl+Alt，直接用3就可以了
        /// public const int MOD_SHIFT = 0x4;      //Shift键值为4
        /// </param>
        /// <param name="vk">快捷键 如：Keys.A </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern UInt32 RegisterHotKey(IntPtr hWnd, UInt32 id, UInt32 fsModifiers, Keys vk);

        /// <summary>
        /// 反注册快捷键
        /// </summary>
        /// <param name="hWnd">注册快捷键的窗体句柄。</param>
        /// <param name="id">被注册的快捷键的id。（把之前注册快捷键的id放进去就可以了）</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern UInt32 UnregisterHotKey(IntPtr hWnd, UInt32 id);



        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalAddAtom(String lpString);

        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom);



        public Hotkey(IntPtr hWnd)
        {

            this.hWnd = hWnd;

            Application.AddMessageFilter(this);    //这样this才会收到消息

        }

        public int RegisterHotkey(Keys Key, UInt32 KeyModifiers)
        {

            UInt32 hotkeyid = GlobalAddAtom(System.Guid.NewGuid().ToString());

            RegisterHotKey((IntPtr)hWnd, hotkeyid, KeyModifiers, Key);

            keyIDs.Add(hotkeyid, hotkeyid);

            return (int)hotkeyid;

        }

        public void UnregisterHotkeys()
        {

            Application.RemoveMessageFilter(this);

            foreach (UInt32 key in keyIDs.Values)
            {

                UnregisterHotKey(hWnd, key);

                GlobalDeleteAtom(key);

            }

        }

        /// <summary>

        /// 消息过滤器

        /// </summary>

        /// <param name="m">收到的消息</param>

        /// <returns>如果筛选消息并禁止消息被调度，则为true；如果允许消息继续到达下一个筛选器或控件，则为false。</returns>

        public bool PreFilterMessage(ref Message m)
        {

            if (m.Msg == WM_HOTKEY)
            {

                if (OnHotkey != null)
                {

                    foreach (UInt32 key in keyIDs.Values)
                    {

                        if ((UInt32)m.WParam == key)
                        {

                            OnHotkey((int)m.WParam);

                            return true;

                        }

                    }

                }

            }

            return false;

        }

    }


}
