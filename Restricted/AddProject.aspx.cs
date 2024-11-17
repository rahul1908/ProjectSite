using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ProjectSite.Restricted
{
    public partial class AddProject : System.Web.UI.Page
    {
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
                                    // Redirect to error page if the user is not in the Employee table
                                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                                }
                                else
                                {
                                    // Load clients into the dropdown list
                                    LoadClients();
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

        protected void LoadClients()
        {
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT Client_ID, Client_Name FROM Clienttbl"; // Adjust this query to match your client table
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                ddlClient.DataSource = cmd.ExecuteReader();
                ddlClient.DataTextField = "Client_Name";
                ddlClient.DataValueField = "Client_ID";
                ddlClient.DataBind();

                // Optionally, add a default item (e.g., "Select a Client")
                ddlClient.Items.Insert(0, new ListItem("--Select a Client--", "0"));
            }
        }

        // Handle form submission
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Check if any fields are empty
            if (string.IsNullOrWhiteSpace(txtProjectName.Text) ||
                string.IsNullOrWhiteSpace(txtProjectDescription.Text) ||
                string.IsNullOrWhiteSpace(txtStartDate.Text) ||
                string.IsNullOrWhiteSpace(txtEndDate.Text) ||
                string.IsNullOrWhiteSpace(txtBudget.Text) ||
                ddlClient.SelectedValue == "")
            {
                litErrorMessage.Text = "Please fill in all fields.";
                return; // Exit the method if any field is empty
            }

            // Check if Start Date is valid
            DateTime startDate;
            if (!DateTime.TryParse(txtStartDate.Text, out startDate))
            {
                litErrorMessage.Text = "Invalid start date format.";
                return; // Exit the method if the date format is incorrect
            }

            // Check if End Date is valid
            DateTime endDate;
            if (!DateTime.TryParse(txtEndDate.Text, out endDate))
            {
                litErrorMessage.Text = "Invalid end date format.";
                return; // Exit the method if the date format is incorrect
            }

            // Check if Start Date is before End Date
            if (startDate > endDate)
            {
                litErrorMessage.Text = "Start date cannot be later than end date.";
                return; // Exit the method if Start Date is later than End Date
            }

         

            // Check if Budget is a valid number
            decimal budget;
            if (!decimal.TryParse(txtBudget.Text, out budget))
            {
                litErrorMessage.Text = "Invalid budget format.";
                return; // Exit the method if the budget is not a valid number
            }

            // Define connection string
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"INSERT INTO Projecttbl
                        (Client_ID, Project_Name, Project_Description, Project_Start_date, Project_End_date, Project_Budget, Manager_ID) 
                        VALUES 
                        (@Client_ID, @Project_Name, @Project_Description, @Project_Start_date, @Project_End_date, @Project_Budget, @Manager_ID)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Client_ID", ddlClient.SelectedValue);
                cmd.Parameters.AddWithValue("@Project_Name", txtProjectName.Text);
                cmd.Parameters.AddWithValue("@Project_Description", txtProjectDescription.Text);
                cmd.Parameters.AddWithValue("@Project_Start_date", startDate);
                cmd.Parameters.AddWithValue("@Project_End_date", endDate);
                cmd.Parameters.AddWithValue("@Project_Budget", budget);
                cmd.Parameters.AddWithValue("@Manager_ID", 72); // Change Manager ID as needed

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Clear fields after success
                    txtProjectName.Text = string.Empty;
                    txtProjectDescription.Text = string.Empty;
                    txtStartDate.Text = string.Empty;
                    txtEndDate.Text = string.Empty;
                    txtBudget.Text = string.Empty;
                    ddlClient.SelectedIndex = 0;

                    // Display success message
                    Response.Write("<script>alert('Project inserted successfully!');</script>");
                }
                catch (Exception ex)
                {
                    // Handle errors, display the error message
                    litErrorMessage.Text = "An error occurred: " + ex.Message;
                }
            }
        }
    }
    }
        
    