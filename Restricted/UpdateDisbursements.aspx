<%@ Page Title="Update Disbursements" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateDisbursements.aspx.cs" Inherits="ProjectSite.Restricted.UpdateDisbursements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Custom CSS or Scripts here if needed -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Project Selection Section -->
        <div class="mb-4">
            <h3>Update Disbursement Details</h3>
            <div class="row mb-3">
                <label for="projectSelection" class="col-sm-2 col-form-label">Select Project</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlProjectSelection" runat="server" CssClass="form-select" DataSourceID="sqlDSProject" DataTextField="Project_Name" DataValueField="Project_ID" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectSelection_SelectedIndexChanged">
                        <asp:ListItem Text="-- Select a Project --" Value="" />
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sqlDSProject" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" 
                        SelectCommand="SELECT DISTINCT Projecttbl.Project_ID, Projecttbl.Project_Name FROM Projecttbl INNER JOIN ProjectAssignmenttbl ON Projecttbl.Project_ID = ProjectAssignmenttbl.Project_ID WHERE (ProjectAssignmenttbl.Employee_ID = @employeeID)">
                        <SelectParameters>
                            <asp:SessionParameter Name="employeeID" SessionField="user_id" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

            <!-- Disbursement Date Selection Section -->
        </div>


        <!-- Project Selection Section -->
        <asp:Label ID="Label1" runat="server" Text="Select the Disbursement you want to update"></asp:Label>
        <asp:GridView runat="server" ID="gvDisbursements" AutoGenerateColumns="False" DataKeyNames="Disbursement_Claim_ID" DataSourceID="sqlDSDisplayDisbursements" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvDisbursements_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Disbursement_Claim_ID" InsertVisible="False" ReadOnly="True" SortExpression="Disbursement_Claim_ID" />
                <asp:BoundField DataField="Assignment_ID" HeaderText="Assignment_ID" SortExpression="Assignment_ID" />
                <asp:BoundField DataField="Disbursement_Travel_Total" HeaderText="Disbursement_Travel_Total" SortExpression="Disbursement_Travel_Total" />
                <asp:BoundField DataField="Disbursement_Expense_Total" HeaderText="Disbursement_Expense_Total" SortExpression="Disbursement_Expense_Total" />
                <asp:BoundField DataField="Disbursement_Total_Claim" HeaderText="Disbursement_Total_Claim" SortExpression="Disbursement_Total_Claim" />
                <asp:BoundField DataField="Disbursement_Date" HeaderText="Disbursement_Date" SortExpression="Disbursement_Date" />
                <asp:BoundField DataField="Manager_ID" HeaderText="Manager_ID" SortExpression="Manager_ID" />
                <asp:BoundField DataField="Disbursement_Approved" HeaderText="Disbursement_Approved" SortExpression="Disbursement_Approved" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>


        <asp:SqlDataSource ID="sqlDSDisplayDisbursements" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" SelectCommand="SELECT DisbursementClaimtbl.Disbursement_Claim_ID, DisbursementClaimtbl.Assignment_ID, DisbursementClaimtbl.Disbursement_Travel_Total, DisbursementClaimtbl.Disbursement_Expense_Total, DisbursementClaimtbl.Disbursement_Total_Claim, DisbursementClaimtbl.Disbursement_Date, DisbursementClaimtbl.Manager_ID, DisbursementClaimtbl.Disbursement_Approved FROM DisbursementClaimtbl INNER JOIN ProjectAssignmenttbl ON DisbursementClaimtbl.Assignment_ID = ProjectAssignmenttbl.Assignment_ID WHERE (ProjectAssignmenttbl.Assignment_ID = @assignID) AND (ProjectAssignmenttbl.Employee_ID = @empID) AND (MONTH(DisbursementClaimtbl.Disbursement_Date) = MONTH(GETDATE())) AND (YEAR(DisbursementClaimtbl.Disbursement_Date) = YEAR(GETDATE()))">
            <SelectParameters>
                <asp:SessionParameter Name="assignID" SessionField="assignment_id" />
                <asp:SessionParameter Name="empID" SessionField="employee_id" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlDSSelectTravel" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" SelectCommand="SELECT Travel_ID, Disbursement_Claim_ID, Assignment_ID, Travel_Description, Travel_Date, Travel_Mileage, Travel_Vehicle_Description, Travel_Total, Travel_Proof FROM Traveltbl WHERE (Disbursement_Claim_ID = @claimID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="gvDisbursements" Name="claimID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlDSSelectExpense" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" SelectCommand="SELECT Expense_ID, Disbursement_Claim_ID, Assignment_ID, Expense_Type_ID, Expense_Total, Expense_Date, Expense_Proof FROM Expensetbl WHERE (Disbursement_Claim_ID = @claimID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="gvDisbursements" Name="claimID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>

        </br>
        </br>
        <asp:Label ID="Label2" runat="server" Text="Select the Travel Record you want to update"></asp:Label>
        <asp:GridView ID="gvSelectTravel" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Travel_ID" DataSourceID="sqlDSSelectTravel" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvSelectTravel_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Travel_ID" HeaderText="Travel_ID" InsertVisible="False" ReadOnly="True" SortExpression="Travel_ID"></asp:BoundField>
                <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Disbursement_Claim_ID" SortExpression="Disbursement_Claim_ID"></asp:BoundField>
                <asp:BoundField DataField="Assignment_ID" HeaderText="Assignment_ID" SortExpression="Assignment_ID"></asp:BoundField>
                <asp:BoundField DataField="Travel_Description" HeaderText="Travel_Description" SortExpression="Travel_Description"></asp:BoundField>
                <asp:BoundField DataField="Travel_Date" HeaderText="Travel_Date" SortExpression="Travel_Date"></asp:BoundField>
                <asp:BoundField DataField="Travel_Mileage" HeaderText="Travel_Mileage" SortExpression="Travel_Mileage"></asp:BoundField>
                <asp:BoundField DataField="Travel_Vehicle_Description" HeaderText="Travel_Vehicle_Description" SortExpression="Travel_Vehicle_Description"></asp:BoundField>
                <asp:BoundField DataField="Travel_Total" HeaderText="Travel_Total" SortExpression="Travel_Total"></asp:BoundField>
                <asp:CheckBoxField DataField="Travel_Proof" HeaderText="Travel_Proof" SortExpression="Travel_Proof" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>

          </br>
        </br>
        <asp:Label ID="Label3" runat="server" Text="Select the Travel Record you want to update"></asp:Label>
        <asp:GridView ID="gvSelectExpense" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Expense_ID" DataSourceID="sqlDSSelectExpense" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvSelectExpense_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Expense_ID" HeaderText="Expense_ID" InsertVisible="False" ReadOnly="True" SortExpression="Expense_ID"></asp:BoundField>
                <asp:BoundField DataField="Disbursement_Claim_ID" HeaderText="Disbursement_Claim_ID" SortExpression="Disbursement_Claim_ID"></asp:BoundField>
                <asp:BoundField DataField="Assignment_ID" HeaderText="Assignment_ID" SortExpression="Assignment_ID"></asp:BoundField>
                <asp:BoundField DataField="Expense_Type_ID" HeaderText="Expense_Type_ID" SortExpression="Expense_Type_ID"></asp:BoundField>
                <asp:BoundField DataField="Expense_Total" HeaderText="Expense_Total" SortExpression="Expense_Total"></asp:BoundField>
                <asp:BoundField DataField="Expense_Date" HeaderText="Expense_Date" SortExpression="Expense_Date"></asp:BoundField>
                <asp:CheckBoxField DataField="Expense_Proof" HeaderText="Expense_Proof" SortExpression="Expense_Proof" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
        </br>
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
        </br>
        <!-- Submit Button -->
        <div class="text-center">
            <asp:Button ID="btnUpdateTravel" runat="server" Text="Update Travel Details" CssClass="btn btn-primary" OnClick="btnUpdateTravel_Click" />
        </div>
    </div>
    </br></br>
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
                            <asp:ListItem Text="Select Expense Type" Value="" />
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sqlDSFillExpenseType" runat="server"
                            ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>"
                            SelectCommand="SELECT * FROM [ExpenseTypetbl]">
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </br>
        <!-- Submit Button -->
        <div class="text-center">
            <asp:SqlDataSource ID="sqlDSUpdateTravel" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" DeleteCommand="DELETE FROM [Traveltbl] WHERE [Travel_ID] = @original_Travel_ID AND [Disbursement_Claim_ID] = @original_Disbursement_Claim_ID AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Travel_Description] = @original_Travel_Description) OR ([Travel_Description] IS NULL AND @original_Travel_Description IS NULL)) AND (([Travel_Date] = @original_Travel_Date) OR ([Travel_Date] IS NULL AND @original_Travel_Date IS NULL)) AND (([Travel_Mileage] = @original_Travel_Mileage) OR ([Travel_Mileage] IS NULL AND @original_Travel_Mileage IS NULL)) AND (([Travel_Vehicle_Description] = @original_Travel_Vehicle_Description) OR ([Travel_Vehicle_Description] IS NULL AND @original_Travel_Vehicle_Description IS NULL)) AND (([Travel_Total] = @original_Travel_Total) OR ([Travel_Total] IS NULL AND @original_Travel_Total IS NULL)) AND (([Travel_Proof] = @original_Travel_Proof) OR ([Travel_Proof] IS NULL AND @original_Travel_Proof IS NULL))" InsertCommand="INSERT INTO [Traveltbl] ([Disbursement_Claim_ID], [Assignment_ID], [Travel_Description], [Travel_Date], [Travel_Mileage], [Travel_Vehicle_Description], [Travel_Total], [Travel_Proof]) VALUES (@Disbursement_Claim_ID, @Assignment_ID, @Travel_Description, @Travel_Date, @Travel_Mileage, @Travel_Vehicle_Description, @Travel_Total, @Travel_Proof)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Traveltbl]" UpdateCommand="UPDATE Traveltbl SET Travel_Description = @Travel_Description, Travel_Date = @Travel_Date, Travel_Mileage = @Travel_Mileage, Travel_Vehicle_Description = @Travel_Vehicle_Description, Travel_Total = @Travel_Total WHERE (Travel_ID = @travelID)">
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
                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="Travel_Description" Type="String" />
                    <asp:Parameter Name="Travel_Date" Type="DateTime" />
                    <asp:Parameter Name="Travel_Mileage" Type="Int32" />
                    <asp:Parameter Name="Travel_Vehicle_Description" Type="String" />
                    <asp:Parameter Name="Travel_Total" Type="Decimal" />
                    <asp:Parameter Name="Travel_Proof" Type="Boolean" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:ControlParameter ControlID="txtTravelDescription" Name="Travel_Description" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtTravelDate" Name="Travel_Date" PropertyName="Text" Type="DateTime" />
                    <asp:ControlParameter ControlID="txtMileage" Name="Travel_Mileage" PropertyName="Text" Type="Int32" />
                    <asp:ControlParameter ControlID="txtVehicleDescription" Name="Travel_Vehicle_Description" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txtTravelTotal" Name="Travel_Total" PropertyName="Text" Type="Decimal" />
                    <asp:SessionParameter Name="travelID" SessionField="selected_travel_id" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlDSUpdateExpense" runat="server" ConnectionString="<%$ ConnectionStrings:G8Wst2024ConnectionString2 %>" DeleteCommand="DELETE FROM [Expensetbl] WHERE [Expense_ID] = @original_Expense_ID AND (([Disbursement_Claim_ID] = @original_Disbursement_Claim_ID) OR ([Disbursement_Claim_ID] IS NULL AND @original_Disbursement_Claim_ID IS NULL)) AND (([Assignment_ID] = @original_Assignment_ID) OR ([Assignment_ID] IS NULL AND @original_Assignment_ID IS NULL)) AND (([Expense_Type_ID] = @original_Expense_Type_ID) OR ([Expense_Type_ID] IS NULL AND @original_Expense_Type_ID IS NULL)) AND (([Expense_Total] = @original_Expense_Total) OR ([Expense_Total] IS NULL AND @original_Expense_Total IS NULL)) AND (([Expense_Date] = @original_Expense_Date) OR ([Expense_Date] IS NULL AND @original_Expense_Date IS NULL)) AND (([Expense_Proof] = @original_Expense_Proof) OR ([Expense_Proof] IS NULL AND @original_Expense_Proof IS NULL))" InsertCommand="INSERT INTO [Expensetbl] ([Disbursement_Claim_ID], [Assignment_ID], [Expense_Type_ID], [Expense_Total], [Expense_Date], [Expense_Proof]) VALUES (@Disbursement_Claim_ID, @Assignment_ID, @Expense_Type_ID, @Expense_Total, @Expense_Date, @Expense_Proof)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Expensetbl]" UpdateCommand="UPDATE Expensetbl SET Expense_Type_ID = @Expense_Type_ID, Expense_Total = @Expense_Total, Expense_Date = @Expense_Date WHERE (Expense_ID = @expenseID)">
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
                    <asp:Parameter Name="Disbursement_Claim_ID" Type="Int32" />
                    <asp:Parameter Name="Assignment_ID" Type="Int32" />
                    <asp:Parameter Name="Expense_Type_ID" Type="Int32" />
                    <asp:Parameter Name="Expense_Total" Type="Decimal" />
                    <asp:Parameter Name="Expense_Date" Type="DateTime" />
                    <asp:Parameter Name="Expense_Proof" Type="Boolean" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:ControlParameter ControlID="ddlExpenseType" Name="Expense_Type_ID" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="txtExpenseAmount" Name="Expense_Total" PropertyName="Text" Type="Decimal" />
                    <asp:ControlParameter ControlID="txtExpenseDate" Name="Expense_Date" PropertyName="Text" Type="DateTime" />
                    <asp:SessionParameter Name="expenseID" SessionField="selected_expense_id" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <asp:Button ID="btnUpdateExpense" runat="server" Text="Update Expense Details" CssClass="btn btn-primary" OnClick="btnUpdateExpense_Click" />
        </div>
    </div>
</asp:Content>
