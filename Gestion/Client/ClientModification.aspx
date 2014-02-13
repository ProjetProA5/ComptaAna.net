<%@ Page Title="Client" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="ClientModification.aspx.vb" Inherits="ComptaAna.net.ClientModification" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content" align="left">
        <h1>
            <asp:Label ID="lblTitre" runat="server"></asp:Label>            
        </h1>
        <br />
       
        <fieldset class="loginUserForm"> 
         <asp:panel ID="panelModif" enabled="True" runat="server">
            <legend><asp:Label ID="lblLegende" runat="server"></asp:Label></legend>
            <div align="left">
                <asp:Label ID="lMsg" runat="server" ForeColor="red" />
            </div>
            <table>
                <tr>
                    <td>
                        Client actif:
                    </td>
                    <td>
                        <asp:CheckBox Checked="true" ID="cbClientActif" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nom* :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbClientNom" runat="server" Width="342px" />
                    </td>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="rfvTbNom" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ControlToValidate="tbClientNom" ErrorMessage=" N'oubliez pas le nom!" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Adresse* :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbClientAdresse" TextMode="multiline" runat="server" Width="340px"
                            Height="80px" />
                    </td>
                    <td align="left" valign="top">
                        <asp:RequiredFieldValidator ID="rfvTbAdresse" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ControlToValidate="tbClientAdresse" ErrorMessage=" N'oubliez pas de saisir l'adresse!" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Code postal* :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbClientCP" runat="server" Width="340px" />
                    </td>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="rfvTbCP" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ControlToValidate="tbClientCP" ErrorMessage=" N'oubliez pas le code postal!" />
                        <asp:RegularExpressionValidator ID="revTbClientCP" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ValidationExpression="^[0-9]{5}$" ControlToValidate="tbClientCP"
                            ErrorMessage="Veuillez insérer un code postal correct." />
                    </td>
                </tr>
                <tr>
                    <td>
                        Ville* :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbClientVille" runat="server" Width="340px" />
                    </td>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="rfvTbVille" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ControlToValidate="tbClientVille" ErrorMessage=" N'oubliez pas la ville!" />
                    </td>
                </tr>
                <tr>
                    <td>
                        E-mail :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbClientMail" runat="server" Width="340px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Facturation Complement :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbFacturationComplement" runat="server" Width="340px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Facturation numero TVA* :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tbFacturationNumTVAFR" runat="server" Width="70px" Text="FR" />
                        <asp:TextBox ID="tbFacturationNumTVA2" runat="server" Width="70px" />
                        <asp:TextBox ID="tbFacturationNumTVA9" runat="server" Width="184px" />
                    </td>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="rfvTbTacNumTVAFR" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ControlToValidate="tbFacturationNumTVAFR" ErrorMessage=" N'oubliez pas le numéro de facturation!" />
                        <asp:RegularExpressionValidator ID="revTbTacNumTVAFR" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ValidationExpression="^[A-Z a-z]{2}$" ControlToValidate="tbFacturationNumTVAFR"
                            ErrorMessage="Première case: veuillez insérer deux lettres." />

                        <asp:RequiredFieldValidator ID="rfvTbTacNumTVA2" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ControlToValidate="tbFacturationNumTVA2" ErrorMessage=" N'oubliez pas le numéro de facturation!" />
                        <asp:RegularExpressionValidator ID="revTbTacNumTVA2" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ValidationExpression="^[0-9]{2}$" ControlToValidate="tbFacturationNumTVA2"
                            ErrorMessage="Deuxième case: Veuillez insérer deux chiffres." />

                        <asp:RequiredFieldValidator ID="rfvTbTacNumTVA9" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ControlToValidate="tbFacturationNumTVA9" ErrorMessage=" N'oubliez pas le numéro de facturation!" />
                        <asp:RegularExpressionValidator ID="revTbTacNumTVA9" ValidationGroup="ClientInfo" runat="server"
                            Display="Dynamic" ValidationExpression="^[0-9]{9}$" ControlToValidate="tbFacturationNumTVA9"
                            ErrorMessage="Troisième case: veuillez insérer neuf chiffres." />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbllisteSite" runat="server">Liste des sites: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:GridView AutoGenerateColumns="False" ID="gvClientSite" runat="server" EmptyDataText="Pas de données"
                            ShowHeaderWhenEmpty="true" OnRowCommand="gvClientSite_RowCommand" CssClass="Grid">
                            <Columns>
                                <asp:BoundField DataField="ClientNom" HeaderText="Nom du client site" SortExpression="ClientNom" />
                                <asp:BoundField DataField="ClientVille" HeaderText="Ville du client site" SortExpression="ClientVille" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:ImageButton ID="Ajouter" runat="server" align="center" CommandName="AjouterClientSite"
                                            CommandArgument='<%# Eval("ClientID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_ajouter.png"
                                            ToolTip="Ajouter un site" 
                                             />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Modifier" runat="server" align="center" CommandName="ModifierClientSite"
                                            CommandArgument='<%# Eval("ClientID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png"
                                            ToolTip="Modifier site"  />
                                        <asp:ImageButton ID="Supprimer" runat="server" align="center" CommandName="SupprimerClientSite"
                                            CommandArgument='<%# Eval("ClientID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png" 
                                            OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce client site?');"
                                            ToolTip="Supprimer site"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblListeAffaire" runat="server">Liste des affaires:</asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:GridView AutoGenerateColumns="False" ID="gvClientAffaire" runat="server" EmptyDataText="Pas de données"
                            ShowHeaderWhenEmpty="true" CssClass="Grid" OnRowDataBound="gvClientAffaire_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="TypeAffaireLibelle" HeaderText="Type d'affaire" />
                                <asp:BoundField DataField="AffaireLibelle" HeaderText="Référence" />
                                <asp:BoundField DataField="AffaireDateDeb" HeaderText="Date de début" />
                                <asp:BoundField DataField="AffaireBudget" HeaderText="Budget" />
                                <asp:BoundField DataField="BudgetReel" HeaderText="Budget Réel" />
                                <asp:BoundField DataField="employe" HeaderText="Employé" />
                                <asp:TemplateField HeaderText="Terminé" ItemStyle-HorizontalAlign="Center">
                                     <ItemTemplate>
                                        <asp:checkbox ID="cbAffaireTermine" runat="server" Checked='<%# Bind("AffaireTermine") %>' Enabled="false"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            </asp:panel>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="bEnregistrerModifClient" class="btn90" runat="server" Text="Enregistrer"
                            ValidationGroup="ClientInfo" CommandArgument ='<%# Eval("ClientID") %>'/>
                    </td>
                    <td>
                        <asp:Button ID="bListeClients" class="btn75" runat="server" Text="Retour" Visible = "false" />
                        <asp:Button ID="bFicheClient" class="btn75" runat="server" Text="Retour" visible="false"/>
                    </td>
                </tr>
            </table>
    
    </fieldset>
    
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
                var obj = document.getElementById('<%=bEnregistrerModifClient.CLientID%>');
                obj.click();
            }
        }
    </script>
</asp:Content>
