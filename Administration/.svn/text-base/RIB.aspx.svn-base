<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RIB.aspx.vb" Inherits="ComptaAna.net.RIB"
    MasterPageFile="~/SiteMaster.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content" id="dListeCoordonnéesBancaires" align="center">
        <h1 id="hTitreListeCoordonnéesBancaires" runat="server">
            Gestion des Coordonnées Bancaires
        </h1>
        <br />
        <div align="center">
            <asp:Label ID="lblRibID" runat="server" Text="" Visible="false"/>
            <asp:Button ID="btnNouveau" class="btn90" runat="server" Text="Nouveau" />
        </div>
        <asp:Panel Width="80%" runat="server">
            <fieldset id="fsNouveau" runat="server" align="center" visible="false">
            <legend id="legend" runat="server" ><b>Création de nouvelles coordonnées bancaires</b></legend>
                <table>
                    <tr>
                        <td  align="center">
                            <asp:Label ID="lblLibelleRib" runat="server" Text="Nom de la banque : "></asp:Label>
                             </td>
                             <td align="left">
                            <asp:TextBox ID="tbLibelleRib" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        
                     <%--   <td>
                            <asp:RadioButtonList ID="rblIBANouRIB" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">RIB</asp:ListItem>
                                <asp:ListItem>IBAN</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>--%>
                    </tr>
                        <tr>
                         <td align="right">
                                <asp:Label ID="lblCP" runat="server" Text="CP : "></asp:Label>
                                 </td>
                             <td align="left">
                                <asp:TextBox ID="tbCP" Text="FR" runat="server" width="30px" Enabled="false"></asp:TextBox>
                            </td>
                             <td align="right">
                                <asp:Label ID="lblCC" runat="server" Text="CC : "></asp:Label>
                                 </td>
                             <td align="left">
                                <asp:TextBox ID="tbCC" runat="server" MaxLength="2"></asp:TextBox>
                            </td>
                         <td align="right">
                            <asp:Label ID="lblCB" runat="server" Text="Code Banque : "></asp:Label>
                             </td>
                             <td align="left">
                            <asp:TextBox ID="tbCB" runat="server" MaxLength="5"></asp:TextBox>
                        </td>
                           
                           
                        </tr>
                                     <tr>
                       
                        <td align="right">
                            <asp:Label ID="lblCG" runat="server" Text="Code guichet : "></asp:Label>
                             </td>
                             <td align="left">
                            <asp:TextBox ID="tbCG" runat="server" MaxLength="5"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNC" runat="server" Text="N° de compte : "></asp:Label>
                             </td>
                             <td align="left">
                            <asp:TextBox ID="tbNC" runat="server" MaxLength="11"></asp:TextBox>
                        </td>
                         <td align="right">
                            <asp:Label ID="lblCle" runat="server" Text="Clé : "></asp:Label>
                             </td>
                             <td align="left">
                            <asp:TextBox ID="tbCle" runat="server" MaxLength="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                   
                     <td align="right">
                            <asp:Label ID="lblBic" runat="server" Text="Bic : "></asp:Label>
                             </td>
                             <td align="left">
                            <asp:TextBox ID="tbBic" runat="server" Width="150px" MaxLength="11"></asp:TextBox>
                        </td></tr>
                    <tr>

                        <td align="left" colspan="4">
                            <asp:Label ID="lblDefault" runat="server" Text="Utilisé cette coordonnée bancaire par défaut"></asp:Label>
                            <asp:CheckBox ID="cbDefault" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                </table>
                <div align="right">
                    <asp:Button ID="btnEnregistrer" class="btn90" runat="server" Text="Enregistrer" ValidationGroup="vgEnregistrer" />
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rfvLibelleRib" ControlToValidate="tbLibelleRib"
                                Display="Dynamic" ErrorMessage="Un nom est requis<br />" ValidationGroup="vgEnregistrer" />
                            <asp:RangeValidator ID="RangeValidatorCC" runat="server" Type="Integer" MinimumValue="02"
                                Display="Dynamic" MaximumValue="98" ControlToValidate="tbCC" ErrorMessage="CC ne doit être composé que de 2 chiffres compris entre 02 et 98<br />" />
                            <asp:CompareValidator ValidationGroup="vgEnregistrer" ID="CompareValidatorCB" runat="server"
                                Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbCB"
                                ErrorMessage="CB ne doit être composé que de 5 chiffres<br />" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvCB" ControlToValidate="tbCB" Display="Dynamic"
                                ErrorMessage="Veuillez renseigner le champs CB <br />" ValidationGroup="vgEnregistrer" />
                            <asp:CompareValidator ValidationGroup="vgEnregistrer" ID="CompareValidatorCG" runat="server"
                                Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbCG"
                                ErrorMessage="CG ne doit être composé que de 5 chiffres<br />" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvCG" ControlToValidate="tbCG" Display="Dynamic"
                                ErrorMessage="Veuillez renseigner le champs CG<br />" ValidationGroup="vgEnregistrer" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvNC" ControlToValidate="tbNC" Display="Dynamic"
                                ErrorMessage="Veuillez renseigner le champs NC<br />" ValidationGroup="vgEnregistrer" />
                            <%---------------------------------------------------------%>
                            <asp:RequiredFieldValidator runat="server" ID="rf" ControlToValidate="tbNC" Display="Dynamic"
                                ErrorMessage="Veuillez renseigner le champs NC<br />" ValidationGroup="vgEnregistrer" />
                            <asp:CompareValidator ValidationGroup="vgEnregistrer" ID="CompareValidatorCle" runat="server"
                                Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbCle"
                                ErrorMessage="La clé ne doit être composée que de 2 chiffres" />
                            <asp:Label ID="lblMsgError" runat="server" Text="" align="left"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>

        <asp:Label ID="lblMsg" runat="server" Text="" align="left"></asp:Label>
        <asp:GridView ID="gvCoordonnéesBancaires" AutoGenerateColumns="False" runat="server"
            CssClass="Grid" EmptyDataText="Pas de Coordonnées Bancaires" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:BoundField DataField="RibID"/>
                <asp:BoundField DataField="RibLibelle" HeaderText="Nom banque" />
                <asp:BoundField DataField="Cp" HeaderText="Code pays" />
                <asp:BoundField DataField="Cc" HeaderText="Clef de contrôle" />
                <asp:BoundField DataField="Cb" HeaderText="Code Banque" />
                <asp:BoundField DataField="Cg" HeaderText="Code guichet" />
                <asp:BoundField DataField="Nc" HeaderText="N° de compte" />
                <asp:BoundField DataField="Cle" HeaderText="clé" />
                <asp:BoundField DataField="Bic" HeaderText="Bic" />
                <asp:TemplateField HeaderText="Modification" Visible="true">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnModification" runat="server" CommandName="ModificationCoordonnesBancaires"
                            Visible="true" CommandArgument='<%# Eval("RibID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png"
                            ToolTip="Modifier cette Coordonnée bancaire" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supprimer" Visible="true">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnSupprimer" runat="server" CommandName="SuppressionCoordonnesBancaires"
                            Visible="true" CommandArgument='<%# Eval("RibID") %>' ImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                            ToolTip="Supprimer cette Coordonnée bancaire" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
