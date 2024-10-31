using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace ProjectSite.Restricted
{
    public partial class Admin_Employee : System.Web.UI.Page
    {
        private readonly string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                {
                    VerifyAdminRole();
                }
                else
                {
                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                }
            }
        }

        private void VerifyAdminRole()
        {
            string userEmail = User.Identity.Name;
            string query = "SELECT COUNT(*) FROM Employeetbl WHERE Employee_Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                    }
                    else
                    {
                        LoadEmployees();
                    }
                }
            }
        }

        private void LoadEmployees(string searchText = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employeetbl";

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " WHERE Employee_Name LIKE @Search OR Employee_ID LIKE @Search OR Employee_Email LIKE @Search";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchText + "%");
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GridViewEmployees.DataSource = dt;
                        GridViewEmployees.DataBind();
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadEmployees(txtSearch.Text.Trim());
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadEmployees();
        }

        protected void GridViewEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewEmployees.EditIndex = e.NewEditIndex;
            LoadEmployees(txtSearch.Text.Trim());
        }

        protected void GridViewEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewEmployees.DataKeys[e.RowIndex].Value.ToString());
            string rsaID = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string name = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string surname = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string jobTitle = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string mobile = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string address1 = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
            string address2 = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
            string suburb = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[8].Controls[0]).Text;
            string city = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[9].Controls[0]).Text;
            string province = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[10].Controls[0]).Text;
            string postalCode = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[11].Controls[0]).Text;
            string email = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[12].Controls[0]).Text;
            string password = ((TextBox)GridViewEmployees.Rows[e.RowIndex].Cells[13].Controls[0]).Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Employeetbl SET RSA_ID = @RSAID, Employee_Name = @Name, Employee_Surname = @Surname, 
                                 Employee_Job_Title = @JobTitle, Employee_Mobile_Number = @Mobile, Address_Line1 = @Address1, 
                                 Address_Line2 = @Address2, Suburb = @Suburb, City = @City, Province = @Province, 
                                 Postal_Code = @PostalCode, Employee_Email = @Email, Employee_Password = @Password 
                                 WHERE Employee_ID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RSAID", rsaID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Surname", surname);
                    cmd.Parameters.AddWithValue("@JobTitle", jobTitle);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    cmd.Parameters.AddWithValue("@Address1", address1);
                    cmd.Parameters.AddWithValue("@Address2", address2);
                    cmd.Parameters.AddWithValue("@Suburb", suburb);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Province", province);
                    cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    GridViewEmployees.EditIndex = -1;
                    LoadEmployees(txtSearch.Text.Trim());
                }
            }
        }

        protected void GridViewEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewEmployees.EditIndex = -1;
            LoadEmployees(txtSearch.Text.Trim());
        }

        protected void GridViewEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridViewEmployees.DataKeys[e.RowIndex].Value.ToString());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Employeetbl WHERE Employee_ID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadEmployees(txtSearch.Text.Trim());
                }
            }
        }
    }
}
