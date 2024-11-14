using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

namespace ProjectSite.Restricted
{
    public partial class ClientsReports : System.Web.UI.Page
    {
        string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadClientProjects();
                int clientId = GetClientIdByEmail(User.Identity.Name);
                string clientName = GetClientNameById(clientId);
                //string projectName = ProjectDropDown.SelectedItem.Text;
                string message = $"Client Name: {clientName} ";
                string projectName = ProjectDropDown.SelectedItem.Text;

              
            }
        }

        private void LoadClientProjects()
        {
            string userEmail = User.Identity.Name;
            int clientId = GetClientIdByEmail(userEmail);

            if (clientId > 0)
            {
                LoadProjects(clientId);
            }
            else
            {
                Response.Redirect("~/ErrorPages/AccessDenied.aspx");
            }
        }

        private int GetClientIdByEmail(string email)
        {
            string query = "SELECT Client_ID FROM Clienttbl WHERE Client_Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? (int)result : 0;
            }
        }

        private void LoadProjects(int clientId)
        {
            string query = "SELECT Project_ID, Project_Name FROM Projecttbl WHERE Client_ID = @ClientId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ClientId", clientId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ProjectDropDown.DataSource = reader;
                    ProjectDropDown.DataTextField = "Project_Name";
                    ProjectDropDown.DataValueField = "Project_ID";
                    ProjectDropDown.DataBind();
                }
            }
            ProjectDropDown.Items.Insert(0, new ListItem("Select Project", "0"));
        }

        protected void ProjectDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int clientId = GetClientIdByEmail(User.Identity.Name);
            string clientName = GetClientNameById(clientId);
            string projectName = ProjectDropDown.SelectedItem.Text;

            string message = $"Client Name: {clientName} ";
            string script = $"alert('{message}');";
            ClientScript.RegisterStartupScript(this.GetType(), "showAlert", script, true);


            string message1 = $"Project Name: {projectName}";

            // Show the message in an alert
            string script1 = $"alert('{message1}');";
            ClientScript.RegisterStartupScript(this.GetType(), "showProjectAlert", script1, true);


            // Check if a valid project has been selected
            if (ProjectDropDown.SelectedValue != "0")
            {
                // Get the full Power BI report URL for the selected project
                string reportUrl = GetPowerBiReportUrl(int.Parse(ProjectDropDown.SelectedValue));
                message += $"\nGenerated Report URL: {reportUrl}";

                // Pass the report URL to JavaScript function to update iframe source dynamically
                ScriptManager.RegisterStartupScript(this, GetType(), "loadReport",
                    $"loadReport('{reportUrl}', '{clientName}', '{projectName}');", true);

                // Display the message via an alert
           

                // Clear any previous error messages
                ReportMessage.Text = "";
            }
            else
            {
                // Clear the iframe if no project is selected and show a message
                PowerBiReportFrame.Text = "";
                ReportMessage.Text = "Please select a project to view the report";
            }
        }


        private string GetClientNameById(int clientId)
        {
            string query = "SELECT Client_Name FROM Clienttbl WHERE Client_ID = @ClientId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ClientId", clientId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
        }
        private string GetPowerBiReportUrl(int projectId)
        {
            // Base report URL (replace with your actual Power BI report URL)
            string baseUrl = "https://app.powerbi.com/view?r=eyJrIjoiYzY2N2EzM2ItOGE3Mi00NTMxLWE0N2MtOTM1NmExYjYyZGFjIiwidCI6IjIyNjgyN2Q2LWE5ZDAtNDcwZC04YzE1LWIxNDZiMDE5MmQ1MSIsImMiOjh9";

            // Retrieve client name and project name based on projectId
            string clientName = GetClientNameById(GetClientIdByEmail(User.Identity.Name));
            string projectName = ProjectDropDown.SelectedItem.Text; // Implement this method to get project name

            if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(projectName))
            {
                throw new Exception("Client or Project name not found.");
            }

            // Encode values for URL
            string encodedClientName = Uri.EscapeDataString(clientName);
            string encodedProjectName = Uri.EscapeDataString(projectName);

            // Append filters to the base URL
            string fullUrl = $"{baseUrl}&filter=Clienttbl/Client_Name eq '{encodedClientName}' and Projecttbl/Project_Name eq '{encodedProjectName}'";

            return fullUrl;

        }

        //private string GetPowerBiReportUrl(string clientName, string projectName)
        //{
        //    // Base report URL
        //    string baseUrl = "https://app.powerbi.com/view?r=eyJrIjoiYzY2N2EzM2ItOGE3Mi00NTMxLWE0N2MtOTM1NmExYjYyZGFjIiwidCI6IjIyNjgyN2Q2LWE5ZDAtNDcwZC04YzE1LWIxNDZiMDE5MmQ1MSIsImMiOjh9";

        //    // Adding filter parameters to the URL
        //    string filterUrl = $"{baseUrl}&filter=Table/ClientName eq '{clientName}' and Table/ProjectName eq '{projectName}'";

        //    return filterUrl;
        //}
    }
}