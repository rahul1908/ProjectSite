<%@ Page Title="Add Project" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="ProjectSite.Restricted.AddProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- You can add additional head content here if needed -->
  <!-- Add Bootstrap for styling -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-primary text-white">
                        <h3>Enter Project Details</h3>
                    </div>
                    <div class="card-body">
                        <asp:Panel runat="server">
                            <div class="form-group">
                                <label for="ddlClient">Client:</label>
                                <asp:DropDownList 
                                    ID="ddlClient" 
                                    runat="server" 
                                    CssClass="form-control" 
                                    DataTextField="ClientName" 
                                    DataValueField="Client_ID">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="txtProjectName">Project Name:</label>
                                <asp:TextBox 
                                    ID="txtProjectName" 
                                    runat="server" 
                                    CssClass="form-control" 
                                    placeholder="Enter project name">
                                </asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtProjectDescription">Project Description:</label>
                                <asp:TextBox 
                                    ID="txtProjectDescription" 
                                    runat="server" 
                                    CssClass="form-control" 
                                    placeholder="Enter project description" 
                                    TextMode="MultiLine" 
                                    Rows="3">
                                </asp:TextBox>
                            </div>

                            <div class="form-row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtStartDate">Project Start Date:</label>
                                        <asp:TextBox 
                                            ID="txtStartDate" 
                                            runat="server" 
                                            CssClass="form-control" 
                                            TextMode="Date">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtEndDate">Project End Date:</label>
                                        <asp:TextBox 
                                            ID="txtEndDate" 
                                            runat="server" 
                                            CssClass="form-control" 
                                            TextMode="Date">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="txtBudget">Project Budget:</label>
                                <asp:TextBox 
                                    ID="txtBudget" 
                                    runat="server" 
                                    CssClass="form-control" 
                                    placeholder="Enter project budget (e.g., 10000.00)">
                                </asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtManagerID">Manager ID:</label>
                                <asp:TextBox 
                                    ID="txtManagerID" 
                                    runat="server" 
                                    CssClass="form-control" 
                                    placeholder="Enter manager ID">
                                </asp:TextBox>
                            </div>

                            <!-- Submit Button with hover effect -->
                            <div class="form-group">
                                <asp:Button 
                                    ID="btnSubmit" 
                                    runat="server" 
                                    Text="Submit" 
                                    CssClass="btn btn-primary btn-block"
                                    OnClick="btnSubmit_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Optional: Add feedback messages for form submission -->
    <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>

    <!-- Add Bootstrap JS and dependencies -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>

