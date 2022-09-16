using Microsoft.Extensions.Configuration;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public class MailService: IMailService
    {
        private readonly IConfiguration config;
        public MailService(IConfiguration config)
        {
            this.config = config;
        }
        public void SendEmail(MailClass mail)
        {
                mail.from= config.GetValue<string>("mailconfig:SenderEmail");
                var apikey = config.GetValue<string>("mailconfig:Apikey");
                var UserName = config.GetValue<string>("mailconfig:hostmails");
                WebClient client = new WebClient();
                NameValueCollection values = new NameValueCollection();
                values.Add("username", UserName);
                values.Add("api_key", apikey);
                values.Add("from", mail.from);
                values.Add("from_name", mail.fromName);
                values.Add("subject", mail.subject);
                values.Add("body_text", mail.bodyText);
                values.Add("to", mail.to);

                byte[] response = client.UploadValues("https://api.elasticemail.com/mailer/send", values);
                //return Encoding.UTF8.GetString(response);
           
        }
    }
}
