using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace ProjectSite
{
    public partial class ViewClaims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (User.Identity.IsAuthenticated)
                {
                    // Check if the user has the "Employee" role
                    if (User.IsInRole("Employee"))
                    {
                        // Get logged-in user's email
                        string userEmail = User.Identity.Name;

                        // Define your connection string
                        string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

                        // Define the SQL query to check if the user exists in the Employee table
                        string query = "SELECT Employee_ID FROM Employeetbl WHERE Employee_Email = @Email";

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
                                object result = cmd.ExecuteScalar();

                                // If the result is null, the user does not exist in the Employee table
                                if (result == null)
                                {
                                    // Redirect to error page if the user is not in the Employee table
                                    Response.Redirect("~/ErrorPages/AccessDenied.aspx");
                                }
                                else
                                {
                                    int employeeId = (int)result;
                                    Session["user_id"] = employeeId;
                                    Session["employee_id"] = employeeId;

                                    LoadClaims(employeeId);
                                }
                            }
                        }
                    }
                    else
                    {
                        // If the user is logged in but does not have the "Employee" role, redirect to access denied page
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

        private void LoadClaims(int employeeId)
        {
            // Define the connection string (fetch from Web.config for security)
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            int? assignmentId = GetAssignmentID(employeeId);

            // SQL query to retrieve claims for the logged-in employee using the retrieved Assignment ID
            string query = @"SELECT 
    dc.Disbursement_Claim_ID,
    pa.Assignment_ID,
    dc.Disbursement_Travel_Total,
    dc.Disbursement_Expense_Total,
    dc.Disbursement_Total_Claim,
    dc.Disbursement_Date,
    dc.Manager_ID,
    dc.Disbursement_Approved,
    p.Project_Name  -- Selecting the project name
FROM 
    DisbursementClaimtbl dc
INNER JOIN 
    ProjectAssignmenttbl pa ON dc.Assignment_ID = pa.Assignment_ID
INNER JOIN 
    Projecttbl p ON pa.Project_ID = p.Project_ID  -- Joining the Project table
WHERE 
    pa.Employee_ID = @assignmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@assignmentId", employeeId);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable claimsTable = new DataTable();
                        adapter.Fill(claimsTable);

                        // Bind the data to the GridView
                        ClaimsGridView.DataSource = claimsTable;
                        ClaimsGridView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or show an error message
                        Console.WriteLine("Error loading claims: " + ex.Message);
                    }
                }
            }
        }

        private int? GetAssignmentID(int employeeId)
        {
            int? assignmentId = null;

            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = @"SELECT Assignment_ID 
                             FROM ProjectAssignmenttbl 
                             WHERE Employee_ID = @employeeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@employeeId", employeeId);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            assignmentId = (int)result;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or show an error message
                        Console.WriteLine("Error retrieving assignment ID: " + ex.Message);
                    }
                }
            }

            return assignmentId;
        }

        protected void ClaimsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Get the row index from the command argument
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = ClaimsGridView.Rows[rowIndex];

                // Get the Disbursement_Claim_ID from the selected row
                int disbursementClaimId = Convert.ToInt32(selectedRow.Cells[0].Text);

                // Load the expense and travel details for the selected claim
                LoadExpenseDetails(disbursementClaimId);
                LoadTravelDetails(disbursementClaimId);

                // Make the panels visible
                ExpensePanel.Visible = true;
                TravelPanel.Visible = true;
            }
        }

        private void LoadExpenseDetails(int disbursementClaimId)
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            // SQL query to retrieve expense details for the selected claim
            string query = @"
        SELECT 
            e.Expense_ID,
            e.Disbursement_Claim_ID,
            e.Assignment_ID,
            e.Expense_Total,
            e.Expense_Date,
            e.Expense_Proof,
            et.Expense_Name
        FROM 
            Expensetbl e
        INNER JOIN 
            ExpenseTypetbl et ON e.Expense_Type_ID = et.Expense_Type_ID
        WHERE 
            e.Disbursement_Claim_ID = @disbursementClaimId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@disbursementClaimId", disbursementClaimId);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable expenseTable = new DataTable();
                        adapter.Fill(expenseTable);

                        // Bind the data to the ExpenseGridView
                        ExpenseGridView.DataSource = expenseTable;
                        ExpenseGridView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or show an error message
                        Console.WriteLine("Error loading expense details: " + ex.Message);
                    }
                }
            }
        }

        private void LoadTravelDetails(int disbursementClaimId)
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";

            // SQL query to retrieve travel details for the selected claim
            string query = @"SELECT 
                        Travel_ID,
                        Disbursement_Claim_ID,
                        Assignment_ID,
                        Travel_Description,
                        Travel_Date,
                        Travel_Mileage,
                        Travel_Vehicle_Description,
                        Travel_Total,
                        Travel_Proof
                     FROM 
                        Traveltbl
                     WHERE 
                        Disbursement_Claim_ID = @disbursementClaimId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@disbursementClaimId", disbursementClaimId);

                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable travelTable = new DataTable();
                        adapter.Fill(travelTable);

                        // Bind the data to the TravelGridView
                        TravelGridView.DataSource = travelTable;
                        TravelGridView.DataBind();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or show an error message
                        Console.WriteLine("Error loading travel details: " + ex.Message);
                    }
                }
            }
        }


        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            if (ClaimsGridView.SelectedRow != null)
            {
                // Retrieve the selected Disbursement_Claim_ID from the GridView
                int disbursementClaimId = Convert.ToInt32(ClaimsGridView.SelectedRow.Cells[0].Text);

                // Create the PDF document
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                MemoryStream stream = new MemoryStream();
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                // Add Title and Claim Details
                pdfDoc.Add(new Paragraph("Disbursement Claim", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)));
                pdfDoc.Add(new Paragraph(" "));

                // Load and add claim details
                AddClaimDetailsToPDF(disbursementClaimId, pdfDoc);

                // Load and add expense details
                AddExpenseDetailsToPDF(disbursementClaimId, pdfDoc);

                // Load and add travel details
                AddTravelDetailsToPDF(disbursementClaimId, pdfDoc);

                pdfDoc.Close();

                // Send the PDF as a downloadable file
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=DisbursementClaim.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
            else
            {
                Console.WriteLine("Please select a claim to generate the PDF.");
            }
        }

        // Method to add claim details to PDF
        private void AddClaimDetailsToPDF(int disbursementClaimId, Document pdfDoc)
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = @"SELECT 
                        dc.Disbursement_Claim_ID,
                        pa.Assignment_ID,
                        dc.Disbursement_Travel_Total,
                        dc.Disbursement_Expense_Total,
                        dc.Disbursement_Total_Claim,
                        dc.Disbursement_Date,
                        p.Project_Name
                     FROM 
                        DisbursementClaimtbl dc
                     INNER JOIN 
                        ProjectAssignmenttbl pa ON dc.Assignment_ID = pa.Assignment_ID
                     INNER JOIN 
                        Projecttbl p ON pa.Project_ID = p.Project_ID
                     WHERE 
                        dc.Disbursement_Claim_ID = @disbursementClaimId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@disbursementClaimId", disbursementClaimId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Add details to PDF
                            pdfDoc.Add(new Paragraph("Claim ID: " + reader["Disbursement_Claim_ID"]));
                            pdfDoc.Add(new Paragraph("Project Name: " + reader["Project_Name"]));
                            pdfDoc.Add(new Paragraph("Total Travel Cost: " + reader["Disbursement_Travel_Total"]));
                            pdfDoc.Add(new Paragraph("Total Expense Cost: " + reader["Disbursement_Expense_Total"]));
                            pdfDoc.Add(new Paragraph("Total Claim: " + reader["Disbursement_Total_Claim"]));
                            pdfDoc.Add(new Paragraph("Claim Date: " + Convert.ToDateTime(reader["Disbursement_Date"]).ToShortDateString()));
                            pdfDoc.Add(new Paragraph(" "));
                        }
                    }
                }
            }
        }

        // Method to add expense details to PDF
        private void AddExpenseDetailsToPDF(int disbursementClaimId, Document pdfDoc)
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = @"SELECT 
                        e.Expense_ID,
                        e.Expense_Total,
                        e.Expense_Date,
                        et.Expense_Name
                     FROM 
                        Expensetbl e
                     INNER JOIN 
                        ExpenseTypetbl et ON e.Expense_Type_ID = et.Expense_Type_ID
                     WHERE 
                        e.Disbursement_Claim_ID = @disbursementClaimId";

            PdfPTable expenseTable = new PdfPTable(4);
            expenseTable.AddCell("Expense ID");
            expenseTable.AddCell("Expense Name");
            expenseTable.AddCell("Expense Total");
            expenseTable.AddCell("Expense Date");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@disbursementClaimId", disbursementClaimId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenseTable.AddCell(reader["Expense_ID"].ToString());
                            expenseTable.AddCell(reader["Expense_Name"].ToString());
                            expenseTable.AddCell(reader["Expense_Total"].ToString());
                            expenseTable.AddCell(Convert.ToDateTime(reader["Expense_Date"]).ToShortDateString());
                        }
                    }
                }
            }

            pdfDoc.Add(new Paragraph("Expense Details", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
            pdfDoc.Add(expenseTable);
            pdfDoc.Add(new Paragraph(" "));
        }

        // Method to add travel details to PDF
        private void AddTravelDetailsToPDF(int disbursementClaimId, Document pdfDoc)
        {
            string connectionString = "Server=146.230.177.46;Database=G8Wst2024;User Id=G8Wst2024;Password=09ujd";
            string query = @"SELECT 
                        Travel_ID,
                        Travel_Description,
                        Travel_Date,
                        Travel_Mileage,
                        Travel_Total
                     FROM 
                        Traveltbl
                     WHERE 
                        Disbursement_Claim_ID = @disbursementClaimId";

            PdfPTable travelTable = new PdfPTable(4);
            travelTable.AddCell("Travel ID");
            travelTable.AddCell("Description");
            travelTable.AddCell("Mileage");
            travelTable.AddCell("Date");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@disbursementClaimId", disbursementClaimId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            travelTable.AddCell(reader["Travel_ID"].ToString());
                            travelTable.AddCell(reader["Travel_Description"].ToString());
                            travelTable.AddCell(reader["Travel_Mileage"].ToString());
                            travelTable.AddCell(Convert.ToDateTime(reader["Travel_Date"]).ToShortDateString());
                        }
                    }
                }
            }

            pdfDoc.Add(new Paragraph("Travel Details", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD)));
            pdfDoc.Add(travelTable);
            pdfDoc.Add(new Paragraph(" "));
        }
    }
}

   

