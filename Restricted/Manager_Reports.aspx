<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manager_Reports.aspx.cs" Inherits="ProjectSite.Restricted.Manager_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* Main container for the page */
        .container {
            margin-top: 20px;
            text-align: center;
            max-width: 1200px;
            margin-left: auto;
            margin-right: auto;
        }

        /* Dropdown styling */
        .dropdown {
            font-size: 16px;
            padding: 10px;
            margin: 10px;
            width: 300px;
            display: inline-block;
        }

        /* Report container */
        .report-container {
            margin-top: 30px;
            text-align: center;
            overflow: hidden;
        }

        /* Larger iframe for better visibility */
        .report-iframe {
            width: 100%;
            height: 800px; /* Increased height for better report visibility */
            border: none;
            max-width: 100%;
        }

        /* Header styling */
        .header {
            font-size: 28px; /* Larger font size for improved visibility */
            margin-bottom: 20px;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Main content section -->
    <div class="container">
        <div class="header">Select a Power BI Report</div>
        <asp:DropDownList ID="ddlReports" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlReports_SelectedIndexChanged">
            <asp:ListItem Text="-- Select Report --" Value="" />
            <asp:ListItem Text="Project Expense Overview" Value="https://app.powerbi.com/view?r=eyJrIjoiOGY4MjVkNDEtNjI1Zi00YjY5LTgwMDctMTA4ZDUxMTQ4YWIxIiwidCI6IjIyNjgyN2Q2LWE5ZDAtNDcwZC04YzE1LWIxNDZiMDE5MmQ1MSIsImMiOjh9" />
            <asp:ListItem Text="Client Location and Projects" Value="https://app.powerbi.com/view?r=eyJrIjoiYjJlNjBiYTctZjVjZS00NTlkLTk3OGUtMTQyZjFjMDhhNDNiIiwidCI6IjIyNjgyN2Q2LWE5ZDAtNDcwZC04YzE1LWIxNDZiMDE5MmQ1MSIsImMiOjh9" />
            <asp:ListItem Text="Disbursement Claim Approval Status Report" Value="https://app.powerbi.com/view?r=eyJrIjoiZDQ5ZjAxNWUtOGFiMy00MzU5LWI1MjYtYzA0YjFhOTBmMTNiIiwidCI6IjIyNjgyN2Q2LWE5ZDAtNDcwZC04YzE1LWIxNDZiMDE5MmQ1MSIsImMiOjh9" />
        </asp:DropDownList>

        <!-- Report display area -->
        <div id="reportContainer" class="report-container" runat="server">
            <iframe id="reportFrame" class="report-iframe" runat="server" src="" title="Power BI Report"></iframe>
        </div>
    </div>
</asp:Content>