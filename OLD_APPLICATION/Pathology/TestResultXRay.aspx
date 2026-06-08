<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestResultXRay.aspx.cs" Inherits="Pathology_TestResultXRay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function ShowDialog() {
        var rtvalue = window.open("TestResultRegPopUp.aspx?TestID=X", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtregno").value = rtvalue.NameValue;
        //document.getElementById("vv").value = rtvalue.ProfessionValue;
    }

    function Calling() {
        var date = new Date();
        $("input[id$='txtdate']").datepicker({ dateFormat: 'dd/mm/yy' });

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


    function Validate() {

        var f = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
        if (f.options[f.selectedIndex].value == 0) {
            alert("Select Pathologist ");
            document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1").focus();
            return false;
        }



        var g = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList2");
        if (g.options[g.selectedIndex].value == 0) {
            alert("Select Checked By.");
            document.getElementById("ctl00_ContentPlaceHolder1_DropDownList2").focus();
            return false;
        }

        var s = document.getElementById("ctl00_ContentPlaceHolder1_ddlSpeciman");
        if (s.options[s.selectedIndex].value == 0) {
            alert("Please select Specimen.");
            document.getElementById("ctl00_ContentPlaceHolder1_ddlSpeciman").focus();
            return false;
        }

    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
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
         <asp:Label ID="Label1" runat="server">X-RAY Test Result</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="txtPId" runat="server" />
            
            <div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
                <asp:TextBox ID="txtregno" runat="server" CssClass="textbox-medium1" 
                    Enabled="False"  ></asp:TextBox>
                <asp:Button ID="Button3" runat="server"  Height="28px"
                    OnClientClick="ShowDialog();" Text="Quick Search" CssClass="submit-search" 
                   /><asp:Button ID="Button4" runat="server" Text="Fetch"   Height="28px"
                    CssClass="submit-button" onclick="Button4_Click1"/>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Requisition No :</strong></label>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                <div class="clear"></div>
            </div>  
            <div class="form-sec-row">
                <label><strong>Patient's Name :</strong></label>
                <asp:TextBox ID="txtname" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                <div class="clear"></div>
            </div>            
            <div class="form-sec-row">
                <label><strong>Address :</strong></label>
                <asp:TextBox ID="txtvillage" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" ></asp:TextBox>
                <div class="clear"></div>
            </div>

            <div class="form-sec-row">
                <label><strong>Date :</strong></label>
                <asp:TextBox ID="txtdate" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                <div class="clear"></div>
            </div>

                  
            <div class="form-sec-row">
                <label><strong>Test Code :</strong></label>
                  <asp:TextBox ID="txtcode" runat="server" CssClass="textbox-medium1" 
                    Enabled="False" AutoPostBack="True" ></asp:TextBox>
                <div class="clear"></div>
            </div>
            <div class="form-sec-row">
                <label><strong>Test Name :</strong></label>
                <asp:TextBox ID="txttestname" runat="server" CssClass="textbox-medium1" Enabled="False" 
                    ></asp:TextBox>    <div class="clear"></div></div>
   
                 <div class="form-sec-row">
                <label><strong>Consultant Pathologist :</strong></label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>

                 <div class="form-sec-row">
                <label><strong>Checked By :</strong></label>
                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                  <div class="form-sec-row">
                <label><strong>Test Name :</strong></label>
                      <asp:TextBox ID="TextBox2" runat="server" CssClass ="textbox-medium1" 
                          Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row">
                <label><strong>Template Name :</strong></label>
                <asp:DropDownList ID="DropDownList11" runat="server" CssClass="textbox-medium1" 
                        onselectedindexchanged="DropDownList11_SelectedIndexChanged" AutoPostBack="True" 
                      >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Result :</strong></label>
                <asp:TextBox ID="TextBox11" runat="server" CssClass="textbox-mediummul" TextMode="MultiLine" 
                    ></asp:TextBox>    <div class="clear"></div></div>
                         <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox21" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                     <asp:Panel ID="Panel2" runat="server">
                          <div class="form-sec-row">
                <label><strong>Test Name :</strong></label>
                      <asp:TextBox ID="TextBox3" runat="server" CssClass ="textbox-medium1" Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row">
                <label><strong>Template Name :</strong></label>
                <asp:DropDownList ID="DropDownList12" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList12_SelectedIndexChanged" 
                      >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Result :</strong></label>
                <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-mediummul" TextMode="MultiLine" 
                    ></asp:TextBox>    <div class="clear"></div></div>
                         <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox22" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                     <asp:Panel ID="Panel3" runat="server">
                          <div class="form-sec-row">
                <label><strong>Test Name :</strong></label>
                      <asp:TextBox ID="TextBox4" runat="server" CssClass ="textbox-medium1" Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row">
                <label><strong>Template Name :</strong></label>
                <asp:DropDownList ID="DropDownList13" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList13_SelectedIndexChanged" 
                      >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Result :</strong></label>
                <asp:TextBox ID="TextBox13" runat="server" CssClass="textbox-mediummul" TextMode="MultiLine" 
                    ></asp:TextBox>    <div class="clear"></div></div>
                         <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox23" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                     <asp:Panel ID="Panel4" runat="server">
                          <div class="form-sec-row">
                <label><strong>Test Name :</strong></label>
                      <asp:TextBox ID="TextBox5" runat="server" CssClass ="textbox-medium1" Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row">
                <label><strong>Template Name :</strong></label>
                <asp:DropDownList ID="DropDownList14" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList14_SelectedIndexChanged" 
                      >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Result :</strong></label>
                <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-mediummul" TextMode="MultiLine" 
                    ></asp:TextBox>    <div class="clear"></div></div>
                         <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox24" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                     <asp:Panel ID="Panel5" runat="server">
                          <div class="form-sec-row">
                <label><strong>Test Name :</strong></label>
                      <asp:TextBox ID="TextBox6" runat="server" CssClass ="textbox-medium1" Enabled="False"></asp:TextBox>
                <div class="clear"></div>
            </div>
                <div class="form-sec-row">
                <label><strong>Template Name :</strong></label>
                <asp:DropDownList ID="DropDownList15" runat="server" CssClass="textbox-medium1" 
                        AutoPostBack="True" onselectedindexchanged="DropDownList15_SelectedIndexChanged" 
                      >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>
              <div class="form-sec-row">
                <label><strong>Result :</strong></label>
                <asp:TextBox ID="TextBox15" runat="server" CssClass="textbox-mediummul" TextMode="MultiLine" 
                    ></asp:TextBox>    <div class="clear"></div></div>
                         <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox25" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

            <%--<div class="listing">
         <asp:GridView id="GridView1"  CssClass="grid" PagerStyle-CssClass="pgr" 
          runat="server" AutoGenerateColumns="False" 
             AllowPaging="True" PageSize ="7" Width="979px" >
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Purchase ID" >
                        <ItemTemplate>
                           <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1">
                </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
 
                      <asp:TemplateField HeaderText="Template" >
                        <ItemTemplate>                        
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-mediummul"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                      </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>--%>
            
                 
            
            <div class="form-sec-row">
                 <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server"  Height="28px"  Text="Submit" CssClass="submit-button" onclick="Button1_Click"  OnClientClick="return Validate();" 
                 />
                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="submit-button"    Height="28px"       />
                <div class="clear"></div>
            </div>                     

            <div class="clear"></div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

