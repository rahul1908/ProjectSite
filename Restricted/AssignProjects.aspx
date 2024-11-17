<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignProjects.aspx.cs" Inherits="ProjectSite.Restricted.AssignProjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Assign Projects</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Assign Project to Employee</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <!-- Employee Dropdown -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="EmployeeID" CssClass="col-md-2 control-label">Employee</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="EmployeeID" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="EmployeeID_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="EmployeeID" 
                    CssClass="text-danger" ErrorMessage="Employee selection is required." />
            </div>
        </div>

        <!-- Employee Details GridView -->
        <div class="form-group">
            <asp:GridView ID="EmployeeDetailsGrid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Employee_Name" HeaderText="Name" />
                    <asp:BoundField DataField="Employee_Surname" HeaderText="Surname" />
                    <asp:BoundField DataField="Employee_Job_Title" HeaderText="Job Title" />
                    <asp:BoundField DataField="Province" HeaderText="Province" />
                </Columns>
            </asp:GridView>
        </div>

        <!-- Project Dropdown -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProjectID" CssClass="col-md-2 control-label">Project</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProjectID" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ProjectID_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectID" 
                    CssClass="text-danger" ErrorMessage="Project selection is required." />
            </div>
        </div>
           <div class="form-group">
            <asp:Label runat="server" CssClass="col-md-2 control-label">Search Projects</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProjectSearchBox" CssClass="form-control" 
                    placeholder="Search by name or description" AutoPostBack="true" OnTextChanged="ProjectSearchBox_TextChanged" />
            </div>
        </div>

        <!-- Project Details GridView -->
        <div class="form-group">
            <asp:GridView ID="ProjectDetailsGrid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Project_Name" HeaderText="Project Name" />
                    <asp:BoundField DataField="Project_Start_date" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Project_End_date" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Project_Description" HeaderText="Description" />
                    <asp:BoundField DataField="Project_Budget" HeaderText="Budget" DataFormatString="R{0:N2}" />
                    
                </Columns>
                  

            </asp:GridView>
            
        </div>

           <!-- Remaining Budget Display -->
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" CssClass="text-success" Font-Size="Large" />
        </div>

        <!-- Date Assigned -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DateAssigned" CssClass="col-md-2 control-label">Date Assigned</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="DateAssigned" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="DateAssigned" 
                    CssClass="text-danger" ErrorMessage="Date is required." />
            </div>
        </div>

        <!-- Assignment Claim Max -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="AssignmentClaimMax" CssClass="col-md-2 control-label">Max Claim</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="AssignmentClaimMax" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="AssignmentClaimMax"
                    CssClass="text-danger" ErrorMessage="Max Claim is required." />
            </div>
        </div>

     <asp:Label ID="RemainingBudgetLabel" runat="server" CssClass="text-success" />

        <!-- Submit Button -->
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="AssignProject_Click" Text="Assign Project" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>