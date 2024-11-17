<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectSite.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="d-flex justify-content-center align-items-center vh-100">
        <div class="card shadow-lg p-4" style="width: 30rem;">
            <div class="card-body">
                <h2 class="text-center mb-4"><%: Title %></h2>
                <section id="loginForm">
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger text-center">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <form class="needs-validation" novalidate>
                        <div class="form-group mb-3">
                            <label for="Email" class="form-label">Email</label>
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </div>
                        <div class="form-group mb-3">
                            <label for="Password" class="form-label">Password</label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                        <div class="form-check mb-3">
                            <asp:CheckBox runat="server" ID="RememberMe" CssClass="form-check-input" />
                            <label class="form-check-label" for="RememberMe">Remember me?</label>
                        </div>
                        <div class="d-grid gap-2">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary" />
                        </div>
                    </form>
                    <div class="text-center mt-4">
                        <asp:HyperLink runat="server" ID="RegisterHyperLink" CssClass="link-primary">Register as a new user</asp:HyperLink>
                        <br />
                        <%-- Enable this once you have account confirmation enabled for password reset functionality
                        <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" CssClass="link-primary">Forgot your password?</asp:HyperLink>
                        --%>
                    </div>
                </section>
                <hr class="my-4" />
                <section id="socialLoginForm" class="text-center">
                    <h5>Or log in with</h5>
                    <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
                </section>
            </div>
        </div>
    </div>
</asp:Content>