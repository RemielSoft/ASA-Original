<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StallionVisa.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="hd1" runat="server">
    <title>Simple Login Form</title>
    <meta charset="UTF-8" />
    <meta name="Designer" content="PremiumPixels.com" />
    <meta name="Author" content="$hekh@r d-Ziner, CSSJUNTION.com" />
    <link rel="stylesheet" type="text/css" href="Styles/structure.css">
</head>
<body>
    <div style=" top:0px;">
    <div id="header"> 
    <img src="../images/loginheader.jpg" width="100%" height="174px" />
    </div>
    <div >
        <form class="box login" runat="server">
        <fieldset class="boxBody">
             <%-- <label>Username</label>
      <asp:TextBox ID="txtUserId" runat="server" TabIndex="1"></asp:TextBox>
	  <label>Password</label>
      <asp:TextBox ID="txtPassword" runat="server" TabIndex="2" TextMode="Password"></asp:TextBox>--%>

       <asp:Login ID="loginControl" runat="server" TitleText="" OnAuthenticate="loginControl_Authenticate"
                RememberMeSet="True" LoginButtonType="Image" LoginButtonImageUrl="../images/login_button.gif" >
                <TextBoxStyle CssClass="textboxlogin" />
                <LabelStyle CssClass="logintext"  />
            </asp:Login>

	</fieldset>
	<%--<footer>
	  <label><input type="checkbox" tabindex="3"  />Keep me logged in</label>

      <asp:Button ID="btnLogin" CssClass="btnLogin" runat="server" Text="Login" 
        TabIndex="4" />
	</footer>--%>
          
        </form>

    </div>
    
    
   
    <div id="bottom">
        <a href="http://www.remielsoft.com" target="_blank" class="txtbottom">
        Designed and Developed by ADP Infosystems Pvt. Ltd.
        </a>
    </div>
    </div>
</body>
</html>
