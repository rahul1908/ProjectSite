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
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-block" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

