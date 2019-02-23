
    using HongHu.DLL;
    using System;
    using System.Windows.Forms;
namespace HongHu
{
	/// <summary>
	/// 快捷键类
	/// </summary>
    public class Kuaijijian
    {
        public string Hname;
        /// <summary>
        /// 热键
        /// </summary>
//        [assembly: CLSCompliant(true)]
        private Hotkey hotkey;
        /// <summary>
        /// 热键
        /// </summary>
        public Hotkey Hotkey
        {
            get { return hotkey; }
            set { hotkey = value; }
        }
        public Keys Key = Keys.Escape;
        public int keyModifiers;
        public bool kjj = false;
        public int regid = 0;

        public Kuaijijian(string _Hname, Keys _key, int _KeyModifiers)
        {
            this.Hname = _Hname;
            this.Key = _key;
            this.keyModifiers = _KeyModifiers;
        }

        public void reghok(IntPtr ipt)
        {
            this.hotkey = new Hotkey(ipt);
            this.regid = this.hotkey.RegisterHotkey(this.Key, (uint) this.keyModifiers);
        }

        public void reghok(HotkeyEventHandler heh, IntPtr ipt)
        {
            this.hotkey = new Hotkey(ipt);
            this.hotkey.OnHotkey += heh;
            this.regid = this.hotkey.RegisterHotkey(this.Key, (uint) this.keyModifiers);
        }
    }
}

