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
    public partial class Admin_Project : System.Web.UI.Page
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
                        LoadProjects();
                    }
                }
            }
        }

        private void LoadProjects(string searchText = "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Projecttbl";

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " WHERE Project_Name LIKE @Search OR Project_ID LIKE @Search OR Client_ID LIKE @Search";
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
                        GridViewProjects.DataSource = dt;
                        GridViewProjects.DataBind();
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProjects(txtSearch.Text.Trim());
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadProjects();
        }

        protected void GridViewProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewProjects.EditIndex = e.NewEditIndex;
            LoadProjects(txtSearch.Text.Trim());
        }

        protected void GridViewProjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewProjects.DataKeys[e.RowIndex].Value.ToString());
            int clientID = Convert.ToInt32(((TextBox)GridViewProjects.Rows[e.RowIndex].Cells[1].Controls[0]).Text);
            string projectName = ((TextBox)GridViewProjects.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string projectDesc = ((TextBox)GridViewProjects.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            DateTime startDate = Convert.ToDateTime(((TextBox)GridViewProjects.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
            DateTime endDate = Convert.ToDateTime(((TextBox)GridViewProjects.Rows[e.RowIndex].Cells[5].Controls[0]).Text);
            decimal budget = Convert.ToDecimal(((TextBox)GridViewProjects.Rows[e.RowIndex].Cells[6].Controls[0]).Text);
            int managerID = Convert.ToInt32(((TextBox)GridViewProjects.Rows[e.RowIndex].Cells[7].Controls[0]).Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Projecttbl SET Client_ID = @ClientID, Project_Name = @ProjectName, Project_Description = @ProjectDesc, 
                                 Project_Start_date = @StartDate, Project_End_date = @EndDate, Project_Budget = @Budget, Manager_ID = @ManagerID 
                                 WHERE Project_ID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientID", clientID);
                    cmd.Parameters.AddWithValue("@ProjectName", projectName);
                    cmd.Parameters.AddWithValue("@ProjectDesc", projectDesc);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@Budget", budget);
                    cmd.Parameters.AddWithValue("@ManagerID", managerID);
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    GridViewProjects.EditIndex = -1;
                    LoadProjects(txtSearch.Text.Trim());
                }
            }
        }

        protected void GridViewProjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewProjects.EditIndex = -1;
            LoadProjects(txtSearch.Text.Trim());
        }

        protected void GridViewProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridViewProjects.DataKeys[e.RowIndex].Value.ToString());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Projecttbl WHERE Project_ID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    LoadProjects(txtSearch.Text.Trim());
                }
            }
        }
    }
}