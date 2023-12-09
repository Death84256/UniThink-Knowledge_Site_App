using ASI.Basecode.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace ASI.Basecode.Services.Services
{
    public class ForgotPass : IForgotPass
    {
        private readonly SmtpClient _smtpClient;
        public ForgotPass()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("zarcokoykoy35@gmail.com", "igewreevznrbbplw")
            };
        }
        public void ChangePass(string email, string host, string username, string code)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("zarcokoykoy35@gmail.com"),
                Subject = "CHANGE PASSWORD",
                Body = "https://" + host + "/Account/ResetPassword?email=" + email + "&code=" + code,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(email));

            _smtpClient.Send(mailMessage);
        }
    }
}
