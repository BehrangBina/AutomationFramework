using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace AutomationFramework.Util.Behrang.EmailHandler
{
    public  class EmailHandler
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="attachmentsAbsolutePath">array containg absolute path of each attachment</param>
        /// <param name="from">from email</param>
        /// <param name="to">to email</param>
        /// <param name="smptpHost">SMTP Host String</param>
        /// <param name="subject">Mail Subject</param>
        /// <param name="body">Mail Body</param>
        public void SendEmailWithAttachment(string[] attachmentsAbsolutePath,
            string from,string[] to,string  smptpHost,string subject,string body)
        {
            //var comment = new StringBuilder();
            try
            {

                 
                MailMessage mail = new MailMessage();
                MailAddress fromMail = new MailAddress(from);
                foreach (var email in to)
                {
                    mail.To.Add(email);
                    mail.From = fromMail;
                }

                SmtpClient client = new SmtpClient
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = smptpHost
                };

                mail.Subject = $"{subject}:  {DateTime.Now}";

                try
                {

                    foreach (var attachment in attachmentsAbsolutePath)
                    {
                        if (attachment == null) continue;
                        //create attachment
                        var data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                        // Add time stamp information for the file.
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(attachment);
                        disposition.ModificationDate = File.GetLastWriteTime(attachment);
                        disposition.ReadDate = File.GetLastAccessTime(attachment);
                        // Add the file attachment to this e-mail message.
                        mail.Attachments.Add(data);
                    }
                    mail.Body = body;
                    client.Send(mail);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
         
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
}
