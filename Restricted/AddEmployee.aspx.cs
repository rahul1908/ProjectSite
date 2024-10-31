using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Data.SqlClient;

namespace ProjectSite.Restricted
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (User.Identity.IsAuthenticated)
                {
                    // Check if the user has the "Manager" role
                    if (User.IsInRole("Manager"))
                    {
                        // Get logged-in user's email
                        string userEmail = User.Identity.Name;

                        // Define your connection string
                        string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

                        // Define the SQL query to check if the user exists in the Employee table
                        string query = "SELECT COUNT(*) FROM Employeetbl WHERE Employee_Email = @Email";

                        // Using SqlConnection and SqlCommand to execute the query
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                // Add the email parameter to the query
                                cmd.Parameters.AddWithValue("@Email", userEmail);

                                // Open the connection
                                conn.Open();

                                // Execute the query and get the result
                                int count = (int)cmd.ExecuteScalar();

                                // If the count is 0, the user does not exist in the Employee table
                                if (count == 0)
                                {
                                    // Redirect to error page if the user is not in the Employee table
                                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                                }
                              
                            }
                        }
                    }
                    else
                    {
                        // If the user is logged in but does not have the "Manager" role, redirect to access denied page
                        Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                    }
                }
                else
                {
                    // Redirect to login page if the user is not authenticated
                    Response.Redirect("~/Account/Login.aspx");
                }


            }
        }





        protected void CreateEmployee_Click(object sender, EventArgs e)
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            // SQL query to insert data into Employee table
            string employeeQuery = "INSERT INTO Employeetbl " +
                "(RSA_ID, Employee_Name, Employee_Surname, Employee_Job_Title, Employee_Mobile_Number, Address_Line1, Address_Line2, Suburb, City, Province, Postal_Code, Employee_Email, Employee_Password) " +
                "VALUES (@RSA_ID, @EmployeeName, @EmployeeSurname, @EmployeeJobTitle, @EmployeeMobileNumber, @AddressLine1, @AddressLine2, @Suburb, @City, @Province, @PostalCode, @EmployeeEmail, @EmployeePassword)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert into Employee table
                    using (SqlCommand cmdEmployee = new SqlCommand(employeeQuery, conn, transaction))
                    {
                        cmdEmployee.Parameters.AddWithValue("@RSA_ID", EmployeeRSAID.Text);
                        cmdEmployee.Parameters.AddWithValue("@EmployeeName", EmployeeName.Text);
                        cmdEmployee.Parameters.AddWithValue("@EmployeeSurname", EmployeeSurname.Text);
                        cmdEmployee.Parameters.AddWithValue("@EmployeeJobTitle", EmployeeJobTitle.Text);
                        cmdEmployee.Parameters.AddWithValue("@EmployeeMobileNumber", EmployeeMobileNumber.Text);
                        cmdEmployee.Parameters.AddWithValue("@AddressLine1", AddressLine1.Text);
                        cmdEmployee.Parameters.AddWithValue("@AddressLine2", AddressLine2.Text);
                        cmdEmployee.Parameters.AddWithValue("@Suburb", Suburb.Text);
                        cmdEmployee.Parameters.AddWithValue("@City", City.Text);
                        cmdEmployee.Parameters.AddWithValue("@Province", Province.Text);
                        cmdEmployee.Parameters.AddWithValue("@PostalCode", PostalCode.Text);
                        cmdEmployee.Parameters.AddWithValue("@EmployeeEmail", EmployeeEmail.Text);
                        cmdEmployee.Parameters.AddWithValue("@EmployeePassword", EmployeePassword.Text);  // Consider hashing this

                        cmdEmployee.ExecuteNonQuery();
                    }

                    // Now add the user to AspNetUsers using ASP.NET Identity
                    var userStore = new UserStore<IdentityUser>(new IdentityDbContext());
                    var userManager = new UserManager<IdentityUser>(userStore);

                    var user = new IdentityUser
                    {
                        UserName = EmployeeEmail.Text,
                        Email = EmployeeEmail.Text,
                        LockoutEnabled = true
                    };

                    // Hash the password and create the user
                    var result = userManager.Create(user, EmployeePassword.Text);

                    if (result.Succeeded)
                    {
                        // Check if the "Employee" role exists, if not, create it
                        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));

                        if (!roleManager.RoleExists("Employee"))
                        {
                            var roleResult = roleManager.Create(new IdentityRole("Employee"));
                            if (!roleResult.Succeeded)
                            {
                                transaction.Rollback();
                                ErrorMessage.Visible = true;
                                ErrorMessage.Text = "Error creating 'Employee' role.";
                                return;
                            }
                        }

                        // Add the new user to the "Employee" role
                        userManager.AddToRole(user.Id, "Employee");

                        transaction.Commit();
                        Response.Write("<script>alert('Employee added and assigned to Employee role successfully!');</script>");
                    }
                    else
                    {
                        // Handle errors from user creation
                        transaction.Rollback();
                        ErrorMessage.Visible = true;
                        ErrorMessage.Text = "Error: " + string.Join(", ", result.Errors);
                    }
                }
                catch (Exception ex)
                {
                    // Rollback transaction in case of error
                    transaction.Rollback();
                    ErrorMessage.Visible = true;
                    ErrorMessage.Text = "Error: " + ex.Message;
                }
            }
        }

    }
}