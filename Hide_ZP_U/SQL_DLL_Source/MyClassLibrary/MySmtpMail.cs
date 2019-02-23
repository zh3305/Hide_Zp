namespace MyClassLibrary
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mail;

    public class MySmtpMail
    {
        private Encoding bodyEncoding;
        private static string emailACopyUser;
        private string emailAttachment;
        private string emailBody;
        private MailPriority emailPriority;
        private string emailServerhost;
        private string emailSubject;
        private string emailto;
        private string emailUserName;
        private string emailUserpassword;
        private string fromemail;

        public MySmtpMail()
        {
            this.emailServerhost = "smtp.qq.com";
            this.emailUserpassword = "123456";
            this.emailUserName = "1029589450@qq.com";
            this.fromemail = "1029589450@qq.com";
            this.emailto = "1029589450@qq.com";
            this.emailSubject = string.Empty;
            this.emailPriority = MailPriority.Normal;
            this.bodyEncoding = Encoding.UTF8;
            this.emailBody = string.Empty;
        }

        public MySmtpMail(string NewEmailBody)
        {
            this.emailServerhost = "smtp.qq.com";
            this.emailUserpassword = "123456";
            this.emailUserName = "1029589450@qq.com";
            this.fromemail = "1029589450@qq.com";
            this.emailto = "1029589450@qq.com";
            this.emailSubject = string.Empty;
            this.emailPriority = MailPriority.Normal;
            this.bodyEncoding = Encoding.UTF8;
            this.emailBody = string.Empty;
            this.EmailBody = NewEmailBody;
        }

        public MySmtpMail(string NewEmailBody, string To)
        {
            this.emailServerhost = "smtp.qq.com";
            this.emailUserpassword = "123456";
            this.emailUserName = "1029589450@qq.com";
            this.fromemail = "1029589450@qq.com";
            this.emailto = "1029589450@qq.com";
            this.emailSubject = string.Empty;
            this.emailPriority = MailPriority.Normal;
            this.bodyEncoding = Encoding.UTF8;
            this.emailBody = string.Empty;
            this.EmailBody = NewEmailBody;
            this.Emailto = To;
        }

        public MySmtpMail(string NewEmailBody, string To, string newemailAttachment)
        {
            this.emailServerhost = "smtp.qq.com";
            this.emailUserpassword = "123456";
            this.emailUserName = "1029589450@qq.com";
            this.fromemail = "1029589450@qq.com";
            this.emailto = "1029589450@qq.com";
            this.emailSubject = string.Empty;
            this.emailPriority = MailPriority.Normal;
            this.bodyEncoding = Encoding.UTF8;
            this.emailBody = string.Empty;
            this.EmailBody = NewEmailBody;
            this.Emailto = To;
            this.EmailAttachment = newemailAttachment;
        }

        public bool SendMessage()
        {
            MailMessage message = new MailMessage();
            message.From = this.Fromemail;
            message.Body = this.EmailBody;
            message.BodyEncoding = this.BodyEncoding;
            message.Cc = this.EmailACopyUser;
            message.Priority = this.emailPriority;
            message.Subject = this.EmailSubject;
            message.To = this.Emailto;
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", this.EmailUserName);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", this.emailUserpassword);
            if ((this.EmailAttachment != null) && (this.EmailAttachment.Trim() != ""))
            {
                char[] delim = new char[] { ',' };
                foreach (string att in this.EmailAttachment.Trim().Split(delim))
                {
                    MailAttachment myAttachment = new MailAttachment(att);
                    message.Attachments.Add(myAttachment);
                }
            }
            try
            {
                SmtpMail.SmtpServer = this.EmailServerhost;
                SmtpMail.Send(message);
            }
            catch (HttpException)
            {
                return false;
            }
            return true;
        }

        public Encoding BodyEncoding
        {
            get
            {
                return this.bodyEncoding;
            }
            set
            {
                this.bodyEncoding = value;
            }
        }

        public string EmailACopyUser
        {
            get
            {
                return emailACopyUser;
            }
            set
            {
                emailACopyUser = value;
            }
        }

        public string EmailAttachment
        {
            get
            {
                return this.emailAttachment;
            }
            set
            {
                this.emailAttachment = value;
            }
        }

        public string EmailBody
        {
            get
            {
                return this.emailBody;
            }
            set
            {
                this.emailBody = value;
            }
        }

        public emailPriorityenum EmailPriority
        {
            get
            {
                if (this.emailPriority == MailPriority.High)
                {
                    return emailPriorityenum.High;
                }
                if (this.emailPriority == MailPriority.Low)
                {
                    return emailPriorityenum.Low;
                }
                return emailPriorityenum.Normal;
            }
            set
            {
                this.emailPriority = (MailPriority) value;
            }
        }

        public string EmailServerhost
        {
            get
            {
                return this.emailServerhost;
            }
            set
            {
                this.emailServerhost = value;
            }
        }

        public string EmailSubject
        {
            get
            {
                return this.emailSubject;
            }
            set
            {
                this.emailSubject = value;
            }
        }

        public string Emailto
        {
            get
            {
                return this.emailto;
            }
            set
            {
                this.emailto = value;
            }
        }

        public string EmailUserName
        {
            get
            {
                return this.emailUserName;
            }
            set
            {
                this.emailUserName = value;
            }
        }

        public string EmailUserpassword
        {
            set
            {
                this.emailUserpassword = value;
            }
        }

        public string Fromemail
        {
            get
            {
                return this.fromemail;
            }
            set
            {
                this.fromemail = value;
            }
        }

        public enum emailPriorityenum
        {
            Normal,
            Low,
            High
        }
    }
}

