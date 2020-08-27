using RentACar.BLL.Models;
using System;
using System.Net.Mail;

namespace RentACar.MailService
{
    public class MailServices
    {

        public void SendEmail(MailPOCO mailModel)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            try
            {
                using (SmtpServer)
                {
                    mail.Subject = mailModel.Subject;
                    mail.From = new MailAddress("xml.RenACar2020@gmail.com");
                    foreach (String email in mailModel.Receivers)
                    {
                        mail.To.Add(email);
                    }


                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("xml.RenACar2020@gmail.com", "RenACar123");
                    SmtpServer.EnableSsl = true;

                    mail.Body = mailModel.Body;

                    SmtpServer.Send(mail);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //if  (SmtpServer is IDisposable) myObject.Dispose();
                SmtpServer.Dispose();
            }

        }
    }
}
