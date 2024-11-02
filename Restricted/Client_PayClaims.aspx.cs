using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ProjectSite.Restricted
{

    public partial class Client_PayClaims : System.Web.UI.Page
    {
        string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is authenticated and has the "Client" role
                if (User.Identity.IsAuthenticated && User.IsInRole("Client"))
                {
                    VerifyClientRole();
                }
                else
                {
                    // Redirect to access denied page if the user is not authenticated or not in the "Client" role
                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                }
            }
        }

        private void VerifyClientRole()
        {
            string userEmail = User.Identity.Name;
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = "SELECT Client_ID FROM Clienttbl WHERE Client_Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the user's email as a parameter
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    conn.Open();

                    object clientId = cmd.ExecuteScalar();

                    if (clientId == null)
                    {
                        // Redirect to access denied page if the email is not found in Clienttbl
                        Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                    }
                    else
                    {
                        // Store ClientID in session for later use
                        Session["ClientID"] = (int)clientId;

                        // Load projects and claims associated with the client
                        LoadClientProjects();

                    }
                }
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
            int projectId;
            if (int.TryParse(ProjectDropDown.SelectedValue, out projectId) && projectId != 0)
            {
                LoadClaims(projectId);
            }
        }

        private void LoadClaims(int projectId)
        {
            string query = @" SELECT dc.Disbursement_Claim_ID, 
       dc.Disbursement_Total_Claim, 
       dc.Disbursement_Date 
FROM DisbursementClaimtbl dc
INNER JOIN ProjectAssignmenttbl pa ON dc.Assignment_ID = pa.Assignment_ID
INNER JOIN Projecttbl p ON pa.Project_ID = p.Project_ID
WHERE p.Project_ID = @ProjectId
AND dc.Disbursement_Approved = 'Approved' ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Logic to populate claims grid or dropdown with the results
                        DisbursementsGridView.DataSource = reader;
                        DisbursementsGridView.DataBind();
                    }
                }
            }
        }

        protected void DisbursementsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PayOut")
            {
                int disbursementId = Convert.ToInt32(e.CommandArgument);
                ProcessPayout(disbursementId);
            }
        }

        private void ProcessPayout(int disbursementId)
        {
            string query = "UPDATE DisbursementClaimtbl SET Disbursement_Approved = 'Paid' WHERE Disbursement_Claim_ID = @DisbursementId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DisbursementId", disbursementId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Refresh disbursements
            int projectId = int.Parse(ProjectDropDown.SelectedValue);
            LoadClaims(projectId);
        }
    }

}