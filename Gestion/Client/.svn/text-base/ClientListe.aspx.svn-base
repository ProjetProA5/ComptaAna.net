<%@ Page Title="Liste des clients" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="ClientListe.aspx.vb" Inherits="ComptaAna.net.ClientListe" %>   

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div align="center">
    <h1>Liste des clients</h1>
    <asp:Button ID="btInsererClient" runat="server" CssClass="btn75" Text="Nouveau"  />
    </div>
    <asp:Table ID="tRecherche" runat="server" align="center">
        <asp:TableRow>
            <asp:TableCell> 
                <asp:Label runat="server">Recherche d'un client : </asp:Label>
                <asp:TextBox ID="tbRechercheClient" runat="server" />
            </asp:TableCell><asp:TableCell>
                <asp:ImageButton ID="btRechercheClient" runat="server" OnClick="btRechercheClient_Click"
                    CommandName="RechercheClient" CommandArgument ='<%# Eval("ClientID") %>'
                    ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_analyses.png"
                    ToolTip="Rechercher" />
            </asp:TableCell><asp:TableCell>
                <asp:ImageButton ID="ibExporter" runat="server" ImageUrl="~/App_Themes/ComptaAna/Design/Icon_Excel.png" ToolTip="Export Excel" />
            </asp:TableCell></asp:TableRow></asp:Table><br />
<div align="center">
    <asp:Label ID="lMessage" runat="server" ForeColor="red"/>
</div>
<div class="content" align="center">
  <asp:GridView AutoGenerateColumns="False" ID="gvClient" runat="server" EmptyDataText="Pas de données" 
        OnRowCommand="gvClient_RowCommand" OnRowDataBound="gvClient_RowDataBound" cssclass="Grid" AlternatingRowStyle-BackColor="#E5F3FF">
            <Columns>
                <asp:BoundField DataField="ClientID" HeaderText="ClientID" Visible="false"/>
                <asp:BoundField DataField="ClientNom" HeaderText="Nom" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="Modifier" runat="server" align="center"
                        CommandName="ModifierClient" 
                        CommandArgument ='<%# Eval("ClientID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_modifier.png"
                        ToolTip="Modifier client" />

                        <asp:ImageButton ID="Supprimer" runat="server" align="center"
                        CommandName="SupprimerClient" 
                        CommandArgument ='<%# Eval("ClientID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer ce client ?');"
                        ToolTip="Supprimer client"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="Consulter" runat="server" align="center"
                        CommandName="ConsulterClient" 
                        CommandArgument ='<%# Eval("ClientID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_modifier.png"
                        ToolTip="Consulter client" />
                        </ItemTemplate>
                </asp:TemplateField>
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
                 var obj = document.getElementById('<%=btRechercheClient.ClientID%>');
                 obj.click();
             }
         }
    </script> 

</asp:Content>