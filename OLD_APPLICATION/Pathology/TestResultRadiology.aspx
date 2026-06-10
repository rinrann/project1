<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestResultRadiology.aspx.cs" Inherits="Pathology_TestResultRadiology" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function ShowDialog() {
        var rtvalue = window.open("TestResultRegPopUp.aspx?TestID=U", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtregno").value = rtvalue.NameValue;
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox1").value = rtvalue.ProfessionValue;
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
         <asp:Label ID="Label1" runat="server">USG Test Result</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" /><asp:HiddenField ID="HiddenField3" runat="server" />
            <div class="form-sec-row">
                <label><strong>Registration No :</strong></label>
                <asp:TextBox ID="txtregno" runat="server" CssClass="textbox-medium1" 
                    Enabled="False"  ></asp:TextBox>
                <asp:Button ID="Button3" runat="server"  Height="28px"
                    OnClientClick="ShowDialog();" Text="Quick Search" CssClass="submit-search" 
                   />
                <asp:Button ID="Button4" runat="server" Text="Fetch"   Height="28px"
                    CssClass="submit-button" onclick="Button4_Click" />
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
               <asp:gridview ID="Gridview11" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Header Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("HeaderName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Header Content" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtheaderContent" runat="server" TextMode="MultiLine" Text='<%# Eval("hc")%>' Height="100px" Width="300px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>

                               <asp:gridview ID="Gridview12" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Parameter Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("Parameter")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value"  >
                            <ItemTemplate>
                                <asp:TextBox ID="txtval" runat="server"  Text='<%# Eval("Value")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>

    
                    <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>


            <asp:Panel ID="Panel2" runat="server">              
               <asp:gridview ID="Gridview13" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Header Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("HeaderName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Header Content" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtheaderContent" runat="server" TextMode="MultiLine" Text='<%# Eval("HeaderContent")%>' Height="100px" Width="300px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>

                               <asp:gridview ID="Gridview14" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Parameter Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("Parameter")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtval" runat="server"  Text='<%# Eval("Value")%>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>

    
                    <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>




            <asp:Panel ID="Panel3" runat="server">              
               <asp:gridview ID="Gridview15" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Header Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("HeaderName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Header Content" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtheaderContent" runat="server" TextMode="MultiLine" Text='<%# Eval("HeaderContent")%>' Height="100px" Width="300px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>

                               <asp:gridview ID="Gridview16" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" >
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Parameter Name">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("Parameter")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtval" runat="server"  Text='<%# Eval("Value")%>' ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                               </Columns>
            
                </asp:gridview>

    
                    <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>
                 <div class="form-sec-row">
                
                  <label><strong></strong></label>
               
                <asp:Button ID="Button5" runat="server" Text="Submit" CssClass="submit-button"   Height="28px" OnClientClick="return Validate();" 
                         onclick="Button5_Click"/> <asp:Button ID="Button6" runat="server"  Height="28px" Text="Cancel" CssClass="submit-button" /></div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

