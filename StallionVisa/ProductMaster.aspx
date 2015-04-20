<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductMaster.aspx.cs" Inherits="StallionVisa.ProductMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        { height:12px; width:50px;}
        .griditemtext{ text-align:left;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnlCompany" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="save"
                ShowMessageBox="true" ShowSummary="false" />
                <div>
                <table id="table-3">
                    <tbody>
                    <tbody>
                           <%-- <tr>
                                <td width="20%">
                                    <asp:Label ID="lblCpnName" runat="server" Text="Company Name"></asp:Label>
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtCpnName" runat="server" CssClass="textbox1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCpnName" runat="server" ControlToValidate="txtCpnName"
                                         ErrorMessage="Company Name is Required " ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" Text="Item Description"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtitemdescription" runat="server"  TextMode="MultiLine" 
                                        Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtitemdescription"
                                         ErrorMessage="Description is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                </td>
                                
                                <caption>
                              
                                </caption>
                           </tr>
                            <tr>
                            <td>
                            Item</td>
                            <td>
                                <asp:RadioButtonList ID="rbtnitem" runat="server" RepeatDirection="Horizontal" 
                                    onselectedindexchanged="rbtnitem_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Electrical Stampings" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Die Casted Rotor" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Waste" Value="3"></asp:ListItem>
                                

                                </asp:RadioButtonList>
                                  <asp:RequiredFieldValidator ID="rfvRadiobutton" Display="None" runat="server" ForeColor="Red"
                                    ErrorMessage="Please Select Item" ControlToValidate="rbtnitem" ValidationGroup="save"></asp:RequiredFieldValidator></td></tr>
                                 <tr>
                            <td id="tdsubitem" runat="server">
                            Sub_Item</td>
                            <td id="tdsubitemradio" runat="server">
                                <asp:RadioButtonList ID="rbtnsubitem" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Stator Stamping" Value="4"></asp:ListItem>
                                <asp:ListItem Text=" Rotor Stamping" Value="5"></asp:ListItem>
                                

                                </asp:RadioButtonList></td></tr>
                              
                            

                           <%-- <tr>
                                <td>
                                    <asp:Label ID="lblDescription" runat="server" Visible="false" Text="Description"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server"  Visible="false" Width="180px" CssClass="textbox1" TabIndex="3" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCorporate" runat="server" ControlToValidate="txtCorporate"
                                         ErrorMessage="Corporate is Required" ForeColor="Red" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="center" colspan="2">
                                
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="save"
                                    Width="60"  onclick="btnSave_Click" />
                                <asp:Button ID="btnUpdate" runat="server" 
                                    Text="Update"  Visible="false" onclick="btnUpdate_Click" />
                                <asp:Button ID="btnCancel" runat="server" 
                                    Text="Cancel"  onclick="btnCancel_Click" />
                            </td>
                            </tr>
                        
                    </tbody>
                </table>
                <br />
                <div align="center">
                    <asp:GridView ID="grdproduct" runat="server"  AutoGenerateColumns="False"
                      CellPadding="3"  Width="90%"
                        GridLines="Vertical" onrowcommand="grdproduct_RowCommand" 
                        BackColor="White" BorderColor="#999999" BorderStyle="None" 
                        BorderWidth="1px" AllowPaging="True" 
                        onpageindexchanging="grdproduct_PageIndexChanging" PageSize="20" 
                        >                         
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

       <%-- <asp:TemplateField HeaderText="Copmany Name" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblCmpnName" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>--%>

       

        <asp:TemplateField HeaderText="Item"  ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblCompany_Name" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField> 
         <asp:TemplateField HeaderText="Sub_Item"  ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblsubitem" runat="server" Text='<%#Eval("SubitemName") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>   
         <asp:TemplateField HeaderText="Item Description" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemDescription") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>      


        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:LinkButton ID="lnkbtnEdit" CommandName="Eddit" CommandArgument='<%#Eval("ProductId") %>' runat="server" Text="Edit"></asp:LinkButton>&nbsp;|
                <asp:LinkButton ID="lnkbtnDelete" OnClientClick='return confirm("Are You Sure To Delete")' CommandName="Delet" CommandArgument='<%#Eval("ProductId") %>' runat="server" Text="Delete"></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
