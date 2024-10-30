using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using ProjectSite.Models;
using System.Web.UI;
using System.Data.SqlClient;

namespace ProjectSite.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected string SuccessMessage { get; private set; }
        public bool HasPhoneNumber { get; private set; }
        public bool TwoFactorEnabled { get; private set; }
        public bool TwoFactorBrowserRemembered { get; private set; }
        public int LoginsCount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            HasPhoneNumber = String.IsNullOrEmpty(manager.GetPhoneNumber(User.Identity.GetUserId()));
            TwoFactorEnabled = manager.GetTwoFactorEnabled(User.Identity.GetUserId());
            LoginsCount = manager.GetLogins(User.Identity.GetUserId()).Count;

            if (!IsPostBack)
            {
                // Load Employee Data
                LoadEmployeeData();

                // Existing logic for password and success messages
                if (HasPassword(manager))
                {
                    ChangePassword.Visible = true;
                }
                else
                {
                    CreatePassword.Visible = true;
                    ChangePassword.Visible = false;
                }

                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    Form.Action = ResolveUrl("~/Account/Manage");
                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Your password has been changed."
                        : message == "SetPwdSuccess" ? "Your password has been set."
                        : message == "RemoveLoginSuccess" ? "The account was removed."
                        : message == "AddPhoneNumberSuccess" ? "Phone number has been added"
                        : message == "RemovePhoneNumberSuccess" ? "Phone number was removed"
                        : message == "UpdateSuccess" ? "Your account details have been updated successfully."
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }
        }
        private void LoadEmployeeData()
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            // Retrieve user email from User.Identity
            string userEmail = User.Identity.GetUserName(); // Assuming this returns the email

            // Define the SQL query to check if the user exists in the Employee table
            string Manager_query = "SELECT Employee_ID FROM Employeetbl WHERE Employee_Email = @Email";

            int employeeId = -1; // Initialize employeeId

            // Using SqlConnection and SqlCommand to execute the query
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Manager_query, conn))
                {
                    // Add the email parameter to the query
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    // Open the connection
                    conn.Open();

                    // Execute the query and get the result
                    object result = cmd.ExecuteScalar();

                    // Check if the result is null
                    if (result != null)
                    {
                        // Store the Employee_ID in session variables
                        employeeId = (int)result;
                        Session["user_id"] = employeeId;
                        Session["employee_id"] = employeeId;

                        // Now load the employee details using the employeeId
                        LoadEmployeeDetails(employeeId);
                    }
                    else
                    {
                        // Handle the case where no employee is found (optional)
                    }
                }
            }
        }
        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }


        private void LoadEmployeeDetails(int employeeId)
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT RSA_ID, Employee_Name, Employee_Surname, Employee_Job_Title, Employee_Mobile_Number, Address_Line1, Address_Line2, Suburb, City, Province, Postal_Code, Employee_Email FROM Employeetbl WHERE Employee_ID = @Employee_ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Employee_ID", employeeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            txtRSAID.Text = reader["RSA_ID"].ToString();
                            txtEmployeeName.Text = reader["Employee_Name"].ToString();
                            txtEmployeeSurname.Text = reader["Employee_Surname"].ToString();
                            txtJobTitle.Text = reader["Employee_Job_Title"].ToString();
                            txtMobileNumber.Text = reader["Employee_Mobile_Number"].ToString();
                            txtAddressLine1.Text = reader["Address_Line1"].ToString();
                            txtAddressLine2.Text = reader["Address_Line2"].ToString();
                            txtSuburb.Text = reader["Suburb"].ToString();
                            txtCity.Text = reader["City"].ToString();
                            txtProvince.Text = reader["Province"].ToString();
                            txtPostalCode.Text = reader["Postal_Code"].ToString();
                            txtEmail.Text = reader["Employee_Email"].ToString();
                        }
                    }
                }
            }
        }


        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // Retrieve the Employee ID from the session variable
            int employeeId = Convert.ToInt32(Session["employee_id"]); // Use the session variable you set earlier

            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd"; // Use your connection string from Web.config

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "UPDATE Employeetbl SET Employee_Name = @EmployeeName, Employee_Surname = @EmployeeSurname, Employee_Job_Title = @JobTitle, Employee_Mobile_Number = @MobileNumber, Address_Line1 = @Address1, Address_Line2 = @Address2, Suburb = @Suburb, City = @City, Province = @Province, Postal_Code = @PostalCode, Employee_Email = @Email WHERE Employee_ID = @EmployeeID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Use the employeeId variable here
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", txtEmployeeName.Text);
                    cmd.Parameters.AddWithValue("@EmployeeSurname", txtEmployeeSurname.Text);
                    cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                    cmd.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text);
                    cmd.Parameters.AddWithValue("@Address1", txtAddressLine1.Text);
                    cmd.Parameters.AddWithValue("@Address2", txtAddressLine2.Text);
                    cmd.Parameters.AddWithValue("@Suburb", txtSuburb.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@Province", txtProvince.Text);
                    cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Details updated successfully!');", true);
                    
                    }
                
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Failed to update details.');", true);
                    }
                }
            }
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // Remove phonenumber from user
        protected void RemovePhone_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var result = manager.SetPhoneNumber(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return;
            }
            var user = manager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess");
            }
        }

        // DisableTwoFactorAuthentication
        protected void TwoFactorDisable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), false);

            Response.Redirect("/Account/Manage");
        }

        //EnableTwoFactorAuthentication 
        protected void TwoFactorEnable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), true);

            Response.Redirect("/Account/Manage");
        }
    }
}