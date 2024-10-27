using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;

namespace ProjectSite
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
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
                // Set up the mail message
                MailMessage mail = new MailMessage();
                mail.To.Add("info@optimult.com");
                mail.From = new MailAddress(email);
                mail.Subject = "Contact Form Submission from " + name;
                mail.Body = message;

                // Configure the SMTP client
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.yourserver.com", // Set your SMTP server
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("username", "password"), // Set your email credentials
                    EnableSsl = true
                };

                // Send the email
                smtp.Send(mail);

                // Show success alert
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Message sent successfully.');", true);
            }
            catch (Exception ex)
            {
                // Display error alert
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Failed to send message: " + System.Web.HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
            }
        }
    }
}