using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ProjectSite.Restricted
{
    public partial class AddProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (User.Identity.IsAuthenticated)
                {
                    // Check if the user has the "Employee" role
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

        // Load clients into dropdown
        protected void LoadClients()
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Client_ID, Client_Name FROM Clienttbl"; // Adjust this query to match your client table
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                ddlClient.DataSource = cmd.ExecuteReader();
                ddlClient.DataTextField = "Client_Name";
                ddlClient.DataValueField = "Client_ID";
                ddlClient.DataBind();
            }
        }

        // Handle form submission
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"INSERT INTO Projecttbl
                                (Client_ID, Project_Name, Project_Description, Project_Start_date, Project_End_date, Project_Budget, Manager_ID) 
                                VALUES 
                                (@Client_ID, @Project_Name, @Project_Description, @Project_Start_date, @Project_End_date, @Project_Budget, @Manager_ID)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Client_ID", ddlClient.SelectedValue);
                cmd.Parameters.AddWithValue("@Project_Name", txtProjectName.Text);
                cmd.Parameters.AddWithValue("@Project_Description", txtProjectDescription.Text);
                cmd.Parameters.AddWithValue("@Project_Start_date", txtStartDate.Text);
                cmd.Parameters.AddWithValue("@Project_End_date", txtEndDate.Text);
                cmd.Parameters.AddWithValue("@Project_Budget", txtBudget.Text);
                cmd.Parameters.AddWithValue("@Manager_ID", txtManagerID.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Redirect or show a success message
            Response.Write("Project inserted successfully!");
        }
    }
}
        
    