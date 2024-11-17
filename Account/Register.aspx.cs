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
            // Define the connection string (replace with your actual connection details)
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

                    // Add the user to ASP.NET Identity
                    var userStore = new UserStore<IdentityUser>(new IdentityDbContext());
                    var userManager = new UserManager<IdentityUser>(userStore);

                    var user = new IdentityUser
                    {
                        UserName = ClientEmail.Text,
                        Email = ClientEmail.Text,
                        LockoutEnabled = true
                    };

                    // Create the user with the password
                    var result = userManager.Create(user, ClientPassword.Text);

                    if (result.Succeeded)
                    {
                        // Create and assign the "Client" role if not already created
                        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));

                        if (!roleManager.RoleExists("Client"))
                        {
                            var roleResult = roleManager.Create(new IdentityRole("Client"));
                            if (!roleResult.Succeeded)
                            {
                                transaction.Rollback();
                                Response.Write("<script>alert('Error creating \"Client\" role.');</script>");
                                return;
                            }
                        }

                        // Assign the user to the "Client" role
                        userManager.AddToRole(user.Id, "Client");

                        // Commit the transaction
                        transaction.Commit();
                        Response.Write("<script>alert('Client added and assigned to Client role successfully!');</script>");
                    }
                    else
                    {
                        // Handle errors from user creation
                        transaction.Rollback();
                        Response.Write("<script>alert('Error: " + string.Join(", ", result.Errors) + "');</script>");
                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    transaction.Rollback();
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }
    }
}





