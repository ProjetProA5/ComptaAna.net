<%@ Page Title="Statistiques Primes et Augmentations" Language="vb" AutoEventWireup="false" CodeBehind="StatPrimesAugmentations.aspx.vb" Inherits="ComptaAna.net.StatPrimesAugmentations" MasterPageFile="~/SiteMaster.Master"%>

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
            Statistiques Primes et Augmentations</h1>
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
                                                    <td>
                                                    <asp:Button ID="btnEXportPrimeAugmentation" runat="server" CssClass="btn225" Text="Export Prime et Augmentation" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
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
              <table cellspacing="40px">
                <tr>
                <td valign="top">
                      <h1 id="lblPrime" runat="server" visible="false" class="h1">PRIMES:</h1>
                          <br />
                <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvPrime"
                    EmptyDataText="Pas de données" CssClass="Grid" runat="server" Visible="false" AlternatingRowStyle-BackColor="#E5F3FF">
                    <Columns>
                        <asp:BoundField DataField="EmployePrimeID" HeaderText="EmployePrimeID" SortExpression="FormationID"
                            Visible="false" />
                        <asp:BoundField DataField="EmployeID" HeaderText="EmployeID"  Visible="false" ></asp:BoundField>
                         <asp:BoundField DataField="Employe" HeaderText="Employé"  ></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeDate" HeaderText="Date"></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeMontant" HeaderText="Montant"></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeTypeAvNature" HeaderText="Avantage en nature"></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeModeleAvNature" HeaderText="Modèle voiture"></asp:BoundField>
                        <%--<asp:TemplateField>
                          <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" runat="server" CommandName="SupprimerPrime"
                            CommandArgument ='<%# Eval("EmployePrimeID") %>'
                                ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                                OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer cette primer');"
                                ToolTip="Supprimer prime" />
                        </ItemTemplate>
                              </asp:TemplateField>--%>
                        </Columns>
                </asp:GridView>
                <br />
                    <asp:Label runat="server" ID="lblTotalPrimes" Visible="false" ForeColor="CadetBlue" Font-Bold="true" Text = "Total des primes: " />
    <asp:TextBox runat="server" ID="tbTotalPrimes" Font-Bold="true" Visible="false" Style="text-align: center" enabled="false"  ForeColor="DarkRed" Width="60px" />
                </td>
                <td valign="top">
                             <h1 id="lblAug" runat="server" visible="false" class="h1">AUGMENTATIONS:</h1>
                                 <br />
 <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvAugmentation"
                    EmptyDataText="Pas de données" CssClass="Grid" runat="server" Visible="false" AlternatingRowStyle-BackColor="#E5F3FF">
                    <Columns>
                        <asp:BoundField DataField="EmployeCoutID" HeaderText="EmployePrimeID" SortExpression="FormationID"
                            Visible="false" />
                        <asp:BoundField DataField="EmployeID" HeaderText="EmployeID"  Visible="false" ></asp:BoundField>
                        <asp:BoundField DataField="Employe" HeaderText="Employé"  ></asp:BoundField>
                        <asp:BoundField DataField="EmployeCoutDateDebut" HeaderText="Date"></asp:BoundField>
                        <asp:BoundField DataField="EmployeCoutCout" HeaderText="Montant"></asp:BoundField>
                        <asp:BoundField DataField="EmployeCoutTaux" HeaderText="Taux d'augmentation"></asp:BoundField>
                             </Columns>
                </asp:GridView>
                 <br />
                    <asp:Label runat="server" ID="lblTxMoyen" ForeColor="CadetBlue" Visible="false"  Font-Bold="true" Text = "Taux moyen d'augmentation: " />
    <asp:TextBox runat="server" ID="tbTxMoyen" Font-Bold="true" Visible="false"  Style="text-align: center" enabled="false"  ForeColor="DarkRed" Width="60px" />
                </td>
                </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="Div1" runat="server" align="right">
    </div>
    </asp:Panel> </div>
</asp:Content>

