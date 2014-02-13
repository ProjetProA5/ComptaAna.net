<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="EmployeModifier.aspx.vb" Inherits="ComptaAna.net.EmployeModifier" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .style1
        {
            width: 128px;
        }
        .btn90
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
    <h1>
        <asp:Label ID="lbInsererModifierUtilisateur" runat="server"></asp:Label>
    </h1>
        <asp:Menu ID="mOnglets" runat="server" Orientation="Horizontal">
            <StaticMenuStyle CssClass="SimpleStaticMenu" />
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover" />
            <Items>
                <asp:MenuItem Value="1" Text="Détails" Selected="true" />
                <asp:MenuItem Value="2" Text="Coût"/>
                <asp:MenuItem Value="3" Text="Droits" />
                  <asp:MenuItem Value="4" Text="Formation" />
                    <asp:MenuItem Value="5" Text="Echéances annuelles" />
            </Items>
        </asp:Menu>

        <br />
        <fieldset class="loginUserForm">
        <asp:Panel ID="modifEmpoye" runat="server" Enabled="true">
            <asp:Label ID="lEnregistrementOk" ForeColor="Blue" runat="server" Text="L'enregistrement a été effectué correctement" Visible="false"/>
            <legend>Informations du compte</legend>
            <table>
                <tr>
                    <td>
                        Actif
                    </td>
                    <td>
                        <asp:CheckBox ID="cbActif" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nom* :
                    </td>
                    <td>
                        <asp:TextBox ID="tbNom" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvTbNom" ValidationGroup="vgInserer" runat="server"
                            Display="Dynamic" ControlToValidate="tbNom" ErrorMessage="Le nom est obligatoire">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Prenom* :
                    </td>
                    <td>
                        <asp:TextBox ID="tbPrenom" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="revTbPrenom" ValidationGroup="vgInserer" runat="server"
                            Display="Dynamic" ControlToValidate="tbPrenom" ErrorMessage="Le prenom est obligatoire">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Login* :
                    </td>
                    <td>
                        <asp:TextBox ID="tbLogin" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvTbLogin" ValidationGroup="vgInserer" runat="server"
                            Display="Dynamic" ControlToValidate="tbLogin" ErrorMessage="Le login est obligatoire">

                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Mot de passe* :
                    </td>
                    <td>
                        <asp:TextBox ID="tbMotDePasse" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvTbMotDePasse" runat="server" Display="Dynamic"
                            ControlToValidate="tbMotDePasse" ValidationGroup="vgInserer" ErrorMessage="Le mot de passe est obligatoire">

                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date entr&eacute;e* :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbDate" Width="65px" />
                        <obout:Calendar ID="cCalendrier" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                            TextBoxId="tbDate" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                        </obout:Calendar>
                    </td>
                    <td>
                        <asp:RangeValidator ID="rvDate" runat="server" ControlToValidate="tbDate" ErrorMessage="Veuillez inserer une date valide"
                            Type="Date" MinimumValue="01/01/1900" MaximumValue="01/01/2100" Display="Dynamic">

                        </asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="rfvCcalendrier" ValidationGroup="vgInserer" runat="server"
                            Display="Dynamic" ControlToValidate="tbDate" ErrorMessage="La date est obligatoire">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cout de facturation interne HT* :
                    </td>
                    <td>
                        <asp:TextBox ID="tbCoutFacturationInterne" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvCoutFacturationInternet" ValidationGroup="vgInserer"
                            runat="server" Display="Dynamic" ControlToValidate="tbCoutFacturationInterne"
                            ErrorMessage="Le Coût est obligatoire">

                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvCoutFacturationInterne" ValidationGroup="vgInserer"
                            runat="server" Display="Dynamic" ControlToValidate="tbCoutFacturationInterne"
                            ErrorMessage="Le Coût est obligatoire">

                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtbCoutFacturationInterne" ValidationGroup="vgInserer"
                            runat="server" Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*"
                            ControlToValidate="tbCoutFacturationInterne" ErrorMessage="Veuillez insérer un nombre">

                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email* :
                    </td>
                    <td>
                        <asp:TextBox ID="tbEmail" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revtbEmail" runat="server" Display="Dynamic"
                            ValidationGroup="vgInserer" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="tbEmail" ErrorMessage="Veuillez insérer un email valide">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Service :
                    </td>
                    <td>
                        <oboutComboBox:ComboBox ID="lbService" runat="server" DataTextField="ServiceLibelle"
                            DataValueField="ServiceID" Width="200px">
                        </oboutComboBox:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Profil :
                    </td>
                    <td>
                        <oboutComboBox:ComboBox ID="lbProfil" runat="server" DataTextField="ProfilLibelle"
                            DataValueField="ProfilID" Width="200px">
                        </oboutComboBox:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Qualification :
                    </td>
                    <td>
                        <oboutComboBox:ComboBox ID="lbQualification" runat="server" DataTextField="Libelle"
                            DataValueField="ID" Width="200px">
                        </oboutComboBox:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Statut :
                    </td>
                    <td>
                       <asp:RadioButtonList ID="rbStatut" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value= "0" >Cadre</asp:ListItem>
                            <asp:ListItem Value= "1">Non cadre</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sexe :
                    </td>
                    <td>
                       <asp:RadioButtonList ID="rbSexe" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value= "0" >Homme</asp:ListItem>
                            <asp:ListItem Value= "1">Femme</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </fieldset>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btValider" ValidationGroup="vgInserer" CssClass="btn90" runat="server"
                        Text="Enregistrer"  CommandArgument ='<%# Eval("ClientID") %>'/>
                </td>
                <td>
                    <asp:Button ID="btRetourListeEmploye" CssClass="btn95" runat="server" Text="Retour à la liste" />
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        document.onkeypress = keyhandler;
        function keyhandler(e) {
            var Key;

            if (window.event)        // IE
            {
                Key = window.event.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                Key = e.which
            }

            if (Key == 13) {
                var obj = document.getElementById('<%=btValider.CLientID%>');
                obj.click();
            }
        }
    </script>
</asp:Content>
