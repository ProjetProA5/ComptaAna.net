<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireFactureDetailsV2.aspx.vb" Inherits="ComptaAna.net.AffaireFactureDetailsV2" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">
    <h1>Détails d'une facture</h1>

    <div class="content" align="center">
        <fieldset style="width:21cm; background: url(../../App_Themes/Axone/Design/Facture_bgSansRib.png) center bottom no-repeat;">
        <table width="100%" style="background: url(../../App_Themes/Axone/Design/Logo_Axe.png) left top no-repeat;">
            <tr>
                <td align="center">
                    <asp:Label ID="lblClientNom" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Black" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblClientAdresse" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Black" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblClientCPetVille" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Black" /><br/><br/><br/><br/><br/><br/>
                </td>
            </tr>
              <tr>
             <td>
             <br />
             </td>
               
             </tr>
            <tr>
                <td align="right" valign="bottom" colspan="2">
                    <asp:Label ID="lblFactureRefetDate" runat="server" Font-Bold="true" ForeColor="Black" />
                </td>
            </tr>
            <tr>
             <td align="right" valign="bottom" colspan="2">
                    <asp:Label ID="lblCom" runat="server" Text="Observation : " ForeColor="Black" />

                    <asp:TextBox ID="tbCommentaireFacture" runat="server" width="20%" TextMode="SingleLine" Wrap="true" ForeColor="Black" />
                </td>
            </tr>
             <tr>
             <td>
             <br />
             </td>
               
             </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="lblAxeTVA" runat="server" Text="N°TVA intracommunautaire AXE : FR 92 429 489 966" ForeColor="Black" />
                    <br />
                    <asp:Label ID="lblClientTVA" runat="server" Text="N°TVA intracommunautaire " ForeColor="Black" />
                </td>
            </tr>
              <tr>
             <td align="left" valign="bottom" colspan="2">
              <br />
                    <asp:Label ID="lblNumBC" runat="server" Text="Bon de commande : " ForeColor="Black" />

                    <asp:TextBox ID="tbBC" runat="server" ForeColor="Black" width="100px"/>
                </td>
            </tr>

             <tr>
             <br />
             <td align="left" valign="bottom" colspan="2">
              <br />
                 <table border="0" cellpadding="0" cellspacing="0">
                     <tr>
                         <td rowspan="2" >
                             <asp:Label ID="lblCom2" runat="server" style="margin-right: 5px" Text="Commentaires : " ForeColor="Black" />
                         </td>
                         <td>
                          <asp:TextBox ID="tbCommentaire2" runat="server" ForeColor="Black" Width="100%" TextMode="MultiLine" Wrap="true"/>
                         </td>
                     </tr>
                 </table>
                </td>
            </tr>
             
        </table>

         <asp:GridView AutoGenerateColumns="False" ID="gvBonCommande" runat="server" DataKeyNames="FactureSauvegardeTypeProduitID" EmptyDataText="Pas de données" 
           OnRowDataBound="gvBonCommande_RowDataBound" CssClass="Grid" ShowHeaderWhenEmpty="true" Width="100%" OnRowEditing="gvBonCommande_RowEditing"
                OnRowUpdating="gvBonCommande_RowUpdating" OnRowCancelingEdit="gvBonCommande_RowCancelingEdit">
                       <Columns>
             <asp:BoundField DataField="FactureSauvegardeTypeProduitID" HeaderText="FactuID" Visible="false" ItemStyle-Width="60%" />
               <%--<asp:BoundField DataField="Designation" HeaderText="Désignation" ItemStyle-Width="60%" />--%>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60%" HeaderText="Désignation">
                        <ItemTemplate>
                            <%# Eval("Designation")%> 
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbDesignation" Width="60%" Text='<%# Eval("Designation") %>'
                                runat="server"  />
                                </EditItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="Nbre" ItemStyle-HorizontalAlign="Center" HeaderText="Nbre" ItemStyle-Width="10%" ReadOnly="true" />
                <asp:BoundField DataField="PU" ItemStyle-HorizontalAlign="Right" HeaderText="PU €uros" ItemStyle-Width="15%" ReadOnly="true"  />
                <asp:BoundField DataField="Total" ItemStyle-HorizontalAlign="Right" HeaderText="Total €uros" ItemStyle-Width="15%" ReadOnly="true" />
                  <asp:CommandField ButtonType="Image" ItemStyle-HorizontalAlign="Center" ShowDeleteButton="false" ShowEditButton="True"
                        HeaderText="" DeleteImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        EditImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" UpdateImageUrl="~/App_Themes/ComptaAna/Design/Icon_tick.png"
                        CancelImageUrl="~/App_Themes/ComptaAna/Design/Icon_cross.png" />
                </Columns>
        </asp:GridView>
     
       <table width="100%" align="right">
        <tr>
        <td align="right">
        <asp:Label ID="lblSSTotalPresta" runat="server" Text="Sous total HT" />
             <asp:TextBox ID="tbSSTotalHT" runat="server" Enabled="false" style="text-align: right;" ForeColor="Black" width="10%"/>
        </td>
          
        </tr>
        <tr>
        <td align="right">
         <asp:Label ID="lblTVA" runat="server" Text="TVA 19,60%" />
             <asp:TextBox ID="tbTVA" runat="server"  Enabled="false" style="text-align: right;" ForeColor="Black" width="10%"/>
        </td>
         
        </tr>
        <tr>
        <td align="right">
          <asp:Label ID="lblTotalPresta" runat="server" Text="Sous total TTC" />
            <asp:TextBox ID="tbSSTotalTTC" runat="server"  Enabled="false" style="text-align: right;" ForeColor="Black" width="10%"/>
        </td>       
        </tr>
         <tr>
        <td align="right">
       <br />
        </td>
        </tr>
        </table>
        
 
      
              <br />

        <br />
         <br />
        <asp:GridView AutoGenerateColumns="False" ID="gvFrais" runat="server" EmptyDataText="Pas de données" 
             OnRowDataBound="gvFrais_RowDataBound" DataKeyNames="ProduitAffaireID" CssClass="Grid" ShowHeaderWhenEmpty="true" Width="100%"
             OnRowEditing="gvFrais_RowEditing" OnRowUpdating="gvFrais_RowUpdating" OnRowCancelingEdit="gvFrais_RowCancelingEdit" OnRowDeleting="gvFrais_RowDeleting" >
            <Columns>
             <asp:BoundField DataField="ProduitAffaireID" HeaderText="ProduitAffaireID" Visible="false" ReadOnly="True" />
             <asp:BoundField DataField="Date" HeaderText="Date" Visible="True" ReadOnly="True" ItemStyle-ForeColor="Gray" />
             <asp:BoundField DataField="Employe" HeaderText="Employé" Visible="true" ReadOnly="True" ItemStyle-ForeColor="Gray"/>
              <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Frais à TVA déductible" ItemStyle-Width="60%" >
                        <ItemTemplate>
                            <%# Eval("Frais")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbEditProduitAffaireLibelle" Text='<%# Eval("Frais") %>'
                                runat="server" Width="250px" />
                        </EditItemTemplate>
                    </asp:TemplateField>
               <%-- <asp:BoundField DataField="Nbre" ItemStyle-HorizontalAlign="Center" HeaderText="Nbre" ItemStyle-Width="10%"  />--%>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Nbre" ItemStyle-Width="10%" >
                        <ItemTemplate>
                            <%# Eval("Nbre")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbEditProduitAffaireNbre" Text='<%# Eval("Nbre") %>'
                                runat="server" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateField>
             <%--   <asp:BoundField DataField="PU" ItemStyle-HorizontalAlign="Right" HeaderText="PU €uros" ItemStyle-Width="15%"  />--%>
                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="PU €uros" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <%# Eval("PU")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbEditProduitAffairePU" Text='<%# Eval("PU") %>'
                                runat="server" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField DataField="Total" ItemStyle-HorizontalAlign="Right" HeaderText="Total €uros" ItemStyle-Width="15%" ReadOnly="True"/>
                <asp:CommandField ButtonType="Image" ItemStyle-HorizontalAlign="Center" ShowDeleteButton="false" ShowEditButton="True"
                        HeaderText="" DeleteImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        EditImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" UpdateImageUrl="~/App_Themes/ComptaAna/Design/Icon_tick.png"
                        CancelImageUrl="~/App_Themes/ComptaAna/Design/Icon_cross.png" />
                <asp:TemplateField>
                          <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" runat="server" CommandName="DeleteProduitAffaire"
                            CommandArgument ='<%# Eval("ProduitAffaireID") %>'
                                ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                                OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce produit');"
                                ToolTip="Supprimer produit" />
                        </ItemTemplate>
                              </asp:TemplateField>
                </Columns>
               </asp:GridView>
              
        <panel id="panelSSTotalFrais" runat="server">
         <asp:Label ID="legendeTableauFrais" runat="server" Text="La date et l'employé n'apparaissent pas dans l'export Excel de la facture" ForeColor="Gray"/>
        <table width="100%" align="right">
        <tr>
        <td align="right">
          <asp:Label ID="lblSSTotalFrais" runat="server" Text="Sous total HT" />
             <asp:TextBox ID="tbSSTotalFrais" runat="server"  Enabled="false" style="text-align: right;" ForeColor="Black" width="10%"/>
           </td>
          
        </tr>
        <tr>
        <td align="right">
          <asp:Label ID="lblTVAF" runat="server" Text="TVA 19,60%" />
             <asp:TextBox ID="tbTVAF" runat="server"  Enabled="false" style="text-align: right;" ForeColor="Black" width="10%"/>
        </td>
          
        </tr>
        <tr>
        <td align="right">
         <asp:Label ID="lblTotalFrais" runat="server" Text="Sous total TTC" />
            <asp:TextBox ID="tbTotalFrais" runat="server"  Enabled="false" style="text-align: right;" ForeColor="Black" width="10%"/>
        </td>
        </tr>
        <tr>
        <td align="right">
       <br />
        </td>
        </tr>
        </table>
           </panel>
        <br />
             <br />

         <br />
        <table width="100%" class="Grid" >
            <tr>
                <th align="left" width="88%">
                    MONTANT NET TOTAL A PAYER EN €UROS A RECEPTION DE FACTURE          
                </th>
                <td><asp:Label ID="lblToutTotal" runat="server" ForeColor="red" Font-Bold="true" /></td>
            </tr>
        </table>

        <table width="100%" align="center" cellspacing="2px">
        
        <tr>
        <td colspan="4" align="left">
        <br />
          <asp:Label ID="lblCondition" runat="server" Font-Size="Small" Text="Conditions de règlement : chèque ou virement à réception de facture"  ForeColor="Black" />
            <br />
        </td>
        </tr>
             <tr>
             <td align="left" valign="bottom" >
            
              <br />
                    <asp:Label ID="lblRib" runat="server" Font-Size="Small" Text="Domiciliation bancaire :"  ForeColor="Black" />
                    </td>
             
               <td align="left" valign="bottom" >

                    <asp:Label ID="lblRibAxe" runat="server" Font-Size="Small" ForeColor="Black" />
                </td>
             
               <td align="left" valign="bottom" >
        
                    <asp:Label ID="lblIban" runat="server" Font-Size="Small" Text="IBAN : "  ForeColor="Black" />
                    </td>
                    <td align="left"  valign="bottom">
                       <asp:label ID="lblIbanNum" runat="server" Font-Size="Small" ForeColor="Black" />
                </td>
             </tr>
             <tr>
             <td align="center" colspan="4">
             
             <table cellspacing="5px">
           <%--  <tr>
              <td align="center" valign="bottom" colspan="2">
          
                    <asp:Label ID="lblNumBanque" runat="server" Font-Size="Small" Text="N°Banque : "  ForeColor="Black" />

                    <asp:label ID="lblNBanque" runat="server" Font-Size="Small" ForeColor="Black" />
                </td>
           
             <td align="center" valign="bottom" colspan="2">
       
                    <asp:Label ID="lblCompte" runat="server" Font-Size="Small" Text="Compte : "  ForeColor="Black" />

                    <asp:label ID="lblNCompte" runat="server" Font-Size="Small" ForeColor="Black" />
                </td>
               <td align="center" valign="bottom" colspan="2">
                    <br />
               </td>
              <td align="center" valign="bottom" colspan="2">
         
                    <asp:Label ID="lblGuichet" runat="server" Font-Size="Small" Text="Guichet : "  ForeColor="Black" />

                    <asp:label ID="lblNGuichet" runat="server" Font-Size="Small" ForeColor="Black" />
                </td>
                 <td align="center" valign="bottom" colspan="2">
        
                    <asp:Label ID="lblCle" runat="server" Font-Size="Small" Text="Clé : "  ForeColor="Black" />

                    <asp:label ID="lblNCle" runat="server" Font-Size="Small" ForeColor="Black" />
                </td>
             </tr>--%>
             
             <tr>
                 <td>
                 <br />
                </td>
             </tr>
             <tr>
            
             <td align="right" colspan="10" style="margin-left:20px">
               <asp:Label ID="Label1" runat="server" Width="100%" style="text-align:left" Font-Size="Small" Text="Pour tout retard de paiement supérieur à 30 jours à compter de la date d'emission de la facture, des intérêts calculés sur la base de 2 fois le taux d'intérêt légal seront ajoutés à la présente facture." Font-Italic="true"  ForeColor="DarkGray" />
             </td>
              </tr>
             </table>
            
             </td>
            </tr>
        
        </table>
                <br /><br /><br /><br /><br /><br /><br />
        </fieldset>

       </div>
       <table>
       <tr>
       <td>
       <asp:Button ID="btnRetour" runat="server" Text="Retour" CssClass="btn75" />
       </td>
       <td>
       <asp:Button ID="btnEnregistrer" runat="server" Text="Enregistrer" CssClass="btn75" />
       </td>
       <td>
        <asp:Button ID="btnExporter" runat="server" Text="Exporter" CssClass="btn75" />
       </td>
       </tr>
       </table>
       
       
      
</asp:Content>
