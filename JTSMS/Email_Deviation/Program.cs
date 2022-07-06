using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email_Deviation
{
    class Program
    {
        static private List<string> emails = ConfigurationManager.AppSettings["email"].Split(';').ToList();

        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            List<DataViewModel> list = new List<DataViewModel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("usp_RequestDetail_get_all_deviationtype", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new DataViewModel
                        {
                            scriptid = reader["scriptid"].ToString(),
                            reqId = reader["reqId"].ToString(),
                            station = reader["station"].ToString(),
                            platform = reader["platform"].ToString(),
                            custName = reader["custName"].ToString(),
                            custId = int.Parse(reader["custId"].ToString()),
                            aging = reader["dayLeft"].ToString(),
                            closureDate = reader["closureDate"].ToString(),
                            expiryDate = reader["expiryDate"].ToString(),
                            assemblyNumber = reader["assemblyNumber"].ToString(),
                            assemblyRevision = reader["assemblyRevision"].ToString(),

                        });
                    }
                    conn.Close();

                    var custNames = list.Select(s => new { s.custName, s.custId }).Distinct().ToList();
                    foreach (var item in custNames)
                    {
                        var custName = item.custName.ToString();
                        var custId = item.custId;

                        List<UserViewModel> lst_User = new List<UserViewModel>();
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("usp_Access_UserRole_Get_By_custId", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_custId", custId);
                        MySqlDataReader reader1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (reader1.Read())
                        {
                            lst_User.Add(new UserViewModel
                            {
                                userEmail = reader1["userEmail"].ToString(),

                            });
                        }
                        conn.Close();


                        var filterList = list.Where(s => s.custName == custName).ToList();
                        var lst_21day = filterList.Where(s => int.Parse(s.aging) == 21).ToList();

                        if (lst_21day.Count > 0)
                        {
                            SentEmail(custName, "Expired ater 21 days", lst_21day, lst_User);
                        }
                        var lst_14day = filterList.Where(s => int.Parse(s.aging) == 14).ToList();
                        if (lst_14day.Count > 0)
                        {
                            SentEmail(custName, "Expired ater 14 days", lst_14day, lst_User);
                        }
                        var lst_7day = filterList.Where(s => int.Parse(s.aging) <= 7).ToList();
                        if (lst_7day.Count > 0)
                        {
                            SentEmail(custName, "Expired Soon", lst_7day, lst_User);
                        }                       
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        static void SentEmail(string custName, string subject, List<DataViewModel> list, List<UserViewModel> lst_User)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient("corimc04.corp.JABIL.ORG");
            message.From = new MailAddress("JTSMS@Jabil.com");

            string table = String.Empty;
            table += "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 0 + " width = " + 400 + "><tbody><tr style = 'background-color:#fefbd8'>" +
                "<th>custName</th><th>assembly Number</th><th>station</th><th>platform</th><th>assembly Revision</th><th>Days After Expired</th><th>closureDate</th><th>Expiry Date</th>" +
                //"<th>Location</th><th>Extension Loop</th><th>Remarks</th><th>Registered By</th><th>Days After Expired</th>" +
                "</tr>";
            foreach (var item in list)
            {
                var assemblyNumber = item.assemblyNumber.ToString();
                var station = item.station.ToString();
                var platform = item.platform.ToString();
                var assemblyRevision = item.assemblyRevision.ToString();
                var aging = item.aging.ToString();
                var closureDate = item.closureDate.ToString();
                var expiryDate = item.expiryDate.ToString();

                table += "<tr><th>" + custName + "</th><th>" + assemblyNumber + "</th><th>" + station + "</th>" +
                "<th>" + platform + "</th><th>" + assemblyRevision + "</th><th>" + aging + "</th><th>" + closureDate + "</th><th>" + expiryDate + "</th>";
            }
            table += "</tbody></table>";

            string body = String.Empty;
            body += "<p>Hi all,</p>";
            body += table;
            body += "</br>";
            body += "This is automatic email, please do not reply all";


            message.Subject = "[" + custName + "] " + subject;

            message.Body = body;
            foreach (var item in lst_User)
            {
                message.To.Add(new MailAddress(item.userEmail));

            }
            foreach (var email in emails)
            {
                if (email != "")
                {
                    message.CC.Add(new MailAddress(email));
                }
            }

            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
