<%@ Page AutoEventWireup="false" CodeBehind="Graphiques.aspx.vb" Inherits="ComptaAna.net.Graphiques"
    Title="Graphiques" Language="vb" MasterPageFile="~/SiteMaster.Master" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content" border="1px">
        <h1 class="h1bis">
            Graphiques</h1>
        <h2 class="legende2">
            Recherche</h2>
        <fieldset class="recherche2">
            <div align="center">
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
                        <td align="right">
                            <asp:Button ID="btnCalcul" runat="server" CssClass="btn90" Text="Calcul" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset id="FieldsetTauxFactu" visible="False" runat="server" class="graph">
            <legend>Taux de Facturation</legend>
            <div align="left">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset id="Fieldset1" visible="True" class="graphField">
                                <asp:Button ID="cbbTauxfact" runat="server" CssClass="btn90" Text="TauxFactu" />
                                <br />
                                <oboutComboBox:ComboBox ID="cbbFiltreStatGalDetail" runat="server" Style="top: 0px;
                                    left: 0px" Width="200px" EnableLoadOnDemand="True" />
                            </fieldset>
                        </td>
                        <td>
                            <asp:Label ID="LabTauxFactu" runat="server"></asp:Label>
                            <%-- OnItemDataBound="rcPanelGraph_ItemDataBound"--%>
                            <telerik:RadChart ID="RadChart1" runat="server" Width="500" Visible="true" SeriesOrientation="Horizontal"
                                AutoLayout="True" AutoTextWrap="false" EnableHandlerDetection="false">
                                <Legend Visible="false">
                                    <Appearance Position-Auto="True">
                                    </Appearance>
                                </Legend>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid" 
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Nom" AutoShrink="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="TauxFactu">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <ChartTitle>
                                    <TextBlock Text="Taux Facturation Employe">
                                    </TextBlock>
                                </ChartTitle>
                            </telerik:RadChart>
                        </td>
                            <td>
                            <asp:Label ID="LabTauxFactuMoinUn" runat="server"></asp:Label>
                            <%-- OnItemDataBound="rcPanelGraph_ItemDataBound"--%>
                            <telerik:RadChart ID="rcTauxFactuMoinUn" runat="server" Width="500" Visible="False" SeriesOrientation="Horizontal"
                                AutoLayout="True" AutoTextWrap="false" EnableHandlerDetection="false">
                                <Legend Visible="false">
                                    <Appearance Position-Auto="True">
                                    </Appearance>
                                </Legend>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid" 
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Nom" AutoShrink="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="TauxFactu">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <ChartTitle>
                                    <TextBlock Text="Taux Facturation Employe N-1">
                                    </TextBlock>
                                </ChartTitle>
                            </telerik:RadChart>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset id="FieldsetCAParService" visible="False" runat="server" class="graph">
            <legend>Chiffre d'Affaire par service</legend>
            <div align="left">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset id="Fieldset3" visible="True" class="graphField">
                                <asp:Button ID="btnCABU" runat="server" CssClass="btn90" Text="CA par BU" />
                                <br />
                                <%--ID="RadioBoutonCAService" runat="server"--%>
                                <asp:CheckBoxList ID="chkbCAParService" runat="server" SelectionMode="Multiple">
                                </asp:CheckBoxList>
                                <br />
                                <asp:Button ID="btnRenitialiser" runat="server" CssClass="btn90" Text="tout cocher" />
                            </fieldset>
                            <fieldset id="FieldOption" visible="True" class="graphField">
                                <asp:Button ID="btnCAService" runat="server" CssClass="btn90" Text="BAR/PIE" />
                                <br />
                                <asp:CheckBox ID="cbPourcentageCAService" runat="server" Text="Graphique en pourcentage" />
                            </fieldset>
                        </td>
                        <td>
                            <asp:Label ID="lblServiceInsuffisant" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcPanelGraphe" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="600" OnItemDataBound="rcPanelGraphe_ItemDataBound">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="Chiffre d'Affaire par service"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="true" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Objet" AutoShrink="True" AutoScale="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Pie" DataYColumn="CAHT">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <Series>
                                    <telerik:ChartSeries Type="Pie" DataYColumn="Objectif">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                         <td>
                            <asp:Label ID="LabNMoinsUn" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcCAServiceNMoinsUn" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="600" OnItemDataBound="rcPanelGrapheNMoinsUn_ItemDataBound" Visible="false">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="Chiffre d'Affaire par service N-1"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="true" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Objet" AutoShrink="True" AutoScale="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Pie" DataYColumn="CAHT">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <Series>
                                    <telerik:ChartSeries Type="Pie" DataYColumn="Objectif">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset id="FieldsetCAEmploye" visible="False" runat="server" class="graph">
            <legend>Chiffre d'Affaire par Employe</legend>
            <div align="left">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset id="Fieldset8" visible="True" class="graphField">
                                <asp:Button ID="btnCAEmploye" runat="server" CssClass="btn95" Text="CA par Employe" />
                                <br />
                                <%--ID="RadioBoutonCAService" runat="server"--%>
                                <asp:CheckBoxList ID="CheckBoxListCAEmploye" runat="server" SelectionMode="Multiple">
                                </asp:CheckBoxList>
                                <br />
                                <asp:Button ID="btnCheckBoxCAEmploye" runat="server" CssClass="btn90" Text="tout cocher" />
                            </fieldset>
                            <fieldset id="Fieldset9" visible="True" class="graphField">
                                <br />
                                <asp:CheckBox ID="cbPourcentageCAParEmploye" runat="server" Text="Graphique en pourcentage" />
                            </fieldset>
                        </td>
                        <td>
                            <asp:Label ID="LabelCAEmploye" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcCAEmploye" runat="server" Width="550" Visible="true" SeriesOrientation="Horizontal"
                                AutoLayout="True" AutoTextWrap="false" Skin="Web20" EnableHandlerDetection="false">
                                <Legend Visible="false">
                                    <Appearance Position-Auto="True">
                                    </Appearance>
                                </Legend>
                                <PlotArea>
                                    <XAxis DataLabelsColumn="Nom" AutoShrink="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="CA">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <ChartTitle>
                                    <TextBlock Text="Le Chiffre d'Affaire par employé">
                                    </TextBlock>
                                </ChartTitle>
                            </telerik:RadChart>
                        </td>
                        <td>
                            <asp:Label ID="LabCAEmployeNMoinsUn" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcCAEmployeNMoinsUn" runat="server" Width="550" Visible="False" SeriesOrientation="Horizontal"
                                AutoLayout="True" AutoTextWrap="false" Skin="Web20" EnableHandlerDetection="false">
                                <Legend Visible="false">
                                    <Appearance Position-Auto="True">
                                    </Appearance>
                                </Legend>
                                <PlotArea>
                                    <XAxis DataLabelsColumn="Nom" AutoShrink="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="CA">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <ChartTitle>
                                    <TextBlock Text="Le Chiffre d'Affaire par employé N-1">
                                    </TextBlock>
                                </ChartTitle>
                            </telerik:RadChart>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset id="FieldsetMasseSalarialeParService" visible="False" runat="server" class="graph">
            <legend>Masse salariale par service</legend>
            <div align="left">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset id="Fieldset5" visible="True" class="graphField">
                                <asp:Button ID="btnMSBU" runat="server" CssClass="btn90" Text="MSU" />
                                <br />
                                <oboutComboBox:ComboBox ID="cbbFiltreStatEmployeService" runat="server" EnableLoadOnDemand="True"
                                    Width="200px"/>
                            </fieldset>
                            <fieldset id="FieldOption2" visible="True" class="graphField">
                                <asp:Button ID="btnBarPie" runat="server" CssClass="btn90" Text="BAR/PIE" />
                                <br />
                                <asp:CheckBox ID="cbMasseSalariale" runat="server" Text="Graphique en pourcentage" />
                            </fieldset>
                        </td>
                        <td>
                            <asp:Label ID="LabMasseSalariale" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcPanelGraphe3" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="500" OnItemDataBound="rcPanelGraph_ItemDataBound">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="Masse salariale par service"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Service">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="pie" DataYColumn="Sum">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                        <td>
                            <asp:Label ID="LabMasseSalarialeNMoinsUn" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcMasseSalarialeNMoinsUn" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="500" OnItemDataBound="rcPanelGraph_ItemDataBound" Visible="false">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="Masse salariale par service N-1"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Service">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="pie" DataYColumn="Sum">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset id="FieldEBE" visible="False" runat="server" class="graph">
            <legend>EBE par service</legend>
            <div align="left">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset id="Fieldset10" visible="True" class="graphField">
                                <asp:Button ID="btnEBE" runat="server" CssClass="btn90" Text="EBE" />
                                <br />
                            </fieldset>
                            <fieldset id="Fieldset6" visible="True" class="graphField">
                                <asp:Button ID="btnBarPieEBEService" runat="server" CssClass="btn90" Text="BAR/PIE" Visible="false" />
                                <br />
                                <asp:CheckBox ID="cbEBEService" runat="server" Text="Graphique en pourcentage" />
                            </fieldset>
                        </td>
                        <td>
                            <asp:Label ID="LabRexGraphe" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcRexGraphe" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="560" OnItemDataBound="rcRexGraphe_ItemDataBound">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="EBE par service"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Service"  >
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="Rex">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                        <td>
                            <asp:Label ID="LabRexGrapheNMoinsUn" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcRexGrapheNMoinsUn" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="560" OnItemDataBound="rcRexGraphe_ItemDataBound" Visible="false">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="EBE par service N-1"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Service">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="Rex">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset id="FieldOutils" visible="False" runat="server" class="graph">
            <legend>Outils par service</legend>
            <div align="left">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset id="Fieldset4" visible="True" class="graphField">
                                <asp:Button ID="btnOutils" runat="server" CssClass="btn90" Text="Outils" />
                                <br />
                            </fieldset>
                            <fieldset id="Fieldset7" visible="True" class="graphField">
                                <asp:Button ID="btnBarPieOutilsService" runat="server" CssClass="btn90" Text="BAR/PIE" />
                                <br />
                                  <asp:CheckBox ID="cbOutils" runat="server" Text="Graphique en pourcentage" />
                            </fieldset>
                        </td>
                        <td>
                            <asp:Label ID="LabOutilsGraphe" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcOutilsGraphe" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="500" OnItemDataBound="rcOutilsGraphe_ItemDataBound">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="Outils par service"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Service">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Pie" DataYColumn="Outils">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                        <td>
                            <asp:Label ID="LabOutilsGrapheNMoinsUN" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcOutilsGrapheNMoinsUn" runat="server" AutoLayout="false" AutoTextWrap="false"
                                EnableHandlerDetection="false" Width="500" OnItemDataBound="rcOutilsGraphe_ItemDataBound" Visible="false">
                                <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="Outils par service N-1"
                                    Appearance-Position-AlignedPosition="Top">
                                </ChartTitle>
                                <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                                    Appearance-FillStyle-MainColor="#ffffff">
                                    <XAxis DataLabelsColumn="Service">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Pie" DataYColumn="Outils">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                            </telerik:RadChart>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset id="FieldsetFormation" visible="False" runat="server" class="graph">
            <legend>Montant de la Formation Prévu par employé</legend>
            <div align="left">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset id="Fieldset11" visible="True" class="graphField">
                                <asp:Button ID="btnFormation" runat="server" CssClass="btn90" Text="Formation" />
                                <br />
                            </fieldset>
                            <fieldset id="Fieldset12" visible="True" class="graphField">
                                <br />
                                <asp:CheckBox ID="cbPourcentageFormation" runat="server" Text="Graphique en pourcentage" />
                            </fieldset>
                        </td>
                        <td>
                            <asp:Label ID="LabelFormation" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcFormationGraphe" runat="server" Width="500" Visible="true"
                                SeriesOrientation="Horizontal" AutoLayout="True" AutoTextWrap="false" Skin="Web20"
                                EnableHandlerDetection="false">
                                <Legend Visible="false">
                                    <Appearance Position-Auto="True">
                                    </Appearance>
                                </Legend>
                                <PlotArea>
                                    <XAxis DataLabelsColumn="Nom" AutoScale="true" AutoShrink="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="Cout">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <ChartTitle>
                                    <TextBlock Text="Le Montant de la Formation Prévu par employé">
                                    </TextBlock>
                                </ChartTitle>
                            </telerik:RadChart>
                        </td>
                         <td>
                            <asp:Label ID="LabFormationNMoinsUn" runat="server"></asp:Label>
                            <telerik:RadChart ID="rcFormationGrapheNMoinsUn" runat="server" Width="500" Visible="False"
                                SeriesOrientation="Horizontal" AutoLayout="True" AutoTextWrap="false" Skin="Web20"
                                EnableHandlerDetection="false">
                                <Legend Visible="false">
                                    <Appearance Position-Auto="True">
                                    </Appearance>
                                </Legend>
                                <PlotArea>
                                    <XAxis DataLabelsColumn="Nom" AutoScale="true" AutoShrink="true">
                                    </XAxis>
                                </PlotArea>
                                <Series>
                                    <telerik:ChartSeries Type="Bar" DataYColumn="Cout">
                                        <Appearance LegendDisplayMode="ItemLabels">
                                            <TextAppearance TextProperties-Color="#6788be">
                                            </TextAppearance>
                                        </Appearance>
                                    </telerik:ChartSeries>
                                </Series>
                                <ChartTitle>
                                    <TextBlock Text="Le Montant de la Formation Prévu par employé N-1">
                                    </TextBlock>
                                </ChartTitle>
                            </telerik:RadChart>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
