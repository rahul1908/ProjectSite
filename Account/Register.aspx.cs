using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ProjectSite.Models;
using System.Data.SqlClient;

namespace ProjectSite.Account
{
    public partial class RegisterClient : System.Web.UI.Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            // Define the connection string directly (replace with your actual connection string)
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            // SQL query to insert data into Clienttbl
            string query = "INSERT INTO Clienttbl (Client_Name, Client_Tier, Client_Rates, Address_Line1, Address_Line2, Suburb, City, Province, Postal_Code, Client_Email) " +
                           "VALUES (@ClientName, @ClientTier, @ClientRates, @AddressLine1, @AddressLine2, @Suburb, @City, @Province, @PostalCode, @ClientEmail)";

            // Establish a connection to the SQL Server
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Create the SQL command
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@ClientName", ClientName.Text);
                    cmd.Parameters.AddWithValue("@ClientTier", ClientTier.SelectedValue);
                    cmd.Parameters.AddWithValue("@ClientRates", ClientRates.Text);
                    cmd.Parameters.AddWithValue("@AddressLine1", AddressLine1.Text);
                    cmd.Parameters.AddWithValue("@AddressLine2", AddressLine2.Text);
                    cmd.Parameters.AddWithValue("@Suburb", Suburb.Text);
                    cmd.Parameters.AddWithValue("@City", City.Text);
                    cmd.Parameters.AddWithValue("@Province", Province.Text);
                    cmd.Parameters.AddWithValue("@PostalCode", PostalCode.Text);
                    cmd.Parameters.AddWithValue("@ClientEmail", ClientEmail.Text);

                    try
                    {
                        // Open the connection and execute the query
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Client registration successful!');</script>");
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that may occur
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
        }
    }
}