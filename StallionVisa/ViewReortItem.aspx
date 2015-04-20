<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ViewReortItem.aspx.cs" Inherits="StallionVisa.ViewReortItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/layout.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <style type="text/css">
        #table-3
        {
            border: 1px solid #5D7B9D;
            background-color: #F9F9F9;
            width: 100%;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            -border-radius: 3px;
            font-family: Arial, "Bitstream Vera Sans" ,Helvetica,Verdana,sans-serif;
            color: #333;
        }
        #table-3 .td, #table-3 .th
        {
            border-top-color: white;
            border-bottom: 1px solid #DFDFDF;
            color: #555;
            font-weight: bold;
        }
        #table-3 .th
        {
            font-family: Arial, "Bitstream Vera Sans" ,Helvetica,Verdana,sans-serif;
            font-weight: normal;
            padding: 7px 7px 8px;
            text-align: left;
            line-height: 1.3em;
            font-size: 14px;
            font-weight: bold;
            color: #5D7B9D;
        }
        #table-3 .td
        {
            font-size: 12px;
            padding: 4px 7px 2px;
            vertical-align: top;
        }
        input.textbox
        {
            height: 12px;
            width: 50px;
        }
        
        
        .cal_Theme1 .ajax__calendar_container
        {
            background-color: #e2e2e2;
            border: solid 1px #cccccc;
            font-size: 9px;
        }
        
        .cal_Theme1 .ajax__calendar_header
        {
            background-color: #ffffff;
            margin-bottom: 0px;
            font-size: 9px;
        }
        
        .cal_Theme1 .ajax__calendar_title, .cal_Theme1 .ajax__calendar_next, .cal_Theme1 .ajax__calendar_prev
        {
            color: #004080;
            padding-top: 0px;
            font-size: 9px;
        }
        
        .cal_Theme1 .ajax__calendar_body
        {
            background-color: #e9e9e9;
            border: solid 1px #cccccc;
            font-size: 9px;
        }
        
        .cal_Theme1 .ajax__calendar_dayname
        {
            text-align: center;
            margin-bottom: 4px;
            margin-top: 2px;
            font-size: 9px;
        }
        
        .cal_Theme1 .ajax__calendar_day
        {
            text-align: center;
            font-size: 9px;
        }
        
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year, .cal_Theme1 .ajax__calendar_active
        {
            color: #004080;
            font-size: 9px;
            background-color: #ffffff;
        }
        
        .cal_Theme1 .ajax__calendar_today
        {
            font-weight: bold;
            font-size: 9px;
        }
        
        .cal_Theme1 .ajax__calendar_other, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title
        {
            color: #bbbbbb;
            font-size: 9px;
        }
    </style>
    <div>
        <table id="table-3">
            <thead>
                <tr>
                    <th class="th" colspan="2">
                        View Bill Reports:
                    </th>
                    <th class="th" colspan="2">
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td width="20%" class="td">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="date"
                            ShowMessageBox="true" ShowSummary="false" />
                        <asp:Label ID="lblCmpnName" runat="server" Text="Company Name"></asp:Label>
                    </td>
                    <td class="td">
                        <asp:DropDownList ID="ddlCmpnName" runat="server" Width="180px" AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                    <td class="td">
                        <asp:RadioButtonList ID="rbtnitem" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true">
                            <asp:ListItem Text="Electrical Stampings" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Die Casted Rotor" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Waste" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Other" Value="4"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="td">
                        <%-- <asp:CheckBoxlist ID="chklist" runat="server" 
                            ></asp:CheckBoxlist>--%>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="td">
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Text="(MM/dd/yyyy)" Visible="false"></asp:Label>
                    </td>
                    <td class="td">
                        <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvfromDate" runat="server" ErrorMessage="FromDate is Required"
                            Display="None" ForeColor="Red" ControlToValidate="txtFromDate" ValidationGroup="date">*</asp:RequiredFieldValidator>
                        <asp:CalendarExtender ID="calenderFromDate" runat="server" TargetControlID="txtFromDate"
                            CssClass="cal_Theme1" PopupButtonID="BottomLeft" Format="dd-MM-yyyy" OnClientDateSelectionChanged="checkdate">
                        </asp:CalendarExtender>
                    </td>
                    <td class="td" colspan="2">
                        <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                        <asp:Label ID="lblToDat" runat="server" Text="(MM/dd/yyyy)" Visible="false"></asp:Label>
                   
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ErrorMessage="ToDate is Required"
                            Display="None" ForeColor="Red" ControlToValidate="txtToDate" ValidationGroup="date">*</asp:RequiredFieldValidator>
                        <asp:CalendarExtender ID="calenderToDate" runat="server" TargetControlID="txtToDate"
                            CssClass="cal_Theme1" PopupButtonID="BottomLeft" Format="dd-MM-yyyy" OnClientDateSelectionChanged="checkdate">
                        </asp:CalendarExtender>
                        
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Button ID="btnViewBill" runat="server" Text="Go" OnClick="btnViewBill_Click"
                            ValidationGroup="date" />
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="td">
                    </td>
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div align="center">
        <table>
            <tr>
                <asp:GridView ID="gridViewBill" runat="server" AutoGenerateColumns="false" CellPadding="3"
                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None"
                    ShowFooter="true" BorderWidth="1px" Width="90%" AllowPaging="True" OnPageIndexChanging="gridViewBill_PageIndexChanging"
                    PageSize="20" OnRowDataBound="gridViewBill_RowDataBound" OnRowCommand="gridViewBill_RowCommand">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Bill Id">
                            <ItemTemplate>
                                <%-- <asp:Label ID="lblBill_Id" runat="server" Text='<%#Eval("BillId") %>'></asp:Label>--%>
                                <asp:LinkButton ID="lblbillid" runat="server" Text='<%#Eval("BillId") %>' CssClass="gridLink"
                                    CommandName="lnkDetails" CommandArgument='<%#Eval("BillId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Date">
                            <ItemTemplate>
                                <asp:Label ID="lblBill_Date" runat="server" Text='<%#Eval("BillDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCompany_Name" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("billdetail.ItemName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Description">
                            <ItemTemplate>
                                <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("billdetail.ItemDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pieces">
                            <ItemTemplate>
                                <asp:Label ID="lblpe" runat="server" Text='<%#Eval("billdetail.Pieces") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qunatity(Kg)">
                            <ItemTemplate>
                                <asp:Label ID="lblQunatity" runat="server" Text='<%#Eval("QuantityMeasurement") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblrate" runat="server" Text='<%#Eval("billdetail.measrment") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount(INR)">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("billdetail.Amount") %>'></asp:Label>
                            </ItemTemplate>
                              <FooterTemplate>
                                <asp:Label ID="lblTotalQuantiy" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Cenvat(%)" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblService_Tax" runat="server" Text='<%#Eval("ServiceTax") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Service Charge">
                            <ItemTemplate>
                                <asp:Label ID="lblService_Charge" runat="server" Text='<%#Eval("ServiceCharge") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                       <%-- <asp:TemplateField HeaderText="Grand Total" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblGrand_Total" runat="server" Text='<%#Eval("GrandTotal") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                          
                        </asp:TemplateField>--%>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </tr>
        </table>
        <div style="position: relative; float: right; right: 50px; top: 20px;">
            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" /></div>
        <asp:Button ID="btnPopUp" runat="server" BackColor="#f8f8f8" BorderStyle="None" BorderWidth="0px"
            CommandName="Select" />
        <div style="position: absolute; top: 1000px;">
            <asp:ModalPopupExtender ID="Modalpopup" runat="server" TargetControlID="btnPopUp"
                PopupControlID="panelUser" BackgroundCssClass="modalBackground" DropShadow="true"
                Enabled="True" PopupDragHandleControlID="PopupMenu">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panelUser" runat="server" CssClass="div-popup">
                <div class="MainDiv">
                    <div class="intabdiv">
                        <asp:Label ID="lblbill" runat="server" Text="Bill Reports" CssClass="LebelDivD" Font-Bold="True"></asp:Label>
                        <asp:LinkButton ID="lnkpopup" CssClass="popup-close" runat="server">
                            <asp:Image ID="Image4" ImageUrl="~/Images/close.jpg" Width="25px" Height="24px" runat="server" /></asp:LinkButton>
                        <a href="#" class="PopupClose">
                    </div>
                    <div class="PopupDivInner">
                        <div class="SpaceDiv">
                            <%--<asp:Label ID="lblCustomerName" runat="server"></asp:Label>--%>
                        </div>
                        <asp:GridView ID="gvBillreports" runat="server" AutoGenerateColumns="false" CellPadding="3"
                            GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None"
                            ShowFooter="true" BorderWidth="1px" Width="100%" AllowPaging="True" PageSize="20">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:TemplateField HeaderText="Bill Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbill" runat="server" Text='<%#Eval("BillId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbilldate" runat="server" Text='<%#Eval("BillDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                    </ItemTemplate>
                                   <%-- <FooterTemplate>
                                        <asp:Label ID="lblTotalQuantiy" runat="server"></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalamount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cenvat(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblservice" runat="server" Text='<%#Eval("ServiceTax") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="E_CESS(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblecess" runat="server" Text='<%#Eval("E_cess") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S.H.E_CESS(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshecess" runat="server" Text='<%#Eval("SHE_cess") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST/VAT Value(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcstvat" runat="server" Text='<%#Eval("CstVatValue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grand Total(INR)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrand" runat="server" Text='<%#Eval("GrandTotal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#000065" />
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
