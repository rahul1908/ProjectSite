<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin_Project.aspx.cs" Inherits="ProjectSite.Restricted.Admin_Project" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* General Container Styling */
        .container {
            max-width: 800px;
            margin: auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            font-family: Arial, sans-serif;
        }

        /* Title Styling */
        h2 {
            text-align: center;
            color: #333;
            font-size: 24px;
            margin-bottom: 20px;
        }

        /* Search Bar Styling */
        .search-bar {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
            gap: 10px;
        }
        
        .search-bar input {
            flex: 1;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ddd;
            border-radius: 6px;
            transition: all 0.3s;
        }
        
        .search-bar input:focus {
            border-color: #007bff;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.2);
        }

        .search-bar button {
            padding: 10px 20px;
            font-size: 14px;
            color: #fff;
            background-color: #007bff;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .search-bar button:hover {
            background-color: #0056b3;
        }

        /* Message Styling */
        #lblMessage {
            display: block;
            text-align: center;
            margin-bottom: 10px;
        }

        /* GridView Styling */
        .grid-container {
            overflow-x: auto;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin-top: 20px;
        }

        .grid-container table {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-container th, .grid-container td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .grid-container th {
            background-color: #007bff;
            color: white;
        }

        .grid-container tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .grid-container tr:hover {
            background-color: #f1f1f1;
        }

        /* Command Buttons Styling */
        .grid-container .button-cell button {
            background-color: #28a745;
            border: none;
            color: white;
            padding: 6px 12px;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        .grid-container .button-cell button.delete {
            background-color: #dc3545;
        }

        .grid-container .button-cell button:hover {
            opacity: 0.85;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Manage Projects</h2>

        <!-- Search Bar -->
        <div class="search-bar">
            <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search by Project Name, ID, or Client ID"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn-secondary" />
        </div>

        <!-- Message Label -->
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

        <!-- GridView Container -->
        <div class="grid-container">
            <asp:GridView ID="GridViewProjects" runat="server" AutoGenerateColumns="False" DataKeyNames="Project_ID"
                CssClass="table" OnRowEditing="GridViewProjects_RowEditing" OnRowUpdating="GridViewProjects_RowUpdating"
                OnRowDeleting="GridViewProjects_RowDeleting" OnRowCancelingEdit="GridViewProjects_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="Project_ID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Client_ID" HeaderText="Client ID" />
                    <asp:BoundField DataField="Project_Name" HeaderText="Project Name" />
                    <asp:BoundField DataField="Project_Description" HeaderText="Description" />
                    <asp:BoundField DataField="Project_Start_date" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Project_End_date" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Project_Budget" HeaderText="Budget" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Manager_ID" HeaderText="Manager ID" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="button-cell">
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="delete" />
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="cancel" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>