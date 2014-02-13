<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="GestionParametreGlobal.aspx.vb" Inherits="ComptaAna.net.GestionParametreGlobal" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="oboutComboBox" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="content">
        <h1>
            Type Echéances annuelles
        </h1>
        <asp:Menu ID="mOnglets" runat="server" Orientation="Horizontal">
            <StaticMenuStyle CssClass="SimpleStaticMenu" />
            <StaticMenuItemStyle CssClass="SimpleStaticMenuItem" />
            <StaticSelectedStyle CssClass="SimpleStaticSelected" />
            <StaticHoverStyle CssClass="SimpleStaticHover" />
            <Items>
                <asp:MenuItem Value="1" Text="Echéances annuelles" Selected="true" />
            </Items>
        </asp:Menu>
        <br />
    </div>
    <table cellspacing="20px">
        <tr>
            <td>
                <asp:Button CssClass="btn175" ID="btnAjoutTypeEcheance" runat="server" Text="Ajouter un type d'échéance" />
            </td>
            <td>
                <asp:Button CssClass="btn175" ID="btnEffacerTypeEcheance" runat="server" Text="Effacer un type d'échéance" />
            </td>
        </tr>
    </table>
    <fieldset id="fsEffacerTypeEcheance" runat="server" visible="false">
        <legend>Effacer un type d'échéance annuelle </legend>
    
        <%--OnDataBinding="gvDataBinding--%>
        <%-- OnRowEditing="gvEcheance_RowEditing"--%>
       <%-- OnRowCommand="Update_RowCommand"--%>
        <asp:GridView AutoGenerateColumns="False" runat="server" CssClass="Grid" ID="gvEffacerTypeEcheanceAnnuelle"
            EmptyDataText="Pas de données" AlternatingRowStyle-BackColor="#EDEDED" OnRowUpdating="gvEcheance_RowUpdating"  >
            <Columns>
                <asp:BoundField DataField="TypeEcheanceAnnuelleID" HeaderText="EmployeEcheanceAnnuelleID"
                    Visible="false" />
                <asp:BoundField DataField="TypeEcheanceAnnuelleLibelle" HeaderText=" Type Echeance Annuelle Libelle "
                    HtmlEncode="False" />
                <asp:CheckBoxField DataField="TypeEcheanceAnnuelleActif" HeaderText=" Activé " runat="server" />
                <asp:buttonfield buttontype="Button" 
                  commandname="Update" text="update" />
               <%-- <asp:CommandField ButtonType="Image" ItemStyle-HorizontalAlign="Center" ShowDeleteButton="false"
                    ShowEditButton="True" HeaderText="" DeleteImageUrl="~/App_Themes/ComptaAna/Design/Icon_supprimer.png"
                    EditImageUrl="~/App_Themes/ComptaAna/Design/Icon_modifier.png" UpdateImageUrl="~/App_Themes/ComptaAna/Design/Icon_tick.png"
                    CancelImageUrl="~/App_Themes/ComptaAna/Design/Icon_cross.png" />--%>
            </Columns>
        </asp:GridView>
    </fieldset>
    <fieldset id="fsNouveauTypeEcheance" runat="server" visible="false">
        <legend>Nouveau type d'échéance </legend>
        <table cellspacing="20px">
            <tr>
                <td>
                    <asp:Label ID="LabTypeEcheance" runat="server" Font-Bold="true" Text="Type d'échéance : " />
                </td>
                <td align="left">
                    <asp:TextBox ID="tbTypeEcheance" runat="server" Width="140px" />
                </td>
                <td>
                    <asp:Label ID="LabTypeEcheanceErreur" runat="server" Font-Bold="true" Text="" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button CssClass="btn95" ID="btnEnregistrerTypeEcheance" runat="server" Text="Enregistrer" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
