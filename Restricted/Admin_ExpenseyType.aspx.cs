using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProjectSite.Restricted
{
    public partial class Admin_ExpenseyType : System.Web.UI.Page
    {
        private readonly string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (User.Identity.IsAuthenticated)
                {
                    // Check if the user has the "Manager" role
                    if (User.IsInRole("Admin"))
                    {
                        // Get logged-in user's email
                        string userEmail = User.Identity.Name;

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
                            }
                        }

                        // Load expense types if user is verified as a Manager and in the Employee table
                        LoadExpenseTypes();
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

        private void LoadExpenseTypes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ExpenseTypetbl", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridViewExpenseType.DataSource = dt;
                    GridViewExpenseType.DataBind();
                }
            }
        }

        protected void btnAddExpenseType_Click(object sender, EventArgs e)
        {
            string expenseName = txtExpenseName.Text.Trim();

            if (!string.IsNullOrEmpty(expenseName))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ExpenseTypetbl (Expense_Name) VALUES (@Expense_Name)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Expense_Name", expenseName);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "Expense type added successfully!";
                txtExpenseName.Text = string.Empty;
                LoadExpenseTypes();
            }
            else
            {
                lblMessage.Text = "Please enter an expense type name.";
            }
        }

        protected void GridViewExpenseType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewExpenseType.EditIndex = e.NewEditIndex;
            LoadExpenseTypes();
        }

        protected void GridViewExpenseType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewExpenseType.DataKeys[e.RowIndex].Value.ToString());
            string expenseName = ((TextBox)GridViewExpenseType.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE ExpenseTypetbl SET Expense_Name = @Expense_Name WHERE Expense_Type_ID = @Expense_Type_ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Expense_Name", expenseName);
                    cmd.Parameters.AddWithValue("@Expense_Type_ID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            GridViewExpenseType.EditIndex = -1;
            lblMessage.Text = "Expense type updated successfully!";
            LoadExpenseTypes();
        }

        protected void GridViewExpenseType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridViewExpenseType.DataKeys[e.RowIndex].Value.ToString());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ExpenseTypetbl WHERE Expense_Type_ID = @Expense_Type_ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Expense_Type_ID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "Expense type deleted successfully!";
            LoadExpenseTypes();
        }

        protected void GridViewExpenseType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewExpenseType.EditIndex = -1;
            LoadExpenseTypes();
        }
    }
}
