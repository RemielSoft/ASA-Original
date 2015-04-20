<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="StallionVisa.UserRegistration" %>
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
        { height:12px; width:50px;}
        .griditemtext{ text-align:left;}
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnlRegistration" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="registration"
                ShowMessageBox="true" ShowSummary="false" />
                <div>
                <table id="table-3">
                    <tbody>
                    <tbody>
                            <tr>
                                <td width="20%">
                                    <asp:Label ID="lblLoginName" runat="server" Text="Login Name"></asp:Label>
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtLoginName" runat="server" CssClass="textbox1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLoginName" runat="server" ControlToValidate="txtLoginName"
                                         ErrorMessage="Login Name is Required " ForeColor="Red" ValidationGroup="registration">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLoginId" runat="server" Text="Login Id"></asp:Label>
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtLoginID" runat="server" CssClass="textbox1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtLoginID"
                                         ErrorMessage="Login Id is Required " ForeColor="Red" ValidationGroup="registration">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtpassword" runat="server" CssClass="textbox1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtpassword"
                                         ErrorMessage="Password is Required " ForeColor="Red" ValidationGroup="registration" >*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textbox1" ></asp:TextBox>
                                      <asp:CompareValidator ID="cvConfirmPassword" runat="server" ErrorMessage="Password is not same" ControlToCompare="txtpassword"
                             ControlToValidate="txtConfirmPassword" ValidationGroup="registration">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
                                </td>
                                <td width="80%">
                                    <asp:DropDownList ID="ddlRole" runat="server" Width="180px">                                        
                                        <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                                         ErrorMessage="Role is Required " ForeColor="Red" ValidationGroup="registration">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="registration"
                                    Width="60" TabIndex="4" onclick="btnSave_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" ValidationGroup="registration" 
                                    Text="Update" TabIndex="5" Visible="false" onclick="btnUpdate_Click" />
                                    <asp:Button ID="btnCancel" runat="server" 
                                    Text="Cancel" TabIndex="6" onclick="btnCancel_Click" />
                            </td>
                            </tr>
                        
                    </tbody>
                </table>
                <br />
                <div align="center">
                    <asp:GridView ID="grdRegistration" runat="server"  AutoGenerateColumns="False"
                      CellPadding="3"  Width="90%"
                        GridLines="Vertical" onrowcommand="grdRegistration_RowCommand" 
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px">                         
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

        <asp:TemplateField HeaderText="Login Name" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblLoginName" runat="server" Text='<%#Eval("LoginName") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Login Id" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblLoginId" runat="server" Text='<%#Eval("LoginId") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Password" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>     
       <%-- <asp:TemplateField HeaderText="Confirm Password"  Visible="false">
        <ItemTemplate>
                <asp:Label ID="lblConfirmPassword" runat="server" Text='<%#Eval("ConfirmPassword") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>--%>

        <asp:TemplateField HeaderText="Role" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:Label ID="lblRole" runat="server" Text='<%#Eval("Role") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>     


        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="griditemtext">
        <ItemTemplate>
                <asp:LinkButton ID="lnkbtnEdit" CommandName="Eddit" CommandArgument='<%#Eval("RegistrationId") %>' runat="server" Text="Edit"></asp:LinkButton>&nbsp;|
                <asp:LinkButton ID="lnkbtnDelete" OnClientClick='return confirm("Are You Sure To Delete")' CommandName="Delet" CommandArgument='<%#Eval("RegistrationId") %>' runat="server" Text="Delete"></asp:LinkButton>
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