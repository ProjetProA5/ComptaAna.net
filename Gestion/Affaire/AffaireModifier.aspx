<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireModifier.aspx.vb" Inherits="ComptaAna.net.AffaireModifier" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #TextCom
        {
            width: 391px;
        }
        .style1
        {
            width: 469px;
        }
        .Grid
        {
            margin-right: 3px;
        }
        .style2
        {
            width: 168px;
        }
        .btn175
        {
        }
    </style>
    <script language="JavaScript" type="text/javascript">
      
              function PrestaRestante() {
            var budget = document.getElementById('<%= tbBudget.ClientID %>');
            var conso = document.getElementById('<%= tbHTconso.ClientID %>');
            var presta = document.getElementById('<%= tbPrestRest.ClientID %>');

            if ((budget.value != "") && (conso.value != "")) {
                var floatBudget = parseFloat(budget.value.replace(/,/, "."));
                var floatconso = parseFloat(conso.value.replace(/,/, "."));
                var total = floatBudget - floatconso;

                presta.value = total.toFixed(3);
                           }
        }

  </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">
    <div class="content">
        <asp:Menu ID="mMenuAffaireModif" runat="server" Orientation="Horizontal" >
            <StaticMenuStyle CssClass="SimpleStaticMenu"/>
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover"/>
            <Items>
                <asp:MenuItem Text="»" Enabled="false" />
                <asp:MenuItem Value="0" Text="D&eacute;tails"  Selected="True"/>
                <asp:MenuItem Value="1" Text="Produit"  />
                <asp:MenuItem Value="2" Text="Facturation" />
                <asp:MenuItem Value="3" Text="Liste des sous-affaire"  />
                 </Items>
        </asp:Menu>

        <h1>
            Informations principales</h1>

            <asp:Label ID="lblNonModif" runat="server" Text="Aucune modification n'est autorisée!!" Visible="false" ForeColor="Red" Font-Bold="true" ></asp:Label>
          
        <%-- Informations generales--%>
        <fieldset id="FSInfoGen" runat="server" class="AffaireForm">
            <legend>Informations G&eacute;n&eacute;rales </legend>
            <table>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblErreurInsertion" Visible="false" Width="350px" Enabled="false"
                            runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                            Client :
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="tbClient" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Libell&eacute;* :
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="tbLibelle" Width="250px" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        Termin&eacute;e:
                    </td>
                    <td>
                        <asp:CheckBox ID="cbTerminee" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblAffaireID" Visible="false" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        D&eacute;but* :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbDate" Width="65px" />
                        <obout:Calendar ID="cCalendrier" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                            TextBoxId="tbDate" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                        </obout:Calendar>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Type Affaire* :
                    </td>
                    <td>
                        <oboutComboBox:ComboBox ID="cbTypeAffaire" Width="160px" runat="server" DataTextField="TypeAffaireLibelle"
                            DataValueField="TypeAffaireID">
                        </oboutComboBox:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Service* :
                    </td>
                    <td>
                        <oboutComboBox:ComboBox ID="cbService" Width="160px" runat="server" DataTextField="ServiceLibelle"
                            DataValueField="ServiceID">
                        </oboutComboBox:ComboBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Charg&eacute; d'affaire* :
                    </td>
                    <td>
                        <oboutComboBox:ComboBox ID="cbChargeAffaire" Width="160px" runat="server" DataTextField="PrenomNom"
                            DataValueField="EmployeID">
                        </oboutComboBox:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Commentaires:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="tbCom" TextMode="Multiline" Width="99%" Height="75px"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CompareValidator ID="cvDate" runat="server" Type="Date" Display="Dynamic" Operator="DataTypeCheck"
                            ControlToValidate="tbDate" ErrorMessage="Veuillez insérer une date valide<br/>" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <%-- informations Budgetaires--%>
        <fieldset id="FSInfoBudget" runat="server" class="InfoBudForm">
            <legend>Informations Budg&eacute;taires</legend>
            <table>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revtbBudget" ValidationGroup="vgInserer" runat="server"
                            Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*"  ControlToValidate="tbBudget"
                            ErrorMessage="Veuillez insérer un nombre">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Commande HT:
                    </td>
                    <td>
                        <asp:TextBox ID="tbBudget" Style="text-align: right" onKeyUp="javascript: PrestaRestante();" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        Total HT consomm&eacute;:
                    </td>
                    <td>
                        <asp:TextBox ID="tbHTconso" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        Prestation restante HT:
                    </td>
                    <td>
                        <asp:TextBox ID="tbPrestRest" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        Frais HT consomm&eacute;s:
                    </td>
                    <td>
                        <asp:TextBox ID="tbFraisConso" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        Nombre de jours pass&eacute;s:
                    </td>
                    <td>
                        <asp:TextBox ID="tbNbJoursPasses" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        Co&ugrave;t moyen journalier:
                    </td>
                    <td>
                        <asp:TextBox ID="tbCtMoyen" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnValider" ValidationGroup="vgInserer" runat="server" Text="Enregistrer"
                        CssClass="btn90" />
                </td>
            </tr>
        </table>
        <h1>
            Autres informations</h1>
        <%-- Affichage Qualification--%>
        <fieldset id="FSQualif" runat="server" class="QualificationFrom">
            <legend>Qualification</legend>
            <asp:Label runat="server" ID="lblMsgQualif" ForeColor="gray" Text="Appuyer sur le bouton 'Modifier' pour enregister chaque modification."/>
            <asp:GridView AutoGenerateColumns="False" ID="gvQualifAffaire" runat="server" EmptyDataText="Pas de donnée"
                ViewStateMode="Enabled" EnableViewState="False" OnRowDataBound="gvQualifAffaire_RowDataBound"
                OnRowCommand="gvQualifAffaire_RowCommand" CssClass="Grid" Width="800" ShowHeaderWhenEmpty="true">
                <Columns>
                    <asp:TemplateField HeaderText="Qualification">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlQualif" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlQualif" AutoPostBack="true" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PrixHT">
                        <ItemTemplate>
                            <asp:TextBox ID="tbPrixHT" runat="server" Text='<%# Bind("QualifMntUnitHT") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbPrixHT" runat="server" Text='<%# Eval("QualifMntUnitHT") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nbre jours">
                        <ItemTemplate>
                            <asp:TextBox ID="tbNbreJours" runat="server" Text='<%# Bind("QualifNbJours") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbNbreJours" runat="server" Text='<%# Eval("QualifNbJours") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prix total">
                        <ItemTemplate>
                            <asp:TextBox ID="tbPrixTotal" Enabled="false" runat="server" Text='<%# Bind("total") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbPrixTotal" Enabled="false" runat="server" Text='<%# Eval("total") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Modifier">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnModifier" runat="server" CommandName="EnregistrerAffaireQualif"
                                CommandArgument='<%# CType(Container, GridViewRow).RowIndex %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" 
                                ToolTip="Modifier" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supprimer">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                                OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer cette qualification?');"
                                ToolTip="Supprimer"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id Qualification" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblQualificationID" runat="server" Text='<%# Bind("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id Affaire Qualification" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAffaireQualificationID" runat="server" Text='<%# Bind("aID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%-- ligne total --%>
            <div runat="server" id="divTotal" visible="false">
                <table>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblTotal" Text="Total :" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" Enabled="false" ID="tbTotal" />
                        </td>
                    </tr>
                </table>
            </div>
            <%--ajout d'une qualification--%>
            <table id="tableAjoutQualif" runat="server">
                <tr>
                    <td>
                        <asp:DropDownList ID="listeQualif" Width="160px" runat="server" DataTextField="Libelle"
                            DataValueField="ID" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="tbPrixHT" Width="165px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tbNbJours" Width="165px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAjouter" runat="server" ValidationGroup="vgQualif" Text="Ajouter"
                            CssClass="btn90" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vgQualif"
                            runat="server" Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*"
                            ControlToValidate="tbPrixHT" ErrorMessage="Veuillez insérer un prix HT correct">
                        </asp:RegularExpressionValidator>
                    </td>
                    </tr>
                      <tr>
                        <td colspan="3">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="vgQualif"
                                runat="server" Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*"
                                ControlToValidate="tbNbJours" ErrorMessage="Veuillez insérer un nombre de jours correct">
                            </asp:RegularExpressionValidator>
                        </td>

                    </tr>
                
            </table>
            <asp:Label ID="lblerror" Visible="false" Width="350px" Enabled="false" runat="server"></asp:Label>
        </fieldset>
        <%-- Etapes de Facturation--%>
        <fieldset id="FSFactu" runat="server" class="EtapeFactuFrom">
            <legend>Etapes de Facturation</legend>

            <%--GridView Etape de facturation en Pourcentage de taux d'avancement--%>
            <asp:GridView ID="gvEtapesFactuPourcentage" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" EmptyDataText="Pas de données"
                OnRowCommand="gvEtapesFactuPourcentage_RowCommand" EnableViewState="False" CssClass="Grid" ShowHeaderWhenEmpty="true" Width="300">
                <Columns>
                    <asp:TemplateField HeaderText="% d'avancement" ControlStyle-Width="140px">
                        <ItemTemplate>
                            <asp:TextBox ID="tbEtapeFacturePourcentage" runat="server" Text='<%# Bind("EtapeFacturePourcentage") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbEtapeFacturePourcentage" CommandName="ModifTauxEtapeFactu" runat="server" Text='<%# Eval("EtapeFacturePourcentage") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Validation" ControlStyle-Width="140px">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbValidation" align="center" runat="server" Checked='<%# Bind("EtapeFactureValide") %>'/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="cbValidation" CommandName="ModifValidEtapeFactu" runat="server" Checked='<%# Eval("EtapeFactureValide") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="idAffaireEtapeFacture" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="idAffaireEtapeFacture" runat="server" Text='<%# Bind("AffaireEtapeFactureID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:ImageButton ID="Ajouter" runat="server" align="left" CommandName="AjouterTauxEtapFactu"
                                            CommandArgument='<%# Eval("AffaireEtapeFactureID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_ajouter.png"
                                            ToolTip="Ajouter"  />
                        </HeaderTemplate>
                            <ItemTemplate>
                                   <asp:ImageButton ID="Supprimer" runat="server" align="center" CommandName="SupprimerTauxEtapFactu"
                                            CommandArgument='<%# Eval("AffaireEtapeFactureID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png" 
                                            OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce pourcentage de facturation?');"
                                            ToolTip="Supprimer" />
                            </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <%--GridView Etape de facturation en detail du mois--%>

            <asp:Table runat="server" ID="tblMois" CssClass="Grid">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell Width="100" ID="tbhcMois1" Text="Mois" />
                    <asp:TableHeaderCell Width="100" ID="tbhcValidation1" Text="Validation" />
                    <asp:TableHeaderCell ID="affaireEtapeFactuID1" Width="100" Text="id_Etape" Visible="False"/>
                    <asp:TableHeaderCell Width="100" ID="tbhcMois2" Text="Mois" />
                    <asp:TableHeaderCell Width="100" ID="tbhcValidation2" Text="Validation" />
                    <asp:TableHeaderCell ID="affaireEtapeFactuID2" Width="100" Text="id_Etape" Visible="False"/>
                </asp:TableHeaderRow>

                <asp:TableRow>
                    <asp:TableCell>Janvier</asp:TableCell>
                    <asp:TableCell><asp:CheckBox ID="cbMois1" align="center" runat="server"/></asp:TableCell>
                    <asp:TableCell Visible="false"><asp:label ID="lblMois1" align="center" runat="server" /></asp:TableCell><asp:TableCell>Juillet</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois7" align="center" runat="server"/></asp:TableCell>
                    <asp:TableCell Visible="false"><asp:label ID="lblMois7" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                     <asp:TableCell>F&eacute;vrier</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois2" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois2" align="center" runat="server" /></asp:TableCell><asp:TableCell>Ao&ucirc;t</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois8" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois8" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                     <asp:TableCell>Mars</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois3" align="center" runat="server" /></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois3" align="center" runat="server" /></asp:TableCell><asp:TableCell>Septembre</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois9" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois9" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                     <asp:TableCell>Avril</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois4" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois4" align="center" runat="server" Visible="False" /></asp:TableCell><asp:TableCell>Octobre</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois10" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois10" align="center" runat="server" Visible="False" /></asp:TableCell></asp:TableRow><asp:TableRow>
                     <asp:TableCell>Mai</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois5" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois5" align="center" runat="server" /></asp:TableCell><asp:TableCell>Novembre</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois11" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois11" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                     <asp:TableCell>Juin</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois6" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois6" align="center" runat="server" /></asp:TableCell><asp:TableCell>Decembre</asp:TableCell><asp:TableCell><asp:CheckBox ID="cbMois12" align="center" runat="server"/></asp:TableCell>
                     <asp:TableCell Visible="False"><asp:label ID="lblMois12" align="center" runat="server" /></asp:TableCell></asp:TableRow></asp:Table><asp:Label ID="ErreurPourcentage" Text="Les pourcentages de facturation dépassent 100%, veuillez saisir des valeurs correctes." runat="server" Visible="false" ForeColor="Red" > </asp:Label><br />
            <br />
            <asp:Button ID="btnEnregistrerEtape" runat="server" Text="Enregistrer" CssClass="btn90" />
        </fieldset>
   
                       <asp:Button ID="btnRetourListe" runat="server" Text="Retour" CssClass="btn90" />
    </div>
</asp:Content>