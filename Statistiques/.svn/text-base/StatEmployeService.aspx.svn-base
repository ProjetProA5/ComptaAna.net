<%@ Page Title="Calcul de la rentabilité" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="StatEmployeService.aspx.vb" Inherits="ComptaAna.net.StatEmployeService" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1 class="h1bis">
            Calcul de la rentabilité</h1>
        <div class="content" align="center">
            <h2 class="legende2">
                Recherche</h2>
            <fieldset class="recherche2">
                <%-- Champs recherche --%>
                <table style="margin-top: 0px; border-spacing: 10px">
                    <tr>
                        <td valign="top">
                            <table style="margin-top: 0px; border-spacing: 10px">
                                <tr>
                                    <td>
                                        <asp:Label CssClass="couleurTextRecherche" ID="LabelTri" runat="server" Text="Période:"
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
                        <td valign="top">
                            <table style="margin-top: 0px; border-spacing: 10px">
                                <tr>
                                    <td>
                                        <asp:Label CssClass="couleurTextRecherche" ID="Label1" runat="server" Text="Restrictions:"
                                            Font-Underline="true" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <oboutComboBox:ComboBox ID="cbbFiltreStatEmployeService" runat="server" EnableLoadOnDemand="True"
                                            Width="200px" Height="300px" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnRechercheStat" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png"
                                            ValidationGroup="vgRechercher" ToolTip="Rechercher" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   
                </table>
                <%-- Message des validators --%>
                <table>
                     <tr>
                        <td>
                            <asp:Button ID="btnCalcRent" runat="server" CssClass="btn175" Text="Calcul et export du REX"
                                ValidationGroup="vgRechercher" />
                        </td>
                         <td>
                             <asp:Button ID="BtnExportBis" runat="server" CssClass="btn95" Text="Export du REX"
                                 ValidationGroup="vgRechercher" />
                         </td>
                        
                    </tr>
                </table>
            </fieldset>
        </div>
        <div align="center">
            <asp:RequiredFieldValidator ID="rfvTbDateDeb" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="tbDateDeb" ErrorMessage="Veuillez saisir une date de début." />
            <asp:RequiredFieldValidator ID="rfvDateFin" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="tbDateFin" ErrorMessage="Veuillez saisir une date de fin." />
            <asp:RequiredFieldValidator ID="rfvCbbFiltre1" ValidationGroup="vgRechercher" runat="server"
                Display="Dynamic" ControlToValidate="cbbFiltreStatEmployeService" ErrorMessage="Veuillez saisir une restriction." />
            <asp:GridView AutoGenerateColumns="False" ID="gvStatEmployeService" runat="server"
                CssClass="Grid" EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#E5F3FF" />
        </div>
    </div>
</asp:Content>
