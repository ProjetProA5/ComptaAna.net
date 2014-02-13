<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="Formation.aspx.vb" Inherits="ComptaAna.net.Formation" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
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
            Formation de l'employé</h1>
        <asp:Menu ID="mOnglets" runat="server" Orientation="Horizontal">
            <StaticMenuStyle CssClass="SimpleStaticMenu" />
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover" />
            <Items>
                <asp:MenuItem Value="1" Text="Détails" />
                <asp:MenuItem Value="2" Text="Coût" />
                <asp:MenuItem Value="3" Text="Droits" />
                <asp:MenuItem Value="4" Text="Formation" Selected="true" />
                <asp:MenuItem Value="5" Text="Echéances annuelles" />
            </Items>
        </asp:Menu>
        <div style="float: left; width: 100%; overflow: auto; margin-top: 0px;">
            <br />
            <asp:Button ID="btnInsererFormation" runat="server" CssClass="btn225" Text="Prévoir une nouvelle formation" />
            <fieldset id="fsRecherche" runat="server" visible="true">
                <legend>Champs de recherche</legend>
                <table style="margin-top: 0px; border-spacing: 10px; border: 20px">
                    <tr>
                        <td class="style18" valign="top">
                            <asp:Label ID="lblType" runat="server" Text="Type de formations:" Font-Underline="true"
                                Font-Bold="true" />
                        </td>
                        <td valign="top">
                            <oboutComboBox:ComboBox ID="cbbTypeFormation" runat="server" Width="200px">
                                <oboutComboBox:ComboBoxItem ID="cbbTtesFormations" Selected="true" Value="Toutes"
                                    runat="server" Text="Toutes les formations" />
                                <oboutComboBox:ComboBoxItem ID="cbbFormationSuivieR" Value="Suivie realisee" runat="server"
                                    Text="Formation suivie réalisée" />
                                <oboutComboBox:ComboBoxItem ID="cbbFormationDispenseeR" Value="Dispensee realisee"
                                    runat="server" Text="Formation dispensée réalisée" />
                                <oboutComboBox:ComboBoxItem ID="cbbFormationPrevue" Value="Prevue" runat="server"
                                    Text="Formation prevue" />
                            </oboutComboBox:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td class="style18">
                                        <asp:Label ID="Label1" Font-Underline="true" runat="server" Font-Bold="true">Recherche sur une période:</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateDebut" runat="server" Text="Date de debut :" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tbDateDeb" Width="152px" />
                                        <obout:Calendar ID="cDateDeb" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                            TextBoxId="tbDateDeb" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDateFin" runat="server" Text="Date de fin :" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tbDateFin" Width="152px" />
                                        <obout:Calendar ID="cDateFin" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                            TextBoxId="tbDateFin" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="style18" align="right">
                                        <asp:ImageButton ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png" ID="ibValider"
                                            runat="server" ToolTip="Rechercher" CommandArgument='<%# Eval("ClientID") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="style18" align="right">
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbDateDeb"
                                            ControlToCompare="tbDateFin" Operator="LessThanEqual" Type="Date" Display="Dynamic"
                                            ErrorMessage="La période n'est pas valide<br/>" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset id="fsNouvelle" runat="server" visible="true">
                <legend>Nouvelle formation en prévision</legend>
                <table style="margin-top: 0px; border-spacing: 10px; border: 20px">
                    <tr>
                        <td class="style18" valign="top">
                            <asp:Label ID="lblLibelle" runat="server" Text="Libellé * : " />
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbLibelle" Width="400" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style18" valign="top">
                            <asp:Label ID="lblOrganisme" runat="server" Text="Organisme * : " />
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbOrganisme" Width="200px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style18" valign="top">
                            <asp:Label ID="lblNbHeures" runat="server" Text="Nombre d'heures * : " />
                        </td>
                        <td>
                            <asp:TextBox ID="tbNbHeures" Width="50px" runat="server" />
                        </td>
                        <td class="style18" valign="top">
                            <asp:Label ID="lblMontant" runat="server" Text="Montant de la formation * : " />
                        </td>
                        <td>
                            <asp:TextBox ID="tbMontant" Width="50px" runat="server" />
                        </td>
                        <td colspan="2" class="style18">
                            <asp:RegularExpressionValidator ID="reNbHeures" ValidationGroup="vgInserer" runat="server"
                                Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbNbHeures"
                                ErrorMessage="Veuillez insérer un nombre correcte d'heures">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style18" valign="top">
                            <asp:Label ID="lblDateDebutPer" runat="server" Text="Date de début * : " />
                        </td>
                        <td>
                            <asp:TextBox ID="tbDateDebutPer" Width="100px" runat="server" />
                            <obout:Calendar ID="CalendarDateDebutPer" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                TextBoxId="tbDateDebutPer" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style18" valign="top">
                            <asp:Label ID="lblDateFinPer" runat="server" Text="Date de fin * : " />
                        </td>
                        <td>
                            <asp:TextBox ID="tbDateFinPer" Width="100px" runat="server" />
                            <obout:Calendar ID="CalendarDateFinPer" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                TextBoxId="tbDateFinPer" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblChampsObli" runat="server" Text="Tous les champs munis d'une * sont obligatoires"
                                Font-Italic="true" ForeColor="LightGray" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErreur" runat="server" Visible="false" Text="" Font-Bold="true"
                                ForeColor="red" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvLibelle" ValidationGroup="vgInserer" runat="server"
                                Display="Dynamic" ControlToValidate="tbLibelle" ErrorMessage="Le libellé est obligatoire">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvOrganisme" ValidationGroup="vgInserer" runat="server"
                                Display="Dynamic" ControlToValidate="tbOrganisme" ErrorMessage="L'organisme est obligatoire">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvNbHeures" ValidationGroup="vgInserer" runat="server"
                                Display="Dynamic" ControlToValidate="tbNbHeures" ErrorMessage="Le nombre d'heure est obligatoire">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvDateDebPer" ValidationGroup="vgInserer" runat="server"
                                Display="Dynamic" ControlToValidate="tbDateDebutPer" ErrorMessage="La date de début est obligatoire">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvDateFinPer" ValidationGroup="vgInserer" runat="server"
                                Display="Dynamic" ControlToValidate="tbDateFinPer" ErrorMessage="La date de fin est obligatoire">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="rfvMontant" ValidationGroup="vgInserer" runat="server"
                                Display="Dynamic" ControlToValidate="tbMontant" ErrorMessage="Le montant est obligatoire">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btEnregistrer" ValidationGroup="vgInserer" CssClass="btn90" runat="server"
                                Text="Enregistrer" CommandArgument='<%# Eval("ClientID") %>' />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset id="fsGridView" runat="server" visible="True">
                <legend>Visualisation des formations de tout les employés </legend>
                <table cellspacing="10px">
                    <tr>
                        <td>
                            <asp:Label ID="LabGridViewGrph" runat="server" Font-Bold="true" Text="Année :" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAnneeGridView" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button CssClass="btn95" ID="btnGridview" runat="server" Text="Visualisation" />
                        </td>
                    </tr>
                </table>
                <div align="center">
                    <%--                                         test                                             --%>
                    <%-- <asp:Panel ID="pVisualisation" runat="server" Visible="False" CssClass="modalPopup" >
                 <asp:GridView AutoGenerateColumns="True" runat="server" CssClass="Grid" ID="gvPopup"
                    EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#EDEDED">
                    <Columns>

                   </Columns>
               </asp:GridView>
                <asp:Button CssClass="btn95" ID="btnvisualisation" runat="server" Text="Quitter" />
                </asp:Panel>--%>
                    <%--                                         test                                             --%>
                    <%--<asp:GridView AutoGenerateColumns="False" runat="server" CssClass="Grid" ID="gvTest"  OnRowUpdating="gvFormation_RowUpdating" 
                    EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#EDEDED" AutoGenerateEditButton="true" >
                    <Columns>
                        <asp:BoundField DataField="Nom" HeaderText="Nom" />
                        
                        <asp:BoundField DataField="Fevrier" HeaderText="Fevrier" />

                         <%--<asp:CommandField ButtonType="Image" ItemStyle-HorizontalAlign="Center" ShowDeleteButton="false" ShowEditButton="true" 
                         ShowSelectButton="False"
                        HeaderText="" DeleteImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        EditImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" UpdateImageUrl="~/App_Themes/ComptaAna/Design/Icon_tick.png"
                        CancelImageUrl="~/App_Themes/ComptaAna/Design/Icon_cross.png" />


                    </Columns>
                </asp:GridView >--%>
                    <asp:GridView AutoGenerateColumns="False" runat="server" CssClass="Grid" ID="gvGrapheFormation"
                        EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#EDEDED">
                        <Columns>
                            <asp:BoundField DataField="Nom" HeaderText="Nom" />
                            <%-- <asp:BoundField  />--%>
                            <asp:BoundField DataField="Janvier" HeaderText="Janvier" HtmlEncode="false" />
                            <asp:BoundField DataField="Fevrier" HeaderText="Fevrier" HtmlEncode="false" />
                            <asp:BoundField DataField="Mars" HeaderText="Mars" HtmlEncode="false" />
                            <asp:BoundField DataField="Avril" HeaderText="Avril" HtmlEncode="false" />
                            <asp:BoundField DataField="Mai" HeaderText="Mai" HtmlEncode="false" />
                            <asp:BoundField DataField="Juin" HeaderText="Juin" HtmlEncode="false" />
                            <asp:BoundField DataField="Juillet" HeaderText="Juillet" HtmlEncode="false" />
                            <asp:BoundField DataField="Aout" HeaderText="Août" HtmlEncode="false" />
                            <asp:BoundField DataField="Septembre" HeaderText="Septembre" HtmlEncode="false" />
                            <asp:BoundField DataField="Octobre" HeaderText="Octobre" HtmlEncode="false" />
                            <asp:BoundField DataField="Novembre" HeaderText="Novembre" HtmlEncode="false" />
                            <asp:BoundField DataField="Decembre" HeaderText="Décembre" HtmlEncode="false" />
                        </Columns>
                    </asp:GridView>
                    <%--DefaultType="Gantt"  Item   OnDataBound="rcEcheanceAnnuelle_ItemDataBound"--%>
                    <asp:Label ID="LabrcFormation" runat="server"></asp:Label>
                    <telerik:RadChart ID="rcFormation" runat='server' SeriesOrientation="Horizontal"
                        AutoLayout="true" Width="800
            px" Visible="false">
                        <Series>
                            <telerik:ChartSeries Name="DateDebString" DataYColumn="DateDeb" DataYColumn2="DateFin"
                                Type="Gantt">
                                <Appearance LegendDisplayMode="ItemLabels">
                                    <TextAppearance TextProperties-Color="#000000">
                                    </TextAppearance>
                                </Appearance>
                            </telerik:ChartSeries>
                        </Series>
                        <PlotArea Appearance-FillStyle-MainColor="#FFFFFF" Appearance-FillStyle-SecondColor="#FFFFFF">
                            <XAxis DataLabelsColumn="Nom" Appearance-MajorTick-Color="Red" Appearance-MajorGridLines-Visible="false">
                                <AxisLabel TextBlock-Text="Employe" Visible="true">
                                </AxisLabel>
                            </XAxis>
                            <YAxis IsZeroBased="false" Appearance-ValueFormat="ShortDate" Appearance-MajorTick-Color="Red"
                                Appearance-LabelAppearance-RotationAngle="45" Appearance-MajorGridLines-Width="3"
                                Appearance-MajorGridLines-PenStyle="Solid">
                                <AxisLabel TextBlock-Text="Date" Visible="true">
                                </AxisLabel>
                            </YAxis>
                        </PlotArea>
                        <Legend Visible="false">
                            <Appearance Visible="False">
                            </Appearance>
                        </Legend>
                        <ChartTitle>
                            <TextBlock Text="Formation Prévue">
                            </TextBlock>
                        </ChartTitle>
                    </telerik:RadChart>
                </div>
            </fieldset>
            <div align="center" style="float: none; width: 100%; overflow: auto; margin-top: 0px;">
                <asp:Label ID="lblConfirmation" runat="server" Visible="false" />
                <br />
                <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvFormation"
                    EmptyDataText="Pas de données" CssClass="Grid" runat="server" AlternatingRowStyle-BackColor="#E5F3FF">
                    <Columns>
                        <asp:BoundField DataField="FormationID" HeaderText="FormationID" SortExpression="FormationID"
                            Visible="false" />
                        <asp:BoundField DataField="FormationLibelle" HeaderText="Libellé"></asp:BoundField>
                        <asp:BoundField DataField="FormationNbHeure" HeaderText="Nombre d'heures"></asp:BoundField>
                        <asp:BoundField DataField="FormationType" HeaderText="Type"></asp:BoundField>
                        <asp:BoundField DataField="FormationOrganisme" HeaderText="Organisme"></asp:BoundField>
                        <asp:BoundField DataField="FormationNbParticipants" HeaderText="Nombre de participants">
                        </asp:BoundField>
                        <asp:BoundField DataField="FormationCout" HeaderText="Coût"></asp:BoundField>
                        <asp:BoundField DataField="FormationDateDeb" HeaderText="Date de début"></asp:BoundField>
                        <asp:BoundField DataField="FormationDateFin" HeaderText="Date de fin"></asp:BoundField>
                        <asp:BoundField DataField="FormationDate" HeaderText="Date"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--  <asp:ImageButton ID="Modifier" runat="server" align="center"
                        CommandName="modifierFormation" 
                        CommandArgument='<%# Eval("FormationID") %>'
                        ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" 
                        ToolTip="Modifier formation" />--%>
                                <asp:ImageButton ID="Supprimer" runat="server" align="center" CommandName="SupprimerFormation"
                                    CommandArgument='<%# Eval("FormationID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                                    OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cette formation ?');"
                                    ToolTip="Supprimer formation" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btRetourListeFormation" CssClass="btn95" runat="server" Text="Retour à la liste" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
