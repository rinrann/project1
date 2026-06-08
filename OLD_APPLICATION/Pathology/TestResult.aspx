<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="TestResult.aspx.cs" Inherits="Pathology_TestResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    function ShowDialog() {
        var rtvalue = window.open("TestResultRegPopUp.aspx?TestID=P", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_txtregno").value = rtvalue.NameValue;
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBox50").value = rtvalue.ProfessionValue;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >


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
         <asp:Label ID="Label1" runat="server">Pathology Test Result</asp:Label>
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
                <asp:TextBox ID="TextBox50" runat="server" CssClass="textbox-medium1" 
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
                <label><strong>Specimen Name :</strong></label>
                <asp:DropDownList ID="ddlSpeciman" runat="server" CssClass="textbox-medium1" >
                </asp:DropDownList>
                <div class="clear"></div>
            </div>

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
                         <asp:gridview ID="Gridview1" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%"  onrowdatabound="Gridview1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID"  runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="ResultID"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                     
                    </Columns>
                   </asp:gridview>

            <asp:Panel ID="Panel1" runat="server">
                     <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                <asp:gridview ID="Gridview2" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview2_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="ResultID"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                    
                        </asp:TemplateField>
                    
                    </Columns>
          
                </asp:gridview>
            <asp:Panel ID="Panel2" runat="server">
             <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

               <asp:gridview ID="Gridview3" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview3_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="ResultID"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                                  </asp:TemplateField>
                       </Columns>
                 </asp:gridview>
            <asp:Panel ID="Panel3" runat="server">
            <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>

            </asp:Panel>

                <asp:gridview ID="Gridview4" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview4_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>' ></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                      
                        </asp:TemplateField>
                            </Columns>
                 </asp:gridview>
            <asp:Panel ID="Panel4" runat="server">
                 <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                <asp:gridview ID="Gridview5" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview5_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server"  Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                                </asp:TemplateField>
                          </Columns>
                   </asp:gridview>
            <asp:Panel ID="Panel5" runat="server">
                 <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                <asp:gridview ID="Gridview6" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview6_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>' ></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                            </asp:TemplateField>
                         </Columns>
                  </asp:gridview>
            <asp:Panel ID="Panel6" runat="server">
                  <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                <asp:gridview ID="Gridview7" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview7_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>' ></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                          </asp:TemplateField>
                           </Columns>
                   </asp:gridview>
            <asp:Panel ID="Panel7" runat="server">
                <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>

            </asp:Panel>

                <asp:gridview ID="Gridview8" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview8_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                              <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                          </asp:TemplateField>
                            </Columns>
                </asp:gridview>
            <asp:Panel ID="Panel8" runat="server">
             <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>
            </asp:Panel>

                <asp:gridview ID="Gridview9" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview9_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                               <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                             </asp:TemplateField>
                               </Columns>
                         </asp:gridview>
            <asp:Panel ID="Panel9" runat="server">
              <div class="form-sec-row" >
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>

            </asp:Panel>

                <asp:gridview ID="Gridview10" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview10_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server"  Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                              </asp:TemplateField>
                        </Columns>
                    </asp:gridview>
            <asp:Panel ID="Panel10" runat="server">
                   <div class="form-sec-row">
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox10" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>

            </asp:Panel>

                <asp:gridview ID="Gridview11" runat="server" CssClass="grid" 
                    PagerStyle-CssClass="pgr" AutoGenerateColumns="false"  Width="100%" 
                onrowdatabound="Gridview11_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Test Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parameter Name"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("ParameterName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="ResultID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblResultID" runat="server" Text='<%# Eval("ResultId")%>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValue" runat="server"  Text='<%# Eval("ResultValue")%>'></asp:TextBox>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Normal Range"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                 <asp:Label ID="lblRange" runat="server" Text='<%# Eval("NormalRange")%>'></asp:Label>                                 
                            </ItemTemplate>
                              </asp:TemplateField>
                        </Columns>
                    </asp:gridview>
            <asp:Panel ID="Panel11" runat="server">
                   <div class="form-sec-row">
                <label><strong>Remark / Impression :</strong></label>
                            <asp:TextBox ID="TextBox11" runat="server" TextMode="MultiLine" CssClass="textbox-mediumRemark"></asp:TextBox>
                    <div class="clear"></div>
            </div>

            </asp:Panel>

            <div class="form-sec-row">
                 <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" Text="Submit"  Height="28px"  CssClass="submit-button" onclick="Button1_Click"  OnClientClick="return Validate();" 
                 />
                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="submit-button"  Height="28px"  />
                <div class="clear"></div>
            </div>                     

            <div class="clear"></div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

