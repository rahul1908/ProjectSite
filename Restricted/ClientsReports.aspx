<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientsReports.aspx.cs" Inherits="ProjectSite.Restricted.ClientsReports" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Add any specific CSS or JavaScript references for Power BI embedding here if needed -->
    <style>
        .report-container {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 20px;
            background-color: #f9f9f9;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .form-group {
            margin-bottom: 20px;
        }
        .powerbi-frame {
            width: 100%;
            height: 600px;
            border: none;
            border-radius: 8px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  
   

    <div class="container">
        <h2 class="mt-4 mb-4 text-center text-primary">Client Project Reports</h2>

        <!-- Project Selection Dropdown -->
        <div class="form-group">
            <label for="ProjectDropDown" class="form-label fw-bold">Select Project:</label>
            <asp:DropDownList ID="ProjectDropDown" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ProjectDropDown_SelectedIndexChanged">
                <asp:ListItem Text="-- Select Project --" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Power BI Report Embed Section -->
        <div id="ReportContainer" class="report-container mt-4">
            <asp:Label ID="ReportMessage" runat="server" Text="Please select a project to view the report" CssClass="text-info fs-5" />
            
            <!-- This is where the Power BI report iframe will be embedded dynamically -->
            <asp:Literal ID="PowerBiReportFrame" runat="server"></asp:Literal>
        </div>
    </div>

    <script type="text/javascript">
        // Function to load the report inside iframe
        function loadReport(reportUrl, clientName, projectName) {
            var iframe = document.createElement('iframe');
            iframe.src = reportUrl;
            iframe.className = 'powerbi-frame';
            iframe.setAttribute('allowfullscreen', 'true');

            var reportContainer = document.getElementById('ReportContainer');
            reportContainer.appendChild(iframe);
        }
    </script>
</asp:Content>