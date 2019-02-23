using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Mail;

namespace HongHu
{

    public class MySmtpMail
    {
        /// <summary>
        /// 指定电子邮件具de优先级
        /// </summary>
        public enum emailPriorityenum
        {
            /// <summary>
            /// 指定电子邮件具有高优先级
            /// </summary>
            High = MailPriority.High,
            /// <summary>
            /// 指定电子邮件具有低优先级
            /// </summary>
            Low = MailPriority.Low,
            /// <summary>
            /// 指定电子邮件具有普通优先级
            /// </summary>
            Normal = MailPriority.Normal
        }
        #region 属性
        string emailServerhost ="smtp.qq.com";
        /// <summary>
        /// 设置用于发送电子邮件的Smtp服务器地址
        /// </summary>
        public string EmailServerhost
        {
            get { return emailServerhost; }
            set { emailServerhost = value; }
        }
        string emailUserpassword = "123456";
        /// <summary>
        /// 设置发送电子邮件的用户密码
        /// </summary>
        public string EmailUserpassword
        {
            set { emailUserpassword = value; }
        }
        string emailUserName = "1029589450@qq.com";
        /// <summary>
        ///   获取或设置发送电子邮件的用户名
        /// </summary>
        public string EmailUserName
        {
            get { return emailUserName; }
            set { emailUserName = value; }
        }

        string fromemail = "1029589450@qq.com";
        /// <summary>
        /// 获取或设置发信人的电子邮件地址
        /// </summary>
        public string Fromemail
        {
            get { return fromemail; }
            set { fromemail = value; }
        }

        string emailto = "1029589450@qq.com";//"330514962@qq.com";
        /// <summary>
        /// 获取或设置收件者邮箱多个以逗号隔开
        /// </summary>
        public string Emailto
        {
            get { return emailto; }
            set { emailto = value; }
        }
        string emailSubject = string.Empty;
        /// <summary>
        /// 获取或设置电子邮件的主题
        /// </summary>
        public string EmailSubject
        {
            get { return emailSubject; }
            set { emailSubject = value; }
        }
        MailPriority emailPriority = MailPriority.Normal;
        /// <summary>
        /// 获取或设置电子邮件的优先级
        /// </summary>
        public emailPriorityenum EmailPriority
        {
            get
            {
                if (emailPriority == MailPriority.High)
                {
                    return emailPriorityenum.High;
                }
                else if (emailPriority == MailPriority.Low)
                {
                    return emailPriorityenum.Low;
                }
                else
                {
                    return emailPriorityenum.Normal;
                }
            }
            set { emailPriority = (MailPriority)value; }
        }

        static string emailACopyUser;
        /// <summary>
        ///  获取或设置邮件抄送人,多个以逗号隔开
        /// </summary>
        public string EmailACopyUser
        {
            get { return emailACopyUser; }
            set { emailACopyUser = value; }
        }
        Encoding bodyEncoding = Encoding.UTF8;
        /// <summary>
        ///  获取或设置邮件的编码
        /// </summary>
        public Encoding BodyEncoding
        {
            get { return bodyEncoding; }
            set { bodyEncoding = value; }
        }

        string emailAttachment;
        /// <summary>
        /// 获取或设置邮件所发送的附件的路径,多个以逗号隔开
        /// </summary>
        public string EmailAttachment
        {
            get { return emailAttachment; }
            set { emailAttachment = value; }
        }

        string emailBody = string.Empty;
        /// <summary>
        /// 获取或设置将要发送的邮件的正文
        /// </summary>
        public string EmailBody
        {
            get { return emailBody; }
            set { emailBody = value; }
        }
        #endregion

        #region 邮件发送构造函数

        /// <summary>
        /// 邮件发送
        /// </summary>
        public MySmtpMail()
        {

        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="NewEmailBody">设置邮件的正文</param>
        public MySmtpMail(string NewEmailBody)
        {
            EmailBody = NewEmailBody;
        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="NewEmailBody">设置邮件的正文</param>
        /// <param name="To">收件者邮箱多个以逗号隔开</param>
        public MySmtpMail(string NewEmailBody, string To)
        {
            EmailBody = NewEmailBody;
            Emailto = To;
        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="To">收件者邮箱多个以逗号隔开</param>
        /// <param name="emailAttachment">邮件所发送的附件的路径,多个以逗号隔开</param>
        public MySmtpMail(string NewEmailBody, string To, string newemailAttachment)
        {
            EmailBody = NewEmailBody;
            Emailto = To;
            EmailAttachment = newemailAttachment;
        }
        #endregion

        #region 发送E-Mail
        /// <summary>
        ///  发送邮件
        /// </summary>
        /// <returns>如果返回为真 , 标示邮件发送成功,否则为失败</returns>
        public bool SendMessage()
        {
            MailMessage message = new MailMessage();
            message.From = Fromemail;
            message.Body = EmailBody;
            message.BodyEncoding = BodyEncoding;
            message.Cc = EmailACopyUser;
            message.Priority = emailPriority;
            message.Subject = EmailSubject;
            message.To = Emailto;
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", EmailUserName);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", emailUserpassword);
            if (EmailAttachment != null && EmailAttachment.Trim()!="")
            {
                char[] delim = new char[] { ',' };
                foreach (string att in EmailAttachment.Trim().Split(delim))
                {
                    MailAttachment myAttachment = new MailAttachment(att);
                    message.Attachments.Add(myAttachment);
                }
            }
            try
            {
                SmtpMail.SmtpServer = EmailServerhost;
                SmtpMail.Send(message);
            }
            catch (System.Web.HttpException ehttp)
            {
                throw ehttp;
                //return false;
            }
            return true;
        }
        #endregion
    }
}