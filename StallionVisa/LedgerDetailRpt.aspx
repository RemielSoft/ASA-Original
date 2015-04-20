<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LedgerDetailRpt.aspx.cs" Inherits="StallionVisa.LedgerDetailRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="True" 
        ReportSourceID="CrystalReportSource1" Height="1202px" 
            HyperlinkTarget="_blank" ToolbarStyle-BorderStyle="Solid" ToolPanelView="None" Width="1104px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="LedgerRpt.rpt">
        </Report>
    </CR:CrystalReportSource>
</div>
</asp:Content>
