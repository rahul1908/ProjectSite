<%@ Page Title="Add New Employee" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="ProjectSite.Restricted.AddEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Include Bootstrap for styling -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center">Add New Employee</h2>

        <!-- Error Message Display -->
        <asp:Label ID="ErrorMessage" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>

        <!-- Employee Form -->
        <form class="needs-validation" novalidate>
            <div class="row">
                <div class="col-md-6">
                    <!-- First Name -->
                    <div class="form-group">
                        <label for="EmployeeName">First Name</label>
                        <asp:TextBox ID="EmployeeName" CssClass="form-control" runat="server" Placeholder="Enter first name" required></asp:TextBox>
                        <div class="invalid-feedback">Please enter the first name.</div>
                    </div>
                </div>

                <div class="col-md-6">
                    <!-- Surname -->
                    <div class="form-group">
                        <label for="EmployeeSurname">Surname</label>
                        <asp:TextBox ID="EmployeeSurname" CssClass="form-control" runat="server" Placeholder="Enter surname" required></asp:TextBox>
                        <div class="invalid-feedback">Please enter the surname.</div>
                    </div>
                </div>
            </div>

            <!-- Job Title -->
            <div class="form-group">
                <label for="EmployeeJobTitle">Job Title</label>
                <asp:TextBox ID="EmployeeJobTitle" CssClass="form-control" runat="server" Placeholder="Enter job title" required></asp:TextBox>
                <div class="invalid-feedback">Please enter the job title.</div>
            </div>

            <!-- Mobile Number -->
            <div class="form-group">
                <label for="EmployeeMobileNumber">Mobile Number</label>
                <asp:TextBox ID="EmployeeMobileNumber" CssClass="form-control" runat="server" Placeholder="Enter mobile number" required></asp:TextBox>
                <div class="invalid-feedback">Please enter the mobile number.</div>
            </div>

            <!-- Address Section -->
            <div class="form-group">
                <label for="AddressLine1">Address Line 1</label>
                <asp:TextBox ID="AddressLine1" CssClass="form-control" runat="server" Placeholder="Enter address line 1" required></asp:TextBox>
                <div class="invalid-feedback">Please enter address line 1.</div>
            </div>

            <div class="form-group">
                <label for="AddressLine2">Address Line 2</label>
                <asp:TextBox ID="AddressLine2" CssClass="form-control" runat="server" Placeholder="Enter address line 2"></asp:TextBox>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <!-- Suburb -->
                    <div class="form-group">
                        <label for="Suburb">Suburb</label>
                        <asp:TextBox ID="Suburb" CssClass="form-control" runat="server" Placeholder="Enter suburb"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-4">
                    <!-- City -->
                    <div class="form-group">
                        <label for="City">City</label>
                        <asp:TextBox ID="City" CssClass="form-control" runat="server" Placeholder="Enter city"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-4">
                    <!-- Province -->
                    <div class="form-group">
                        <label for="Province">Province</label>
                        <asp:TextBox ID="Province" CssClass="form-control" runat="server" Placeholder="Enter province"></asp:TextBox>
                    </div>
                </div>
            </div>

            <!-- Postal Code -->
            <div class="form-group">
                <label for="PostalCode">Postal Code</label>
                <asp:TextBox ID="PostalCode" CssClass="form-control" runat="server" Placeholder="Enter postal code"></asp:TextBox>
            </div>

            <!-- Email -->
            <div class="form-group">
                <label for="EmployeeEmail">Email</label>
                <asp:TextBox ID="EmployeeEmail" CssClass="form-control" runat="server" Placeholder="Enter email" required></asp:TextBox>
                <div class="invalid-feedback">Please enter a valid email.</div>
            </div>

            <!-- Password -->
            <div class="form-group">
                <label for="EmployeePassword">Password</label>
                <asp:TextBox ID="EmployeePassword" CssClass="form-control" TextMode="Password" runat="server" Placeholder="Enter password" required></asp:TextBox>
                <div class="invalid-feedback">Please enter a password.</div>
            </div>

            <!-- Submit Button -->
            <asp:Button ID="AddEmployeeBtn" runat="server" CssClass="btn btn-primary" Text="Add Employee" OnClick="CreateEmployee_Click" />
        </form>
    </div>

    <!-- Bootstrap JS & Form Validation -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.10.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <script>
        // Bootstrap form validation
        (function () {
            'use strict';
            window.addEventListener('load', function () {
                var forms = document.getElementsByClassName('needs-validation');
                var validation = Array.prototype.filter.call(forms, function (form) {
                    form.addEventListener('submit', function (event) {
                        if (form.checkValidity() === false) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        form.classList.add('was-validated');
                    }, false);
                });
            }, false);
        })();
    </script>
</asp:Content>

