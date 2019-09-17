using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JNPPortal
{
    public static class Helper
    {

        public static void SendEmail(string toEmailAddress, string emailSubject, string emailBody)
        {

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("jabujacob@gmail.com","Pass75word");
            MailMessage msgobj = new MailMessage();
            msgobj.To.Add(toEmailAddress);
            msgobj.From = new MailAddress("jabujacob@gmail.com");
            msgobj.Subject = emailSubject;
            msgobj.Body = emailBody;
            client.Send(msgobj);

            
        }

        public static List<SelectListItem> BoolDropDownListReBind()
        {
            List<SelectListItem> SelYN = new List<SelectListItem>();

            SelYN.Add(new SelectListItem
            {
                Text = "Yes",
                Value = true.ToString()
            });
            SelYN.Add(new SelectListItem
            {
                Text = "No",
                Value = false.ToString()
            });

            return SelYN;
        }



    }
}