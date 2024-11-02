<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client_PayClaims.aspx.cs" Inherits="ProjectSite.Restricted.Client_PayClaims" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Client Disbursement Payout</h2>

    <!-- Project Selection -->
    <asp:Panel runat="server" CssClass="claims-panel">
        <asp:DropDownList ID="ProjectDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProjectDropDown_SelectedIndexChanged">
            <asp:ListItem Text="Select Project" Value="0"></asp:ListItem>
        </asp:DropDownList>
    </asp:Panel>

    <!-- Disbursements Grid -->
    <asp:GridView ID="DisbursementsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped claims-grid">
        <Columns>
            <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Claim ID" />
            <asp:BoundField DataField="Disbursement_Total_Claim" HeaderText="Total Claim" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Disbursement_Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="PayOutButton" runat="server" Text="Pay Out" CommandName="PayOut" CommandArgument='<%# Eval("Disbursement_Claim_ID") %>' CssClass="btn btn-primary btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>