<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ProjectSite.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 80%; margin: 0 auto; font-family: Arial, sans-serif; padding: 20px; background-color: #f9f9f9; border-radius: 10px;">
        <h1 style="color: #4CAF50; text-align: center; margin-bottom: 20px;">Contact Optimult</h1>
        
        <p style="text-align: center; color: #555; font-size: 1.1em; line-height: 1.6;">
            Have questions, feedback, or need assistance? Reach out to our team. We’re here to help and will respond to your inquiries as soon as possible.
        </p>

        <!-- Contact Form Section -->
        <asp:Panel ID="ContactFormPanel" runat="server" style="max-width: 600px; margin: 30px auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);">
            <div style="margin-bottom: 15px;">
                <label for="txtName" style="font-weight: bold; color: #333;">Name:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Your Name" style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 5px;" required="true" />
            </div>
            
            <div style="margin-bottom: 15px;">
                <label for="txtEmail" style="font-weight: bold; color: #333;">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 5px;" required="true" />
            </div>
            
            <div style="margin-bottom: 20px;">
                <label for="txtMessage" style="font-weight: bold; color: #333;">Message:</label>
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 5px;" required="true" />
            </div>

            <asp:Button ID="btnSubmit" runat="server" Text="Send Message" OnClick="btnSubmit_Click" 
                style="width: 100%; padding: 10px; background-color: #4CAF50; color: #fff; font-size: 1.1em; font-weight: bold; border: none; border-radius: 5px; cursor: pointer;" />
        </asp:Panel>

        <!-- Contact Details Section -->
        <div style="text-align: center; color: #333; margin-top: 40px;">
            <h2 style="color: #333; margin-top: 30px; text-align: center; font-size: 1.5em;">Our Office</h2>
            <p style="font-size: 1.1em;">
                123 Optimult Street, Innovation Park<br />
                Durban, South Africa
            </p>
            <p style="font-size: 1.1em;">
                Email: contact@optimult.com<br />
                Phone: +27 (0) 123 456 7890
            </p>
        </div>
    </div>
</asp:Content>
