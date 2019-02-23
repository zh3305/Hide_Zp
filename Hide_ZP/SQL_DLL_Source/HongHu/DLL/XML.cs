using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Soap;
using System.Drawing;


namespace HongHu.DLL.Config
{

    /// <summary>
    /// 对xml存档进行操作
    /// </summary>
    public class XML:ConfigItem,IConfig
    {
        public XML()
        {
            ReadConfig();
        }
        public string ConnStrSplit = "";
        /// <summary>
        /// 读取存档
        /// </summary>
        /// <returns></returns>
        public void ReadConfig()
        {
            //存档文件是否存在
            if (File.Exists(SetString.ConfigPath))
            {

                try
                {
                    XmlTextReader xml = new XmlTextReader(SetString.ConfigPath);
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element && xml.Name == "ConnStr")
                        {
                            this.ConnStr = xml.ReadString();
                            xml.MoveToAttribute("ConnStrSplit");
                          ConnStrSplit=   xml.Value;
                            xml.MoveToElement();
                        }

                        //if (xml.NodeType == XmlNodeType.Element && xml.Name == "ConnStrSplit")
                        //{
                            //xml.MoveToAttribute("MaintoolstripX");
                            //MaintoolstripX = Convert.ToInt16(xml.Value);
                            //xml.MoveToAttribute("MaintoolstripY");
                            //MaintoolstripY = Convert.ToInt16(xml.Value);
                        //    //xml.MoveToElement();
                        //}
                    }
                }
                catch (Exception ex)
                {
                    SysDataLog.add(ex.ToString());
                }
            }
            else
            {
                //HongHu.UI.SetConnString Setconnform = new HongHu.UI.SetConnString();
                //Setconnform.ShowDialog();
            }
        }
        /// <summary>
        /// 将图片保存到XML文件
        /// </summary>
        /// <param name="imagepath"></param>
        /// <param name="xmlpath"></param>
        public void writreimagexml(string imagepath, string xmlpath)
        {
            Stream stream = new FileStream(xmlpath, FileMode.Create, FileAccess.Write, FileShare.None);
            SoapFormatter f = new SoapFormatter();
            Image img = Image.FromFile(imagepath);
            f.Serialize(stream, img);
            stream.Close();
        }

        /// <summary>
        /// 保存存档
        /// </summary>
        public void SaveConfig()
        {

            XmlDocument doc = new XmlDocument();
            //设置根节点名称
            XmlElement setting = doc.CreateElement("Config");
            doc.AppendChild(setting);


            //保存数据库连接字符串
            XmlElement X_ConnString = doc.CreateElement("ConnStr");
            X_ConnString.InnerText =this.ConnStr .ToString();

            X_ConnString.SetAttribute("ConnStrSplit", ConnStrSplit);
            setting.AppendChild(X_ConnString);



            //XmlElement SaveMaintoolstrip = doc.CreateElement("Maintoolstrip");
            //SaveMaintoolstrip.SetAttribute("MaintoolstripY", MaintoolstripY.ToString());
            //SaveMaintoolstrip.SetAttribute("MaintoolstripX", MaintoolstripX.ToString());

            //XmlElement SaveMaintoolstriptplace = doc.CreateElement("Maintoolstriptplace");
            //SaveMaintoolstriptplace.InnerText = maintoolstriptplace.ToString();

            //SaveMaintoolstrip.AppendChild(SaveMaintoolstriptplace);
            //SetString.AppendChild(SaveMaintoolstrip);

            ////保存使用的数据库
            //XmlElement X_DataServer = doc.CreateElement("DataServer");
            //X_DataServer.InnerText = DataServer.ToString();
            //SetString.AppendChild(X_DataServer);

            ////保存窗体是否总在最上
            //XmlElement Topmost = doc.CreateElement("MainTopmost");
            //Topmost.InnerText = MainTopmost.ToString();
            //SetString.AppendChild(Topmost);

            ////保存出现位置的X坐标
            //XmlElement y = doc.CreateElement("Mainy");
            //y.InnerText = Mainy.ToString();
            //SetString.AppendChild(y);

            ////保存出现位置的Y坐标
            //XmlElement x = doc.CreateElement("Mainx");
            //x.InnerText = Mainx.ToString();
            //SetString.AppendChild(x);

            ////保存单击最关闭是否最小化
            //XmlElement thisetxt = doc.CreateElement("Etxt");
            //thisetxt.InnerText = Etxt.ToString();
            //SetString.AppendChild(thisetxt);

            ////保存maintoolstript的在toolpanle中的位置
            //XmlElement SaveMaintoolstrip = doc.CreateElement("Maintoolstrip");
            //SaveMaintoolstrip.SetAttribute("MaintoolstripY", MaintoolstripY.ToString());
            //SaveMaintoolstrip.SetAttribute("MaintoolstripX", MaintoolstripX.ToString());

            //XmlElement SaveMaintoolstriptplace = doc.CreateElement("Maintoolstriptplace");
            //SaveMaintoolstriptplace.InnerText = maintoolstriptplace.ToString();

            //SaveMaintoolstrip.AppendChild(SaveMaintoolstriptplace);
            //SetString.AppendChild(SaveMaintoolstrip);

            XmlElement root = doc.DocumentElement;
            XmlDeclaration xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", null);
            doc.InsertBefore(xmlDec, root);
            try
            {
                doc.Save(SetString.ConfigPath);
            }
            catch (Exception xe)
            {
                  SysDataLog.add(xe.Message);
            }
        }
    }
}
