<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="AnalysisMaster.aspx.cs" Inherits="Account_AnalysisMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
         <asp:Label ID="Label1" runat="server">Analysis Master</asp:Label>
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
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
     <asp:HiddenField ID="HiddenField1" runat="server" />
			<div class="form-sec-row">
                <label><strong>Analysis Code :</strong></label>
                 <asp:TextBox ID="txtanalysiscode" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>

            <div class="form-sec-row">
                <label><strong>Analysis Name :</strong></label>
                 <asp:TextBox ID="txtanalysisname" runat="server" CssClass="textbox-medium1" 
                   ></asp:TextBox>
                <div class="clear">  </div>
            </div>

            <div class="form-sec-row">
                <label><strong>Analysis Type :</strong></label>
                 <asp:DropDownList ID="dropdownlisttype" runat="server" CssClass="combo-big1" AutoPostBack="false">

                                <%-- <asp:ListItem Text="Bed Charge" Value="bed_charge"></asp:ListItem>
                                <asp:ListItem Text="Medicine" Value="medicine"></asp:ListItem>
                                <asp:ListItem Text="Comsumable" Value="comsumable"></asp:ListItem>
                                <asp:ListItem Text="Services" Value="services"></asp:ListItem>
                                <asp:ListItem Text="Sister/Aya" Value="sis_aya"></asp:ListItem>
                                <asp:ListItem Text="Doctor Visit" Value="doc_visit"></asp:ListItem>
                                <asp:ListItem Text="Ambulance" Value="ambulance"></asp:ListItem>
                                <asp:ListItem Text="Others" Value="others"></asp:ListItem>--%>
                 </asp:DropDownList>
                <div class="clear"></div>
                   </div>
            
            <div class="form-sec-row">
                <label><strong>&nbsp;</strong></label>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" Height="28px" CssClass="submit-button" />
                <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" Height="28px" CssClass="submit-button" />
                <div class="clear"></div>
            </div>  
     </div>


            </asp:View>

          <asp:View ID="View2" runat="server">

           <div style='width:100%;'>
  <table width="100%" style='background-color:#FB7B13; color:#FFF;'>
  <tr style='border:1px solid black;'> 
   <td style='width:60px;' align="center">Anal Code</td> 
      <td style='width:60px;' align="center">Anal Name</td> 
      <td style='width:60px;' align="center">Anal Type</td> 
        <td style='width:50px;' align="center">Edit</td>
          <td style='width:50px;' align="center" id="code1" runat="server">Delete</td>
          </tr>
  </table> 
  </div> 


  <div class="listing"   style='width:100%; height:500px; overflow:auto;'>
         <asp:GridView id="GridView1"  CssClass="grid" OnRowDataBound="GridView1_RowDataBound"
                 PagerStyle-CssClass="pgr"  runat="server"  ShowHeader="false"
                 AutoGenerateColumns="False" AllowPaging="True" PageSize ="100"  Width="100%"
                 OnPageIndexChanging="GridView1_PageIndexChanging"  SelectedRowStyle-BackColor="GreenYellow"
                 OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
                <RowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Anal Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ANALCODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                       <asp:TemplateField HeaderText="Anal code "     ItemStyle-Width="60px">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("ANALCODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Anal Name "     ItemStyle-Width="60px">
                       
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("ANALNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Anal Type "     ItemStyle-Width="60px" Visible="false">
                       
                        <ItemTemplate >
                            <asp:Label ID="lbltype" runat="server" Text='<%# Bind("CFiller02") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

               
                    <asp:CommandField SelectText="Edit" ShowSelectButton="True"    ItemStyle-Width="50px"  ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp"    ItemStyle-Width="50px"  HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                        <ControlStyle CssClass="temp"></ControlStyle>
                    </asp:CommandField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <EditRowStyle BackColor="#CCFF33" />
                <AlternatingRowStyle BackColor="#FFDB91" />
        </asp:GridView> 
        </div>
        </asp:View>
        </asp:MultiView>
        </div>

           
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

