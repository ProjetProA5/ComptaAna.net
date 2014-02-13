<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="EmployeCout.aspx.vb" Inherits="ComptaAna.net.EmployeCout" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.TreeView" TagPrefix="obout" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn175
        {
        }
    </style>
</asp:Content>--%>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <asp:ScriptManager ID="ScriptManagerTEST" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>

        <h1>Coût de l'employé</h1>
        <asp:Menu ID="mOnglets" runat="server" Orientation="Horizontal">
            <StaticMenuStyle CssClass="SimpleStaticMenu" />
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover" />
            <Items>
                <asp:MenuItem Value="1" Text="Détails"/>
                <asp:MenuItem Value="2" Text="Coût" Selected="true" />
                <asp:MenuItem Value="3" Text="Droits" />
                   <asp:MenuItem Value="4" Text="Formation" />
                     <asp:MenuItem Value="5" Text="Echéances annuelles" />
            </Items>
        </asp:Menu>
        <br />
        <table cellspacing="10px">
        <tr>
        <td>
        <asp:Button ID="btNouveauCout" runat="server" CssClass="btn95" Text="Nouveau coût" visible="true"/>
       </td>
       <td>
        <asp:Button ID="btNouvellePrime" runat="server" CssClass="btn95" Text="Nouvelle prime" visible="true"/>
   </td>
       <td>
        <asp:Button ID="btHistorique" runat="server" CssClass="btn225" Text="Historique primes et augmentations" visible="true"/></td>
        </tr>
        </table>
                
        <br />

        <div id="TreeView" style="float: left; height: 600px; width: 200px; overflow: auto;">
            <obout:Tree ID="tvEmployeCout" runat="server"  CssClass="vista" 
                OnSelectedTreeNodeChanged="tvEmployeCout_SelectedTreeNodeChanged" ViewStateMode="Inherit" Visible="true">
            </obout:Tree>
                <br />
                    <br />
              <asp:Button ID="btRetourListeEmploye" CssClass="btn95" runat="server" Text="Retour à la liste" />
             
        </div>

        <asp:Button ID="btSupprimer" runat="server" CssClass="btn75" Text="Supprimer" visible="false"/>

        <asp:CheckBox ID="cbNouveauCout" runat="server" Checked="false" Visible="false" />
        <asp:Label ID="lSuppressionImpossible" runat="server" BorderColor="Black" ForeColor="Red"
                Text="La suppression est impossible car le coût de cette période est achevée" Visible="False" />
        <asp:Label ID="lEnregistrementReussi" runat="server" BorderColor="Black" ForeColor="Blue"
            Text="L'enregistrement a été effectué" Visible="False" />
        <fieldset ID="fCoutEmploye" runat="server" class="loginUserForm" visible="false">
            <legend>Cout de l'employé</legend>
            <table>
            <tr>
                <td>Libelle : </td>
                <td class="style1"><asp:TextBox ID="tbLibelleCout" runat="server"/></td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvTbLibelle" ValidationGroup="vgCout" runat="server"
                        Display="Dynamic" ControlToValidate="tbLibelleCout" ErrorMessage="Le libelle est obligatoire"> </asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td>Augmentation : </td>
                <td class="style1"><asp:CheckBox ID="cbAugmentation" AutoPostBack="true" runat="server"/></td>
               
            </tr>
                 
            <tr>
             <td><asp:label ID="lblTx" runat="server" Text="Taux : " Visible="false"/> </td>
                <td> <asp:TextBox ID="tbTaux" runat="server" Visible="false" />
                </td></tr>
               
            <tr>
                <td>Coût global :</td>
                <td class="style1"><asp:TextBox ID="tbCoutGlobal" runat="server"/></td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvTbCoutGlobal" ValidationGroup="vgCout" runat="server"
                        Display="Dynamic" ControlToValidate="tbCoutGlobal" ErrorMessage="Le Coût global est obligatoire"> </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTbCoutGlobal" ValidationGroup="vgCout" runat="server"
                        Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbCoutGlobal"
                        ErrorMessage="Veuillez insérer un nombre"> </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Date début :</td>
                <td class="style1"><asp:TextBox ID="tbDateDebut" runat="server" Width="65px" />
                    <obout:Calendar ID="cCalendrier1" runat="server" DateFormat="dd/MM/yyyy" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                        DatePickerMode="true" TextBoxId="tbDateDebut" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                    </obout:Calendar>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvTbDateDebut" ValidationGroup="vgCout" runat="server"
                        Display="Dynamic" ControlToValidate="tbDateDebut" ErrorMessage="La date de début est obligatoire"> </asp:RequiredFieldValidator>
                    <asp:Label ID="lPeriodeIncorrecte" runat="server" BorderColor="Black" ForeColor="Red"
                Text="La période que vous avez choisi n'est pas correcte par rapport aux autres coûts" Visible="False" />
                <asp:Label ID="LabTauxAuguementationInfAUn" runat="server" BorderColor="Black" ForeColor="Red"
                Text=" un taux d'augmentation ne peut être inférieur a 1" Visible="False" />
                </td>
            </tr>
        </table>
        </fieldset>

        <fieldset ID="fsPrime" runat="server" class="loginUserForm" visible="false">
            <legend>Nouvelle prime</legend>
            <table>
            <tr>
                <td>Montant : </td>
                <td class="style1"><asp:TextBox ID="tbMontantPrime" runat="server"/></td>
                 <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vgPrime" runat="server"
                        Display="Dynamic" ControlToValidate="tbMontantPrime" ErrorMessage="Le montant de la prime est obligatoire"> </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vgPrime" runat="server"
                        Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*" ControlToValidate="tbMontantPrime"
                        ErrorMessage="Veuillez insérer un nombre"> </asp:RegularExpressionValidator>
                </td>
            </tr>
            
            <tr>
                <td>Date de la prime :</td>
                <td class="style1"><asp:TextBox ID="tbDatePrime" runat="server" Width="65px" />
                    <obout:Calendar ID="Calendar1" runat="server" DateFormat="dd/MM/yyyy" DatePickerImagePath="~/App_Themes/ComptaAna/Design/Icon_datePicker.gif"
                        DatePickerMode="true" TextBoxId="tbDatePrime" CultureName="fr-FR" ShortDayNames="Di,Lu,Ma,Me,Je,Ve,Sa">
                    </obout:Calendar>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="vgPrime" runat="server"
                        Display="Dynamic" ControlToValidate="tbDatePrime" ErrorMessage="La date de la prime est obligatoire"> </asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
            <td>Avantage en nature : 
            </td>
            <td>
            <asp:CheckBox ID="cbAvNat" AutoPostBack="true" runat="server" />
            </td>
            </tr>
            <tr>
            <td><asp:Label ID="lblAvNat" Text="Modèle de voiture :" runat="server" visible="false" /> </td>
            <td>
            <asp:TextBox ID="tbModele" runat="server" visible="false"></asp:TextBox></td>
            </tr>
             <tr>
                <td><asp:Button ID="btEnregistrerPrime" ValidationGroup="vgPrime" CssClass="btn95" runat="server"   CommandArgument ='<%# Eval("ClientID") %>'
                 Text="Enregistrer" visible="false" /></td>
                 </tr>
        </table>
        </fieldset>
        <%--<div align="center" style="float: none; width: 100%; overflow: auto; margin-top: 0px;">--%>
                <asp:Label ID="lblConfirmation" runat="server" Visible="false" />
                <br />
                <table cellspacing="20px">
                <tr>
                <td valign="top">
                      <h1 id="lblPrime" runat="server" visible="false" class="h1">PRIMES:</h1>
                <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvPrime"
                    EmptyDataText="Pas de données" CssClass="Grid" runat="server" Visible="false" AlternatingRowStyle-BackColor="#E5F3FF">
                    <Columns>
                        <asp:BoundField DataField="EmployePrimeID" HeaderText="EmployePrimeID" SortExpression="FormationID"
                            Visible="false" />
                        <asp:BoundField DataField="EmployeID" HeaderText="Employé"  Visible="false" ></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeDate" HeaderText="Date"></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeMontant" HeaderText="Montant"></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeTypeAvNature" HeaderText="Avantage en nature"></asp:BoundField>
                        <asp:BoundField DataField="EmployePrimeModeleAvNature" HeaderText="Modèle voiture"></asp:BoundField>
                        <asp:TemplateField>
                          <ItemTemplate>
                            <asp:ImageButton ID="BtnDelete" runat="server" CommandName="SupprimerPrime"
                            CommandArgument ='<%# Eval("EmployePrimeID") %>'
                                ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                                OnClientClick="return confirm('Êtes-vous sur de vouloir supprimer cette prime');"
                                ToolTip="Supprimer prime" />
                        </ItemTemplate>
                              </asp:TemplateField>
                        </Columns>
                </asp:GridView>
                </td>
                <td valign="top">
                             <h1 id="lblAug" runat="server" visible="false" class="h1">AUGMENTATIONS:</h1>
 <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvAugmentation"
                    EmptyDataText="Pas de données" CssClass="Grid" runat="server" Visible="false" AlternatingRowStyle-BackColor="#E5F3FF">
                    <Columns>
                        <asp:BoundField DataField="EmployeCoutID" HeaderText="EmployePrimeID" SortExpression="FormationID"
                            Visible="false" />
                        <asp:BoundField DataField="EmployeID" HeaderText="Employé"  Visible="false" ></asp:BoundField>
                        <asp:BoundField DataField="EmployeCoutDateDebut" HeaderText="Date"></asp:BoundField>
                        <asp:BoundField DataField="EmployeCoutCout" HeaderText="Montant"></asp:BoundField>
                        <asp:BoundField DataField="EmployeCoutTaux" HeaderText="Taux d'augmentation"></asp:BoundField>
                             </Columns>
                </asp:GridView>
                </td>
                </tr>
                </table>
                
