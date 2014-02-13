Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class AffaireSousAffaireListe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesFacture) Then
                    mMenuAffaireModif.FindItem("2").Enabled = False
                End If
                If Not verifDroit(lDroit, eModule.AccesAffaireLecture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
                If Not verifDroit(lDroit, eModule.AccesAffaireEcriture) Then
                    btnNouveau.Visible = False
                    gvListeSousAffaire.Columns(3).Visible = False
                    ' mMenuAffaireModif.FindItem("0").Enabled = False
                End If
             
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try

            lblSousAffaire.Text = "Liste des sous affaires"
            If Not Request.QueryString("affaire") = "" Then
                LoadListe()
            End If
        End If
        Dim iAffaireID As New Integer

        iAffaireID = CInt(Request.QueryString("affaire"))

        mMenuAffaireModif.Items(4).Selected = True
    End Sub

    Protected Sub LoadListe()
        Dim oAffaireDAO As New CAffaireDAO
        Dim iAffaireID As New Integer

        iAffaireID = CInt(Request.QueryString("affaire"))

        gvListeSousAffaire.DataSource = oAffaireDAO.GetListeSousAffaire(iAffaireID)
        gvListeSousAffaire.DataBind()

    End Sub

    Protected Sub btnRetourFiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetourFiche.Click
        If Not Request.QueryString("affaire") = "" Then
            Response.Redirect("AffaireModifier.aspx?id=" & Request.QueryString("affaire"))
        End If
    End Sub


    Protected Sub gvListeSousAffaire_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListeSousAffaire.RowCommand
        If e.CommandName = "ModifierSousAffaire" Then
            Response.Redirect("AffaireModifier.aspx?id=" & CInt(e.CommandArgument))
        ElseIf e.CommandName = "SupprimerSousAffaire" Then
            Dim oAffaireDAO As New CAffaireDAO
            Dim iSupprimer As Integer = oAffaireDAO.DeleteAffaire(CInt(e.CommandArgument))
            If iSupprimer = 0 Then
                lblSousAffaire.Text = "Suppression effectuée!"
            ElseIf iSupprimer = 1 Then
                lblSousAffaire.Text = "Suppression échouée, l'affaire possède au moins un produit!"
            End If
            LoadListe()
        End If
    End Sub

    Private Sub btnRetourListe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetourListe.Click
        Try

            Response.Redirect("AffaireLister.aspx?affaire=" & CInt(Request.QueryString("id")))

        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try
    End Sub

    Private Sub btnNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNouveau.Click
        Response.Redirect("AffaireInserer.aspx?affaire=" & CInt(Request.QueryString("affaire")))
    End Sub

    Private Sub mMenuAffaireModif_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mMenuAffaireModif.MenuItemClick
        Dim iAffaireID As Integer = CInt(Request.QueryString("affaire"))

        If e.Item.Value = "0" Then
            Response.Redirect("AffaireModifier.aspx?id=" & iAffaireID)
        ElseIf e.Item.Value = "1" Then
            Response.Redirect("AffaireProduits.aspx?id=" & iAffaireID)
        ElseIf e.Item.Value = "2" Then
            If Not CConfiguration.NouvelleVersion Then
                Response.Redirect("~/Gestion/Affaire/AffaireFacture.aspx?id=" & iAffaireID)
            Else
                Response.Redirect("~/Gestion/Affaire/AffaireFacturation.aspx?id=" & iAffaireID)
            End If

        ElseIf e.Item.Value = "3" Then
                Response.Redirect("AffaireSousAffaireListe.aspx?affaire=" & iAffaireID)

            End If


    End Sub

End Class