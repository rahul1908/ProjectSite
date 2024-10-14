<%@ Page Title="Add Project" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="ProjectSite.Restricted.AddProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- You can add additional head content here if needed -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Enter Project Details</h2>
    
    <!-- ASP.NET Form for project details -->
    <asp:Panel runat="server">
        <!-- Client Dropdown -->
        <div>
            <label for="ddlClient">Client:</label>
            <asp:DropDownList ID="ddlClient" runat="server" DataTextField="ClientName" DataValueField="Client_ID">
            </asp:DropDownList>
        </div>
        <br />

        <!-- Project Name -->
        <div>
            <label for="txtProjectName">Project Name:</label>
            <asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox>
        </div>
        <br />

        <!-- Project Description -->
        <div>
            <label for="txtProjectDescription">Project Description:</label>
            <asp:TextBox ID="txtProjectDescription" runat="server"></asp:TextBox>
        </div>
        <br />

        <!-- Project Start Date -->
        <div>
            <label for="txtStartDate">Project Start Date:</label>
            <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox>
        </div>
        <br />

        <!-- Project End Date -->
        <div>
            <label for="txtEndDate">Project End Date:</label>
            <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>
        </div>
        <br />

        <!-- Project Budget -->
        <div>
            <label for="txtBudget">Project Budget:</label>
            <asp:TextBox ID="txtBudget" runat="server"></asp:TextBox>
        </div>
        <br />

        <!-- Manager ID -->
        <div>
            <label for="txtManagerID">Manager ID:</label>
            <asp:TextBox ID="txtManagerID" runat="server"></asp:TextBox>
        </div>
        <br />

        <!-- Submit Button -->
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    </asp:Panel>
</asp:Content>

