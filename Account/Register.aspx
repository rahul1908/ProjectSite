<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ProjectSite.Account.RegisterClient" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Client Registration</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Register as a new client</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <!-- Client Name -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ClientName" CssClass="col-md-2 control-label">Client Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ClientName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ClientName"
                    CssClass="text-danger" ErrorMessage="Client name is required." />
            </div>
        </div>

        <!-- Client Email -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ClientEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ClientEmail" TextMode="Email" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ClientEmail"
                    CssClass="text-danger" ErrorMessage="Email is required." />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="ClientEmail"
                    ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" CssClass="text-danger"
                    ErrorMessage="Please enter a valid email address." />
            </div>
        </div>
         <!-- Client Password -->
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ClientPassword" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ClientPassword" TextMode="Password" CssClass="form-control" placeholder="Enter Password" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ClientPassword"
                    CssClass="text-danger" ErrorMessage="Password is required." />
            </div>
        </div>

        <!-- Client Tier -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ClientTier" CssClass="col-md-2 control-label">Client Tier</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ClientTier" CssClass="form-control">
                    <asp:ListItem Value="1">Tier 1</asp:ListItem>
                    <asp:ListItem Value="2">Tier 2</asp:ListItem>
                    <asp:ListItem Value="3">Tier 3</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ClientTier"
                    CssClass="text-danger" ErrorMessage="Client tier is required." />
            </div>
        </div>

        <!-- Client Rates -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ClientRates" CssClass="col-md-2 control-label">Mileage Rate- (eg. R6 per KM)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ClientRates" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ClientRates"
                    CssClass="text-danger" ErrorMessage="Client rates are required." />
            </div>
        </div>

        <!-- Address Line 1 -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="AddressLine1" CssClass="col-md-2 control-label">Address Line 1</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="AddressLine1" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="AddressLine1"
                    CssClass="text-danger" ErrorMessage="Address line 1 is required." />
            </div>
        </div>

        <!-- Address Line 2 -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="AddressLine2" CssClass="col-md-2 control-label">Address Line 2</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="AddressLine2" CssClass="form-control" />
            </div>
        </div>

        <!-- Suburb -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Suburb" CssClass="col-md-2 control-label">Suburb</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Suburb" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Suburb"
                    CssClass="text-danger" ErrorMessage="Suburb is required." />
            </div>
        </div>

        <!-- City -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="City" CssClass="col-md-2 control-label">City</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="City" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="City"
                    CssClass="text-danger" ErrorMessage="City is required." />
            </div>
        </div>

        <!-- Province -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Province" CssClass="col-md-2 control-label">Province</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Province" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Province"
                    CssClass="text-danger" ErrorMessage="Province is required." />
            </div>
        </div>

        <!-- Postal Code -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PostalCode" CssClass="col-md-2 control-label">Postal Code</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="PostalCode" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PostalCode"
                    CssClass="text-danger" ErrorMessage="Postal code is required." />
            </div>
        </div>

        <!-- Submit Button -->
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
