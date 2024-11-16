<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ProjectSite.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* Reset margins and padding */
        html, body {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
        }

        /* Entire page background styling */
       .about-page {
    position: relative;
    background-image: url('/Images/background.jpg'); /* Replace with your image path */
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center;
    min-height: 100vh; /* Full viewport height */
    display: flex;
    justify-content: center;
    align-items: center;
    overflow: hidden;
    width: 100%; /* Ensure it covers the full width */
}
        /* Transparent overlay to lighten the image */
        .about-page::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Adjust transparency (0.5 = 50%) */
            z-index: 1;
        }

        /* Centered text panel styling */
        .content-panel {
    position: relative;
    z-index: 2;
    background-color: rgba(255, 255, 255, 0.8); /* Light transparent background for the panel */
    padding: 30px;
    border-radius: 10px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3); /* Optional shadow for better visibility */
    max-width: 100%; /* Ensure it covers the full width */
    text-align: center;
    margin: 0; /* Remove any default margins */
}

        /* Header and paragraph styling */
        .content-panel h1 {
            font-size: 2.5rem;
            font-weight: bold;
            color: #333; /* Dark text for contrast */
            margin-bottom: 20px;
          
        }

        .content-panel p {
            font-size: 1.2rem;
            color: #555; /* Dark text for readability */
            line-height: 1.8;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="about-page">
        <!-- Content panel -->
        <div class="content-panel">
            <h1>About Us</h1>
            <p>
                Welcome to Optimult! We are committed to delivering exceptional solutions that 
                meet your business needs. Our team of experts ensures that every project is executed 
                with the highest standards of quality and efficiency.
            </p>
            <p>
                Our mission is to empower organizations with innovative tools and strategies to 
                enhance productivity and achieve success. Thank you for choosing us as your trusted partner.
            </p>
        </div>
    </div>
</asp:Content>


