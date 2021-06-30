using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TuHobbyWeb.Helpers
{
    public class EmailHelper
    {
        private static readonly string SMTP_SERVER = ConfigurationManager.AppSettings["SMTP_SERVER"];
        private static readonly string SMTP_USER = ConfigurationManager.AppSettings["SMTP_USER"];
        private static readonly string SMTP_PASSWORD = ConfigurationManager.AppSettings["SMTP_PASSWORD"];
        private static readonly string SMTP_PORT = ConfigurationManager.AppSettings["SMTP_PORT"];
        private static readonly string FROM_EMAIL = ConfigurationManager.AppSettings["FROM_EMAIL"];

        public static bool Send(string emailTo, string subject, string body, out string msg)
        {
            try
            {
                //definir el mensaje
                MailMessage message = new MailMessage(FROM_EMAIL, emailTo);
                message.IsBodyHtml = true;
                message.Body = "<h1 style='color: blue'>Tu Hobby Web</h1>";
                message.Body += $"<p>{body}</p>";
                message.Subject = subject;

                //agregar los parametros del mensaje
                SmtpClient client = new SmtpClient(SMTP_SERVER, int.Parse(SMTP_PORT));
                client.Credentials = new NetworkCredential(SMTP_USER, SMTP_PASSWORD);

                // enviar el mensaje
                client.Send(message);

                // Feedback para el usuario
                msg = "The email has been sent successfully";
                return true;
            } catch (Exception e)
            {
                // guardar en un log el error
                msg = e.Message;
                return false;
            }
        }
    }
}