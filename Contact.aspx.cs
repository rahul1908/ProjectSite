using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;

namespace ProjectSite
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            // Fetch the values from input fields
            string name = txtName.Text;
            string email = txtEmail.Text;
            string message = txtMessage.Text;

            // Check for empty fields and alert the user
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please fill in all fields.');", true);
                return;
            }

            try
            {
                // Send the email using Mailtrap's SMTP
                var result = await SendEmailAsync(name, email, message);

                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Message sent successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Failed to send message.');", true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Failed to send message: " + System.Web.HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
            }
        }

        private async Task<bool> SendEmailAsync(string name, string fromEmail, string message)
        {
            // Mailtrap SMTP configuration
            string smtpServer = "smtp.mailtrap.io"; // Mailtrap SMTP server
            int smtpPort = 587; // SMTP port
            string smtpUser = "b0ee5500645691"; // Replace with your Mailtrap username
            string smtpPass = "04854fca159a81"; // Replace with your Mailtrap password

            try
            {
                using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail, name),
                        Subject = $"Contact Form Submission from {name}",
                        Body = message,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(new MailAddress("optimultinfo@gmail.com", "Optimult Info"));

                    await smtpClient.SendMailAsync(mailMessage);
                }

                return true; // Return true if the email was sent successfully
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SMTP Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return false; // Return false if sending fails
            }
        }
    }
}

