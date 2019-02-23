namespace HongHu.DLL.Config
{
    using HongHu;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Xml;

    public class ConfigItemXML : ConfigItem
    {
        private string connStrSplit = "";

        public ConfigItemXML()
        {
            SysDataLog.log.Info("开始加载用户配置");
            this.ReadConfig();
        }

        public void ReadConfig()
        {
            if (File.Exists(SetString.ConfigPath))
            {
                try
                {
                    XmlTextReader xml = new XmlTextReader(SetString.ConfigPath);
                    while (xml.Read())
                    {
                        if ((xml.NodeType == XmlNodeType.Element) && (xml.Name == "SQLConn"))
                        {
                            xml.MoveToAttribute("ConnLinkmode");
                            SetString.LinkMode = (SetString.SQLLinkMode) Enum.Parse(typeof(SetString.SQLLinkMode), xml.Value);
                            xml.MoveToAttribute("ConnStrSplit");
                            this.ConnStrSplit = xml.Value;
                            xml.MoveToElement();
                            SetString.SQLConn = xml.ReadString();
                        }
                        if ((xml.NodeType == XmlNodeType.Element) && (xml.Name == "ConfigJson"))
                        {
                            SetString.ConfigJson = xml.ReadString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SysDataLog.log.Error("读取用户配置文件:" + SetString.ConfigPath + " 出错", ex);
                }
            }
        }

        public void SaveConfig()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement setting = doc.CreateElement("Config");
            doc.AppendChild(setting);
            XmlElement X_ConnString = doc.CreateElement("SQLConn");
            X_ConnString.InnerText = SetString.SQLConn;
            X_ConnString.SetAttribute("ConnLinkmode", SetString.LinkMode.ToString());
            X_ConnString.SetAttribute("ConnStrSplit", this.ConnStrSplit);
            setting.AppendChild(X_ConnString);
            XmlElement CinfigDate = doc.CreateElement("ConfigJson");
            CinfigDate.InnerText = SetString.ConfigJson;
            setting.AppendChild(CinfigDate);
            XmlElement root = doc.DocumentElement;
            XmlDeclaration xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", null);
            doc.InsertBefore(xmlDec, root);
            try
            {
                doc.Save(SetString.ConfigPath);
            }
            catch (Exception xe)
            {
                SysDataLog.log.Error("保存用户配置文件出错!", xe);
            }
        }

        public void writreimagexml(string imagepath, string xmlpath)
        {
            Stream stream = new FileStream(xmlpath, FileMode.Create, FileAccess.Write, FileShare.None);
            SoapFormatter f = new SoapFormatter();
            Image img = Image.FromFile(imagepath);
            f.Serialize(stream, img);
            stream.Close();
        }

        public string ConnStrSplit
        {
            get
            {
                this.connStrSplit = SetString.DBServer + "^" + SetString.DBName + "^" + SetString.DBUser + "^" + SetString.DBPass;
                return this.connStrSplit;
            }
            set
            {
                this.connStrSplit = value;
                string[] Css = value.Split(new char[] { '^' });
                SetString.DBServer = Css[0];
                SetString.DBName = Css[1];
                SetString.DBUser = Css[2];
                SetString.DBPass = Css[3];
            }
        }
    }
}

