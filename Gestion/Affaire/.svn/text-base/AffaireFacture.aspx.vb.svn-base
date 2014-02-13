Imports ComptaAna.Business
Imports Obout.ComboBox
Imports ComptaAna.net.Droit

Public Class AffaireFacture
    Inherits System.Web.UI.Page
    Dim iAffaireID As Integer

    ''' <summary>
    ''' chargement de la page
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        iAffaireID = CInt(Request.QueryString("id"))
        Dim oAffaireDAO As New CAffaireDAO
        Dim sLibelle As String = oAffaireDAO.GetAffaireLibelle(iAffaireID)

        If Not Page.IsPostBack Then
            LoadTreeView()

            btnModification.Visible = False
            btnSuppression.Visible = False
            btnProduits.Visible = False

            'Try
            '    If CType(Session("Employe"), CEmploye).ProfilID = 1 Then
            '        pNouvelleFacture.Visible = False
            '    End If
            'Catch ex As NullReferenceException
            '    Response.Redirect("~/Login.aspx?Erreur=401")
            'End Try

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesFacture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")

                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try


            gvFactureSelectProduit.Visible = False
            gvProduitPourcentage.Visible = False
            btnConfirmPourcentage.Visible = False
            gvManuelProduit.Visible = False
            
            tbLibelle.Text = sLibelle
            rbAffaireProduitType.Visible = False
            btnChoix.Visible = False

            Dim oFactureAffaireDAO As New CFactureAffaireDAO
            lblMaxReference.Text = oFactureAffaireDAO.GetMaxFactureRef().ToString

            mMenuAffaireModif.Items(3).Selected = True
        End If
    End Sub

    Private Sub LoadTreeView()
        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim ds As DataSet = oFactureAffaireDAO.GetFactureAffaireByAffaireID(iAffaireID)

        If Not IsCallback Then
            tvFacture.Nodes.Clear()
        End If

        'branche parent
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim tnTypeProduit As Obout.Ajax.UI.TreeView.Node = New Obout.Ajax.UI.TreeView.Node(CStr(ds.Tables(0).Rows(i)("FactureAffaireRef")) & ", " & CStr(ds.Tables(0).Rows(i)("FactureAffaireDate")))
            tnTypeProduit.Value = CStr(ds.Tables(0).Rows(i)("FactureAffaireID"))
            tnTypeProduit.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_facture.png"
            tvFacture.Nodes.Add(tnTypeProduit)
        Next
    End Sub

    Private Sub SelectionProduit()
        'on sélectionne les produits concernes
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduits As DataSet
        dsProduits = oProduitAffaireDAO.GetProduitAffaireAssocieByAffaireIDetDateFacture(iAffaireID, CDate(tbFactureDate.Text))

        gvProduitPourcentage.Visible = False
        btnConfirmPourcentage.Visible = False
        gvManuelProduit.Visible = False
        gvFactureSelectProduit.Visible = True

        gvFactureSelectProduit.DataSource = dsProduits
        gvFactureSelectProduit.DataBind()

    End Sub

    Private Sub EditPercentage()
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim dsPourcentage As DataSet = oAffaireEtapeFactureDAO.GetFactureProduitPourcentage(iAffaireID)

        gvFactureSelectProduit.Visible = False
        gvManuelProduit.Visible = False
        gvProduitPourcentage.Visible = True
        btnConfirmPourcentage.Visible = True

        gvProduitPourcentage.DataSource = dsPourcentage
        gvProduitPourcentage.DataBind()
    End Sub

    Private Sub ManuelProduit()
        Dim oFactureAffaireDAO As New CFactureAffaireDAO

        gvFactureSelectProduit.Visible = False
        gvProduitPourcentage.Visible = False
        btnConfirmPourcentage.Visible = False
        gvManuelProduit.Visible = True

        Dim dt As New DataTable()
        dt.Columns.Add("ProduitLibelle")
        dt.Columns.Add("Prix")
        dt.Columns.Add("Quantite")
        dt.Columns.Add("TVA")
        dt.Columns.Add("TotalHT")
        dt.Rows.Add(dt.NewRow())

        gvManuelProduit.DataSource = dt
        gvManuelProduit.DataBind()

        gvManuelProduit.Rows(0).Visible = False

    End Sub

    Private Sub RadioBoutonSelection()
        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim iFactureMax As New Integer
        iFactureMax = oFactureAffaireDAO.GetMaxFactureAffaireID()

        If rbAffaireProduitType.SelectedIndex = 0 Then
            oFactureAffaireDAO.UpdateFactureAffaireType(iFactureMax, 1)
            SelectionProduit()
        ElseIf rbAffaireProduitType.SelectedIndex = 1 Then
            oFactureAffaireDAO.UpdateFactureAffaireType(iFactureMax, 2)
            EditPercentage()
        ElseIf rbAffaireProduitType.SelectedIndex = 2 Then
            oFactureAffaireDAO.UpdateFactureAffaireType(iFactureMax, 3)
            ManuelProduit()
        End If
    End Sub

    Protected Sub gvFactureSelectProduit_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFactureSelectProduit.RowCommand
        If e.CommandName = "SelectionnerProduit" Then
            Dim oFactureAffaireDAO As New CFactureAffaireDAO
            Dim iFactureMax As New Integer
            iFactureMax = oFactureAffaireDAO.GetMaxFactureAffaireID()

            Dim nombreProduit As Integer = 0
            Dim nombreTitre As Integer = 0

            For i = 0 To gvFactureSelectProduit.Rows.Count - 1
                Dim cbProductSelector As CheckBox = CType(gvFactureSelectProduit.Rows(i).FindControl("cbProduitSelect"), CheckBox)
                If Not IsNothing(cbProductSelector) And cbProductSelector.Checked Then
                    Dim oProduitAffaireDAO As New CProduitAffaireDAO
                    Try
                        oProduitAffaireDAO.UpdateProduitAffaireFactureId(iFactureMax, CInt(gvFactureSelectProduit.DataKeys(i).Values(0)))
                        nombreProduit += 1
                    Catch ex As InvalidCastException
                        nombreTitre += 1
                    End Try
                End If
            Next

            lblerror3.Text = "Sélection réussie, vous avez ajouté " & nombreProduit & " produits à la facture. "
            If Not nombreTitre = 0 Then
                lblerror3.Text &= "Les " & nombreTitre & " titres que vous avez sélectionné n'ont pas été pris en compte."
            End If
        End If
    End Sub

    Protected Sub gvFactureSelectProduit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFactureSelectProduit.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)

            If drv("ProduitAffaireDate").ToString = "" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 254, 204)
            End If
        End If
    End Sub

    Protected Sub gvManuelProduit_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvManuelProduit.RowCommand
        If e.CommandName = "AjouterProduit" Then
            Dim oFactureAffaireDAO As New CFactureAffaireDAO
            Dim iFactureMax As New Integer
            iFactureMax = oFactureAffaireDAO.GetMaxFactureAffaireID()

            Dim Libelle As TextBox = CType(gvManuelProduit.FooterRow.FindControl("tbManuelLibelle"), TextBox)
            Dim Prix As TextBox = CType(gvManuelProduit.FooterRow.FindControl("tbManuelPrix"), TextBox)
            Dim Quantite As TextBox = CType(gvManuelProduit.FooterRow.FindControl("tbManuelQte"), TextBox)
            Dim Tva As ComboBox = CType(gvManuelProduit.FooterRow.FindControl("cbbManuelTVA"), ComboBox)

            Dim oFactureProduit As CFactureProduit = New CFactureProduit(iFactureMax, Libelle.Text, CDec(Prix.Text.Replace(".", ",")), CDec(Quantite.Text.Replace(".", ",")), CInt(Tva.SelectedValue))
            If oFactureAffaireDAO.InsertFactureProduitManuel(oFactureProduit) > 0 Then
                lblerror3.Visible = True
                lblerror3.Text = "Insertion réussie."
            End If

            gvManuelProduit.DataSource = oFactureAffaireDAO.GetFactureProduitInserted(iFactureMax)
            gvManuelProduit.DataBind()

            Libelle.Text = ""
            Prix.Text = ""
            Quantite.Text = ""
        End If
    End Sub

    Protected Sub gvManuelProduit_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvManuelProduit.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim Tva As ComboBox = CType(e.Row.FindControl("cbbManuelTVA"), ComboBox)
            If Not IsNothing(Tva) Then
                Dim oTVADAO As New CTVADAO
                Dim dsTVA As DataSet = oTVADAO.GetAllTvaActif()
                Tva.DataSource = dsTVA
                Tva.DataTextField = "TvaTaux"
                Tva.DataValueField = "TvaID"
                Tva.DataBind()
                Tva.SelectedIndex = 0
            End If
        End If
    End Sub

    ''' <summary>
    ''' Ajout d'une facture lors du click sur le bouton Ajouter
    ''' </summary>
    Protected Sub btnAjouter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAjouter.Click

        If String.IsNullOrWhiteSpace(tbFactureRef.Text) Or String.IsNullOrWhiteSpace(tbFactureDate.Text) Then
            ' on verifie que les champs obligatoires soient bien renseignés
            lblerror.Visible = True
            lblerror.ForeColor = Drawing.Color.Red
            lblerror.Text = ("erreur: Tous les champs avec des * doivent être remplis.")
        Else
            pIdentification.Visible = False

            Dim oFactureAffaireDAO As New CFactureAffaireDAO
            Dim oFactureAffaire As New CFactureAffaire

            'on récupère l'id de l'affaire concernee
            iAffaireID = CInt(Request.QueryString("id"))

            'on instancie un objet de type FactureAffaire pour l'insertion
            oFactureAffaire.Affaire.AffaireID = iAffaireID
            oFactureAffaire.FactureAffaireDate = CDate(tbFactureDate.Text)
            oFactureAffaire.FactureAffaireLibelle = tbLibelle.Text
            oFactureAffaire.FactureAffaireRef = tbFactureRef.Text

            oFactureAffaireDAO.InsererFactureAffaire(oFactureAffaire)

            lblerror2.Visible = True
            lblerror2.ForeColor = Drawing.Color.Red
            lblerror2.Text = ("La facture a bien été créée, on ajoute les produits maintenant. Vous voulez: ")

            LoadTreeView()
            tbReferenceFacture.Text = ""
            tbLibelleFacture.Text = ""
            tbDateFacture.Text = ""
            tbTotHtFacture.Text = ""
            tbTotTtcFacture.Text = ""
            btnSuppression.Visible = False

            rbAffaireProduitType.Visible = True
            btnChoix.Visible = True

        End If
    End Sub

    Protected Sub btnProduits_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProduits.Click
        Try
            Response.Redirect("AffaireFactureDetails.aspx?id=" & CInt(tvFacture.SelectedNode.Value))
        Catch ex As NullReferenceException
            lblMsg.Text = "Vous n'avez pas encore sélectionné de facture. "
        End Try

    End Sub

    Protected Sub btnSuppression_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSuppression.Click
        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO

        Dim iFactureAffaireID As Integer = CInt(tvFacture.SelectedNode.Value)

        oFactureAffaireDAO.UpdateSupppressionFacture(iFactureAffaireID)
        oFactureAffaireDAO.DeleteFacture(iFactureAffaireID)

        LoadTreeView()
        tbReferenceFacture.Text = ""
        tbLibelleFacture.Text = ""
        tbDateFacture.Text = ""
        tbTotHtFacture.Text = ""
        tbTotTtcFacture.Text = ""
        btnSuppression.Visible = False
    End Sub

    Protected Sub btnChoix_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChoix.Click
        RadioBoutonSelection()
    End Sub

    Protected Sub btnConfirmPourcentage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmPourcentage.Click
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim oFactureProduit As CFactureProduit

        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim iFactureMax As New Integer
        iFactureMax = oFactureAffaireDAO.GetMaxFactureAffaireID()

        Dim iCount As Integer = gvProduitPourcentage.Rows.Count
        Dim nombreProduit As Integer = 0

        Dim sType As String = "edit"

        For i = 0 To iCount - 1
            Dim EtapeID As Integer = CInt(gvProduitPourcentage.DataKeys(i).Values(1))
            Dim Libelle As TextBox = CType(gvProduitPourcentage.Rows(i).Cells(1).FindControl("tbPourcentageLibelle"), TextBox)
            Dim Prix As TextBox = CType(gvProduitPourcentage.Rows(i).Cells(2).FindControl("tbPourcentagePrix"), TextBox)
            Dim Quantite As TextBox = CType(gvProduitPourcentage.Rows(i).Cells(3).FindControl("tbPourcentageQte"), TextBox)

            oFactureProduit = New CFactureProduit(iFactureMax, EtapeID, Libelle.Text, CDec(Prix.Text.Replace(".", ",")), CDec(Quantite.Text.Replace(".", ",")))
            If oAffaireEtapeFactureDAO.InsertFactureProduitPourcentage(oFactureProduit) > 0 Then
                nombreProduit += 1
            End If
        Next

        If nombreProduit = 1 Then
            lblerror3.Visible = True
            lblerror3.Text = nombreProduit & " produit enregistré."
        Else
            lblerror3.Visible = True
            lblerror3.Text = nombreProduit & " produits enregistrés."
        End If

        Dim dsProduit As DataSet = oAffaireEtapeFactureDAO.GetFactureProduitEdited(iAffaireID)
        gvProduitPourcentage.DataSource = dsProduit
        gvProduitPourcentage.DataBind()
    End Sub


   

    Protected Sub tvFacture_SelectedTreeNodeChanged(ByVal sender As Object, ByVal e As Obout.Ajax.UI.TreeView.NodeEventArgs) Handles tvFacture.SelectedTreeNodeChanged
        pIdentification.Visible = True

        tbFactureRef.Text = ""
        tbLibelle.Text = ""
        tbFactureDate.Text = ""
        lblerror.Text = ""
        lblerror2.Text = ""
        lblerror3.Text = ""
        rbAffaireProduitType.Visible = False
        btnChoix.Visible = False
        gvFactureSelectProduit.Visible = False
        gvProduitPourcentage.Visible = False
        btnConfirmPourcentage.Visible = False
        gvManuelProduit.Visible = False

        Try
            If CType(Session("Employe"), CEmploye).ProfilID = 1 Then
                btnModification.Visible = False
                btnSuppression.Visible = False
                btnProduits.Visible = False
                pNouvelleFacture.Visible = False
            Else
                btnModification.Visible = True
                btnSuppression.Visible = True
                btnProduits.Visible = True
            End If
        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try

        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim oFacture As CFactureAffaire = oFactureAffaireDAO.GetFactureAffaireByFactureID(CInt(e.Node.Value), False)

        tbReferenceFacture.Text = oFacture.FactureAffaireRef
        tbLibelleFacture.Text = oFacture.FactureAffaireLibelle
        tbDateFacture.Text = CStr(oFacture.FactureAffaireDate)

        Dim total() As Decimal = oFactureAffaireDAO.GetTotalByFactureID(CInt(e.Node.Value))
        tbTotHtFacture.Text = CStr(total(0))
        tbTotTtcFacture.Text = CStr(total(1))

    End Sub
    ''' <summary>
    ''' redirection a la page AffaireModifier quand click sur le bouton Retour
    ''' </summary>
    Protected Sub btnRetourListe_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetourListe.Click
        Try

            Response.Redirect("AffaireLister.aspx?affaire=" & CInt(Request.QueryString("id")))

        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try
    End Sub

    Private Sub btnRetourFiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetourFiche.Click
        Response.Redirect("AffaireModifier.aspx?id=" & CInt(Request.QueryString("id")))
    End Sub

    Private Sub mMenuAffaireModif_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mMenuAffaireModif.MenuItemClick
        Dim iAffaireID As Integer = CInt(Request.QueryString("id"))

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