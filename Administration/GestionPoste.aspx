<%@ Page Title="Gestion des postes" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" 
CodeBehind="GestionPoste.aspx.vb" Inherits="ComptaAna.net.GestionPoste" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head" >
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Gestion des postes
        </h1>
        <br />
        <fieldset>
            <legend>Nouveau poste</legend>
            <table>
                <tr>
                    <td>
                        Nom du poste :
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauNom" runat="server" />
                    </td>
                    <td>
                         <asp:RequiredFieldValidator runat="server" ID="rfvDateDeb" ControlToValidate="tbNouveauNom"
                        Display="Dynamic" ErrorMessage="Un nom de poste est requis<br/>" ValidationGroup="vgPoste" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAjouterNouveau" CssClass="btn90" runat="server" Text="Enregistrer" ValidationGroup="vgPoste" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" />
        <br />
        <div align="center">
        <asp:GridView ID="gvPoste" runat="server" AutoGenerateColumns="False" EmptyDataText="Pas de données"
            CssClass="Grid" ShowHeaderWhenEmpty="true" OnRowCommand="gvPoste_RowCommand" AlternatingRowStyle-BackColor="#E5F3FF">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:BoundField DataField="Libelle" HeaderText="Libelle" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="Supprimer" runat="server" align="center"
                            CommandName="SupprimerPoste" 
                            CommandArgument ='<%# Eval("ID") %>'
                            ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                            OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce poste ?');"
                            ToolTip="Supprimer"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
    </div>
</asp:Content>
