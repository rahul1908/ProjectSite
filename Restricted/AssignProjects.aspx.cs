using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ProjectSite.Restricted
{
    public partial class AssignProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
                LoadProjects();
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


            }

        private void LoadEmployees()
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "SELECT Employee_ID, Employee_Name FROM Employeetbl";  // Replace with your actual employee table

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        EmployeeID.DataSource = reader;
                        EmployeeID.DataValueField = "Employee_ID";
                        EmployeeID.DataTextField = "Employee_Name";
                        EmployeeID.DataBind();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "Error loading employees: " + ex.Message;
                    }
                }
            }
            EmployeeID.Items.Insert(0, new ListItem("--Select Employee--", ""));
        }

        // Load projects into dropdown
        private void LoadProjects()
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "SELECT Project_ID, Project_Name FROM Projecttbl";  // Replace with your actual project table

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        ProjectID.DataSource = reader;
                        ProjectID.DataValueField = "Project_ID";
                        ProjectID.DataTextField = "Project_Name";
                        ProjectID.DataBind();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "Error loading projects: " + ex.Message;
                    }
                }
            }
            ProjectID.Items.Insert(0, new ListItem("--Select Project--", ""));
        }

        // Assign project to employee
        protected void AssignProject_Click(object sender, EventArgs e)
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "INSERT INTO ProjectAssignmenttbl (Employee_ID, Project_ID, Date_Assigned, Assignment_Claim_Max, Assignment_Claim_Balance) " +
                           "VALUES (@EmployeeID, @ProjectID, @DateAssigned, @AssignmentClaimMax, @AssignmentClaimBalance)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProjectID", ProjectID.SelectedValue);
                    cmd.Parameters.AddWithValue("@DateAssigned", DateAssigned.Text);
                    cmd.Parameters.AddWithValue("@AssignmentClaimMax", AssignmentClaimMax.Text);
                    cmd.Parameters.AddWithValue("@AssignmentClaimBalance", AssignmentClaimBalance.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Project assigned successfully');</script>");
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "Error assigning project: " + ex.Message;
                    }
                }
            }
        }
        }
}