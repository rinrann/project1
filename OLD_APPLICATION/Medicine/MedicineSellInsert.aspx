<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="MedicineSellInsert.aspx.cs" Inherits="Medicine_MedicineSellInsert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="javascript">
       function ShowDialog() {
           var rtvalue = window.open("PurchaseMedicinePopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
           document.getElementById("ctl00_ContentPlaceHolder1_txtPurchaseMedicineId").value = rtvalue;

       }
</script>

<%--For Busy Loader .............................--%>

  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    
       <div id="aaa" class="progressBackgroundFilter">          </div>
        <div id="bbb" class="processMessage"><img alt="Loading" src="../images/pwait.gif"/>
      
              </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

    <%--For Busy Loader End.............................--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
       <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Medicine Sell Price</asp:Label>
        </div>

              <div class="error">
                    <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                    </strong>
                    <div class="clear">
                    </div>
                </div>

         
              <div class="form-sec-row">
                    <label><strong>
                        Purchase Invoice :</strong></label>
                    <asp:TextBox ID="txtPurchaseMedicineId" runat="server" 
                        CssClass="textbox-medium1" Enabled="False"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" CssClass="submit-button" Text="SEARCH" Height="28px" OnClientClick="ShowDialog()"/>
                    <asp:Button ID="Button4" runat="server" CssClass="submit-button" Text="FETCH"  Height="28px" onclick="Button4_Click" />
                    <div class="clear">
                    </div>
                </div>

                <table width="100%">                   
                    <tr  style='background-color:#FF9300;'>
                    

                        <td  align="center">
                            <asp:Label ID="lblMediGrp" runat="server" Font-Bold="True" Text="Medicine Group" Width="124px"></asp:Label>
                        </td>

                             <td  align="center">
                            <asp:Label ID="lblMediSubGrp" runat="server" Font-Bold="True" Text="Med. Sub Group" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblMedi" runat="server" Font-Bold="True" Text="Medicine" Width="124px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblBatch" runat="server" Font-Bold="True" Text="Batch" Width="110px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblExpiry" runat="server" Font-Bold="True" Text="Expiry" Width="124px"></asp:Label>
                        </td>
                    
                   <%--    <td  align="center">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Quantity" Width="124px"></asp:Label>
                        </td>--%>

                        <td  align="center">
                            <asp:Label ID="lblUnitPrice" runat="server" Font-Bold="True" Text="Unit Price" Width="110px"></asp:Label>
                        </td>
                        <td  align="center">
                            <asp:Label ID="lblTotalPrice" runat="server" Font-Bold="True" 
                                Text="Sell Price/Unit" Width="145px"></asp:Label>
                        </td>

                        <%--     <td  align="center">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" 
                                Text="Unit Selling Price" Width="153px"></asp:Label>
                        </td>--%>

                    </tr>
                    <tr>
                   
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp1" runat="server" Width="154px" 
                               AutoPostBack="true"  
                                onselectedindexchanged="ddlMediGrp1_SelectedIndexChanged" Height="16px">
                            </asp:DropDownList>
                        </td>
                           <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp1" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="ddlMediSubGrp1_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi1" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                               <asp:DropDownList ID="DropDownList1" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar2" runat="server"  Enabled="False" ></asp:TextBox>
                        </td>
                   
                   <%--<td class="style1">
                            <asp:TextBox ID="txtQty1" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice1" runat="server" Enabled="False"  ></asp:TextBox>
                        </td>
                        <td >
                            <asp:TextBox ID="txtTotalPrice1" runat="server" Width="82px"  ></asp:TextBox>/
                                      <asp:TextBox ID="TextBox1" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                      
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp2" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp2" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp2_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi2" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                       <td class="style1">
                               <asp:DropDownList ID="DropDownList2" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar3" runat="server"  Enabled="False" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                    
                   <%--   <td class="style1">
                            <asp:TextBox ID="txtQty2" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice2" runat="server"  Enabled="False" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice2" runat="server" Width="82px" ></asp:TextBox>/
                            <asp:TextBox ID="TextBox2" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                  
                    <tr>
                     
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp3" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp3" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp3_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi3" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                          <td class="style1">
                               <asp:DropDownList ID="DropDownList3" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList3_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar4" runat="server"   Enabled="False" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                    
                      <%--  <td class="style1">
                            <asp:TextBox ID="txtQty3" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice3" runat="server"  Enabled="False" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice3" runat="server" Width="82px" ></asp:TextBox>/
                            <asp:TextBox ID="TextBox3" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                  
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp4" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp4" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp4_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi4" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                               <asp:DropDownList ID="DropDownList4" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar5" runat="server"  Enabled="False" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                      
                       <%--   <td class="style1">
                            <asp:TextBox ID="txtQty4" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice4" runat="server" Enabled="False" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice4" runat="server" Width="82px" ></asp:TextBox>/
                            <asp:TextBox ID="TextBox4" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                      
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp5" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp5_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp5" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp5_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi5" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi5_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                               <asp:DropDownList ID="DropDownList5" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList5_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar6" runat="server"  Enabled="False"  >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                    
                   <%--     <td class="style1">
                            <asp:TextBox ID="txtQty5" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice5" runat="server"  Enabled="False"  ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice5" runat="server" Width="82px"></asp:TextBox>/
                            <asp:TextBox ID="TextBox5" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp6" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp6_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp6" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp6_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi6" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi6_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                      <td class="style1">
                               <asp:DropDownList ID="DropDownList6" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList6_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar7" runat="server" Enabled="False"  >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
<%--
                            <td class="style1">
                            <asp:TextBox ID="txtQty6" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>
                 
                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice6" runat="server" Enabled="False" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice6" runat="server"  Width="82px"></asp:TextBox>/
                             <asp:TextBox ID="TextBox6" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                     
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp7" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp7_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp7" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp7_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi7" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi7_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                               <asp:DropDownList ID="DropDownList7" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList7_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar8" runat="server"  Enabled="False"  >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                    
                      <%--  <td class="style1">
                            <asp:TextBox ID="txtQty7" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice7" runat="server"  Enabled="false" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice7" runat="server" Width="82px" ></asp:TextBox>/
                             <asp:TextBox ID="TextBox7" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                         
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp8" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp8_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp8" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp8_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi8" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi8_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                      <td class="style1">
                               <asp:DropDownList ID="DropDownList8" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList8_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar9" runat="server"  Enabled="False"  >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                  
                 <%--     <td class="style1">
                            <asp:TextBox ID="txtQty8" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice8" runat="server" Enabled="False"  ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice8" runat="server" Width="82px" ></asp:TextBox>/
                             <asp:TextBox ID="TextBox8" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp9" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp9_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp9" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp9_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi9" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi9_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                       <td class="style1">
                               <asp:DropDownList ID="DropDownList9" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList9_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar10" runat="server"  Enabled="False" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                     
                        <%-- <td class="style1">
                            <asp:TextBox ID="txtQty9" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice9" runat="server" Enabled="False"  ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice9" runat="server"  Width="82px" ></asp:TextBox>/
                             <asp:TextBox ID="TextBox9" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp10" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp10_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp10" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp10_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi10" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi10_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                         <td class="style1">
                               <asp:DropDownList ID="DropDownList10" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList10_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar11" runat="server"  Enabled="False"  >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                  <%-- 
                       <td class="style1">
                            <asp:TextBox ID="txtQty10" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice10" runat="server" Enabled="False" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice10" runat="server" Width="82px" ></asp:TextBox>/
                             <asp:TextBox ID="TextBox10" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                      
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp11" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp11_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp11" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp11_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>

                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi11" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi11_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                               <asp:DropDownList ID="DropDownList11" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList11_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar12" runat="server"  Enabled="False"  >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                    
                      <%--  <td class="style1">
                            <asp:TextBox ID="txtQty11" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice11" runat="server" Enabled="False"  ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice11" runat="server" Width="82px"></asp:TextBox>/
                             <asp:TextBox ID="TextBox11" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       
                        <td class="style1">
                            <asp:DropDownList ID="ddlMediGrp12" runat="server" Width="154px" 
                               AutoPostBack="true"  onselectedindexchanged="ddlMediGrp12_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>

                            <td class="style1">
                            <asp:DropDownList ID="ddlMediSubGrp12" runat="server" Width="154px" 
                                AutoPostBack="True" 
                                    onselectedindexchanged="ddlMediSubGrp12_SelectedIndexChanged"  >
                            </asp:DropDownList>
                        </td>


                        <td class="style1">
                            <asp:DropDownList ID="ddlMedi12" runat="server" Width="154px" 
                                AutoPostBack="True" onselectedindexchanged="ddlMedi12_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                          <td class="style1">
                               <asp:DropDownList ID="DropDownList12" runat="server" Width="110px" 
                                AutoPostBack="True" 
                                   onselectedindexchanged="DropDownList12_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="Calendar13" runat="server"   Enabled="False" >
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </td>
                      <%--   <td class="style1">
                            <asp:TextBox ID="txtQty12" runat="server" Width="154px" Enabled="False"  ></asp:TextBox>
                        </td>--%>

                        <td class="style1">
                            <asp:TextBox ID="txtUnitPrice12" runat="server"  Enabled="False" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalPrice12" runat="server" Width="82px"></asp:TextBox>/
                             <asp:TextBox ID="TextBox12" Enabled="false" runat="server"  Width="46px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
             

                 <div class="form-sec-row">
                    &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                    <asp:Button ID="Button1" runat="server" CssClass="submit-button" Text="Submit"  Height="28px"
                          onclick="Button1_Click"  />
                    <asp:Button ID="Button2" runat="server" CssClass="submit-button" Text="Cancel"  Height="28px"
                          onclick="Button2_Click"  />
                    <div class="clear">
                    </div>
                    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
     
</asp:Content>

