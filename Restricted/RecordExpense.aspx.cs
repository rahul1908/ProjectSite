using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ProjectSite.Restricted
{
    public partial class RecordExpense : System.Web.UI.Page
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

                        // Check if the session contains the project ID and project name
                        if (Session["project_id"] != null && Session["selected_project_name"] != null)
                        {
                            // Retrieve the project name and display it
                            string projectName = Session["selected_project_name"].ToString();
                            LabelProjectName.Text = projectName;

                            // If you need the project ID for further processing, you can also retrieve it
                            string projectId = Session["project_id"].ToString();
                            // You can use projectId as needed in your logic
                        }
                        else
                        {
                            // Redirect back to the ProjectSelect page if no project is selected
                            Response.Redirect("~/Restricted/ProjectSelect.aspx");
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
    }
}
