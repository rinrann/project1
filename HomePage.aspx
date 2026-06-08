<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="menuboxmain">
                <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
                <div class="menuboxmain2">
                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="Images/ddd.png" width="20" height="20" alt="" />Admin/Master</div>
                        <ul>
                            <li><a href="Security/UserRole.aspx">User Role</a></li>
                            <li><a href="Security/UserCreation.aspx">User Creation</a></li>
                            <li><a href="Security/ChangePassword.aspx">Change Password</a></li>
                            <li><a href="Security/UserAction.aspx">User Action</a></li> 
                            <li><a href="Utility/DataConsistency.aspx">Data Consistancy</a></li>
                                   <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                        </ul>
                    </div>

                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="images/aaaa.png" width="20" height="20" />Registration</div>
                        <ul>
                            <li><a href="OPD/PatientEnrollment.aspx">Patient Registration</a></li>
                            <li><a href="OPD/PatientAppointment.aspx">Patient Appointment</a></li>
                            <li><a href="OPD/PatientsDetails.aspx">Patient Details</a></li>
                            <li><a href="OPD/PatientEnrollment.aspx">Patient Enrollment</a></li>
                            <li><a href="Pathology/PatientRequisition.aspx">Patient Requisition</a></li>
                            <li><a href="Pathology/RequisitionBill.aspx">Investigation Bill Entry</a></li>
                        </ul>
                    </div>


                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="images/nnn.png" width="20" height="20" />Investigation</div>
                        <ul>
                            <li><a href="#">Imaging Service</a></li>
                            <li><a href="#">Consultancy Service</a></li>
                            <li><a href="#">Foetal Medicine Services</a></li>
                            <%--<li><a href="Pathology/SpecimanMaster.aspx">Sample Master</a></li>
                            <li><a href="DayCare/DialysisPayment.aspx">Dialysis Payment</a></li>--%>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>

                        </ul>
                    </div>

                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="images/sssssss.png" width="20" height="20" />Procedure & IVF</div>
                        <ul>
                            <li><a href="#">Infertility Treatment</a></li>
                            <li><a href="#">Miscellaneous Service</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>

                            <%--<li><a href="Pathology/PatientRegistration.aspx">Patient Registration</a></li>
                            <li><a href="Pathology/ReagentOrder.aspx">Reagent Order</a></li>
                            <li><a href="Pathology/ReagentEntry.aspx">Reagent Entry</a></li>
                            <li><a href="Pathology/ReagentRefund.aspx">Reagent Refund</a></li>--%>
                            <%--           <li><a href="../Pathology/TestResult.aspx">Pathology Test Result</a></li>
                                 <li><a href="../Pathology/TestResultXRay.aspx">X-Ray Test Result</a></li>
                              <li><a href="../Pathology/TestResultRadiology.aspx">USG Test Result</a></li>--%>
                        </ul>
                    </div>

                </div>

                <div class="menuboxmain2">

                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="images/sssssss.png" width="20" height="20" />Pathology/Pharma</div>
                        <ul>
                            <li><a href="Pathology/SuppilierMaster.aspx">Supplier Entry</a></li>
                            <li><a href="Pathology/ManufactureMaster.aspx">Manufacturer Entry</a></li>
                            <li><a href="Medicine/MedicineMaster.aspx">Medicine Master</a></li>
                            <li><a href="Medicine/MedicineSalesNew.aspx">Medicine Issue Entry</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>
                        </ul>
                    </div>

                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="images/pppp.png" width="20" height="20" />Stores</div>
                        <ul>
                            <li><a href="Medicine/PurchaseMedicine.aspx">Medicine Purchase</a></li>
                            <li><a href="Medicine/MedicineReturn.aspx">Purchase Return</a></li>
                            <li><a href="Medicine/MedicineSales.aspx">Medicine Sales</a></li>
                            <li><a href="Medicine/MedicineSaleReturn.aspx">Sales Return</a></li>
                            <li><a href="Medicine/PurchaseInvoiceReport.aspx">Invoice Report</a></li>
                            <li><a href="Medicine/MedicineStockReport.aspx">Stock Maintain</a></li>


                        </ul>
                    </div>


                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="images/acac.png" width="20" height="20" />A/c Reports</div>
                        <ul> 
                            <li><a href="Bill/BillGeneration.aspx">Discharge Bill Details</a></li>
                            <li><a href="IPD/MoneyReceipt.aspx">Money Receipt</a></li> 
                            <li><a href="Settings/DoctorPaymentReport.aspx">Doctor Payment Report</a></li>
                            <li><a href="Medicine/Rep_StockMovement.aspx">Stock Movement</a></li>
                            <li><a href="Medicine/Rep_StockLedger.aspx">Stock Ledger Details</a></li>
                             <li><a href="Medicine/Rep_StockValuation.aspx">Stock Valuation</a></li>
                        </ul>
                    </div>

                    <div class="boxdiv">
                        <div class="boxdivhead">
                            <img src="images/cccccccccccc.png" width="20" height="20" />MIS Reports</div>
                        <ul>

                            <li><a href="Pathology/Rep_InvestGrpwisecollection.aspx">Investigation Group wise Collection</a></li>
                            <li><a href="Rep_SampleCollection.aspx">Sample wise collection</a></li>
                            <li><a href="Medicine/ItemStockAlert.aspx">Stock End Report</a></li>
                            <li><a href="Medicine/ExpiryAlertReport.aspx">Expiry Alert Report</a></li>
                            <li><a href="Medicine/MedicineStockDetails.aspx">Medicine Stock Details</a></li>
                            <li style="background-image: none;"><a href="#">&nbsp;</a></li>

                        </ul>
                    </div>



                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

