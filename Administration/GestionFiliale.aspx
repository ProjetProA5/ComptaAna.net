<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="GestionFiliale.aspx.vb" Inherits="ComptaAna.net.GestionFiliale" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Gestion des filiales
        </h1>
        <br />
        <fieldset>
            <legend>Nouvelle filiale</legend>
            <table>
                <tr>
                    <td>
                        Nom de la filiale :
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauNom" runat="server" />
                    </td>
                    <td>
                         <asp:RequiredFieldValidator runat="server" ID="rfvDateDeb" ControlToValidate="tbNouveauNom"
                        Display="Dynamic" ErrorMessage="Un nom de filiale est requis<br/>" ValidationGroup="vgFiliale" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAjouterNouveau" CssClass="btn90" runat="server" Text="Enregistrer" ValidationGroup="vgFiliale" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <div align="center">
        <asp:GridView ID="gvFiliale" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" EmptyDataText="Pas de données"
            EnableViewState="False" CssClass="Grid" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Nom de la filiale">
                    <ItemTemplate>
                        <asp:TextBox ID="tbFilialeNom" runat="server" Text='<%# Bind("FilialeNom") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbFilialeNom" runat="server" Text='<%# Eval("FilialeNom") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id Filiale" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFilialeID" runat="server" Text='<%# Bind("FilialeID") %>'>
                        </asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actif">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbFilialeActif" runat="server" Checked='<%# Bind("FilialeActif") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <br />
        <asp:Button ID="btnEnregistrer" CssClass="btn90" runat="server" Text="Enregistrer" />
        &nbsp;<asp:Label runat="server" ID="lblEnregistrement" Visible="false" Text="Enregistrement effectué avec succès" />
    </div>
</asp:Content>
