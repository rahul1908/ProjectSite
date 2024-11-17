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

        private decimal GetClientRate(int assignmentID)
        {
            decimal clientrate;
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
                    clientrate = Convert.ToDecimal(result);
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
                        lblAssignmentBalance.Text = "Assignment Balance for this project: "+Session["assignment_balance"].ToString();
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
            if (!string.IsNullOrWhiteSpace(txtMileage.Text) && decimal.TryParse(txtMileage.Text, out decimal mileage)){
                // Example: Calculate the travel total based on mileage (assuming a fixed rate per mile).
               // double mileage;
              
                    int assignment_id = Convert.ToInt32(Session["assignment_id"].ToString());
                    decimal ratePerMile = GetClientRate(assignment_id); // Example rate, adjust as needed
                    decimal travelTotal = mileage * ratePerMile;
                    txtTravelTotal.Text = travelTotal.ToString();
            }
            else
            {
                ShowAlert("Mileage has to be a valid numeric value");
                txtMileage.Text = gvSelectTravel.SelectedRow.Cells[6].Text;
                txtTravelTotal.Text = gvSelectTravel.SelectedRow.Cells[8].Text;
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
            lblSelectDisbursement.Visible = true;
            gvDisbursements.Visible = true;
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
            travelDetailsPanel.Visible = true;
            btnUpdateTravel.Visible = true;
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

            // set expense type
            // Get the value from column 4 of the selected row (Expense Type ID or Name).
            string expenseTypeValue = gvSelectExpense.SelectedRow.Cells[4].Text;

            // Set the selected value of ddlExpenseType to match the value from the GridView.
            // Get the name of the Expense Type from the selected row in the GridView (column index may vary).
            string selectedExpenseName = gvSelectExpense.SelectedRow.Cells[4].Text.Trim();

            // Find the ListItem in the DropDownList using the displayed text (Expense_Name).
            ListItem selectedItem = ddlExpenseType.Items.FindByText(selectedExpenseName);

            if (selectedItem != null)
            {
                // Set the selected item in the DropDownList based on the matched text.
                ddlExpenseType.SelectedValue = selectedItem.Value;
            }
            else
            {
                // Optionally, clear the selection if no match is found.
                ddlExpenseType.ClearSelection();
            }

            // Get the selected row
            GridViewRow row = gvSelectExpense.SelectedRow;

            // Get the value from the first cell (Disbursement_Claim_ID)
            int expenseID = Convert.ToInt32(row.Cells[1].Text);

            // Store it in session
            Session["selected_expense_id"] = expenseID;
            expenseDetailsPanel.Visible = true;
            btnUpdateExpense.Visible = true;
        }


        protected void gvDisbursements_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected row
            GridViewRow row = gvDisbursements.SelectedRow;

            // Get the value from the first cell (Disbursement_Claim_ID)
            int disbursementId = Convert.ToInt32(row.Cells[1].Text);

            // Store it in session
            Session["disbursement_id_to_update"] = disbursementId;

            lblSelectTravel.Visible = true;
            lblSelectExpense.Visible = true;
            gvSelectTravel.Visible = true;
            gvSelectExpense.Visible = true;

            travelDetailsPanel.Visible = false;
            expenseDetailsPanel.Visible = false;
        }

        protected void btnUpdateTravel_Click(object sender, EventArgs e)
        {
            if (validTravelEntries())
            {
                decimal assignment_balance = (decimal)Session["assignment_balance"];
                decimal total_travel = Convert.ToDecimal(txtTravelTotal.Text);
                if (assignment_balance > total_travel)
                {
                    try
                    {
                        // Execute the update command
                        sqlDSUpdateTravel.Update();
                        sqlDSUpdateDisbursementValues.Update();
                        sqlDSUpdateAssignmentValues.Update();
                        assignmentBalance();

                        // Display a success message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update Successful');", true);

                        // Rebind the GridView to reflect the updated data
                        gvSelectTravel.DataSourceID = "sqlDSSelectTravel";
                        gvSelectTravel.DataBind();

                        // rebind disbursement table
                        gvDisbursements.DataSourceID = "sqlDSDisplayDisbursements";
                        gvDisbursements.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors and display a message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Update Failed: {ex.Message}');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insufficient balance for this update');", true);
                    txtMileage.Text = "";
                    txtTravelTotal.Text = "";
                }
            }
        }


        protected void btnUpdateExpense_Click(object sender, EventArgs e)
        {
            if (validExpenseEntries())
            {
                decimal assignment_balance = (decimal)Session["assignment_balance"];
                decimal total_expense = Convert.ToDecimal(txtTravelTotal.Text);
                if (assignment_balance > total_expense)
                {
                    try
                    {
                        // Execute the update command
                        sqlDSUpdateExpense.Update();
                        sqlDSUpdateDisbursementValues.Update();
                        sqlDSUpdateAssignmentValues.Update();
                        assignmentBalance();
                        // Display a success message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update Successful');", true);

                        // Rebind the GridView to reflect the updated data
                        gvSelectExpense.DataSourceID = "sqlDSSelectExpense";
                        gvSelectExpense.DataBind();

                        // rebind disbursement table
                        gvDisbursements.DataSourceID = "sqlDSDisplayDisbursements";
                        gvDisbursements.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors and display a failure message
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Update Failed: {ex.Message}');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insufficient balance for this update');", true);
                    txtMileage.Text = "";
                    txtTravelTotal.Text = "";
                }
            }
        }

        //validation
        private bool validTravelEntries()
        {
            // Validate travel date
            DateTime travelDate;
            if (!DateTime.TryParse(txtTravelDate.Text, out travelDate))
            {
                // Invalid date format
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a valid travel date.');", true);
                resetTravelDate();
                return false;
            }

            // Check if the date is in the future or more than 7 days in the past
            DateTime checkDate;
            DateTime currentDate = DateTime.Now;
            string dateText = gvSelectTravel.SelectedRow.Cells[5].Text;

            if (DateTime.TryParse(dateText, out checkDate))
            {
                currentDate = checkDate;
            }

            // Validate that the travel date is not in the future
            if (travelDate > currentDate)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel date cannot be in the future from the current record.');", true);
                resetTravelDate();
                return false;
            }

            // Validate that the travel date is not more than 7 days earlier
            if ((currentDate - travelDate).TotalDays > 7)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel date cannot be more than 7 days earlier than today.');", true);
                resetTravelDate();
                return false;
            }

            // Validate that the travel date is within the same month
            if (travelDate.Month != currentDate.Month || travelDate.Year != currentDate.Year)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel date must be within the same month as the current record.');", true);
                resetTravelDate();
                return false;
            }

            // Validate mileage
            if (string.IsNullOrWhiteSpace(txtMileage.Text) || !decimal.TryParse(txtMileage.Text, out decimal mileage))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a valid numeric mileage.');", true);
                txtMileage.Text = gvSelectTravel.SelectedRow.Cells[6].Text;
                txtTravelTotal.Text = gvSelectTravel.SelectedRow.Cells[8].Text;
                return false;
            }

            // Validate vehicle description
            if (string.IsNullOrWhiteSpace(txtVehicleDescription.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vehicle description cannot be empty.');", true);
                txtVehicleDescription.Text = gvSelectTravel.SelectedRow.Cells[4].Text;
                return false;
            }

            // Validate travel description
            if (string.IsNullOrWhiteSpace(txtTravelDescription.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel description cannot be empty.');", true);
                txtTravelDescription.Text = gvSelectTravel.SelectedRow.Cells[7].Text;
                return false;
            }

            // If all validations pass, return true
            return true;
        }



        private bool validExpenseEntries()
        {
            // Validate expense date
            DateTime expenseDate;
            if (!DateTime.TryParse(txtExpenseDate.Text, out expenseDate))
            {
                ShowAlert("Please enter a valid expense date.");
                resetExpenseDate();
                return false;
            }

            // Check if the date is in the future or more than 7 days in the past
            DateTime checkDate;
            DateTime currentDate = DateTime.Now;
            string dateText = gvSelectExpense.SelectedRow.Cells[6].Text;

            if (DateTime.TryParse(dateText, out checkDate))
            {
                currentDate = checkDate;
            }

            // Validate that the expense date is not in the future
            if (expenseDate > currentDate)
            {
                ShowAlert("Expense date cannot be in the future from the current record.");
                resetExpenseDate();
                return false;
            }

            // Validate that the expense date is not more than 7 days earlier
            if ((currentDate - expenseDate).TotalDays > 7)
            {
                ShowAlert("Expense date cannot be more than 7 days earlier than today.");
                resetExpenseDate();
                return false;
            }

            // Validate that the expense date is within the same month
            if (expenseDate.Month != currentDate.Month || expenseDate.Year != currentDate.Year)
            {
                ShowAlert("Expense date must be within the same month as the current record.");
                resetExpenseDate();
                return false;
            }

            // Validate expense amount
            if (string.IsNullOrWhiteSpace(txtExpenseAmount.Text) || !decimal.TryParse(txtExpenseAmount.Text, out decimal expenseAmount))
            {
                ShowAlert("Please enter a valid numeric expense amount.");
                txtExpenseAmount.Text = gvSelectExpense.SelectedRow.Cells[5].Text;
                return false;
            }

            // If all validations pass, return true
            return true;
        }


        private void ShowAlert(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }

        private void resetTravelDate()
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
        }

        private void resetExpenseDate()
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
        }
    }

}