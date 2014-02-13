<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireFacturation.aspx.vb" Inherits="ComptaAna.net.AffaireFacturation" %>

<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.TreeView" TagPrefix="obout" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ouvrirFacture(url) {
            window.open(url);
        }
    
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">
    <asp:Menu ID="mMenuAffaireModif" runat="server" Orientation="Horizontal">
        <StaticMenuStyle CssClass="SimpleStaticMenu" />
        <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
        <StaticSelectedStyle CssClass="SimpleStaticSelected" />
        <StaticHoverStyle CssClass="SimpleStaticHover" />
        <Items>
            <asp:MenuItem Text="»" Enabled="false" />
            <asp:MenuItem Value="0" Text="Détails" />
            <asp:MenuItem Value="1" Text="Produit" />
            <asp:MenuItem Value="2" Text="Facturation" />
            <asp:MenuItem Value="3" Text="Liste des sous-affaire" />
        </Items>
    </asp:Menu>
    <div id="dListeFactures" align="center">
        <h1 id="hTitrePageFacture" runat="server">
            Liste des factures
        </h1>
        <br />
        <asp:Panel runat="server" Width="60%">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnNouveau" runat="server" Text="Nouvelle facture" CssClass="btn95" />
                    </td>
                    <td>
                        <asp:Button ID="btnAvoir" runat="server" Text="Nouvel avoir" CssClass="btn90" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" />
            <fieldset align="left" id="fsNouvelleFacture" runat="server" visible="false">
                <legend>Nouvelle facture</legend>
                <table id="listNouvelleFacture" style="list-style-type: none" cellspacing="15px">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblNewRef" runat="server" Font-Bold="true" Text="Référence :"  />
                        </td>
                        <td align="left">
                            <asp:Label ID="lblFactureRef" runat="server" Text="" />
                        </td>

                       
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblNewLibelle" runat="server" Font-Bold="true" Text="Libellé :" Width="80px" />
                        </td>
                        <td colspan="4" align="left">
                            <asp:TextBox ID="tbLibelle" runat="server" Width="100%" ValidationGroup="vgEnregistrement"/>
                        </td>
                                          </tr>
                            <asp:RequiredFieldValidator ID="rfvLibelle" runat="server" ControlToValidate="tbLibelle" ErrorMessage="Le libellé est obligatoire" Display="Dynamic" ValidationGroup="vgEnregistrement"/>
                        

                    <tr>
                        <td align="right">
                          
                                        <asp:Label ID="lblPourcentageMois" runat="server" Text="" Font-Bold="true" />
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPourcentageMoisFacturation" runat="server">
                                        </asp:DropDownList>
                                    
                        </td>

                        <td align="right">
                                        <asp:Label ID="lblDateFacture" runat="server" Text="Date effective de la facture:"
                                            Font-Bold="true" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbFactureDate" runat="server" Width="80px" />
                                        <obout:Calendar ID="Calendar1" runat="server" CultureName="fr-FR" DateFormat="dd/MM/yyyy"
                                            DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif" DatePickerMode="true"
                                            ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" TextBoxId="tbFactureDate" />
                                    </td>
                    </tr>
                  
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblRib" runat="server" Font-Bold="true" Text="A facturer sur le compte : "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlRib" runat="server"></asp:DropDownList>
                            
                        </td>
                         <td align="right">
                            <asp:Label ID="lblNumBonCommande" runat="server" Font-Bold="true" Text="N° bon de commande : " ></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbNumBonCommande" runat="server" Width="100%" ></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            
                            <asp:Label ID="lblErreurDate" runat="server" ForeColor="Red" Text="Veuillez insérer une date."
                                Visible="false" Width="350px" />
                            <%--  <asp:CompareValidator ID="cvDateFin" runat="server" 
                                            ControlToValidate="tbFactureDate" Display="Dynamic" 
                                            ErrorMessage="Veuillez insérer une date valide." Operator="DataTypeCheck" 
                                            Type="Date" ValidationGroup="vgDateFin" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnEnregistrer" runat="server" CssClass="btn75" Text="Enregistrer"
                                            ValidationGroup="vgEnregistrement" CommandArgument='<%# Eval("ClientID") %>' />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnAnnuler" runat="server" CssClass="btn75" Text="Annuler" />
                                    </td>
                                      
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset align="left" id="fsAvoir" runat="server" visible="false">
                <legend>Nouvel avoir</legend>
                <table id="Table1" style="list-style-type: none;" cellpadding="5px" cellspacing="5px">
                    <tr>
                        <td>
                            <asp:Label ID="lblReferenceAvoir" runat="server" Font-Bold="true" Text="Référence : " />
                        </td>
                        <td>
                            <asp:Label ID="lblRefAv" runat="server" Text="" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLibelleAvoir" runat="server" Font-Bold="true" Text="Libellé : " />
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="tbLibelleAvoir" runat="server" Width="300px" />
                            <asp:RequiredFieldValidator ID="rfvLibelleAvoir" runat="server" Display = "Dynamic" ControlToValidate="tbLibelleAvoir" ValidationGroup="vgSaveAvoir" ErrorMessage="Le libellé de l'avoir est obligatoire"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMontantAvoir" runat="server" Font-Bold="true" Text="Montant de l'avoir : " />
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="tbMntAvoir" runat="server" Width="100px" />
                            <asp:RequiredFieldValidator ID="rfvMntAvoir" runat="server" Display = "Dynamic" ControlToValidate="tbMntAvoir" ValidationGroup="vgSaveAvoir" ErrorMessage="Le montant de l'avoir est obligatoire"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                                        <asp:Label ID="lblDateAvoir" runat="server" Text="Date effective de l'avoir:"
                                            Font-Bold="true" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="tbDateAvoir" runat="server" Width="80px" />
                                        <obout:Calendar ID="Calendar2" runat="server" CultureName="fr-FR" DateFormat="dd/MM/yyyy"
                                            DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif" DatePickerMode="true"
                                            ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" TextBoxId="tbDateAvoir" />
                                    </td>
                                    <td>
                                     <asp:CompareValidator ValidationGroup="vgSaveAvoir" ID="cvDateFin" runat="server" Type="Date" Operator="DataTypeCheck"
                                    ControlToValidate="tbDateAvoir" ErrorMessage="Veuillez insérer une date valide<br/>"
                                    Display="Dynamic" />
                                <asp:RequiredFieldValidator ValidationGroup="vgSaveAvoir" runat="server" ID="rfvDateFin" ControlToValidate="tbDateAvoir"
                                    Display="Dynamic" ErrorMessage="Une date est requise<br/>" />
                                    </td>
                        </tr>
                        <tr>
                         <td align="right">
                            <asp:Label ID="lblRibAvoir" runat="server" Font-Bold="true" Text="A débiter du compte : "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlRibAvoir" runat="server"></asp:DropDownList>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnSaveAvoir" runat="server" CssClass="btn75" Text="Enregistrer" ValidationGroup="vgSaveAvoir"
                                            CommandArgument='<%# Eval("ClientID") %>' />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnAnnulerAvoir" runat="server" CssClass="btn75" Text="Annuler" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            </table>
        </asp:Panel>
        <asp:GridView ID="gvListeDesFactures" AutoGenerateColumns="False" runat="server"
            CssClass="Grid" EmptyDataText="Pas de Facture" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:BoundField DataField="FacturationAffaireID" Visible="false" />
                <asp:BoundField DataField="FacturationAffaireRef" HeaderText="Référence" />
                <asp:BoundField DataField="FacturationAffaireLibelle" HeaderText="Libellé" />
                <asp:BoundField DataField="FacturationAffaireDate" HeaderText="Date de blocage des produits (inclu):" />
                <asp:BoundField DataField="CoordonneeBancaire" HeaderText="Coordonnée Bancaire" />
                <asp:TemplateField HeaderText="Editer">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditer" runat="server" CommandName="LienDetails"
                                CommandArgument='<%# Eval("FacturationAffaireID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" 
                                ToolTip="Editer" />
                        </ItemTemplate>
                    </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Supprimer" Visible="true">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnSupprimer" runat="server" CommandName="SuppressionFacture"
                            CommandArgument='<%# Eval("FacturationAffaireRef") %>' 
                            ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                             OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cette facture ? ');"
                              ToolTip="Supprimer Facture" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div style="margin-left: 15%">
            <table align="left">
                <tr>
                    <td>
                        <asp:Button ID="btnRetourFiche" runat="server" Text="Retour à la fiche" CssClass="btn95"
                            ToolTip="Retour à la fiche de l'affaire" />
                    </td>
                    <td>
                        <asp:Button ID="btnRetourListe" runat="server" Text="Retour à la liste" CssClass="btn95"
                            ToolTip=" Retour à la liste des affaires" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Panel ID="pPopupSaveFacture" CssClass="modalPopup" runat="server" Visible="false"
        ScrollBars="Auto" Height="50%">
        <h2>
            Archivage de la facture ou de l'avoir sur le réseau</h2>
        <br />
        <h3>
            Veuillez choisir la version modifiée du fichier à sauvegarder sur le réseau:</h3>
        <br />
        <br />
        <div id="divSave" runat="server" align="center">
            <asp:FileUpload ID="FUSaveFacture" runat="server" />
            <asp:Label ID="lblErreurFile" runat="server" Visible="false" Fore-color="Red" Text="Veuillez choisir un fichier" />
            <br />
            <br />
            <table id="tableBtn" runat="server">
                <tr>
                    
                    <td>
                        <asp:Button ID="btnArchiverPopUp" runat="server" Text="Archiver" CssClass="btn90" />
                        
                    </td>
                     <td>
                        <asp:Button ID="btnQuitter" runat="server" Text="Quitter" CssClass="btn90" />
                    </td>
                    <td>
                     <asp:Label ID="lblRefArchivage" runat="server" Text="" Visible="false"></asp:Label>
                     <asp:Label ID="lblFactureID" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                   
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pPopupSavePlusieursMois" CssClass="modalPopup" runat="server" Visible="false"
        ScrollBars="Auto" Height="500px">
        <h2>
            Attention :
        </h2>
        <br />
        <h3>
            Vous êtes sur le point de facturer plusieurs mois à la fois</h3>
        <br />
        <h3>
            Appuyez sur continuer pour tous les facturer sinon appuyer sur annuler</h3>
        <br />
        <br />
        <div id="div1" runat="server" align="Right">
            <br />
            <br />
            <table id="table2" runat="server">
                <tr>
                    
                    <td>
                        <asp:Button ID="btnContinuer" runat="server" Text="Continuer" CssClass="btn90" />
                    </td>
                    <td>
                        <asp:Button ID="btnQuitterSaveMois" runat="server" Text="Quitter" CssClass="btn90" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <%--   <script language="javascript" type="text/javascript">
                        document.onkeypress = keyhandler;
                        function keyhandler(e) {
                            var Key;

                            if (window.event)        // IE
                            {
                                Key = window.event.keyCode;
                            }
                            else if (e.which) // Netscape/Firefox/Opera
                            {
                                Key = e.which
                            }

                            if (Key == 13) {
                                var obj = document.getElementById('<%=btnEnregistrer.CLientID%>');
                                obj.click();
                            }
                        }
    </script>--%>

</asp:Content>
