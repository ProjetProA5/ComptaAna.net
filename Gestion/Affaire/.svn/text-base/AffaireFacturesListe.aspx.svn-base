<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AffaireFacturesListe.aspx.vb" MasterPageFile="~/SiteMaster.Master" Inherits="ComptaAna.net.AffaireFacturesListe" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">

    <h1  class="h1bis">Liste des Factures</h1>
    <h2 class="legende2">Recherche</h2>
    <fieldset class="recherche2">
        <%-- Champs recherche --%>
        <table style="margin-top: 0px; border-spacing: 10px">
            <tr>
                <td valign="top">
                    <table style="margin-top: 0px; border-spacing: 10px" class="tableau" >
                        <tr>
                        <td>
                            <table cellspacing="5px">
                            <tr>
                             <td>
                                        <asp:Label ID="lblCritere" runat="server" Text="Filtre:" 
                                            Font-Bold="true" CssClass="couleurTextRecherche" Font-Underline="true"/>
                                    </td>
                            </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblParCient" runat="server" Text="Par Client:" CssClass="couleurTextRecherche"
                                            Font-Bold="true" />
                                    </td>
                                    <td>
                                        <oboutComboBox:ComboBox ID="cbbFiltreClient" runat="server" Width="200px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblParBU" runat="server" Text="Par Service:" 
                                            Font-Bold="true" CssClass="couleurTextRecherche" />
                                    </td>
                                    <td>
                                        <oboutComboBox:ComboBox ID="cbbFiltreService" runat="server" Width="200px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblParTypeAffaire" runat="server" Text="Par Type d'affaire:" 
                                            Font-Bold="true"  CssClass="couleurTextRecherche"/>
                                    </td>
                                    <td>
                                        <oboutComboBox:ComboBox ID="cbbFiltreType" runat="server" Width="200px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table cellspacing="5px">
                                    <td>
                                        <asp:Label ID="lblPaye" runat="server" Text="Etat de facturation:" Font-Underline="False"
                                            Font-Bold="True" CssClass="couleurTextRecherche" />
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rbPaye" runat="server" RepeatDirection="horizontal">
                                            <asp:ListItem Value="0" class="couleurTextRecherche">Non payé</asp:ListItem>
                                            <asp:ListItem Value="1" class="couleurTextRecherche">Payé</asp:ListItem>
                                            <asp:ListItem Value="2" Selected="True" class="couleurTextRecherche">Tous</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table cellspacing="5px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblParMontant" runat="server" Text="Rechercher une facture à partir d'un montant:"
                                                Font-Underline="False" Font-Bold="True" CssClass="couleurTextRecherche" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbMontant" runat="server" Width="100px" />
                                        </td>
                                         
                                    </tr>
                                </table >
                            </td>
                           
                        </tr>
                        <tr>
                        <td colspan="6" align="right">
                                <asp:ImageButton ID="btRechercheFacture" runat="server" OnClick="btRechercheFacture_Click"
                                    CommandName="RechercheFacture" CommandArgument='<%# Eval("ClientID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png"
                                    ToolTip="Rechercher" />
                                <asp:ImageButton ID="ibExporter" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_Excel.png"
                                    ToolTip="Export Excel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>


   
              
            <%--<tr>
            <td>
            <asp:Label runat="server" Text="Somme des produits en cours de production:" ></asp:Label>
            <asp:TextBox ID="tbEnCours" runat="server" Enabled="false" width="100"></asp:TextBox>
            </td>
            </tr>
            <tr>
             <td>
            <asp:Label ID="lblAvance" runat="server" Text="Somme des produits constatés d'avance:" ></asp:Label>
            <asp:TextBox ID="tbAvance" runat="server" Enabled="false" width="100"></asp:TextBox>
            </td>
            </tr>--%>
            </table>
            <br />
<div align="center">
    <asp:Label ID="lMessage" runat="server" ForeColor="red"/>
