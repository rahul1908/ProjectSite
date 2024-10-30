<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="ProjectSite.Account.Manage" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false">
            <p class="text-success">Profile updated successfully.</p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Change your account settings</h4>
                <hr />

                <!-- Existing ASPX Structure for Account Settings -->
                <dl class="dl-horizontal">
                    <dt>Password:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Change]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                    <dt>External Logins:</dt>
                    <dd><%: LoginsCount %>
                        <asp:HyperLink NavigateUrl="/Account/ManageLogins" Text="[Manage]" runat="server" />
                    </dd>
                    <dt>Two-Factor Authentication:</dt>
                    <dd>
                        <p>
                            There are no two-factor authentication providers configured.
                        </p>
                    </dd>
                </dl>

                <!-- New Profile Edit Form Fields for Employeetbl -->
                <h4>Edit Profile</h4>
                <asp:ValidationSummary runat="server" CssClass="text-danger" />
                <div class="form-group">
                    <label>Employee ID:</label>
                    <asp:Label ID="lblEmployeeID" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>RSA ID:</label>
                    <asp:TextBox ID="txtRSAID" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Name:</label>
                    <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Surname:</label>
                    <asp:TextBox ID="txtEmployeeSurname" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Job Title:</label>
                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Mobile Number:</label>
                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Address Line 1:</label>
                    <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Address Line 2:</label>
                    <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Suburb:</label>
                    <asp:TextBox ID="txtSuburb" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>City:</label>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Province:</label>
                    <asp:TextBox ID="txtProvince" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Postal Code:</label>
                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>
                

                <!-- Update Button -->
                <asp:Button ID="btnSaveChanges" runat="server" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" />
            </div>
        </div>
    </div>

    <div class="modal fade" id="successModal" tabindex="-1" role="dialog" aria-labelledby="successModalLabel" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="successModalLabel">Update Status</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body" id="modalBody"></div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

</asp:Content>