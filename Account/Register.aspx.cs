using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ProjectSite.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectSite.Account
{

    public partial class RegisterClient : System.Web.UI.Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            // Define the connection string directly (replace with your actual connection string)
            string connString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            // SQL query to insert data into Clienttbl
            string clientQuery = "INSERT INTO Clienttbl (Client_Name, Client_Tier, Client_Rates, Address_Line1, Address_Line2, Suburb, City, Province, Postal_Code, Client_Email) " +
                                 "VALUES (@ClientName, @ClientTier, @ClientRates, @AddressLine1, @AddressLine2, @Suburb, @City, @Province, @PostalCode, @ClientEmail)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert into Clienttbl
                    using (SqlCommand cmdClient = new SqlCommand(clientQuery, conn, transaction))
                    {
                        cmdClient.Parameters.AddWithValue("@ClientName", ClientName.Text);
                        cmdClient.Parameters.AddWithValue("@ClientTier", ClientTier.SelectedValue);
                        cmdClient.Parameters.AddWithValue("@ClientRates", Decimal.Parse(ClientRates.Text));
                        cmdClient.Parameters.AddWithValue("@AddressLine1", AddressLine1.Text);
                        cmdClient.Parameters.AddWithValue("@AddressLine2", AddressLine2.Text);
                        cmdClient.Parameters.AddWithValue("@Suburb", Suburb.Text);
                        cmdClient.Parameters.AddWithValue("@City", City.Text);
                        cmdClient.Parameters.AddWithValue("@Province", Province.Text);
                        cmdClient.Parameters.AddWithValue("@PostalCode", PostalCode.Text);
                        cmdClient.Parameters.AddWithValue("@ClientEmail", ClientEmail.Text);

                        cmdClient.ExecuteNonQuery();
                    }

                    // Use UserManager to create the ASP.NET user
                    var userStore = new UserStore<IdentityUser>(new IdentityDbContext());
                    var userManager = new UserManager<IdentityUser>(userStore);

                    // Create the user object
                    var user = new IdentityUser
                    {
                        UserName = ClientEmail.Text,
                        Email = ClientEmail.Text
                    };

                    // Hash the password (replace "ClientPassword.Text" with the actual password input)
                    var result = userManager.Create(user, ClientPassword.Text);

                    if (result.Succeeded)
                    {
                        transaction.Commit();
                        Response.Write("<script>alert('Client and user registration successful!');</script>");
                    }
                    else
                    {
                        transaction.Rollback();
                        Response.Write("<script>alert('Error: " + string.Join(", ", result.Errors) + "');</script>");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }
    }
}





