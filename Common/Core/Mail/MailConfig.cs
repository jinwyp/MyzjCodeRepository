using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Core.Mail
{
    public class MailConfig
    {
        private string[] mailTo;
        private string[] bcc;
        private string[] cc;
        private MailPriority priority;
        private string subject;
        private string body;
        private string bodyFormat;
        private Encoding bodyEncoding;
        private bool isHtml;


        private string[] _attachmentFiles;

        public string[] AttachmentFiles
        {
            get { return _attachmentFiles; }
            set { _attachmentFiles = value; }
        }
        /// <summary>
        /// 邮件收件人地址列表
        /// </summary>

        public string[] MailTo
        {
            get { return mailTo; }
            set { mailTo = value; }
        }


        /// <summary>
        /// 邮件优先级
        /// </summary>
        public MailPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        /// <summary>
        /// 暗送地址列表
        /// </summary>
        public string[] Bcc
        {
            get { return bcc; }
            set { bcc = value; }
        }
        /// <summary>
        /// 抄送地址列表
        /// </summary>
        public string[] Cc
        {
            get { return cc; }
            set { cc = value; }
        }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        /// <summary>
        /// 邮件内容编码
        /// </summary>
        public Encoding Bodyencoding
        {
            get { return bodyEncoding; }
            set { bodyEncoding = value; }
        }

        public string Bodyformat
        {
            get { return bodyFormat; }
            set { bodyFormat = value; }
        }

        /// <summary>
        /// 邮件格式是否为Html格式
        /// </summary>
        public bool IsHtml
        {
            get { return isHtml; }
            set { isHtml = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MailConfig()
        {
            mailTo = new string[] { string.Empty };
            subject = string.Empty;
            body = string.Empty;
            bcc = new string[] { string.Empty };
            cc = new string[] { string.Empty };
            priority = MailPriority.Normal;
            bodyFormat = string.Empty;
            //  _Bodyencoding = Encoding.Default;
            isHtml = false;
        }

        /// <summary>
        ///  封装邮件类 主要参数 构造函数
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        public MailConfig(string to, string subject, string body)
        {
            MailTo = to.Split(new char[] { ';' });
            Subject = subject;
            Body = body;
            // _Bcc = new string[] { string.Empty };
            //  _Cc = new string[] { string.Empty };
            //   _Priority = MailPriority.Normal;
            //   _Bodyformat = string.Empty;
            //  _Bodyencoding = Encoding.Default;
            //   _IsHtml = false;
        }

        /// <summary>
        /// 封装邮件类 构造函数
        /// </summary>
        /// <param name="mailTo">收件人</param>
        /// <param name="bcc">暗送人列表</param>
        /// <param name="cc">抄送列表</param>
        /// <param name="priority">优先级</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="bodyformat">内容格式</param>
        /// <param name="bodyencoding">内容编码</param>
        /// <param name="isHtml">邮件是否为Html格式</param>
        public MailConfig(string mailTo, string[] bcc, string[] cc, MailPriority priority, string subject, string body, string bodyformat, Encoding bodyencoding, bool isHtml)
        {
            MailTo = mailTo.Split(new char[] { ';' });
            Bcc = bcc;
            Cc = cc;
            Priority = priority;
            Subject = subject;
            Body = body;
            bodyFormat = bodyformat;
            bodyEncoding = bodyencoding;
            IsHtml = isHtml;
        }

        //public bool SendEmail(out string message)
        //{
        //    //return WebDataProvider.Instance().SendEmail(this, out message);
        //    //message = null;
        //    //return true;
        //    MessageProvider
        //}
    }

}
