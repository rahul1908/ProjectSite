<%@ Page Title="Update Disbursements" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateDisbursements.aspx.cs" Inherits="ProjectSite.Restricted.UpdateDisbursements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Custom CSS or Scripts here if needed -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Project Selection Section -->
        <div class="mb-4">
            <h3>Update Disbursement Details</h3>
            <div class="row mb-3">
                <label for="projectSelection" class="col-sm-2 col-form-label">Select Project</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlProjectSelection" runat="server" CssClass="form-select" DataSourceID="sqlDSProject" DataTextField="Project_Name" DataValueField="Project_ID" AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Text="-- Select a Project --" Value="" />
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sqlDSProject" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" 
                        SelectCommand="SELECT DISTINCT Projecttbl.Project_ID, Projecttbl.Project_Name FROM Projecttbl INNER JOIN ProjectAssignmenttbl ON Projecttbl.Project_ID = ProjectAssignmenttbl.Project_ID WHERE (ProjectAssignmenttbl.Employee_ID = @employeeID)">
                        <SelectParameters>
                            <asp:SessionParameter Name="employeeID" SessionField="user_id" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

            <!-- Disbursement Date Selection Section -->
            <div class="row mb-3">
                <label for="disbursementDate" class="col-sm-2 col-form-label">Select Disbursement Date</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlDisbursementDate" runat="server" CssClass="form-select" DataSourceID="sqlDSDisbursementDate" DataTextField="DisbursementDate" DataValueField="DisbursementID" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Select Disbursement Date --" Value="" />
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sqlDSDisbursementDate" runat="server"
                        ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>"
                        SelectCommand="SELECT DisbursementClaimtbl.Disbursement_Claim_ID, DisbursementClaimtbl.Disbursement_Date FROM DisbursementClaimtbl INNER JOIN ProjectAssignmenttbl ON DisbursementClaimtbl.Assignment_ID = ProjectAssignmenttbl.Assignment_ID WHERE (ProjectAssignmenttbl.Project_ID = @ProjectID) AND (ProjectAssignmenttbl.Employee_ID = @employeeID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlProjectSelection" Name="ProjectID" PropertyName="SelectedValue" Type="Int32" />
                            <asp:SessionParameter Name="employeeID" SessionField="user_id" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>


        <!-- Project Selection Section -->
<div class="card mb-4">
    <div class="card-header">
        <h5 class="card-title">Project Selection</h5>
    </div>
    <div class="card-body">
        <!-- Project Name Selection -->
        <div class="row mb-3">
            <label for="projectName" class="col-sm-2 col-form-label">Project Name</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Select Project Name" Value="" />
                </asp:DropDownList>
            </div>
        </div>

        <!-- New Assignment ID -->
        <div class="row mb-3">
            <label for="newAssignmentID" class="col-sm-2 col-form-label">New Assignment ID</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtNewAssignmentID" runat="server" CssClass="form-control" placeholder="Enter new assignment ID"></asp:TextBox>
            </div>
        </div>

        <!-- New Assignment Balance -->
        <div class="row mb-3">
            <label for="newAssignmentBalance" class="col-sm-2 col-form-label">New Assignment Balance</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtNewAssignmentBalance" runat="server" CssClass="form-control" placeholder="Enter new assignment balance"></asp:TextBox>
            </div>
        </div>

        <!-- Current Disbursement ID -->
        <div class="row mb-3">
            <label for="currentDisbursementID" class="col-sm-2 col-form-label">Current Disbursement ID</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtCurrentDisbursementID" runat="server" CssClass="form-control" placeholder="Enter current disbursement ID"></asp:TextBox>
            </div>
        </div>
    </div>
</div>


        <!-- Travel Section -->
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-title">Travel Details</h5>
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <label for="travelDate" class="col-sm-2 col-form-label">Travel Date</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtTravelDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="mileage" class="col-sm-2 col-form-label">Mileage</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtMileage" runat="server" CssClass="form-control" placeholder="Enter mileage" AutoPostBack="True" OnTextChanged="txtMileage_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="travelTotal" class="col-sm-2 col-form-label">Travel Total</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtTravelTotal" runat="server" CssClass="form-control" placeholder="Enter travel total" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="vehicleDescription" class="col-sm-2 col-form-label">Vehicle Description</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtVehicleDescription" runat="server" CssClass="form-control" placeholder="Enter vehicle description"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="travelDescription" class="col-sm-2 col-form-label">Travel Description</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtTravelDescription" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Enter travel description"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <!-- Expense Section -->
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-title">Expense Details</h5>
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <label for="expenseDate" class="col-sm-2 col-form-label">Expense Date</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtExpenseDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="expenseAmount" class="col-sm-2 col-form-label">Expense Amount</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtExpenseAmount" runat="server" CssClass="form-control" placeholder="Enter expense amount"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="expenseType" class="col-sm-2 col-form-label">Expense Type</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-select" DataSourceID="sqlDSFillExpenseType" DataTextField="Expense_Name" DataValueField="Expense_Type_ID">
                            <asp:ListItem Text="Select Expense Type" Value="" />
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sqlDSFillExpenseType" runat="server"
                            ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>"
                            SelectCommand="SELECT * FROM [ExpenseTypetbl]">
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>

        <!-- Submit Button -->
        <div class="text-center">
            <asp:Button ID="btnUpdateDisbursement" runat="server" Text="Update Disbursement" CssClass="btn btn-primary" OnClick="btnUpdateDisbursement_Click" />
        </div>
    </div>
</asp:Content>
