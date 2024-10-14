using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

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
                                // Redirect to login page or display an error if the user does not exist
                                Response.Redirect("~/Account/Login.aspx");
                            }
                        }
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