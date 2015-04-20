<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompanyMaster.aspx.cs" Inherits="StallionVisa.CompanyMaster" %>
    <%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
        #table-3 td, #table-3 th
        {
            border-top-color: white;
            border-bottom: 1px solid #DFDFDF;
            color: #555;
            font-weight: bold;
        }
        .ButtonAlign
        {
            position: relative;
            float: right;
            padding-bottom: 20px;
            margin-right: 10px;
        }
        #table-3 th
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
        #table-3 td
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
        .griditemtext
        {
            text-align: left;
        }
        .style1
        {
            height: 28px;
        }
        .style2
        {
            width: 71%;
        }
        .style3
        {
            height: 28px;
            width: 34%;
        }
        .style4
        {
            width: 34%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="upnlCompany" runat="server">
        <ContentTemplate>--%>
            <asp:ValidationSummary ID="Validationsummery1" ShowMessageBox="true" ShowSummary="false"
                runat="server" ValidationGroup="summary" />
            <div>
                <table id="table-3">
                    <tbody>
                        
                        <tr>
                            <td width="20%" class="style1">
                                <asp:Label ID="lblCpnName" runat="server" Text="Company Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCpnName" runat="server" CssClass="textbox1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCpnName" runat="server" ControlToValidate="txtCpnName"
                                    ErrorMessage="Company Name is Required" ForeColor="Red" ValidationGroup="summary">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblContact" runat="server" Text="Contact No"></asp:Label>
                            </td>
                            <td class="center">
                                <asp:TextBox ID="txtContact" runat="server" CssClass="TextBox" MaxLength="30"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContact"
                                    ErrorMessage="Contact is Required" ForeColor="Red" ValidationGroup="summary">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            </td>
                            <td class="style4" colspan="3">
                                <asp:TextBox ID="txtCmpnAddress" runat="server" Height="50px" TextMode="MultiLine"
                                    Width="500px" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtCmpnAddress"
                                    ErrorMessage="Address is Required" ForeColor="Red" ValidationGroup="summary">*</asp:RequiredFieldValidator>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>Applicable text type</td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rbtnvat" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnvat_SelectedIndexChanged"
                                    Width="114px">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvRadiobutton" Display="None" runat="server" ForeColor="Red"
                                    ErrorMessage="Please Select VAT or CST" ControlToValidate="rbtnvat" ValidationGroup="summary"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ECC
                            </td>
                            <td class="style4">
                                <asp:TextBox ID="txtEcc" runat="server" CssClass="TextBox" MaxLength="30" ></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator ID="rfvEcc" runat="server" ControlToValidate="txtEcc"
                                    ErrorMessage="ECC is Required" ForeColor="Red" ValidationGroup="summary">*</asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="center">
                                TIN
                            </td>
                            <td>
                                <asp:TextBox ID="txtTan" runat="server" MaxLength="30" CssClass="TextBox" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTan" runat="server" ControlToValidate="txtTan"
                                    ErrorMessage="TIN is Required" ForeColor="Red" ValidationGroup="summary">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtan" runat="server" ControlToValidate="txtTan"
                                    ValidationGroup="summary" Display="None" ValidationExpression="\d+" ErrorMessage="Please Enter the Numeric value"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
            </div>
            <br />
            <div class="ButtonAlign">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="summary" Width="60"
                     OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update"  Visible="false"
                    OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  OnClick="btnCancel_Click" />
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <br />
    <div align="center">
        <asp:GridView ID="grdCompany" runat="server" AutoGenerateColumns="False" CellPadding="3"
            Width="90%" GridLines="Vertical" OnRowCommand="grdCompany_RowCommand" BackColor="White"
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" AllowPaging="True"
            OnPageIndexChanging="grdCompany_PageIndexChanging" PageSize="20" OnRowEditing="grdCompany_RowEditing"
            OnRowDeleting="grdCompany_RowDeleting">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="Copmany Name" ItemStyle-CssClass="griditemtext">
                    <ItemTemplate>
                        <asp:Label ID="lblCmpnName" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Copmany Address" ItemStyle-CssClass="griditemtext">
                    <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Charges" Visible="True" ItemStyle-CssClass="griditemtext">
                    <ItemTemplate>
                        <asp:Label ID="lblCharges_Name" runat="server" Text='<%#Eval("ChargesName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="griditemtext">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnEdit" CommandName="Edit" CommandArgument='<%#Eval("CompanyId") %>'
                            runat="server" Text="Edit"></asp:LinkButton>&nbsp;|
                        <asp:LinkButton ID="lnkbtnDelete" OnClientClick='return confirm("Are You Sure To Delete")'
                            CommandName="Delete" CommandArgument='<%#Eval("CompanyId") %>' runat="server"
                            Text="Delete"></asp:LinkButton>
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
</asp:Content>
