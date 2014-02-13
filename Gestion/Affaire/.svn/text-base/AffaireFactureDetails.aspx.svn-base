<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="AffaireFactureDetails.aspx.vb" Inherits="ComptaAna.net.AffaireFactureDetails" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content" runat="server">
    <h1>Détails d'une facture</h1>

    <div class="content" align="center">
        <fieldset style="width:21cm; background: url(../../App_Themes/ComptaAna/Design/Facture_bg.png) center bottom no-repeat;">
        <table width="100%" style="background: url(../../App_Themes/ComptaAna/Design/Logo_Axe.png) left top no-repeat;">
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
                <td align="right" valign="bottom" colspan="2">
                    <asp:Label ID="lblFactureRefetDate" runat="server" Font-Bold="true" ForeColor="Black" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="lblAxeTVA" runat="server" Text="N°TVA intracommunautaire AXE : FR 92 429 489 966" ForeColor="Black" />
                    <br />
                    <asp:Label ID="lblClientTVA" runat="server" ForeColor="Black" />
                </td>
            </tr>
        </table>
        
        <br />
        <br />
        <asp:GridView AutoGenerateColumns="False" ID="gvProduitFacture" runat="server" EmptyDataText="Pas de données" 
            OnRowDataBound="gvProduitFacture_RowDataBound" CssClass="Grid" ShowHeaderWhenEmpty="true" Width="100%" DataKeyNames="TvaTaux, Tva" >
            <Columns>
                <asp:BoundField DataField="ProduitAffaireDate" HeaderText="date" DataFormatString="{0:d}" />
                <asp:BoundField DataField="ProduitAffaireLibelle" HeaderText="Désignation" ItemStyle-Width="61.8%"  />
                <asp:BoundField DataField="ProduitAffaireQte" HeaderText="Nbre" />
                <asp:BoundField DataField="ProduitAffaireMntUnitHT" HeaderText="PU €uros" />
                <asp:BoundField DataField="TotalHT" HeaderText="Total €uros" />
            </Columns>
        </asp:GridView>

        <br />

        <Table width="100%" class="Grid" >
            <tr>
                <th align="left" width="88%">
                    MONTANT NET TOTAL A PAYER EN €UROS A RECEPTION DE FACTURE          
                </th>
                <td><asp:Label ID="lblToutTotal" runat="server" ForeColor="red" Font-Bold="true" /></td>
            </tr>
        </Table>

        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        </fieldset>
       </div><asp:Button ID="btnRetour" runat="server" Text="Retour" CssClass="btn75" />
</asp:Content>
