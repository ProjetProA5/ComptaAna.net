<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master"
    CodeBehind="EmployeLister.aspx.vb" Inherits="ComptaAna.net.ListerEmploye" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Content">
    <div align="center">
        <h1>Liste des employés</h1>
        <asp:Button ID="btNouvelEmploye" runat="server" CssClass="btn75" Text="Nouveau" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Recherche d'un employé : "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbRechercheEmploye" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="ibRechercheEmploye" runat="server" 
                        ImageUrl="~/App_Themes/ComptaAna/Design/Icon_analyses.png" 
                        ToolTip="Rechercher"/>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div align="center"><asp:Label ID="lMessage" runat="server" ForeColor="red"/></div>
    <div class="content" align="center">
    <asp:GridView HorizontalAlign="Center" AutoGenerateColumns="False" ID="gvEmploye" EmptyDataText="Pas de données"
        CssClass="Grid" runat="server" OnRowDataBound="gvEmploye_RowDataBound" OnRowCommand="gvEmploye_RowCommand" AlternatingRowStyle-BackColor="#E5F3FF">
        <Columns>
            <asp:BoundField DataField="EmployeID" HeaderText="EmployeID" SortExpression="EmployeID"
                Visible="false" />
            <asp:BoundField DataField="EmployeNom" HeaderText="Nom" SortExpression="EmployeNom">
            </asp:BoundField>
            <asp:BoundField DataField="EmployePrenom" HeaderText="Prenom" SortExpression="EmployePrenom">
            </asp:BoundField>
            <asp:BoundField DataField="ServiceLibelle" HeaderText="Service" SortExpression="ServiceLibelle">
            </asp:BoundField>
            <asp:BoundField DataField="QualificationLibelle" HeaderText="Qualification" SortExpression="QualificationLibelle">
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="Modifier" runat="server" align="center"
                        CommandName="modifierEmploye" 
                        CommandArgument='<%# Eval("EmployeID") %>'
                        ImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" 
                        ToolTip="Modifier employé" />

                    <asp:ImageButton ID="Supprimer" runat="server" align="center"
                        CommandName="SupprimerEmploye" 
                        CommandArgument ='<%# Eval("EmployeID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                        OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cet employé ? ' + '\n' 
                                     +'(cette opération supprimera aussi les produits et les affaires liés à cet employé.)');"
                                     ToolTip="Supprimer employé" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="Consulter" runat="server" align="center"
                        CommandName="consulterEmploye" 
                        CommandArgument ='<%# Eval("EmployeID") %>'
                        ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_modifier.png"
                        ToolTip="Consulter employé" />
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
                 var obj = document.getElementById('<%=ibRechercheEmploye.ClientID%>');
                 obj.click();
             }
         }
    </script> 

</asp:Content>
