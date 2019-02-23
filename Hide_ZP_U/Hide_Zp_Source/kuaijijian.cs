
    using HongHu.DLL;
    using System;
    using System.Windows.Forms;
namespace HongHu
{
	/// <summary>
	/// 快捷键类
	/// </summary>
    public class kuaijijian
    {
        public string Hname;
        /// <summary>
        /// 热键
        /// </summary>
//        [assembly: CLSCompliant(true)]
        public Hotkey hotkey;
        public Keys Key = Keys.Escape;
        public int KeyModifiers;
        public bool kjj = false;
        public int regid = 0;

        public kuaijijian(string _Hname, Keys _key, int _KeyModifiers)
        {
            this.Hname = _Hname;
            this.Key = _key;
            this.KeyModifiers = _KeyModifiers;
        }

        public void reghok(IntPtr ipt)
        {
            this.hotkey = new Hotkey(ipt);
            this.regid = this.hotkey.RegisterHotkey(this.Key, (uint) this.KeyModifiers);
        }

        public void reghok(HotkeyEventHandler heh, IntPtr ipt)
        {
            this.hotkey = new Hotkey(ipt);
            this.hotkey.OnHotkey += heh;
            this.regid = this.hotkey.RegisterHotkey(this.Key, (uint) this.KeyModifiers);
        }
    }
}

