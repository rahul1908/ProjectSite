<%@ Page Title="Record Expense" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecordExpense.aspx.cs" Inherits="ProjectSite.Restricted.RecordExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">Record Disbursement</h2>
         <h4 class="text-center mb-3">Selected Project:</h4>
        <asp:Label ID="LabelProjectName" runat="server" Text="" Font-Bold="True" Font-Size="Large"></asp:Label>
        <!-- Travel Section -->
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-title">Travel Details</h5>
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <label for="travelDate" class="col-sm-2 col-form-label">Travel Date</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtTravelDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="mileage" class="col-sm-2 col-form-label">Mileage</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtMileage" runat="server" CssClass="form-control" placeholder="Enter mileage"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="travelTotal" class="col-sm-2 col-form-label">Travel Total</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtTravelTotal" runat="server" CssClass="form-control" placeholder="Enter travel total"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="vehicleDescription" class="col-sm-2 col-form-label">Vehicle Description</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtVehicleDescription" runat="server" CssClass="form-control" placeholder="Enter vehicle description"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="travelDescription" class="col-sm-2 col-form-label">Description</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtTravelDescription" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Enter travel description"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <!-- Expense Section -->
        <div class="card mb-4">
            <div class="card-header">
                <h5 class="card-title">Expense Details</h5>
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <label for="expenseDate" class="col-sm-2 col-form-label">Expense Date</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtExpenseDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="expenseAmount" class="col-sm-2 col-form-label">Expense Amount</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtExpenseAmount" runat="server" CssClass="form-control" placeholder="Enter expense amount"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="expenseType" class="col-sm-2 col-form-label">Expense Type</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Select Expense Type" Value=""></asp:ListItem>
                            <asp:ListItem Text="Accommodation" Value="Accommodation"></asp:ListItem>
                            <asp:ListItem Text="Food" Value="Food"></asp:ListItem>
                            <asp:ListItem Text="Supplies" Value="Supplies"></asp:ListItem>
                            <asp:ListItem Text="Miscellaneous" Value="Miscellaneous"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <!-- Submit Button -->
        <div class="text-center">
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" />
        </div>
    </div>
</asp:Content>
