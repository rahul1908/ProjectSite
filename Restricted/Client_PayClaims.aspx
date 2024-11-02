<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client_PayClaims.aspx.cs" Inherits="ProjectSite.Restricted.Client_PayClaims" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-center mb-4">Client Disbursement Payout</h2>

        <!-- Project Selection -->
        <div class="row mb-3">
            <div class="col-md-6 offset-md-3">
                <asp:Panel runat="server" CssClass="claims-panel">
                    <div class="input-group">
                        <asp:DropDownList ID="ProjectDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProjectDropDown_SelectedIndexChanged" CssClass="form-select">
                            <asp:ListItem Text="Select Project" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <!-- Disbursements Grid -->
        <div class="table-responsive">
            <asp:GridView ID="DisbursementsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered claims-grid" OnRowCommand="DisbursementsGridView_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Claim ID" />
                    <asp:BoundField DataField="Disbursement_Total_Claim" HeaderText="Total Claim" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Disbursement_Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="PayOutButton" runat="server" Text="Pay Out" CommandName="PayOut" CommandArgument='<%# Eval("Disbursement_Claim_ID") %>' CssClass="btn btn-success btn-sm" OnClientClick="return confirm('Are you sure you want to process this payout?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="table-header" />
                <RowStyle CssClass="table-row" />
                <FooterStyle CssClass="table-footer" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>