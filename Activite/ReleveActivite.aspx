<%@ Page Title="Relevé d'activité" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="ReleveActivite.aspx.vb" Inherits="ComptaAna.net.ReleveActivite" EnableEventValidation="true" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">

  <script language="JavaScript">
      function SiJourWE(sender, args) {
          var dateText = args.Value;
          var dateChoisie = new Date()

          day = dateText.substring(0, 2);
          month = dateText.substring(3, 5);
          year = dateText.substring(6, 10);

          dateChoisie.setDate(day);
          dateChoisie.setMonth(month-1);
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
          var tvaValue = parseFloat(tvaChoisi.replace(/,/,"."))
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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">

    <div class="content">
        <h1 style="white-space: nowrap;" >Relevé d'activité</h1>

            <asp:Menu ID="mMenuInsertionVisualision" runat="server" OnMenuItemClick="mMenuInformationCout_MenuItemClick"
            Orientation="Horizontal"  >
            <StaticMenuStyle CssClass="SimpleStaticMenu"/>
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover"/>
            <Items>
                <asp:MenuItem Text="»" Enabled="false" />
                <asp:MenuItem Value="0" Text="Insertion" Selected="true"/>
                <asp:MenuItem Value="1" Text="Visualisation générale" />
                <asp:MenuItem Value="2" Text="Mon relevé par affaire" />
            </Items>
            </asp:Menu>

        
        <asp:MultiView ID="mvInsertion" runat="server" ActiveViewIndex="0">
            <asp:View ID="vInsertion" runat="server">
                <fieldset class="loginUserForm">
                    <legend>Relevé d'activité </legend>
                    <asp:Button ID="btnPeriode" runat="server" Text="Changer de période" CssClass="btn175" Visible="false" />
                    <table style="margin-top: 0px; border-spacing: 10px">
                     <tr>
                                <td>
                                    <asp:Label ID="LabelTri" runat="server" Text="Période:" Font-Underline="true" Font-Bold="true" />
                                </td>
                            </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDateDebut" runat="server" Text="Date de debut :" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="tbDateDeb" Width="65px" />
                                <obout:Calendar ID="cDateDeb" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                    TextBoxId="tbDateDeb" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                            </td>
                            <td>
                                <asp:Label ID="lblDateFin" runat="server" Text="Date de fin :" />
                            </td>
                            <td valign="middle">
                                <asp:TextBox runat="server" ID="tbDateFin" Width="65px"/>
                                <obout:Calendar ID="cDateFin" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                    TextBoxId="tbDateFin" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ibDates" runat="server" 
                                    ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png" 
                                    style="height: 26px" 
                                    ToolTip ="Rechercher" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ibExporter" runat="server"
                                    ToolTip ="Export Excel" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ibPourcentageRemplissage" runat="server" Visible="false"/>
                            </td>
                            <td>
                                <asp:CompareValidator ID="cvDateDebut" runat="server" Type="Date" Display="Dynamic"
                                    Operator="DataTypeCheck" ControlToValidate="tbDateDeb" ErrorMessage="Veuillez insérer une date de début Valide<br/>" />
                                <asp:CompareValidator ID="cvDateFin" runat="server" Type="Date" Operator="DataTypeCheck"
                                    ControlToValidate="tbDateFin" ErrorMessage="Veuillez insérer une date de fin Valide<br/>"
                                    Display="Dynamic" />
                                <asp:CompareValidator ID="cvPeriode" runat="server" ControlToValidate="tbDateDeb"
                                    ControlToCompare="tbDateFin" Operator="LessThanEqual" Type="Date" Display="Dynamic"
                                    ErrorMessage="La période n'est pas valide<br/>" />
                                <asp:RequiredFieldValidator runat="server" ID="rfvDateDeb" ControlToValidate="tbDateDeb"
                                    Display="Dynamic" ErrorMessage="Une date de début est requise<br/>" />
                                <asp:RequiredFieldValidator runat="server" ID="rfvDateFin" ControlToValidate="tbDateFin"
                                    Display="Dynamic" ErrorMessage="Une date de fin est requise<br/>" />
                            </td>
                        </tr>
                    </table>

                    <asp:Label runat="server" ID="lblResume" Font-Bold="True" Font-Size="15px" ForeColor="gray" />

                    <asp:Panel ID="pPopupInfosRemplissage" CssClass="modalPopup" runat="server" Visible="false" Height="500px" >
                        <div align="center">
                            <h2>Relevé d'activité - Jours non remplis à 100%</h2>
                            <br />
                            <asp:Panel runat="server" ScrollBars="Auto" Height="400px">
                                <asp:GridView ID="gvInfosRemplissage" runat="server" AutoGenerateColumns="True" ScrollBars="True"/>
                            </asp:Panel>
                            <br />
                            <asp:Button ID="btnQuitter" runat="server" Text="Quitter" CssClass="btn90" />
                        </div>
                    </asp:Panel>

                    <div runat="server" id="divReleve" visible="false">
                        <fieldset visible="false">
                            <legend>Nouveau produit</legend>
                            <asp:RadioButtonList ID="tblReleve" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="true">Associé à une affaire</asp:ListItem>
                                <asp:ListItem> Non associé à une affaire</asp:ListItem>
                            </asp:RadioButtonList>
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
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblAffaire" runat="server" Text="Affaire: " />
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlAffaire" runat="server" DataValueField="AffaireID" DataTextField="AffaireLibelle" 
                                            AutoPostBack="true" />
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
                                    <td>
                                    <asp:RadioButtonList ID="rbIntExt" runat="server" AutoPostBack="true" Visible="false" RepeatDirection="Horizontal">
                            <asp:ListItem Value= "0" >Interne</asp:ListItem>
                            <asp:ListItem Value= "1">Externe</asp:ListItem>
                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                         <asp:Label runat="server" ID="lblLibelle" Text="Libelle*: " />
                                    </td>
                                    <td colspan="7">
                                         <asp:TextBox runat="server" ID="tbLibelle" Width="700px" />
                                    </td>
                                </tr>
                               
                                 <tr>
                                 <td colspan="1" align="right">
                                        <asp:Label ID="lblNbHeures" runat="server" Text="Nombre d'heures*:" Visible="false"/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbNbHeures" runat="server" Width="50px"  Visible="false" />
                                    </td>
                                   
                                </tr>
                                <tr>
                             

                                     <td align="right">
                                        <asp:Label ID="lblOrganisme" runat="server" Text="Organisme et Lieu de formation*:" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbOrga" runat="server" Width="300px" visible="false" />
                                    </td>

                                    
                                    </tr>
                                   <tr>
                                   
                                             <td align="right">
                                                    <asp:Label ID="lblNbParticipants" runat="server" Text="Nombre de participants*:"
                                                        Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbNbParticipants" runat="server" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblCout" runat="server" Text="Coût horaire*:" Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbCout" runat="server" Visible="false" />
                                                </td>
                                            </tr>
                                        
                                       
                                     <tr>
                                    <td align="right">
                                        <asp:Label ID="lblQuantite" runat="server" Text="Qté*: " />
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
                             <tr>
                                 <td colspan="5" align="right">
                                     <asp:Label ID="lblTotal" runat="server" Text="Total:" />
                                 </td>
                                 <td>
                                     <asp:TextBox ID="tbTotal" runat="server" AutoPostBack="true" Enabled="false" />
                                 </td>
                             </tr>
                                   
                                    <tr>
                                        <td align="right" colspan="16">
                                            <asp:Button ID="bNouveau" runat="server" CssClass="btn90" Text="Enregistrer" 
                                                ValidationGroup="vgReleve" />
                                        </td>
                                    </tr>
                                </tr>
                            
                            <tr>
                            <td runat="server" colspan="4">
                            <asp:Label runat="server" ID="lblMsg" ForeColor="blue" />

                              
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

                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" ID="rfvAffaire" runat="server"
                                ControlToValidate="ddlAffaire" InitialValue="default" ErrorMessage="Le choix d'une affaire est requis<br/>"
                                Display="Dynamic" />

                            <asp:RequiredFieldValidator ValidationGroup="vgReleve" ID="rfvSite" runat="server"
                                ControlToValidate="ddlSite" InitialValue="default" ErrorMessage="Le choix d'un site est requis<br/>"
                                Display="Dynamic" />
                            <asp:Label ID="lbQteErreur" runat="server" ForeColor="Red" Text="Ajout impossible : la quantité journalière est supérieure à 1<br />"
                                Visible="False" />
                                 <asp:Label ID="lblQteNulle" runat="server" ForeColor="Red" Text="Ajout impossible : la quantité journalière est égale à 0<br />"
                                Visible="False" />
                                 <asp:Label ID="lblPrixNull" runat="server" ForeColor="Red" Text="Ajout impossible : le prix est null<br />"
                                Visible="False" />
                            </td>  </tr>  </table>
                        </fieldset>
                    </div>
                    <br />
                    <div align="center" style="float:none; width: 100%;  overflow: auto; margin-top: 0px;">
                    <asp:Label runat="server" ID="lblConfirmation" ForeColor="blue" />
                        <br />
                    <asp:GridView ID="gvProduitAffaire" runat="server" AutoGenerateColumns="False" CssClass="Grid" EmptyDataText="Pas de données"
                        ShowHeaderWhenEmpty="true" DataKeyNames="ProduitAffaireID,TypeProduitID,ProduitAffaireDate" >
                        <Columns>
                            <asp:BoundField DataField="ProduitAffaireID" HeaderText="ProduitAffaireID" Visible="False" ReadOnly="True"/>
                            <asp:BoundField DataField="ProduitAffaireDate" HeaderText="Date" ReadOnly="True" />
                            <asp:BoundField DataField="ClientNom" HeaderText="Client - Site" ReadOnly="True" />
                            <asp:BoundField DataField="ProduitRef" HeaderText="Référence" ReadOnly="True" />
                            
                            <asp:BoundField DataField="ProduitAffaireLibelle" HeaderText="Libelle" Visible="false" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Libellé" HeaderStyle-Width="250px">
                                <ItemTemplate>
                                    <%# Eval("ProduitAffaireLibelle")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbEditProduitAffaireLibelle" Text='<%# Eval("ProduitAffaireLibelle") %>' runat="server" Width="250px" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="ProduitAffaireDonneurOrdre" HeaderText="Donneur d'ordre" ReadOnly="True" />
                            
                            <asp:BoundField DataField="ProduitAffairePrixUnit" HeaderText="Prix Unitaire" Visible="false" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Prix Unitaire" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <%# Eval("ProduitAffairePrixUnit")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbEditProduitAffaireMntUnitHT" Text='<%# Eval("ProduitAffairePrixUnit") %>' runat="server" Width="150px" />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ProduitAffaireQte" HeaderText="Quantité" Visible="false"  />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Quantité" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <%# Eval("ProduitAffaireQte")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbEditProduitAffaireQte" Text='<%# Eval("ProduitAffaireQte") %>' runat="server" Width="80px" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="totalHT" HeaderText="Total HT" ReadOnly="True" />
                            <asp:BoundField DataField="TvaTaux" HeaderText="TVA" ReadOnly="True" />
                            <asp:BoundField DataField="TotalTTC" HeaderText="Total TTC" ReadOnly="True" />                            
                            <asp:BoundField DataField="ProduitAffaireDepassementMnt" HeaderText="" Visible="False"/>

                            <asp:CommandField  buttontype="Image" ShowDeleteButton="false" ShowEditButton="true" HeaderText=""
                                EditImageUrl = "~/App_Themes/ComptaAna/Design/Icon_modifier.png"  
                                UpdateImageUrl = "~/App_Themes/ComptaAna/Design/Icon_tick.png" 
                                CancelImageUrl = "~/App_Themes/ComptaAna/Design/Icon_cross.png"/>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnDelete" runat="server" CommandName="DeleteProduitAffaire" 
                                        CommandArgument ='<%# Eval("ProduitAffaireID") %>'
                                        ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                                        OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer ce produit');"
                                        ToolTip ="Supprimer"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </fieldset>
            </asp:View>
            <asp:View ID="vVisualisation" runat="server">
                <fieldset class="loginUserForm">
                    <legend>Visualisation des données </legend>
                    <table style="margin-top: 0px; border-spacing: 10px">
                       <tr>
                        <td valign="top">
                            <table style="margin-top: 0px; border-spacing: 10px">
                             <tr>
                                <td>
                                    <asp:Label ID="lblPeriodeVisu" runat="server" Text="Période:" Font-Underline="true" Font-Bold="true" />
                                </td>
                            </tr>
                             <tr>
                       
                            <td>
                                <asp:Label ID="lblDateDebVisu" runat="server" Text="Date de debut :" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="tbDateDebVisualisation" Width="65px" />
                                <obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                    TextBoxId="tbDateDebVisualisation" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa"></obout:Calendar>
                            </td>
                            <td>
                                <asp:Label ID="lblDateFinVisu" runat="server" Text="Date de fin :" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="tbDateFinVisualisation" Width="65px" />
                                <obout:Calendar ID="Calendar2" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                    TextBoxId="tbDateFinVisualisation" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa"></obout:Calendar>
                            </td>
                         
                            <td>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Type="Date" Display="Dynamic"
                                    Operator="DataTypeCheck" ControlToValidate="tbDateDebVisualisation" ErrorMessage="Veuillez insérer une date de début Valide<br/>" />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Type="Date" Operator="DataTypeCheck"
                                    ControlToValidate="tbDateFinVisualisation" ErrorMessage="Veuillez insérer une date de fin Valide<br/>"
                                    Display="Dynamic" />
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tbDateDebVisualisation"
                                    ControlToCompare="tbDateFinVisualisation" Operator="LessThanEqual" Type="Date"
                                    Display="Dynamic" ErrorMessage="La période n'est pas valide<br/>" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="tbDateDebVisualisation"
                                    Display="Dynamic" ErrorMessage="Une date de début est requise<br/>" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="tbDateFinVisualisation"
                                    Display="Dynamic" ErrorMessage="Une date de fin est requise<br/>" />
                            </td>
                        </tr>
                             </table>
                    </td>
                    
                            <td valign="top">
                              <table style="margin-top: 0px; border-spacing: 10px">
                               <tr>
                                  <td>
                                    <asp:Label ID="Label1" runat="server" Text="Restrictions:" Font-Underline="true"
                                        Font-Bold="true" />
                                </td>
                            </tr>
                        <tr>
                            <td colspan="2">
                                <oboutComboBox:ComboBox runat="server" ID="ddlEmployeVisualisation" DataValueField="employeID"
                                    DataTextField="employe" Width="300px" Height="300px" />
                            </td>
                            <td colspan="3">
                                <oboutComboBox:ComboBox runat="server" ID="cbbAffaire1" DataValueField="AffaireID"
                                    DataTextField="AffaireLibelle" width="300" Height="300px"/>
                            </td>
                               <td>
                                <asp:ImageButton ID="ibDatesVisualisation" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png"
                                    BorderStyle="None" ToolTip ="Rechercher" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnExportExcelVisu" runat="server" 
                                    ImageUrl="~/App_Themes/ComptaAna/Design/Icon_Excel.png" 
                                    ToolTip ="Export Excel" />
                            </td>
                        </tr>
                         <tr>
                                    <td colspan="3">
                                <asp:CheckBox ID="cbNonAssocie" runat="server" Text="Afficher les produits non associés aux affaires" />
                            </td>

                                </tr>
                                 
                        </table>
                    </td>
                </tr>
               
                  </table>
                  <div align="center" style="float:none; width: 100%;  overflow: auto; margin-top: 0px;">
                    <asp:GridView ID="gvProduitAffaireVisualisation" runat="server" AutoGenerateColumns="False" EmptyDataText="Pas de données"
                        CssClass="Grid" ShowHeaderWhenEmpty="true" OnRowDataBound="gvProduitAffaireVisualisation_RowDataBound" >
                        <Columns>
                            <asp:BoundField DataField="ProduitAffaireID" HeaderText="ProduitAffaireID" Visible="False" />
                            <asp:BoundField DataField="EmployeID" HeaderText="EmployeID" Visible="False"/>
                            <asp:BoundField DataField="Employe" HeaderText="Employé" />
                            <asp:BoundField DataField="ProduitAffaireDate" HeaderText="Date" />
                            <asp:BoundField DataField="ClientNom" HeaderText="Client - Site" />
                            <asp:BoundField DataField="ProduitRef" HeaderText="Référence" />
                            <asp:BoundField DataField="ProduitAffaireLibelle" HeaderText="Libelle" />
                            <asp:BoundField DataField="ProduitAffaireDonneurOrdre" HeaderText="Donneur d'ordre" />
                            <asp:BoundField DataField="ProduitAffairePrixUnit" HeaderText="Prix Unitaire" />
                            <asp:BoundField DataField="ProduitAffaireQte" HeaderText="Quantité" />
                            <asp:BoundField DataField="totalHT" HeaderText="Total HT" />
                            <asp:BoundField DataField="TvaTaux" HeaderText="TVA" />
                            <asp:BoundField DataField="TotalTTC" HeaderText="Total TTC" />                            
                        </Columns>
                    </asp:GridView>
                    </div>
                </fieldset>
            </asp:View>
            <asp:View ID="vVisuEmploye" runat="server">
                <fieldset class="loginUserForm">
                    <legend>Visualisation des données </legend>

            <table style="margin-top: 0px; border-spacing: 10px">
                <tr>
                    <td valign="top">
                        <table style="margin-top: 0px; border-spacing: 10px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPeriode" runat="server" Text="Période:" Font-Underline="true" Font-Bold="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date de début:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbDateDebVisuEmploye" Width="65px" />
                                    <obout:Calendar ID="Calendar5" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                        TextBoxId="tbDateDebVisuEmploye" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                                      </obout:Calendar>
                                </td>
                                <td>
                                    Date de fin:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="tbDateFinVisuEmploye" Width="65px" />
                                    <obout:Calendar ID="Calendar6" runat="server" DatePickerMode="true" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                                        TextBoxId="tbDateFinVisuEmploye" DateFormat="dd/MM/yyyy" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                                        
                                    </obout:Calendar>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table style="margin-top: 0px; border-spacing: 10px">
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Restrictions:" Font-Underline="true"
                                        Font-Bold="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <oboutComboBox:ComboBox runat="server" ID="cbbAffaires2" DataValueField="AffaireID"
                                    DataTextField="AffaireLibelle" width="300" Height="300px" />
                            </td>
                                <td>
                                    <asp:ImageButton ID="btnRechercheVisuEmploye" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png"
                                         ToolTip="Rechercher" />
                                </td>

                                <td>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" Type="Date" Display="Dynamic"
                                    Operator="DataTypeCheck" ControlToValidate="tbDateDebVisuEmploye" ErrorMessage="Veuillez insérer une date de début Valide<br/>" />
                                <asp:CompareValidator ID="CompareValidator5" runat="server" Type="Date" Operator="DataTypeCheck"
                                    ControlToValidate="tbDateFinVisuEmploye" ErrorMessage="Veuillez insérer une date de fin Valide<br/>"
                                    Display="Dynamic" />
                                <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="tbDateDebVisuEmploye"
                                    ControlToCompare="tbDateFinVisuEmploye" Operator="LessThanEqual" Type="Date"
                                    Display="Dynamic" ErrorMessage="La période n'est pas valide<br/>" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="tbDateDebVisuEmploye"
                                    Display="Dynamic" ErrorMessage="Une date de début est requise<br/>" />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="tbDateFinVisuEmploye"
                                    Display="Dynamic" ErrorMessage="Une date de fin est requise<br/>" />
                            </td>
                            
                            </tr>
                        </table>
                    </td>
                </tr>
               
            </table>
                      <div align="center" style="float:none; width: 100%;  overflow: auto; margin-top: 0px;">
                    <asp:GridView ID="gvVisuEmploye" runat="server" AutoGenerateColumns="False" EmptyDataText="Pas de données"
                        CssClass="Grid" ShowHeaderWhenEmpty="true" OnRowDataBound="gvProduitAffaireVisualisation_RowDataBound" >
                        <Columns>
                            <asp:BoundField DataField="ProduitAffaireID" HeaderText="ProduitAffaireID" Visible="False" />
                            <asp:BoundField DataField="EmployeID" HeaderText="EmployeID" Visible="False"/>
                            <asp:BoundField DataField="Employe" HeaderText="Employé" />
                            <asp:BoundField DataField="ProduitAffaireDate" HeaderText="Date" />
                            <asp:BoundField DataField="ClientNom" HeaderText="Client - Site" />
                            <asp:BoundField DataField="ProduitRef" HeaderText="Référence" />
                            <asp:BoundField DataField="ProduitAffaireLibelle" HeaderText="Libelle" />
                            <asp:BoundField DataField="ProduitAffaireDonneurOrdre" HeaderText="Donneur d'ordre" />
                            <asp:BoundField DataField="ProduitAffairePrixUnit" HeaderText="Prix Unitaire" />
                            <asp:BoundField DataField="ProduitAffaireQte" HeaderText="Quantité" />
                            <asp:BoundField DataField="totalHT" HeaderText="Total HT" />
                            <asp:BoundField DataField="TvaTaux" HeaderText="TVA" />
                            <asp:BoundField DataField="TotalTTC" HeaderText="Total TTC" />                            
                        </Columns>
                    </asp:GridView>
                    </div>
                </fieldset>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:Panel ID="pDepassement" CssClass="modalPopup" runat="server" Visible="false"
        Width=" 30% " Height="200px">
        <div id="Div1" runat="server" align="center">
            <br />
            <h2>
                Produit en dépassement</h2>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblDep" runat="server" Text="Attention vous venez de saisir un produit qui dépasse le budget de l'affaire." />
                    </td>
                </tr>
                 <br />
                <tr>
                               
                        <td align="right">
                            <asp:Button ID="btnOK" runat="server" CssClass="btn90" Text="Ok" />
                        </td>
                        <td  align="right">
                            <asp:Button ID="btnAnnuler" runat="server" CssClass="btn90" Text="Annuler" />
                        </td>
                   
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
