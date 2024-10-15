using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace LibraryManagement
{
    public class EmailHelper
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toEmail);
                mail.From = new MailAddress("bharathyadav0047@gmail.com"); // Your email
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // Your SMTP host
                    Port = 587, // Change as per your provider (e.g., 587 for TLS)
                    EnableSsl = true, // Enable SSL if required by your provider
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("bharathyadav0047@gmail.com", "bharath123") // Your credentials
                };

                try
                {
                    smtp.Send(mail);
                }
                catch (SmtpException smtpEx)
                {
                    // Log specific SMTP errors
                    Console.WriteLine("SMTP Error: " + smtpEx.Message);
                }
                catch (Exception ex)
                {
                    // Handle any other errors
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                // Log or handle the error
                throw new Exception("Error sending email: " + ex.Message);
            }
        }
    }
}