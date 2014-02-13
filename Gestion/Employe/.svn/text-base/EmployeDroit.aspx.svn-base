<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="EmployeDroit.aspx.vb" Inherits="ComptaAna.net.EmployeDroit" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
    <style type="text/css">
        .style1
        {
            width: 128px;
        }
        .btn90
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div class="content">
        <h1>
            Droits de l'employé</h1>
        <asp:Menu ID="mOnglets" runat="server" Orientation="Horizontal">
            <StaticMenuStyle CssClass="SimpleStaticMenu" />
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover" />
            <Items>
                <asp:MenuItem Value="1" Text="Détails" />
                <asp:MenuItem Value="2" Text="Coût" />
                <asp:MenuItem Value="3" Text="Droits" Selected="true" />
                <asp:MenuItem Value="4" Text="Formation" />
                  <asp:MenuItem Value="5" Text="Echéances annuelles" />
            </Items>
        </asp:Menu>
        <br />
        <fieldset class="loginUserForm">
            <legend>Droits</legend>
            <asp:Label ID="saveOk" ForeColor="Blue" runat="server" Text="L'enregistrement a été effectué correctement"
                Visible="false" />
            <br />
            <table cellspacing="10px" cellpadding="5px" style="border-color: Blue;">
                <tr>
                    <td colspan="2">
                        <fieldset id="fsEmploye" class="loginUserForm">
                            <legend>Droits relatifs aux employés</legend>
                            <table cellspacing="10px" cellpadding="5px" style="border-color: Blue;">
                                <tr>
                                    <td>
                                        Employés (Ecriture)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employés (Lecture)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit2" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employés (Liste)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit3" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employés (Coût/Droit)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit4" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employé (Formations)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit5" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employé (Visu RA)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit14" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <fieldset id="Fieldset1" class="loginUserForm">
                            <legend>Droits relatifs aux clients</legend>
                            <table id="Table1" cellspacing="10px" cellpadding="5px" style="border-color: Blue;">
                                <tr>
                                    <td>
                                        Clients (Ecriture)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit6" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Clients (Lecture)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit7" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Clients (Liste)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit8" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <fieldset id="Fieldset2" class="loginUserForm">
                            <legend>Droits relatifs aux affaires et à la facturation</legend>
                            <table id="Table2" cellspacing="10px" cellpadding="5px" style="border-color: Blue;">
                                <tr>
                                    <td>
                                        Affaires (Ecriture)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit9" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Affaires (Liste)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit10" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Affaires (Facturation)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit11" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Suivi des factures (Tableau)
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit12" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2">
                        <fieldset id="Fieldset3" class="loginUserForm">
                            <legend>Droits relatifs aux statistiques</legend>
                            <table id="Table3" cellspacing="10px" cellpadding="5px" style="border-color: Blue;">
                                <tr>
                                    <td>
                                        Statistiques générales
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit15" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Statistiques frais fixes et variables
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit16" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Statistiques employés par service
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit17" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Statistiques sur les coûts salariaux
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit18" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Graphiques Général
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit22" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Graphiques ResponsableBU
                                    </td>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rbDroit23" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Non</asp:ListItem>
                                            <asp:ListItem Value="1">Oui</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <fieldset id="Fieldset4" class="loginUserForm">
                            <legend>Autres droits</legend>
                        <table id="Table4" cellspacing="10px" cellpadding="5px" style="border-color: Blue;">
                         <tr>
                        <td>
                                    Accès au catalogue
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rbDroit13" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">Non</asp:ListItem>
                                        <asp:ListItem Value="1">Oui</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                                 
                </tr>
                            <tr>
                                <td>
                                    Accès à l'administration
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rbDroit19" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">Non</asp:ListItem>
                                        <asp:ListItem Value="1">Oui</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Administrateur général
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="rbDroit20" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">Non</asp:ListItem>
                                        <asp:ListItem Value="1">Oui</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                          </fieldset>
                    </td>
                </tr>
                <%-- <tr>
                    <td>
                        Accès à la gestion commerciale
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbDroit21" runat="server" RepeatDirection="Horizontal">
                             <asp:ListItem Value= "0" >Non</asp:ListItem>
                            <asp:ListItem Value= "1">Oui</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>--%>
            </table>
        </fieldset>
        <table> <tr> <td> <asp:Button ID="btValider" ValidationGroup="vgInserer" CssClass="btn90"
        runat="server" Text="Enregistrer" CommandArgument ='<%# Eval("ClientID") %>'/> </td>
        <td> <asp:Button ID="btRetourListeEmploye" CssClass="btn95" runat="server" Text="Retour
        à la liste" /> </td> </tr> </table>
    </div>
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
                var obj = document.getElementById('<%=btValider.CLientID%>');
                obj.click();
            }
        }
    </script>
</asp:Content>
