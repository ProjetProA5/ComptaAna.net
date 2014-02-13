<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireInserer.aspx.vb" Inherits="ComptaAna.net.AffaireInserer" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .style1
        {
            width: 441px;
        }
        .Grid
        {
        }
        .style2
        {
            width: 207px;
        }
        .style3
        {
            width: 247px;
        }
        .style4
        {
            width: 93px;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        function SaveMontantHT() {
            montantHT = document.getElementById('<%= tbBudget.ClientID %>');
            floatmontantHT = parseFloat(montantHT.value.replace(/,/, "."));
        }


        function MontantSansFrais() {
            var budget = document.getElementById('<%= tbBudget.ClientID %>');
            var FraisInclus = document.getElementById('<%= tbMntFraisInclus.ClientID %>');

            if ((budget.value != "") && (FraisInclus.value != "")) {
                var floatBudget = parseFloat(budget.value.replace(/,/, "."));
                var floatFraisInclus = parseFloat(FraisInclus.value.replace(/,/, "."));

                var total = floatmontantHT - floatFraisInclus;

                budget.value = total;
            }
        }
        function CommentaireSansFrais() {
            var budget = document.getElementById('<%= tbBudget.ClientID %>');
            var FraisInclus = document.getElementById('<%= tbMntFraisInclus.ClientID %>');
            var com = document.getElementById('<%= tbCom.ClientID %>');

            if ((budget.value != "") && (FraisInclus.value != "")) {
                var floatBudget = parseFloat(budget.value.replace(/,/, "."));
                var floatFraisInclus = parseFloat(FraisInclus.value.replace(/,/, "."));
                //                alert("Commande de " + floatBudget + " euros dont " + floatFraisInclus + " euros de frais prévus")
                com.value = "Frais inclus: \nCommande de " + floatBudget + " euros \nMontant prévisionnel des frais : " + floatFraisInclus + " euros";
            }
        }


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
                var obj = document.getElementById('<%=btnValider.ClientID%>');
                obj.click();
            }
        }
   
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Insertion d'une affaire
        </h1>
        <%-- Informations generales--%>
        <fieldset class="AffaireForm">
            <legend>Informations Générales</legend>
            <table>
                <tr>
                    <td align="right">
                        Client* :
                    </td>
                    <td class="style3">
                        <oboutComboBox:ComboBox ID="cbClient" DataTextField="ClientNom" DataValueField="ClientID"
                            runat="server" Width="250px" Height="300px" />
                    </td>
                    <td align="right" class="style2">
                        Commande HT:
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="tbBudget" runat="server" Style="margin-left: 0px" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revtbBudget" ValidationGroup="vgInserer" runat="server"
                            Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbBudget"
                            ErrorMessage="Veuillez insérer un nombre">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblIdAff" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Frais inclus:
                    </td>
                    <td>
                        <asp:CheckBox ID="cbFraisInclus" AutoPostBack="true" runat="server" />
                    </td>
                    <td id="lblMntPrevi" runat="server" align="right" class="style2">
                        Montant prévisionnel de frais:
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="tbMntFraisInclus" Width="200px" onBlur="javascript: CommentaireSansFrais();"
                            runat="server" Style="margin-left: 0px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <asp:Label ID="lblInfoFrais" runat="server" ForeColor="CadetBlue" Display="Dynamic"
                            Font-Bold="true" Text="Ce montant sera automatiquement déduit du montant de la commande HT"
                            Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Libellé* :
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="tbLibelle" Width="350px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Début* :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbDate" Width="65px" />
                        <obout:Calendar ID="cCalendrier" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                            TextBoxId="tbDate" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                        </obout:Calendar>
                    </td>
                    <td align="right" class="style2">
                        Type Affaire* :
                    </td>
                    <td class="style3">
                        <oboutComboBox:ComboBox ID="cbTypeAffaire" Width="200px" runat="server" DataTextField="TypeAffaireLibelle"
                            DataValueField="TypeAffaireID">
                        </oboutComboBox:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Chargé d'affaire* :
                    </td>
                    <td class="style3">
                        <oboutComboBox:ComboBox ID="cbChargeAffaire" Width="250px" Height="300px" runat="server"
                            DataTextField="PrenomNom" DataValueField="EmployeID">
                        </oboutComboBox:ComboBox>
                    </td>
                    <td align="right" class="style2">
                        Service* :
                    </td>
                    <td class="style3">
                        <oboutComboBox:ComboBox ID="cbService" Width="200px" runat="server" DataTextField="ServiceLibelle"
                            DataValueField="ServiceID">
                        </oboutComboBox:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Commentaires:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="tbCom" TextMode="Multiline" Width="99%" Height="75px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Label ID="lblErreurInsertion" Visible="false" Enabled="false" runat="server"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnValider" ValidationGroup="vgInserer" runat="server" Text="Enregistrer"
                            CssClass="btn75" />
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
    </div>
    <div runat="server" id="divQualif" visible="false">
        <%-- Affichage Qualification--%>
        <fieldset class="QualificationFrom">
            <legend>Qualification</legend>
            <asp:GridView AutoGenerateColumns="False" ID="gvQualifAffaire" border="1" runat="server"
                EmptyDataText="Pas de données" CssClass="Grid" ShowHeaderWhenEmpty="true" Width="650">
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
                            <asp:TextBox ID="tbPrixTotal" Enabled="false" runat="server" Text='<%# Bind("total") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbPrixTotal" Enabled="false" runat="server" Text='<%# Eval("total") %>' />
                        </EditItemTemplate>
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
            <asp:Table ID="tableQualif" runat="server" Width="624px">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="listeQualif" Width="160px" runat="server" DataTextField="Libelle"
                            DataValueField="ID" OnSelectedIndexChanged="listeQualif_changed" AutoPostBack="true">
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbPrixHT" Width="140px" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="tbNbJours" Width="145px" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="btnAjouter" runat="server" ValidationGroup="vgQualif" Enabled="false"
                            Text="Enregistrer" CssClass="btn75" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <table>
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
            <asp:Label ID="lblerror" Visible="false" Width="350px" Enabled="false" runat="server"
                ForeColor="Red" Font-Bold="true"></asp:Label>
        </fieldset>
    </div>
    <div runat="server" id="divEtapFactu" visible="false">
        <fieldset id="fs4" visible="True" runat="server">
            <legend>Etapes de Facturation</legend>
            <div runat="server" id="divChoixFactu" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Choix du mode de facturation:" Font-Underline="true"
                                Font-Bold="true" />
                            <asp:RadioButtonList ID="rbChoixFactu" runat="server">
                                <asp:ListItem>0% - 100%</asp:ListItem>
                                <asp:ListItem>0% - 50% - 50%</asp:ListItem>
                                <asp:ListItem>0% - 10% - 30% - 60%</asp:ListItem>
                                <asp:ListItem>0% - 30% - 70%</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
                            <asp:Button ID="btnSaveEtapeFactu" runat="server" Text="Enregistrer" CssClass="btn75" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:GridView AutoGenerateColumns="False" ID="gvEtapesFactuPourcentageInsertion"
                border="1" runat="server" ShowHeaderWhenEmpty="true" Width="505px" CssClass="Grid"
                EmptyDataText="Pas de données">
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
                        <asp:Label ID="lblMois7" align="center" runat="server" /></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Février</asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="cbMois2" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois2" align="center" runat="server" /></asp:TableCell><asp:TableCell>Août</asp:TableCell><asp:TableCell>
                            <asp:CheckBox ID="cbMois8" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois8" align="center" runat="server" /></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Mars</asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="cbMois3" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois3" align="center" runat="server" /></asp:TableCell><asp:TableCell>Septembre</asp:TableCell><asp:TableCell>
                            <asp:CheckBox ID="cbMois9" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois9" align="center" runat="server" /></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Avril</asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="cbMois4" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois4" align="center" runat="server" /></asp:TableCell><asp:TableCell>Octobre</asp:TableCell><asp:TableCell>
                            <asp:CheckBox ID="cbMois10" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois10" align="center" runat="server" /></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Mai</asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="cbMois5" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois5" align="center" runat="server" />
                    </asp:TableCell><asp:TableCell>Novembre</asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="cbMois11" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois11" align="center" runat="server" /></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Juin</asp:TableCell><asp:TableCell>
                        <asp:CheckBox ID="cbMois6" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois6" align="center" runat="server" /></asp:TableCell><asp:TableCell>Décembre</asp:TableCell><asp:TableCell>
                            <asp:CheckBox ID="cbMois12" align="center" runat="server" Enabled="false" /></asp:TableCell>
                    <asp:TableCell Visible="False">
                        <asp:Label ID="lblMois12" align="center" runat="server" />
                    </asp:TableCell></asp:TableRow>
            </asp:Table>
        </fieldset>
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnRetourFiche" runat="server" Visible="false" Text="Retour à la fiche"
                                CssClass="btn90" ToolTip="Retour à la fiche de l'affaire" />
                        </td>
                        <td>
                            <asp:Button ID="btnRetourListe" runat="server" Text="Retour à la liste" CssClass="btn90"
                                ToolTip=" Retour à la liste des affaires" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
</asp:Content>
