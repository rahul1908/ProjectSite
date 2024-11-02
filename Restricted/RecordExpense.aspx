<%@ Page Title="Record Expense" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecordExpense.aspx.cs" Inherits="ProjectSite.Restricted.RecordExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
       <h2 class="text-center mb-4" style="background-color: #a8e6cf;">Record Disbursement</h2>
<asp:Label ID="lblSelectedProject" runat="server" Text="Selected Project:" CssClass="text-center mb-3" 
           Style="display: block; text-align: center; font-weight: bold; font-size: large; background-color: #a8e6cf; padding: 10px;" />

        </br>
        <asp:Label ID="lblassignID" runat="server" Text="Label"
            Style="font-weight: bold; font-size: 16px;"></asp:Label>
        </br>
        <asp:Label ID="lblAssignmentBalance" runat="server" Text="Label"
            Style="font-weight: bold; font-size: 16px;"></asp:Label>
         </br>
        </br>
       <div style="text-align: center; background-color: #e6f9e6; padding: 20px; border-radius: 10px;">
           <asp:Label ID="Label1" runat="server" Text="What do you want to record?"
           style="font-weight: bold; font-size: 24px; font-family: Arial, sans-serif;"></asp:Label>

           <br />
           <asp:CheckBox ID="chbTravel" runat="server" Text="   Select to record travel details"
               Font-Bold="False" Font-Size="Large" AutoPostBack="True"
               OnCheckedChanged="chbTravel_CheckedChanged" />
           <br />
           <asp:CheckBox ID="chbExpense" runat="server" Text="  Select to record expense details"
               Font-Bold="False" Font-Size="Large" AutoPostBack="True"
               OnCheckedChanged="chbExpense_CheckedChanged" />
           <br />
       </div>

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
                        <asp:TextBox ID="txtMileage" runat="server" CssClass="form-control" placeholder="Enter mileage" AutoPostBack="True" OnTextChanged="txtMileage_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="travelTotal" class="col-sm-2 col-form-label">Travel Total</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtTravelTotal" runat="server" CssClass="form-control" placeholder="Enter travel total" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="vehicleDescription" class="col-sm-2 col-form-label">Vehicle Description</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtVehicleDescription" runat="server" CssClass="form-control" placeholder="Enter vehicle description"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-3">
                    <label for="travelDescription" class="col-sm-2 col-form-label">Travel Description</label>
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
                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-select" DataSourceID="sqlDSFillExpenseType" DataTextField="Expense_Name" DataValueField="Expense_Type_ID">
                            <asp:ListItem Text="Select Expense Type" Value=""></asp:ListItem>
                            <%--<asp:ListItem Text="Accommodation" Value="Accommodation"></asp:ListItem>
                            <asp:ListItem Text="Food" Value="Food"></asp:ListItem>
                            <asp:ListItem Text="Supplies" Value="Supplies"></asp:ListItem>
                            <asp:ListItem Text="Miscellaneous" Value="Miscellaneous"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <!-- Submit Button -->
        <div class="text-center">
            <asp:SqlDataSource ID="sqlDSInsertTravel" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" DeleteCommand="DELETE FROM [Traveltbl] WHERE [Travel_ID] = @original_Travel_ID AND [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Travel_Description] = @original_Travel_Description) OR ([Travel_Description] IS NULL AND @original_Travel_Description IS NULL)) AND (([Travel_Date] = @original_Travel_Date) OR ([Travel_Date] IS NULL AND @original_Travel_Date IS NULL)) AND (([Travel_Mileage] = @original_Travel_Mileage) OR ([Travel_Mileage] IS NULL AND @original_Travel_Mileage IS NULL)) AND (([Travel_Vehicle_Description] = @original_Travel_Vehicle_Description) OR ([Travel_Vehicle_Description] IS NULL AND @original_Travel_Vehicle_Description IS NULL)) AND (([Travel_Total] = @original_Travel_Total) OR ([Travel_Total] IS NULL AND @original_Travel_Total IS NULL)) AND (([Travel_Proof] = @original_Travel_Proof) OR ([Travel_Proof] IS NULL AND @original_Travel_Proof IS NULL))" InsertCommand="INSERT INTO Traveltbl(Disbursement_Claim_ID, Assignment_ID, Travel_Description, Travel_Date, Travel_Mileage, Travel_Vehicle_Description, Travel_Total, Travel_Proof) VALUES (@Disbursement_Claim_ID, @Assignment_ID, @Travel_Description, @Travel_Date, @Travel_Mileage, @Travel_Vehicle_Description, @Travel_Total, 0)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Traveltbl]" UpdateCommand="UPDATE [Traveltbl] SET [Disbursement_Claim_ID] = @Disbursement_Claim_ID, [Assignment_ID] = @Assignment_ID, [Travel_Description] = @Travel_Description, [Travel_Date] = @Travel_Date, [Travel_Mileage] = @Travel_Mileage, [Travel_Vehicle_Description] = @Travel_Vehicle_Description, [Travel_Total] = @Travel_Total, [Travel_Proof] = @Travel_Proof WHERE [Travel_ID] = @original_Travel_ID AND [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Travel_Description] = @original_Travel_Description) OR ([Travel_Description] IS NULL AND @original_Travel_Description IS NULL)) AND (([Travel_Date] = @original_Travel_Date) OR ([Travel_Date] IS NULL AND @original_Travel_Date IS NULL)) AND (([Travel_Mileage] = @original_Travel_Mileage) OR ([Travel_Mileage] IS NULL AND @original_Travel_Mileage IS NULL)) AND (([Travel_Vehicle_Description] = @original_Travel_Vehicle_Description) OR ([Travel_Vehicle_Description] IS NULL AND @original_Travel_Vehicle_Description IS NULL)) AND (([Travel_Total] = @original_Travel_Total) OR ([Travel_Total] IS NULL AND @original_Travel_Total IS NULL)) AND (([Travel_Proof] = @original_Travel_Proof) OR ([Travel_Proof] IS NULL AND @original_Travel_Proof IS NULL))">
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
                    <asp:SessionParameter Name="Disbursement_Claim_ID" SessionField="MaxDisbursementClaimID" Type="Int32" />
                    <asp:SessionParameter Name="Assignment_ID" SessionField="assignment_id" Type="Int32" />
                    <asp:ControlParameter ControlID="txtTravelDescription" Name="Travel_Description" PropertyName="Text" Type="String" />
                    <asp:ControlParameter Name="Travel_Date" Type="DateTime" ControlID="txtTravelDate" PropertyName="Text" />
                    <asp:ControlParameter ControlID="txtMileage" Name="Travel_Mileage" PropertyName="Text" Type="Int32" />
                    <asp:ControlParameter ControlID="txtVehicleDescription" Name="Travel_Vehicle_Description" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtTravelTotal" Name="Travel_Total" PropertyName="Text" Type="Decimal" />
                    <asp:Parameter Name="Travel_Proof" Type="Boolean" DefaultValue="" />
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
            <asp:SqlDataSource ID="sqlDSInsertExpense" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" OnSelecting="sqlDSInsertExpense_Selecting" SelectCommand="SELECT * FROM [Expensetbl]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [Expensetbl] WHERE [Expense_ID] = @original_Expense_ID AND (([Disbursement_Claim_ID] = @original_Disbursement_Claim_ID) OR ([Disbursement_Claim_ID] IS NULL AND @original_Disbursement_Claim_ID IS NULL)) AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Expense_Type_ID] = @original_Expense_Type_ID) OR ([Expense_Type_ID] IS NULL AND @original_Expense_Type_ID IS NULL)) AND (([Expense_Total] = @original_Expense_Total) OR ([Expense_Total] IS NULL AND @original_Expense_Total IS NULL)) AND (([Expense_Date] = @original_Expense_Date) OR ([Expense_Date] IS NULL AND @original_Expense_Date IS NULL)) AND (([Expense_Proof] = @original_Expense_Proof) OR ([Expense_Proof] IS NULL AND @original_Expense_Proof IS NULL))" InsertCommand="INSERT INTO Expensetbl(Disbursement_Claim_ID, Assignment_ID, Expense_Type_ID, Expense_Total, Expense_Date, Expense_Proof) VALUES (@Disbursement_Claim_ID, @Assignment_ID, @Expense_Type_ID, @Expense_Total, @Expense_Date, 0)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [Expensetbl] SET [Disbursement_Claim_ID] = @Disbursement_Claim_ID, [Assignment_ID] = @Assignment_ID, [Expense_Type_ID] = @Expense_Type_ID, [Expense_Total] = @Expense_Total, [Expense_Date] = @Expense_Date, [Expense_Proof] = @Expense_Proof WHERE [Expense_ID] = @original_Expense_ID AND (([Disbursement_Claim_ID] = @original_Disbursement_Claim_ID) OR ([Disbursement_Claim_ID] IS NULL AND @original_Disbursement_Claim_ID IS NULL)) AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Expense_Type_ID] = @original_Expense_Type_ID) OR ([Expense_Type_ID] IS NULL AND @original_Expense_Type_ID IS NULL)) AND (([Expense_Total] = @original_Expense_Total) OR ([Expense_Total] IS NULL AND @original_Expense_Total IS NULL)) AND (([Expense_Date] = @original_Expense_Date) OR ([Expense_Date] IS NULL AND @original_Expense_Date IS NULL)) AND (([Expense_Proof] = @original_Expense_Proof) OR ([Expense_Proof] IS NULL AND @original_Expense_Proof IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_Expense_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Expense_Type_ID" Type="Int32" />
                    <asp:Parameter Name="original_Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Expense_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Expense_Proof" Type="Boolean" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:SessionParameter Name="Disbursement_Claim_ID" SessionField="MaxDisbursementClaimID" Type="Int32" />
                    <asp:SessionParameter Name="Assignment_ID" SessionField="assignment_id" Type="Int32" />
                    <asp:ControlParameter ControlID="ddlExpenseType" Name="Expense_Type_ID" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="txtExpenseAmount" Name="Expense_Total" PropertyName="Text" Type="Decimal" />
                    <asp:ControlParameter ControlID="txtExpenseDate" Name="Expense_Date" PropertyName="Text" Type="DateTime" />
                    <asp:Parameter Name="Expense_Proof" Type="Boolean" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="Expense_Type_ID" Type="Int32" />
                    <asp:Parameter Name="Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="Expense_Date" Type="DateTime" />
                    <asp:Parameter Name="Expense_Proof" Type="Boolean" />
                    <asp:Parameter Name="original_Expense_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Expense_Type_ID" Type="Int32" />
                    <asp:Parameter Name="original_Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Expense_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Expense_Proof" Type="Boolean" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSUpdateDisbursementValues" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" DeleteCommand="DELETE FROM [DisbursementClaimtbl] WHERE [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Disbursement_Travel_Total] = @original_Disbursement_Travel_Total) OR ([Disbursement_Travel_Total] IS NULL AND @original_Disbursement_Travel_Total IS NULL)) AND (([Disbursement_Expense_Total] = @original_Disbursement_Expense_Total) OR ([Disbursement_Expense_Total] IS NULL AND @original_Disbursement_Expense_Total IS NULL)) AND (([Disbursement_Total_Claim] = @original_Disbursement_Total_Claim) OR ([Disbursement_Total_Claim] IS NULL AND @original_Disbursement_Total_Claim IS NULL)) AND [Disbursement_Date] = @original_Disbursement_Date AND (([Manager_ID] = @original_Manager_ID) OR ([Manager_ID] IS NULL AND @original_Manager_ID IS NULL)) AND (([Disbursement_Approved] = @original_Disbursement_Approved) OR ([Disbursement_Approved] IS NULL AND @original_Disbursement_Approved IS NULL))" InsertCommand="INSERT INTO [DisbursementClaimtbl] ([Assignment_ID], [Disbursement_Travel_Total], [Disbursement_Expense_Total], [Disbursement_Total_Claim], [Disbursement_Date], [Manager_ID], [Disbursement_Approved]) VALUES (@Assignment_ID, @Disbursement_Travel_Total, @Disbursement_Expense_Total, @Disbursement_Total_Claim, @Disbursement_Date, @Manager_ID, @Disbursement_Approved)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [DisbursementClaimtbl]" UpdateCommand="UPDATE DisbursementClaimtbl SET Disbursement_Travel_Total = COALESCE ((SELECT SUM(Travel_Total) AS Expr1 FROM Traveltbl WHERE (Disbursement_Claim_ID = DisbursementClaimtbl.Disbursement_Claim_ID)), 0), Disbursement_Expense_Total = COALESCE ((SELECT SUM(Expense_Total) AS Expr1 FROM Expensetbl WHERE (Disbursement_Claim_ID = DisbursementClaimtbl.Disbursement_Claim_ID)), 0), Disbursement_Total_Claim = COALESCE ((SELECT SUM(Travel_Total) AS Expr1 FROM Traveltbl AS Traveltbl_1 WHERE (Disbursement_Claim_ID = DisbursementClaimtbl.Disbursement_Claim_ID)), 0) + COALESCE ((SELECT SUM(Expense_Total) AS Expr1 FROM Expensetbl AS Expensetbl_1 WHERE (Disbursement_Claim_ID = DisbursementClaimtbl.Disbursement_Claim_ID)), 0)">
                <DeleteParameters>
                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Disbursement_Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Disbursement_Total_Claim" Type="Decimal" />
                    <asp:Parameter Name="original_Disbursement_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Manager_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Approved" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="Disbursement_Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="Disbursement_Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="Disbursement_Total_Claim" Type="Decimal" />
                    <asp:Parameter Name="Disbursement_Date" Type="DateTime" />
                    <asp:Parameter Name="Manager_ID" Type="Int32" />
                    <asp:Parameter Name="Disbursement_Approved" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="Disbursement_Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="Disbursement_Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="Disbursement_Total_Claim" Type="Decimal" />
                    <asp:Parameter Name="Disbursement_Date" Type="DateTime" />
                    <asp:Parameter Name="Manager_ID" Type="Int32" />
                    <asp:Parameter Name="Disbursement_Approved" Type="String" />
                    <asp:Parameter Name="original_Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Disbursement_Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="original_Disbursement_Total_Claim" Type="Decimal" />
                    <asp:Parameter Name="original_Disbursement_Date" Type="DateTime" />
                    <asp:Parameter Name="original_Manager_ID" Type="Int32" />
                    <asp:Parameter Name="original_Disbursement_Approved" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSFillExpenseType" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" SelectCommand="SELECT * FROM [ExpenseTypetbl]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSUpdateAssignmentValues" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" DeleteCommand="DELETE FROM [ProjectAssignmenttbl] WHERE [Assignment_ID] = @original_Assignment_ID AND [Project_ID] = @original_Project_ID AND [Employee_ID] = @original_Employee_ID AND [Date_Assigned] = @original_Date_Assigned AND [Assignment_Claim_Max] = @original_Assignment_Claim_Max AND (([Assignment_Claim_Balance] = @original_Assignment_Claim_Balance) OR ([Assignment_Claim_Balance] IS NULL AND @original_Assignment_Claim_Balance IS NULL))" InsertCommand="INSERT INTO [ProjectAssignmenttbl] ([Project_ID], [Employee_ID], [Date_Assigned], [Assignment_Claim_Max], [Assignment_Claim_Balance]) VALUES (@Project_ID, @Employee_ID, @Date_Assigned, @Assignment_Claim_Max, @Assignment_Claim_Balance)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [ProjectAssignmenttbl]" UpdateCommand="UPDATE ProjectAssignmenttbl SET Assignment_Claim_Balance = CASE WHEN Assignment_Claim_Max IS NULL THEN 0 ELSE Assignment_Claim_Max - COALESCE ((SELECT SUM(Disbursement_Total_Claim) FROM DisbursementClaimtbl WHERE DisbursementClaimtbl.Assignment_ID = ProjectAssignmenttbl.Assignment_ID) , 0) END">
                <DeleteParameters>
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Project_ID" Type="Int32" />
                    <asp:Parameter Name="original_Employee_ID" Type="Int32" />
                    <asp:Parameter Name="original_Date_Assigned" Type="DateTime" />
                    <asp:Parameter Name="original_Assignment_Claim_Max" Type="Decimal" />
                    <asp:Parameter Name="original_Assignment_Claim_Balance" Type="Decimal" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Project_ID" Type="Int32" />
                    <asp:Parameter Name="Employee_ID" Type="Int32" />
                    <asp:Parameter Name="Date_Assigned" Type="DateTime" />
                    <asp:Parameter Name="Assignment_Claim_Max" Type="Decimal" />
                    <asp:Parameter Name="Assignment_Claim_Balance" Type="Decimal" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Project_ID" Type="Int32" />
                    <asp:Parameter Name="Employee_ID" Type="Int32" />
                    <asp:Parameter Name="Date_Assigned" Type="DateTime" />
                    <asp:Parameter Name="Assignment_Claim_Max" Type="Decimal" />
                    <asp:Parameter Name="Assignment_Claim_Balance" Type="Decimal" />
                    <asp:Parameter Name="original_Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="original_Project_ID" Type="Int32" />
                    <asp:Parameter Name="original_Employee_ID" Type="Int32" />
                    <asp:Parameter Name="original_Date_Assigned" Type="DateTime" />
                    <asp:Parameter Name="original_Assignment_Claim_Max" Type="Decimal" />
                    <asp:Parameter Name="original_Assignment_Claim_Balance" Type="Decimal" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
            </br></br>
            <asp:Button ID="btnNewRecord" runat="server" CssClass="btn btn-primary" Text="Do you want to make a new record?" OnClick="btnNewRecord_Click" Visible="False" />
            </br></br>
            <asp:Button ID="btnNewDisbursement" runat="server" CssClass="btn btn-primary" Text="Do you want to record a new Disbursement?" OnClick="btnNewDisbursement_Click" Visible="False" />
        </div>
    </div>
</asp:Content>
