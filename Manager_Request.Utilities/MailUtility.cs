using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Manager_Request.Ultilities
{
    public class MailUtility
    {
        #region Methods

        public static bool Send(string from, string displayName, string password, string to, string subject, string body, bool isBodyHtml, string host, int port, bool enableSSL, ref Exception error, byte[] file)
        {
            //Khai báo một lá thư
            MailMessage mail = new MailMessage();

            //Khai báo đối tượng gửi thư , người đưa thư
            SmtpClient smtpServer = new SmtpClient();

            //Ghi các giá trị lên lá thư
            mail.From = new MailAddress(from, displayName);
            mail.To.Add(to);
            mail.To.Add("admissions@eiu.edu.vn");
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            if (file != null)
            {
              mail.Attachments.Add(new Attachment(new MemoryStream(file), "Phiếu đăng ký xét tuyển ĐH chính quy năm 2021.pdf"));
            }

            smtpServer.Host = host;
            smtpServer.Credentials = new NetworkCredential(from, password);
            smtpServer.EnableSsl = enableSSL;
            smtpServer.Port = port;

            try
            {
                smtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                return false;
            }
        }


        public bool Send()
        {
            Exception error = null;
            bool result = Send(this.From, this.DisplayName, this.Password, this.To, this.Subject, this.Body, this.IsBodyHtml, this.Host, this.Port, this.EnableSSL, ref error, this.File);
            this.Error = error;

            return result;
        }

        public void Send(string emailTo, string emailCc, string subject, string content)
        {
            var fromEmailAddress = this.From;
            var fromEmailPassword = this.Password;
            var fromMailDisplayName = this.DisplayName;
            var smtpHost = this.Host;
            var smtpPort = this.Port;
            bool enableSsl = this.EnableSSL;

            string body = content;

            MailMessage mailMessage = new MailMessage(new MailAddress(fromEmailAddress, fromMailDisplayName), new MailAddress(emailTo));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            if (!string.IsNullOrEmpty(emailCc))
            {
                foreach (var mailCC in emailCc.Split(";"))
                {
                    mailMessage.CC.Add(mailCC);
                }
            }

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enableSsl;
            client.Port = smtpPort;
            client.Send(mailMessage);
        }


        public async Task SendMaiAsync(string toMail, string subject, string body, string from, string password, bool isBodyHtml, string host, int port, bool enableSSL)
        {
            MailMessage mail = new MailMessage();

            //Khai báo đối tượng gửi thư , người đưa thư
            SmtpClient smtpServer = new SmtpClient();

            mail.To.Add(toMail);
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            smtpServer.Host = host;
            smtpServer.Credentials = new NetworkCredential(from, password);
            smtpServer.EnableSsl = enableSSL;
            smtpServer.Port = port;
            try
            {
                await smtpServer.SendMailAsync(mail);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion Methods

        #region Properties

        /// <summary>
        /// Trả về hoặc thiết lập giá trị From
        /// </summary>
        public string From
        {
            get;
            set;
        }

        /// <summary>
        /// Trả về hoặc thiết lập Password
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// Trả về hoặc thiêế lập điểm đến;
        /// </summary>
        public string To
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        public string Body
        {
            get;
            set;
        }

        public bool IsBodyHtml
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public bool EnableSSL
        {
            get;
            set;
        }

        public Exception Error
        {
            get;
            set;
        }

        public byte[] File { get; set; }

        #endregion Properties

        #region Constructor

        public MailUtility()
        {
        }

        public MailUtility(string from, string password)
        {
            this.From = from;
            this.Password = password;
        }

        #endregion Constructor
    }
}