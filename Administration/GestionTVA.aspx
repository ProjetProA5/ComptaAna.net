<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="GestionTVA.aspx.vb" Inherits="ComptaAna.net.GestionTVA" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Gestion de la TVA
        </h1>
        <br />
        <fieldset>
            <legend>Nouvelle TVA </legend>
            <table>
                <tr>
                    <td>
                        Taux
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauTaux" runat="server" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator runat="server" ID="rfvDateDeb" ControlToValidate="tbNouveauTaux"
                            Display="Dynamic" ErrorMessage="Un taux est requis<br/>" ValidationGroup="vgTva" />
                        <asp:RegularExpressionValidator ID="revTbNouveauTaux" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbNouveauTaux"
                            ErrorMessage="Veuillez insérer un nombre" ValidationGroup="vgTva" />
                      
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAjouterNouveau" runat="server" Text="Nouveau" CssClass="btn75" ValidationGroup="vgTva" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
                 </fieldset>
            
            <br />
        <div align="center">
        <asp:GridView ID="gvTva" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" EmptyDataText="Pas de données"
            Width="174px" EnableViewState="False" CssClass="Grid" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Taux">
                    <ItemTemplate>
                        <asp:TextBox ID="tbTvaTaux" runat="server" Text='<%# Bind("TvaTaux") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id Tva" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTvaID" runat="server" Text='<%# Bind("TvaID") %>'>
                        </asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actif">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTvaActif" runat="server" Checked='<%# Bind("TvaActif") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RegularExpressionValidator ID="revTbTvaTaux" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbTvaTaux"
                            ErrorMessage="Veuillez insérer un nombre">
                        </asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></div>
        <asp:Button ID="btnEnregistrer" runat="server" Text="Enregistrer" CssClass="btn90" />
            <asp:Label runat="server" ID="lblEnregistrement" Visible="false" Text="Enregistrement effectué avec succès" />
    </div>
</asp:Content>
