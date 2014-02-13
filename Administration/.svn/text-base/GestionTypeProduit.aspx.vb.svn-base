Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class GestionTypeProduit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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


        LoadGridView()
        lblEnregistrement.Visible = False
    End Sub

    ''' <summary>
    ''' Charge le gridView
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadGridView()
        Dim oTypeProduitDAO As New CTypeProduitDAO
        Dim dsTypeProduit As DataSet

        dsTypeProduit = oTypeProduitDAO.getAllTypeProduit

        gvTypeProduit.DataSource = dsTypeProduit
        gvTypeProduit.DataBind()
    End Sub

    ''' <summary>
    ''' control pour supprimer un type de produit
    ''' </summary>
    Protected Sub gvTypeProduit_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "SupprimerTypeProduit" Then
            Dim oTypeProduitDAO As CTypeProduitDAO = New CTypeProduitDAO
            Dim iDelete As Integer = oTypeProduitDAO.DeleteTypeProduit(CInt(e.CommandArgument.ToString))

            Dim ds As DataSet
            ds = oTypeProduitDAO.GetAllTypeProduit()
            gvTypeProduit.DataSource = ds
            gvTypeProduit.DataBind()

            If iDelete = 0 Then
                lblMsgSupp.Text = "Suppression effectuée!"
            ElseIf iDelete = 1 Then
                lblMsgSupp.Text = "Suppression echouée, ce type de produit est lie à au moins un produit. "
            End If
        End If
    End Sub



    ''' <summary>
    ''' Enregistre le gridview sur le click du bouton enregistrer
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnregistrer.Click
        Dim oTypeProduit As CTypeProduit
        Dim oTypeProduitDAO As New CTypeProduitDAO

        Dim iCount As Integer = gvTypeProduit.Rows.Count

        For i = 0 To iCount - 1
            Dim cbTypeProduitAffaire As CheckBox = CType(gvTypeProduit.Rows(i).Cells(1).FindControl("cbTypeProduitAffaire"), CheckBox)
            Dim cbTypeProduitJournee As CheckBox = CType(gvTypeProduit.Rows(i).Cells(3).FindControl("cbTypeProduitJournee"), CheckBox)
            Dim cbTypeProduitCA As CheckBox = CType(gvTypeProduit.Rows(i).Cells(4).FindControl("cbTypeProduitCA"), CheckBox)
            Dim cbTypeProduitBudgetAffaire As CheckBox = CType(gvTypeProduit.Rows(i).Cells(2).FindControl("cbTypeProduitBudgetAffaire"), CheckBox)
            Dim cbTypeProduitCAAxe As CheckBox = CType(gvTypeProduit.Rows(i).Cells(5).FindControl("cbTypeProduitCAAxe"), CheckBox)
            Dim cbTypeProduitFactInterne As CheckBox = CType(gvTypeProduit.Rows(i).Cells(7).FindControl("cbTypeProduitFactInterne"), CheckBox)
            Dim cbTypeProduitFactQualif As CheckBox = CType(gvTypeProduit.Rows(i).Cells(6).FindControl("cbTypeProduitFactQualif"), CheckBox)
            Dim cbTypeProduitVisible As CheckBox = CType(gvTypeProduit.Rows(i).Cells(8).FindControl("cbTypeProduitVisible"), CheckBox)

            Dim tbLibelle As TextBox = CType(gvTypeProduit.Rows(i).Cells(0).FindControl("tbLibelle"), TextBox)
            Dim iTypeProduitID As Int32 = CType(CType(gvTypeProduit.Rows(i).Cells(9).FindControl("lblTypeProduitID"), Label).Text, Int32)


            oTypeProduit = New CTypeProduit(iTypeProduitID, tbLibelle.Text,
                    CBool(cbTypeProduitAffaire.Checked), CBool(cbTypeProduitJournee.Checked),
                    CBool(cbTypeProduitCA.Checked), CBool(cbTypeProduitBudgetAffaire.Checked),
                    CBool(cbTypeProduitCAAxe.Checked), CBool(cbTypeProduitFactInterne.Checked),
                    CBool(cbTypeProduitFactQualif.Checked), CBool(cbTypeProduitVisible.Checked))

            oTypeProduitDAO.UpdateTypeProduit(oTypeProduit)
        Next
        lblEnregistrement.Visible = True
    End Sub

    ''' <summary>
    ''' Ajoute un nouveau type de produit
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnAjouterNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjouterNouveau.Click
        Dim oTypeProduit As CTypeProduit
        Dim oTypeProduitDAO As New CTypeProduitDAO

        oTypeProduit = New CTypeProduit(tbNouveauLibelle.Text,
                   CBool(cbNouveauTypeProduitAffaire.Checked), CBool(cbNouveauTypeProduitJournee.Checked),
                   CBool(cbNouveauTypeProduitCA.Checked), CBool(cbNouveauTypeProduitBudgetAffaire.Checked),
                   CBool(cbNouveauTypeProduitCAAxe.Checked), CBool(cbNouveauTypeProduitFactInterne.Checked),
                   CBool(cbNouveauTypeProduitFactQualif.Checked), CBool(cbNouveauTypeProduitVisible.Checked))

        oTypeProduitDAO.InsertTypeProduit(oTypeProduit)

        LoadGridView()
    End Sub
End Class