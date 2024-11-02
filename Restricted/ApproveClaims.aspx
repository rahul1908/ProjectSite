<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveClaims.aspx.cs" Inherits="ProjectSite.Restricted.ApproveClaims" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center my-4 header-text">Approve Claims</h2>

    <!-- Project Selection Panel -->
    <asp:Panel runat="server" CssClass="claims-panel">
        <div class="mb-3">
            <asp:Label ID="ProjectLabel" runat="server" AssociatedControlID="ProjectDropDown" Text="Select Project:" CssClass="form-label fw-bold" />
            <asp:DropDownList ID="ProjectDropDown" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ProjectDropDown_SelectedIndexChanged">
                <asp:ListItem Text="Select Project" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Claims GridView -->
        <asp:GridView ID="ClaimsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover claims-grid" OnRowCommand="ClaimsGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Claim ID" />
                <asp:BoundField DataField="Assignment_ID" HeaderText="Assignment ID" />
                <asp:BoundField DataField="Disbursement_Travel_Total" HeaderText="Travel Total" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Disbursement_Expense_Total" HeaderText="Expense Total" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Disbursement_Total_Claim" HeaderText="Total Claim" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Disbursement_Date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <div class="d-flex gap-2">
                            <asp:Button ID="ApproveButton" runat="server" Text="Approve" CommandName="Approve" CommandArgument='<%# Eval("Disbursement_Claim_ID") %>' CssClass="btn btn-success btn-sm" />
                            <asp:Button ID="RejectButton" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("Disbursement_Claim_ID") %>' CssClass="btn btn-danger btn-sm" />
                            <asp:Button ID="ViewDetailsButton" runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%# Eval("Disbursement_Claim_ID") %>' CssClass="btn btn-info btn-sm" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <!-- Claim Details Panel -->
    <asp:Panel ID="ClaimDetailsPanel" runat="server" CssClass="details-panel" Visible="false">
        <h3 class="header-text">Claim Details</h3>

        <!-- Employee Information Section -->
        <h4 class="header-text mt-4">Employee Information</h4>
        <div class="mb-3">
            <asp:Label ID="EmployeeNameLabel" runat="server" CssClass="label-info" />
        </div>
        <div class="mb-3">
            <asp:Label ID="EmployeeJobTitleLabel" runat="server" CssClass="label-info" />
        </div>
        <div class="mb-3">
            <asp:Label ID="EmployeeContactLabel" runat="server" CssClass="label-info" />
        </div>

        <!-- Travel Details GridView -->
        <h4 class="header-text mt-4">Travel Details</h4>
        <asp:GridView ID="TravelDetailsGridView" runat="server" AutoGenerateColumns="True" CssClass="table table-bordered details-grid" />

        <!-- Expense Details GridView -->
        <h4 class="header-text mt-4">Expense Details</h4>
        <asp:GridView ID="ExpenseDetailsGridView" runat="server" AutoGenerateColumns="True" CssClass="table table-bordered details-grid" />
    </asp:Panel>

    <!-- Include Bootstrap CSS and custom styles directly in the page or through a linked stylesheet -->
    <style>
        /* Custom Styles */
        .claims-panel {
            margin-top: 2rem;
            background-color: #f8f9fa;
            padding: 2rem;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .details-panel {
            margin-top: 2rem;
            background-color: #ffffff;
            padding: 2rem;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .claims-grid, .details-grid {
            margin-top: 1rem;
        }
        .header-text {
            color: #4a5568;
        }
        .label-info {
            font-weight: 500;
            color: #6c757d;
        }
    </style>
</asp:Content>