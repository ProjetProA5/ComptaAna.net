<%@ Page Title="Produits liés à l'affaire" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/SiteMaster.Master" CodeBehind="AffaireProduits.aspx.vb" Inherits="ComptaAna.net.AffaireProduits"
    EnableEventValidation="true" %>

<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">


  <script language="JavaScript">
      function SiJourWE(sender, args) {
          var dateText = args.Value;
          var dateChoisie = new Date()

          day = dateText.substring(0, 2);
          month = dateText.substring(3, 5);
          year = dateText.substring(6, 10);

          dateChoisie.setDate(day);
          dateChoisie.setMonth(month - 1);
          dateChoisie.setFullYear(year);

          var result = dateChoisie.getDay()

          if (result == 0) {
              args.IsValid = false;
          }
          else {
              if (result == 6) {
                  args.IsValid = false;
              }
              else {
                  args.IsValid = true;
              }
          }
      }

      function PrixTotal() {
          var qte = document.getElementById('<%= tbQuantite.ClientID %>');
          var pu = document.getElementById('<%= tbPrixUnit.ClientID %>');
          var tva = document.getElementById('<%= ddlTVA.ClientID %>');
          var tvaChoisi = tva.options[tva.selectedIndex].text;
          var tvaValue = parseFloat(tvaChoisi.replace(/,/, "."))
          var prix = document.getElementById('<%= tbTotal.ClientID %>');

          if ((qte.value != "") && (pu.value != "")) {
              var floatQte = parseFloat(qte.value.replace(/,/, "."));
              var floatPu = parseFloat(pu.value.replace(/,/, "."));
              var floatTva = (tvaValue + 100) / 100;

              var total = floatQte * floatPu * floatTva;

              prix.value = total.toFixed(3);
          }
      }

  </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">

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
    <div class="content">
        <h1>
            Produits de l'affaire :
            <asp:Label ID="lblNomAffaire" runat="server"></asp:Label>
        </h1>
        <asp:Label ID="lblNoProduits" runat="server"></asp:Label>
        <div runat="server" id="divRestrictions">
            <fieldset class="AffaireForm">
                <legend>Restrictions</legend>
                <table style="margin-top: 5px; border-spacing: 10px">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmployes" runat="server" DataTextField="PrenomNom" DataValueField="EmployeID"
                                OnSelectedIndexChanged="ddlEmployesAndTypes_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTypes" runat="server" DataTextField="TypeProduitLibelle"
                                DataValueField="TypeProduitID" OnSelectedIndexChanged="ddlEmployesAndTypes_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBU" runat="server" DataTextField="ServiceLibelle" DataValueField="ServiceID"
                                OnSelectedIndexChanged="ddlEmployesAndTypes_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table style="margin-top: 5px; border-spacing: 10px">
                    <tr>
                        <td>
                            <asp:Label ID="LabelTri" runat="server" Text="Période:" Font-Underline="true" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Début (inclus):
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbDateDeb" Width="65px" />
                            <obout:Calendar ID="cCalendrierDebut" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                TextBoxId="tbDateDeb" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                            </obout:Calendar>
                        </td>
                        <td>
                            &nbsp;
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </td>
                        <td>
                            Fin (inclus):
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbDateFin" Width="65px" />
                            <obout:Calendar ID="cCalendrierFin" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                TextBoxId="tbDateFin" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                            </obout:Calendar>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ValidationGroup="vgAffaire" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png"
                                ID="ibValider" runat="server" ToolTip="Rechercher" />
                            <asp:ImageButton ImageUrl="~/App_Themes/ComptaAna/Design/Icon_Excel.png" ID="ibExporter"
                                runat="server" ToolTip="Export Exel" />
                            <asp:CompareValidator ID="cvDateDebut" ValidationGroup="vgAffaire" runat="server"
                                Type="Date" Display="Dynamic" Operator="DataTypeCheck" ControlToValidate="tbDateDeb"
                                ErrorMessage="Veuillez insérer une date de début Valide<br/>" />
                            <asp:CompareValidator ID="cvDateFin" ValidationGroup="vgAffaire" runat="server" Type="Date"
                                Operator="DataTypeCheck" ControlToValidate="tbDateFin" ErrorMessage="Veuillez insérer une date de fin Valide<br/>"
                                Display="Dynamic" />
                            <asp:CompareValidator ID="cvPeriode" ValidationGroup="vgAffaire" runat="server" ControlToValidate="tbDateDeb"
                                ControlToCompare="tbDateFin" Operator="LessThanEqual" Type="Date" Display="Dynamic"
                                ErrorMessage="La période n'est pas valide<br/>" />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="vgAffaire" ID="rfvDateDeb"
                                ControlToValidate="tbDateDeb" Display="Dynamic" ErrorMessage="Une date de début est requise<br/>" />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="vgAffaire" ID="rfvDateFin"
                                ControlToValidate="tbDateFin" Display="Dynamic" ErrorMessage="Une date de fin est requise<br/>" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div align="center">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red" />
            <asp:GridView AutoGenerateColumns="False" ID="gvProduitAffaire" OnRowDataBound="gvProduitAffaire_RowDataBound"
                DataKeyNames="ProduitAffaireID, EmployeID, TypeProduitID, date" border="1" runat="server"
                CssClass="Grid" ShowHeaderWhenEmpty="true" EmptyDataText="Pas de données" OnRowEditing="gvProduitAffaire_RowEditing"
                OnRowUpdating="gvProduitAffaire_RowUpdating" OnRowDeleting="gvProduitAffaire_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ProduitAffaireID" HeaderText="ProduitAffaireID" Visible="false"
                        ReadOnly="True" />
                    <asp:BoundField DataField="EmployeID" Visible="false" ReadOnly="True" />
                    <asp:BoundField DataField="ProduitAffaireDate" HeaderText="Date" ReadOnly="True" />
                    <asp:BoundField DataField="ProduitRef" HeaderText="Référence" ReadOnly="True" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Libellé" HeaderStyle-Width="250px">
                        <ItemTemplate>
                            <%# Eval("ProduitAffaireLibelle")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbEditProduitAffaireLibelle" Text='<%# Eval("ProduitAffaireLibelle") %>'
                                runat="server" Width="250px" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TvaTaux" HeaderText="TVA" ReadOnly="True" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Prix HT">
                        <ItemTemplate>
                            <%# Eval("ProduitAffaireMntUnitHT")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbEditProduitAffaireMntUnitHT" Text='<%# Eval("ProduitAffaireMntUnitHT") %>'
                                runat="server" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Quantité">
                        <ItemTemplate>
                            <%# Eval("ProduitAffaireQte")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbEditProduitAffaireQte" Text='<%# Eval("ProduitAffaireQte") %>'
                                runat="server" Width="80px" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TotalHT" HeaderText="Total HT" ReadOnly="True" />
                    <asp:BoundField DataField="ProduitAffaireDepassementMnt" HeaderText="Dépassement"
                        ReadOnly="True" />
                    <asp:BoundField DataField="ServiceLibelle" HeaderText="Service" ReadOnly="True" />
                    <asp:BoundField DataField="ProduitAffaireDonneurOrdre" HeaderText="Donneur d'ordre"
                        ReadOnly="True" />

                         <asp:CommandField ButtonType="Image" ShowDeleteButton="false" ShowEditButton="True"
                        HeaderText="" DeleteImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        EditImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" UpdateImageUrl="~/App_Themes/ComptaAna/Design/Icon_tick.png"
                        CancelImageUrl="~/App_Themes/ComptaAna/Design/Icon_cross.png" />
                   <asp:TemplateField>
                     <HeaderTemplate>
                            <asp:ImageButton ID="btAjouterProduit" runat="server" align="left" CommandName="AjouterProduit"
                                           ImageUrl="~/App_Themes/ComptaAna/Design/Icon_ajouter.png"
                                            ToolTip="Ajouter"  />
                        </HeaderTemplate>
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
 </div>
            <asp:Panel ID="pPopupAjoutProduit" CssClass="modalPopup" runat="server" Visible="false" ScrollBars="Auto" width="63%"  >
                        <h2>Ajout d'un nouveau produit</h2>
                        <div runat="server" id="divReleve" >
                        <fieldset >
                            <legend>Nouveau produit</legend>
                            
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblDate" runat="server" Text="Date:" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tbDate" Width="65px" />
                                        <obout:Calendar ID="cCalendrier" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                            TextBoxId="tbDate" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa"></obout:Calendar>
                                    </td>

                                     <td align="right">
                                         Employé concerné :
                                     </td>
                                      <td>
                                       <asp:DropDownList ID="ddlEmployeConcerne" runat="server" DataTextField="PrenomNom" DataValueField="EmployeID"
                                            OnSelectedIndexChanged="ddlEmployesAndTypes_SelectedIndexChanged" AutoPostBack="true">
                                         </asp:DropDownList>
                                     </td>
                                    
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblAffaire" runat="server" Text="Affaire: " />
                                    </td>
                                    <td colspan="3">
                                     <asp:textBox Id="tbAffaire" runat="server" DataValueField="AffaireID"></asp:textBox>
                                     <asp:textBox Id="tbAffaireID" runat="server" DataValueField="AffaireID" Visible="false"></asp:textBox>
                                      <%--  <asp:DropDownList ID="ddlAffaire" runat="server" DataValueField="AffaireID" DataTextField="AffaireLibelle" 
                                            AutoPostBack="true" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblSite" runat="server" Text="Sites concerné: " />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSite" runat="server" DataValueField="ClientID" DataTextField="ClientNom" >
                                            <asp:ListItem Text="-- Site -- " Value="default" />
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblQualification" runat="server" Text="Qualification: " />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlQualification" runat="server" DataValueField="QualificationID"
                                            DataTextField="QualificationLibelle" AutoPostBack="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td id="tdDonneurOrdreLabel" runat="server" align="right">
                                         <asp:Label runat="server" ID="lblDonneurOrdre" Text="Donneur d'ordre: " />
                                    </td>
                                    <td id="tdDonneurOrdreTextBox" runat="server">
                                         <asp:TextBox runat="server" ID="tbDonneurOrdre" Width="200px" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblService" runat="server" Text="Service: " CssClass="left" ></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlService" runat="server" DataValueField="ServiceID" DataTextField="serviceLibelle" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblType" runat="server" Text="Type: " />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlType" runat="server" DataValueField="typeProduitID" DataTextField="typeProduitLibelle" Width="180px"
                                             AutoPostBack="true">
                                            <asp:ListItem Text="Choisir un Type" Value="Default" />
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblReference" runat="server" Text="Référence: " />
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlProduit" runat="server" DataValueField="produitID" DataTextField="ProduitRef"
                                            Width="250px" AutoPostBack="true">
                                            <asp:ListItem Text="-- Produit -- " Value="default" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                         <asp:Label runat="server" ID="lblLibelle" Text="Libelle: " />
                                    </td>
                                    <td colspan="7">
                                         <asp:TextBox runat="server" ID="tbLibelle" Width="700px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblQuantite" runat="server" Text="Qté: " />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbQuantite" runat="server" text="0" onkeyup="javascript: PrixTotal();" Width="80px"/>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblPU" runat="server" Text="PU HT: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbPrixUnit" runat="server" text="0" onkeyup="javascript: PrixTotal();" Width="80px"/>
                                        <asp:Label ID="lblWarningTTC" runat="server" Text=" *" Visible="false" ToolTip="Facturé en TTC" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblTVA" runat="server" Text="Tva:" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTVA" runat="server" DataValueField="tvaID" DataTextField="tvaTaux" onchange="javascript: PrixTotal();" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblTotal" runat="server" Text="Total:" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbTotal" runat="server" AutoPostBack="true" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="16" align="right">
                                        <asp:Button ID="bNouveau" CssClass="btn75" runat="server" Text="Enregistrer" ValidationGroup="vgReleve" />
                                    </td>
                                </tr>
                            </table>
                            
                            <asp:Label runat="server" ID="lblErreur" ForeColor="red" />

                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" runat="server" ID="rfvPU"
                                ControlToValidate="tbPrixUnit" Display="Dynamic" ErrorMessage="Un prix unitaire est requis <br/>" />

                            <asp:CompareValidator ValidationGroup="vgReleve" ID="cvPU" runat="server" Type="Double" 
                                Display="Dynamic" Operator="DataTypeCheck" ControlToValidate="tbPrixUnit" ErrorMessage="Veuillez insérer un PU valide <br/>" />

                            <asp:CompareValidator ValidationGroup="vgReleve" ID="cvDate" runat="server" Type="Date"
                                Display="Dynamic" Operator="DataTypeCheck" ControlToValidate="tbDate" ErrorMessage="Veuillez insérer une date Valide <br/>" />
                            <asp:CompareValidator ValidationGroup="vgReleve" ID="cvDateMin" runat="server" ControlToValidate="tbDate"
                                ControlToCompare="tbDateDeb" Operator="GreaterThanEqual" Type="Date" Display="Dynamic"
                                ErrorMessage="La date n'est pas dans la période <br/>" />
                            <asp:CompareValidator ValidationGroup="vgReleve" ID="cvDateMax" runat="server" ControlToValidate="tbDate"
                                ControlToCompare="tbDateFin" Operator="LessThanEqual" Type="Date" Display="Dynamic"
                                ErrorMessage="La date n'est pas dans la période <br/>" />
                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" runat="server" ID="rfvDate"
                                ControlToValidate="tbDate" Display="Dynamic" ErrorMessage="Une date est requise <br/>" />
                            <asp:CustomValidator ValidationGroup="vgReleve" runat="server" ID="ctmDate" ControlToValidate="tbDate" ClientValidationFunction="SiJourWE"
                                Display="Dynamic" ErrorMessage="Vous avez choisi un jour de week end <br/>" />

                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" ID="RfvType" runat="server"
                                ControlToValidate="ddlType" InitialValue="default" ErrorMessage="Le choix d'un type de produit est requis <br/>"
                                Display="Dynamic" />

                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" ID="rfvProduit" runat="server"
                                ControlToValidate="ddlProduit" InitialValue="default" ErrorMessage="Le choix d'un produit est requis<br/>"
                                Display="Dynamic" />

                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" ID="rfvQuantite" runat="server"
                                ControlToValidate="tbQuantite" ErrorMessage="Une quantite est requise<br/>" Display="Dynamic" />
                            <asp:RegularExpressionValidator ValidationGroup="vgReleve" ID="revTbQuantite" runat="server"
                                Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbQuantite"
                                ErrorMessage="Veuillez insérer un nombre<br/>" />

                            <asp:RegularExpressionValidator ValidationGroup="vgReleve" ID="revPrixunit" runat="server"
                                Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbQuantite"
                                ErrorMessage="Veuillez insérer un nombre<br/>" />

                            
                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" ID="rfvSite" runat="server"
                                ControlToValidate="ddlSite" InitialValue="default" ErrorMessage="Le choix d'un site est requis<br/>"
                                Display="Dynamic" />
                            <asp:Label ID="lbQteErreur" runat="server" ForeColor="Red" Text="Ajout impossible : la quantité journalière est supérieure à 1<br />"
                                Visible="False" />
                                 <asp:Label ID="lblQteNulle" runat="server" ForeColor="Red" Text="Ajout impossible : la quantité journalière est égale à 0<br />"
                                Visible="False" />
                                 <asp:Label ID="lblPrixNull" runat="server" ForeColor="Red" Text="Ajout impossible : le prix est null<br />"
                                Visible="False" />

                        </fieldset>
                    </div>
                        <asp:Button ID="btnQuitter" runat="server" Text="Quitter" CssClass="btn90" />
                    </asp:Panel>
       
       <table>
       <tr>
       <td>
         <asp:Button ID="btnRetourFiche" runat="server" Text="Retour à la fiche" CssClass="btn95" Tooltip="Retour à la fiche de l'affaire"/>
       </td>
        <td>
<asp:Button ID="btnRetourListe" runat="server" Text="Retour à la liste" CssClass="btn95" ToolTip=" Retour à la liste des affaires"/>
        </td>
         
       </tr>
       </table>
     
    </div>
</asp:Content>
