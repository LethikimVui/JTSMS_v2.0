using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using SharedObjects.ValueObjects;
using SharedObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JTSMS.Models
{
    public class Notification
    {
      
        #region Sent Email
        public static void SentEmail(RegistrationViewModel model, VApproval Approval_get)
        {
            string body = string.Empty;

            body += "<div style='border - top:3px solid #22BCE5'>Hi,</div>";
            body += "There is a new request pending at your step";

            body += "<p>You may access <a href='http://vnhcmm0teapp05/jtsms/Request/RequestDetail_get_by_id?reqid=" + model.ReqId + "'>here</a> to get detail</p>";
            body += " <p>This is automatic email, please do not reply</p>    Thanks";
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient("corimc04.corp.JABIL.ORG");
            message.From = new MailAddress("JTSMS@Jabil.com");

            //foreach (var email in Approval_get.Email.Split('|'))
            //{
            //    if (email != null)
            //    {
            //        message.To.Add(new MailAddress(email));
            //    }
            //}



            message.To.Add("vui_le@jabil.com");
           
            message.Subject = "New Request Is Pending For Your Approval";
            message.Body = body;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
        #endregion
    }
}