</div>
<div class="content" align="center">
  <asp:GridView AutoGenerateColumns="False" ID="gvFacture" runat="server" DataKeyNames="FacturationAffaireID" EmptyDataText="Pas de données" 
        OnRowCommand="gvFacture_RowCommand"  cssclass="Grid" AlternatingRowStyle-BackColor="#E5F3FF">
            <Columns>
                <asp:BoundField DataField="FacturationAffaireID" HeaderText="FactureID" Visible="false"/>
                <asp:BoundField DataField="FacturationAffaireRef" HeaderText="Référence facture" ReadOnly="true"/>
               <asp:BoundField DataField="Designation" HeaderText="Désignation" ReadOnly="true" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Commentaire">
                        <ItemTemplate>
                            <%# Eval("Commentaire")%> 
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbCom" Text='<%# Eval("Commentaire") %>'
                                runat="server"  />
                                </EditItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="TypeAffaire" HeaderText="Type d'affaire"  ReadOnly="true"/>
                <asp:BoundField DataField="ServiceAffaire" HeaderText="Service" ReadOnly="true"/>
                <asp:BoundField DataField="MontantPrestas" HeaderText="Prestations HT" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                <asp:BoundField DataField="MontantFrais" HeaderText="Frais HT" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                <asp:BoundField DataField="MontantTTC" HeaderText="Montant TTC" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date demande facture">
                        <ItemTemplate>
                            <%# Eval("DateDemandeFacture")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbDateDemandeFacture" MaxLength="10" Text='<%# Eval("DateDemandeFacture") %>'
                                runat="server"  />
                                <obout:Calendar ID="cCalendrier1" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                            TextBoxId="tbDateDemandeFacture" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                        </obout:Calendar>
                                </EditItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Envoyé le">
                        <ItemTemplate>
                            <%# Eval("DateEnvoi")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbDateEnvoi" MaxLength="10" Text='<%# Eval("DateEnvoi") %>'
                                runat="server"  />
                                <obout:Calendar ID="cCalendrier2" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                            TextBoxId="tbDateEnvoi" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                        </obout:Calendar>
                                </EditItemTemplate>
                    </asp:TemplateField>
                <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Payé" >
                        <ItemTemplate>
                            <%# Eval("Paye")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbPaye"  MaxLength="1" Text='<%# Eval("Paye") %>'
                                runat="server" />
                                </EditItemTemplate>
                    </asp:TemplateField>--%>
                     <asp:TemplateField HeaderText="Payé" >
                        <ItemTemplate>
                            <asp:CheckBox ID="cbPaye" align="center" runat="server" Enabled="False" Checked='<%# Bind("Paye") %>'/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="cbPaye" CommandName="ModifValidEtapeFactu" runat="server" Checked='<%# Eval("Paye") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="Date de paiement" >
                        <ItemTemplate>
                            <%# Eval("DatePaiement")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbDatePaiement" MaxLength="10"  Text='<%# Eval("DatePaiement") %>'
                                runat="server" />
                                <obout:Calendar ID="cCalendrier3" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                            TextBoxId="tbDatePaiement" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                        </obout:Calendar>
                                </EditItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="CompteAuto" HeaderText="Délai paiement" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />

                <asp:CommandField ButtonType="Image" ShowDeleteButton="false" ShowEditButton="True"
                        HeaderText="Modifier" DeleteImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        EditImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" UpdateImageUrl="~/App_Themes/ComptaAna/Design/Icon_tick.png"
                        CancelImageUrl="~/App_Themes/ComptaAna/Design/Icon_cross.png" />
                        
                </Columns>
  </asp:GridView>
</div>
  
    <br />
     <script language="javascript" type="text/javascript">
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
                 var obj = document.getElementById('<%=btRechercheFacture.CLientID%>');
                 obj.click();
             }
         }
    </script> 
</asp:Content>