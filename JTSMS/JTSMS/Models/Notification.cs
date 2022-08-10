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
        private readonly IConfiguration configuration;
        public Notification()
        {

        }
        public Notification(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #region Sent Email
        public void SentEmail(RegistrationViewModel model, VApproval Approval_get, string CC)
        {
            string body = string.Empty;

            body += "<div style='border - top:3px solid #22BCE5'>Hi,</div>";
            body += "There is a new request pending at your step";

            body += "<p>You may access <a href='http://vnhcmm0teapp05/jtsmstest/Request/RequestDetail_get_by_id?reqid=" + model.RegId + "'>here</a> to get detail</p>";
            body += " <p>This is automatic email, please do not reply</p>    Thanks";
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient("corimc04.corp.JABIL.ORG");
            message.From = new MailAddress("JTSMS@Jabil.com");

            foreach (var email in Approval_get.Email.Split(','))
            {
                if (email != null)
                {
                    message.To.Add(new MailAddress(email));
                }
            }

            message.CC.Add(model.CreatedEmail);
            var configureEmail = CC.Split(';');
            foreach (var email in configureEmail)
            {
                if (email != "")
                {
                    message.CC.Add(new MailAddress(email));
                }
            }
            message.Subject = String.Format( "[Testing] [Req # {0} ] New Request Is Pending For Your Approval", model.RegId);
            message.Body = body;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
        #endregion
        #region SentEmail_UpdateStatus
        public void SentEmail_UpdateStatus(RegistrationViewModel model, VRequest request, string CC, VApproval approval = null, List<VUserRole> userList = null)
        {
            string body = string.Empty;
            body += "<div style='border - top:3px solid #22BCE5'>Hi,</div>";
            //body += "There is a new change request by " + model.CreatedEmail + " as below:";

            body += "<p>You may access <a href='http://vnhcmm0teapp05/jtsms/Request/RequestDetail_get_by_id?reqid=" + model.RegId + "'>here</a> to get detail</p>";
            body += "<p>This is automatic email, please do not reply</p>    Thanks";
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient("corimc04.corp.JABIL.ORG");
            message.From = new MailAddress("JTSMS@Jabil.com");

            var statusId = request.StatusId;
            string subject = "";
            switch (statusId)
            {
                case 2: // pending approval
                    foreach (var email in approval.Email.Split('|'))
                    {
                        if (email != null)
                        {
                            message.To.Add(new MailAddress(email));
                        }
                    }
                    subject = "[Pending Approval] Request Is Pending Approval";
                    break;
                case 4: // approved

                    if (userList.Any())
                    {
                        foreach (var email in userList)
                        {
                            if (email.UserEmail != null)
                            {
                                message.To.Add(new MailAddress(email.UserEmail));
                            }
                        }
                    }
                    subject = "[Closed] Request is approved and closed";
                    break;
                default:
                    message.To.Add(new MailAddress(request.CreatedEmail));
                    subject = "[Rejected] Request is rejected";
                    break;
            }

            message.CC.Add(new MailAddress(model.CreatedEmail));
            var configureEmail = CC.Split(';');
            foreach (var email in configureEmail)
            {
                if (email != "")
                {
                    message.CC.Add(new MailAddress(email));
                }
            }
            message.Subject = String.Format("[TESTING][Req # {0}] ", model.RegId) + subject;
            message.Body = body;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
        #endregion

    }
}
