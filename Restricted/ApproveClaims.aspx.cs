using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;


namespace ProjectSite.Restricted
{
    public partial class ApproveClaims : System.Web.UI.Page
    {
        string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
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
                                    // Store the Employee ID in the session
                                    Session["user_id"] = (int)result;

                                    // Load projects and employees for the manager
                                  
                                    LoadProjects();
                                }
                            }
                        }
                    }
                    else
                    {
                        // Redirect to access denied page if the user does not have the "Manager" role
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

        private void LoadProjects()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Project_ID, Project_Name FROM Projecttbl";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    ProjectDropDown.DataSource = reader;
                    ProjectDropDown.DataTextField = "Project_Name";
                    ProjectDropDown.DataValueField = "Project_ID";
                    ProjectDropDown.DataBind();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading projects: " + ex.Message);
                }
            }
        }

        protected void ProjectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectId = int.Parse(ProjectDropDown.SelectedValue);
            LoadClaims(projectId);
        }

        private void LoadClaims(int projectId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT dc.Disbursement_Claim_ID, dc.Assignment_ID, dc.Disbursement_Travel_Total, 
                                 dc.Disbursement_Expense_Total, dc.Disbursement_Total_Claim, dc.Disbursement_Date 
                                 FROM DisbursementClaimtbl dc
                                 INNER JOIN ProjectAssignmenttbl pa ON dc.Assignment_ID = pa.Assignment_ID
                                 WHERE pa.Project_ID = @projectId AND dc.Disbursement_Approved='Not Approved'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@projectId", projectId);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable claimsTable = new DataTable();
                        adapter.Fill(claimsTable);

                        ClaimsGridView.DataSource = claimsTable;
                        ClaimsGridView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading claims: " + ex.Message);
                    }
                }
            }
        }

        protected void ClaimsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int claimId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
               
                bool isApproved = e.CommandName == "Approve";

                UpdateClaimApproval(claimId, isApproved);
            }
            else if (e.CommandName == "ViewDetails")
            {
                LoadClaimDetails(claimId);
            }

        }
        private void LoadClaimDetails(int claimId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve Employee Information
                string employeeQuery = @"SELECT e.Employee_Name, e.Employee_Job_Title, e.Employee_Mobile_Number
                                         FROM EmployeeTbl e
                                         INNER JOIN ProjectAssignmenttbl pa ON e.Employee_ID = pa.Employee_ID
                                         INNER JOIN DisbursementClaimtbl dc ON pa.Assignment_ID = dc.Assignment_ID
                                         WHERE dc.Disbursement_Claim_ID = @claimId";

                using (SqlCommand command = new SqlCommand(employeeQuery, connection))
                {
                    command.Parameters.AddWithValue("@claimId", claimId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        EmployeeNameLabel.Text = "Name: " + reader["Employee_Name"].ToString();
                        EmployeeJobTitleLabel.Text = "Job Title: " + reader["Employee_Job_Title"].ToString();
                        EmployeeContactLabel.Text = "Contact: " + reader["Employee_Mobile_Number"].ToString();
                    }
                    reader.Close();
                }

                // Retrieve Travel Details
                string travelQuery = @"SELECT 
                        Travel_ID,
                        Travel_Description,
                        Travel_Date,
                        Travel_Mileage,
                        Travel_Total
                     FROM 
                        Traveltbl
                     WHERE 
                        Disbursement_Claim_ID = @claimId";
                SqlDataAdapter travelAdapter = new SqlDataAdapter(new SqlCommand(travelQuery, connection));
                travelAdapter.SelectCommand.Parameters.AddWithValue("@claimId", claimId);
                DataTable travelTable = new DataTable();
                travelAdapter.Fill(travelTable);
                TravelDetailsGridView.DataSource = travelTable;
                TravelDetailsGridView.DataBind();

                // Retrieve Expense Details
                string expenseQuery = @"SELECT 
                        e.Expense_ID,
                        e.Expense_Total,
                        e.Expense_Date,
                        et.Expense_Name
                     FROM 
                        Expensetbl e
                     INNER JOIN 
                        ExpenseTypetbl et ON e.Expense_Type_ID = et.Expense_Type_ID
                     WHERE 
                        e.Disbursement_Claim_ID = @claimId";
                SqlDataAdapter expenseAdapter = new SqlDataAdapter(new SqlCommand(expenseQuery, connection));
                expenseAdapter.SelectCommand.Parameters.AddWithValue("@claimId", claimId);
                DataTable expenseTable = new DataTable();
                expenseAdapter.Fill(expenseTable);
                ExpenseDetailsGridView.DataSource = expenseTable;
                ExpenseDetailsGridView.DataBind();
            }

            ClaimDetailsPanel.Visible = true;
        }

        private void UpdateClaimApproval(int claimId, bool isApproved)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE DisbursementClaimtbl 
                                 SET Disbursement_Approved = @approvalStatus 
                                 WHERE Disbursement_Claim_ID = @claimId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@approvalStatus", isApproved ? "Approved" : "Rejected");
                    command.Parameters.AddWithValue("@claimId", claimId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error updating claim approval: " + ex.Message);
                    }
                }
            }

            // Refresh claims list after approval
            LoadClaims(int.Parse(ProjectDropDown.SelectedValue));
        }
    }
}