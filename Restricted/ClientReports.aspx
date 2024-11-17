<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientReports.aspx.cs" Inherits="ProjectSite.Restricted.ClientReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Client Reports</h2>
    
    <!-- Embed Power BI Report -->
    <div class="embed-container">
        <iframe width="100%" height="800" src="https://app.powerbi.com/view?r=eyJrIjoiYzY2N2EzM2ItOGE3Mi00NTMxLWE0N2MtOTM1NmExYjYyZGFjIiwidCI6IjIyNjgyN2Q2LWE5ZDAtNDcwZC04YzE1LWIxNDZiMDE5MmQ1MSIsImMiOjh9" frameborder="0" allowFullScreen="true"></iframe>
    </div>

</asp:Content>