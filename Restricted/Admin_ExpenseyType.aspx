<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin_ExpenseyType.aspx.cs" Inherits="ProjectSite.Restricted.Admin_ExpenseyType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* Main Container */
        .container {
            max-width: 800px;
            margin: auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            font-family: Arial, sans-serif;
        }

        /* Page Header */
        h2, h3 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        /* Table Styling */
        .table-container {
            overflow-x: auto;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            margin-top: 20px;
        }

        .table-container table {
            width: 100%;
            border-collapse: collapse;
        }

        .table-container th, .table-container td {
            padding: 12px;
            text-align: center;
            border-bottom: 1px solid #ddd;
        }

        .table-container th {
            background-color: #007bff;
            color: white;
        }

        .table-container tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .table-container tr:hover {
            background-color: #f1f1f1;
        }

        /* Form Styling */
        .form-group {
            margin-top: 20px;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .form-control {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ddd;
            border-radius: 6px;
            transition: all 0.3s;
            margin-bottom: 15px;
        }

        .form-control:focus {
            border-color: #007bff;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.2);
        }

        /* Buttons */
        .btn-primary, .btn-action {
            padding: 10px 20px;
            font-size: 14px;
            color: #fff;
            background-color: #007bff;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            transition: background-color 0.3s;
            width: 100%;
            text-align: center;
        }

        .btn-primary:hover, .btn-action:hover {
            background-color: #0056b3;
        }

        /* Message Label Styling */
        #lblMessage {
            display: block;
            text-align: center;
            margin-top: 15px;
            font-size: 14px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <!-- Page Header -->
        <h2>Manage Expense Types</h2>

        <!-- Expense Types Table -->
        <div class="table-container">
            <asp:GridView ID="GridViewExpenseType" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="Expense_Type_ID" OnRowEditing="GridViewExpenseType_RowEditing" OnRowDeleting="GridViewExpenseType_RowDeleting" OnRowUpdating="GridViewExpenseType_RowUpdating" OnRowCancelingEdit="GridViewExpenseType_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="Expense_Type_ID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Expense_Name" HeaderText="Expense Type Name" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="button-cell">
                                <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="btn-action" />
                                <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn-action" />
                            </div>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="btn-action" />
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn-action" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <!-- Add New Expense Type Form -->
        <div class="form-group">
            <h3>Add New Expense Type</h3>
            <asp:TextBox ID="txtExpenseName" runat="server" CssClass="form-control" placeholder="Enter Expense Type Name"></asp:TextBox>
            <asp:Button ID="btnAddExpenseType" runat="server" CssClass="btn-primary" Text="Add Expense Type" OnClick="btnAddExpenseType_Click" />
        </div>

        <!-- Feedback Message Label -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-success" />
    </div>
</asp:Content>