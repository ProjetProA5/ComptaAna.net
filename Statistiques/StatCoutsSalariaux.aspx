<%@ Page Title="Statistiques sur les coûts salariaux"  Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
CodeBehind="StatCoutsSalariaux.aspx.vb" Inherits="ComptaAna.net.StatCoutsSalariaux" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
<div class="content">
    <h1 class="h1bis">Statistiques sur les coûts salariaux</h1>
    <div class="content" align="center">
    <h2 class="legende2">Recherche</h2>
    <fieldset class="recherche2">
        <table>
            <tr>
                <td class="couleurTextRecherche">Année: </td>
                <td><oboutComboBox:ComboBox ID="cbbPeriodeAnnee" runat="server" /></td>
                <td class="couleurTextRecherche">Mois: </td>
                <td>
                    <oboutComboBox:ComboBox ID="cbbPeriodeMois" runat="server" SelectedIndex="0" Width="125px" >
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem0" Value="1" runat="server" Text="Janvier" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem1" Value="2" runat="server" Text="Févier" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem2" Value="3" runat="server" Text="Mars" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem3" Value="4" runat="server" Text="Avril" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem5" Value="5" runat="server" Text="Mai" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem6" Value="6" runat="server" Text="Juin" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem7" Value="7" runat="server" Text="Juillet" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem9" Value="8" runat="server" Text="Août" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem8" Value="9" runat="server" Text="Septembre" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem4" Value="10" runat="server" Text="Octobre" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem10" Value="11" runat="server" Text="Novembre" />
                        <oboutComboBox:ComboBoxItem ID="ComboBoxItem11" Value="12" runat="server" Text="Décembre" />
                    </oboutComboBox:ComboBox>
                </td>
                <td class="couleurTextRecherche">Employé: </td>
                <td><oboutComboBox:ComboBox ID="cbbEmployeActif" runat="server" SelectedIndex="0" Width="200px">
                        <oboutComboBox:ComboBoxItem ID="cbbTousActifs" Value="True" runat="server" Text="Tous les employés actifs" />
                        <oboutComboBox:ComboBoxItem ID="cbbTous" Value="False" runat="server" Text="Tous les employés" />
                    </oboutComboBox:ComboBox>
                </td>
                <td><asp:ImageButton ID="btnRechercheStat" runat="server" 
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_analyses.png"  
                        ValidationGroup="vgRechercher" ToolTip="Rechercher"/></td>
            </tr>
        </table>
    </fieldset>
    </div>
    <div align="center">
        <asp:GridView AutoGenerateColumns="False" ID="gvStatCoutSalaire" runat="server" CssClass="Grid" 
            EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#E5F3FF" />
    </div>

</div>
</asp:Content>