<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="InstrumentCost.aspx.cs" Inherits="IPD_InstrumentCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 


<script type="text/javascript" language="javascript">
    function ShowDialog() {
        var rtvalue = window.open("IpdpatientInstrumentPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox46").value = rtvalue.NameValue;
    }

    function Calling() {
        var date = new Date();
        $("input[id$='TextBox52']").datepicker({ dateFormat: 'dd/mm/yy' });

        $("input[id$='Tab2']").click(function () {


            if ($("input[id$='TextBox46']").val() == '') {
                alert('Please Enter Registration No !');
                $("input[id$='TextBox46']").focus();
                $("input[id$='TextBox46']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox46']").removeClass('textboxerr');
            }
        });

        $("input[id$='Button1']").click(function () {


            if ($("input[id$='TextBox52']").val() == '') {
                alert('Please Enter Date !');
                $("input[id$='TextBox52']").focus();
                $("input[id$='TextBox52']").addClass('textboxerr');
                return false;
            }
            else {
                $("input[id$='TextBox52']").removeClass('textboxerr');
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

    function calc_cost(src,trg) {
        var qty;
        var untcst;
        var cst;
        var srcid;
        var trgid;

        srcid = GetClientID("TextBox" + src);
        trgid = GetClientID("TextBox" + trg);
        qty = document.getElementById(srcid).value;
        untcst = document.getElementById(trgid).value;
        cst = parseFloat(untcst) * parseFloat(qty);
        document.getElementById(trgid).value = cst.toFixed(2);
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
         <asp:Label ID="Label1" runat="server">Instrument Cost</asp:Label>
     </div>

     
    <table width="290px" cellpadding="0" cellspacing="0">
         <tr>
            <td>
                <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px"  Height="40px" 
                    CssClass="Initial" runat="server" onclick="Tab1_Click"
                     /></td>
                     <td>
                <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial"  Width="145px"  Height="40px" 
                    runat="server" onclick="Tab2_Click"
                    /></td>
           
                     </tr>
                     </table>


     <div class="formbox">
      <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
     <div class="form-sec">
        
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                </strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
			           
                       
             <div class="form-sec-row">
                        <label>
                        <strong>
                       Registration No :</strong></label>
                          <asp:TextBox ID="TextBox46" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                                <asp:Button ID="Button3" runat="server" Height="28px" Text="Quick Search" CssClass="submit-buttonCheck" OnClientClick="ShowDialog()"/>
                              <asp:Button ID="Button2" runat="server" Text="Fetch"  Height="28px"
                            CssClass="submit-button" onclick="Button2_Click"/>
                        <div class="clear">
                        </div>
                    </div>
                         <div class="form-sec-row">
                        <label>
                        <strong>
                     Requisition No :</strong></label>
                          <asp:TextBox ID="TextBox47" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Patient's Name :</strong></label>
                          <asp:TextBox ID="TextBox48" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                     Current Bed No :</strong></label>
                          <asp:TextBox ID="TextBox49" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                         <div class="form-sec-row">
                            
                            <label id="lbltype" runat="server">
                        <strong>
                     Operation Name :</strong></label>
                             

                            <asp:TextBox ID="TextBox50" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                          
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Admission Date :</strong></label>
                          <asp:TextBox ID="TextBox51" runat="server" CssClass="textbox-medium1" 
                                Enabled="False"></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>

                        <div class="form-sec-row">
                        <label>
                        <strong>
                       Entry Date :</strong></label>
                          <asp:TextBox ID="TextBox52" runat="server" CssClass="textbox-medium1" 
                              ></asp:TextBox>
                        <div class="clear">
                        </div>
                    </div>
   
     </div>

     <center>
    <table border="1" width="90%" cellpadding="0" cellspacing="0" >
          
       <tr style='background-color:#FF9300;'>
                <th>
               
             <label><strong>Instrument Name</strong></label> 
             
        </th> 

      
                <th>
                           
             <label ><strong> Unit </strong></label>
                           
</th>
 <th>
                     
             <label><strong>Usage Cost</strong></label> 
           
            
</th>

<th>
    <label><strong>Continue</strong></label> 
</th>
 <th>
             
             <label><strong>Remarks</strong></label> 
             
            
</th>
        
                      
            </tr>
                <tr>
                <td align="center" style='width:160px;'>
         
             <asp:DropDownList ID="DropDownList1" runat="server"  
                            Width="250px"  OnSelectedIndexChanged="ins1_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
          
            
</td> 

       
      
 <td  align="center" >
               
                       <asp:TextBox ID="TextBox1" runat="server"  Width="50px" onblur="calc_cost('1','2')"></asp:TextBox>
      
            
</td>
 <td align="center">
                    
             <asp:TextBox ID="TextBox2" runat="server"  Width="60px"></asp:TextBox>
     
            
</td>
                    <td>
                        <asp:CheckBox ID="chkins1" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox3" runat="server"  Width="150px" ></asp:TextBox>
           
            
</td>
 </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
            <asp:DropDownList ID="DropDownList2" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins2_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

    
         
 <td align="center">
                   
                         <asp:TextBox ID="TextBox4" runat="server"  Width="50px" onblur="calc_cost('4','5')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox5" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
    <td>
                        <asp:CheckBox ID="chkins2" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox6" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
   </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
            <asp:DropDownList ID="DropDownList3" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins3_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox7" runat="server"  Width="50px" onblur="calc_cost('7','8')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                        <asp:TextBox ID="TextBox8" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
    <td>
                        <asp:CheckBox ID="chkins3" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox9" runat="server"  Width="150px"></asp:TextBox>
            
            
</td>
  </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
           <asp:DropDownList ID="DropDownList4" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins4_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
       
                         <asp:TextBox ID="TextBox10" runat="server"  Width="50px" onblur="calc_cost('10','11')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                          <asp:TextBox ID="TextBox11" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
    <td>
                        <asp:CheckBox ID="chkins4" runat="server" />
                    </td>

<td align="center">
                   
             <asp:TextBox ID="TextBox12" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
</tr>

<tr>
                <td align="center" style='width:160px;'>
                   
            <asp:DropDownList ID="DropDownList5" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins5_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox13" runat="server"  Width="50px" onblur="calc_cost('12','13')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox14" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>

    <td>
                        <asp:CheckBox ID="chkins5" runat="server" />
                    </td>

<td align="center">
                   
             <asp:TextBox ID="TextBox15" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
  </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
            <asp:DropDownList ID="DropDownList6" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins6_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox16" runat="server"  Width="50px" onblur="calc_cost('16','17')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox17" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>

    <td>
                        <asp:CheckBox ID="chkins6" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox18" runat="server"  Width="150px" ></asp:TextBox>
            
            
</td>
  </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
            <asp:DropDownList ID="DropDownList7" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins7_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 
 <td align="center">
                   
                         <asp:TextBox ID="TextBox19" runat="server"  Width="50px" onblur="calc_cost('19','20')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox20" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
    <td>
                        <asp:CheckBox ID="chkins7" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox21" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
 </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
            <asp:DropDownList ID="DropDownList8" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins8_selIndexChange" AutoPostBack="true" >
                            </asp:DropDownList>
            
            
</td> 
   
      
 <td align="center">
                   
                         <asp:TextBox ID="TextBox22" runat="server"  Width="50px" onblur="calc_cost('22','23')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                          <asp:TextBox ID="TextBox23" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>

    <td>
                        <asp:CheckBox ID="chkins8" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox24" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
  </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
           <asp:DropDownList ID="DropDownList9" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins9_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox25" runat="server"  Width="50px" onblur="calc_cost('25','26')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox26" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>

    <td>
                        <asp:CheckBox ID="chkins9" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox27" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
   </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
            <asp:DropDownList ID="DropDownList10" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins10_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox28" runat="server"  Width="50px" onblur="calc_cost('28','29')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox29" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
    <td>
                        <asp:CheckBox ID="chkins10" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox30" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
 </tr>

<tr>
                <td align="center" style='width:160px;'>
                   
             <asp:DropDownList ID="DropDownList11" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins11_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox31" runat="server"  Width="50px" onblur="calc_cost('31','32')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox32" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
    <td>
                        <asp:CheckBox ID="chkins11" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox33" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
  </tr>
  <tr>
                <td align="center" style='width:160px;'>
                   
             <asp:DropDownList ID="DropDownList12" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins12_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox34" runat="server"  Width="50px" onblur="calc_cost('34','35')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox35" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
      <td>
                        <asp:CheckBox ID="chkins12" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox36" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
  </tr>

  <tr>
                <td align="center" style='width:160px;'>
                   
             <asp:DropDownList ID="DropDownList13" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins13_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox37" runat="server"  Width="50px" onblur="calc_cost('37','38')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox38" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
      <td>
                        <asp:CheckBox ID="chkins13" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox39" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
  </tr>

  <tr>
                <td align="center" style='width:160px;'>
                   
             <asp:DropDownList ID="DropDownList14" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins14_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox40" runat="server"  Width="50px" onblur="calc_cost('40','41')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox41" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
      <td>
                        <asp:CheckBox ID="chkins14" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox42" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
  </tr>

  <tr>
                <td align="center" style='width:160px;'>
                   
             <asp:DropDownList ID="DropDownList15" runat="server"  
                            Width="250px" OnSelectedIndexChanged="ins15_selIndexChange" AutoPostBack="true">
                            </asp:DropDownList>
            
            
</td> 

 <td align="center">
                   
                         <asp:TextBox ID="TextBox43" runat="server"  Width="50px" onblur="calc_cost('43','44')"></asp:TextBox>
            
            
</td>
 <td align="center">
                   
                         <asp:TextBox ID="TextBox44" runat="server"  Width="60px"></asp:TextBox>
            
            
</td>
      <td>
                        <asp:CheckBox ID="chkins15" runat="server" />
                    </td>
<td align="center">
                   
             <asp:TextBox ID="TextBox45" runat="server"  Width="150px"  ></asp:TextBox>
            
            
</td>
  </tr>
 </table>
  </center> 
     <div class="form-sec-row"> 
     <label><strong ></strong></label>
     <asp:Button ID="Button1" runat="server" Text="Submit"  Height="28px"
        CssClass="submit-buttondtls" onclick="Button1_Click"/> 
       <asp:Button ID="Button4" runat="server" Text="Cancel"  Height="28px"
        CssClass="submit-buttondtls" onclick="Button4_Click" />
    
     </div>
   
    <div id="otdiv" class="listing"  style='width:100%; height:150px; overflow:auto;' runat="server">
                <asp:GridView id="GridView2"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="OperationReqID" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow"  PageSize="100"
                            onrowcommand="GridView2_RowCommand"  >
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Requisition No" >
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("OperationReqID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientRegId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Operation Type">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("OperationTypeName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Operation Name">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                     </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>

            <div id="srvdiv" class="listing"  style='width:100%; height:150px; overflow:auto;' runat="server">
                <asp:GridView id="GridView3"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="ServiceId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True" 
                 SelectedRowStyle-BackColor="GreenYellow"  PageSize="100"
                            onrowcommand="GridView3_RowCommand"  >
                    <Columns>
                      <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                  
                        <asp:TemplateField HeaderText="Requisition No" >
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             
                        <asp:TemplateField HeaderText="Registration No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Category Type">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Service">
                            <ItemTemplate>
                                <asp:Label ID="lblservice" runat="server" Text='<%# Bind("ServiceTemplateName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Issue Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server" Text='<%# Bind("IssueDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                </asp:GridView>
            </div>
 
 </asp:View>

      <asp:View ID="View2" runat="server">    
    <div class="listing"  style='width:100%; height:150px; overflow:auto;'>
                <asp:GridView id="GridView1"  Width="100%" CssClass="grid" PagerStyle-CssClass="pgr"
                 DataKeyNames ="RowId" runat="server" 
                 AutoGenerateColumns="False" AllowPaging="True"  PageSize="100"
                 SelectedRowStyle-BackColor="GreenYellow" 
                    onpageindexchanging="GridView1_PageIndexChanging" onrowdatabound="GridView1_RowDataBound"
                    onrowdeleting="GridView1_RowDeleting" 
                    onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" onrowcancelingedit="GridView1_RowCancelingEdit" >
                    <Columns>
                            <%--  <asp:CommandField SelectText="Select" ShowSelectButton="True" />--%>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("RowId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
             <%--
                        <asp:TemplateField HeaderText="Registration No">
                            <ItemTemplate>
                                <asp:Label ID="lblRegno" runat="server" Text='<%# Bind("PatientReg") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Patient's Name">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%# Bind("patient_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operaton Name">
                            <ItemTemplate>
                                <asp:Label ID="lblotname" runat="server" Text='<%# Bind("OperationName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                     

                         <asp:TemplateField HeaderText="Admission Date">
                            <ItemTemplate>
                                <asp:Label ID="lbladate" runat="server" Text='<%# Bind("adate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bed No">
                            <ItemTemplate>
                                <asp:Label ID="lblbedno" runat="server" Text='<%# Bind("BedNoText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Entry Date" >
                            <ItemTemplate>
                                <asp:Label ID="lblendate" runat="server" Text='<%# Bind("endate") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                    <asp:TextBox ID="txtgDate"  Width="80px"  CssClass="NumberInput"   runat="server" Text='<%# Bind("endate") %>'></asp:TextBox>
                             </EditItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="Unit" >
                            <ItemTemplate>
                                <asp:Label ID="lblunit" runat="server" Text='<%# Bind("Unit") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                    <asp:TextBox ID="txtgUnit"  Width="80px"  CssClass="NumberInput"   runat="server" Text='<%# Bind("Unit") %>'></asp:TextBox>
                             </EditItemTemplate>
                        </asp:TemplateField>   

                             <asp:TemplateField HeaderText="Usage Cost" >
                            <ItemTemplate>
                                <asp:Label ID="lblusagecost" runat="server" Text='<%# Bind("UsageCost") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                    <asp:TextBox ID="txtusagecost"  Width="80px"  CssClass="NumberInput"   runat="server" Text='<%# Bind("UsageCost") %>'></asp:TextBox>
                             </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Continue" >
                            <ItemTemplate>
                                <asp:Label ID="lblcont" runat="server" Visible="false" Text='<%# Bind("cont") %>'></asp:Label>
                                <asp:CheckBox ID="continue"  runat="server" Enabled="false"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="cont" Width="50px" runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Remarks" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblremarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                    <asp:TextBox ID="txtremarks"  Width="80px"  CssClass="NumberInput"   runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                             </EditItemTemplate>
                        </asp:TemplateField>
                            
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                    </Columns>
                  <PagerStyle CssClass="pgr" />
                    <EditRowStyle BackColor="#CCFF33" />
                    <AlternatingRowStyle BackColor="#FFDB91" />
                    <SelectedRowStyle BackColor="GreenYellow" />
                </asp:GridView>
            </div>

            </asp:View>
            </asp:MultiView>
 
          </div>  
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

