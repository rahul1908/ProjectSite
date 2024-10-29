using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ProjectSite

{
    public partial class ViewClaims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (User.Identity.IsAuthenticated)
                {
                    // Check if the user has the "Employee" role
                    if (User.IsInRole("Employee"))
                    {
                        // Get logged-in user's email
                        string userEmail = User.Identity.Name;

                        // Define your connection string
                        string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

                        // Define the SQL query to check if the user exists in the Employee table
                        string query = "SELECT Employee_ID FROM Employeetbl WHERE Employee_Email = @Email";

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
                                int id = (int)cmd.ExecuteScalar();


                                // If the count is 0, the user does not exist in the Employee table
                                if (id == null)

                                {
                                    // Redirect to error page if the user is not in the Employee table
                                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                                }
                                else
                                {
                                    Session["user_id"] = id;
                                    Session["employee_id"] = id;

                                    LoadClaims();


                                }

                            }
                        }


                    }
                    else
                    {
                        // If the user is logged in but does not have the "Employee" role, redirect to access denied page
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
        private void LoadClaims()
        {
            // Define the connection string (fetch from Web.config for security)
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            // Get the employee ID of the logged-in user
            int employeeID = (int)Session["user_id"]; // Implement this method based on your authentication logic

            // SQL query to retrieve claims for the logged-in employee
            string query = @"SELECT 
                                Disbursement_Claim_ID,
                                Assignment_ID,
                                Disbursement_Travel_Total,
                                Disbursement_Expense_Total,
                                Disbursement_Total_Claim,
                                Disbursement_Date,
                                Manager_ID,
                                Disbursement_Approved
                             FROM 
                                DisbursementClaimtbl
                             WHERE 
                                Employee_ID = @EmployeeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", 1);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable claimsTable = new DataTable();
                        adapter.Fill(claimsTable);

                        // Bind the data to the GridView
                        ClaimsGridView.DataSource = claimsTable;
                        ClaimsGridView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or show an error message
                        Console.WriteLine("Error loading claims: " + ex.Message);
                    }
                }
            }
        }
    }
}