<%@ Page Title="Catalogue des produits" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="Catalogue.aspx.vb" Inherits="ComptaAna.net.Catalogue" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.TreeView" TagPrefix="obout" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <asp:ScriptManager ID="ScriptManagerTEST" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <h1>
            Catalogue des produits
        </h1>
      
        <asp:Button ID="btnNouveau" CssClass="btn75" runat="server" CommandName="Nouveau"
            Text="Nouveau" />
             <div id="TreeView" style="float: left; overflow: auto; 
            margin-right: 30px;">
            <obout:Tree ID="tvCatalogue" runat="server" CssClass="vista" OnSelectedTreeNodeChanged="tvCatalogue_SelectedTreeNodeChanged"
                ViewStateMode="Inherit">
            </obout:Tree>
        </div>
        
        <asp:Label runat="server" ID="lblMsgSupp" ForeColor="Red" />
       <h2 id="hCreationNewProduit" runat="server" visible="false" >
        Création d'un nouveau produit
       </h2>
        <fieldset id="fsDetailsProduit" runat="server" visible="false">
            <legend>Détails du produit</legend>
            <asp:Panel runat="server" ID="pDetailsProduit" Enabled="false">
                <table>
                    <tr>
                        <td>
                            Type* :
                        </td>
                        <td>
                            <oboutComboBox:ComboBox ID="listeType" runat="server" DataTextField="TypeProduitLibelle"
                                DataValueField="TypeProduitID" OnSelectedIndexChanged="listeType_Click" AutoPostBack="True"
                                Width="250px" Enabled="false">
                            </oboutComboBox:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Référence* :
                        </td>
                        <td>
                            <asp:TextBox ID="tbReference" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTbRef" ValidationGroup="vgEngegistrer" runat="server"
                                Display="Dynamic" ControlToValidate="tbReference" ErrorMessage="Veuillez saisir une référence">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Libellé* :
                        </td>
                        <td>
                            <asp:TextBox ID="tbLibelle" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTbLibelle" ValidationGroup="vgEngegistrer" runat="server"
                                Display="Dynamic" ControlToValidate="tbLibelle" ErrorMessage="Veuillez saisir un libellé">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tva* :
                        </td>
                        <td>
                            <oboutComboBox:ComboBox ID="listeTva" runat="server" DataTextField="TvaTaux" DataValueField="TvaID"
                                Width="250px" Enabled="false" style="top: 0px; left: 0px">
                            </oboutComboBox:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Refac* :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbRefac" runat="server">
                                <asp:ListItem>HT</asp:ListItem>
                                <asp:ListItem>TTC</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <fieldset>
                    <legend>Caractéristiques du produit</legend>
                    <table>
                        <tr>
                            <td>
                                Produit associé aux affaires
                            </td>
                            <td>
                                <asp:Image ID="tpAffaire" runat="server" ImageUrl="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Produit à imputer au budget des affaires
                            </td>
                            <td>
                                <asp:Image ID="tpBudgetAffaire" runat="server" ImageUrl="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Produit à saisir à la journée
                            </td>
                            <td>
                                <asp:Image ID="tpJournee" runat="server" ImageUrl="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Produit pris en compte dans le calcul du CA employé
                            </td>
                            <td>
                                <asp:Image ID="tpCAEmploye" runat="server" ImageUrl="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Produit pris en compte dans le calcul du CA AXE
                            </td>
                            <td>
                                <asp:Image ID="tpCAAxe" runat="server" ImageUrl="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Produit facturé par qualification
                            </td>
                            <td>
                                <asp:Image ID="tpFactQualif" runat="server" ImageUrl="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Produit refacturé en interne
                            </td>
                            <td>
                                <asp:Image ID="tpFacInter" runat="server" ImageUrl="" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnEnregistrer" CssClass="btn75" runat="server" CommandName="Enregistrer"
                            Text="Enregistrer" ValidationGroup="vgEngegistrer" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnModifier" CssClass="btn75" runat="server" CommandName="Modifier"
                            Text="Modifier" />
                    </td>
                    <td>
                        <asp:Button ID="btnSupprimer" CssClass="btn75" runat="server" CommandName="Supprimer"
                            Text="Supprimer" OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce produit ?');" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
