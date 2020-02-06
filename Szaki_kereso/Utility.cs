using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Szaki_kereso
{
    public class Utility
    {
        public static string Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static void SendMail(string emailToAdress)
        {
            string subjectText = "(Registartion complete! )";
            string emailBodyText = ("Registartion complete now you can use the APP!");
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("szakikereso2020@gmail.com");
                mail.To.Add(emailToAdress);
                mail.Subject = subjectText;
                mail.Body = emailBodyText;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("szakikereso2020@gmail.com", "Szakikereso94");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
