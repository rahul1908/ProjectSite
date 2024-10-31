using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

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
                    // Check if the user has the "Manager" role
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
                                object result = cmd.ExecuteScalar();

                                // If the result is null, the user does not exist in the Employee table
                                if (result == null)
                                {
                                    // Redirect to error page if the user is not in the Employee table
                                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                                }
                                else
                                {
                                    Session["user_id"] = (int)result;
                                    LoadEmployees();
                                    LoadProjects();

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

        private void LoadProjects()
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "SELECT Project_ID, Project_Name FROM Projecttbl Where Manager_ID = @managerid";  // Replace with your actual project table

            using (SqlConnection conn = new SqlConnection(connString))
            {
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int manid = (int)Session["user_id"];
                    cmd.Parameters.AddWithValue("@ManagerID", manid);
               
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

        protected void EmployeeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display selected employee's details
            var selectedEmployeeId = EmployeeID.SelectedValue;
            DisplayEmployeeDetails(selectedEmployeeId);
        }

        private void DisplayEmployeeDetails(string employeeId)
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "SELECT Employee_Name, Employee_Surname, Employee_Job_Title, Province FROM Employeetbl WHERE Employee_ID = @EmployeeID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        EmployeeDetailsGrid.DataSource = dt;
                        EmployeeDetailsGrid.DataBind();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "Error loading employee details: " + ex.Message;
                    }
                }
            }
        }

        protected void ProjectID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display selected project's details
            var selectedProjectId = ProjectID.SelectedValue;
            DisplayProjectDetails(selectedProjectId);
        }

        private void DisplayProjectDetails(string projectId)
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "SELECT Project_Name, Project_Start_date, Project_End_date, Project_Description, Project_Budget FROM Projecttbl WHERE Project_ID = @ProjectID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjectID", projectId);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ProjectDetailsGrid.DataSource = dt;
                        ProjectDetailsGrid.DataBind();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "Error loading project details: " + ex.Message;
                    }
                }
            }
        }

        protected void AssignProject_Click(object sender, EventArgs e)
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string checkQuery = "SELECT COUNT(*) FROM ProjectAssignmenttbl WHERE Employee_ID = @EmployeeID AND Project_ID = @ProjectID";
            string insertQuery = "INSERT INTO ProjectAssignmenttbl (Employee_ID, Project_ID, Date_Assigned, Assignment_Claim_Max, Assignment_Claim_Balance) " +
                                 "VALUES (@EmployeeID, @ProjectID, @DateAssigned, @AssignmentClaimMax, @AssignmentClaimBalance)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID.SelectedValue);
                    checkCmd.Parameters.AddWithValue("@ProjectID", ProjectID.SelectedValue);

                    try
                    {
                        conn.Open();
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            ErrorMessage.Text = "This employee is already assigned to the selected project.";
                        }
                        else
                        {
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID.SelectedValue);
                                insertCmd.Parameters.AddWithValue("@ProjectID", ProjectID.SelectedValue);
                                insertCmd.Parameters.AddWithValue("@DateAssigned", DateAssigned.Text);
                                insertCmd.Parameters.AddWithValue("@AssignmentClaimMax",AssignmentClaimMax.Text);
                                insertCmd.Parameters.AddWithValue("@AssignmentClaimBalance", AssignmentClaimMax.Text);

                                insertCmd.ExecuteNonQuery();
                                Response.Write("<script>alert('Project assigned successfully');</script>");

                                // Clear the fields after successful assignment
                                EmployeeID.SelectedIndex = 0;
                                ProjectID.SelectedIndex = 0;
                                DateAssigned.Text = string.Empty;
                                AssignmentClaimMax.Text = string.Empty;
                               
                            }
                        }
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