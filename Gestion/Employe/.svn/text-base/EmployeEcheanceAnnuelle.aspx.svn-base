<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmployeEcheanceAnnuelle.aspx.vb"
    MasterPageFile="~/SiteMaster.Master" Inherits="ComptaAna.net.EmployeEcheanceAnnuelle" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .btn90
        {
            height: 26px;
        }
        .btn95
        {}
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Echéances annuelles de l'employé</h1>
        <asp:Menu ID="mOnglets" runat="server" Orientation="Horizontal">
            <StaticMenuStyle CssClass="SimpleStaticMenu" />
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover" />
            <Items>
                <asp:MenuItem Value="1" Text="Détails" />
                <asp:MenuItem Value="2" Text="Coût" />
                <asp:MenuItem Value="3" Text="Droits" />
                <asp:MenuItem Value="4" Text="Formation" />
                <asp:MenuItem Value="5" Text="Echéances annuelles" Selected="true" />
            </Items>
        </asp:Menu>
        <br />
        <table cellspacing="20px">
            <tr>
                
                <td>
                    <asp:Button CssClass="btn95" ID="btnAjout" runat="server" Text="Ajouter" />
                </td>
            </tr>
        </table>
        
        <fieldset id="fsNouvelleEcheance" runat="server" visible="false">
        <legend> Nouvelle échéance annuelle </legend>
            <table cellspacing="20px">
            <tr>
        <td>
        <asp:Label ID="lblEcheance" runat="server" Font-Bold="true" Text="Type d'échéance : " />
        </td>
        <td>
        <asp:DropDownList ID="ddlEcheance" runat="server"> </asp:DropDownList>
        </td>
         <td align="right">
                                        <asp:Label ID="lblDateEcheance" runat="server" Text="Date de l'échéance :"
                                            Font-Bold="true" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbDate" runat="server" Width="80px" />
                                        <obout:Calendar ID="Calendar1" runat="server" CultureName="fr-FR" DateFormat="dd/MM/yyyy"
                                            DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif" DatePickerMode="true"
                                            ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" TextBoxId="tbDate" />
                                    </td>
               </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabEmploye" runat="server" Font-Bold="true" Text="Nom Employe : " />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmploye" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
        <tr>
        <td>
        <asp:Button CssClass="btn95" ID="btnEnregistrer" runat="server" Text="Enregistrer" 
                Height="26px"/>
        </td>
        <td>
        <asp:Label ID="LabErreurNouvelleEcheance" runat="server" Font-Bold="true" Text="" />
        </td>
       
        </tr>
            
            </table>
        </fieldset>
        <fieldset id="fsGridView" runat="server" visible="True">
            <legend> Visualisation global des employés </legend>
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
                <tr>
                    <td>
                        <asp:Label ID="LabEcheanceGridView" runat="server" Font-Bold="true" Text="Echeance :" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAnneeGridViewEcheance" runat="server">
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
                <asp:GridView AutoGenerateColumns="False" AutoGenerateField="False" runat="server" CssClass="Grid" ID="gvGrapheEcheance"
                    EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#EDEDED">
                    <Columns>
                        <asp:BoundField DataField="Nom" HeaderText="Nom" />
                        <%-- <asp:BoundField  />--%>
                       <%-- <asp:TemplateField HeaderText="Janvier">
                          <HeaderStyle CssClass="header" />--%>
                              <%--<ItemTemplate  >
                                <b DataField="Janvier">V1</b><br />
                                V2<br />
                                V3
                            </ItemTemplate>
                        </asp:TemplateField> --%>
                        <asp:BoundField DataField="Janvier" HeaderText="Janvier" HtmlEncode="False"  />
                        <asp:BoundField DataField="Fevrier" HeaderText="Fevrier" HtmlEncode="False" />
                        <asp:BoundField DataField="Mars" HeaderText="Mars" HtmlEncode="False" />
                        <asp:BoundField DataField="Avril" HeaderText="Avril" HtmlEncode="False" />
                        <asp:BoundField DataField="Mai" HeaderText="Mai" HtmlEncode="False" />
                        <asp:BoundField DataField="Juin" HeaderText="Juin" HtmlEncode="False" />
                        <asp:BoundField DataField="Juillet" HeaderText="Juillet" HtmlEncode="False" />
                        <asp:BoundField DataField="Aout" HeaderText="Août" HtmlEncode="False" />
                        <asp:BoundField DataField="Septembre" HeaderText="Septembre" HtmlEncode="False" />
                        <asp:BoundField DataField="Octobre" HeaderText="Octobre" HtmlEncode="False" />
                        <asp:BoundField DataField="Novembre" HeaderText="Novembre" HtmlEncode="False" />
                        <asp:BoundField DataField="Decembre" HeaderText="Décembre" HtmlEncode="False" />
                    </Columns>
                </asp:GridView>
                <%--DefaultType="Gantt"  Item   OnDataBound="rcEcheanceAnnuelle_ItemDataBound"--%>
                <asp:Label ID="LabrcEchenaceAnnuelle" runat="server"></asp:Label>
                <telerik:RadChart ID="rcEcheanceAnnuelle" runat='server' SeriesOrientation="Horizontal" 
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
                        <AxisLabel TextBlock-Text="Employe"  Visible="true"> </AxisLabel>
                        </XAxis>
                        <YAxis IsZeroBased="false" Appearance-ValueFormat="ShortDate" Appearance-MajorTick-Color="Red" Appearance-LabelAppearance-RotationAngle="45"
                            Appearance-MajorGridLines-Width="3" Appearance-MajorGridLines-PenStyle="Solid" >
                             <AxisLabel TextBlock-Text="Date"  Visible="true"> </AxisLabel>
                        </YAxis>
                        
                    </PlotArea>
                    <Legend Visible="false">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                    <ChartTitle>
                        <TextBlock Text="Echeances Annuelles">
                        </TextBlock>
                    </ChartTitle>
                </telerik:RadChart>
            </div>
        </fieldset>
        
        

        <fieldset id="fsEcheances" runat="server" visible="false">
        <legend> Echéances annuelles de l'employé </legend>
            <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblAnnee" runat="server" Text="Année : " Font-Bold="true" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAnnee" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button CssClass="btn95" ID="btnAfficherEcheance" runat="server" Text="Afficher" />
                        </td>
                    </tr>
                </table>
            <table>
                
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblErreur" runat="server" Text="Pas encore d'échéance" ForeColor="Red"
                                        Visible="false"></asp:Label>
                                    <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvEcheance"
                                        EmptyDataText="Pas de données" CssClass="Grid" runat="server" Visible="false"
                                        AlternatingRowStyle-BackColor="#E5F3FF">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeEcheanceAnnuelleID" HeaderText="EmployeEcheanceAnnuelleID"
                                                SortExpression="FormationID" Visible="false" />
                                            <asp:BoundField DataField="TypeEcheanceAnnuelleID" HeaderText="TypeEcheanceAnnuelleID"
                                                SortExpression="FormationID" Visible="false" />
                                            <asp:BoundField DataField="TypeEcheanceAnnuelleLibelle" HeaderText="Echéance"></asp:BoundField>
                                            <asp:BoundField DataField="EmployeEcheanceAnnuelleDate" HeaderText="Date"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BtnDelete" runat="server" CommandName="SupprimerEcheance" CommandArgument='<%# Eval("EmployeEcheanceAnnuelleID") %>'
                                                        ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png" OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer cette date');"
                                                        ToolTip="Supprimer prime" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btRetourListeEmploye" CssClass="btn95" runat="server" Text="Retour à la liste" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                            <table cellspacing="10px">
                                <tr>
                                    <td>
                                        <asp:Label ID="Lab" runat="server" Font-Bold="true" Text="" />
                                    </td>
                                </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
        </div>
        </asp:Content>
            
            
