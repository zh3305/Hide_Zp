namespace HongHu
{
    using System;

    public class UA_AccountItem
    {
        /// <summary>
        /// ÊÇ·ñÒþ²Ø
        /// </summary>
        private bool cAcc_hide;
        /// <summary>
        /// ÕÊÌ×ºÅ
        /// </summary>
        private string cAcc_Id;
        /// <summary>
        /// Òþ²ØµÈ¼¶
        /// </summary>
        private string cAcc_live;
        /// <summary>
        /// ÕÊÌ×Ãû³Æ
        /// </summary>
        private string cAcc_Name;
        /// <summary>
        /// Òþ²ØID
        /// </summary>
        private string hideId;

        public UA_AccountItem()
        {
        }

        public UA_AccountItem(string _cAcc_Id, string _cAcc_Name, string _cAcc_live, bool _cAcc_hide)
        {
            this.cAcc_Name = _cAcc_Name;
            this.cAcc_Id = _cAcc_Id;
            this.cAcc_live = _cAcc_live;
            this.cAcc_hide = _cAcc_hide;
        }

        public UA_AccountItem(string _cAcc_Id, string _cAcc_Name, string _cAcc_live, bool _cAcc_hide, string _HideId)
        {
            this.cAcc_Name = _cAcc_Name;
            this.cAcc_Id = _cAcc_Id;
            this.cAcc_live = _cAcc_live;
            this.cAcc_hide = _cAcc_hide;
            this.hideId = _HideId;
        }

        public bool CAcc_hide
        {
            get
            {
                return this.cAcc_hide;
            }
            set
            {
                this.cAcc_hide = value;
            }
        }

        public string CAcc_hide_p
        {
            get
            {
                return (this.cAcc_hide ? "ÏÔÊ¾" : "Òþ²Ø");
            }
        }

        public string CAcc_Id
        {
            get
            {
                return this.cAcc_Id;
            }
            set
            {
                this.cAcc_Id = value;
            }
        }

        public string CAcc_live
        {
            get
            {
                return this.cAcc_live;
            }
            set
            {
                this.cAcc_live = value;
            }
        }

        public string CAcc_live_p
        {
            get
            {
                return EnumDescription.GetFieldText((Runfs)Enum.Parse(typeof(Runfs), this.cAcc_live)).ToString();
            }
        }

        public string CAcc_Name
        {
            get
            {
                return this.cAcc_Name;
            }
            set
            {
                this.cAcc_Name = value;
            }
        }

        public string HideId
        {
            get
            {
                return this.hideId;
            }
            set
            {
                this.hideId = value;
            }
        }
    }
}

