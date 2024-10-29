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
                        // Get the logged-in user's email
                        string userEmail = User.Identity.Name;

                        // Define your connection string
                        string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

                        // Define the SQL query to check if the user exists in the Employee table
                        string query = "SELECT Employee_ID FROM Employeetbl WHERE Employee_Email = @Email";

                        int employeeId = -1; // Initialize employeeId

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
                                object result = cmd.ExecuteScalar();

                                // Check if the result is null
                                if (result == null)
                                {
                                    // Redirect to error page if the user is not in the Employee table
                                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                                }
                                else
                                {
                                    // Store the Employee_ID in session variables
                                    employeeId = (int)result;
                                    Session["user_id"] = employeeId;
                                    Session["employee_id"] = employeeId;
                                }
                            }
                        }

                        // Now that you have the employee ID in session, you can proceed to use it in your assignment query
                        // Assuming you have the project ID in the session already
                        if (Session["project_id"] != null)
                        {
                            string projectId = Session["project_id"].ToString();

                            // Your logic to use the employeeId and projectId for further processing
                            // For example, retrieving the assignment ID:
                            string assignmentQuery = @"
                            SELECT ProjectAssignmenttbl.Assignment_ID 
                            FROM ProjectAssignmenttbl 
                            WHERE ProjectAssignmenttbl.Employee_ID = @EmployeeID 
                            AND ProjectAssignmenttbl.Project_ID = @ProjectID";

                            using (SqlConnection conn = new SqlConnection(connectionString)) // New connection for the assignment query
                            {
                                using (SqlCommand assignmentCmd = new SqlCommand(assignmentQuery, conn))
                                {
                                    // Add parameters to the assignment query
                                    assignmentCmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                                    assignmentCmd.Parameters.AddWithValue("@ProjectID", projectId);

                                    // Open the connection for the assignment query
                                    conn.Open();

                                    // Execute the assignment query and get the result
                                    object assignmentId = assignmentCmd.ExecuteScalar();

                                    // Check if the assignment ID is not null
                                    if (assignmentId != null)
                                    {
                                        lblassignID.Text = assignmentId.ToString();
                                    }
                                    else
                                    {
                                        lblassignID.Text = "No assignment found for this project.";
                                        Response.Redirect("~/Restricted/ProjectSelect.aspx");

                                    }
                                }
                            }
                        }
                        else
                        {
                            // Handle case where no project ID is found in the session
                            Response.Redirect("~/Restricted/ProjectSelect.aspx");
                        }
                    }
                    else
                    {
                        // Redirect to access denied page if not an Employee
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
        protected void sqlDSInsertExpense_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
   
}