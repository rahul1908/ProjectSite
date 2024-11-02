using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
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

            // disabling fields on form load
            if (chbTravel.Checked == false)
            {
                disableTravel();
            }
            if (chbExpense.Checked == false)
            {
                disableExpense();
            }
        }

        private void disableTravel()
        {
            txtTravelDate.Enabled = false;
            txtTravelDescription.Enabled = false;
            txtVehicleDescription.Enabled = false;
            txtMileage.Enabled = false;
        }

        private void disableExpense()
        {
            txtExpenseAmount.Enabled = false;
            txtExpenseDate.Enabled = false;
            ddlExpenseType.Enabled = false;
        }

        private void enableTravel()
        {
            txtTravelDate.Enabled = true;
            txtTravelDescription.Enabled = true;
            txtVehicleDescription.Enabled = true;
            txtMileage.Enabled = true;
        }

        private void enableExpense()
        {
            txtExpenseAmount.Enabled = true;
            txtExpenseDate.Enabled = true;
            ddlExpenseType.Enabled = true;
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
        protected void sqlDSInsertExpense_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Proceed only if at least one checkbox is checked
            if (chbExpense.Checked || chbTravel.Checked)
            {
                bool travelInserted = false;
                bool expenseInserted = false;

                // Retrieve the assignment balance from the session
                decimal assignmentBalance = Convert.ToDecimal(Session["assignment_balance"]);

                // Check if travel entry is valid and checkbox is checked
                if (chbTravel.Checked)
                {
                    if (validTravelEntries())
                    {
                        // Validate against the assignment balance
                        if (decimal.TryParse(txtTravelTotal.Text, out decimal travelTotal))
                        {
                            if (travelTotal > assignmentBalance)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel total exceeds available balance.');", true);
                                return; // Exit if validation fails
                            }

                            sqlDSInsertTravel.Insert();
                            travelInserted = true;

                            // display methods
                            btnNewRecord.Visible = true;
                            btnNewDisbursement.Visible = true;
                            btnSubmit.Enabled = false;

                            sqlDSUpdateDisbursementValues.Update();
                            sqlDSUpdateAssignmentValues.Update();
                        }
                    }
                }

                // Check if expense entry is valid and checkbox is checked
                if (chbExpense.Checked)
                {
                    if (validExpenseEntries())
                    {
                        // Validate against the assignment balance
                        if (decimal.TryParse(txtExpenseAmount.Text, out decimal expenseAmount))
                        {
                            if (expenseAmount > assignmentBalance)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Expense amount exceeds available balance.');", true);
                                return; // Exit if validation fails
                            }

                            sqlDSInsertExpense.Insert();
                            expenseInserted = true;

                            // display methods
                            btnNewRecord.Visible = true;
                            btnNewDisbursement.Visible = true;
                            btnSubmit.Enabled = false;

                            sqlDSUpdateDisbursementValues.Update();
                            sqlDSUpdateAssignmentValues.Update();
                        }
                    }
                }

                // Prepare the message for the alert based on what was inserted
                if (travelInserted || expenseInserted)
                {
                    string message = "Insert successful!";
                    if (travelInserted && expenseInserted)
                    {
                        message = "Travel and Expense records inserted successfully!";
                    }
                    else if (travelInserted)
                    {
                        message = "Travel record inserted successfully!";
                    }
                    else if (expenseInserted)
                    {
                        message = "Expense record inserted successfully!";
                    }

                    // Register the JavaScript to show the alert
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
                }

                
            }
            else
            {
                // Optional: alert the user if no checkboxes are checked
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select at least one option (Travel or Expense).');", true);
            }
        }






        private bool validTravelEntries()
        {
            // Validate travel date
            DateTime travelDate;
            if (!DateTime.TryParse(txtTravelDate.Text, out travelDate))
            {
                // Invalid date format
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a valid travel date.');", true);
                return false;
            }

            // Check if the date is in the future or more than 7 days in the past
            DateTime currentDate = DateTime.Now;
            if (travelDate > currentDate)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel date cannot be in the future.');", true);
                return false;
            }
            if ((currentDate - travelDate).TotalDays > 7)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel date cannot be more than 7 days earlier than today.');", true);
                return false;
            }

            // Validate mileage
            if (string.IsNullOrWhiteSpace(txtMileage.Text) || !decimal.TryParse(txtMileage.Text, out decimal mileage))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a valid numeric mileage.');", true);
                return false;
            }

            // Validate vehicle description
            if (string.IsNullOrWhiteSpace(txtVehicleDescription.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vehicle description cannot be empty.');", true);
                return false;
            }

            // Validate travel description
            if (string.IsNullOrWhiteSpace(txtTravelDescription.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Travel description cannot be empty.');", true);
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
                return false;
            }

            // Check if the date is in the future or more than 7 days in the past
            DateTime currentDate = DateTime.Now;
            if (expenseDate > currentDate)
            {
                ShowAlert("Expense date cannot be in the future.");
                return false;
            }
            if ((currentDate - expenseDate).TotalDays > 7)
            {
                ShowAlert("Expense date cannot be more than 7 days earlier than today.");
                return false;
            }

            // Validate expense amount
            if (string.IsNullOrWhiteSpace(txtExpenseAmount.Text) || !decimal.TryParse(txtExpenseAmount.Text, out decimal expenseAmount))
            {
                ShowAlert("Please enter a valid numeric expense amount.");
                return false;
            }

            // If all validations pass, return true
            return true;
        }

        private void ShowAlert(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
        }



        protected void chbTravel_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTravel.Checked)
            {
                enableTravel();
            }
            else
            {
                disableTravel();
            }
        }

        protected void chbExpense_CheckedChanged(object sender, EventArgs e)
        {
            if (chbExpense.Checked)
            {
                enableExpense();
            }
            else
            {
                disableExpense();
            }
        }

        protected void txtMileage_TextChanged(object sender, EventArgs e)
        {
            // Check if the input is not empty and is a valid number
            if (!string.IsNullOrWhiteSpace(txtMileage.Text) && double.TryParse(txtMileage.Text, out double mileage))
            {
                int assignment_id = (int)Session["assignment_id"];
                double client_rate = GetClientRate(assignment_id);
                double totalTravel = client_rate * mileage;

                txtTravelTotal.Text = totalTravel.ToString("F2"); // Format to 2 decimal places, optional
            }
            else
            {
                // Show an alert to the user if input is invalid
                ShowAlert("Please enter a valid number for mileage.");
                txtMileage.Text = ""; // Clear the invalid input
            }
        }


        protected void btnNewRecord_Click(object sender, EventArgs e)
        {
            txtTravelDate.Text = "";
            txtTravelDescription.Text = "";
            txtVehicleDescription.Text = "";
            txtMileage.Text = "";
            txtTravelTotal.Text = "";

            txtExpenseAmount.Text = "";
            txtExpenseDate.Text = "";
            //ddlExpenseType.Text = "";

            btnNewRecord.Visible = false;
            chbExpense.Checked = false;
            chbTravel.Checked = false;
            btnSubmit.Enabled = true;
        }

        protected void btnNewDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Restricted/ProjectSelect.aspx");
        }
    }
   
}