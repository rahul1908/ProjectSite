using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectSite.Restricted
{
    public partial class UpdateDisbursements : System.Web.UI.Page
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
            //LoadProjects("Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd");
        }

        private double GetClientRate(int assignmentID)
        {
            double clientrate;
            string connectionstring = "Data Source=146.230.177.46;Initial Catalog=G8Wst2024;User ID=G8Wst2024;password=09ujd";
            string query = @"SELECT clienttbl.Client_Rates 
FROM Clienttbl INNER JOIN
                         Projecttbl ON Clienttbl.Client_ID = Projecttbl.Client_ID INNER JOIN
                         ProjectAssignmenttbl ON Projecttbl.Project_ID = ProjectAssignmenttbl.Project_ID
WHERE(ProjectAssignmenttbl.Assignment_ID = @assignmentID)";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand Cmd = new SqlCommand(query, connection))
                {
                    Cmd.Parameters.Add(new SqlParameter("@assignmentID", SqlDbType.Int));
                    Cmd.Parameters["@assignmentID"].Value = assignmentID;
                    connection.Open();
                    object result = Cmd.ExecuteScalar();
                    clientrate = Convert.ToDouble(result);
                }
            }

            return clientrate;
        }


        private void assignmentBalance()
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            // get manager id
            string query = @"SELECT Assignment_Claim_Balance FROM ProjectAssignmenttbl WHERE Assignment_ID = @id

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
                        Session["assignment_balance"] = assignmentBalance;
                    }
                    else
                    {
                        //Response.Redirect("~/Restricted/ProjectSelect.aspx");

                    }
                }
            }
        }
        protected void txtMileage_TextChanged(object sender, EventArgs e)
        {
            // Example: Calculate the travel total based on mileage (assuming a fixed rate per mile).
            double mileage;
            if (double.TryParse(txtMileage.Text, out mileage))
            {
                double ratePerMile = 0.5; // Example rate, adjust as needed
                double travelTotal = mileage * ratePerMile;
                txtTravelTotal.Text = travelTotal.ToString();
            }
            else
            {
                txtTravelTotal.Text = "0.00";
            }
            

        }

        protected void btnUpdateDisbursement_Click(object sender, EventArgs e)
        {
            //code to go
        }

        protected void ddlProjectSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            // Handle form submission here
            Session["project_id"] = ddlProjectSelection.SelectedValue;

            string selectedProjectName = ddlProjectSelection.SelectedItem.Text;
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
                                    //lblAssignID.Text = "Assignment ID is : " + assignmentId.ToString();
                                    Session["assignment_id"] = assignmentId;
                                }
                                else
                                {
                                    //lblAssignID.Text = "No assignment found for this project.";
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
                        managerCmd.Parameters.AddWithValue("@ID", ddlProjectSelection.SelectedValue);

                        // Open the connection for the assignment query
                        conn.Open();

                        // Execute the assignment query and get the result
                        object managerID = managerCmd.ExecuteScalar();

                        // Check if the assignment ID is not null
                        if (managerID != null)
                        {
                            //lblManagerID.Text = "Manager ID is : " + managerID.ToString();
                            Session["manager_id"] = managerID;
                        }
                        else
                        {
                           // lblManagerID.Text = "No assignment found for this project.";
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

        protected void btnConfirmSelection_Click(object sender, EventArgs e)
        {
          
                // Get the selected row
                GridViewRow row = gvDisbursements.SelectedRow;

                // Get the value from the first cell (Disbursement_Claim_ID)
                int disbursementId = Convert.ToInt32(row.Cells[1].Text);

                // Store it in session
                Session["disbursement_id_to_update"] = disbursementId;

                // Optional: Display a message
               /// lblMessage.Text = "Selected Disbursement ID: " + disbursementId;  

        }

        protected void gvSelectTravel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Safely retrieve and parse the date
            DateTime travelDate;
            string dateText = gvSelectTravel.SelectedRow.Cells[5].Text;

            if (DateTime.TryParse(dateText, out travelDate))
            {
                // Format the date as 'yyyy-MM-dd' for TextMode="Date"
                txtTravelDate.Text = travelDate.ToString("yyyy-MM-dd");
            }
            else
            {
                // Handle invalid date format
                txtTravelDate.Text = "";
            }

            // Retrieve other fields safely
            txtMileage.Text = gvSelectTravel.SelectedRow.Cells[6].Text;
            txtTravelTotal.Text = gvSelectTravel.SelectedRow.Cells[8].Text;
            txtVehicleDescription.Text = gvSelectTravel.SelectedRow.Cells[4].Text;
            txtTravelDescription.Text = gvSelectTravel.SelectedRow.Cells[7].Text;

            // Get the selected row
            GridViewRow row = gvSelectTravel.SelectedRow;

            // Get the value from the first cell (Disbursement_Claim_ID)
            int travelID = Convert.ToInt32(row.Cells[1].Text);

            // Store it in session
            Session["selected_travel_id"] = travelID;
        }


        protected void gvSelectExpense_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Safely retrieve and parse the date from the selected row
            DateTime expenseDate;
            string dateText = gvSelectExpense.SelectedRow.Cells[6].Text;

            if (DateTime.TryParse(dateText, out expenseDate))
            {
                // Set the date in the correct format for TextMode="Date"
                txtExpenseDate.Text = expenseDate.ToString("yyyy-MM-dd");
            }
            else
            {
                // Handle invalid date format
                txtExpenseDate.Text = "";
            }

            // Set the expense amount
            txtExpenseAmount.Text = gvSelectExpense.SelectedRow.Cells[5].Text;

            // Get the selected row
            GridViewRow row = gvSelectExpense.SelectedRow;

            // Get the value from the first cell (Disbursement_Claim_ID)
            int expenseID = Convert.ToInt32(row.Cells[1].Text);

            // Store it in session
            Session["selected_expense_id"] = expenseID;
        }


        protected void gvDisbursements_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected row
            GridViewRow row = gvDisbursements.SelectedRow;

            // Get the value from the first cell (Disbursement_Claim_ID)
            int disbursementId = Convert.ToInt32(row.Cells[1].Text);

            // Store it in session
            Session["disbursement_id_to_update"] = disbursementId;
        }
    }
}