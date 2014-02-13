<%@ Page Title="Statistiques Générales" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="StatistiquesGenerales.aspx.vb" Inherits="ComptaAna.net.StatitiquesGenerales" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1 class="h1bis">
            Statistiques générales</h1>
        <h2 class="legende2">
            Recherche</h2>
        <fieldset class="recherche2">
            <asp:Panel runat="server">
                <div align="center">
                    <%-- Champs recherche --%>
                    <table style="margin-top: 0px">
                        <tr>
                            <td valign="top">
                                <table style="margin-top: 0px; border-spacing: 10px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelTri" CssClass="couleurTextRecherche" runat="server" Text="Période:"
                                                Font-Underline="true" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="couleurTextRecherche">
                                            Date de début:
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbDateDeb" Width="65px" />
                                            <obout:Calendar ID="cDateDeb" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                                TextBoxId="tbDateDeb" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                                            </obout:Calendar>
                                        </td>
                                        <td class="couleurTextRecherche">
                                            Date de fin:
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="tbDateFin" Width="65px" />
                                            <obout:Calendar ID="cDateFin" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                                TextBoxId="tbDateFin" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                                            </obout:Calendar>
                                        </td>
                                        <td class="couleurTextRecherche">
                                            <asp:CheckBox ID="cbn1" runat="server" />année-1
                                        </td>
                                        <td class="couleurTextRecherche">
                                            <asp:CheckBox ID="cbn2" runat="server" />année-2
                                        </td>
                                        <%--<td>
                                    <asp:CheckBox ID="cbRANul" runat="server" Visible="false" Checked="false" />Afficher RA nuls
                                </td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table style="margin-top: 0px;">
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="lblRestriction" runat="server" Text="Restrictions:" Font-Underline="true"
                                                Font-Bold="true" CssClass="couleurTextRecherche" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <oboutComboBox:ComboBox ID="cbbFiltreStatGeneral" runat="server" AutoPostBack="True"
                                                SelectedIndex="0" OnSelectedIndexChanged="cbbFiltreStatGeneral_Click" Style="top: 0px;
                                                left: 0px">
                                                <oboutComboBox:ComboBoxItem ID="cbbItemToutEmploye" Value="Employe" runat="server"
                                                    Text="Tous les Employés" />
                                                <oboutComboBox:ComboBoxItem ID="cbbItemToutService" Value="Service" runat="server"
                                                    Text="Tous les Services" />
                                            </oboutComboBox:ComboBox>
                                        </td>
                                        <td>
                                            <oboutComboBox:ComboBox ID="cbbFiltreStatGalDetail" runat="server" CssClass="" />
                                        </td>
                                        <td valign="top">
                                            <table style="margin-top: 0px; border-spacing: 5px">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnRechercheStat" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png"
                                                            ValidationGroup="vgRechercher" ToolTip="Rechercher" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnExporter" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_Excel.png"
                                                            ValidationGroup="vgRechercher" ToolTip="Export Excel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <table style="margin-top: 0px; border-spacing: 0px">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Légende:" Font-Underline="true" Font-Bold="true"
                                                    Font-Italic="true" CssClass="couleurTextRecherche" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" ID="lblLegendeRAIncomplet"
                                                                runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRAnonComplet" CssClass="couleurTextRecherche" runat="server" Text="Relevé d'activité non complété à 100%"
                                                                Font-Italic="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label Text="00,00" ID="lblLegendeDepassement" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDep" runat="server" Text="En dépassement" CssClass="couleurTextRecherche"
                                                                Font-Italic="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </table>
                            </td>
                        </tr>
                        <%-- Message des validators --%>
                    </table>
                </div>
            </asp:Panel>
        </fieldset>
        <div align="center">
            <asp:RequiredFieldValidator ID="rfvTbDateDeb" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="tbDateDeb" ErrorMessage="&nbsp &nbsp Veuillez saisir une date de début. " />
            <asp:RequiredFieldValidator ID="rfvDateFin" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="tbDateFin" ErrorMessage="Veuillez saisir une date de fin. " />
            <asp:RequiredFieldValidator ID="rfvCbbFiltre1" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="cbbFiltreStatGeneral" ErrorMessage="Veuillez saisir une restriction. " />
            <%-- Affichage des stats --%>
            <div align="center">
                <asp:GridView AutoGenerateColumns="False" ID="gvStatGenerales" runat="server" CssClass="Grid"
                    OnRowDataBound="gvStat_RowDataBound" EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#EDEDED">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                        <asp:BoundField DataField="Objet" />
                        <asp:BoundField DataField="Annee" HeaderText="Année" />
                        <asp:BoundField DataField="TauxFactu" HeaderText="Taux de factu" />
                        <asp:BoundField DataField="CAHT" HeaderText="CA HT" />
                        <asp:BoundField DataField="Outils" HeaderText="Outils" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <oboutComboBox:ComboBox ID="cbbFiltreHeader" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbFiltreHeader_Click" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblColonneChoisie" runat="server" Text='<%# Eval("JourDetail") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NbJoursCA" HeaderText="Nb Jours CA" />
                        <asp:BoundField DataField="NbJoursRA" HeaderText="Nb Jours RA" />
                        <asp:BoundField DataField="lNbJoursOuvres" HeaderText="Nb Jours Total" />
                        <asp:BoundField DataField="NbJoursAbsents" HeaderText="Nb Jours Abs" />
                        <asp:BoundField DataField="Depassement" HeaderText="Dép CA" />
                        <asp:BoundField DataField="NbJoursDep" HeaderText="Jours Dép" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div runat="server" align="right">
    </div>
    </asp:Panel> </div>
</asp:Content>
