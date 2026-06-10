<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TotalTransactionPopup.aspx.cs" Inherits="IPD_TotalTransactionPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="formbox">
    <table width="100%" border="1" cellpadding="0" cellspacing="0"  style='font-family:Arial;'>
    <tr>
    <td colspan="4">
     <div class="pageheader">
                <asp:Label ID="Label1" runat="server"><u>Patient's Transaction Details</u></asp:Label>
            </div>
            </td></tr>
    <tr>
    <td><strong>Bill No :</strong></td>  
       <td>
        <asp:TextBox ID="txtBillNo" Enabled="false" runat="server"></asp:TextBox> </td>

           <td ><strong> Reg No :</strong></td>  
       <td >
        <asp:TextBox ID="txtRegNo" Enabled="false" runat="server"></asp:TextBox> </td>
    </tr>


        <tr>
     <td ><strong>Name :</strong></td>  
    <td> 
        <asp:TextBox ID="txtName" Enabled="false" runat="server"></asp:TextBox> </td>

           <td><strong>Age :</strong></td>  
    <td> 
        <asp:TextBox ID="txtAge" Enabled="false" runat="server"></asp:TextBox> </td>
    </tr>


        <tr>
     <td><strong>Address :</strong></td>  
    <td> 
        <asp:TextBox ID="txtAddress" Enabled="false" runat="server"></asp:TextBox> </td>

           <td><strong>Phone No :</strong></td>  
    <td> 
        <asp:TextBox ID="txtPhNo" Enabled="false" runat="server"></asp:TextBox> </td>
    </tr>
    </table>
     </div>

      <div class="formbox">
    <table width="100%" style='font-family:Arial;'>
              <tr>
    <td align="left" style='width:200px;'>Bed Charge :</td>  
    <td> 
        <asp:TextBox ID="txtBedCharge" Enabled="false" Width="80px" runat="server"></asp:TextBox> </td>
          <td align="left" style='width:200px;'>Dcotor Visit Charge :</td>  
    <td> 
        <asp:TextBox ID="txtDoctorVisitCharge" Width="80px" Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

          <tr>
                 <td align="left" style='width:200px;'>Medicine Charge :</td>  
    <td> 
        <asp:TextBox ID="txtMedicineCharge" Width="80px" Enabled="false" runat="server"></asp:TextBox> </td>
          <td align="left" style='width:200px;'>Cosumable Charge :</td>  
    <td> 
        <asp:TextBox ID="txtConsumable" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

          <tr>
                    <td align="left" style='width:200px;'>Service Charge :</td>  
    <td> 
        <asp:TextBox ID="txtServiceCharge" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
       
    <td align="left" style='width:200px;'>Pathologe Charge :</td>  
    <td> 
        <asp:TextBox ID="txtpathology" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

          <tr>
                   <td align="left" style='width:200px;'>X-Ray Charge :</td>  
    <td> 
        <asp:TextBox ID="txtXRayCharge" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
         <td align="left" style='width:200px;'>USG Charge :</td>  
    <td> 
        <asp:TextBox ID="txtusg" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

          <tr>
                 <td align="left" style='width:200px;'>OT Charge :</td>  
    <td> 
        <asp:TextBox ID="txtOTCharge" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
          <td align="left" style='width:200px;'>OT Attendence Charge :</td>  
    <td> 
        <asp:TextBox ID="txtotAttendence" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

          <tr>
                  <td align="left" style='width:200px;'>OT Consumable Charge :</td>  
    <td> 
        <asp:TextBox ID="txtotConsumableCharge" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
           <td align="left" style='width:200px;'>Ambulance Charge :</td>  
    <td> 
        <asp:TextBox ID="txtAmbulance"  Width="80px" Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

          <tr>
                   <td align="left" style='width:200px;'>SisterAya Charge :</td>  
    <td> 
        <asp:TextBox ID="txtSisterAyaCharge" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
                  <td align="left" style='width:200px;'>Instrument Charge :</td>  
    <td> 
        <asp:TextBox ID="txtInstrument" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

          <tr>
             <td align="left" style='width:200px;'>Anesthesia Medicine :</td>  
    <td> 
        <asp:TextBox ID="txtAnesthesiaMedicine" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
          <td align="left" style='width:200px;'>Anesthesia Consumable :</td>  
    <td> 
        <asp:TextBox ID="txtAnesthesiaconsumable" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

           <tr>
              <td colspan="4">___________________________________________________________________________________________________________________</td>
       </tr>

          <tr>
              <td >&nbsp;</td>
                 <td >&nbsp;</td>
    <td align="left" style='width:200px;'>Due Amount :</td>  
    <td> 
        <asp:TextBox ID="txtDue" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>


         <tr>
              <td >&nbsp;</td>
                 <td >&nbsp;</td>
    <td align="left" style='width:200px;'>Discount Amount :</td>  
    <td> 
        <asp:TextBox ID="txtDiscount" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>

           <tr>
              <td >&nbsp;</td>
                 <td >&nbsp;</td>
    <td align="left" style='width:200px;'>Total Amount :</td>  
    <td> 
        <asp:TextBox ID="txtTotal" Width="80px"  Enabled="false" runat="server"></asp:TextBox> </td>
        </tr>
     
    </table> 
    </div>
    </form>
</body>
</html>
