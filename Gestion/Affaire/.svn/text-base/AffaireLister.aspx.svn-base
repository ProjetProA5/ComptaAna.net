<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireLister.aspx.vb" Inherits="ComptaAna.net.AffaireLister" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.TreeView" TagPrefix="obout" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 196px;
        }
        .style18
        {
            width: 378px;
            height: 30px;
        }
        .style20
        {
            width: 310px;
        }
        .style21
        {
            width: 248px;
        }
        .style22
        {
            width: 363px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManagerTEST" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div class="content">
        <h1>
            Liste des affaires
        </h1>
        <table width="100%">
            <tr>
                <td valign="top">
                    <div style="float: left; width: 100%; height: 1000px; margin-top: 10px;">
                        <table style="margin-top: 30px; border-spacing: 10px">
                            <tr>
                                <asp:Button ID="btnInsererAffaire" runat="server" CssClass="btn95" Text="Nouvelle affaire " />
                            </tr>
                            <br />
                            <tr>
                                <asp:Label ID="lblNoAffaire" runat="server"></asp:Label>
                            </tr>
                            <%-- Width="250px" Height="1000px"<td style="width:5%">
                
                </td>--%>
                            <obout:Tree ID="tvAffaires" CssClass="vista" runat="server" OnSelectedTreeNodeChanged="tvAffaires_SelectedTreeNodeChanged" />
                        </table>
                    </div>
                </td>
                <td valign="top" style="margin-right: 20px">
                    <div style="float: left; width: 100%; overflow: auto; margin-top: 0px;">
                        <table>
                            <tr>
                                <td valign="top">
                                    <%--style="width: 100%"--%>
                                    <h2 class="legende">
                                        Champs de recherche</h2>
                                    <fieldset class="recherche">
                                        <table style="margin-top: 0px; border-spacing: 10px; width: 100%;">
                                            <tr>
                                                <td valign="top">
                                                    <div style="float: left; overflow: auto; width: 100%; margin-top: 0px;">
                                                        <table style="margin-top: 0px; border-spacing: 10px">
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Label ID="LabelTri" runat="server" Text="Critère de tri:" Font-Underline="true"
                                                                        Font-Bold="true" CssClass="couleurTextRecherche" />
                                                                </td>
                                                                <td valign="top">
                                                                    <oboutComboBox:ComboBox ID="cbbChampRechercheAffaire" runat="server" Width="190px">
                                                                        <oboutComboBox:ComboBoxItem ID="cbbItemClient" Value="Client" runat="server" Text="Par Client" />
                                                                        <oboutComboBox:ComboBoxItem ID="cbbItemType" Value="Type" runat="server" Text="Par Type d'affaire" />
                                                                        <oboutComboBox:ComboBoxItem ID="cbbItemBU" Value="BU" runat="server" Text="Par BU" />
                                                                    </oboutComboBox:ComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Label ID="Labelcharge" CssClass="couleurTextRecherche" Font-Underline="true"
                                                                        runat="server" Font-Bold="true">Recherche par employé:</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <oboutComboBox:ComboBox ID="cbbEmploye" Width="190px" runat="server" DataTextField="PrenomNom"
                                                                        DataValueField="EmployeID">
                                                                    </oboutComboBox:ComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td valign="top">
                                                    <div style="float: left; overflow: auto; width: 100%; margin-top: 0px;">
                                                        <table style="margin-top: 0px; border-spacing: 10px">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" Font-Underline="true" runat="server" Font-Bold="true" CssClass="couleurTextRecherche">Recherche sur une période:</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDateDebut" runat="server" Text="Date de debut :" CssClass="couleurTextRecherche" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="tbDateDeb" Width="110px"/>
                                                                    <obout:Calendar ID="cDateDeb" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                                                        TextBoxId="tbDateDeb" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblDateFin" runat="server" Text="Date de fin :" CssClass="couleurTextRecherche" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="tbDateFin" Width="110px" />
                                                                    <obout:Calendar ID="cDateFin" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                                                        TextBoxId="tbDateFin" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" class="style18">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td valign="top">
                                                    <div style="float: left; overflow: auto; width: 100%; margin-top: 0px;">
                                                        <table style="margin-top: 0px; border-spacing: 10px">
                                                            <tr>
                                                                <td valign="top" colspan="2">
                                                                    <asp:Label ID="Label2" CssClass="couleurTextRecherche" runat="server" Text="Affiner la recherche:"
                                                                        Font-Underline="true" Font-Bold="true" />
                                                                    <asp:RadioButtonList ID="rbAffaireFiltre" runat="server">
                                                                        <asp:ListItem class="couleurTextRecherche">Terminées</asp:ListItem>
                                                                        <asp:ListItem class="couleurTextRecherche">En dépassement</asp:ListItem>
                                                                        <asp:ListItem class="couleurTextRecherche">Dormantes</asp:ListItem>
                                                                        <asp:ListItem class="couleurTextRecherche">En retard de facturation</asp:ListItem>
                                                                        <asp:ListItem class="couleurTextRecherche">En cours</asp:ListItem>
                                                                        <asp:ListItem class="couleurTextRecherche">Toutes les affaires</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="right" class="style18">
                                                    <table>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblCompteurAffaire" runat="server" ForeColor="White" Font-Bold="true"
                                                                    Text="" />
                                                            </td>
                                                            <td>
                                                                <br />
                                                            </td>
                                                            <td colspan="3" align="right">
                                                                <asp:ImageButton ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png" ID="ibValider"
                                                                    runat="server" ToolTip="Rechercher" CommandArgument='<%# Eval("ClientID") %>' />
                                                                <asp:ImageButton ImageUrl="~/App_Themes/ComptaAna/Design/Icon_Excel.png" ID="ibExporter"
                                                                    runat="server" ToolTip="Export Exel" />
                                                            </td>
                                                            <td align="left">
                                                                <asp:Button runat="server" ID="btnReinit" Text="Réinitialiser" CssClass="btn90" ToolTip="Nouvelle recherche" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <%-- Première ligne avec le champ de recherche table 2--%>
                            <tr>
                                <td width="100%" valign="top">
                                    <div align="center">
                                        <asp:Label ID="lblPeriodeIncorrete" runat="server" ForeColor="Red" Text="Période saisie incorrecte"
                                            Visible="False"></asp:Label>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbDateDeb"
                                            ControlToCompare="tbDateFin" Operator="LessThanEqual" Type="Date" Display="Dynamic"
                                            ErrorMessage="La période n'est pas valide<br/>" />
                                    </div>
                                    <asp:Menu ID="mMenuBouton" runat="server" Orientation="Horizontal" Visible="False">
                                        <StaticMenuStyle CssClass="SimpleStaticMenu" />
                                        <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
                                        <StaticSelectedStyle CssClass="SimpleStaticSelected" />
                                        <StaticHoverStyle CssClass="SimpleStaticHover" />
                                        <Items>
                                            <asp:MenuItem Text="»" Enabled="false" />
                                            <asp:MenuItem Value="2" Text="Détails" Selected="true" />
                                            <asp:MenuItem Value="3" Text="Produit" />
                                            <asp:MenuItem Value="4" Text="Facturation" />
                                            <asp:MenuItem Value="5" Text="Liste des sous-affaires" />
                                        </Items>
                                    </asp:Menu>
                                </td>
                            </tr>
                            <%--Deuxième ligne avec le menu dans la deuxième table--%>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblMsg" ForeColor="Red" /><br />
                                    <fieldset id="fs1" visible="false" runat="server">
                                        <legend>Informations Générales</legend>
                                        <table>
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
                                                    Libellé :
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="tbLibelle" Width="250px" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Terminée :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbTerminee" Enabled="false" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAffaireID" Visible="false" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Début:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbDate" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    Type d'affaire:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTypeAffaire" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Service:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbService" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    Chargé d'affaire:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbChargeDAffaire" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Commentaires:
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="tbCom" Style="resize: none;" TextMode="Multiline" Width="99%" Height="75px"
                                                        Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" align="right">
                                                    <asp:ImageButton ID="ibModifier" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png"
                                                        ToolTip="Modifier affaire" />
                                                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                                                        OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer cette affaire');"
                                                        ToolTip="Supprimer affaire" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset id="fs2" visible="false" runat="server">
                                        > <legend>Informations Budgétaires</legend>
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    Commande HT:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbBudget" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Total HT consommé:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbHTconso" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Prestation restante HT:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbPrestRest" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Frais HT consommés:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbFraisConso" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Nombre de jours passés:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbNbJoursPasses" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Coût moyen journalier:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbCtMoyen" Style="text-align: right" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset id="fs3" visible="false" runat="server">
                                        > <legend>Qualification</legend>
                                        <asp:GridView AutoGenerateColumns="False" ID="gvQualifAffaire" border="1" runat="server"
                                            EmptyDataText="Pas de données" ShowHeaderWhenEmpty="true" Width="600" OnRowCommand="gvQualifAffaire_RowCommand"
                                            CssClass="Grid">
                                            <Columns>
                                                <asp:BoundField DataField="Libelle" HeaderText="Qualification" SortExpression="Qualification">
                                                    <ItemStyle Height="20px" Width="160px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QualifMntUnitHT" HeaderText="PrixHT" SortExpression="PrixHT">
                                                    <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QualifNbJours" HeaderText="Nbre jours" SortExpression="NbreJours">
                                                    <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Prix total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tbPrixTotal" Enabled="false" Style="text-align: right" runat="server"
                                                            Text='<%# Bind("total") %>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbPrixTotal" runat="server" Text='<%# Eval("total") %>' />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%-- total--%>
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
                                                        <asp:TextBox runat="server" ID="tbTotal" Style="text-align: right" Enabled="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                    <fieldset id="fs4" visible="false" runat="server">
                                        > <legend>Etapes de Facturation</legend>
                                        <asp:GridView AutoGenerateColumns="False" ID="gvEtapesFactuPourcentage" border="1"
                                            runat="server" ShowHeaderWhenEmpty="true" Width="300" OnRowCommand="gvEtapesFactu_RowCommand"
                                            CssClass="Grid" EmptyDataText="Pas de données">
                                            <Columns>
                                                <asp:BoundField DataField="EtapeFacturePourcentage" HeaderText="% d'avancement" SortExpression="EtapeFacturePourcentage">
                                                    <ItemStyle Height="20px" Width="140px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Validation" ControlStyle-Width="140px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkValide" align="center" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("EtapeFactureValide")) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Table runat="server" ID="tblMois" CssClass="Grid">
                                            <asp:TableHeaderRow>
                                                <asp:TableHeaderCell ID="tbhcMois1" Width="100" Text="Mois" />
                                                <asp:TableHeaderCell ID="tbhcValidation1" Width="100" Text="Validation" />
                                                <asp:TableHeaderCell ID="affaireEtapeFactuID1" Width="100" Text="ID_etape" Visible="False" />
                                                <asp:TableHeaderCell ID="tbhcMois2" Width="100" Text="Mois" />
                                                <asp:TableHeaderCell ID="tbhcValidation2" Width="100" Text="Validation" />
                                                <asp:TableHeaderCell ID="affaireEtapeFactuID2" Width="100" Text="ID_etape" Visible="False" />
                                            </asp:TableHeaderRow>
                                            <asp:TableRow>
                                                <asp:TableCell>Janvier</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:CheckBox ID="cbMois1" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois1" align="center" runat="server" /></asp:TableCell><asp:TableCell>Juillet</asp:TableCell><asp:TableCell>
                                                        <asp:CheckBox ID="cbMois7" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois7" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell>Février</asp:TableCell><asp:TableCell>
                                                    <asp:CheckBox ID="cbMois2" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois2" align="center" runat="server" /></asp:TableCell><asp:TableCell>Août</asp:TableCell><asp:TableCell>
                                                        <asp:CheckBox ID="cbMois8" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois8" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell>Mars</asp:TableCell><asp:TableCell>
                                                    <asp:CheckBox ID="cbMois3" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois3" align="center" runat="server" /></asp:TableCell><asp:TableCell>Septembre</asp:TableCell><asp:TableCell>
                                                        <asp:CheckBox ID="cbMois9" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois9" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell>Avril</asp:TableCell><asp:TableCell>
                                                    <asp:CheckBox ID="cbMois4" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois4" align="center" runat="server" /></asp:TableCell><asp:TableCell>Octobre</asp:TableCell><asp:TableCell>
                                                        <asp:CheckBox ID="cbMois10" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois10" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell>Mai</asp:TableCell><asp:TableCell>
                                                    <asp:CheckBox ID="cbMois5" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois5" align="center" runat="server" />
                                                </asp:TableCell><asp:TableCell>Novembre</asp:TableCell><asp:TableCell>
                                                    <asp:CheckBox ID="cbMois11" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois11" align="center" runat="server" /></asp:TableCell></asp:TableRow><asp:TableRow>
                                                <asp:TableCell>Juin</asp:TableCell><asp:TableCell>
                                                    <asp:CheckBox ID="cbMois6" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois6" align="center" runat="server" /></asp:TableCell><asp:TableCell>Décembre</asp:TableCell><asp:TableCell>
                                                        <asp:CheckBox ID="cbMois12" align="center" runat="server" Enabled="false" /></asp:TableCell>
                                                <asp:TableCell Visible="False">
                                                    <asp:Label ID="lblMois12" align="center" runat="server" />
                                                </asp:TableCell></asp:TableRow></asp:Table></fieldset> </td></tr></table></div></td></tr></table></div><asp:Panel ID="pPopupAlerteSuppression" CssClass="modalPopup" runat="server" Visible="false"
        ScrollBars="Auto">
        <h2>
            Attention, vous êtes sur le point de supprimer une affaire. Voulez vous continuez
            ?</h2><table>
            <tr>
                <td>
                    <asp:Button ID="btnAnnuler" runat="server" Text="Annuler" CssClass="btn90" />
                </td>
                <td>
                    <asp:Button ID="btnContinuer" runat="server" Text="Continuer" CssClass="btn90" />
                </td>
            </tr>
        </table>
    </asp:Panel>
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
                var obj = document.getElementById('<%=ibValider.ClientID%>');
                obj.click();
            }
        }
    </script>
</asp:Content>
