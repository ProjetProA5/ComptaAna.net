<%@ Page Title="Se connecter" Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="ComptaAna.net.Login" Theme="ComptaAna" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
     ComptAxe - Axe Environnement
    </title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/App_Themes/ComptaAna/Design/Logo_ComptaAna_a.png" type="image/x-icon"/>
    <link id="Link2" runat="server" rel="icon" href="~/App_Themes/ComptaAna/Design/Logo_ComptaAna_a.png" type="image/ico"/>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
</head>
    <body id="bodylogin" class="bodyLogin">
        <div class="connection">
        <form runat="server">
        <span>
            <asp:Label ID="lFailureText" CssClass="failureLogin" runat="server" Text="Identifiants incorrects, veuillez ressayer." />
        </span>
            <Table ID="Table1" runat="server" >
                <tr>
                    <td>
                        <asp:Label ID="lblLoginUser" runat="server" AssociatedControlID="tbLoginUser" />
                    </td>
                    <td>
                        <asp:TextBox ID="tbLoginUser" runat="server" Width="200px" CssClass="tbUser" />
                        <asp:RequiredFieldValidator ID="rfvLoginUser" runat="server" ControlToValidate="tbLoginUser"
                            CssClass="failureUser" ErrorMessage="Un nom d'utilisateur est requis."
                            ToolTip="Un nom d'utilisateur est requis." ValidationGroup="vsLoginUser" />
                    </td>
                    </tr>
                    
                    <tr>
                    <td>
                        <asp:Label ID="lblPassword" runat="server" AssociatedControlID="tbPassword" />
                    </td>
                    <td>
                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="200px" CssClass="tbMdp" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbPassword"
                            CssClass="failureMDP" ErrorMessage="Un mot de passe est requis." ToolTip="Un mot de passe est requis."
                            ValidationGroup="vsLoginUser" />
                    </td>
                    </tr>
                    <tr>
                    <td>
                        &nbsp;
                    </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Button ID="LoginButton" CssClass="btnLogin" runat="server" CommandName="Login" ValidationGroup="vsLoginUser" />                        
                    </td>
                    </tr>
              </Table>
              <asp:Label ID="lblPreprod" runat="server" ForeColor="Red" Visible="false"></asp:Label><br />
         </form>
         </div>
      </body>
</html>