<%--            </div>--%>

        <fieldset ID="fRepartitionFiliale" runat="server"  class="loginUserForm" visible="false">
            <legend>Répartition filiale</legend>
            <asp:Label ID="lbVerifReparition" runat="server" BorderColor="Black" ForeColor="Red"
                Text="La répatition des filiales n'est pas égale à 100%" Visible="False" />
            <asp:GridView HorizontalAlign="Left" AutoGenerateColumns="False" ID="gvRepartition" EmptyDataText="Pas de données"
                CssClass="Grid" runat="server" OnRowCreated="gvRepartition_RowCreated">
                <Columns>
                    <asp:BoundField DataField="FilialeID" HeaderText="FilialeID" SortExpression="FilialeID" />
                    <asp:BoundField DataField="FilialeNom" HeaderText="Nom" SortExpression="FilialeNom" />
                    <asp:TemplateField HeaderText="Pourcentage">
                        <ItemTemplate>
                            <asp:TextBox ID="tbPourcent" runat="server" Width="72px" Text='<%# Bind("Repartition") %>' />%</ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
        <asp:RadioButtonList ID="rdFacturable" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" Visible="false">
            <asp:ListItem Selected="true">Facturable</asp:ListItem>
            <asp:ListItem> Non Facturable</asp:ListItem>
        </asp:RadioButtonList>
        <fieldset ID="fNonFacturable" runat="server"  class="loginUserForm" visible="false">
            <legend>Non facturable <asp:CheckBox ID="cbNonFacturable" runat="server" Visible="false" /></legend>
            <asp:Label ID="lbVerifReparitionService" runat="server" BorderColor="Black" ForeColor="Red"
                Text="La répatition des services n'est pas égale à 100%" Visible="False"></asp:Label>
            <asp:GridView ID="gvRepartitionService" runat="server" AutoGenerateColumns="False" EmptyDataText="Pas de données"
                CssClass="Grid" HorizontalAlign="Left" OnRowCreated="gvRepartitionService_RowCreated" visible="true">
                <Columns>
                    <asp:BoundField DataField="ServiceID" HeaderText="ServiceID" SortExpression="ServiceID" />
                    <asp:BoundField DataField="ServiceLibelle" HeaderText="Service" SortExpression="ServiceLibelle" />
                    <asp:TemplateField HeaderText="Pourcentage">
                        <ItemTemplate>
                            <asp:TextBox ID="tbPourcentService" runat="server" Text='<%# Bind("Repartition") %>'
                                Width="36px" />%
                            <asp:RegularExpressionValidator ID="revTbPourcentService" ValidationGroup="vgCout"
                                runat="server" Display="Dynamic" ValidationExpression="[-+]?[0-9]*[.|,]?[0-9]*"
                                ControlToValidate="tbPourcentService" ErrorMessage="Veuillez insérer un nombre">
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>

    <table>
            <tr>
                <td><asp:Button ID="btEnregistrer" ValidationGroup="vgCout" CssClass="btn95" runat="server"   CommandArgument ='<%# Eval("ClientID") %>'
                 Text="Enregistrer" visible="false" /></td>

                 
            </tr>
    </table>
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
                    var obj = document.getElementById('<%=btEnregistrer.CLientID%>');
                    obj.click();
                }
            }
    </script>
</asp:Content>
