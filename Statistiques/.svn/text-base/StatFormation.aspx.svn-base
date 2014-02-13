<%@ Page Title="Statistiques sur les coûts salariaux" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/SiteMaster.Master" CodeBehind="StatFormation.aspx.vb" Inherits="ComptaAna.net.StatFormation" %>

    <%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1 class="h1bis">
            Statistiques sur les formations</h1>
        <div class="content" align="center">
        <h2 class="legende3">Recherche</h2>
        <fieldset class="recherche3">
            
            <table cellspacing="13px">
                <tr>
                    <td  valign="top" align="right">
                        <asp:Label ID="lblType" runat="server" Text="Type de formations:" Font-Underline="true"
                            Font-Bold="true" CssClass="couleurTextRecherche" />
                    </td>
                    <td valign="top">
                        <oboutComboBox:ComboBox ID="cbbTypeFormation" runat="server" Width="230px">
                            <oboutComboBox:ComboBoxItem ID="cbbTtesFormations" Selected="true" Value="Toutes"
                                runat="server" Text="Toutes les formations" />
                            <oboutComboBox:ComboBoxItem ID="cbbFormationSuivieR" Value="Suivie" runat="server"
                                Text="Formation suivie réalisée" />
                            <oboutComboBox:ComboBoxItem ID="cbbFormationDispenseeR" Value="Dispensee" runat="server"
                                Text="Formation dispensée réalisée" />
                            <oboutComboBox:ComboBoxItem ID="cbbFormationPrevue" Value="Prevue" runat="server" Text="Formation prévue" />
                        </oboutComboBox:ComboBox>
                    </td>
                     
                     <td  valign="top" align="right">
                        <asp:Label ID="lblEmploye" runat="server" Text="Par employé:" Font-Underline="true"
                            Font-Bold="true" CssClass="couleurTextRecherche" />
                    </td>
                    
                        <td colspan="2" valign="top">
                                 <oboutComboBox:ComboBox ID="cbbEmploye" Width="230px" runat="server" DataTextField="PrenomNom"
                                                                    DataValueField="EmployeID" 
                                     style="top: 0px; left: 0px">
                                                                </oboutComboBox:ComboBox>
                            </td>
                   
                </tr>
                 <tr>
                 <td  valign="top" align="right">
                        <asp:Label ID="Label2" runat="server" Text="Par statut:" Font-Underline="true"
                            Font-Bold="true" CssClass="couleurTextRecherche"/>
                    </td>
                  <td valign="top" >
                   <asp:RadioButtonList ID="rbStatut" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value= "0" class="couleurTextRecherche">Cadre</asp:ListItem>
                            <asp:ListItem Value= "1" class="couleurTextRecherche">Non cadre</asp:ListItem>
                            <asp:ListItem Value= "2" Selected="True" class="couleurTextRecherche">Tous</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                <td valign="top" align="right">
                                     <asp:Label CssClass="couleurTextRecherche" ID="Label1" Font-Underline="true" runat="server" Font-Bold="true">Période:</asp:Label>
                                 </td>
                                  <td rowspan="2" >
                                     <table cellpadding="5px">
                                         <tr>
                                             <td align="right" valign="top">
                                                 <asp:Label  CssClass="couleurTextRecherche" ID="lblDateDebut" runat="server" Text="Date de debut :" />
                                             </td>
                                             <td colspan="3" >
                                                 <asp:TextBox runat="server" ID="tbDateDeb" Width="100px" />
                                                 </td>
                                                   <td colspan="3" >
                                                 <obout:Calendar ID="cDateDeb" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                                     TextBoxId="tbDateDeb" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right" >
                                                 <asp:Label ID="lblDateFin" runat="server" Text="Date de fin :" CssClass="couleurTextRecherche"/>
                                             </td>
                                             <td colspan="3" >
                                                 <asp:TextBox runat="server" ID="tbDateFin" Width="100px" />
                                                   </td>
                                                   <td colspan="3">
                                                 <obout:Calendar ID="cDateFin" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                                     TextBoxId="tbDateFin" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                                             </td>
                                             </tr>
                                             
                                             
                                     </table>
                                 </td>
                </tr>
                  
                   <tr>
                      <td valign="top" align="right">
                        <asp:Label ID="Label3" runat="server" Text="Par genre:" Font-Underline="true"
                            Font-Bold="true" CssClass="couleurTextRecherche" />
                    </td>
                    <td valign="top">
                       <asp:RadioButtonList ID="rbSexe" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value= "0" class="couleurTextRecherche">Homme</asp:ListItem>
                            <asp:ListItem Value= "1" class="couleurTextRecherche">Femme</asp:ListItem>
                              <asp:ListItem Value= "2" Selected="True" class="couleurTextRecherche">Tous</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                    </td>
                           
                    </tr>
                    <tr>
                    
                                                <td colspan="2">
                                                    &nbsp;</td>
                                                 <td colspan="2" align="right" valign="top">
                                                 <table cellpadding="5px">
                                                 <tr>
                                                 <td>
                                                  <asp:ImageButton ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png" ID="ibValider"
                                                         runat="server" ToolTip="Rechercher" CommandArgument='<%# Eval("ClientID") %>' />
                                                       
                                                     <asp:ImageButton ID="btnExporter" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_Excel.png"
                                                         ValidationGroup="vgRechercher" ToolTip="Export Excel" />
                                                 </td>
                                            
                                                 </tr>
                                                 
                                                 </table>
                                                    </td>
                                             </tr>
             
                           </table>
                      </fieldset>
                      </div>
        <div align="center" style="float:none; width: 100%;  overflow: auto; margin-top: 0px;">
                                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbDateDeb"
                                         ControlToCompare="tbDateFin" Operator="LessThanEqual" Type="Date" Display="Dynamic"
                                         ErrorMessage="La période n'est pas valide<br/>" />
        <asp:Label ID="lblConfirmation" runat="server" Visible="false"/>
    <br />  
           <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvFormation" EmptyDataText="Pas de données"
        CssClass="Grid" runat="server"  AlternatingRowStyle-BackColor="#E5F3FF">
        <Columns>
            <asp:BoundField DataField="FormationID" HeaderText="FormationID" SortExpression="FormationID"
                Visible="false" />
                <asp:BoundField DataField="EmployeNom" HeaderText="Nom">
            </asp:BoundField>
            <asp:BoundField DataField="EmployePrenom" HeaderText="Prenom">
            </asp:BoundField>
            <asp:BoundField DataField="FormationLibelle" HeaderText="Libellé de la formation">
            </asp:BoundField>
            <asp:BoundField DataField="FormationNbHeure" HeaderText="Nombre d'heures" >
            </asp:BoundField>
             <asp:BoundField DataField="FormationType" HeaderText="Type" >
            </asp:BoundField>
            <asp:BoundField DataField="FormationOrganisme" HeaderText="Organisme" >
            </asp:BoundField>
            <asp:BoundField DataField="FormationNbParticipants" HeaderText="Nombre de participants">
            </asp:BoundField>
              <asp:BoundField DataField="FormationCout" HeaderText="Coût">
            </asp:BoundField>
            <asp:BoundField DataField="FormationDateDeb" HeaderText="Date de début">
            </asp:BoundField>
            <asp:BoundField DataField="FormationDateFin" HeaderText="Date de fin">
            </asp:BoundField>
            <asp:BoundField DataField="FormationDate" HeaderText="Date">
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
               
                    <asp:ImageButton ID="Supprimer" runat="server" align="center"
                        CommandName="SupprimerFormation" 
                        CommandArgument ='<%# Eval("FormationID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cette formation ?');"
                                     ToolTip="Supprimer formation" />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label runat="server" ID="lblTotalHeures" ForeColor="CadetBlue" Font-Bold="true" Text = "Total des heures de formation: " />
    <asp:TextBox runat="server" ID="tbTotal" Font-Bold="true" Style="text-align: center" enabled="false"  ForeColor="DarkRed" Width="60px" />
    </div>
    </div>

  </asp:Content>
