<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="GestionPrixQualifTypeAffaire.aspx.vb" Inherits="ComptaAna.net.GestionPrixQualifTypeAffaire" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Gestion des prix par qualifications
        </h1>
        <br />
        <fieldset>
            <legend>Nouvelle Qualification et ses prix </legend>
            <table>
                <tr>
                    <td>
                        Libellé
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauLibelle" runat="server" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator runat="server" ID="rfvDateDeb" ControlToValidate="tbNouveauLibelle"
                            Display="Dynamic" ErrorMessage="Un libellé est requis<br/>" ValidationGroup="vgQualif" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Contrat cadre
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauContratCadre" runat="server" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revTbNouveauContratCadre" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbNouveauContratCadre"
                            ErrorMessage="Veuillez insérer un nombre" ValidationGroup="vgQualif" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Forfait
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauForfait" runat="server" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revTbNouveauForfait" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbNouveauForfait"
                            ErrorMessage="Veuillez insérer un nombre" ValidationGroup="vgQualif" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Régie
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauRegie" runat="server" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revTbNouveauRegie" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbNouveauRegie"
                            ErrorMessage="Veuillez insérer un nombre" ValidationGroup="vgQualif" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAjouterNouveau" runat="server" Text="Nouveau" CssClass="btn75"
                            ValidationGroup="vgQualif" Visible="false" />
                            Ajout impossible pour le moment
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <br />
        <div align="center">
        <asp:GridView ID="gvPrixQualifTypeAffaire" runat="server" AutoGenerateColumns="False" EmptyDataText="Pas de données"
            ViewStateMode="Enabled" EnableViewState="False" CssClass="Grid" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="Visible">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbQualificationActif" runat="server" Checked='<%# Bind("QualificationActif") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qualification">
                    <ItemTemplate>
                        <asp:TextBox ID="tbQualificationLibelle" runat="server" Text='<%# Bind("QualificationLibelle") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbQualificationLibelle" runat="server" Text='<%# Eval("QualificationLibelle") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contrat Cadre">
                    <ItemTemplate>
                        <asp:TextBox ID="tbContratCadre" runat="server" Text='<%# Bind("ContratCadre") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbContratCadre" runat="server" Text='<%# Eval("ContratCadre") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Régie">
                    <ItemTemplate>
                        <asp:TextBox ID="tbRegie" runat="server" Text='<%# Bind("Regie") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbRegie" runat="server" Text='<%# Eval("Regie") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Forfait">
                    <ItemTemplate>
                        <asp:TextBox ID="tbForfait" runat="server" Text='<%# Bind("Forfait") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbForfait" runat="server" Text='<%# Eval("Forfait") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id Qualification" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblQualificationID" runat="server" Text='<%# Bind("QualificationID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RegularExpressionValidator ID="revTbContratCadre" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbContratCadre"
                            ErrorMessage="Veuillez insérer un nombre">
                        </asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="revTbForfait" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbForfait"
                            ErrorMessage="Veuillez insérer un nombre">
                        </asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="revTbRegie" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbRegie" ErrorMessage="Veuillez insérer un nombre">
                        </asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <asp:Button ID="btnEnregistrer" runat="server" Text="Enregistrer" CssClass="btn90" />
        <asp:Label runat="server" ID="lblEnregistrement" Visible="false" Text="Enregistrement effectué avec succès" />
    </div>
</asp:Content>
