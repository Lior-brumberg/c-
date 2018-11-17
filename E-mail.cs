using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_mail
{
    /*Help class for sending mail using a Gmail dns.
     * recieves from address, to address and username & password in array format
     * applies ssl encryption and sends E-mail using the subject and content recieved in the "Send_Email" function parameters.
     */
    class Gmail_mail_Handler
    {
        private System.Net.Mail.MailMessage message;
        private System.Net.Mail.SmtpClient smtp;
        public string host_address { get; set; }
        public string Reciever_address { get; set; }
        private string userName, Password;

        public Gmail_mail_Handler(string from, string to, string[] crudentials)
        {
            this.userName = crudentials[0];
            this.Password = crudentials[1];
            this.message = new System.Net.Mail.MailMessage();
            this.host_address = from;
            this.Reciever_address = to;
            this.smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
        }

        public void Send_Email(string subject, string content)
        {
            this.message.To.Add(this.Reciever_address);
            this.message.Subject = subject;
            this.message.From = new System.Net.Mail.MailAddress(this.host_address);
            this.message.Body = content;
            this.smtp.Credentials = new System.Net.NetworkCredential(this.userName, this.Password);
            this.smtp.EnableSsl = true;

            try
            {
                this.smtp.Send(this.message);
            }
            catch (Exception e) { Console.WriteLine("ERROR: {0}", e.ToString()); }
        }
        public void Set_Crudentials(string[] newC)
        {
            if (newC != null)
            {
                this.userName = newC[0];
                this.Password = newC[1];
            }
            else
                throw new ArgumentException("Crudentials are not valid");
            
        }
        public string[] Get_crudentials()
        {
            string[] to_return = new string[2];
            to_return[0] = this.userName;
            to_return[1] = this.Password;

            return to_return;
        }
        
    }
}
