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
                LoadClients();
            }
        }

        // Load clients into dropdown
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
            }
        }

        // Handle form submission
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
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
                cmd.Parameters.AddWithValue("@Project_Start_date", txtStartDate.Text);
                cmd.Parameters.AddWithValue("@Project_End_date", txtEndDate.Text);
                cmd.Parameters.AddWithValue("@Project_Budget", txtBudget.Text);
                cmd.Parameters.AddWithValue("@Manager_ID", txtManagerID.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Redirect or show a success message
            Response.Write("Project inserted successfully!");
        }
    }
}
        
    