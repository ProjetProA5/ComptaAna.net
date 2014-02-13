<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireFacture.aspx.vb" Inherits="ComptaAna.net.AffaireFacture" %>

<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.TreeView" TagPrefix="obout" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Reference() {
            var Ref = document.getElementById('<%= tbFactureRef.ClientID %>');
            var oldRef = document.getElementById('<%= lblMaxReference.ClientID %>');

            if (Ref.value == "") {
                var today = new Date();
                var annee = today.getFullYear();
                var newRef = parseInt(oldRef.innerText) + 1;
                
                Ref.value = newRef + "/" + annee;
            }
        }
    
    </script>    
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">
    <asp:ScriptManager ID="ScriptManagerTEST" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div class="content" >

      <asp:Menu ID="mMenuAffaireModif" runat="server" Orientation="Horizontal" >
            <StaticMenuStyle CssClass="SimpleStaticMenu"/>
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover"/>
            <Items>
                <asp:MenuItem Text="»" Enabled="false" />
                <asp:MenuItem Value="0" Text="Détails"/>
                <asp:MenuItem Value="1" Text="Produit"  />
                <asp:MenuItem Value="2" Text="Facturation" />
                <asp:MenuItem Value="3" Text="Liste des sous-affaire"  />
                 </Items>
        </asp:Menu>

    <div id="TreeView" style="float: left; height: 600px; width:200px; overflow: auto; margin-right:30px;">
        <obout:Tree ID="tvFacture" runat="server" CssClass="vista" 
            OnSelectedTreeNodeChanged="tvFacture_SelectedTreeNodeChanged" ViewStateMode="Inherit" />
    </div>

    <h1>
        Factures
    </h1>

    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" />

    <asp:Panel ID="pIdentification" runat="server">
    <fieldset>
        <legend>Identification de l'affaire</legend>
        <ul style="list-style-type:none;">
            <li style="padding: 0.5em 0em 0.5em 0em;">
                <asp:label ID="lblOldRef" runat="server" Text="Référence :" Width="80px" />
                <asp:TextBox ID="tbReferenceFacture" runat="server" Width="135px" />
            </li>
            <li style="padding: 0.5em 0em 0.5em 0em;">
                <asp:label ID="lblOldLibelle" runat="server" Text="Libellé :" Width="80px" />
                <asp:TextBox ID="tbLibelleFacture" runat="server" Width="405px" />
            </li>
            <li style="padding: 0.5em 0em 0.5em 0em;">
                <asp:label ID="lblOldDate" runat="server" Text="Prendre en compte les produits saisi jusqu'au (inclu) * :" Width="350px" />
                <asp:TextBox runat="server" ID="tbDateFacture" Width="135px" Enabled="false" />
            </li>
            <li style="padding: 0.5em 0em 0.5em 0em;">
                <asp:label ID="lblOldHT" runat="server" Text="Total HT :" Width="80px" />
                <asp:TextBox runat="server" ID="tbTotHtFacture" Width="135px" Enabled="false" />
                <asp:label ID="lblOldTTC" runat="server" Text="Total TTC :" Width="80px" style="margin-left:43px;" />
                <asp:TextBox runat="server" ID="tbTotTtcFacture" Width="135px" Enabled="false" />
            </li>
            <li><br /></li>
            <li>
                <table>
                <tr>
                <td><asp:Button ID="btnModification" runat="server" Text="Enregistrer" CssClass="btn75" /></td>
                <td><asp:Button ID="btnSuppression" runat="server" Text="Supprimer" CssClass="btn75"
                     OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cette facture ?');" /></td>
                <td><asp:Button ID="btnProduits" runat="server" Text="Produits" CssClass="btn75" /></td>
                </tr>
                </table>
            </li>
        </ul>
    </fieldset>
    </asp:Panel>

    <br />

    <asp:Panel ID="pNouvelleFacture" runat="server">
    <fieldset>
        <legend>Nouvelle facture</legend>
        <ul id="listNouvelleFacture" style="list-style-type:none;">
            <li style="padding: 0.5em 0em 0.5em 0em;">
                <table>
                    <tr>
                        <td><asp:label ID="lblNewRef" runat="server" Text="Référence* :" Width="80px" /></td>
                        <td><asp:TextBox ID="tbFactureRef" runat="server" Width="135px" onclick="javascript: Reference();" />
                            <asp:RegularExpressionValidator ID="revFactureRef" ValidationGroup="InsertFacture" runat="server" Display="Dynamic" 
                                    ValidationExpression="^([0-9]+[/]+[0-9]{4})$" ControlToValidate="tbFactureRef"
                                    ErrorMessage="Référence format: numéro/année." />
                        </td>
                        <td><asp:Label ID="lblMaxReference" runat="server" ForeColor="Transparent" /></td>
                    </tr>
                </table>

            </li>
            <li style="padding: 0.5em 0em 0.5em 0em;">
                <asp:label ID="lblNewLibelle" runat="server" Text="Libellé :" Width="80px" />
                <asp:TextBox ID="tbLibelle" runat="server" Width="405px" />
            </li>
            <li style="padding: 0.5em 0em 0.5em 0em;">
                <asp:label ID="lblNewDate" runat="server" Text="Prendre en compte les produits saisi jusqu'au (inclu) * :" Width="350px" />
                <asp:TextBox runat="server" ID="tbFactureDate" Width="135px" />
                <obout:Calendar ID="cCalendrier" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                        TextBoxId="tbFactureDate" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                <asp:CompareValidator ID="cvDateFin" runat="server" Type="Date" Display="Dynamic"
                        Operator="DataTypeCheck" ControlToValidate="tbFactureDate" ErrorMessage="Veuillez insérer une date valide." />
            </li>
            <li>
                <asp:Label ID="lblerror" runat="server" Visible="false" />
            </li>
            <li>
                <asp:Button ID="btnAjouter" runat="server" Text="Enregistrer" CssClass="btn75" ValidationGroup="InsertFacture" />
            </li>
            <li>
                <br />
            </li>
            <li>
                    <asp:Label ID="lblerror2" runat="server" Visible="false"></asp:Label>
            </li>
            <li>
                <asp:RadioButtonList ID="rbAffaireProduitType" runat="server">
                    <asp:ListItem>Sélectionner les produits liées à cette affaire</asp:ListItem>
                    <asp:ListItem>Facturation en pourcentage sur les étapes déjà validée</asp:ListItem>
                    <asp:ListItem>Ajouter les produits manuellement</asp:ListItem>
                </asp:RadioButtonList>
            </li>
            <li>
                <asp:Button ID="btnChoix" runat="server" Text="ok" CssClass="btn75" />
            </li>
            <li></li>
            <li>
                    <asp:Label ID="lblerror3" runat="server" Visible="false"></asp:Label>
            </li>
            <li>
                <asp:GridView ID="gvFactureSelectProduit" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    CssClass="Grid" ShowHeaderWhenEmpty="true" EmptyDataText="Pas de données"
                    DataKeyNames="ProduitAffaireID, ProduitID, TypeProduitID" 
                    OnRowDataBound="gvFactureSelectProduit_RowDataBound" OnRowCommand="gvFactureSelectProduit_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbProduitSelect" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="btnSelection" runat="server" align="center" CommandName="SelectionnerProduit"
                                    ImageUrl="~/App_Themes/ComptaAna/Design/Icon_tick.png" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProduitAffaireDate" DataFormatString="{0:d}" HeaderText="Date" />
                        <asp:BoundField DataField="ProduitInfo" HeaderText="Détail des produits" />
                        <asp:BoundField DataField="ProduitAffaireLibelle" HeaderText="Libelle" />
                        <asp:BoundField DataField="ProduitAffaireMntUnitHT" HeaderText="Prix" />
                        <asp:BoundField DataField="ProduitAffaireQte" HeaderText="Quantité" />
                        <asp:BoundField DataField="TotalHT" HeaderText="Total HT" />
                    </Columns>
                </asp:GridView>
            </li>
            <li></li>
            <li>
                <asp:GridView ID="gvProduitPourcentage" runat="server" AutoGenerateColumns="False"
                    CssClass="Grid" ShowHeaderWhenEmpty="true" EmptyDataText="Pas de données"
                    DataKeyNames="AffaireID, AffaireEtapeFactureID" >
                    <Columns>
                        <asp:BoundField DataField="EtapeFacturePourcentage" HeaderText="Pourcentage" />
                        <asp:TemplateField HeaderText="Libelle">
                            <ItemTemplate>
                                <asp:TextBox ID="tbPourcentageLibelle" runat="server" Text='<%# Bind("ProduitLibelle") %>' Width="300px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prix">
                            <ItemTemplate>
                                <asp:TextBox ID="tbPourcentagePrix" runat="server" Text='<%# Bind("Prix") %>' Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantité">
                            <ItemTemplate>
                                <asp:TextBox ID="tbPourcentageQte" runat="server" Text='<%# Bind("Quantite") %>' Width="100px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TotalHT" HeaderText="Total HT" />
                    </Columns>
                </asp:GridView>
            </li>
            <li>
                <asp:Button ID="btnConfirmPourcentage" runat="server" Text="ok" CssClass="btn75" />
            </li>
            <li></li>
            <li>
                <asp:GridView ID="gvManuelProduit" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    CssClass="Grid" ShowHeaderWhenEmpty="true" EmptyDataText="Pas de données" OnRowCommand="gvManuelProduit_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="Libelle">
                            <ItemTemplate>
                                <asp:Label ID="lblManuelLibelle" runat="server" Text='<%# Eval("ProduitLibelle") %>' Width="300px" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="tbManuelLibelle" runat="server" Width="300px" />
                                <br />
                                <asp:RequiredFieldValidator ID="rfvLibelle" runat="server" ControlToValidate="tbManuelLibelle" Display="Dynamic" 
                                ErrorMessage="Veuillez rentrer une description du produit." ValidationGroup="InsertProduct">
                                </asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prix">
                            <ItemTemplate>
                                <asp:Label ID="lblManuelPrix" runat="server" Text='<%# Eval("Prix") %>' Width="100px" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="tbManuelPrix" runat="server" Width="100px" />
                                <br />
                                <asp:RequiredFieldValidator ID="rfvPrix" runat="server" ControlToValidate="tbManuelPrix" Display="Dynamic"  
                                    ErrorMessage="Prix obligatoire." ValidationGroup="InsertProduct"/>
                                <asp:RegularExpressionValidator ID="revPrix" ValidationGroup="InsertProduct" runat="server" Display="Dynamic" 
                                    ValidationExpression="^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$" ControlToValidate="tbManuelPrix"
                                    ErrorMessage="Format: numérique." />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantité">
                            <ItemTemplate>
                                <asp:Label ID="lblManuelQte" runat="server" Text='<%# Eval("Quantite") %>' Width="100px" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="tbManuelQte" runat="server" Width="100px" />
                                <br />
                                <asp:RequiredFieldValidator ID="rfvQte" runat="server" ControlToValidate="tbManuelQte" Display="Dynamic"  
                                    ErrorMessage="Quantité obligatoire." ValidationGroup="InsertProduct"/>
                                <asp:RegularExpressionValidator ID="revQte" ValidationGroup="InsertProduct" runat="server" Display="Dynamic" 
                                    ValidationExpression="^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$" ControlToValidate="tbManuelQte"
                                    ErrorMessage="Format: numérique." />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TVA">
                            <ItemTemplate>
                                <asp:Label ID="lblManuelTVA" runat="server" Width="100px" Text='<%# Eval("TVA") %>' />
                            </ItemTemplate>
                            <FooterTemplate>
                                <oboutComboBox:ComboBox ID="cbbManuelTVA" runat="server" Width="100px" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total HT">
                            <ItemTemplate>
                                <asp:Label ID="lblManuelTotal" runat="server" Text='<%# Eval("TotalHT") %>' Width="100px" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="ibtnAjouter" runat="server" align="center" CommandName="AjouterProduit" AlternateText="Ajouter"
                                    ImageUrl="~/App_Themes/ComptaAna/Design/Icon_ajouter.png" ValidationGroup="InsertProduct" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </li>
        </ul>
    </fieldset>
    </asp:Panel>
     <table>
       <tr>
       <td>
         <asp:Button ID="btnRetourFiche" runat="server" Text="Retour à la fiche" CssClass="btn90" Tooltip="Retour à la fiche de l'affaire"/>
       </td>
        <td>
<asp:Button ID="btnRetourListe" runat="server" Text="Retour à la liste" CssClass="btn90" ToolTip=" Retour à la liste des affaires"/>
        </td>
         
       </tr>
       </table>
    </div>
</asp:Content>