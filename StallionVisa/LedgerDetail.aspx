<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LedgerDetail.aspx.cs" Inherits="StallionVisa.LedgerDetail" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
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
        
        .botdiv
        { margin:5px 0px 0px 200px; 
            }
            
      .txtlebel
      { text-align:left; padding-left:0px; width:100%;
          }
          
     .griditemtext{ text-align:left;}
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnlLedger" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save"
                ShowMessageBox="true" ShowSummary="false" />
                <div>
                
                    <asp:Label ID="lblCmpnName" runat="server" Text="Company Name "></asp:Label>
                        <asp:DropDownList ID="ddlCmpnName" runat="server" Width="180px" 
                        onselectedindexchanged="ddlCmpnName_SelectedIndexChanged" AutoPostBack="True" 
                         ></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="rfvDDLCpn" runat="server" ErrorMessage="Please Select Company Name" 
                                ControlToValidate="ddlCmpnName" ValidationGroup="save" InitialValue="0" ForeColor="red">*</asp:RequiredFieldValidator>
                </div>
                <br />
            <div>
                <table id="table-3">
                    <tbody>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblEntryType" runat="server" Text="Entry Type"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEntryType" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDDLET" runat="server" ErrorMessage="Please Select Entry Type" 
                                ControlToValidate="ddlEntryType" ValidationGroup="save" InitialValue="0" ForeColor="red">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblEntryDetail" runat="server" Text="Entry Detail"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEntryDetail" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEntryDetail" runat="server" ErrorMessage="Entry Detail is Required" 
                                ControlToValidate="txtEntryDetail" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEntryDetail" runat="server" ValidationGroup="save"
                                ErrorMessage="More than 100 charactor is not allow" ForeColor="red" 
                                    ControlToValidate="txtEntryDetail" ValidationExpression="^(.|\s){1,100}$">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDebit1" runat="server" Text="Debit"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDebit" runat="server" MaxLength="7"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDebit" runat="server" ErrorMessage="Debit is Required" 
                                ControlToValidate="txtDebit" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                <%--<asp:RangeValidator ID="rnvDebit" runat="server" ControlToValidate="txtDebit" MinimumValue="1" 
                                MaximumValue="7" ErrorMessage="You can't enter more than 7 digit." 
                                    ValidationGroup="save" ForeColor="red" Type="Integer">*</asp:RangeValidator>--%>

                                <asp:RegularExpressionValidator ID="revDebit" runat="server" ControlToValidate="txtDebit"
                                    ErrorMessage="Only Numeric Value is Allowed" ValidationGroup="save" ForeColor="Red" ValidationExpression="^\d{0,7}(\.\d{1,2})?$">*
                                    </asp:RegularExpressionValidator>

                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="save"/>
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div align="center">
                    <asp:GridView ID="gridLedger" runat="server" AutoGenerateColumns="False" 
                        Width="90%" onrowdatabound="gridLedger_RowDataBound" CellPadding="3" 
                        GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                        BorderStyle="None" BorderWidth="1px" AllowPaging="True" 
                        onpageindexchanging="gridLedger_PageIndexChanging" PageSize="20" >
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Date" ItemStyle-CssClass="griditemtext">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridDate" runat="server" Text='<%#Eval("BillDate","{0:dd-MMM-yyyy}") %>' ></asp:Label>
                                   <%-- <asp:Label ID="lblPaymentDate" runat="server" ></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle CssClass="griditemtext" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entry Type" ItemStyle-CssClass="griditemtext">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridEntryType" runat="server" Text='<%#Eval("EntryType") %>'></asp:Label>
                                     <asp:Label ID="lblBillId" runat="server" Text='<%#Eval("BillId") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="griditemtext" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="griditemtext">
                                <ItemTemplate>
                                   <%-- <asp:Label ID="lblClientName" runat="server" Text='<%#Eval("ClientName") %>'></asp:Label>--%>
                                    <asp:Label ID="lblEntryDetail" runat="server" Text='<%#Eval("EntryDetail") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="griditemtext" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit" ItemStyle-CssClass="griditemtext">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridDebit" runat="server" Text='<%#Eval("Credit") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="griditemtext" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit" ItemStyle-CssClass="griditemtext">
                            <ItemTemplate>
                                <asp:Label ID="lblGridCredit" runat="server" Text='<%#Eval("Debit") %>'></asp:Label>
                            </ItemTemplate>
                                <ItemStyle CssClass="griditemtext" />
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
                    <%--<div class="botdiv">
                    
                        <asp:Label ID="lblCreditBal" runat="server" Text="Credit Balance :"></asp:Label>    
                         <asp:Label ID="lblCredit" runat="server" Text=""></asp:Label>
                           &nbsp;&nbsp; &nbsp;&nbsp;  
                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>&nbsp;:&nbsp;&nbsp; 
                        <asp:Label ID="lblDebit" runat="server" Text="Label"></asp:Label>
                    </div>--%>
                    &nbsp;<%--<div class="botdiv">
                    
                        <asp:Label ID="lblCreditBal" runat="server" Text="Credit Balance :"></asp:Label>    
                         <asp:Label ID="lblCredit" runat="server" Text=""></asp:Label>
                           &nbsp;&nbsp; &nbsp;&nbsp;  
                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>&nbsp;:&nbsp;&nbsp; 
                        <asp:Label ID="lblDebit" runat="server" Text="Label"></asp:Label>
                    </div>--%><div style="text-align:right; width:90%; padding-right:30px; font-weight:bold; color:#5D7B9D;">
                        <asp:Label ID="lblAdvancePayment" runat="server" Text="Advance Payment :" Visible="false"></asp:Label>
                        <asp:Label ID="lblAdvance" runat="server" Text="" Visible="false"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblTotalBal" runat="server" Text="Total Balance :" Visible="false"></asp:Label>
                        <asp:Label ID="lblTotalBalance" runat="server"  Visible="false"></asp:Label><br /><br />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" 
                            onclick="btnPrint_Click" Width="68px" />
                    </div>
                    <br /><br /><br />
                  
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
