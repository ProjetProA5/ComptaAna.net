<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireSousAffaireListe.aspx.vb" Inherits="ComptaAna.net.AffaireSousAffaireListe" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">

  <asp:Menu ID="mMenuAffaireModif" runat="server" Orientation="Horizontal" >
            <StaticMenuStyle CssClass="SimpleStaticMenu"/>
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover"/>
            <Items>
                <asp:MenuItem Text="»" Enabled="false" />
                <asp:MenuItem Value="0" Text="Détails" />
                <asp:MenuItem Value="1" Text="Produit"  />
                <asp:MenuItem Value="2" Text="Facturation" />
                <asp:MenuItem Value="3" Text="Liste des sous-affaire"  />
                 </Items>
        </asp:Menu>
    <div class="content" >
    <h1>Liste des sous-affaire</h1>

    <asp:Button ID="btnNouveau" runat="server" Text="Nouvelle" CssClass="btn90" ToolTip="Créer une nouvelle sous-affaire"/>

    <fieldset >
        <legend><asp:Label ID="lblSousAffaire" runat="server" Font-Bold="true" ForeColor="Gray" /></legend>


            <asp:GridView AutoGenerateColumns="False" ID="gvListeSousAffaire" runat="server" EmptyDataText="Pas de données"
                        ShowHeaderWhenEmpty="true" CssClass="Grid" OnRowCommand="gvListeSousAffaire_RowCommand">
                <Columns>
                    <asp:BoundField DataField="AffaireID" HeaderText="AffaireID" Visible="false" />
                    <asp:BoundField DataField="AffaireLibelle" HeaderText="Libelle sous-affaire" />
                    <asp:BoundField DataField="AffaireBudget" HeaderText="Budget sous-affaire" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Modifier" runat="server" align="center"
                        CommandName="ModifierSousAffaire" 
                        CommandArgument ='<%# Eval("AffaireID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_modifier.png"/>

                        <asp:ImageButton ID="Supprimer" runat="server" align="center"
                        CommandName="SupprimerSousAffaire" 
                        CommandArgument ='<%# Eval("AffaireID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cette sous affaire ?');"/>
                    </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </fieldset>
    </div>

      <table>
       <tr>
       <td>
         <asp:Button ID="btnRetourFiche" runat="server" Text="Retour à la fiche" CssClass="btn95" Tooltip="Retour à la fiche de l'affaire"/>
       </td>
        <td>
<asp:Button ID="btnRetourListe" runat="server" Text="Retour à la liste" CssClass="btn95" ToolTip=" Retour à la liste des affaires"/>
        </td>
         
       </tr>
       </table>

</asp:Content>