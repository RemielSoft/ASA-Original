<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="GenerateBill.aspx.cs" Inherits="StallionVisa._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        table
        {
            max-width: 100%;
            background-color: transparent;
            border-collapse: collapse;
            border-spacing: 0;
        }
        .table
        {
            width: 100%;
            margin-bottom: 0px;
        }
        .table .bold
        {
            font-weight: bold;
        }
        .table th, .table td
        {
            padding: 8px;
            line-height: 18px;
            text-align: left;
            vertical-align: top;
            border-top: 1px solid #dddddd;
        }
        .table th
        {
            font-weight: bold;
        }
        .table thead th
        {
            vertical-align: bottom;
        }
        .table caption + thead tr:first-child th, .table caption + thead tr:first-child td, .table colgroup + thead tr:first-child th, .table colgroup + thead tr:first-child td, .table thead:first-child tr:first-child th, .table thead:first-child tr:first-child td
        {
            border-top: 0;
        }
        .table tbody + tbody
        {
            border-top: 2px solid #dddddd;
        }
        .table-condensed th, .table-condensed td
        {
            padding: 4px 5px;
        }
        
        .table-bordered
        {
            border: 1px solid #dddddd;
            border-collapse: separate;
            border-collapse: collapsed;
            border-left: 0;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
        }
        .table-bordered th, .table-bordered td
        {
            border-left: 1px solid #dddddd;
        }
        .table-bordered caption + thead tr:first-child th, .table-bordered caption + tbody tr:first-child th, .table-bordered caption + tbody tr:first-child td, .table-bordered colgroup + thead tr:first-child th, .table-bordered colgroup + tbody tr:first-child th, .table-bordered colgroup + tbody tr:first-child td, .table-bordered thead:first-child tr:first-child th, .table-bordered tbody:first-child tr:first-child th, .table-bordered tbody:first-child tr:first-child td
        {
            border-top: 0;
        }
        .table-bordered thead:first-child tr:first-child th:first-child, .table-bordered tbody:first-child tr:first-child td:first-child
        {
            -webkit-border-top-left-radius: 4px;
            border-top-left-radius: 4px;
            -moz-border-radius-topleft: 4px;
        }
        .table-bordered thead:first-child tr:first-child th:last-child, .table-bordered tbody:first-child tr:first-child td:last-child
        {
            -webkit-border-top-right-radius: 4px;
            border-top-right-radius: 4px;
            -moz-border-radius-topright: 4px;
        }
        .table-bordered thead:last-child tr:last-child th:first-child, .table-bordered tbody:last-child tr:last-child td:first-child
        {
            -webkit-border-radius: 0 0 0 4px;
            -moz-border-radius: 0 0 0 4px;
            border-radius: 0 0 0 4px;
            -webkit-border-bottom-left-radius: 4px;
            border-bottom-left-radius: 4px;
            -moz-border-radius-bottomleft: 4px;
        }
        .table-bordered thead:last-child tr:last-child th:last-child, .table-bordered tbody:last-child tr:last-child td:last-child
        {
            -webkit-border-bottom-right-radius: 4px;
            border-bottom-right-radius: 4px;
            -moz-border-radius-bottomright: 4px;
        }
        .table-striped tbody tr:nth-child(odd) td, .table-striped tbody tr:nth-child(odd) th
        {
            background-color: #f9f9f9;
        }
        
        
        
       
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
            width: 50px;
        }
        .style1
        {
            width: 32%;
        }
        .style4
        {
            width: 17%;
        }
    </style>
    <script type="text/javascript">
 function fnValidrate() {
            if (document.getElementById("<%=txtRate.ClientID%>").value != "") {
                var dtText = document.getElementById("<%=txtRate.ClientID%>").value;
                if (isValidrte(dtText)) {

                } else {
                    alert("Only Numeric Value is Allowed");
                    document.getElementById("<%=txtRate.ClientID %>").value = "";
                    document.getElementById("<%=txtRate.ClientID%>").focus();
                    return false;
                }
            }
        }


        function isValidrte(sText) {
            var rerate = /(^\d{0,7}(\.\d{1,2})?$)/;
            return rerate.test(sText);
        }
        </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--<div align="left">
        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/LOGO.bmp" />
    </div>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="add"
                ShowMessageBox="true" ShowSummary="false" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="save"
                ShowMessageBox="true" ShowSummary="false" />
            <div>
                <table class="table table-bordered table-striped table-condensed">
                    <thead>
                        <tr>
                            <th colspan="2">
                                BILL TO :
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblCpnName" runat="server" Text="Company Name"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCmpnName" runat="server" Width="350px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCmpnName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCpnName" runat="server" ControlToValidate="ddlCmpnName"
                                    ErrorMessage="Company Name is Required " ForeColor="Red" ValidationGroup="add">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" Height="73px" TextMode="MultiLine" Width="350px"
                                    Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                    ErrorMessage="Address is Required" ForeColor="Red" ValidationGroup="add">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCorporate" runat="server" Text="Corporate" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCorporate" runat="server" CssClass="textbox1" Visible="false"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvCorporate" runat="server" ControlToValidate="txtCorporate"
                                         ErrorMessage="Corporate is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text="Name" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" CssClass="textbox1" Visible="false"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="Name is Required" ForeColor="Red" >*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tbody>
                </table>
            </div>
            <br />
            <asp:UpdatePanel ID="updPanelItem" runat="server">
                <ContentTemplate>
                    <div>
                        <table class="table table-bordered table-striped table-condensed">
                            <thead>
                                <tr>
                                    <th colspan="4">
                                        Item Entries :
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        Item
                                    </td>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="rbtnitem" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnitem_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Electrical Stampings" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Die Casted Rotor" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Waste" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Other" Value="6"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvRadiobutton" Display="None" runat="server" ForeColor="Red"
                                            ErrorMessage="Please Select Item" ControlToValidate="rbtnitem" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="tdsubitem" runat="server">
                                        Sub_Item
                                    </td>
                                    <td id="tdsubitemradio" runat="server" colspan="3">
                                        <asp:RadioButtonList ID="rbtnsubitem" runat="server" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rbtnsubitem_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Stator Stamping" Value="4"></asp:ListItem>
                                            <asp:ListItem Text=" Rotor Stamping" Value="5"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr valign="middle">
                                    <td id="ddlHide" runat="server">
                                        Description :
                                        <asp:DropDownList ID="ddlitem" runat="server" Width="180px" OnSelectedIndexChanged="ddlitem_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox ID="txtItemDesc" runat="server" CssClass="textbox1" TabIndex="4"></asp:TextBox>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlitem"
                                            ErrorMessage="Item Description is Required" InitialValue="0" ForeColor="Red"
                                            ValidationGroup="add">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="style1" id="otherText" runat="server">
                                        Description :
                                        <asp:TextBox ID="txtOther" runat="server"></asp:TextBox>
                                    </td>
                                    <td id="tdpieces" runat="server" visible="false" width="20%" colspan="3">
                                        Pieces
                                        <asp:TextBox ID="txtpieces" runat="server" CssClass="textbox" ValidationGroup="add"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtpieces"
                                            ErrorMessage="Only Numeric Value is Allowed" ValidationGroup="add" ForeColor="Red"
                                            ValidationExpression="^\d{0,7}(\.\d{1,2})?$">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr valign="middle">
                                    <td width="15%">
                                        Box
                                        <asp:TextBox ID="txtbox" runat="server" CssClass="textbox" ValidationGroup="add"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbox"
                                            ErrorMessage="Only Numeric Value is Allowed" ValidationGroup="add" ForeColor="Red"
                                            ValidationExpression="^\d{0,7}(\.\d{1,2})?$">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="lblquantity" runat="server" Text="Piece" Visible="false"></asp:Label>
                                        <asp:Label ID="lblkg" runat="server" Text="Kg"></asp:Label>
                                        <%--<asp:Label ID="lblPK" runat="server" Text="Pieces/Kgs :"></asp:Label>--%>
                                       : <asp:TextBox ID="txtQuantity" runat="server" AutoPostBack="True" CssClass="textbox"
                                            MaxLength="10" OnTextChanged="txtQuantity_TextChanged" ValidationGroup="add"></asp:TextBox>
                                            <asp:CheckBox ID ="chkkg" runat="server" Visible="false" 
                                            oncheckedchanged="chkkg_CheckedChanged" AutoPostBack="true"/>
                                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity"
                                            ErrorMessage="Quantity is Required" ForeColor="Red" ValidationGroup="add">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revQuantity" runat="server" ControlToValidate="txtQuantity"
                                            ErrorMessage="Only Numeric Value is Allowed" ValidationGroup="add" ForeColor="Red"
                                            ValidationExpression="^\d{0,7}(\.\d{1,2})?$">*
                                        </asp:RegularExpressionValidator>
                                    </td>
                                    <td class="style4">
                                        Rate :
                                        <asp:TextBox ID="txtRate" runat="server" AutoPostBack="True" CssClass="textbox" MaxLength="7"
                                            OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRate" runat="server" ControlToValidate="txtRate"
                                            ErrorMessage="Rate Per Unit is Required" ForeColor="Red" ValidationGroup="add">*</asp:RequiredFieldValidator>
                                         <%--<asp:RegularExpressionValidator ID="revRate" runat="server" ControlToValidate="txtRate"
                                            ErrorMessage="Only Numeric Value is Allowed" ValidationGroup="add" ForeColor="Red"
                                            ValidationExpression="^\d{0,7}(\.\d{1,2})?$">*
                                        </asp:RegularExpressionValidator>--%>
                                    </td>
                                    <td width="20%">
                                        Amount :
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                    <td>
                                        <div style="float: right;">
                                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" ValidationGroup="add"
                                                Width="60" />
                                            <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click1"
                                                />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br />
                    <div align="center">
                        <asp:GridView ID="gvItemDesc" runat="server" AutoGenerateColumns="False" Width="80%"
                            CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                            BorderStyle="None" BorderWidth="1px" AllowPaging="True" OnPageIndexChanging="gvItemDesc_PageIndexChanging">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:TemplateField HeaderText="SELECT">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRemove" runat="server" />
                                        <%--<asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("Id") %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField ControlStyle-Width="200" DataField="ItemDescription" HeaderText="ITEM DESCRIPTION">
                                    <ControlStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField ControlStyle-Width="10" DataField="quantity" HeaderText="QUANTITY">
                                    <ControlStyle Width="10px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="measrment" HeaderText="MEASUREMENT" />
                                <asp:BoundField DataField="rate" HeaderText="CHARGES/UNIT" />
                                <asp:BoundField DataField="amount" HeaderText="AMOUNT(INR)" />
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
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <table class="table table-bordered table-striped table-condensed">
                    <tbody>
                        <tr>
                            <td width="38%">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cenvat (%)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                <asp:TextBox ID="txtserTax" runat="server" CssClass="textbox" ValidationGroup="save"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;E.CESS (%)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                :
                                <asp:TextBox ID="txtecess" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td width="24%">
                                Total :
                                <asp:TextBox ID="txtTotal" runat="server" Enabled="false" Width="100"></asp:TextBox>
                                [INR]
                                <asp:RequiredFieldValidator ID="rfvTotal" runat="server" ControlToValidate="txtTotal"
                                    ErrorMessage="Total is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;S.H.E.CESS (%)&nbsp;&nbsp;&nbsp; &nbsp;
                                :
                                <asp:TextBox ID="txtshecess" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total With Tax&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                :
                                <asp:TextBox ID="txttotaltax" runat="server" Enabled="false" Width="100"></asp:TextBox>
                                [INR]
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttotaltax"
                                    ErrorMessage="TotalWithTax is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <%-- <td width="38%">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CST (%)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : <asp:TextBox ID="txtcst" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>                            
                            --%>
                            <td width="38%">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblvat" runat="server" Text="VAT (%) " Visible="false"></asp:Label>
                                <asp:Label ID="lblcst" runat="server" Text="CST (%) "></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                :<asp:TextBox ID="txtvat" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Freight (INR)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                <asp:TextBox ID="txtFreight" runat="server" CssClass="textbox" OnTextChanged="txtFreight_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdtcs" runat="server" colspan="3">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TCS (%)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                <asp:TextBox ID="txtTCS" runat="server" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Grand Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                
                                : <asp:TextBox ID="txtGTotal" runat="server" Enabled="false"></asp:TextBox> [INR]
                               
                            </td>
                            <td colspan="2">
                                &nbsp;&nbsp;Vehicle No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; :
                                <asp:TextBox ID="txtVehicleNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                            <td>
                                <div style="float: right;">
                                    <asp:Button ID="btnPrint" runat="server" ValidationGroup="save" OnClick="btnPrint_Click"
                                        Text="Print" Width="60" Visible="false" />
                                    <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" ValidationGroup="save"
                                        Width="60" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60" OnClick="btnReset_Click"
                                        OnClientClick="btnReset_Click" />
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
