<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectSelect.aspx.cs" Inherits="ProjectSite.Restricted.ProjectSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3B4xgl6B2yDlLW118697B6O6s9b4uO4fa/sdmE+8V9697D47j56R9B5V45X4G" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C8fC4Zt34b09BX65a5x5607696136a15997b6877540a14569709D13997828B86" crossorigin="anonymous"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container d-flex justify-content-center align-items-center" style="min-height: 100vh;">
        <div class="row justify-content-center w-100">
            <div class="col-md-6">
                <div class="card shadow-lg">
                    <div class="card-header text-center bg-primary text-white">
                        <h5 class="card-title mb-0">Project Selection</h5>
                    </div>
                    <div class="card-body p-4">
                        <div class="mb-3">
                            <asp:DropDownList ID="ddlProjects" runat="server" CssClass="form-select" DataTextField="Project_Name" DataValueField="Project_ID" AutoPostBack="True" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" DataSourceID="SqlDataSource1">
                            </asp:DropDownList>
                        </div>
                        <div class="d-grid gap-2">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" SelectCommand="SELECT DISTINCT Projecttbl.Project_ID, Projecttbl.Project_Name FROM Projecttbl INNER JOIN ProjectAssignmenttbl ON Projecttbl.Project_ID = ProjectAssignmenttbl.Project_ID WHERE (ProjectAssignmenttbl.Employee_ID = @employeeID)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="employeeID" SessionField="user_id" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-block" OnClick="btnSubmit_Click" />
                            <asp:SqlDataSource ID="sqlDSInsertDisbursement" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" DeleteCommand="DELETE FROM [DisbursementClaimtbl] WHERE [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Disbursement_Travel_Total] = @original_Disbursement_Travel_Total) OR ([Disbursement_Travel_Total] IS NULL AND @original_Disbursement_Travel_Total IS NULL)) AND (([Disbursement_Expense_Total] = @original_Disbursement_Expense_Total) OR ([Disbursement_Expense_Total] IS NULL AND @original_Disbursement_Expense_Total IS NULL)) AND (([Disbursement_Total_Claim] = @original_Disbursement_Total_Claim) OR ([Disbursement_Total_Claim] IS NULL AND @original_Disbursement_Total_Claim IS NULL)) AND [Disbursement_Date] = @original_Disbursement_Date AND (([Manager_ID] = @original_Manager_ID) OR ([Manager_ID] IS NULL AND @original_Manager_ID IS NULL)) AND (([Disbursement_Approved] = @original_Disbursement_Approved) OR ([Disbursement_Approved] IS NULL AND @original_Disbursement_Approved IS NULL))" InsertCommand="INSERT INTO DisbursementClaimtbl(Assignment_ID, Disbursement_Travel_Total, Disbursement_Expense_Total, Disbursement_Total_Claim, Disbursement_Date, Manager_ID, Disbursement_Approved) VALUES (@Assignment_ID, 0, 0, 0, GETDATE(), @Manager_ID, @Disbursement_Approved)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [DisbursementClaimtbl]" UpdateCommand="UPDATE [DisbursementClaimtbl] SET [Assignment_ID] = @Assignment_ID, [Disbursement_Travel_Total] = @Disbursement_Travel_Total, [Disbursement_Expense_Total] = @Disbursement_Expense_Total, [Disbursement_Total_Claim] = @Disbursement_Total_Claim, [Disbursement_Date] = @Disbursement_Date, [Manager_ID] = @Manager_ID, [Disbursement_Approved] = @Disbursement_Approved WHERE [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Disbursement_Travel_Total] = @original_Disbursement_Travel_Total) OR ([Disbursement_Travel_Total] IS NULL AND @original_Disbursement_Travel_Total IS NULL)) AND (([Disbursement_Expense_Total] = @original_Disbursement_Expense_Total) OR ([Disbursement_Expense_Total] IS NULL AND @original_Disbursement_Expense_Total IS NULL)) AND (([Disbursement_Total_Claim] = @original_Disbursement_Total_Claim) OR ([Disbursement_Total_Claim] IS NULL AND @original_Disbursement_Total_Claim IS NULL)) AND [Disbursement_Date] = @original_Disbursement_Date AND (([Manager_ID] = @original_Manager_ID) OR ([Manager_ID] IS NULL AND @original_Manager_ID IS NULL)) AND (([Disbursement_Approved] = @original_Disbursement_Approved) OR ([Disbursement_Approved] IS NULL AND @original_Disbursement_Approved IS NULL))">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                                    <asp:Parameter Name="original_Disbursement_Travel_Total" Type="Decimal" />
                                    <asp:Parameter Name="original_Disbursement_Expense_Total" Type="Decimal" />
                                    <asp:Parameter Name="original_Disbursement_Total_Claim" Type="Decimal" />
                                    <asp:Parameter Name="original_Disbursement_Date" Type="DateTime" />
                                    <asp:Parameter Name="original_Manager_ID" Type="Int32" />
                                    <asp:Parameter Name="original_Disbursement_Approved" Type="String" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                                    <asp:Parameter DefaultValue="0" Name="Disbursement_Travel_Total" Type="Decimal" />
                                    <asp:Parameter DefaultValue="0" Name="Disbursement_Expense_Total" Type="Decimal" />
                                    <asp:Parameter DefaultValue="0" Name="Disbursement_Total_Claim" Type="Decimal" />
                                    <asp:QueryStringParameter DefaultValue="" Name="Disbursement_Date" Type="DateTime" />
                                    <asp:Parameter Name="Manager_ID" Type="Int32" />
                                    <asp:Parameter Name="Disbursement_Approved" Type="String" />
                                </InsertParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                                    <asp:Parameter Name="Disbursement_Travel_Total" Type="Decimal" />
                                    <asp:Parameter Name="Disbursement_Expense_Total" Type="Decimal" />
                                    <asp:Parameter Name="Disbursement_Total_Claim" Type="Decimal" />
                                    <asp:Parameter Name="Disbursement_Date" Type="DateTime" />
                                    <asp:Parameter Name="Manager_ID" Type="Int32" />
                                    <asp:Parameter Name="Disbursement_Approved" Type="String" />
                                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                                    <asp:Parameter Name="original_Disbursement_Travel_Total" Type="Decimal" />
                                    <asp:Parameter Name="original_Disbursement_Expense_Total" Type="Decimal" />
                                    <asp:Parameter Name="original_Disbursement_Total_Claim" Type="Decimal" />
                                    <asp:Parameter Name="original_Disbursement_Date" Type="DateTime" />
                                    <asp:Parameter Name="original_Manager_ID" Type="Int32" />
                                    <asp:Parameter Name="original_Disbursement_Approved" Type="String" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Assignment_ID" DataSourceID="SqlDataSource2">
                                <Columns>
                                    <asp:BoundField DataField="Assignment_ID" HeaderText="Assignment_ID" InsertVisible="False" ReadOnly="True" SortExpression="Assignment_ID" />
                                    <asp:BoundField DataField="Project_ID" HeaderText="Project_ID" SortExpression="Project_ID" />
                                    <asp:BoundField DataField="Employee_ID" HeaderText="Employee_ID" SortExpression="Employee_ID" />
                                    <asp:BoundField DataField="Date_Assigned" HeaderText="Date_Assigned" SortExpression="Date_Assigned" />
                                    <asp:BoundField DataField="Assignment_Claim_Max" HeaderText="Assignment_Claim_Max" SortExpression="Assignment_Claim_Max" />
                                    <asp:BoundField DataField="Assignment_Claim_Balance" HeaderText="Assignment_Claim_Balance" SortExpression="Assignment_Claim_Balance" />
                                    <asp:BoundField DataField="Project_Name" HeaderText="Project_Name" SortExpression="Project_Name" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" SelectCommand="SELECT ProjectAssignmenttbl.Assignment_ID, ProjectAssignmenttbl.Project_ID, ProjectAssignmenttbl.Employee_ID, ProjectAssignmenttbl.Date_Assigned, ProjectAssignmenttbl.Assignment_Claim_Max, ProjectAssignmenttbl.Assignment_Claim_Balance, Projecttbl.Project_Name FROM ProjectAssignmenttbl INNER JOIN Projecttbl ON ProjectAssignmenttbl.Project_ID = Projecttbl.Project_ID WHERE (ProjectAssignmenttbl.Employee_ID = @employeeID) AND (ProjectAssignmenttbl.Project_ID = @ID)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="employeeID" SessionField="employee_id" />
                                    <asp:SessionParameter DefaultValue="" Name="ID" SessionField="&quot;project_id&quot;" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

