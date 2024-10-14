<%@ Page Title="Access Denied" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="ProjectSite.ErrorPages.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .access-denied-message {
            text-align: center;
            margin-top: 50px;
            font-family: Arial, sans-serif;
        }
        .access-denied-message h1 {
            font-size: 36px;
            color: #d9534f; /* Red color to indicate error */
        }
        .access-denied-message p {
            font-size: 18px;
            color: #5a5a5a;
        }
        .access-denied-message a {
            color: #337ab7; /* Bootstrap link color */
            text-decoration: none;
        }
        .access-denied-message a:hover {
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="access-denied-message">
        <h1>Access Denied</h1>
        <p>You do not have permission to access this page.</p>
        <p>If you believe this is an error, please contact the system administrator.</p>
        <p><a runat="server" href="~/">Return to Home Page</a></p>
    </div>
</asp:Content>
