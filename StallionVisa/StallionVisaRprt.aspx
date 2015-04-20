<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StallionVisaRprt.aspx.cs" MasterPageFile="~/Site.Master" Inherits="StallionVisa.StallionVisaRprt" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ToolkitScriptManager runat="server">
    </asp:ToolkitScriptManager>
   

     <div>
  
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
              AutoDataBind="true"
            ReportSourceID="CrystalReportSource1" Height="1202px" 
            HyperlinkTarget="_blank" ToolbarStyle-BorderStyle="Solid" ToolPanelView="None" Width="1104px" 
            
            />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="StallionVisaRpt.rpt">
            </Report>
        </CR:CrystalReportSource>
    
    </div>
    </asp:Content>
<%--<div>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
    </form>
</body>
</html>--%>
