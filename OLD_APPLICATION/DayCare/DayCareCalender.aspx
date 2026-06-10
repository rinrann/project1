<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="DayCareCalender.aspx.cs" Inherits="DayCare_DayCareCalender" %>
<% @Register Namespace="DataControls" Assembly="DataCalendar" TagPrefix="dc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="pageheader">
     <asp:Label ID="Label1" runat="server">DayCare Calender </asp:Label>
</div>
<div class="formbox">
    <div class="form-sec">
     <dc:DataCalendar id="cal1" runat="server" width="100%"                            
                             Font-Name="Tahoma">

                <TitleStyle BackColor="#FAAC58"
                               ForeColor="white"
                               Font-Bold="True"/>

                <NextPrevStyle ForeColor="white"
                               Font-Size="18px"
                               Font-Bold="True"/>                               
                             
                <DayStyle HorizontalAlign="Left" VerticalAlign="Top"
                          Font-Size="8" font-Name="Arial"/>
                        
                <DayWithEventsStyle BackColor="honeyDew" Font-Bold="True" />

                <WeekendDayStyle BackColor="lightYellow" />
                
                          
                <OtherMonthDayStyle BackColor="LightGray" ForeColor="DarkGray"  />

                <ItemTemplate>
                
                    <p style="margin-top: 0px; margin-bottom: 6px; font-size: 10pt; font-weight: bold;">
                         
                           <a href="AppoinmentList.aspx?Date=<%#Container.DataItem["MeetingDate"] %>&shift=<%# Container.DataItem["ShiftID"] %>">
                        <%# Container.DataItem["Title"] %>      
                         ( <%# Container.DataItem["NoOfPatient"]%> )
                         </a>
                    </p>
                </ItemTemplate>
                
                <NoEventsTemplate>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </NoEventsTemplate>
                
            </dc:DataCalendar>
    </div>
    
</div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

