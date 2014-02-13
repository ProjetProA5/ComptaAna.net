Imports ComptaAna.Business
Imports System.Data
Imports ComptaAna.net.Droit

Public Class GestionPoste
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Try
            '    If Not CType(Session("Employe"), CEmploye).ProfilID = 3 Then
            '        Response.Redirect("~/Login.aspx?Erreur=403")
            '    End If
            'Catch ex As NullReferenceException
            '    Response.Redirect("~/Login.aspx?Erreur=401")
            'End Try

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesAdministration) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try


            LoadGridview()

        End If
    End Sub

    Protected Sub LoadGridview()
        Dim oPosteDAO As New CPosteDAO
        gvPoste.DataSource = oPosteDAO.GetAllPoste()
        gvPoste.DataBind()
    End Sub

    Protected Sub gvPoste_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "SupprimerPoste" Then
            Dim oPosteDAO As New CPosteDAO

            Dim iDelete As Integer = oPosteDAO.DeletePoste(CInt(e.CommandArgument))

            LoadGridview()

            If iDelete > 0 Then
                lblMsg.Text = "Suppression réussie!"
            Else
                lblMsg.Text = "Suppression échouée!"
            End If
        End If
    End Sub

    Private Sub btnAjouterNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjouterNouveau.Click
        Dim oPosteDAO As New CPosteDAO
        Dim iInsert As Integer = oPosteDAO.InsertPoste(tbNouveauNom.Text)
        LoadGridview()
        If iInsert > 0 Then
            lblMsg.Text = "Insertion réussie!"
        Else
            lblMsg.Text = "Insertion échouée!"
        End If
    End Sub
End Class