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
                                    Session["employee_id"] = id;
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
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            // Handle form submission here
            Session["project_id"] = ddlProjects.SelectedValue;

            string selectedProjectName = ddlProjects.SelectedItem.Text;
            Session["selected_project_name"] = selectedProjectName;
            // Check if user is logged in
            if (User.Identity.IsAuthenticated)
            {
                // Check if the user has the "Employee" role
                if (User.IsInRole("Employee"))
                {
                    // Get the logged-in user's email
                    string userEmail = User.Identity.Name;

                    // Define your connection string


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
                                    lblAssignID.Text = "Assignment ID is : " + assignmentId.ToString();
                                    Session["assignment_id"] = assignmentId;
                                }
                                else
                                {
                                    lblAssignID.Text = "No assignment found for this project.";
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

                // get manager id
                string query = @"
                            SELECT Manager_ID 
                            FROM Projecttbl 
                            WHERE Project_ID = @ID 
                            ";
                using (SqlConnection conn = new SqlConnection(connectionString)) // New connection for the assignment query
                {
                    using (SqlCommand managerCmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to the assignment query
                        managerCmd.Parameters.AddWithValue("@ID", ddlProjects.SelectedValue);

                        // Open the connection for the assignment query
                        conn.Open();

                        // Execute the assignment query and get the result
                        object managerID = managerCmd.ExecuteScalar();

                        // Check if the assignment ID is not null
                        if (managerID != null)
                        {
                            lblManagerID.Text = "Manager ID is : " + managerID.ToString();
                            Session["manager_id"] = managerID;
                        }
                        else
                        {
                            lblManagerID.Text = "No assignment found for this project.";
                            Response.Redirect("~/Restricted/ProjectSelect.aspx");

                        }
                    }
                }
            }
            else
            {
                // Redirect to login page if the user is not authenticated
                Response.Redirect("~/Account/Login.aspx");
            }

            assignmentBalance();


        }

        private void assignmentBalance()
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            // get manager id
            string query = @"
                            SELECT 
    Disbursement_Total_Claim - (Disbursement_Travel_Total + Disbursement_Expense_Total) AS NetClaim
FROM 
    DisbursementClaimtbl
WHERE 
    Assignment_ID = @id 
    AND Disbursement_Claim_ID = (SELECT MAX(Disbursement_Claim_ID) 
                                 FROM DisbursementClaimtbl 
                                 WHERE Assignment_ID = @id)

                            ";
            using (SqlConnection conn = new SqlConnection(connectionString)) // New connection for the assignment query
            {
                using (SqlCommand assignmentBalanceCmd = new SqlCommand(query, conn))
                {
                    // Add parameters to the assignment query
                    assignmentBalanceCmd.Parameters.AddWithValue("@id", Session["assignment_id"]);

                    // Open the connection for the assignment query
                    conn.Open();

                    // Execute the assignment query and get the result
                    object assignmentBalance = assignmentBalanceCmd.ExecuteScalar();

                    // Check if the assignment ID is not null
                    if (assignmentBalance != null)
                    {
                        Label3.Text = "Amount Available for this project : " + assignmentBalance.ToString();
                        Session["assignment_balance"] = assignmentBalance;
                    }
                    else
                    {
                        Response.Redirect("~/Restricted/ProjectSelect.aspx");

                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Handle form submission here
            Session["project_id"] = ddlProjects.SelectedValue;

            string selectedProjectName = ddlProjects.SelectedItem.Text;
            Session["selected_project_name"] = selectedProjectName;

            sqlDSInsertDisbursement.Insert();
            setDisbursementClaimID();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Insert was successful!');", true);


            // Redirect to the RecordExpense page
            Response.Redirect("~/Restricted/RecordExpense.aspx");
            // Label1.Text = Session["project_id"].ToString();
        }

        private void setDisbursementClaimID()
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "SELECT        MAX(Disbursement_Claim_ID) AS Expr1 FROM DisbursementClaimtbl";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // Execute the query and get the result
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value) // Check if there is a result
                {
                    // Store the result in a session variable
                    Session["MaxDisbursementClaimID"] = (int)result; // Cast to int if the field is int
                }
                else
                {
                    // Handle case where there is no result (e.g., table is empty)
                    Session["MaxDisbursementClaimID"] = null;
                }
            }
        } 

    }
}