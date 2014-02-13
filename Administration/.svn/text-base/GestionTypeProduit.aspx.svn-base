<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GestionTypeProduit.aspx.vb"
    Inherits="ComptaAna.net.GestionTypeProduit" MasterPageFile="~/SiteMaster.Master" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Gestion des Types de Produit
        </h1>
        <br />
        <fieldset>
            <legend>Nouveau type de produit</legend>
            <table>
                <tr>
                    <td>
                        Libellé
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="tbNouveauLibelle" runat="server" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator runat="server" ID="rfvLibelle" ControlToValidate="tbNouveauLibelle"
                            Display="Dynamic" ValidationGroup="vgProduit"    ErrorMessage="Un libellé est requis<br/>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Visible
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitVisible" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                        Associé aux affaires
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitAffaire" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Imputer au budget affaires
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitBudgetAffaire" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                        A la journée
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitJournee" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        CA employé
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitCA" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                        CA Axe
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitCAAxe" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Facturer par Qualif
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitFactQualif" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                        Refacturer en interne
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauTypeProduitFactInterne" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnAjouterNouveau" ValidationGroup="vgProduit" runat="server" Text="Enregister" CssClass="btn90" />
        </fieldset>
        <br />
        <br />
        <asp:Label runat="server" ID="lblMsgSupp" ForeColor="red" />
        </div>
        <div align="center">
        <asp:GridView ID="gvTypeProduit" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" OnRowCommand="gvTypeProduit_RowCommand"
            Width="843px" EmptyDataText="Pas de données" EnableViewState="False" CssClass="Grid" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Libellé">
                    <ItemTemplate>
                        <asp:TextBox ID="tbLibelle" runat="server" Text='<%# Bind("TypeProduitLibelle") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbLibelle" runat="server" Text='<%# Eval("TypeProduitLibelle") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Associé aux affaires">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitAffaire" runat="server" Checked='<%# Bind("TypeProduitAffaire") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Imputer au budget affaires">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitBudgetAffaire" runat="server" Checked='<%# Bind("TypeProduitBudgetAffaire") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A la journée">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitJournee" runat="server" Checked='<%# Bind("TypeProduitJournee") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CA employé">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitCA" runat="server" Checked='<%# Bind("TypeProduitCA") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CA Axe">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitCAAxe" runat="server" Checked='<%# Bind("TypeProduitCAAxe") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Facturer par Qualif">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitFactQualif" runat="server" Checked='<%# Bind("TypeProduitFactQualif") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Refacturer en interne">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitFactInterne" runat="server" Checked='<%# Bind("TypeProduitFactInterne") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Visible">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTypeProduitVisible" runat="server" Checked='<%# Bind("TypeProduitVisible") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id produit" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTypeProduitID" runat="server" Text='<%# Bind("TypeProduitID") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Supprimer" runat="server" align="center"
                        CommandName="SupprimerTypeProduit" 
                        CommandArgument ='<%# Eval("TypeProduitID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce type de produit ?');"
                        ToolTip="Supprimer"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></div>
        <asp:Button ID="btnEnregistrer" runat="server" Text="Enregistrer" CssClass="btn90" />
        <br />
        <asp:Label runat="server" ID="lblEnregistrement" Visible="false" font-bold="true" ForeColor="Red" Text="Enregistrement effectué avec succès." />
</asp:Content>
