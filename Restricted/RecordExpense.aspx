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
        </br>
        </br>
        <asp:Label ID="Label1" runat="server" Text="What do you want to record?"></asp:Label>
        </br>
        <asp:CheckBox ID="chbTravel" runat="server" Text="Select to record travel details" />
        </br>
        <asp:CheckBox ID="chbExpense" runat="server" Text="Select to record expense details" />
        </br>
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
            <asp:SqlDataSource ID="sqlDSInsertTravel" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" DeleteCommand="DELETE FROM [Traveltbl] WHERE [Travel_ID] = @original_Travel_ID AND [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Travel_Description] = @original_Travel_Description) OR ([Travel_Description] IS NULL AND @original_Travel_Description IS NULL)) AND (([Travel_Date] = @original_Travel_Date) OR ([Travel_Date] IS NULL AND @original_Travel_Date IS NULL)) AND (([Travel_Mileage] = @original_Travel_Mileage) OR ([Travel_Mileage] IS NULL AND @original_Travel_Mileage IS NULL)) AND (([Travel_Vehicle_Description] = @original_Travel_Vehicle_Description) OR ([Travel_Vehicle_Description] IS NULL AND @original_Travel_Vehicle_Description IS NULL)) AND (([Travel_Total] = @original_Travel_Total) OR ([Travel_Total] IS NULL AND @original_Travel_Total IS NULL)) AND (([Travel_Proof] = @original_Travel_Proof) OR ([Travel_Proof] IS NULL AND @original_Travel_Proof IS NULL))" InsertCommand="INSERT INTO [Traveltbl] ([Disbursement_Claim_ID], [Assignment_ID], [Travel_Description], [Travel_Date], [Travel_Mileage], [Travel_Vehicle_Description], [Travel_Total], [Travel_Proof]) VALUES (@Disbursement_Claim_ID, @Assignment_ID, @Travel_Description, @Travel_Date, @Travel_Mileage, @Travel_Vehicle_Description, @Travel_Total, @Travel_Proof)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Traveltbl]" UpdateCommand="UPDATE [Traveltbl] SET [Disbursement_Claim_ID] = @Disbursement_Claim_ID, [Assignment_ID] = @Assignment_ID, [Travel_Description] = @Travel_Description, [Travel_Date] = @Travel_Date, [Travel_Mileage] = @Travel_Mileage, [Travel_Vehicle_Description] = @Travel_Vehicle_Description, [Travel_Total] = @Travel_Total, [Travel_Proof] = @Travel_Proof WHERE [Travel_ID] = @original_Travel_ID AND [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Travel_Description] = @original_Travel_Description) OR ([Travel_Description] IS NULL AND @original_Travel_Description IS NULL)) AND (([Travel_Date] = @original_Travel_Date) OR ([Travel_Date] IS NULL AND @original_Travel_Date IS NULL)) AND (([Travel_Mileage] = @original_Travel_Mileage) OR ([Travel_Mileage] IS NULL AND @original_Travel_Mileage IS NULL)) AND (([Travel_Vehicle_Description] = @original_Travel_Vehicle_Description) OR ([Travel_Vehicle_Description] IS NULL AND @original_Travel_Vehicle_Description IS NULL)) AND (([Travel_Total] = @original_Travel_Total) OR ([Travel_Total] IS NULL AND @original_Travel_Total IS NULL)) AND (([Travel_Proof] = @original_Travel_Proof) OR ([Travel_Proof] IS NULL AND @original_Travel_Proof IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_Travel_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Travel_Description" Type="String" />
                    <asp:Parameter Name="original_Travel_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Travel_Mileage" Type="Int32" />
                    <asp:Parameter Name="original_Travel_Vehicle_Description" Type="String" />
                    <asp:Parameter Name="original_Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Travel_Proof" Type="Boolean" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Disbursement_Claim_ID" Type="Int32" />
                    <asp:SessionParameter Name="Assignment_ID" SessionField="project_id" Type="Int32" />
                    <asp:ControlParameter ControlID="txtTravelDescription" Name="Travel_Description" PropertyName="Text" Type="String" />
                    <asp:ControlParameter Name="Travel_Date" Type="DateTime" />
                    <asp:ControlParameter ControlID="txtMileage" Name="Travel_Mileage" PropertyName="Text" Type="Int32" />
                    <asp:ControlParameter ControlID="txtVehicleDescription" Name="Travel_Vehicle_Description" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtTravelTotal" Name="Travel_Total" PropertyName="Text" Type="Decimal" />
                    <asp:Parameter DefaultValue="" Name="Travel_Proof" Type="Boolean" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="Travel_Description" Type="String" />
                    <asp:Parameter Name="Travel_Date" Type="DateTime" />
                    <asp:Parameter Name="Travel_Mileage" Type="Int32" />
                    <asp:Parameter Name="Travel_Vehicle_Description" Type="String" />
                    <asp:Parameter Name="Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="Travel_Proof" Type="Boolean" />
                    <asp:Parameter Name="original_Travel_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Travel_Description" Type="String" />
                    <asp:Parameter Name="original_Travel_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Travel_Mileage" Type="Int32" />
                    <asp:Parameter Name="original_Travel_Vehicle_Description" Type="String" />
                    <asp:Parameter Name="original_Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Travel_Proof" Type="Boolean" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSInsertExpense" runat="server"></asp:SqlDataSource>
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" />
        </div>
    </div>
</asp:Content>
