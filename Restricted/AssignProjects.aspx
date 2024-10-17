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
                <asp:DropDownList runat="server" ID="EmployeeID" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="EmployeeID" 
                    CssClass="text-danger" ErrorMessage="Employee selection is required." />
            </div>
        </div>

        <!-- Project Dropdown -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProjectID" CssClass="col-md-2 control-label">Project</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProjectID" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ProjectID" 
                    CssClass="text-danger" ErrorMessage="Project selection is required." />
            </div>
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

        <!-- Assignment Claim Balance -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="AssignmentClaimBalance" CssClass="col-md-2 control-label">Claim Balance</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="AssignmentClaimBalance" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="AssignmentClaimBalance"
                    CssClass="text-danger" ErrorMessage="Claim Balance is required." />
            </div>
        </div>

        <!-- Submit Button -->
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="AssignProject_Click" Text="Assign Project" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
