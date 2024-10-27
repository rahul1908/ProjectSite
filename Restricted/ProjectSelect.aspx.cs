using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ProjectSite.Restricted
{
    public partial class ProjectSelect : System.Web.UI.Page
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
                                    //System.Diagnostics.Debug.WriteLine(Session["user_id"]);
                                    //Label2.Text = Session["user_id"].ToString();

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
            LoadProjects("Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd");
        }
        private void LoadProjects(string connectionString)
        {
            //// Using SqlConnection to fetch project details
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand("SELECT DISTINCT Projecttbl.Project_ID, Projecttbl.Project_Name FROM Projecttbl INNER JOIN ProjectAssignmenttbl ON Projecttbl.Project_ID = ProjectAssignmenttbl.Project_ID WHERE ProjectAssignmenttbl.Employee_ID = @employeeID", connection);
            //    command.Parameters.AddWithValue("@employeeID", Session["user_id"]);

            //    SqlDataReader reader = command.ExecuteReader();

            //    // Bind the project data to the dropdown list
            //    ddlProjects.DataSource = reader;
            //    ddlProjects.DataTextField = "Project_Name";
            //    ddlProjects.DataValueField = "Project_ID";
            //    ddlProjects.DataBind();

            //    reader.Close();
            //}
        }
        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle project selection changes here
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Handle form submission here
            Session["project_id"] = ddlProjects.SelectedValue;

            string selectedProjectName = ddlProjects.SelectedItem.Text;
            Session["selected_project_name"] = selectedProjectName;

            // Redirect to the RecordExpense page
            Response.Redirect("~/Restricted/RecordExpense.aspx");
            // Label1.Text = Session["project_id"].ToString();
        }


    }
}