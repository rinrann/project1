<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="PurchaseMedicine.aspx.cs" Inherits="Master_PurchaseMedicine" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
       function ShowDialog() {
           var rtvalue = window.open("PurchaseMedicinePopup.aspx", "sss", "Width:1000px; Height:550px; dialogLeft:250px;");
           //document.getElementById("ctl00_ContentPlaceHolder1_txtPurchaseMedicineId").value = rtvalue;

       }

       function isNumberKey(evt) {
           var charCode = (evt.which) ? evt.which : event.keyCode;
           if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
               return false;

           return true;
       }

       function Calling() {
           //var date = new Date();
           //$("input[id$='Calendar1']").datepicker({ dateFormat: 'dd/mm/yy' });


           //var date = new Date();
           //$("input[id$='Calendar2']").datepicker({ dateFormat: 'dd/mm/yy' });


           //var date = new Date();
           //$("input[id$='Calendar3']").datepicker({ dateFormat: 'dd/mm/yy' });


           //var date = new Date();
           //$("input[id$='Calendar4']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar5']").datepicker({ dateFormat: 'dd/mm/yy' });


           //var date = new Date();
           //$("input[id$='Calendar6']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar7']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar8']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar9']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar10']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar11']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar12']").datepicker({ dateFormat: 'dd/mm/yy' });

           //var date = new Date();
           //$("input[id$='Calendar13']").datepicker({ dateFormat: 'dd/mm/yy' });


           $("input[id$='Button1']").click(function () {

               //if ($("select[id$='DropDownList1']").val() == '0') {
               //    alert('Please Select Supplier Name!');
               //    $("select[id$='DropDownList1']").addClass('textboxerr');
               //    $("select[id$='DropDownList1']").focus();
               //    return false;
               //}
               //else {
               //    $("select[id$='DropDownList1']").removeClass('textboxerr');
               //}

               if ($("input[id$='txtPurchaseMedicineId']").val() == '') {
                   alert('Please Enter Medicine ID!');
                   $("input[id$='txtPurchaseMedicineId']").focus();
                   $("input[id$='txtPurchaseMedicineId']").addClass('textboxerr');
                   return false;
               }
               else {
                   $("input[id$='txtPurchaseMedicineId']").removeClass('textboxerr');
               }

               if ($("input[id$='txtPurchaseMedicineName']").val() == '') {
                   alert('Please Enter Medicine Name!');
                   $("input[id$='txtPurchaseMedicineName']").focus();
                   $("input[id$='txtPurchaseMedicineName']").addClass('textboxerr');
                   return false;
               }
               else {
                   $("input[id$='txtPurchaseMedicineName']").removeClass('textboxerr');
               }

               if ($("input[id$='Calendar1']").val() == '') {
                   alert('Please Enter purchase Date!');
                   $("input[id$='Calendar1']").focus();
                   $("input[id$='Calendar1']").addClass('textboxerr');
                   return false;
               }
               else {
                   $("input[id$='Calendar1']").removeClass('textboxerr');
               }

               //if ($("input[id$='txtBillNo']").val() == '') {
               //    alert('Please Enter Bill No!');
               //    $("input[id$='txtBillNo']").focus();
               //    $("input[id$='txtBillNo']").addClass('textboxerr');
               //    return false;
               //}
               //else {
               //    $("input[id$='txtBillNo']").removeClass('textboxerr');
               //}
           });


           $("input[id$='txtQty1']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });



           $("input[id$='txtQty2']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });



           $("input[id$='txtQty3']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });



           $("input[id$='txtQty4']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });




           $("input[id$='txtQty5']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });




           $("input[id$='txtQty6']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });




           $("input[id$='txtQty7']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });


           $("input[id$='txtQty8']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });


           $("input[id$='txtQty9']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });


           $("input[id$='txtQty10']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });


           $("input[id$='txtQty11']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });



           $("input[id$='txtQty12']").keydown(function (event) {
               if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    (event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
                   return;
               }
               else {
                   if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                       event.preventDefault();
                   }
               }
           });
       }
       $(document).ready(function () {
           Calling();

           var prm = Sys.WebForms.PageRequestManager.getInstance();
           if (prm != null) {
               prm.add_endRequest(function (sender, e) {
                   if (sender._postBackSettings.panelsToUpdate != null) {

                       Calling();

                   }
               });
           };

       });



       function GetClientID(asp_net_id) {
           return $("[id$=" + asp_net_id + "]").attr("id");
       };

       function calc_less(id) {

           var lsid = '';
           var lesper = '';
           var i;
           var untid;
           var untprc = '';
           var qtyid;
           var qty = '';
           var lesprId;
           var lessAmtID;
           var amt = '';
           if (id == "0") {
               lsid = GetClientID("txtlessper");
               lesper = document.getElementById(lsid).value;
               if (lesper == "") { lesper = "0"; }
               for (i = 1; i <= 12; i++) {
                   untid = GetClientID("txtUnitPrice" + i);
                   untprc = document.getElementById(untid).value;
                   untprc = document.getElementById(untid).value;
                   qtyid = GetClientID("txtQty" + i);
                   qty = document.getElementById(qtyid).value;
                   if (untprc != "" && qty != "") {
                       lesprId = GetClientID("txtLessper" + i);
                       lessAmtID = GetClientID("txtLess" + i);
                       document.getElementById(lesprId).value = lesper;
                       amt = parseFloat(untprc) * parseFloat(qty) * (parseFloat(lesper) / 100);
                       document.getElementById(lessAmtID).value = amt.toFixed(2);

                   }
               }
           }
           else {
               debugger;
               lsid = GetClientID("txtLessper" + id);
               lesper = document.getElementById(lsid).value;
               if (lesper == "") { lesper = "0"; }
               untid = GetClientID("txtUnitPrice" + id);
               untprc = document.getElementById(untid).value;
               qtyid = GetClientID("txtQty" + id);
               qty = document.getElementById(qtyid).value;
                
               if (untprc != "" && qty != "") {
                   lesprId = GetClientID("txtLessper" + id);
                   lessAmtID = GetClientID("txtLess" + id);
                   document.getElementById(lesprId).value = lesper;
                   if (lesper != "0") {
                       amt = parseFloat(untprc) * parseFloat(qty) * (parseFloat(lesper) / 100);
                   }
                   else {
                       amt = parseFloat(0);
                   }
                   document.getElementById(lessAmtID).value = amt.toFixed(2);

               }
           }
           calc_tax(id);
           calc_total(id);
       }


       function calc_tax(id) {
           
           var taxid = '';
           var taxper = '';
           var i;
           var mrpid;
           var mrpprc = '';
           var qtyid;
           var qty = '';
           var frqtyid = '';
           var frqty;
           var totqty;
           var taxprId;
           var taxAmtID;
           var cgstperId;
           var cgstAmt;
           var sgstperId;
           var sgstAmt;
           var igstperId;
           var igstAmt;
           var amt = '';
           if (id == "0") {
               taxid = GetClientID("txtTaxper");
               taxper = document.getElementById(taxid).value;

               cgsttaxid = GetClientID("txtCgstRt");
               cgstper = document.getElementById(cgsttaxid).value;

               if (taxper == "") { taxper = "0"; }
               for (i = 1; i <= 12; i++) {
                   mrpid = GetClientID("txtSellPrice" + i);
                   lessid = GetClientID("txtLess" + i); 
                   mrpprc = parseFloat(document.getElementById(mrpid).value) - parseFloat(document.getElementById(lessid).value);
                   alert(mrpprc);
                   qtyid = GetClientID("txtQty" + i);
                   qty = document.getElementById(qtyid).value;
                   if (qty == "") { qty = 0; }
                   frqtyid = GetClientID("txtfreeqty" + i);
                   frqty = document.getElementById(frqtyid).value;
                   if (frqty == "") { frqty = 0; }
                   totqty = qty + frqty;
                   if (mrpprc != "" && totqty > 0) {
                       taxprId = GetClientID("txtStaxper" + i);
                       taxAmtID = GetClientID("txtStax" + i);
                       document.getElementById(taxprId).value = taxper;
                       amt = parseFloat(mrpprc) * parseFloat(totqty) * (parseFloat(taxper) / 100);
                       document.getElementById(taxAmtID).value = amt.toFixed(2);

                   }
               }
           }
           else {

               taxid = GetClientID("txtStaxper" + id);
               taxper = document.getElementById(taxid).value;
               if (taxper == "") { taxper = "0"; }

               cgsttaxid = GetClientID("txtCgstRt" + id);
               cgsttaxper = document.getElementById(cgsttaxid).value;
               sgsttaxid = GetClientID("txtSgstRt" + id);
               sgsttaxper = document.getElementById(sgsttaxid).value;
               igsttaxid = GetClientID("txtIgstRt" + id);
               igsttaxper = document.getElementById(igsttaxid).value;

               mrpid = GetClientID("txtUnitPrice" + id);
               lessid = GetClientID("txtLess" + id);
              
               mrpprc = parseFloat(document.getElementById(mrpid).value);
                
               qtyid = GetClientID("txtQty" + id);
               qty = document.getElementById(qtyid).value;
               if (qty == "") { qty = 0; }
               frqtyid = GetClientID("txtfreeqty" + id);
               frqty = document.getElementById(frqtyid).value;
               if (frqty == "") { frqty = 0; }
               totqty = parseInt(qty);
               if (mrpprc != "" && totqty > 0) {
                   taxprId = GetClientID("txtStaxper" + id);
                   taxAmtID = GetClientID("txtStax" + id);

                   cgsttaxprId = GetClientID("txtCgstRt" + id);
                   cgsttaxAmtID = GetClientID("txtCgstAmt" + id);
                   sgsttaxprId = GetClientID("txtSgstRt" + id);
                   sgsttaxAmtID = GetClientID("txtSgstAmt" + id);
                   igsttaxprId = GetClientID("txtIgstRt" + id);
                   igsttaxAmtID = GetClientID("txtIgstAmt" + id);


                   document.getElementById(taxprId).value = taxper;
                   amt = parseFloat(mrpprc) * parseFloat(totqty) * (parseFloat(taxper) / 100);
                   document.getElementById(taxAmtID).value = amt.toFixed(2);
                    
                   document.getElementById(cgsttaxprId).value = cgsttaxper;
                   amt = ((parseFloat(mrpprc) * parseFloat(totqty)) - parseFloat(document.getElementById(lessid).value)) * (parseFloat(cgsttaxper) / 100);
                   document.getElementById(cgsttaxAmtID).value = amt.toFixed(2);

                   document.getElementById(sgsttaxprId).value = sgsttaxper;
                   amt = ((parseFloat(mrpprc) * parseFloat(totqty)) - parseFloat(document.getElementById(lessid).value)) * (parseFloat(sgsttaxper) / 100);
                   document.getElementById(sgsttaxAmtID).value = amt.toFixed(2);

                   document.getElementById(igsttaxprId).value = igsttaxper;
                   amt = ((parseFloat(mrpprc) * parseFloat(totqty)) - parseFloat(document.getElementById(lessid).value)) * (parseFloat(igsttaxper) / 100);
                   document.getElementById(igsttaxAmtID).value = amt.toFixed(2);
                    
                   document.getElementById(taxAmtID).value = parseFloat(document.getElementById(cgsttaxAmtID).value) + parseFloat(document.getElementById(sgsttaxAmtID).value) + parseFloat(document.getElementById(igsttaxAmtID).value);
                  
                    
               }
           }

           calc_total(id);
       }


       function calc_total(id) {

           var taxamtid;
           var taxamt = '';
           var lessamtid;
           var lessamt = '';
           var totamtid;
           var totamt;
           var untid;
           var untprc = '';
           var qtyid;
           var qty = '';
           var i;

           if (id == "0") {
               for (i = 1; i <= 12; i++) {
                   totamt = '';
                   untid = GetClientID("txtUnitPrice" + i);
                   untprc = document.getElementById(untid).value;
                   //if(untprc==""){untprc="0";}
                   qtyid = GetClientID("txtQty" + i);
                   qty = document.getElementById(qtyid).value;
                   //if (qty == "") { qty = "0"; }
                   if (untprc != "" && qty != "") {
                       taxamtid = GetClientID("txtStax" + i);
                       taxamt = document.getElementById(taxamtid).value;
                       if (taxamt == "") { taxamt = "0"; }
                       lessamtid = GetClientID("txtLess" + i);
                       lessamt = document.getElementById(lessamtid).value;
                       if (lessamt == "") { lessamt = "0"; }

                       totamt = parseFloat(untprc) * parseFloat(qty) + parseFloat(taxamt) - parseFloat(lessamt);
                       totamtid = GetClientID("txtTotalPrice" + i);
                       document.getElementById(totamtid).value = totamt.toFixed(2);
                       alert(totamt - taxamt);
                       //document.getElementById(GetClientID("txtTotalPricewithouttax" + i)).value = totamt.toFixed(2) - taxamt;
                       
                   }
               }
           }
           else {
               untid = GetClientID("txtUnitPrice" + id);
               untprc = document.getElementById(untid).value;
               //if (untprc == "") { untprc = "0"; }
               qtyid = GetClientID("txtQty" + id);
               qty = document.getElementById(qtyid).value;
               //if (qty == "") { qty = "0"; }
               if (untprc != "" && qty != "") {
                   taxamtid = GetClientID("txtStax" + id);
                   taxamt = document.getElementById(taxamtid).value;
                   if (taxamt == "") { taxamt = "0"; }
                   lessamtid = GetClientID("txtLess" + id);
                   lessamt = document.getElementById(lessamtid).value;
                   if (lessamt == "") { lessamt = "0"; }
                   totamt = parseFloat(untprc) * parseFloat(qty) + parseFloat(taxamt) - parseFloat(lessamt);
                   totamtid = GetClientID("txtTotalPrice" + id);
                   document.getElementById(totamtid).value = totamt.toFixed(2);

                   TotalPricewithouttax = GetClientID("txtTotalPricewithouttax" + id);
                   document.getElementById(TotalPricewithouttax).value = parseFloat(totamt) - parseFloat(taxamt);
               }
           }

           calc_costprice(id);
           calc_nettotal();
       }

       function calc_costprice(id) {
           var totamtid;
           var totamt = '';
           var totqtyid;
           var totqty = '';
           var costprcid;
           var costprc = '';
           var i;
           if (id == "0") {
               for (i = 1; i <= 12; i++) {
                   costprc = '';
                   totamtid = GetClientID("txtTotalPrice" + i);
                   totamt = document.getElementById(totamtid).value;
                   // if (totamt == "") { totamt = "0"; }
                   totqtyid = GetClientID("txtTotQty" + i);
                   totqty = document.getElementById(totqtyid).value;
                   //if(totqty=="0"){totqty="0";}
                   if (totamt != "" && totqty != "") {
                       costprc = parseFloat(totamt) / parseFloat(totqty);
                   }
                   costprcid = GetClientID("txtCostPrice" + i);
                   document.getElementById(costprcid).value = costprc.toFixed(2);
               }
           }
           else {
               totamtid = GetClientID("txtTotalPrice" + id);
               totamt = document.getElementById(totamtid).value;
               //  if (totamt == "") { totamt = "0"; }
               totqtyid = GetClientID("txtTotQty" + id);
               totqty = document.getElementById(totqtyid).value;
               //  if (totqty == "") { totqty = "0"; }
               if (totamt != "" && totqty != "") {
                   costprc = parseFloat(totamt) / parseFloat(totqty);
               }
               costprcid = GetClientID("txtCostPrice" + id);
               document.getElementById(costprcid).value = costprc.toFixed(2);
           }
       }



       function unitprice_change(id) {
           calc_less(id);
           calc_tax(id);
       }

       function qty_change(id) {
           var qtyid;
           var qty = '';
           var freeqtyid;
           var freeqty = '';
           var totqtyid;
           var totqty = '';;
           qtyid = GetClientID("txtQty" + id);
           qty = document.getElementById(qtyid).value;

           if (qty == "") { qty = "0"; }
           freeqtyid = GetClientID("txtfreeqty" + id);
           freeqty = document.getElementById(freeqtyid).value;
           if (freeqty == "") { freeqty = "0"; }
           totqty = parseInt(qty) + parseInt(freeqty);
           totqtyid = GetClientID("txtTotQty" + id);
           document.getElementById(totqtyid).value = totqty.toFixed(2);
           calc_less(id);
           calc_tax(id);
       }

       function freeqty_change(id) {
           var qtyid;
           var qty = '';
           var freeqtyid;
           var freeqty = '';
           var totqtyid;
           var totqty = '';;
           qtyid = GetClientID("txtQty" + id);
           qty = document.getElementById(qtyid).value;

           if (qty == "") { qty = "0"; }
           freeqtyid = GetClientID("txtfreeqty" + id);
           freeqty = document.getElementById(freeqtyid).value;
           if (freeqty == "") { freeqty = "0"; }
           totqty = parseInt(qty) + parseInt(freeqty);
           totqtyid = GetClientID("txtTotQty" + id);
           document.getElementById(totqtyid).value = totqty.toFixed(2);
           calc_costprice(id);
           calc_tax(id);
       }
       function calc_nettotal() {

           var totamtid;
           var totamt = '';
           var netamtid;
           var grossamtid;
           var roundoffamtid;
           var netamt = 0,grossamt=0,roundoffamt=0;
           var i;
           for (i = 1; i <= 12; i++) {

               totamtid = GetClientID("txtTotalPrice" + i);
               totamt = document.getElementById(totamtid).value;
               if (totamt == "") { totamt = "0"; }
               grossamt = grossamt + parseFloat(totamt);
           }
           netamt = Math.round(grossamt);
           roundoffamt = netamt - grossamt;
           grossamtid = GetClientID("txtgross");
           roundoffamtid = GetClientID("txtRoundOff");
           netamtid = GetClientID("txtNetAmt");
           document.getElementById(grossamtid).value = grossamt.toFixed(2);
           document.getElementById(roundoffamtid).value = roundoffamt.toFixed(2);
           document.getElementById(netamtid).value = netamt.toFixed(2);
       }
    </script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
 
<%--For Busy Loader .............................--%>


<%-- 
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>--%>


    <%--For Busy Loader End.............................--%>


        <!-- Form Section html start -->

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div id="divContent" runat="server">
        <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Purchase Medicine</asp:Label>
        </div>
        <div class="formbox" style="width:100%;">
            <div class="form-sec">
                <div class="error">
                    <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                    </strong>
                    <div class="clear">
                    </div>
                </div>
                <asp:HiddenField ID="hdnMode" runat="server" Value="0" />   <asp:HiddenField ID="HiddenField1" runat="server"/>
                <div class="form-sec-row">
                    <label><strong>
                        Purchase Invoice :</strong></label>
                    <asp:TextBox ID="txtPurchaseMedicineId" runat="server" 
                        CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" CssClass="submit-button" Text="SEARCH" Height="28px" OnClientClick="ShowDialog()"/>
                    <asp:Button ID="Button4" runat="server" CssClass="submit-button" Text="FETCH"  Height="28px" onclick="Button4_Click" />&nbsp;
                    <asp:Button ID="Button5" runat="server" CssClass="submit-generate" Text="Quick Medicine Entry"  Height="28px" OnClick="Button5_Click" Style="margin-left:100px;margin-right:0px;" />
                    <div class="clear">
                    </div>
                </div>
              <div class="form-sec-row">
                    <label><strong>
                        Medicine/Reagent :</strong></label>
                    
                     <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" OnSelectedIndexChanged="ddl2_selecttedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>

                 <div class="form-sec-row">
                        <label>
                         <strong>
                        Supplier Name : </strong></label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                              >
                               
                         </asp:DropDownList>
                        <div class="clear">
                        </div>
                    </div>

                    <div class="form-sec-row">
                    <label><strong> 
                        Purchase Date :</strong></label>
                    <asp:TextBox ID="Calendar1" runat="server" CssClass="textbox-medium1" TextMode="Date"> 
                    </asp:TextBox>
                          <%--<asp:Label ID="Label3" runat="server" ForeColor="Green" Text="(DD/MM/YYYY)"></asp:Label>--%>
                    <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row">
                    <label><strong>
                      Supplier Bill No :</strong></label>
                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>
<div class="form-sec-row">
                    <label><strong>
                      Place of Origin :</strong></label>
                   <asp:DropDownList ID="ddlFrom" runat="server" CssClass="textbox-medium1" AutoPostBack="True" OnSelectedIndexChanged="ddlFrom_SelectedIndexChanged" ></asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>
                <div class="form-sec-row">
                    <label><strong>
                      Place of Delivery	:</strong></label>
                   <asp:DropDownList ID="ddlTo" runat="server" CssClass="textbox-medium1" AutoPostBack="True" OnSelectedIndexChanged="ddlTo_SelectedIndexChanged" ></asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>
                  <div class="form-sec-row">
                    <label><strong>
                     Type of GST :</strong></label>
                     <asp:DropDownList ID="ddlgsttype" runat="server" CssClass="textbox-medium1" Enabled="false" >
                         <asp:ListItem Selected="True" Value="S">CGST / SGST</asp:ListItem>
                         <asp:ListItem Value="I">IGST</asp:ListItem>
                     </asp:DropDownList>
                    <div class="clear">
                    </div>
                </div>



                    <div class="form-sec-row" style="display:none;">
                    <label><strong>
                      Checked By Medicine :</strong></label>
                    <asp:TextBox ID="txtCheckMedicine" runat="server" Width="700px" CssClass="textbox-medium1"></asp:TextBox>
    <cc1:AutoCompleteExtender ServiceMethod="SearchMedicine"   MinimumPrefixLength="2"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtCheckMedicine"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                    <div class="clear">
                    </div>
                </div>


                          <div class="form-sec-row" style="display:none;">
                    <label><strong>
                      Checked By Sub Group :</strong></label>
                    <asp:TextBox ID="txtSubgroup" runat="server" Width="700px" CssClass="textbox-medium1"></asp:TextBox>
    <cc1:AutoCompleteExtender ServiceMethod="SearchSubGroup"   MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtSubgroup"  ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                    <div class="clear">
                    </div>
                         
                </div>

                <div style="width:100%;overflow:auto;">
                <table style="text-align: left;">
                   
                    
                        <tr  style='background-color:#FF9300;'>
                        <td  align="center">
                            <asp:Label ID="lblMedi" runat="server" Font-Bold="True" Text="Medicine" Width="124px"></asp:Label>
                        </td>
                        <td align="center" style="display:none">
                            <asp:Label ID="lblMfg" runat="server" Font-Bold="True" Text="Manufacture "
                                Width="160px"></asp:Label>
                        </td>

                        <td  align="center" style="display:none">
                            <asp:Label ID="lblMediGrp" runat="server" Font-Bold="True" Text=" Group" Width="124px"></asp:Label>
                        </td>

                             <td  align="center" style="display:none">
                            <asp:Label ID="lblMediSubGrp" runat="server" Font-Bold="True" Text=" Group" Width="124px"></asp:Label>
                        </td>
                        
                        <td  align="center">
                            <asp:Label ID="lblBatch" runat="server" Font-Bold="True" Text="Batch" Width="120px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblExpiry" runat="server" Font-Bold="True" Text="Expiry" Width="150px"></asp:Label>
                        </td>
                    
                        <td  align="center">
                            <asp:Label ID="lblUnitPrice" runat="server" Font-Bold="True" Text="Trend Price" Width="80px"></asp:Label>
                        </td>
                        
                            <td  align="center">
                            <asp:Label ID="lblQty" runat="server" Font-Bold="True" Text="Qty" Width="80px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblfreeqty" runat="server" Font-Bold="True" Text="Free Qty" Width="80px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblTotqty" runat="server" Font-Bold="True" 
                                Text="Total Qty" Width="80px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                                Text="MRP" Width="80px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblLessDisc" runat="server" Font-Bold="True" Text="Less / Discount (%)" Width="80px"></asp:Label>
                            <asp:TextBox ID="txtlessper" runat="server" Width="50px"  onkeypress="return isNumberKey(event)"   style="text-align:right" onblur="calc_less('0')" Visible="false"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="%" Visible="false"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lbllessAmt" runat="server" Font-Bold="True" Text="Less Amount" Width="80px"></asp:Label>
                            
                        </td>
                            <td  align="center" style="width:10%;">
                            <asp:Label ID="lblHsn" runat="server" Font-Bold="True" Text="HSN Code" style="width:100%;"></asp:Label>
                        </td>
                             <td  align="center" style="width:10%;">
                            <asp:Label ID="lblCgstRt" runat="server" Font-Bold="True" Text="Cgst Rate" style="width:100%;"></asp:Label>
                        </td>
                        <td  align="center" style="width:10%;">
                            <asp:Label ID="lblCgstAmt" runat="server" Font-Bold="True" Text="Cgst Amt" style="width:100%;"></asp:Label>
                        </td>
                        <td  align="center" style="width:10%;">
                            <asp:Label ID="lblSgstRt" runat="server" Font-Bold="True" Text="Sgst Rate"  style="width:100%;"></asp:Label>
                        </td>
                        <td  align="center" style="width:10%;">
                            <asp:Label ID="lblSgstAmt" runat="server" Font-Bold="True" Text="Sgst Amt"  style="width:100%;"></asp:Label>
                        </td>
                              <td  align="center" style="width:10%;">
                            <asp:Label ID="lblIgstRt" runat="server" Font-Bold="True" Text="Igst Rate"  style="width:100%;"></asp:Label>
                        </td>
                        <td  align="center" style="width:10%;">
                            <asp:Label ID="lblIgstAmt" runat="server" Font-Bold="True" Text="Igst Amt"  style="width:100%;"></asp:Label>
                        </td>
                        <td style="display:none" align="center">
                            <asp:Label ID="lblTax" runat="server" Font-Bold="True" Text="Add S.Tax" Width="80px"></asp:Label>
                            <asp:TextBox ID="txtTaxper" runat="server" Width="50px"  onkeypress="return isNumberKey(event)" onblur="calc_tax('0')" style="text-align:right"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="%"></asp:Label>
                        </td>
                       
                        
                        
                        <td  align="center">
                            <asp:Label ID="lblcostprice" runat="server" Font-Bold="True" 
                                Text="Purchase Cost Price" Width="80px"></asp:Label>
                        </td>
                             <td class="style1"  align="center">
                              <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Taxable Amt" Width="80px"></asp:Label>
                            
                        </td>
                        

                             <td style="" align="center">
                            <asp:Label ID="lblTaxAmt" runat="server" Font-Bold="True" Text="Tax Amt" Width="80px"></asp:Label>
                            
                        </td>
                         <td  align="center">
                            <asp:Label ID="lblTotalPrice" runat="server" Font-Bold="True" 
                                Text="Total Amt" Width="80px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi1" runat="server" Width="154px" AutoPostBack="true"   onselectedindexchanged="ddlMed1_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor1" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td  style="display:none" >
                            <asp:DropDownList ID="ddlMfg1" runat="server" Width="154px" AutoPostBack="True" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none" >
                            <asp:DropDownList ID="ddlMediGrp1" runat="server" Width="154px" 
                                Height="16px">
                            </asp:DropDownList>
                        </td>
                           <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp1" runat="server" Width="154px"  >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch1" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar2" runat="server" Width="100px" TextMode="Date">
                              </asp:TextBox>--%>
                            <%--<asp:TextBox ID="Calendar2" runat="server" Width="100px">
                              </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth1" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear1" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                       
                        <td >
                            <asp:TextBox ID="txtUnitPrice1" runat="server" Width="80px"  style="text-align:right" onblur="unitprice_change('1')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>
                         <td > 
                            <asp:TextBox ID="txtQty1" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" style="text-align:right" onblur="qty_change('1')"></asp:TextBox>
                        </td>
                        <td > 
                            <asp:TextBox ID="txtfreeqty1" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('1')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty1" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice1" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('1')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessper1" runat="server" Width="80px"   
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('1')"  style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess1" runat="server" Width="80px" Enabled="false" 
                                  style="text-align:right" ></asp:TextBox>
                        </td>

                        <td class="style1">
                            <asp:TextBox ID="txtHsnCode1" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt1" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt1" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt1" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt1" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt1" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt1" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper1" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('1')" style="text-align:right" ></asp:TextBox>
                        </td>
                       
                        
                        
                        <td>
                            <asp:TextBox ID="txtCostPrice1" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPricewithouttax1" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td > 
                            <asp:TextBox ID="txtStax1" runat="server" Width="80px"  style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="txtTotalPrice1" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi2" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed2_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor2" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg2" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp2" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp2" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>
                        
                        <td >
                            <asp:TextBox ID="txtBatch2" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar3" runat="server" Width="100px">
                              </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth2" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear2" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                   
                        <td >
                            <asp:TextBox ID="txtUnitPrice2" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('2')"  onkeypress="return isNumberKey(event)"  ></asp:TextBox>
                        </td>

                             <td > 
                            <asp:TextBox ID="txtQty2" runat="server" Width="80px" 
                                style="text-align:right" onblur="qty_change('2')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty2" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('2')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty2" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice2" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('2')"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtLessper2" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('2')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess2" runat="server" Width="80px"  Enabled="false" 
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                         <td class="style1">
                            <asp:TextBox ID="txtHsnCode2" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt2" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt2" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt2" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt2" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt2" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt2" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none" > 
                            <asp:TextBox ID="txtStaxper2" runat="server" Width="80px"  onblur="calc_tax('2')"
                                 onkeypress="return isNumberKey(event)"  style="text-align:right" ></asp:TextBox>
                        </td>
                        

                        <td>
                            <asp:TextBox ID="txtCostPrice2" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPricewithouttax2" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>

                       
                         <td > 
                            <asp:TextBox ID="txtStax2" runat="server" Width="80px"  Enabled="false"
                                   style="text-align:right" ></asp:TextBox>
                        </td>
                         <td >
                            <asp:TextBox ID="txtTotalPrice2" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi3" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed3_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor3" runat="server" Visible="false"></asp:Label>
                        </td>
                         <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg3" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp3" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>
                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp3" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch3" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar4" runat="server" Width="100px">
                             </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth3" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear3" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                      
                        <td >
                            <asp:TextBox ID="txtUnitPrice3" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('3')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>

                          <td > 
                            <asp:TextBox ID="txtQty3" runat="server" Width="80px" 
                                 style="text-align:right" onblur="qty_change('3')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty3" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('3')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty3" runat="server" Width="80px" v></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtSellPrice3" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('3')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessper3" runat="server" Width="80px" AutoPostBack="True"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('3')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess3" runat="server" Width="80px" Enabled="false"  
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode3" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt3" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt3" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt3" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt3" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt3" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt3" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper3" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)"  style="text-align:right" ></asp:TextBox>
                        </td>
                        


                        <td>
                            <asp:TextBox ID="txtCostPrice3" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                          <td>
                            <asp:TextBox ID="txtTotalPricewithouttax3" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                     

                           <td > 
                            <asp:TextBox ID="txtStax3" runat="server" Width="80px"  style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         <td >
                            <asp:TextBox ID="txtTotalPrice3" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi4" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed4_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor4" runat="server" Visible="false"></asp:Label>
                        </td>
                       <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg4" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp4" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp4" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch4" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar5" runat="server" Width="100px">
                             </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth4" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear4" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                  
                        <td >
                            <asp:TextBox ID="txtUnitPrice4" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('4')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>

                              <td > 
                            <asp:TextBox ID="txtQty4" runat="server" Width="80px" 
                                 style="text-align:right" onblur="qty_change('4')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty4" runat="server" Width="80px"  
                                onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('4')"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="txtTotQty4" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>

                         <td>
                            <asp:TextBox ID="txtSellPrice4" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('4')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessper4" runat="server" Width="80px"   
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('4')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess4" runat="server" Width="80px"  Enabled="false"
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode4" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt4" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt4" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt4" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt4" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt4" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt4" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper4" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('4')" style="text-align:right" ></asp:TextBox>
                        </td>
                         
                       
                       


                        <td>
                            <asp:TextBox ID="txtCostPrice4" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                           <td>
                            <asp:TextBox ID="txtTotalPricewithouttax4" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>

                        

                          <td > 
                            <asp:TextBox ID="txtStax4" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                     <td >
                            <asp:TextBox ID="txtTotalPrice4" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi5" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed5_SelectedIndexChanged"  >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor5" runat="server" Visible="false"></asp:Label>
                        </td>
                         <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg5" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp5" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp5" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch5" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar6" runat="server" Width="100px">
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth5" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear5" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                   
                        <td >
                            <asp:TextBox ID="txtUnitPrice5" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('5')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>
                             <td > 
                            <asp:TextBox ID="txtQty5" runat="server" Width="80px" 
                                 style="text-align:right" onblur="qty_change('5')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty5" runat="server" Width="80px"
                                onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('5')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty5" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtSellPrice5" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('5')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessper5" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('5')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess5" runat="server" Width="80px"  Enabled="false"
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                         <td class="style1">
                            <asp:TextBox ID="txtHsnCode5" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt5" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt5" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt5" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt5" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt5" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt5" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper5" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('5')" style="text-align:right" ></asp:TextBox>
                        </td>
                        


                        <td>
                            <asp:TextBox ID="txtCostPrice5" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>

                         <td>
                            <asp:TextBox ID="txtTotalPricewithouttax5" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        

                          <td > 
                            <asp:TextBox ID="txtStax5" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                       <td >
                            <asp:TextBox ID="txtTotalPrice5" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi6" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed6_SelectedIndexChanged"  >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor6" runat="server" Visible="false"></asp:Label>
                        </td>
                          <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg6" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp6" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp6" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch6" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar7" runat="server" Width="100px">
                           </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth6" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear6" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                     
                        <td >
                            <asp:TextBox ID="txtUnitPrice6" runat="server" Width="80px"  style="text-align:right" onblur="unitprice_change('6')" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>

                           <td > 
                            <asp:TextBox ID="txtQty6" runat="server" Width="80px" 
                                 style="text-align:right" onblur="qty_change('6')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty6" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('6')"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtTotQty6" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice6" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('6')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessper6" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('6')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess6" runat="server" Width="80px"   Enabled="false"
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode6" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt6" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt6" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt6" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt6" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt6" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt6" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper6" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('6')" style="text-align:right" ></asp:TextBox>
                        </td>
                         
                        


                        <td>
                            <asp:TextBox ID="txtCostPrice6" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>


                       <td>
                            <asp:TextBox ID="txtTotalPricewithouttax6" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>

                          <td > 
                            <asp:TextBox ID="txtStax6" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                        
                         <td >
                            <asp:TextBox ID="txtTotalPrice6" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi7" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed7_SelectedIndexChanged"  >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor7" runat="server" Visible="false"></asp:Label>
                        </td>
                         <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg7" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp7" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp7" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch7" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar8" runat="server" Width="100px">
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth7" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear7" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                  
                        <td >
                            <asp:TextBox ID="txtUnitPrice7" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('7')"  onkeypress="return isNumberKey(event)"  ></asp:TextBox>
                        </td>

                              <td > 
                            <asp:TextBox ID="txtQty7" runat="server" Width="80px"  
                                 style="text-align:right" onblur="qty_change('7')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty7" runat="server" Width="80px"  onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('7')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty7" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtSellPrice7" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('7')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLessper7" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('7')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess7" runat="server" Width="80px"   Enabled="false"
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode7" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt7" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt7" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt7" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt7" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt7" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt7" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper7" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('7')" style="text-align:right" ></asp:TextBox>
                        </td>
                        


                        <td>
                            <asp:TextBox ID="txtCostPrice7" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="txtTotalPricewithouttax7" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                       

                          <td > 
                            <asp:TextBox ID="txtStax7" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                        <td >
                            <asp:TextBox ID="txtTotalPrice7" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi8" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed8_SelectedIndexChanged"   >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor8" runat="server" Visible="false"></asp:Label>
                        </td>
                         <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg8" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp8" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp8" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch8" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar9" runat="server" Width="100px">
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth8" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear8" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                     
                        <td >
                            <asp:TextBox ID="txtUnitPrice8" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('8')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>

                           <td > 
                            <asp:TextBox ID="txtQty8" runat="server" Width="80px"  style="text-align:right" onblur="qty_change('8')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty8" runat="server" Width="80px" 
                                onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('8')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty8" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice8" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('8')"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtLessper8" runat="server" Width="80px"   
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('8')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess8" runat="server" Width="80px" Enabled="false"
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode8" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt8" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt8" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt8" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt8" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt8" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt8" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper8" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('8')" style="text-align:right" ></asp:TextBox>
                        </td>
                         

                        <td>
                            <asp:TextBox ID="txtCostPrice8" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                          <td>
                            <asp:TextBox ID="txtTotalPricewithouttax8" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        

                            <td > 
                            <asp:TextBox ID="txtStax8" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                      <td >
                            <asp:TextBox ID="txtTotalPrice8" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                         <td >
                            <asp:DropDownList ID="ddlMedi9" runat="server" Width="154px"  AutoPostBack="true" onselectedindexchanged="ddlMed9_SelectedIndexChanged" >
                            </asp:DropDownList>
                             <asp:Label ID="lblConvrtFactor9" runat="server" Visible="false"></asp:Label>
                        </td>
                         <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg9" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp9" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp9" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>

                       
                        <td >
                            <asp:TextBox ID="txtBatch9" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar10" runat="server" Width="100px">
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth9" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear9" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                    
                        <td >
                            <asp:TextBox ID="txtUnitPrice9" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('9')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>


                        
                        <td > 
                            <asp:TextBox ID="txtQty9" runat="server" Width="80px"  style="text-align:right" onblur="qty_change('9')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty9" runat="server" Width="80px"  onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('9')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty9" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice9" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('9')"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtLessper9" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('9')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess9" runat="server" Width="80px"   Enabled="false"
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode9" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt9" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt9" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt9" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt9" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt9" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt9" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper9" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('9')" style="text-align:right" ></asp:TextBox>
                        </td>
                         

                        <td>
                            <asp:TextBox ID="txtCostPrice9" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="txtTotalPricewithouttax9" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                       
                        <td > 
                            <asp:TextBox ID="txtStax9" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                        <td >
                            <asp:TextBox ID="txtTotalPrice9" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi10" runat="server" Width="154px"  AutoPostBack="true" onselectedindexchanged="ddlMed10_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor10" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg10" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp10" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp10" runat="server" Width="154px"  >
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch10" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar11" runat="server" Width="100px" >
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth10" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear10" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                 
                        <td >
                            <asp:TextBox ID="txtUnitPrice10" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('10')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>

                               <td > 
                            <asp:TextBox ID="txtQty10" runat="server" Width="80px"  style="text-align:right" onblur="qty_change('10')"></asp:TextBox>
                        </td>

                                                <td > 
                            <asp:TextBox ID="txtfreeqty10" runat="server" Width="80px" onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('10')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty10" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice10" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('10')"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtLessper10" runat="server" Width="80px"   
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('10')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess10" runat="server" Width="80px" Enabled="false" 
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode10" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt10" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt10" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt10" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt10" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt10" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt10" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper10" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('10')" style="text-align:right" ></asp:TextBox>
                        </td>
                        


                        <td>
                            <asp:TextBox ID="txtCostPrice10" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                           <td>
                            <asp:TextBox ID="txtTotalPricewithouttax10" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                       

                         <td > 
                            <asp:TextBox ID="txtStax10" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                      <td >
                            <asp:TextBox ID="txtTotalPrice10" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         

                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi11" runat="server" Width="154px" AutoPostBack="true" onselectedindexchanged="ddlMed11_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor11" runat="server" Visible="false"></asp:Label>
                        </td>
                         <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg11" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp11" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp11" runat="server" Width="154px">
                            </asp:DropDownList>
                        </td>

                        
                        <td >
                            <asp:TextBox ID="txtBatch11" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar12" runat="server" Width="100px">
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth11" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear11" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                       
                        <td >
                            <asp:TextBox ID="txtUnitPrice11" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('11')"  onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>

                         <td > 
                            <asp:TextBox ID="txtQty11" runat="server" Width="80px"  style="text-align:right" onblur="qty_change('11')"></asp:TextBox>
                        </td>
                        <td > 
                            <asp:TextBox ID="txtfreeqty11" runat="server" Width="80px"  onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('11')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty11" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice11" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('11')"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtLessper11" runat="server" Width="80px" onblur="calc_less('11')"  
                                 onkeypress="return isNumberKey(event)" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess11" runat="server" Width="80px"  Enabled="false"
                                 style="text-align:right" ></asp:TextBox>
                        </td>
                         <td class="style1">
                            <asp:TextBox ID="txtHsnCode11" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt11" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt11" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt11" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt11" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt11" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt11" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper11" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('11')" style="text-align:right" ></asp:TextBox>
                        </td>
                       


                        
                        


                        <td>
                            <asp:TextBox ID="txtCostPrice11" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="txtTotalPricewithouttax11" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                       

                        <td > 
                            <asp:TextBox ID="txtStax11" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                        <td >
                            <asp:TextBox ID="txtTotalPrice11" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                         
                    </tr>
                    <tr>
                        <td >
                            <asp:DropDownList ID="ddlMedi12" runat="server" Width="154px"  AutoPostBack="true" onselectedindexchanged="ddlMed12_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblConvrtFactor12" runat="server" Visible="false"></asp:Label>
                        </td>
                         <td  style="display:none">
                            <asp:DropDownList ID="ddlMfg12" runat="server" Width="154px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="display:none">
                            <asp:DropDownList ID="ddlMediGrp12" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>

                            <td style="display:none">
                            <asp:DropDownList ID="ddlMediSubGrp12" runat="server" Width="154px" >
                            </asp:DropDownList>
                        </td>


                        
                        <td >
                            <asp:TextBox ID="txtBatch12" runat="server" Width="120px" ></asp:TextBox>
                        </td>
                        <td >
                            <%--<asp:TextBox ID="Calendar13" runat="server" Width="100px">
                            </asp:TextBox>--%>
                            <asp:DropDownList ID="ddlmonth12" runat="server"  CssClass="textbox-medium1" Width="70px"></asp:DropDownList>
                            <asp:DropDownList ID="ddlyear12" runat="server"  CssClass="textbox-medium1"  Width="70px"></asp:DropDownList>
                        </td>
                       
                        <td >
                            <asp:TextBox ID="txtUnitPrice12" runat="server" Width="80px" style="text-align:right" onblur="unitprice_change('12')"  onkeypress="return isNumberKey(event)"  ></asp:TextBox>
                        </td>

                         <td > 
                            <asp:TextBox ID="txtQty12" runat="server" Width="80px" 
                                style="text-align:right" onblur="qty_change('12')"></asp:TextBox>
                        </td>

                        <td > 
                            <asp:TextBox ID="txtfreeqty12" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" style="text-align:right" onblur="freeqty_change('12')"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotQty12" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellPrice12" runat="server" Width="80px" style="text-align:right" onblur="calc_tax('12')"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtLessper12" runat="server" Width="80px"  
                                 onkeypress="return isNumberKey(event)" onblur="calc_less('12')" style="text-align:right" ></asp:TextBox> 
                        </td>
                        <td > 
                            <asp:TextBox ID="txtLess12" runat="server" Width="80px" Enabled="false"
                                  style="text-align:right" ></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtHsnCode12" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstRt12" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtCgstAmt12" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstRt12" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSgstAmt12" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="style1">
                            <asp:TextBox ID="txtIgstRt12" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtIgstAmt12" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="display:none"> 
                            <asp:TextBox ID="txtStaxper12" runat="server" Width="80px" 
                                 onkeypress="return isNumberKey(event)" onblur="calc_tax('12')" style="text-align:right" ></asp:TextBox>
                        </td>
                       



                        <td>
                            <asp:TextBox ID="txtCostPrice12" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>

                            <td>
                            <asp:TextBox ID="txtTotalPricewithouttax12" runat="server" Width="100px" style="text-align:right;" Enabled="false"></asp:TextBox>
                        </td>
                       

                          <td > 
                            <asp:TextBox ID="txtStax12" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                    
                         <td >
                            <asp:TextBox ID="txtTotalPrice12" runat="server" Width="80px" style="text-align:right" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </div>

                <div class="form-sec-row">
                    <table style="text-align: left;">
                        <tr>
                            <td style="width:100px;">Total</td>
                            <td style="width:150px;"><asp:TextBox ID="txtgross" runat="server" Width="99%" style="text-align:right;"></asp:TextBox></td>
                            <td style="width:100px;">Round Off</td>
                            <td style="width:150px;"><asp:TextBox ID="txtRoundOff" runat="server" Width="99%" style="text-align:right;"></asp:TextBox></td>
                            <td style="width:100px;">Net Amount</td>
                            <td style="width:150px;"><asp:TextBox ID="txtNetAmt" runat="server" style="text-align:right"></asp:TextBox></td>
                        </tr>
                    </table>
                    <div class="clear">
                    </div>
                </div>
                  
                            <div class="form-sec-row" style="display:none;">
                    <label><strong>
                      Discount :</strong></label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" Enabled="false"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                          <div class="form-sec-row" style="display:none;">
                    <label><strong>
                      Vat (%) :</strong></label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>

                     <%--     <div class="form-sec-row">
                    <label><strong>
                      Net Amount :</strong></label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                    <div class="clear">
                    </div>
                </div>--%>
                  <div class="form-sec-row">
                    &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                    <asp:Button ID="Button1" runat="server" CssClass="submit-button" Text="Submit"  Height="28px"
                          onclick="Button1_Click"  />
                    <asp:Button ID="Button2" runat="server" CssClass="submit-button" Text="Cancel"  Height="28px"
                          onclick="Button2_Click"  />
                    <asp:Button ID="Button6" runat="server" CssClass="submit-button" Text="Back"  Height="28px"
                          onclick="Button6_Click"  />
                    <div class="clear">
                    </div>
                               <asp:HiddenField ID="hdnRowId1" runat="server" Value="0" />     
                               <asp:HiddenField ID="hdnRowId2" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId3" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId4" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId5" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId6" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId7" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId8" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId9" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId10" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId11" runat="server" Value="0" />  
                               <asp:HiddenField ID="hdnRowId12" runat="server" Value="0" />  

                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        </div>
     </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

