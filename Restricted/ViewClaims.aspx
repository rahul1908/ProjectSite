
<%@ Page Title="View Claims" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewClaims.aspx.cs" Inherits="ProjectSite.ViewClaims" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>View Claims</h2>
    <p>Here you can view your submitted claims.</p>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Disbursement_Claim_ID" DataSourceID="SqlDataSource1">
    <Columns>
        <asp:CommandField ShowSelectButton="True" />
        <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Disbursement_Claim_ID" InsertVisible="False" ReadOnly="True" SortExpression="Disbursement_Claim_ID" />
        <asp:BoundField DataField="Assignment_ID" HeaderText="Assignment_ID" SortExpression="Assignment_ID" />
        <asp:BoundField DataField="Disbursement_Travel_Total" HeaderText="Disbursement_Travel_Total" SortExpression="Disbursement_Travel_Total" />
        <asp:BoundField DataField="Disbursement_Expense_Total" HeaderText="Disbursement_Expense_Total" SortExpression="Disbursement_Expense_Total" />
        <asp:BoundField DataField="Disbursement_Total_Claim" HeaderText="Disbursement_Total_Claim" SortExpression="Disbursement_Total_Claim" />
        <asp:BoundField DataField="Disbursement_Date" HeaderText="Disbursement_Date" SortExpression="Disbursement_Date" />
        <asp:BoundField DataField="Manager_ID" HeaderText="Manager_ID" SortExpression="Manager_ID" />
        <asp:BoundField DataField="Disbursement_Approved" HeaderText="Disbursement_Approved" SortExpression="Disbursement_Approved" />
    </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString %>" DeleteCommand="DELETE FROM [DisbursementClaimtbl] WHERE [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Disbursement_Travel_Total] = @original_Disbursement_Travel_Total) OR ([Disbursement_Travel_Total] IS NULL AND @original_Disbursement_Travel_Total IS NULL)) AND (([Disbursement_Expense_Total] = @original_Disbursement_Expense_Total) OR ([Disbursement_Expense_Total] IS NULL AND @original_Disbursement_Expense_Total IS NULL)) AND (([Disbursement_Total_Claim] = @original_Disbursement_Total_Claim) OR ([Disbursement_Total_Claim] IS NULL AND @original_Disbursement_Total_Claim IS NULL)) AND [Disbursement_Date] = @original_Disbursement_Date AND (([Manager_ID] = @original_Manager_ID) OR ([Manager_ID] IS NULL AND @original_Manager_ID IS NULL)) AND (([Disbursement_Approved] = @original_Disbursement_Approved) OR ([Disbursement_Approved] IS NULL AND @original_Disbursement_Approved IS NULL))" InsertCommand="INSERT INTO [DisbursementClaimtbl] ([Assignment_ID], [Disbursement_Travel_Total], [Disbursement_Expense_Total], [Disbursement_Total_Claim], [Disbursement_Date], [Manager_ID], [Disbursement_Approved]) VALUES (@Assignment_ID, @Disbursement_Travel_Total, @Disbursement_Expense_Total, @Disbursement_Total_Claim, @Disbursement_Date, @Manager_ID, @Disbursement_Approved)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [DisbursementClaimtbl]" UpdateCommand="UPDATE [DisbursementClaimtbl] SET [Assignment_ID] = @Assignment_ID, [Disbursement_Travel_Total] = @Disbursement_Travel_Total, [Disbursement_Expense_Total] = @Disbursement_Expense_Total, [Disbursement_Total_Claim] = @Disbursement_Total_Claim, [Disbursement_Date] = @Disbursement_Date, [Manager_ID] = @Manager_ID, [Disbursement_Approved] = @Disbursement_Approved WHERE [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Disbursement_Travel_Total] = @original_Disbursement_Travel_Total) OR ([Disbursement_Travel_Total] IS NULL AND @original_Disbursement_Travel_Total IS NULL)) AND (([Disbursement_Expense_Total] = @original_Disbursement_Expense_Total) OR ([Disbursement_Expense_Total] IS NULL AND @original_Disbursement_Expense_Total IS NULL)) AND (([Disbursement_Total_Claim] = @original_Disbursement_Total_Claim) OR ([Disbursement_Total_Claim] IS NULL AND @original_Disbursement_Total_Claim IS NULL)) AND [Disbursement_Date] = @original_Disbursement_Date AND (([Manager_ID] = @original_Manager_ID) OR ([Manager_ID] IS NULL AND @original_Manager_ID IS NULL)) AND (([Disbursement_Approved] = @original_Disbursement_Approved) OR ([Disbursement_Approved] IS NULL AND @original_Disbursement_Approved IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
            <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
            <asp:Parameter Name="original_Disbursement_Travel_Total" Type="Decimal" />
            <asp:Parameter Name="original_Disbursement_Expense_Total" Type="Decimal" />
            <asp:Parameter Name="original_Disbursement_Total_Claim" Type="Decimal" />
            <asp:Parameter Name="original_Disbursement_Date" Type="DateTime" />
            <asp:Parameter Name="original_Manager_ID" Type="Int32" />
            <asp:Parameter Name="original_Disbursement_Approved" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Assignment_ID" Type="Int32" />
            <asp:Parameter Name="Disbursement_Travel_Total" Type="Decimal" />
            <asp:Parameter Name="Disbursement_Expense_Total" Type="Decimal" />
            <asp:Parameter Name="Disbursement_Total_Claim" Type="Decimal" />
            <asp:Parameter Name="Disbursement_Date" Type="DateTime" />
            <asp:Parameter Name="Manager_ID" Type="Int32" />
            <asp:Parameter Name="Disbursement_Approved" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Assignment_ID" Type="Int32" />
            <asp:Parameter Name="Disbursement_Travel_Total" Type="Decimal" />
            <asp:Parameter Name="Disbursement_Expense_Total" Type="Decimal" />
            <asp:Parameter Name="Disbursement_Total_Claim" Type="Decimal" />
            <asp:Parameter Name="Disbursement_Date" Type="DateTime" />
            <asp:Parameter Name="Manager_ID" Type="Int32" />
            <asp:Parameter Name="Disbursement_Approved" Type="String" />
            <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
            <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
            <asp:Parameter Name="original_Disbursement_Travel_Total" Type="Decimal" />
            <asp:Parameter Name="original_Disbursement_Expense_Total" Type="Decimal" />
            <asp:Parameter Name="original_Disbursement_Total_Claim" Type="Decimal" />
            <asp:Parameter Name="original_Disbursement_Date" Type="DateTime" />
            <asp:Parameter Name="original_Manager_ID" Type="Int32" />
            <asp:Parameter Name="original_Disbursement_Approved" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <!-- Add table or other controls to display claims data -->
</asp:Content>
