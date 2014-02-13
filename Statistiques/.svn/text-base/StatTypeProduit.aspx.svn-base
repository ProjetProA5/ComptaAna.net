<%@ Page Title="Statistiques sur les frais fixes et frais variables" Language="vb"
    AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="StatTypeProduit.aspx.vb"
    Inherits="ComptaAna.net.StatTypeProduit" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1 class="h1bis">
            Statistiques sur les frais fixes et frais variables</h1>
        <div class="content" align="center">
        <h2 class="legende3">Recherche</h2>
        <fieldset class="recherche3">
            
            <%-- Message des validators --%>
            <%-- Champs recherche --%>
            <table style="margin-top: 0px; border-spacing: 10px">
                <tr>
                    <td valign="top">
                        <table style="margin-top: 0px; border-spacing: 10px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPeriode" runat="server" Text="Période:" CssClass="couleurTextRecherche" Font-Underline="true" Font-Bold="true" />
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
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table style="margin-top: 0px; border-spacing: 10px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblRestriction" runat="server" Text="Restrictions:" Font-Underline="true"
                                        Font-Bold="true" CssClass="couleurTextRecherche" />
                                </td>
                                </tr>
                        <tr>
                        
                                <td>
                                    <oboutComboBox:ComboBox ID="cbbFiltreStatTypeProduit" runat="server" EnableLoadOnDemand="True"
                                        Width="200px" />
                                </td>
                                  <td>
                                    <oboutComboBox:ComboBox ID="cbbFiltreStatBU" runat="server" EnableLoadOnDemand="True"
                                        Width="200px" />
                                </td>
                              
                                <td class="couleurTextRecherche">
                                    <asp:CheckBox ID="cbn1" runat="server" />année-1
                                </td>
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
            </table>
        </fieldset>
        </div>
        <div align="center">
            <asp:RequiredFieldValidator ID="rfvCbbFiltre1" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="cbbFiltreStatTypeProduit" ErrorMessage="Veuillez saisir une restriction." />
            <asp:RequiredFieldValidator ID="rfvDateFin" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="tbDateFin" ErrorMessage="Veuillez saisir une date de fin." />
            <asp:RequiredFieldValidator ID="rfvTbDateDeb" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="tbDateDeb" ErrorMessage="Veuillez saisir une date de début."/>
            <asp:GridView ID="gvStatTypeProduit" AutoGenerateColumns="false" runat="server" CssClass="Grid"
                EmptyDataText="Pas de données" ShowHeaderWhenEmpty="true">
                <Columns>
                    <asp:BoundField DataField="ProduitID" HeaderText="" Visible="false" />
                    <asp:BoundField DataField="ProduitRef" HeaderText="Type de produit" />
                    <asp:BoundField DataField="ProduitLibelle" HeaderText="" />
                    <asp:BoundField DataField="TypeProduitID" HeaderText="" Visible="false" />
                    <asp:BoundField DataField="TypeProduitLibelle" HeaderText="" Visible="false" />
                    <asp:BoundField DataField="ProduitCA" HeaderText="Chiffre d'affaire" />
                    <asp:BoundField DataField="ProduitN1" HeaderText="CA - 1" Visible="false" />
                    <asp:TemplateField HeaderText="" Visible="true">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAfficherDetailsEmploye" runat="server" CommandName="AfficherDetailsEmploye"
                                Visible="true" CommandArgument='<%# Eval("ProduitID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:Panel ID="pPopupInfosDetailsEmployes" CssClass="modalPopup" runat="server" Visible="false"
        ScrollBars="Auto" Height="600px" Width="600px">
        <div runat="server" align="center">
        <br />
            <h2>
                Employés ayant entrés des informations pour ce type de produit</h2>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvInfosDetailsEmployes" runat="server" AutoGenerateColumns="false"
                            ScrollBars="True" CssClass="Grid" EmptyDataText="Pas de données" ShowHeaderWhenEmpty="true"
                            AlternatingRowStyle-BackColor="#E5F3FF">
                            <Columns>
                                <asp:BoundField DataField="ProduitAffaireID" HeaderText="" Visible="false" />
                                <asp:BoundField DataField="EmployeID" HeaderText="Employe ID" Visible="false" />
                                <asp:BoundField DataField="EmployeNom" HeaderText="Nom de l'Employé" />
                                <asp:BoundField DataField="ServiceLibelle" HeaderText="Service" />
                                <asp:BoundField DataField="ProduitAffaireQte" HeaderText="Quantité" />
                                <asp:BoundField DataField="ProduitAffaireMntUnitHT" HeaderText="Montant à l'unité" />
                                <asp:TemplateField HeaderText="Consulter" Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnAfficherDetailsAffaires" runat="server" CommandName="AfficherDetailsAffaires"
                                            Visible="true" CommandArgument='<%# Eval("EmployeID").ToString() &";"& Eval("ProduitAffaireID").ToString() %>'
                                            ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    </tr>
                    <caption>
                        <br />
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnQuitterDetailsEmployes" runat="server" CssClass="btn90" 
                                    Text="Quitter" />
                            </td>
                        </tr>
                </caption>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pPopupInfosDetailsAffaire" CssClass="modalPopup" runat="server" Visible="false"
        ScrollBars="Auto" Height="600px" Width="700px">
        <div id="Div1" runat="server" align="center">
        <br />
            <h2>
                Cet employé a entré des informations pour cette affaire</h2>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvInfosDetailsAffaire" runat="server" AutoGenerateColumns="false"
                            ScrollBars="True" CssClass="Grid" EmptyDataText="Pas de données" ShowHeaderWhenEmpty="true"
                            AlternatingRowStyle-BackColor="#E5F3FF">
                            <Columns>
                                <asp:BoundField DataField="ClientNom" HeaderText="Nom du client" />
                                <asp:BoundField DataField="AffaireLibelle" HeaderText="Nom de l'affaire" />
                                <asp:BoundField DataField="ProduitAffaireDate" HeaderText="Date" />
                                <asp:BoundField DataField="ProduitAffaireLibelle" HeaderText="Libellé" />
                                <asp:BoundField DataField="ProduitAffaireMntUnitHT" HeaderText="Valeur" />
                                <asp:BoundField DataField="ProduitAffaireQte" HeaderText="Quantité" />
                                <asp:BoundField DataField="ServiceLibelle" HeaderText="Service" />
                            </Columns>
                        </asp:GridView>
                    </td>
</tr>
<tr>
                    <td align="right">
                        <asp:Button ID="btnQuitterDetailsAffaire" runat="server" Text="Quitter" CssClass="btn90" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
