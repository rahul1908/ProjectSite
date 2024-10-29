
<%@ Page Title="View Claims" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewClaims.aspx.cs" Inherits="ProjectSite.ViewClaims" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>View Disbursement Claims</h2>
    
    <asp:Panel runat="server" CssClass="claims-panel">
        <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="False" CssClass="claims-grid"
                      HeaderStyle-CssClass="header-style" RowStyle-CssClass="row-style" 
                      AlternatingRowStyle-CssClass="alt-row-style"  
                      OnRowCommand="ClaimsGridView_RowCommand">
            <Columns>
                 <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Claim ID" />
        <asp:BoundField DataField="Project_Name" HeaderText="Project Name" /> 
        <asp:BoundField DataField="Disbursement_Travel_Total" HeaderText="Travel Total" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Disbursement_Expense_Total" HeaderText="Expense Total" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Disbursement_Total_Claim" HeaderText="Total Claim" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Disbursement_Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
        <asp:BoundField DataField="Manager_ID" HeaderText="Manager ID" />
        <asp:BoundField DataField="Disbursement_Approved" HeaderText="Approved Status" />
                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="View Details" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel runat="server" CssClass="claims-panel" Visible="false" ID="ExpensePanel">
        <h3>Expense Details</h3>
        <asp:GridView ID="ExpenseGridView" runat="server" AutoGenerateColumns="False" CssClass="claims-grid"
                      HeaderStyle-CssClass="header-style" RowStyle-CssClass="row-style" 
                      AlternatingRowStyle-CssClass="alt-row-style">
            <Columns>
                        <asp:BoundField DataField="Expense_ID" HeaderText="Expense ID" />
        <asp:BoundField DataField="Expense_Name" HeaderText="Expense Name" />
        <asp:BoundField DataField="Expense_Total" HeaderText="Expense Total" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Expense_Date" HeaderText="Expense Date" DataFormatString="{0:MM/dd/yyyy}" />
        <asp:BoundField DataField="Expense_Proof" HeaderText="Expense Proof" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <asp:Panel runat="server" CssClass="claims-panel" Visible="false" ID="TravelPanel">
        <h3>Travel Details</h3>
        <asp:GridView ID="TravelGridView" runat="server" AutoGenerateColumns="False" CssClass="claims-grid"
                      HeaderStyle-CssClass="header-style" RowStyle-CssClass="row-style" 
                      AlternatingRowStyle-CssClass="alt-row-style">
            <Columns>
              <asp:BoundField DataField="Travel_ID" HeaderText="Travel ID" />
        <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Claim ID" />
       
        <asp:BoundField DataField="Travel_Description" HeaderText="Description" />
        <asp:BoundField DataField="Travel_Date" HeaderText="Travel Date" DataFormatString="{0:MM/dd/yyyy}" />
        <asp:BoundField DataField="Travel_Mileage" HeaderText="Mileage" />
        <asp:BoundField DataField="Travel_Vehicle_Description" HeaderText="Vehicle Description" />
        <asp:BoundField DataField="Travel_Total" HeaderText="Travel Total" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Travel_Proof" HeaderText="Proof" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <style>
        /* Overall page styling */
        h2 {
            text-align: center;
            color: #2f4f4f;
            font-weight: bold;
            margin-bottom: 20px;
        }
        .claims-panel {
            padding: 20px;
            background-color: #e6f7e6;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        /* GridView styling */
        .claims-grid {
            width: 100%;
            border-collapse: collapse;
            margin: 0 auto;
        }
        .header-style {
            background-color: #a8d5a2;
            color: #2f4f4f;
            font-weight: bold;
        }
        .row-style {
            background-color: #ffffff;
            color: #333333;
        }
        .alt-row-style {
            background-color: #e6f7e6;
            color: #333333;
        }
        /* Grid cells and borders */
        .claims-grid th, .claims-grid td {
            padding: 10px;
            text-align: center;
            border: 1px solid #a8d5a2;
        }
    </style>
</asp:Content>

