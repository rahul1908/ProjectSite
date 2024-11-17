<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientReports.aspx.cs" Inherits="ProjectSite.Restricted.ClientReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .container {
            margin-top: 20px;
            text-align: center;
            max-width: 100%;
            margin-left: auto;
            margin-right: auto;
        }

        .report-container {
            margin-top: 30px;
            text-align: center;
        }

        .report-iframe {
            width: 100%;
            height: 600px;
            border: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <!-- Container for the report iframe -->
        <div class="report-container">
            <iframe id="reportFrame" class="report-iframe" runat="server" src="" title="Power BI Report"></iframe>
        </div>
    </div>
</asp:Content>