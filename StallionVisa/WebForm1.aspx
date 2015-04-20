<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="StallionVisa.WebForm1" %>


<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
              AutoDataBind="true"
            ReportSourceID="CrystalReportSource1" Height="1202px" 
            HyperlinkTarget="_blank" ToolbarStyle-BorderStyle="Solid" ToolPanelView="None" Width="1104px" 
            
            />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="test.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>
