<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="GestionService.aspx.vb" Inherits="ComptaAna.net.GestionService" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Gestion des services
        </h1>
        <br />
        <fieldset>
            <legend>Nouveau service</legend>
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
                            Display="Dynamic" ErrorMessage="Un libellé est requis<br/>" ValidationGroup="vgService" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Actif
                    </td>
                    <td>
                        <asp:CheckBox ID="cbNouveauServiceActif" runat="server" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Objectif de CA annuel
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauServiceObjectif" runat="server" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revTbNouveauServiceObjectif" runat="server" Display="Dynamic"
                            Width="572px" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbNouveauServiceObjectif"
                            ErrorMessage="Veuillez insérer un nombre" ValidationGroup="vgService" />
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        Durée des affaires (en mois)
                    </td>
                    <td>
                        <asp:TextBox ID="tbNouveauServiceDureeMouvement" runat="server" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="revTbNouveauServiceDureeMouvement" runat="server"
                            Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbNouveauServiceDureeMouvement"
                            ErrorMessage="Veuillez insérer un nombre" ValidationGroup="vgService" />
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAjouterNouveau" runat="server" Text="Enregistrer" CssClass="btn90" ValidationGroup="vgService"  />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <div align="center">
        <asp:GridView ID="gvService" runat="server" AutoGenerateColumns="False" ViewStateMode="Enabled" EmptyDataText="Pas de données"
            EnableViewState="False" CssClass="Grid" ShowHeaderWhenEmpty="true" OnRowCommand="gvService_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Actif">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbServiceActif" runat="server" Checked='<%# Bind("ServiceActif") %>'
                            Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Libellé">
                    <ItemTemplate>
                        <asp:TextBox ID="tbServiceLibelle" runat="server" Text='<%# Bind("ServiceLibelle") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbServiceLibelle" runat="server" Text='<%# Eval("ServiceLibelle") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Objectif de CA annuel">
                    <ItemTemplate>
                        <asp:TextBox ID="tbServiceObjectif" runat="server" Text='<%# Bind("ServiceObjectif") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbServiceObjectif" runat="server" Text='<%# Eval("ServiceObjectif") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Durée des affaires (en mois)">
                    <ItemTemplate>
                        <asp:TextBox ID="tbServiceDureeMouvement" runat="server" Text='<%# Bind("ServiceDureeMouvement") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbServiceDureeMouvement" runat="server" Text='<%# Eval("ServiceDureeMouvement") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id service" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblServiceID" runat="server" Text='<%# Bind("ServiceID") %>'>
                        </asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="Supprimer" runat="server" align="center"
                            CommandName="SupprimerService" 
                            CommandArgument ='<%# Eval("ServiceID") %>'
                            ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                            OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce service ?');"
                            ToolTip="Supprimer"/>

                        <asp:RegularExpressionValidator ID="revTbServiceObjectif" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbServiceObjectif"
                            ErrorMessage="Veuillez insérer un nombre" />
                        
                        <asp:RegularExpressionValidator ID="revTbServiceDureeMouvement" runat="server" Display="Dynamic"
                            ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbServiceDureeMouvement"
                            ErrorMessage="Veuillez insérer un nombre" />                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></div>
        <asp:Button ID="btnEnregistrer" runat="server" Text="Enregistrer" CssClass="btn90" />
    </div>
</asp:Content>
