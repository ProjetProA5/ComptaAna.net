Imports ComptaAna.net
Imports ComptaAna.Business
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit

Public Class AffaireFacturation
    Inherits System.Web.UI.Page
    Dim iAffaireID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        iAffaireID = CInt(Request.QueryString("id"))
        Dim oAffaireDAO As New CAffaireDAO
        Dim sLibelle As String = oAffaireDAO.GetAffaireLibelle(iAffaireID)


        If Not Page.IsPostBack Then
            LoadGridView()

            hTitrePageFacture.InnerText &= " pour l'affaire " & sLibelle
            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesFacture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            chargerDdlRib()

            If tbFactureDate.Text = "" Then
                If DateTime.Now.Month < 10 Then
                    tbFactureDate.Text = DateTime.Now.Day & "/0" & DateTime.Now.Month & "/" & DateTime.Now.Year
                Else
                    tbFactureDate.Text = DateTime.Now.Day & "/" & DateTime.Now.Month & "/" & DateTime.Now.Year
                End If

            End If


            mMenuAffaireModif.Items(3).Selected = True
        End If
    End Sub

    Private Sub gvListeDesFactures_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListeDesFactures.RowDataBound
        'Dim sLienFacture As String
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim iRibID As String = e.Row.Cells(4).Text
            Dim oRibDao As New CRibDAO

            e.Row.Cells(4).Text = oRibDao.getLibelleAvecRibID(iRibID)

            ' Version YB
            'Dim iFactuID As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "FacturationAffaireID"))
            'If oFacturationAffaireDAO.isAvoirWithRef(e.Row.Cells(1).Text) Then
            '    sLienFacture = GetURLFacture(iAffaireID, iFactuID)
            'Else
            '    sLienFacture = GetURLFacture(iAffaireID, iFactuID)
            'End If
            ''  CType(e.Row.FindControl("hlFacture"), HyperLink).NavigateUrl = "javascript:ouvrirFacture('" & sLienFacture & "');"
            '' CType(e.Row.FindControl("hlFacture"), HyperLink).Text = "Lien facture"
        End If
        'Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
        'Dim iFactuID As Integer = CInt(drv("FacturationAffaireID"))

        'For i As Integer = 0 To gvListeDesFactures.Rows.Count - 1
        '    If System.IO.Directory.Exists(CConfiguration.RepertoireUpload & "\Factures\" & iAffaireID) Then

        '        sLienFacture = Dir(CConfiguration.RepertoireUpload & "\Factures\" & iAffaireID & "\" & iFactuID & "*.xls")

        '        CType(e.Row.FindControl("hlFacture"), HyperLink).NavigateUrl = "javascript:ouvrirFacture('" & sLienFacture & "');"
        '        CType(e.Row.FindControl("hlFacture"), HyperLink).Text = sLienFacture


        '    End If


        'Next
    End Sub



    Protected Sub gvListeDesFactures_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvListeDesFactures.SelectedIndexChanged
        lblMsg.Text = ""
    End Sub

    Private Sub btnNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNouveau.Click
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim oAffaire As CAffaire
        oAffaire = oAffaireDAO.GetAffaire(iAffaireID)
        Dim sLibelle As String = oAffaireDAO.GetAffaireLibelle(iAffaireID)
        tbLibelle.Text = "Facture de l'affaire : " & sLibelle

        'On cache le fieldSet Avoir
        fsAvoir.Visible = False
        btnSaveAvoir.Visible = False
        btnAnnulerAvoir.Visible = False

        If oAffaire.TypeAffaire.TypeAffaireLibelle = "Forfait" Then
            lblPourcentageMois.Text = "Pourcentage à facturer:"
            Dim dsPourcent As DataSet = oFacturationAffaireDAO.GetPourcentageFacturation(iAffaireID)
            If Not dsPourcent.Tables(0).Rows.Count = 0 Then
                fsNouvelleFacture.Visible = True
                btnEnregistrer.Visible = True
                btnAnnuler.Visible = True
                lblMsg.Text = ""
                LoadRef()
                ChargerRBPourcentage()
            Else
                lblMsg.Visible = True
                lblMsg.Text = "Cette affaire est facturée à 100%"
                lblMsg.ForeColor = Drawing.Color.Red
            End If
        Else
            lblPourcentageMois.Text = "Mois à facturer:"
            Dim dsMois As DataSet = oFacturationAffaireDAO.GetMoisFacturation(iAffaireID)
            If Not dsMois.Tables(0).Rows.Count = 0 Then
                fsNouvelleFacture.Visible = True
                btnEnregistrer.Visible = True
                btnAnnuler.Visible = True
                lblMsg.Text = ""
                LoadRef()
                ChagerRBMoisFacturation()
               
            Else
                lblMsg.Visible = True
                lblMsg.Text = "Cette affaire est facturée à 100%"
                lblMsg.ForeColor = Drawing.Color.Red
            End If
        End If


    End Sub

    Private Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrer.Click
        lblErreurDate.Visible = False
        lblMsg.Text = ""
        Dim oAffaireDAO = New CAffaireDAO
        Dim affaire = oAffaireDAO.GetAffaire(iAffaireID)
        Dim iAffaireType As Integer = affaire.TypeAffaire.TypeAffaireID
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim oFacturationAffaire As New CFacturationAffaire

        If Not tbFactureDate.Text = "" Then
            'Affaire au forfait
            If iAffaireType = modEnum.eTypeAffaire.Forfait Then
                fsNouvelleFacture.Visible = False
                btnEnregistrer.Visible = False
                btnAnnuler.Visible = False


                'on récupère l'id de l'affaire concernee
                iAffaireID = CInt(Request.QueryString("id"))

                'on instancie un objet de type FacturationAffaire pour l'insertion
                oFacturationAffaire.Affaire.AffaireID = iAffaireID
                oFacturationAffaire.FacturationAffaireDate = CDate(tbFactureDate.Text)
                oFacturationAffaire.FacturationAffaireLibelle = tbLibelle.Text & " - " & ddlPourcentageMoisFacturation.SelectedItem.Text
                oFacturationAffaire.FacturationAffaireRef = lblFactureRef.Text
                oFacturationAffaire.RibID = CInt(ddlRib.SelectedValue)
                oFacturationAffaire.Paye = 0
                oFacturationAffaire.FacturationAffaireCommentaire = ""
                oFacturationAffaire.FacturationAffaireCommentaireDateFacture = ""
                oFacturationAffaire.FacturationAffaireCommentaireClausesFacture = ""
                oFacturationAffaire.FacturationAffaireBC = tbNumBonCommande.Text

                Dim iRes As Integer = oFacturationAffaireDAO.InsererFacturationAffaire(oFacturationAffaire)

                oFacturationAffaire.FacturationAffaireID = oFacturationAffaireDAO.GetMaxFacturationAffaireID

                If iRes = 0 Then
                    lblMsg.Visible = True
                    lblMsg.Text = "La nouvelle facture n'a pas pu être enregistrée"
                    lblMsg.ForeColor = Drawing.Color.Red
                Else
                    lblMsg.Visible = True
                    lblMsg.Text = "La nouvelle facture a bien été enregistrée"
                    lblMsg.ForeColor = Drawing.Color.Blue
                    LoadGridView()
                    LierFactureEtape(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                    ValiderEtapeFacture(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                    LierFraisFacturation(iAffaireID, oFacturationAffaire)
                    oFacturationAffaireDAO.UpDateFactureSauvegarde(oFacturationAffaire.FacturationAffaireID, iAffaireType, CInt(ddlPourcentageMoisFacturation.SelectedItem.Text))
                    UpdateMontantFacturationAffaire(iAffaireID, oFacturationAffaire)
                    FacturerFraisAssocies(oFacturationAffaire.FacturationAffaireID)
                    Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & oFacturationAffaire.FacturationAffaireID)
                End If
                'Contrat cadre 
            ElseIf iAffaireType = modEnum.eTypeAffaire.ContratCadre Then
                fsNouvelleFacture.Visible = False
                btnEnregistrer.Visible = False
                btnAnnuler.Visible = False

                Dim iRes As Integer = 1
                'on récupère l'id de l'affaire concernee
                iAffaireID = CInt(Request.QueryString("id"))

                ' Facturaction de plusieurs mois 
                If Not FirstMonth() Then
                    pPopupSavePlusieursMois.Visible = True
                Else

                    'on instancie un objet de type FacturationAffaire pour l'insertion
                    oFacturationAffaire.Affaire.AffaireID = iAffaireID
                    oFacturationAffaire.FacturationAffaireDate = CDate(tbFactureDate.Text)
                    oFacturationAffaire.FacturationAffaireLibelle = tbLibelle.Text
                    oFacturationAffaire.FacturationAffaireRef = lblFactureRef.Text
                    oFacturationAffaire.RibID = CInt(ddlRib.SelectedValue)
                    oFacturationAffaire.Paye = 0
                    oFacturationAffaire.FacturationAffaireCommentaire = ""
                    oFacturationAffaire.FacturationAffaireCommentaireDateFacture = ""
                    oFacturationAffaire.FacturationAffaireCommentaireClausesFacture = ""
                    oFacturationAffaire.FacturationAffaireBC = tbNumBonCommande.Text

                    iRes = oFacturationAffaireDAO.InsererFacturationAffaire(oFacturationAffaire)

                    oFacturationAffaire.FacturationAffaireID = oFacturationAffaireDAO.GetMaxFacturationAffaireID

                    If iRes = 0 Then
                        lblMsg.Visible = True
                        lblMsg.Text = "La nouvelle facture n'a pas pu être enregistrée"
                        lblMsg.ForeColor = Drawing.Color.Red
                    Else
                        lblMsg.Visible = True
                        lblMsg.Text = "La nouvelle facture a bien été enregistrée"
                        lblMsg.ForeColor = Drawing.Color.Blue
                        LoadGridView()
                        LierFactureEtape(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                        ValiderEtapeFacture(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                        LierProduitFacturation(iAffaireID, oFacturationAffaire)
                        oFacturationAffaireDAO.UpDateFactureSauvegarde(oFacturationAffaire.FacturationAffaireID, iAffaireType, CInt(ddlPourcentageMoisFacturation.SelectedItem.Text))
                        UpdateMontantFacturationAffaire(iAffaireID, oFacturationAffaire)
                        FacturerFraisAssocies(oFacturationAffaire.FacturationAffaireID)
                        Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & oFacturationAffaire.FacturationAffaireID)
                    End If

                End If
                ' régie
            Else

                fsNouvelleFacture.Visible = False
                btnEnregistrer.Visible = False
                btnAnnuler.Visible = False
                Dim iRes As Integer = 1
                'on récupère l'id de l'affaire concernee
                iAffaireID = CInt(Request.QueryString("id"))

                ' Facturaction de plusieurs mois 
                If Not FirstMonth() Then
                    pPopupSavePlusieursMois.Visible = True

                Else

                    'on instancie un objet de type FacturationAffaire pour l'insertion
                    oFacturationAffaire.Affaire.AffaireID = iAffaireID
                    oFacturationAffaire.FacturationAffaireDate = CDate(tbFactureDate.Text)
                    oFacturationAffaire.FacturationAffaireLibelle = tbLibelle.Text
                    oFacturationAffaire.FacturationAffaireRef = lblFactureRef.Text
                    oFacturationAffaire.RibID = CInt(ddlRib.SelectedValue)
                    oFacturationAffaire.Paye = 0
                    oFacturationAffaire.FacturationAffaireCommentaire = ""
                    oFacturationAffaire.FacturationAffaireCommentaireDateFacture = ""
                    oFacturationAffaire.FacturationAffaireCommentaireClausesFacture = ""
                    oFacturationAffaire.FacturationAffaireBC = tbNumBonCommande.Text

                    iRes = oFacturationAffaireDAO.InsererFacturationAffaire(oFacturationAffaire)

                    oFacturationAffaire.FacturationAffaireID = oFacturationAffaireDAO.GetMaxFacturationAffaireID

                    If iRes = 0 Then
                        lblMsg.Visible = True
                        lblMsg.Text = "La nouvelle facture n'a pas pu être enregistrée"
                        lblMsg.ForeColor = Drawing.Color.Red
                    Else
                        lblMsg.Visible = True
                        lblMsg.Text = "La nouvelle facture a bien été enregistrée"
                        lblMsg.ForeColor = Drawing.Color.Blue
                        LoadGridView()
                        LierFactureEtape(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                        ValiderEtapeFacture(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                        LierProduitFacturation(iAffaireID, oFacturationAffaire)
                        oFacturationAffaireDAO.UpDateFactureSauvegarde(oFacturationAffaire.FacturationAffaireID, iAffaireType, CInt(ddlPourcentageMoisFacturation.SelectedItem.Text))
                        UpdateMontantFacturationAffaire(iAffaireID, oFacturationAffaire)
                        FacturerProduitsAssocies(oFacturationAffaire.FacturationAffaireID)
                        Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & oFacturationAffaire.FacturationAffaireID)
                    End If

                End If
            End If
        Else
            lblErreurDate.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' Met à 1 AffaireEtapeFacture pour spécifier que l'étape (le mois ou le pourcentage) est facturée
    ''' </summary>
    Private Sub ValiderEtapeFacture(ByVal iAffaireEtapeFactureID As Integer)
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        oFacturationAffaireDAO.ValiderEtapeFactureDAO(iAffaireEtapeFactureID)
    End Sub

    ''' <summary>
    ''' Met à 1 ProduitAffaireFacture dans ProduitAffaireAssocie Pour spécifier que le produit est facturé
    ''' </summary>
    Private Sub FacturerProduitsAssocies(ByVal iFactAffaireID As Integer)
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO

        oFacturationAffaireDAO.FacturerProduits(iFactAffaireID)
    End Sub

    ''' <summary>
    ''' Rempli FacturationAffaireID dans ProduitAffaire pour lier le produit avec la facture
    ''' </summary>
    Private Sub LierProduitFacturation(ByVal iAffaireID As Integer, ByVal oFacturationAffaire As CFacturationAffaire)
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO

        oFacturationAffaireDAO.LierProduits(iAffaireID, oFacturationAffaire)
    End Sub


    Private Sub btnAnnuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
        fsNouvelleFacture.Visible = False
        btnEnregistrer.Visible = False
        btnAnnuler.Visible = False
        lblMsg.Text = ""

    End Sub

    Private Sub LoadGridView()
        Dim oFacturationAffaire As New CFacturationAffaireDAO
        Dim ds As DataSet
        ds = oFacturationAffaire.GetFacturationAffaireByAffaireID(iAffaireID)

        gvListeDesFactures.DataSource = ds
        gvListeDesFactures.DataBind()
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

    ''' <summary>
    ''' chargement des étapes de facturation restantes pour cette affaire
    ''' </summary>
    Private Sub ChargerRBPourcentage()
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        ddlPourcentageMoisFacturation.Items.Clear()
        Dim ds As New DataSet
        ds = oFacturationAffaireDAO.GetPourcentageFacturation(iAffaireID)

        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("Pourcentage").ToString, ds.Tables(0).Rows(i)("AffaireEtapeFactureID").ToString)
            ddlPourcentageMoisFacturation.Items.Add(li)
        Next
        ddlPourcentageMoisFacturation.Items(0).Selected = True
    End Sub

    ''' <summary>
    ''' chargement automatique de la nouvelle référence. 
    ''' </summary>
    Private Sub LoadRef()
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim sMaxReference As String = oFacturationAffaireDAO.GetMaxOuVideFactureRef().ToString

        Dim annee = Now.Year()
        Dim newRef = CInt(sMaxReference) + 1

        lblFactureRef.Text = newRef & "/" & annee
        lblRefAv.Text = newRef & "/" & annee

    End Sub

    ''' <summary>
    ''' chargement des mois restant à facturer. 
    ''' </summary>
    Private Sub ChagerRBMoisFacturation()
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        ddlPourcentageMoisFacturation.Items.Clear()
        Dim ds As New DataSet
        ds = oFacturationAffaireDAO.GetMoisFacturation(iAffaireID)

        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("Mois").ToString, ds.Tables(0).Rows(i)("AffaireEtapeFactureID").ToString)
            ddlPourcentageMoisFacturation.Items.Add(li)
        Next
        ddlPourcentageMoisFacturation.Items(0).Selected = True
    End Sub

    ''' <summary>
    ''' redirection a la page AffaireModifier quand click sur le bouton Retour
    ''' </summary>
    Private Sub btnRetourFiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetourFiche.Click
        Response.Redirect("AffaireModifier.aspx?id=" & CInt(Request.QueryString("id")))
    End Sub

    ''' <summary>
    ''' redirection a la page AffaireLister quand click sur le bouton Retour
    ''' </summary>
    Protected Sub btnRetourListe_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetourListe.Click
        Try
            Response.Redirect("AffaireLister.aspx?affaire=" & CInt(Request.QueryString("id")))

        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try
    End Sub

    ''' <summary>
    ''' chargement de la référence pour l'année précédente
    ''' </summary>
    Private Sub LoadRefMoins1()
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO

        Dim annee = Now.Year() - 1
        Dim sMaxReference As String = oFacturationAffaireDAO.GetMaxOuVideFactureRef(annee).ToString
        Dim newRef = CInt(sMaxReference) + 1
        lblFactureRef.Text = newRef & "/" & annee
    End Sub

    Private Sub gvListeDesFactures_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListeDesFactures.RowCommand

        If e.CommandName = "LienDetails" Then
            Dim idFactureAffaire As Integer = CInt(e.CommandArgument)
            Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & idFactureAffaire)
            'ElseIf e.CommandName = "ArchivageFacture" Then
            '    ArchiverFacture(sRef)
        ElseIf e.CommandName = "SuppressionFacture" Then
            Dim sRef As String = e.CommandArgument.ToString
            Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
            If oFacturationAffaireDAO.isAvoirWithRef(sRef) Then
                SupprimerAvoir(sRef)
            Else
                SupprimerFacture(sRef)

            End If
            LoadGridView()
            lblMsg.Visible = True
            lblMsg.Text = "La facture a bien été supprimée"
            lblMsg.ForeColor = Drawing.Color.Blue
        End If

    End Sub

    'Private Sub btnAnneeMoins1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnneeMoins1.Click
    '    LoadRefMoins1()
    'End Sub

    ''' <summary>
    ''' Suppression d'une facture + remise à jour des tables liées à la facture
    ''' </summary>
    Private Sub SupprimerFacture(ByVal sRefFacture As String)
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim oAffaireEtapeFactuDAO As New CAffaireEtapeFactureDAO

        Dim iFactuID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRefFacture)

        ' Dim iAffaireEtapeFactureID As Integer = oFacturationAffaireDAO.GetAffaireEtapeFacturationByRef(sRefFacture)


        'Remet à 0 AffaireEtapeFactureValide
        oFacturationAffaireDAO.SupprimerEtapeFacture(iFactuID)
        'On supprime le lien dans la table AffaireEtapeFacture

        If oAffaireEtapeFactuDAO.GetEtapeFactureMois(iFactuID) = 13 Then
            oAffaireEtapeFactuDAO.DeleteByAffaireEtapeFactuID(oFacturationAffaireDAO.GetAffaireEtapeFacturationByRef(sRefFacture))
        Else
            oAffaireEtapeFactuDAO.SupprimerLienEtapeFacture(iFactuID)
        End If


        'Remet à 0 ProduitAffaireFacture dans ProduitAffaireAssocie
        oFacturationAffaireDAO.SupprimerEtatProduitAffaireFacture(iFactuID)
        'Remet à NULL FacturationAffaireID dans ProduitAffaire
        oFacturationAffaireDAO.SupprimerLienProduit(iFactuID)

        'Suppression du fichier dans Upload
        'Dim sURL As String = CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactuID & ".xls"
        'System.IO.File.Delete(sURL)

        'Suppression des sauvegardes de factures
        oFacturationAffaireDAO.SupprimerFactureSauvegarde(iFactuID)

        'Supprime la facturationAffaire de la base
        oFacturationAffaireDAO.SupprimerFactureByID(iFactuID)
    End Sub

    Private Sub ExportFacture(ByVal sRefFacture As String)

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture Presta et frais")


        'ligne
        Dim lRefEtDate As Integer = 6
        Dim lTVAAXE As Integer = 9
        Dim lTVAClient As Integer = 10
        Dim lDesignationEntetes As Integer = 14
        Dim lDesignation As Integer = 15
        Dim lBonCommandeEntete As Integer = 16
        Dim lDesignationStotalHT As Integer = 17
        Dim lDesignationTVA As Integer = 18
        Dim lDesignationStotalTTC As Integer = 19

        Dim lFraisEntetes As Integer = 22
        Dim lFraisStotalHT As Integer = 23
        Dim lFraisTVA As Integer = 24
        Dim lFraisStotalTTC As Integer = 25

        Dim lMontantEntetes As Integer = 29
        Dim lReceptionFactureEntetes As Integer = 30

        'Colonne

        Dim cEntetes As Integer = 0
        Dim cNbre As Integer = 2
        Dim cPUeuro As Integer = 3
        Dim cTotaleuro As Integer = 4

        ' maintenant on va remplir les cellules avec les datasets
        ' création des datasets
        Dim dsClient As New DataSet
        'Dim dsProduitsFacturees As New DataSet
        Dim dsForfait As New DataSet

        Dim dsPourcentageAffaire As New DataSet
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim dPourcentage As Double = 0
        Dim dBudget As Double = 0
        Dim iDecalageLigneDesignation As Integer = 0
        Dim iDecalageLigneFrais As Integer = 0
        Dim iFactureID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRefFacture)

        'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
        dsClient = oFacturationAffaireDAO.getInfoPourExportFacture(sRefFacture)

        'AffaireBudget, Qte, ProduitAffaireLibelle, MntUnitHT, ProduitAffaireDate, TypeAffaireID
        'dsProduitsFacturees = oFacturationAffaireDAO.getProduitAFactureesPourExportFacture(sRefFacture)

        dsForfait = oFacturationAffaireDAO.getForfaitPourExportFacture(iAffaireID)


        'Si c'est un forfait
        If CInt(dsForfait.Tables(0).Rows(0)("TypeAffaireID")) = 1 Then
            Dim dsFraisFactureesContratCadreEtRegie As DataSet = oFacturationAffaireDAO.getFraisPourExportFactureContratCadreEtRegie(sRefFacture)

            'Pourcentage
            dsPourcentageAffaire = oFacturationAffaireDAO.GetPourcentageFacturationAffaire(sRefFacture)

            dPourcentage = CType(dsPourcentageAffaire.Tables(0).Rows(0)("Pourcentage"), Double)
            dBudget = CType(dsForfait.Tables(0).Rows(0)("AffaireBudget"), Double)

            ws.Cells(lBonCommandeEntete + 1, cEntetes).Value = dPourcentage & "% Facturé"
            ws.Cells(lBonCommandeEntete + 1, cNbre).Value = dPourcentage / 100
            ws.Cells(lBonCommandeEntete + 1, cPUeuro).Value = dBudget

            ws.Cells(lBonCommandeEntete + 1, cTotaleuro).Formula = "= C" & lBonCommandeEntete + 2 & "*D" & lBonCommandeEntete + 2
            ws.Cells(lBonCommandeEntete + 1, cTotaleuro).Style.NumberFormat = "0.00"
            iDecalageLigneDesignation = 1
            'iDecalageLigneFrais = 1

            'alignement du texte 
            ws.Cells(lBonCommandeEntete + 1, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(lBonCommandeEntete + 1, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center

            'bordures
            ws.Cells(lBonCommandeEntete + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ws.Cells(lBonCommandeEntete + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ws.Cells(lBonCommandeEntete + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ws.Cells(lBonCommandeEntete + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ws.Cells(lBonCommandeEntete + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

            ' ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ' ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ' ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            'ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            'ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

            ' On ajuste la largeur des colonnes et des lignes
            ws.Rows(lBonCommandeEntete + 1).Height = CInt(1.5 * 256)

            ' permet de fusionner les lignes 
            ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete + 1, cEntetes, lBonCommandeEntete + 1, cEntetes + 1).Merged = True
            'ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes, lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True


            Try

                For Each row As DataRow In dsFraisFactureesContratCadreEtRegie.Tables(0).Rows

                    Dim dQte As Double = CDbl(row("Qte"))
                    Dim dMntUnitHT As Double = CDbl(row("MntUnitHT"))


                    If Not IsDBNull(row("ProduitAffaireLibelle")) Then
                        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = CStr(row("ProduitAffaireLibelle"))
                    Else
                        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = ""
                    End If

                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cNbre).Value = dQte
                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cPUeuro).Value = dMntUnitHT

                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Formula = "= C" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2 & "*D" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2
                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Style.NumberFormat = "0.00"
                    iDecalageLigneFrais += 1

                    'alignement du texte 
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center

                    'bordures
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

                    ' On ajuste la largeur des colonnes et des lignes
                    ws.Rows(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

                    ' permet de fusionner les lignes 
                    ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes, lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).Merged = True

                Next
            Catch

            End Try

            If iDecalageLigneFrais = 0 Then

                iDecalageLigneFrais = 1

                'bordures
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

                ' permet de fusionner les lignes 
                ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes, lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True
            End If




        Else

            Dim dsProduitsFactureesContratCadreEtRegie As DataSet = oFacturationAffaireDAO.getPrestationsRegie(iFactureID)
            Dim dsFraisFactureesContratCadreEtRegie As DataSet = oFacturationAffaireDAO.getPrestationsRegie(iFactureID)

            Dim sAncienTypeProduitLibelle As String = ""
            Dim sNouveauTypeProduitLibelle As String = ""
            Try
                sNouveauTypeProduitLibelle = CStr(dsProduitsFactureesContratCadreEtRegie.Tables(0).Rows(0)("TypeProduitLibelle"))
            Catch

            End Try


            For Each row As DataRow In dsProduitsFactureesContratCadreEtRegie.Tables(0).Rows
                'AffaireBudget, Qte, ProduitAffaireLibelle, MntUnitHT, ProduitAffaireDate, TypeAffaireID, TypeProduitLibelle
                Dim dQte As Double = CDbl(row("Qte"))
                Dim dMntUnitHT As Double = CDbl(row("MntUnitHT"))


                sNouveauTypeProduitLibelle = CStr(row("TypeProduitLibelle"))

                ' ajoute du TypeProduitLibelle 
                If Not sNouveauTypeProduitLibelle = sAncienTypeProduitLibelle Then
                    sAncienTypeProduitLibelle = sNouveauTypeProduitLibelle

                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cEntetes).Value = CStr(row("TypeProduitLibelle"))

                    iDecalageLigneDesignation += 1

                    ' couleur
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cNbre).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cPUeuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cTotaleuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))

                    'alignement du texte 
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

                    'bordures
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

                    ' On ajuste la largeur des colonnes et des lignes
                    ws.Rows(lBonCommandeEntete + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

                    ' permet de fusionner les lignes 
                    ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes, lBonCommandeEntete + iDecalageLigneDesignation, cEntetes + 1).Merged = True

                End If
                If Not IsDBNull(row("ProduitAffaireLibelle")) Then
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cEntetes).Value = CStr(row("ProduitAffaireLibelle"))
                Else
                    ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cEntetes).Value = ""
                End If

                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cNbre).Value = dQte
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cPUeuro).Value = dMntUnitHT

                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cTotaleuro).Formula = "= C" & lBonCommandeEntete + iDecalageLigneDesignation + 2 & "*D" & lBonCommandeEntete + iDecalageLigneDesignation + 2
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cTotaleuro).Style.NumberFormat = "0.00"
                iDecalageLigneDesignation += 1



                'alignement du texte 
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center

                'bordures
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)



                ' On ajuste la largeur des colonnes et des lignes
                ws.Rows(lBonCommandeEntete + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

                ' permet de fusionner les lignes 
                ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes, lBonCommandeEntete + iDecalageLigneDesignation, cEntetes + 1).Merged = True


            Next

            Try
                For Each row As DataRow In dsFraisFactureesContratCadreEtRegie.Tables(0).Rows

                    Dim dQte As Double = CDbl(row("Qte"))
                    Dim dMntUnitHT As Double = CDbl(row("MntUnitHT"))


                    If Not IsDBNull(row("ProduitAffaireLibelle")) Then
                        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = CStr(row("ProduitAffaireLibelle"))
                    Else
                        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = ""
                    End If

                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cNbre).Value = dQte
                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cPUeuro).Value = dMntUnitHT

                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Formula = "= C" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2 & "*D" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2
                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Style.NumberFormat = "0.00"
                    iDecalageLigneFrais += 1

                    'alignement du texte 
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center

                    'bordures
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                    ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

                    ' On ajuste la largeur des colonnes et des lignes
                    ws.Rows(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

                    ' permet de fusionner les lignes 
                    ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes, lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).Merged = True

                Next
            Catch

            End Try

            If iDecalageLigneFrais = 0 Then

                iDecalageLigneFrais = 1

                'bordures
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

                ' permet de fusionner les lignes 
                ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes, lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True
            End If

        End If

        'on applique le decalage de ligne
        'ligne
        lDesignationStotalHT = 17 + iDecalageLigneDesignation
        lDesignationTVA = 18 + iDecalageLigneDesignation
        lDesignationStotalTTC = 19 + iDecalageLigneDesignation

        lFraisEntetes = 22 + iDecalageLigneDesignation
        lFraisStotalHT = 23 + iDecalageLigneDesignation + iDecalageLigneFrais
        lFraisTVA = 24 + iDecalageLigneDesignation + iDecalageLigneFrais
        lFraisStotalTTC = 25 + iDecalageLigneDesignation + iDecalageLigneFrais

        lMontantEntetes = 29 + iDecalageLigneDesignation + iDecalageLigneFrais
        lReceptionFactureEntetes = 30 + iDecalageLigneDesignation + iDecalageLigneFrais

        'création des cellules
        'colonne A
        Dim ecDesignationEntete As ExcelCell = ws.Cells(lDesignationEntetes, cEntetes)
        Dim ecDesignation As ExcelCell = ws.Cells(lDesignation, cEntetes)
        Dim ecBonCommandeEntete As ExcelCell = ws.Cells(lBonCommandeEntete, cEntetes)
        Dim ecFraisEntete As ExcelCell = ws.Cells(lFraisEntetes, cEntetes)
        Dim ecMontantEntete As ExcelCell = ws.Cells(lMontantEntetes, cEntetes)
        Dim ecReceptionFactureEntete As ExcelCell = ws.Cells(lReceptionFactureEntetes, cEntetes)
        Dim ecTVAAXE As ExcelCell = ws.Cells(lTVAAXE, cEntetes)
        Dim ecTVAClient As ExcelCell = ws.Cells(lTVAClient, cEntetes)

        'colonne C
        Dim ecDesignationNbre As ExcelCell = ws.Cells(lDesignationEntetes, cNbre)
        Dim ecFraisNbre As ExcelCell = ws.Cells(lFraisEntetes, cNbre)

        'colonne D
        Dim ecDesignationPUeuro As ExcelCell = ws.Cells(lDesignationEntetes, cPUeuro)
        Dim ecFraisPUeuro As ExcelCell = ws.Cells(lFraisEntetes, cPUeuro)

        'colonne E
        Dim ecRefEtDate As ExcelCell = ws.Cells(lRefEtDate, cTotaleuro)
        Dim ecDesignationTotaleuro As ExcelCell = ws.Cells(lDesignationEntetes, cTotaleuro)
        Dim ecFraisTotaleuro As ExcelCell = ws.Cells(lFraisEntetes, cTotaleuro)
        Dim ecMontantTotaleuro As ExcelCell = ws.Cells(lMontantEntetes, cTotaleuro)


        ' Remplissage des cellules
        ecRefEtDate.Value = "FACTURE N°" & sRefFacture & " EN DATE DU " & tbFactureDate.Text.Split(CChar("/"))(0) & "/" & tbFactureDate.Text.Split(CChar("/"))(1) & "/" & tbFactureDate.Text.Split(CChar("/"))(2)

        ecTVAAXE.Value = "N° TVA intracommunautaire AXE : FR 92 429 489 966"
        ecTVAClient.Value = "N° TVA intracommunautaire " & dsClient.Tables(0).Rows(0)("ClientNom").ToString & " : " & dsClient.Tables(0).Rows(0)("ClientFacturationNumTVA").ToString

        ecDesignationEntete.Value = "Désignation"
        ecDesignationNbre.Value = "Nbre"
        ecDesignationPUeuro.Value = "PU €uros"
        ecDesignationTotaleuro.Value = "Total €uros"

        ecDesignation.Value = dsClient.Tables(0).Rows(0)("ClientNom")

        If tbNumBonCommande.Text = "" Then
            ecBonCommandeEntete.Value = "BON DE COMMANDE "
        Else
            ecBonCommandeEntete.Value = "BON DE COMMANDE n° " & tbNumBonCommande.Text
        End If

        ecFraisEntete.Value = "Frais à TVA déductible"
        ecFraisNbre.Value = "Nbre"
        ecFraisPUeuro.Value = "PU €uros"
        ecFraisTotaleuro.Value = "Total €uros"

        ecMontantEntete.Value = "MONTANT NET A PAYER EN €UROS"
        ecReceptionFactureEntete.Value = "A RECEPTION DE FACTURE"

        ecMontantTotaleuro.Value = "Total €uros"

        ' on va maintenant créer les dernières celules 
        Dim ecDesignationStotalHT As ExcelCell = ws.Cells(lDesignationStotalHT, cPUeuro)
        Dim ecDesignationTVA As ExcelCell = ws.Cells(lDesignationTVA, cPUeuro)
        Dim ecDesignationStotalTTC As ExcelCell = ws.Cells(lDesignationStotalTTC, cPUeuro)
        Dim ecDesignationStotalHTSom As ExcelCell = ws.Cells(lDesignationStotalHT, cTotaleuro)
        Dim ecDesignationTVASom As ExcelCell = ws.Cells(lDesignationTVA, cTotaleuro)
        Dim ecDesignationStotalTTCSom As ExcelCell = ws.Cells(lDesignationStotalTTC, cTotaleuro)

        Dim ecFraisStotalHT As ExcelCell = ws.Cells(lFraisStotalHT, cPUeuro)
        Dim ecFraisTVA As ExcelCell = ws.Cells(lFraisTVA, cPUeuro)
        Dim ecFraisStotalTTC As ExcelCell = ws.Cells(lFraisStotalTTC, cPUeuro)
        Dim ecFraisStotalHTSom As ExcelCell = ws.Cells(lFraisStotalHT, cTotaleuro)
        Dim ecFraisTVASom As ExcelCell = ws.Cells(lFraisTVA, cTotaleuro)
        Dim ecFraisStotalTTCSom As ExcelCell = ws.Cells(lFraisStotalTTC, cTotaleuro)

        Dim ecReceptionFactureTotaleuros As ExcelCell = ws.Cells(lReceptionFactureEntetes, cTotaleuro)

        ' remplissage 
        ecDesignationStotalHT.Value = "S/total HT"
        ecDesignationTVA.Value = "TVA 19,60%"
        ecDesignationStotalTTC.Value = "S/total TTC"

        ecFraisStotalHT.Value = "S/total HT"
        ecFraisTVA.Value = "TVA 19,60%"
        ecFraisStotalTTC.Value = "S/total TTC"



        ' les formules
        ecDesignationStotalHTSom.Formula = "=SUM(E" & lBonCommandeEntete + 2 & ":E" & lBonCommandeEntete + iDecalageLigneDesignation + 1 & ")"
        ecDesignationStotalHTSom.Style.NumberFormat = "0.00"
        ecDesignationTVASom.Formula = "= E" & lDesignationStotalHT + 1 & "*0.196"
        ecDesignationTVASom.Style.NumberFormat = "0.00"
        ecDesignationStotalTTCSom.Formula = "=SUM(E" & lDesignationTVA + 1 & ":E" & lDesignationStotalHT + 1 & ")"
        ecDesignationStotalTTCSom.Style.NumberFormat = "0.00"
        ecFraisStotalHTSom.Formula = "=SUM(E" & lFraisEntetes + 2 & ":E" & lFraisEntetes + iDecalageLigneFrais + 1 & ")"
        ecFraisStotalHTSom.Style.NumberFormat = "0.00"
        ecFraisTVASom.Formula = "= E" & lFraisStotalHT + 1 & "*0.196"
        ecFraisTVASom.Style.NumberFormat = "0.00"
        ecFraisStotalTTCSom.Formula = "=SUM(E" & lFraisTVA + 1 & ":E" & lFraisStotalHT + 1 & ")"
        ecFraisStotalTTCSom.Style.NumberFormat = "0.00"

        ecReceptionFactureTotaleuros.Formula = "= E" & lFraisStotalTTC + 1 & "+E" & lDesignationStotalTTC + 1
        ecReceptionFactureTotaleuros.Style.NumberFormat = "0.00"
        ' On ajuste la largeur des colonnes et des lignes
        ws.Columns(cEntetes).Width = 40 * 256
        ws.Columns(cTotaleuro).Width = 13 * 256

        ws.Rows(lDesignation).Height = 2 * 256


        'permet de fusionner les cellules 
        ws.Cells.GetSubrangeAbsolute(lDesignationEntetes, cEntetes, lDesignationEntetes, 1).Merged = True

        ws.Cells.GetSubrangeAbsolute(lDesignation, cEntetes, lDesignation, cTotaleuro).Merged = True

        ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete, cEntetes, lBonCommandeEntete, cTotaleuro).Merged = True

        ws.Cells.GetSubrangeAbsolute(lFraisEntetes, cEntetes, lFraisEntetes, 1).Merged = True

        ws.Cells.GetSubrangeAbsolute(lMontantEntetes, cEntetes, lMontantEntetes, cPUeuro).Merged = True
        ws.Cells.GetSubrangeAbsolute(lReceptionFactureEntetes, cEntetes, lReceptionFactureEntetes, cPUeuro).Merged = True

        ' les couleurs
        ecDesignationEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecDesignationNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecDesignationPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecDesignationTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

        ecDesignation.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.PaleGreen, Drawing.Color.PaleGreen)

        ecBonCommandeEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

        ecFraisEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecFraisNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecFraisPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecFraisTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

        ecMontantEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecMontantTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecReceptionFactureEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

        ' alignement du texte 
        ecRefEtDate.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right

        ecDesignationNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ecDesignationPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ecDesignationTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        ecDesignation.Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ecFraisNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ecFraisPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ecFraisTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        ' les polices
        ecRefEtDate.Style.Font.Weight = 1000

        ecDesignationEntete.Style.Font.Weight = 1000
        ecDesignationNbre.Style.Font.Weight = 1000
        ecDesignationPUeuro.Style.Font.Weight = 1000
        ecDesignationTotaleuro.Style.Font.Weight = 1000

        ecDesignation.Style.Font.Weight = 1000

        ecBonCommandeEntete.Style.Font.Weight = 1000

        ecFraisEntete.Style.Font.Weight = 1000
        ecFraisNbre.Style.Font.Weight = 1000
        ecFraisPUeuro.Style.Font.Weight = 1000
        ecFraisTotaleuro.Style.Font.Weight = 1000

        ecMontantEntete.Style.Font.Weight = 1000
        ecMontantTotaleuro.Style.Font.Weight = 1000
        ecReceptionFactureEntete.Style.Font.Weight = 1000

        ecDesignationStotalHT.Style.Font.Weight = 1000
        ecDesignationTVA.Style.Font.Weight = 1000
        ecDesignationStotalTTC.Style.Font.Weight = 1000
        ecDesignationStotalHTSom.Style.Font.Weight = 1000
        ecDesignationTVASom.Style.Font.Weight = 1000
        ecDesignationStotalTTCSom.Style.Font.Weight = 1000

        ecFraisStotalHT.Style.Font.Weight = 1000
        ecFraisTVA.Style.Font.Weight = 1000
        ecFraisStotalTTC.Style.Font.Weight = 1000
        ecFraisStotalHTSom.Style.Font.Weight = 1000
        ecFraisTVASom.Style.Font.Weight = 1000
        ecFraisStotalTTCSom.Style.Font.Weight = 1000

        ecReceptionFactureTotaleuros.Style.Font.Weight = 1000

        ' les bordures
        ecDesignationEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ecDesignation.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ecBonCommandeEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ecMontantEntete.SetBorders(MultipleBorders.Top, Drawing.Color.Black, LineStyle.Thin)
        ecMontantEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
        ecMontantTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)

        ecDesignationStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ecFraisStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecFraisStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ecReceptionFactureTotaleuros.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ' haut/pied page 
        'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
        Try
            ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom")) & CStr(dsClient.Tables(0).Rows(0)("ClientAdresse"))
        Catch
            ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom"))
        End Try

        Dim iRibID As Integer = oFacturationAffaireDAO.getRibIDAvecFacturationID(iFactureID)
        If Not iRibID = -1 Then
            Dim oRib As CRib = New CRib(CStr(iRibID))

            ws.HeadersFooters.Footer = "IBAN AXE : " & oRib.Cp & oRib.Cc & " " & oRib.Cb.ToString.Substring(0, 4) & " " _
                     & oRib.Cb.ToString.Substring(4, 1) & oRib.Cg.ToString.Substring(0, 3) & " " & oRib.Cg.ToString.Substring(3, 2) _
                     & oRib.Nc.ToString.Substring(0, 2) & " " & oRib.Nc.ToString.Substring(2, 4) & " " & oRib.Nc.ToString.Substring(6, 4) _
                     & " " & oRib.Nc.ToString.Substring(10, 1) & oRib.Cle & " BRUZ 35170"
        
        End If

        With ws.PrintOptions

            .HeaderMargin = 0.5
            .FooterMargin = 0.5

            .TopMargin = 0.5
            .BottomMargin = 0.5
            .LeftMargin = 0.25
            .RightMargin = .LeftMargin

        End With

        Response.Clear()
        ' Enregistrement du fichier Excel
        'Dim sNomFichier = "Facture " & sRefFacture

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & iFactureID & ".xls")
        Dim sUpload As String = CConfiguration.UploadPhysicalPath

        'Si le répertoire factures n'existe pas on le créer
        If Not System.IO.Directory.Exists(sUpload & "\Factures") Then
            System.IO.Directory.CreateDirectory(sUpload & "\Factures")
        End If
        ' Si le répertoire de l'affaire n'existe pas on le créer
        If Not System.IO.Directory.Exists(sUpload & "\Factures\" & iAffaireID) Then
            System.IO.Directory.CreateDirectory(sUpload & "\Factures\" & iAffaireID)
        End If

        'save dans le répertoire upload sur le serveur
        ef.SaveXls(CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactureID & ".xls")

        ef.SaveXls(Response.OutputStream)


    End Sub


    Private Sub ExportTreize(ByVal sRefFacture As String)
        If ddlPourcentageMoisFacturation.SelectedItem.Text = "12" Then
            Dim oAffaireDAO As New CAffaireDAO
            Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
            Dim oAffaire As CAffaire = oAffaireDAO.GetAffaire(iAffaireID)
            Dim dDiff As Decimal = oAffaire.AffaireBudget - oAffaire.AffaireBudgetInitial
            If dDiff > 0 Then
                Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
                Dim oFacturationAffaire As New CFacturationAffaire
                oFacturationAffaire.Affaire = oAffaire
                oFacturationAffaire.Avoir = 0
                oFacturationAffaire.FacturationAffaireDate = CDate(tbFactureDate.Text)
                oFacturationAffaire.FacturationAffaireID = iAffaireID
                oFacturationAffaire.FacturationAffaireLibelle = " Facture du 13ème mois"
                oFacturationAffaire.FacturationAffaireRef = lblFactureRef.Text
                oFacturationAffaire.RibID = CInt(ddlRib.SelectedValue)
                oFacturationAffaire.Paye = 0
                oFacturationAffaire.FacturationAffaireCommentaire = ""


                oFacturationAffaireDAO.InsererFacturationAffaire(oFacturationAffaire)


                oAffaireEtapeFactureDAO.InsertEtapeMoisByAffaireID(iAffaireID, 13, "1")


                oAffaireEtapeFactureDAO.LierEtapeFacture(oAffaireEtapeFactureDAO.GetCourantID(), oFacturationAffaireDAO.GetMaxFacturationAffaireID())





                ' Initialisation de l'objet Excel
                SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
                Dim ef As ExcelFile = New ExcelFile
                Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture Presta et frais")


                'ligne
                Dim lRefEtDate As Integer = 6
                Dim lTVAAXE As Integer = 9
                Dim lTVAClient As Integer = 10
                Dim lDesignation As Integer = 15
                Dim lBonCommandeEntete As Integer = 16
                Dim lMontantInitial As Integer = 17
                Dim lMontantRevu As Integer = 18
                Dim lMontantEntetes As Integer = 29
                Dim lReceptionFactureEntetes As Integer = 30


                'Colonne


                Dim cEntetes As Integer = 0
                Dim cNbre As Integer = 2
                Dim cPUeuro As Integer = 3
                Dim cTotaleuro As Integer = 4


                ' maintenant on va remplir les cellules avec les datasets
                ' création des datasets
                Dim dsClient As New DataSet


                Dim dBudget As Double = 0
                Dim iFactureID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRefFacture)


                'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
                dsClient = oFacturationAffaireDAO.getInfoPourExportFacture(sRefFacture)


                'AffaireBudget, Qte, ProduitAffaireLibelle, MntUnitHT, ProduitAffaireDate, TypeAffaireID
                'dsProduitsFacturees = oFacturationAffaireDAO.getProduitAFactureesPourExportFacture(sRefFacture)





                'on applique le decalage de ligne
                'ligne



                lMontantEntetes = 20
                lReceptionFactureEntetes = 21


                'création des cellules
                'colonne A
                Dim ecDesignation As ExcelCell = ws.Cells(lDesignation, cEntetes)
                Dim ecBonCommandeEntete As ExcelCell = ws.Cells(lBonCommandeEntete, cEntetes)
                Dim ecMontantEntete As ExcelCell = ws.Cells(lMontantEntetes, cEntetes)
                Dim ecReceptionFactureEntete As ExcelCell = ws.Cells(lReceptionFactureEntetes, cEntetes)
                Dim ecTVAAXE As ExcelCell = ws.Cells(lTVAAXE, cEntetes)
                Dim ecTVAClient As ExcelCell = ws.Cells(lTVAClient, cEntetes)
                Dim ecMontantInitial As ExcelCell = ws.Cells(lMontantInitial, cEntetes)
                Dim ecMontantRevu As ExcelCell = ws.Cells(lMontantRevu, cEntetes)


                'colonne E
                Dim ecMontantInitialPrix As ExcelCell = ws.Cells(lMontantInitial, cTotaleuro)
                Dim ecMontantRevuPrix As ExcelCell = ws.Cells(lMontantRevu, cTotaleuro)
                Dim ecRefEtDate As ExcelCell = ws.Cells(lRefEtDate, cTotaleuro)
                Dim ecMontantTotaleuro As ExcelCell = ws.Cells(lMontantEntetes, cTotaleuro)





                ' Remplissage des cellules
                ecRefEtDate.Value = "FACTURE N°" & sRefFacture & " EN DATE DU " & tbFactureDate.Text.Split(CChar("/"))(1) & "/" & tbFactureDate.Text.Split(CChar("/"))(2)


                ecTVAAXE.Value = "N° TVA intracommunautaire AXE : FR 92 429 489 966"
                ecTVAClient.Value = "N° TVA intracommunautaire " & dsClient.Tables(0).Rows(0)("ClientNom").ToString & " : " & dsClient.Tables(0).Rows(0)("ClientFacturationNumTVA").ToString
                ecDesignation.Value = dsClient.Tables(0).Rows(0)("ClientNom")


                If tbNumBonCommande.Text = "" Then
                    ecBonCommandeEntete.Value = "BON DE COMMANDE "
                Else
                    ecBonCommandeEntete.Value = "BON DE COMMANDE n° " & tbNumBonCommande.Text
                End If
                ecMontantInitial.Value = "MONTANT INITIAL DE LA COMMANDE"
                ecMontantInitialPrix.Value = oAffaire.AffaireBudgetInitial
                ecMontantRevu.Value = "MONTANT REVU"
                ecMontantRevuPrix.Value = oAffaire.AffaireBudget
                ecMontantEntete.Value = "DIFFERENCE NET A PAYER EN €UROS"
                ecReceptionFactureEntete.Value = "A RECEPTION DE FACTURE"


                ecMontantTotaleuro.Value = "Total €uros"

                ' on va maintenant créer les dernières celules 
                Dim ecDesignationNbrePourcentage As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cNbre)
                Dim ecDesignationBudgetInitial As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cPUeuro)
                Dim ecDesignationTotalHT As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cTotaleuro)


                Dim ecReceptionFactureTotaleuros As ExcelCell = ws.Cells(lReceptionFactureEntetes, cTotaleuro)



                ' remplissage 


                ' les formules
                ecReceptionFactureTotaleuros.Formula = " = " & " E" & lMontantRevu + 1 & " - " & "E" & lMontantInitial + 1
                ecReceptionFactureTotaleuros.Style.NumberFormat = "0.00"
                ' On ajuste la largeur des colonnes et des lignes
                ws.Columns(cEntetes).Width = 40 * 256
                ws.Columns(cTotaleuro).Width = 13 * 256


                ws.Rows(lDesignation).Height = 2 * 256



                'permet de fusionner les cellules 
                ws.Cells.GetSubrangeAbsolute(lDesignation, cEntetes, lDesignation, cTotaleuro).Merged = True
                ws.Cells.GetSubrangeAbsolute(lMontantInitial, cEntetes, lMontantInitial, cPUeuro).Merged = True
                ws.Cells.GetSubrangeAbsolute(lMontantRevu, cEntetes, lMontantRevu, cPUeuro).Merged = True


                ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete, cEntetes, lBonCommandeEntete, cTotaleuro).Merged = True


                ws.Cells.GetSubrangeAbsolute(lMontantEntetes, cEntetes, lMontantEntetes, cPUeuro).Merged = True
                ws.Cells.GetSubrangeAbsolute(lReceptionFactureEntetes, cEntetes, lReceptionFactureEntetes, cPUeuro).Merged = True


                ' les couleurs


                ecDesignation.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.PaleGreen, Drawing.Color.PaleGreen)



                ecMontantEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ecMontantInitial.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ws.Cells(lMontantInitial, cEntetes + 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ws.Cells(lMontantInitial, cNbre).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ws.Cells(lMontantInitial, cPUeuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                'ws.Cells(lMontantInitial, cTotaleuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ecMontantRevu.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ws.Cells(lMontantRevu, cEntetes + 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ws.Cells(lMontantRevu, cNbre).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ws.Cells(lMontantRevu, cPUeuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ' ws.Cells(lMontantRevu, cTotaleuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ecMontantTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
                ecReceptionFactureEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)


                ' alignement du texte 
                ecRefEtDate.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
                ecDesignation.Style.VerticalAlignment = VerticalAlignmentStyle.Center



                ' les polices
                ecRefEtDate.Style.Font.Weight = 1000


                ecDesignation.Style.Font.Weight = 1000
                ecMontantInitial.Style.Font.Weight = 1000
                ecMontantInitialPrix.Style.Font.Weight = 1000
                ecMontantRevu.Style.Font.Weight = 1000
                ecMontantRevuPrix.Style.Font.Weight = 1000
                ecBonCommandeEntete.Style.Font.Weight = 1000


                ecMontantEntete.Style.Font.Weight = 1000
                ecMontantTotaleuro.Style.Font.Weight = 1000
                ecReceptionFactureEntete.Style.Font.Weight = 1000


                ecReceptionFactureTotaleuros.Style.Font.Weight = 1000


                ' les bordures
                ecDesignation.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)


                ecBonCommandeEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)


                ecMontantEntete.SetBorders(MultipleBorders.Top, Drawing.Color.Black, LineStyle.Thin)
                ecMontantEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
                'ecMontantInitial.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
                ecMontantInitial.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
                ecMontantInitial.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)
                ecMontantInitialPrix.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)


                ecMontantRevu.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
                ecMontantRevu.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
                ecMontantRevu.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)
                ecMontantRevuPrix.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)


                ecMontantTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ecReceptionFactureEntete.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
                ecReceptionFactureEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
                ecReceptionFactureEntete.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)


                ecReceptionFactureTotaleuros.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)


                ' haut/pied page 
                'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
                Try
                    ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom")) & CStr(dsClient.Tables(0).Rows(0)("ClientAdresse"))
                Catch
                    ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom"))
                End Try


                Dim iRibID As Integer = oFacturationAffaireDAO.getRibIDAvecFacturationID(iFactureID)
                If Not iRibID = -1 Then
                    Dim oRib As CRib = New CRib(CStr(iRibID))

                    ws.HeadersFooters.Footer = "IBAN AXE : " & oRib.Cp & oRib.Cc & " " & oRib.Cb.ToString.Substring(0, 4) & " " _
                        & oRib.Cb.ToString.Substring(4, 1) & oRib.Cg.ToString.Substring(0, 3) & " " & oRib.Cg.ToString.Substring(3, 2) _
                        & oRib.Nc.ToString.Substring(0, 2) & " " & oRib.Nc.ToString.Substring(2, 4) & " " & oRib.Nc.ToString.Substring(6, 4) _
                        & " " & oRib.Nc.ToString.Substring(10, 1) & oRib.Cle & " BRUZ 35170"

                End If


                With ws.PrintOptions


                    .HeaderMargin = 0.5
                    .FooterMargin = 0.5


                    .TopMargin = 0.5
                    .BottomMargin = 0.5
                    .LeftMargin = 0.25
                    .RightMargin = .LeftMargin


                End With


                Response.Clear()
                ' Enregistrement du fichier Excel


                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & iFactureID & ".xls")
                Dim sUpload As String = CConfiguration.UploadPhysicalPath


                'Si le répertoire factures n'existe pas on le créer
                If Not System.IO.Directory.Exists(sUpload & "\Factures") Then
                    System.IO.Directory.CreateDirectory(sUpload & "\Factures")
                End If
                ' Si le répertoire de l'affaire n'existe pas on le créer
                If Not System.IO.Directory.Exists(sUpload & "\Factures\" & iAffaireID) Then
                    System.IO.Directory.CreateDirectory(sUpload & "\Factures\" & iAffaireID)
                End If


                'save dans le répertoire upload sur le serveur
                ef.SaveXls(CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactureID & ".xls")


                ef.SaveXls(Response.OutputStream)
                Response.ClearHeaders()


            ElseIf dDiff < 0 Then


                Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
                Dim oFacturationAffaire As New CFacturationAffaire


                'on récupère l'id de l'affaire concernee
                iAffaireID = CInt(Request.QueryString("id"))


                'on instancie un objet de type FacturationAffaire pour l'insertion
                oFacturationAffaire.Affaire.AffaireID = iAffaireID
                oFacturationAffaire.FacturationAffaireDate = Now
                oFacturationAffaire.FacturationAffaireLibelle = tbLibelleAvoir.Text
                oFacturationAffaire.FacturationAffaireRef = lblRefAv.Text



                Dim iRes As Integer = oFacturationAffaireDAO.InsererAvoirAffaire(oFacturationAffaire)


                If iRes = 0 Then
                    lblMsg.Visible = True
                    lblMsg.Text = "Le nouvel avoir n'a pas pu être enregistré"
                    lblMsg.ForeColor = Drawing.Color.Red
                Else
                    tbLibelleAvoir.Text = oAffaire.AffaireLibelle
                    tbMntAvoir.Text = CStr(-dDiff)
                    exportAvoir(sRefFacture)
                    lblMsg.Visible = True
                    lblMsg.Text = "Le nouvel avoir a bien été enregistré"
                    lblMsg.ForeColor = Drawing.Color.Blue


                    fsAvoir.Visible = False
                    btnSaveAvoir.Visible = False
                    btnAnnulerAvoir.Visible = False
                    LoadGridView()
                End If


                Response.Redirect("AffaireFacturation.aspx?id=" & iAffaireID)
            End If
        End If
    End Sub


    Private Sub btnQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitter.Click
        pPopupSaveFacture.Visible = False

    End Sub

    
    Private Sub btnAvoir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAvoir.Click
        iAffaireID = CInt(Request.QueryString("id"))
        Dim oAffaireDAO As New CAffaireDAO

        Dim sLibelle As String = oAffaireDAO.GetAffaireLibelle(iAffaireID)
        tbLibelleAvoir.Text = "Avoir pour l'affaire: " & sLibelle
        fsNouvelleFacture.Visible = False
        btnEnregistrer.Visible = False
        btnAnnuler.Visible = False
        fsAvoir.Visible = True
        btnSaveAvoir.Visible = True
        btnAnnulerAvoir.Visible = True
        lblMsg.Text = ""
        LoadRef()
    End Sub

    Private Sub btnSaveAvoir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveAvoir.Click
        Dim oAffaireDAO = New CAffaireDAO

        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim oFacturationAffaire As New CFacturationAffaire

        'on récupère l'id de l'affaire concernee
        iAffaireID = CInt(Request.QueryString("id"))

        'on instancie un objet de type FacturationAffaire pour l'insertion
        oFacturationAffaire.Affaire.AffaireID = iAffaireID
        oFacturationAffaire.FacturationAffaireDate = CDate(tbDateAvoir.Text)
        oFacturationAffaire.FacturationAffaireLibelle = tbLibelleAvoir.Text
        oFacturationAffaire.FacturationAffaireRef = lblRefAv.Text
        oFacturationAffaire.RibID = CInt(ddlRibAvoir.SelectedValue)
        oFacturationAffaire.Paye = 0
        oFacturationAffaire.FacturationAffaireCommentaire = ""
        oFacturationAffaire.FacturationAffaireCommentaireDateFacture = ""
        oFacturationAffaire.FacturationAffaireCommentaireClausesFacture = ""
        oFacturationAffaire.FacturationAffaireBC = ""
        oFacturationAffaire.QteAvoir = 1
        oFacturationAffaire.MontantAvoir = CDec(tbMntAvoir.Text)

        Dim iRes As Integer = oFacturationAffaireDAO.InsererAvoirAffaire(oFacturationAffaire)
        oFacturationAffaire.FacturationAffaireID = oFacturationAffaireDAO.GetMaxFacturationAffaireID
        If iRes = 0 Then
            lblMsg.Visible = True
            lblMsg.Text = "Le nouvel avoir n'a pas pu être enregistré"
            lblMsg.ForeColor = Drawing.Color.Red
        Else
            'exportAvoir(lblRefAv.Text)
            oFacturationAffaireDAO.UpDateFactureSauvegarde(oFacturationAffaire.FacturationAffaireID, -1, -1)
            lblMsg.Visible = True
            lblMsg.Text = "Le nouvel avoir a bien été enregistré"
            lblMsg.ForeColor = Drawing.Color.Blue

            fsAvoir.Visible = False
            btnSaveAvoir.Visible = False
            btnAnnulerAvoir.Visible = False
            LoadGridView()
        End If

        Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & oFacturationAffaire.FacturationAffaireID)
    End Sub

    Private Sub btnAnnulerAvoir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulerAvoir.Click
        fsAvoir.Visible = False
        btnSaveAvoir.Visible = False
        btnAnnulerAvoir.Visible = False
        lblMsg.Text = ""
    End Sub

    Private Function FirstMonth() As Boolean
        Return CInt(ddlPourcentageMoisFacturation.SelectedIndex) = 0
    End Function

    Private Sub btnQuitterSaveMois_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitterSaveMois.Click
        pPopupSavePlusieursMois.Visible = False
        ddlPourcentageMoisFacturation.SelectedIndex = 0

        fsNouvelleFacture.Visible = True
        btnEnregistrer.Visible = True
        btnAnnuler.Visible = True
    End Sub

    'Private Sub btnContinuer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinuer.Click
    '    pPopupSavePlusieursMois.Visible = False

    '    Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
    '    Dim oFacturationAffaire As New CFacturationAffaire
    '    Dim iRes As Integer = 1

    '    'on récupère l'id de l'affaire concernee
    '    iAffaireID = CInt(Request.QueryString("id"))

    '    Dim iIndexMoisSelected = ddlPourcentageMoisFacturation.SelectedIndex
    '    Dim i As Integer = 0
    '    'Facturation de plusieurs mois à la fois
    '    While iRes = 1 And i <= iIndexMoisSelected

    '        ddlPourcentageMoisFacturation.SelectedIndex = i
    '        'on instancie un objet de type FacturationAffaire pour l'insertion
    '        oFacturationAffaire.Affaire.AffaireID = iAffaireID
    '        oFacturationAffaire.FacturationAffaireEtapeFactureID = CInt(ddlPourcentageMoisFacturation.SelectedItem.Value)
    '        oFacturationAffaire.FacturationAffaireDate = CDate(tbFactureDate.Text)
    '        oFacturationAffaire.FacturationAffaireLibelle = tbLibelle.Text
    '        oFacturationAffaire.FacturationAffaireRef = lblFactureRef.Text
    '        oFacturationAffaire.RibID = CInt(ddlRib.SelectedValue)


    '        iRes = oFacturationAffaireDAO.InsererFacturationAffaire(oFacturationAffaire)

    '        If iRes = 1 Then
    '            ValiderEtapeFacture(oFacturationAffaire.FacturationAffaireEtapeFactureID)
    '            LierProduitFacturation(iAffaireID, oFacturationAffaire)
    '            FacturerProduitsAssocies(iAffaireID, oFacturationAffaireDAO.GetMaxFacturationAffaireID)
    '        End If

    '        i += 1
    '    End While

    '    If iRes = 0 Then
    '        lblMsg.Visible = True
    '        lblMsg.Text = "La nouvelle facture n'a pas pu être enregistrée"
    '        lblMsg.ForeColor = Drawing.Color.Red
    '    Else
    '        lblMsg.Visible = True
    '        lblMsg.Text = "La nouvelle facture a bien été enregistrée"
    '        lblMsg.ForeColor = Drawing.Color.Blue
    '        LoadGridView()

    '        ExportFacture(oFacturationAffaire.FacturationAffaireRef)
    '        Response.Redirect("AffaireFacturation.aspx?id=" & iAffaireID)
    '    End If

    'End Sub

    Private Sub chargerDdlRib()
        Dim oRibDao As New CRibDAO
        Dim ds As DataSet = oRibDao.GetListeCoordonnéesBancaires()
        Dim bUnItemEstSelectionne As Boolean = False

        ddlRib.Items.Clear()
        ddlRibAvoir.Items.Clear()

        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("RibLibelle").ToString, ds.Tables(0).Rows(i)("RibID").ToString)
            ddlRib.Items.Add(li)
            ddlRibAvoir.Items.Add(li)
            If CBool(ds.Tables(0).Rows(i)("RibParDefault")) Then
                bUnItemEstSelectionne = True
                ddlRib.Items(i).Selected = True
                ddlRibAvoir.Items(i).Selected = True
            End If

        Next
        If Not bUnItemEstSelectionne Then
            ddlRib.Items(0).Selected = True
            ddlRibAvoir.Items(0).Selected = True
        End If
    End Sub

    Private Sub SupprimerAvoir(ByVal sRef As String)
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim iFactuID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRef)

        'Suppression du fichier dans Upload
        Dim sURL As String = CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactuID & ".xls"
        System.IO.File.Delete(sURL)

        'Supprime la facturationAffaire de la base
        oFacturationAffaireDAO.SupprimerFactureByID(iFactuID)


    End Sub

    Private Sub exportAvoir(ByVal sRefFacture As String)

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Avoir")

        'ligne
        Dim lRefEtDate As Integer = 6
        Dim lTVAAXE As Integer = 9
        Dim lTVAClient As Integer = 10

        Dim lMontantEntetes As Integer = 12
        Dim lReceptionFactureEntetes As Integer = 13

        'Colonne

        Dim cEntetes As Integer = 0
        Dim cNbre As Integer = 2
        Dim cPUeuro As Integer = 3
        Dim cTotaleuro As Integer = 4

        ' maintenant on va remplir les cellules avec les datasets
        ' création des datasets
        Dim dsClient As New DataSet
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim iFactureID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRefFacture)

        'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
        dsClient = oFacturationAffaireDAO.getInfoPourExportFacture(sRefFacture)

        'création des cellules
        'colonne A

        Dim ecMontantEntete As ExcelCell = ws.Cells(lMontantEntetes, cEntetes)
        Dim ecReceptionFactureEntete As ExcelCell = ws.Cells(lReceptionFactureEntetes, cEntetes)
        Dim ecTVAAXE As ExcelCell = ws.Cells(lTVAAXE, cEntetes)
        Dim ecTVAClient As ExcelCell = ws.Cells(lTVAClient, cEntetes)

        'colonne E
        Dim ecRefEtDate As ExcelCell = ws.Cells(lRefEtDate, cTotaleuro)
        Dim ecMontantTotaleuro As ExcelCell = ws.Cells(lMontantEntetes, cTotaleuro)


        ' Remplissage des cellules
        ecRefEtDate.Value = "AVOIR N°" & sRefFacture & " EN DATE DU " & Now.Day & "/" & Now.Month & "/" & Now.Year

        ecTVAAXE.Value = "N° TVA intracommunautaire AXE : FR 92 429 489 966"
        ecTVAClient.Value = "N° TVA intracommunautaire " & dsClient.Tables(0).Rows(0)("ClientNom").ToString & " : " & dsClient.Tables(0).Rows(0)("ClientFacturationNumTVA").ToString

        ecMontantEntete.Value = "MONTANT NET DE L'AVOIR EN €UROS"
        ecReceptionFactureEntete.Value = ""

        ecMontantTotaleuro.Value = "Total €uros"

        ' on va maintenant créer les dernières celules 


        Dim ecReceptionFactureTotaleuros As ExcelCell = ws.Cells(lReceptionFactureEntetes, cTotaleuro)
        ecReceptionFactureTotaleuros.Value = tbMntAvoir.Text


        ' On ajuste la largeur des colonnes et des lignes
        ws.Columns(cEntetes).Width = 40 * 256
        ws.Columns(cTotaleuro).Width = 13 * 256


        'permet de fusionner les cellules 

        ws.Cells.GetSubrangeAbsolute(lMontantEntetes, cEntetes, lMontantEntetes, cPUeuro).Merged = True
        ws.Cells.GetSubrangeAbsolute(lReceptionFactureEntetes, cEntetes, lReceptionFactureEntetes, cPUeuro).Merged = True

        ' les couleurs

        ecMontantEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecMontantTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecReceptionFactureEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

        ' alignement du texte 
        ecRefEtDate.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right

        ' les polices
        ecRefEtDate.Style.Font.Weight = 1000
        ecMontantEntete.Style.Font.Weight = 1000
        ecMontantTotaleuro.Style.Font.Weight = 1000
        ecReceptionFactureEntete.Style.Font.Weight = 1000
        ecReceptionFactureTotaleuros.Style.Font.Weight = 1000

        ' les bordures

        ecMontantEntete.SetBorders(MultipleBorders.Top, Drawing.Color.Black, LineStyle.Thin)
        ecMontantEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
        ecMontantTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureTotaleuros.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ' haut/pied page 
        'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
        Try
            ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom")) & CStr(dsClient.Tables(0).Rows(0)("ClientAdresse"))
        Catch
            ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom"))
        End Try

        With ws.PrintOptions

            .HeaderMargin = 0.5
            .FooterMargin = 0.5

            .TopMargin = 0.5
            .BottomMargin = 0.5
            .LeftMargin = 0.25
            .RightMargin = .LeftMargin

        End With

        Response.Clear()
        ' Enregistrement du fichier Excel
        'Dim sNomFichier = "Avoir " & sRefFacture

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & iFactureID & ".xls")
        Dim sUpload As String = CConfiguration.UploadPhysicalPath

        'Si le répertoire factures n'existe pas on le créer
        If Not System.IO.Directory.Exists(sUpload & "\Factures") Then
            System.IO.Directory.CreateDirectory(sUpload & "\Factures")
        End If
        ' Si le répertoire de l'affaire n'existe pas on le créer
        If Not System.IO.Directory.Exists(sUpload & "\Factures\" & iAffaireID) Then
            System.IO.Directory.CreateDirectory(sUpload & "\Factures\" & iAffaireID)
        End If

        'save dans le répertoire upload sur le serveur
        ef.SaveXls(CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactureID & ".xls")

        ef.SaveXls(Response.OutputStream)
        ' ef.LoadXlsxFromDirectory()

    End Sub

    Private Sub LierFactureEtape(ByVal AffaireEtapeFactureID As Integer)
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim iFactuID As Integer = oFacturationAffaireDAO.GetMaxFacturationAffaireID
        oAffaireEtapeFactureDAO.LierEtapeFacture(AffaireEtapeFactureID, iFactuID)

    End Sub

    Private Sub btnContinuer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinuer.Click
        pPopupSavePlusieursMois.Visible = False
        Dim oFacturationAffaire As New CFacturationAffaire
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO

        'on instancie un objet de type FacturationAffaire pour l'insertion
        oFacturationAffaire.Affaire.AffaireID = iAffaireID
        ' oFacturationAffaire.FacturationAffaireEtapeFactureID = CInt(ddlPourcentageMoisFacturation.SelectedItem.Value)
        oFacturationAffaire.FacturationAffaireDate = CDate(tbFactureDate.Text)
        oFacturationAffaire.FacturationAffaireLibelle = tbLibelle.Text
        oFacturationAffaire.FacturationAffaireRef = lblFactureRef.Text
        oFacturationAffaire.RibID = CInt(ddlRib.SelectedValue)
        oFacturationAffaire.Paye = 0
        oFacturationAffaire.FacturationAffaireCommentaire = ""
        oFacturationAffaire.FacturationAffaireCommentaireDateFacture = ""
        oFacturationAffaire.FacturationAffaireCommentaireClausesFacture = ""
        oFacturationAffaire.FacturationAffaireBC = tbNumBonCommande.Text

        Dim iRes As Integer = oFacturationAffaireDAO.InsererFacturationAffaire(oFacturationAffaire)

        oFacturationAffaire.FacturationAffaireID = oFacturationAffaireDAO.GetMaxFacturationAffaireID

        Dim oAffaireDAO = New CAffaireDAO
        Dim affaire = oAffaireDAO.GetAffaire(iAffaireID)
        Dim iAffaireType As Integer = affaire.TypeAffaire.TypeAffaireID

        If Not tbFactureDate.Text = "" Then

            If iAffaireType = modEnum.eTypeAffaire.ContratCadre Then
                If iRes = 0 Then
                    lblMsg.Visible = True
                    lblMsg.Text = "La nouvelle facture n'a pas pu être enregistrée"
                    lblMsg.ForeColor = Drawing.Color.Red
                Else
                    lblMsg.Visible = True
                    lblMsg.Text = "La nouvelle facture a bien été enregistrée"
                    lblMsg.ForeColor = Drawing.Color.Blue
                    LoadGridView()

                    Dim iIndexMoisSelectionne As Integer = ddlPourcentageMoisFacturation.SelectedIndex

                    For i As Integer = 0 To iIndexMoisSelectionne
                        ddlPourcentageMoisFacturation.SelectedIndex = i
                        LierFactureEtape(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                        ValiderEtapeFacture(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                    Next
                    LierProduitFacturation(iAffaireID, oFacturationAffaire)
                    FacturerFraisAssocies(oFacturationAffaire.FacturationAffaireID)
                    oFacturationAffaireDAO.UpDateFactureSauvegarde(oFacturationAffaire.FacturationAffaireID, iAffaireType, CInt(ddlPourcentageMoisFacturation.SelectedItem.Text))
                    UpdateMontantFacturationAffaire(iAffaireID, oFacturationAffaire)
                    ' FacturerProduitsAssocies(oFacturationAffaire.FacturationAffaireID)
                    Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & oFacturationAffaire.FacturationAffaireID)

                End If
            ElseIf iAffaireType = modEnum.eTypeAffaire.Regie Then

                If iRes = 0 Then
                    lblMsg.Visible = True
                    lblMsg.Text = "La nouvelle facture n'a pas pu être enregistrée"
                    lblMsg.ForeColor = Drawing.Color.Red
                Else
                    lblMsg.Visible = True
                    lblMsg.Text = "La nouvelle facture a bien été enregistrée"
                    lblMsg.ForeColor = Drawing.Color.Blue
                    LoadGridView()


                    Dim iIndexMoisSelectionne As Integer = ddlPourcentageMoisFacturation.SelectedIndex


                    For i As Integer = 0 To iIndexMoisSelectionne
                        ddlPourcentageMoisFacturation.SelectedIndex = i
                        LierFactureEtape(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                        ValiderEtapeFacture(CInt(ddlPourcentageMoisFacturation.SelectedItem.Value))
                    Next
                    LierProduitFacturation(iAffaireID, oFacturationAffaire)
                    FacturerFraisAssocies(oFacturationAffaire.FacturationAffaireID)
                    FacturerProduitsAssocies(oFacturationAffaire.FacturationAffaireID)
                    oFacturationAffaireDAO.UpDateFactureSauvegarde(oFacturationAffaire.FacturationAffaireID, iAffaireType, CInt(ddlPourcentageMoisFacturation.SelectedItem.Text))
                    UpdateMontantFacturationAffaire(iAffaireID, oFacturationAffaire)
                    Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & oFacturationAffaire.FacturationAffaireID)

                End If
            End If
        Else
            lblErreurDate.Visible = True
        End If

    End Sub



    'Private Sub ExportFacturesMultiMois(ByVal sRefFacture As String)

    '    ' Initialisation de l'objet Excel
    '    SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
    '    Dim ef As ExcelFile = New ExcelFile
    '    Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture Presta et frais")


    '    'ligne
    '    Dim lRefEtDate As Integer = 6
    '    Dim lTVAAXE As Integer = 9
    '    Dim lTVAClient As Integer = 10
    '    Dim lDesignationEntetes As Integer = 14
    '    Dim lDesignation As Integer = 15
    '    Dim lBonCommandeEntete As Integer = 16
    '    Dim lDesignationStotalHT As Integer = 17
    '    Dim lDesignationTVA As Integer = 18
    '    Dim lDesignationStotalTTC As Integer = 19

    '    Dim lFraisEntetes As Integer = 22
    '    Dim lFraisStotalHT As Integer = 23
    '    Dim lFraisTVA As Integer = 24
    '    Dim lFraisStotalTTC As Integer = 25

    '    Dim lMontantEntetes As Integer = 29
    '    Dim lReceptionFactureEntetes As Integer = 30

    '    'Colonne

    '    Dim cEntetes As Integer = 0
    '    Dim cNbre As Integer = 2
    '    Dim cPUeuro As Integer = 3
    '    Dim cTotaleuro As Integer = 4

    '    ' maintenant on va remplir les cellules avec les datasets
    '    ' création des datasets
    '    Dim dsClient As New DataSet

    '    Dim dsPourcentageAffaire As New DataSet
    '    Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
    '    Dim dPourcentage As Double = 0
    '    Dim dBudget As Double = 0
    '    Dim iDecalageLigneDesignation As Integer = 0
    '    Dim iDecalageLigneFrais As Integer = 0
    '    Dim iFactureID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRefFacture)

    '    'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
    '    dsClient = oFacturationAffaireDAO.getInfoPourExportFacture(sRefFacture)


    '    Dim dsProduitsFactureesContratCadreEtRegie As DataSet = oFacturationAffaireDAO.getPrestationsRegie(iFactureID)
    '    Dim dsFraisFactureesContratCadreEtRegie As DataSet = oFacturationAffaireDAO.getPrestationsRegie(iFactureID)

    '    Dim sAncienTypeProduitLibelle As String = ""
    '    Dim sNouveauTypeProduitLibelle As String = ""
    '    Try
    '        sNouveauTypeProduitLibelle = CStr(dsProduitsFactureesContratCadreEtRegie.Tables(0).Rows(0)("TypeProduitLibelle"))
    '    Catch

    '    End Try


    '    For Each row As DataRow In dsProduitsFactureesContratCadreEtRegie.Tables(0).Rows
    '        'AffaireBudget, Qte, ProduitAffaireLibelle, MntUnitHT, ProduitAffaireDate, TypeAffaireID, TypeProduitLibelle
    '        Dim dQte As Double = CDbl(row("Qte"))
    '        Dim dMntUnitHT As Double = CDbl(row("MntUnitHT"))


    '        sNouveauTypeProduitLibelle = CStr(row("TypeProduitLibelle"))

    '        ' ajoute du TypeProduitLibelle 
    '        If Not sNouveauTypeProduitLibelle = sAncienTypeProduitLibelle Then
    '            sAncienTypeProduitLibelle = sNouveauTypeProduitLibelle

    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cEntetes).Value = CStr(row("TypeProduitLibelle"))

    '            iDecalageLigneDesignation += 1

    '            ' couleur
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cNbre).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cPUeuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cTotaleuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))

    '            'alignement du texte 
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

    '            'bordures
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ' On ajuste la largeur des colonnes et des lignes
    '            ws.Rows(lBonCommandeEntete + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

    '            ' permet de fusionner les lignes 
    '            ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes, lBonCommandeEntete + iDecalageLigneDesignation, cEntetes + 1).Merged = True

    '        End If

    '        If Not IsDBNull(row("ProduitAffaireLibelle")) Then
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cEntetes).Value = CStr(row("ProduitAffaireLibelle"))
    '        Else
    '            ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cEntetes).Value = ""
    '        End If
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cNbre).Value = dQte
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cPUeuro).Value = dMntUnitHT

    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation + 1, cTotaleuro).Formula = "= C" & lBonCommandeEntete + iDecalageLigneDesignation + 2 & "*D" & lBonCommandeEntete + iDecalageLigneDesignation + 2

    '        iDecalageLigneDesignation += 1



    '        'alignement du texte 
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center

    '        'bordures
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lBonCommandeEntete + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)



    '        ' On ajuste la largeur des colonnes et des lignes
    '        ws.Rows(lBonCommandeEntete + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

    '        ' permet de fusionner les lignes 
    '        ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete + iDecalageLigneDesignation, cEntetes, lBonCommandeEntete + iDecalageLigneDesignation, cEntetes + 1).Merged = True



    '    Next

    '    Try
    '        For Each row As DataRow In dsFraisFactureesContratCadreEtRegie.Tables(0).Rows

    '            Dim dQte As Double = CDbl(row("Qte"))
    '            Dim dMntUnitHT As Double = CDbl(row("MntUnitHT"))


    '            If Not IsDBNull(row("ProduitAffaireLibelle")) Then
    '                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = CStr(row("ProduitAffaireLibelle"))
    '            Else
    '                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = ""
    '            End If
    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cNbre).Value = dQte
    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cPUeuro).Value = dMntUnitHT

    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Formula = "= C" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2 & "*D" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2

    '            iDecalageLigneFrais += 1

    '            'alignement du texte 
    '            ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '            ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center

    '            'bordures
    '            ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ' On ajuste la largeur des colonnes et des lignes
    '            ws.Rows(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

    '            ' permet de fusionner les lignes 
    '            ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes, lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).Merged = True


    '        Next

    '    Catch

    '    End Try

    '    If iDecalageLigneFrais = 0 Then

    '        iDecalageLigneFrais = 1

    '        'bordures
    '        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '        ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '        ' permet de fusionner les lignes 
    '        ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes, lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True
    '    End If

    '    'on applique le decalage de ligne
    '    'ligne
    '    lDesignationStotalHT = 17 + iDecalageLigneDesignation
    '    lDesignationTVA = 18 + iDecalageLigneDesignation
    '    lDesignationStotalTTC = 19 + iDecalageLigneDesignation

    '    lFraisEntetes = 22 + iDecalageLigneDesignation
    '    lFraisStotalHT = 23 + iDecalageLigneDesignation + iDecalageLigneFrais
    '    lFraisTVA = 24 + iDecalageLigneDesignation + iDecalageLigneFrais
    '    lFraisStotalTTC = 25 + iDecalageLigneDesignation + iDecalageLigneFrais

    '    lMontantEntetes = 29 + iDecalageLigneDesignation + iDecalageLigneFrais
    '    lReceptionFactureEntetes = 30 + iDecalageLigneDesignation + iDecalageLigneFrais

    '    'création des cellules
    '    'colonne A
    '    Dim ecDesignationEntete As ExcelCell = ws.Cells(lDesignationEntetes, cEntetes)
    '    Dim ecDesignation As ExcelCell = ws.Cells(lDesignation, cEntetes)
    '    Dim ecBonCommandeEntete As ExcelCell = ws.Cells(lBonCommandeEntete, cEntetes)
    '    Dim ecFraisEntete As ExcelCell = ws.Cells(lFraisEntetes, cEntetes)
    '    Dim ecMontantEntete As ExcelCell = ws.Cells(lMontantEntetes, cEntetes)
    '    Dim ecReceptionFactureEntete As ExcelCell = ws.Cells(lReceptionFactureEntetes, cEntetes)
    '    Dim ecTVAAXE As ExcelCell = ws.Cells(lTVAAXE, cEntetes)
    '    Dim ecTVAClient As ExcelCell = ws.Cells(lTVAClient, cEntetes)

    '    'colonne C
    '    Dim ecDesignationNbre As ExcelCell = ws.Cells(lDesignationEntetes, cNbre)
    '    Dim ecFraisNbre As ExcelCell = ws.Cells(lFraisEntetes, cNbre)

    '    'colonne D
    '    Dim ecDesignationPUeuro As ExcelCell = ws.Cells(lDesignationEntetes, cPUeuro)
    '    Dim ecFraisPUeuro As ExcelCell = ws.Cells(lFraisEntetes, cPUeuro)

    '    'colonne E
    '    Dim ecRefEtDate As ExcelCell = ws.Cells(lRefEtDate, cTotaleuro)
    '    Dim ecDesignationTotaleuro As ExcelCell = ws.Cells(lDesignationEntetes, cTotaleuro)
    '    Dim ecFraisTotaleuro As ExcelCell = ws.Cells(lFraisEntetes, cTotaleuro)
    '    Dim ecMontantTotaleuro As ExcelCell = ws.Cells(lMontantEntetes, cTotaleuro)


    '    ' Remplissage des cellules
    '    ecRefEtDate.Value = "FACTURE N°" & sRefFacture & " EN DATE DU " & tbFactureDate.Text.Split(CChar("/"))(0) & "/" & tbFactureDate.Text.Split(CChar("/"))(1) & "/" & tbFactureDate.Text.Split(CChar("/"))(2)

    '    ecTVAAXE.Value = "N° TVA intracommunautaire AXE : FR 92 429 489 966"
    '    ecTVAClient.Value = "N° TVA intracommunautaire " & dsClient.Tables(0).Rows(0)("ClientNom").ToString & " : " & dsClient.Tables(0).Rows(0)("ClientFacturationNumTVA").ToString

    '    ecDesignationEntete.Value = "Désignation"
    '    ecDesignationNbre.Value = "Nbre"
    '    ecDesignationPUeuro.Value = "PU €uros"
    '    ecDesignationTotaleuro.Value = "Total €uros"

    '    ecDesignation.Value = dsClient.Tables(0).Rows(0)("ClientNom")

    '    If tbNumBonCommande.Text = "" Then
    '        ecBonCommandeEntete.Value = "BON DE COMMANDE "
    '    Else
    '        ecBonCommandeEntete.Value = "BON DE COMMANDE n° " & tbNumBonCommande.Text
    '    End If

    '    ecFraisEntete.Value = "Frais à TVA déductible"
    '    ecFraisNbre.Value = "Nbre"
    '    ecFraisPUeuro.Value = "PU €uros"
    '    ecFraisTotaleuro.Value = "Total €uros"

    '    ecMontantEntete.Value = "MONTANT NET A PAYER EN €UROS"
    '    ecReceptionFactureEntete.Value = "A RECEPTION DE FACTURE"

    '    ecMontantTotaleuro.Value = "Total €uros"

    '    ' on va maintenant créer les dernières celules 
    '    Dim ecDesignationStotalHT As ExcelCell = ws.Cells(lDesignationStotalHT, cPUeuro)
    '    Dim ecDesignationTVA As ExcelCell = ws.Cells(lDesignationTVA, cPUeuro)
    '    Dim ecDesignationStotalTTC As ExcelCell = ws.Cells(lDesignationStotalTTC, cPUeuro)
    '    Dim ecDesignationStotalHTSom As ExcelCell = ws.Cells(lDesignationStotalHT, cTotaleuro)
    '    Dim ecDesignationTVASom As ExcelCell = ws.Cells(lDesignationTVA, cTotaleuro)
    '    Dim ecDesignationStotalTTCSom As ExcelCell = ws.Cells(lDesignationStotalTTC, cTotaleuro)

    '    Dim ecFraisStotalHT As ExcelCell = ws.Cells(lFraisStotalHT, cPUeuro)
    '    Dim ecFraisTVA As ExcelCell = ws.Cells(lFraisTVA, cPUeuro)
    '    Dim ecFraisStotalTTC As ExcelCell = ws.Cells(lFraisStotalTTC, cPUeuro)
    '    Dim ecFraisStotalHTSom As ExcelCell = ws.Cells(lFraisStotalHT, cTotaleuro)
    '    Dim ecFraisTVASom As ExcelCell = ws.Cells(lFraisTVA, cTotaleuro)
    '    Dim ecFraisStotalTTCSom As ExcelCell = ws.Cells(lFraisStotalTTC, cTotaleuro)

    '    Dim ecReceptionFactureTotaleuros As ExcelCell = ws.Cells(lReceptionFactureEntetes, cTotaleuro)

    '    ' remplissage 
    '    ecDesignationStotalHT.Value = "S/total HT"
    '    ecDesignationTVA.Value = "TVA 19,60%"
    '    ecDesignationStotalTTC.Value = "S/total TTC"

    '    ecFraisStotalHT.Value = "S/total HT"
    '    ecFraisTVA.Value = "TVA 19,60%"
    '    ecFraisStotalTTC.Value = "S/total TTC"



    '    ' les formules
    '    ecDesignationStotalHTSom.Formula = "=SUM(E" & lBonCommandeEntete + 2 & ":E" & lBonCommandeEntete + iDecalageLigneDesignation + 1 & ")"
    '    ecDesignationTVASom.Formula = "= E" & lDesignationStotalHT + 1 & "*0.196"
    '    ecDesignationStotalTTCSom.Formula = "=SUM(E" & lDesignationTVA + 1 & ":E" & lDesignationStotalHT + 1 & ")"

    '    ecFraisStotalHTSom.Formula = "=SUM(E" & lFraisEntetes + 2 & ":E" & lFraisEntetes + iDecalageLigneFrais + 1 & ")"
    '    ecFraisTVASom.Formula = "= E" & lFraisStotalHT + 1 & "*0.196"
    '    ecFraisStotalTTCSom.Formula = "=SUM(E" & lFraisTVA + 1 & ":E" & lFraisStotalHT + 1 & ")"

    '    ecReceptionFactureTotaleuros.Formula = "= E" & lFraisStotalTTC + 1 & "+E" & lDesignationStotalTTC + 1

    '    ' On ajuste la largeur des colonnes et des lignes
    '    ws.Columns(cEntetes).Width = 40 * 256
    '    ws.Columns(cTotaleuro).Width = 13 * 256

    '    ws.Rows(lDesignation).Height = 2 * 256


    '    'permet de fusionner les cellules 
    '    ws.Cells.GetSubrangeAbsolute(lDesignationEntetes, cEntetes, lDesignationEntetes, 1).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lDesignation, cEntetes, lDesignation, cTotaleuro).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete, cEntetes, lBonCommandeEntete, cTotaleuro).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lFraisEntetes, cEntetes, lFraisEntetes, 1).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lMontantEntetes, cEntetes, lMontantEntetes, cPUeuro).Merged = True
    '    ws.Cells.GetSubrangeAbsolute(lReceptionFactureEntetes, cEntetes, lReceptionFactureEntetes, cPUeuro).Merged = True

    '    ' les couleurs
    '    ecDesignationEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecDesignationNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecDesignationPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecDesignationTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ecDesignation.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.PaleGreen, Drawing.Color.PaleGreen)

    '    ecBonCommandeEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ecFraisEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecFraisNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecFraisPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecFraisTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ecMontantEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecMontantTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecReceptionFactureEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ' alignement du texte 
    '    ecRefEtDate.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right

    '    ecDesignationNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecDesignationPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecDesignationTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

    '    ecDesignation.Style.VerticalAlignment = VerticalAlignmentStyle.Center

    '    ecFraisNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecFraisPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecFraisTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

    '    ' les polices
    '    ecRefEtDate.Style.Font.Weight = 1000

    '    ecDesignationEntete.Style.Font.Weight = 1000
    '    ecDesignationNbre.Style.Font.Weight = 1000
    '    ecDesignationPUeuro.Style.Font.Weight = 1000
    '    ecDesignationTotaleuro.Style.Font.Weight = 1000

    '    ecDesignation.Style.Font.Weight = 1000

    '    ecBonCommandeEntete.Style.Font.Weight = 1000

    '    ecFraisEntete.Style.Font.Weight = 1000
    '    ecFraisNbre.Style.Font.Weight = 1000
    '    ecFraisPUeuro.Style.Font.Weight = 1000
    '    ecFraisTotaleuro.Style.Font.Weight = 1000

    '    ecMontantEntete.Style.Font.Weight = 1000
    '    ecMontantTotaleuro.Style.Font.Weight = 1000
    '    ecReceptionFactureEntete.Style.Font.Weight = 1000

    '    ecDesignationStotalHT.Style.Font.Weight = 1000
    '    ecDesignationTVA.Style.Font.Weight = 1000
    '    ecDesignationStotalTTC.Style.Font.Weight = 1000
    '    ecDesignationStotalHTSom.Style.Font.Weight = 1000
    '    ecDesignationTVASom.Style.Font.Weight = 1000
    '    ecDesignationStotalTTCSom.Style.Font.Weight = 1000

    '    ecFraisStotalHT.Style.Font.Weight = 1000
    '    ecFraisTVA.Style.Font.Weight = 1000
    '    ecFraisStotalTTC.Style.Font.Weight = 1000
    '    ecFraisStotalHTSom.Style.Font.Weight = 1000
    '    ecFraisTVASom.Style.Font.Weight = 1000
    '    ecFraisStotalTTCSom.Style.Font.Weight = 1000

    '    ecReceptionFactureTotaleuros.Style.Font.Weight = 1000

    '    ' les bordures
    '    ecDesignationEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecDesignation.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecBonCommandeEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecMontantEntete.SetBorders(MultipleBorders.Top, Drawing.Color.Black, LineStyle.Thin)
    '    ecMontantEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ecMontantTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecReceptionFactureEntete.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '    ecReceptionFactureEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ecReceptionFactureEntete.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)

    '    ecDesignationStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecFraisStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecReceptionFactureTotaleuros.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ' haut/pied page 
    '    'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
    '    Try
    '        ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom")) & CStr(dsClient.Tables(0).Rows(0)("ClientAdresse"))
    '    Catch
    '        ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom"))
    '    End Try

    '    Dim iRibID As Integer = oFacturationAffaireDAO.getRibIDAvecFacturationID(iFactureID)
    '    If Not iRibID = -1 Then
    '        Dim oRib As CRib = New CRib(CStr(iRibID))

    '        If oRib.Iban Then
    '            ws.HeadersFooters.Footer = "IBAN AXE : " & oRib.Cp & oRib.Cc & " " & oRib.Cb.ToString.Substring(0, 4) & " " _
    '                & oRib.Cb.ToString.Substring(4, 1) & oRib.Cg.ToString.Substring(0, 3) & " " & oRib.Cg.ToString.Substring(3, 2) _
    '                & oRib.Nc.ToString.Substring(0, 2) & " " & oRib.Nc.ToString.Substring(2, 4) & " " & oRib.Nc.ToString.Substring(6, 4) _
    '                & " " & oRib.Nc.ToString.Substring(10, 1) & oRib.Cle & " BRUZ 35170"
    '        Else
    '            ws.HeadersFooters.Footer = "RIB AXE : " & oRib.RibLibelle & "    N° Banque : " & oRib.Cb & "" & vbCrLf & "Compte : " & oRib.Nc & "    Guichet : " & oRib.Cg & "    Clé : " & oRib.Cle & "    BRUZ 35170"

    '        End If
    '    End If

    '    With ws.PrintOptions

    '        .HeaderMargin = 0.5
    '        .FooterMargin = 0.5

    '        .TopMargin = 0.5
    '        .BottomMargin = 0.5
    '        .LeftMargin = 0.25
    '        .RightMargin = .LeftMargin

    '    End With

    '    Response.Clear()
    '    ' Enregistrement du fichier Excel
    '    'Dim sNomFichier = "Facture " & sRefFacture

    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AddHeader("Content-Disposition", "attachment; filename=" & iFactureID & ".xls")
    '    Dim sUpload As String = CConfiguration.UploadPhysicalPath

    '    'Si le répertoire factures n'existe pas on le créer
    '    If Not System.IO.Directory.Exists(sUpload & "\Factures") Then
    '        System.IO.Directory.CreateDirectory(sUpload & "\Factures")
    '    End If
    '    ' Si le répertoire de l'affaire n'existe pas on le créer
    '    If Not System.IO.Directory.Exists(sUpload & "\Factures\" & iAffaireID) Then
    '        System.IO.Directory.CreateDirectory(sUpload & "\Factures\" & iAffaireID)
    '    End If

    '    'save dans le répertoire upload sur le serveur
    '    ef.SaveXls(CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactureID & ".xls")

    '    ef.SaveXls(Response.OutputStream)

    'End Sub

    Private Sub LierFraisFacturation(ByVal iAffaireID As Integer, ByVal oFacturationAffaire As CFacturationAffaire)
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO

        oFacturationAffaireDAO.LierFrais(iAffaireID, oFacturationAffaire)
    End Sub

    Private Sub FacturerFraisAssocies(ByVal iFactAffaireID As Integer)
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO

        oFacturationAffaireDAO.FacturerFrais(iFactAffaireID)
    End Sub
    'Private Sub ExportTreize(sRefFacture As String)
    '    If ddlPourcentageMoisFacturation.SelectedItem.Text = "12" Then
    '        Dim oAffaireDAO As New CAffaireDAO
    '        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
    '        Dim oAffaire As CAffaire = oAffaireDAO.GetAffaire(iAffaireID)
    '        Dim dDiff As Decimal = oAffaire.AffaireBudget - oAffaire.AffaireBudgetInitial
    '        If dDiff > 0 Then
    '            Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
    '            Dim oFacturationAffaire As New CFacturationAffaire
    '            oFacturationAffaire.Affaire = oAffaire
    '            oFacturationAffaire.Avoir = 0
    '            oFacturationAffaire.FacturationAffaireDate = CDate(tbFactureDate.Text)
    '            oFacturationAffaire.FacturationAffaireID = iAffaireID
    '            oFacturationAffaire.FacturationAffaireLibelle = " Facture du 13ème mois"
    '            oFacturationAffaire.FacturationAffaireRef = lblFactureRef.Text
    '            oFacturationAffaire.RibID = CInt(ddlRib.SelectedValue)
    '            oFacturationAffaire.Paye = 0
    '            oFacturationAffaire.FacturationAffaireCommentaire = ""

    '            oFacturationAffaireDAO.InsererFacturationAffaire(oFacturationAffaire)

    '            oAffaireEtapeFactureDAO.InsertEtapeMoisByAffaireID(iAffaireID, 13, "1")

    '            oAffaireEtapeFactureDAO.LierEtapeFacture(oAffaireEtapeFactureDAO.GetCourantID(), oFacturationAffaireDAO.GetMaxFacturationAffaireID())



    '            ' Initialisation de l'objet Excel
    '            SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
    '            Dim ef As ExcelFile = New ExcelFile
    '            Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture Presta et frais")

    '            'ligne
    '            Dim lRefEtDate As Integer = 6
    '            Dim lTVAAXE As Integer = 9
    '            Dim lTVAClient As Integer = 10
    '            Dim lDesignation As Integer = 15
    '            Dim lBonCommandeEntete As Integer = 16
    '            Dim lMontantInitial As Integer = 17
    '            Dim lMontantRevu As Integer = 18
    '            Dim lMontantEntetes As Integer = 29
    '            Dim lReceptionFactureEntetes As Integer = 30

    '            'Colonne

    '            Dim cEntetes As Integer = 0
    '            Dim cNbre As Integer = 2
    '            Dim cPUeuro As Integer = 3
    '            Dim cTotaleuro As Integer = 4

    '            ' maintenant on va remplir les cellules avec les datasets
    '            ' création des datasets
    '            Dim dsClient As New DataSet

    '            Dim dBudget As Double = 0
    '            Dim iFactureID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRefFacture)

    '            'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
    '            dsClient = oFacturationAffaireDAO.getInfoPourExportFacture(sRefFacture)

    '            'AffaireBudget, Qte, ProduitAffaireLibelle, MntUnitHT, ProduitAffaireDate, TypeAffaireID
    '            'dsProduitsFacturees = oFacturationAffaireDAO.getProduitAFactureesPourExportFacture(sRefFacture)



    '            'on applique le decalage de ligne
    '            'ligne


    '            lMontantEntetes = 20
    '            lReceptionFactureEntetes = 21

    '            'création des cellules
    '            'colonne A
    '            Dim ecDesignation As ExcelCell = ws.Cells(lDesignation, cEntetes)
    '            Dim ecBonCommandeEntete As ExcelCell = ws.Cells(lBonCommandeEntete, cEntetes)
    '            Dim ecMontantEntete As ExcelCell = ws.Cells(lMontantEntetes, cEntetes)
    '            Dim ecReceptionFactureEntete As ExcelCell = ws.Cells(lReceptionFactureEntetes, cEntetes)
    '            Dim ecTVAAXE As ExcelCell = ws.Cells(lTVAAXE, cEntetes)
    '            Dim ecTVAClient As ExcelCell = ws.Cells(lTVAClient, cEntetes)
    '            Dim ecMontantInitial As ExcelCell = ws.Cells(lMontantInitial, cEntetes)
    '            Dim ecMontantRevu As ExcelCell = ws.Cells(lMontantRevu, cEntetes)

    '            'colonne E
    '            Dim ecMontantInitialPrix As ExcelCell = ws.Cells(lMontantInitial, cTotaleuro)
    '            Dim ecMontantRevuPrix As ExcelCell = ws.Cells(lMontantRevu, cTotaleuro)
    '            Dim ecRefEtDate As ExcelCell = ws.Cells(lRefEtDate, cTotaleuro)
    '            Dim ecMontantTotaleuro As ExcelCell = ws.Cells(lMontantEntetes, cTotaleuro)



    '            ' Remplissage des cellules
    '            ecRefEtDate.Value = "FACTURE N°" & sRefFacture & " EN DATE DU " & tbFactureDate.Text.Split(CChar("/"))(0) & "/" & tbFactureDate.Text.Split(CChar("/"))(1) & "/" & tbFactureDate.Text.Split(CChar("/"))(2)

    '            ecTVAAXE.Value = "N° TVA intracommunautaire AXE : FR 92 429 489 966"
    '            ecTVAClient.Value = "N° TVA intracommunautaire " & dsClient.Tables(0).Rows(0)("ClientNom").ToString & " : " & dsClient.Tables(0).Rows(0)("ClientFacturationNumTVA").ToString
    '            ecDesignation.Value = dsClient.Tables(0).Rows(0)("ClientNom")

    '            If tbNumBonCommande.Text = "" Then
    '                ecBonCommandeEntete.Value = "BON DE COMMANDE "
    '            Else
    '                ecBonCommandeEntete.Value = "BON DE COMMANDE n° " & tbNumBonCommande.Text
    '            End If
    '            ecMontantInitial.Value = "MONTANT INITIAL DE LA COMMANDE"
    '            ecMontantInitialPrix.Value = oAffaire.AffaireBudgetInitial
    '            ecMontantRevu.Value = "MONTANT REVU"
    '            ecMontantRevuPrix.Value = oAffaire.AffaireBudget
    '            ecMontantEntete.Value = "DIFFERENCE NET A PAYER EN €UROS"
    '            ecReceptionFactureEntete.Value = "A RECEPTION DE FACTURE"

    '            ecMontantTotaleuro.Value = "Total €uros"



    '            ' on va maintenant créer les dernières celules 
    '            Dim ecDesignationNbrePourcentage As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cNbre)
    '            Dim ecDesignationBudgetInitial As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cPUeuro)
    '            Dim ecDesignationTotalHT As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cTotaleuro)

    '            Dim ecReceptionFactureTotaleuros As ExcelCell = ws.Cells(lReceptionFactureEntetes, cTotaleuro)


    '            ' remplissage 

    '            ' les formules
    '            ecReceptionFactureTotaleuros.Formula = " = " & " E" & lMontantRevu + 1 & " - " & "E" & lMontantInitial + 1
    '            ' On ajuste la largeur des colonnes et des lignes
    '            ws.Columns(cEntetes).Width = 40 * 256
    '            ws.Columns(cTotaleuro).Width = 13 * 256

    '            ws.Rows(lDesignation).Height = 2 * 256


    '            'permet de fusionner les cellules 
    '            ws.Cells.GetSubrangeAbsolute(lDesignation, cEntetes, lDesignation, cTotaleuro).Merged = True
    '            ws.Cells.GetSubrangeAbsolute(lMontantInitial, cEntetes, lMontantInitial, cPUeuro).Merged = True
    '            ws.Cells.GetSubrangeAbsolute(lMontantRevu, cEntetes, lMontantRevu, cPUeuro).Merged = True

    '            ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete, cEntetes, lBonCommandeEntete, cTotaleuro).Merged = True

    '            ws.Cells.GetSubrangeAbsolute(lMontantEntetes, cEntetes, lMontantEntetes, cPUeuro).Merged = True
    '            ws.Cells.GetSubrangeAbsolute(lReceptionFactureEntetes, cEntetes, lReceptionFactureEntetes, cPUeuro).Merged = True

    '            ' les couleurs

    '            ecDesignation.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.PaleGreen, Drawing.Color.PaleGreen)


    '            ecMontantEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ecMontantInitial.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ws.Cells(lMontantInitial, cEntetes + 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ws.Cells(lMontantInitial, cNbre).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ws.Cells(lMontantInitial, cPUeuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            'ws.Cells(lMontantInitial, cTotaleuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ecMontantRevu.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ws.Cells(lMontantRevu, cEntetes + 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ws.Cells(lMontantRevu, cNbre).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ws.Cells(lMontantRevu, cPUeuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ' ws.Cells(lMontantRevu, cTotaleuro).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ecMontantTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '            ecReceptionFactureEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '            ' alignement du texte 
    '            ecRefEtDate.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
    '            ecDesignation.Style.VerticalAlignment = VerticalAlignmentStyle.Center


    '            ' les polices
    '            ecRefEtDate.Style.Font.Weight = 1000

    '            ecDesignation.Style.Font.Weight = 1000
    '            ecMontantInitial.Style.Font.Weight = 1000
    '            ecMontantInitialPrix.Style.Font.Weight = 1000
    '            ecMontantRevu.Style.Font.Weight = 1000
    '            ecMontantRevuPrix.Style.Font.Weight = 1000
    '            ecBonCommandeEntete.Style.Font.Weight = 1000

    '            ecMontantEntete.Style.Font.Weight = 1000
    '            ecMontantTotaleuro.Style.Font.Weight = 1000
    '            ecReceptionFactureEntete.Style.Font.Weight = 1000

    '            ecReceptionFactureTotaleuros.Style.Font.Weight = 1000

    '            ' les bordures
    '            ecDesignation.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ecBonCommandeEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ecMontantEntete.SetBorders(MultipleBorders.Top, Drawing.Color.Black, LineStyle.Thin)
    '            ecMontantEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '            'ecMontantInitial.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '            ecMontantInitial.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '            ecMontantInitial.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)
    '            ecMontantInitialPrix.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ecMontantRevu.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '            ecMontantRevu.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '            ecMontantRevu.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)
    '            ecMontantRevuPrix.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ecMontantTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ecReceptionFactureEntete.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '            ecReceptionFactureEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '            ecReceptionFactureEntete.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)

    '            ecReceptionFactureTotaleuros.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ' haut/pied page 
    '            'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
    '            Try
    '                ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom")) & CStr(dsClient.Tables(0).Rows(0)("ClientAdresse"))
    '            Catch
    '                ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom"))
    '            End Try

    '            Dim iRibID As Integer = oFacturationAffaireDAO.getRibIDAvecFacturationID(iFactureID)
    '            If Not iRibID = -1 Then
    '                Dim oRib As CRib = New CRib(CStr(iRibID))

    '                If oRib.Iban Then
    '                    ws.HeadersFooters.Footer = "IBAN AXE : " & oRib.Cp & oRib.Cc & " " & oRib.Cb.ToString.Substring(0, 4) & " " _
    '                        & oRib.Cb.ToString.Substring(4, 1) & oRib.Cg.ToString.Substring(0, 3) & " " & oRib.Cg.ToString.Substring(3, 2) _
    '                        & oRib.Nc.ToString.Substring(0, 2) & " " & oRib.Nc.ToString.Substring(2, 4) & " " & oRib.Nc.ToString.Substring(6, 4) _
    '                        & " " & oRib.Nc.ToString.Substring(10, 1) & oRib.Cle & " BRUZ 35170"
    '                Else
    '                    ws.HeadersFooters.Footer = "RIB AXE : " & oRib.RibLibelle & "    N° Banque : " & oRib.Cb & "" & vbCrLf & "Compte : " & oRib.Nc & "    Guichet : " & oRib.Cg & "    Clé : " & oRib.Cle & "    BRUZ 35170"

    '                End If
    '            End If

    '            With ws.PrintOptions

    '                .HeaderMargin = 0.5
    '                .FooterMargin = 0.5

    '                .TopMargin = 0.5
    '                .BottomMargin = 0.5
    '                .LeftMargin = 0.25
    '                .RightMargin = .LeftMargin

    '            End With

    '            Response.Clear()
    '            ' Enregistrement du fichier Excel

    '            Response.ContentType = "application/vnd.ms-excel"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & iFactureID & ".xls")
    '            Dim sUpload As String = CConfiguration.UploadPhysicalPath

    '            'Si le répertoire factures n'existe pas on le créer
    '            If Not System.IO.Directory.Exists(sUpload & "\Factures") Then
    '                System.IO.Directory.CreateDirectory(sUpload & "\Factures")
    '            End If
    '            ' Si le répertoire de l'affaire n'existe pas on le créer
    '            If Not System.IO.Directory.Exists(sUpload & "\Factures\" & iAffaireID) Then
    '                System.IO.Directory.CreateDirectory(sUpload & "\Factures\" & iAffaireID)
    '            End If

    '            'save dans le répertoire upload sur le serveur
    '            ef.SaveXls(CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactureID & ".xls")

    '            ef.SaveXls(Response.OutputStream)
    '            Response.ClearHeaders()

    '        ElseIf dDiff < 0 Then

    '            Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
    '            Dim oFacturationAffaire As New CFacturationAffaire

    '            'on récupère l'id de l'affaire concernee
    '            iAffaireID = CInt(Request.QueryString("id"))

    '            'on instancie un objet de type FacturationAffaire pour l'insertion
    '            oFacturationAffaire.Affaire.AffaireID = iAffaireID
    '            oFacturationAffaire.FacturationAffaireDate = Now
    '            oFacturationAffaire.FacturationAffaireLibelle = tbLibelleAvoir.Text
    '            oFacturationAffaire.FacturationAffaireRef = lblRefAv.Text


    '            Dim iRes As Integer = oFacturationAffaireDAO.InsererAvoirAffaire(oFacturationAffaire)

    '            If iRes = 0 Then
    '                lblMsg.Visible = True
    '                lblMsg.Text = "Le nouvel avoir n'a pas pu être enregistré"
    '                lblMsg.ForeColor = Drawing.Color.Red
    '            Else
    '                tbLibelleAvoir.Text = oAffaire.AffaireLibelle
    '                tbMntAvoir.Text = CStr(-dDiff)
    '                exportAvoir(sRefFacture)
    '                lblMsg.Visible = True
    '                lblMsg.Text = "Le nouvel avoir a bien été enregistré"
    '                lblMsg.ForeColor = Drawing.Color.Blue

    '                fsAvoir.Visible = False
    '                btnSaveAvoir.Visible = False
    '                btnAnnulerAvoir.Visible = False
    '                LoadGridView()
    '            End If

    '            Response.Redirect("AffaireFacturation.aspx?id=" & iAffaireID)
    '        End If
    '    End If
    'End Sub
    'Private Sub ExportFactureContratCadre(sRefFacture As String)
    '    ' Initialisation de l'objet Excel
    '    SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
    '    Dim ef As ExcelFile = New ExcelFile
    '    Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture Presta et frais")
    '    Dim nbMois As Integer = CInt(ddlPourcentageMoisFacturation.SelectedItem.Text) - CInt(ddlPourcentageMoisFacturation.Items(0).Text) + 1

    '    'ligne
    '    Dim lRefEtDate As Integer = 6
    '    Dim lTVAAXE As Integer = 9
    '    Dim lTVAClient As Integer = 10
    '    Dim lDesignationEntetes As Integer = 14
    '    Dim lDesignation As Integer = 15
    '    Dim lBonCommandeEntete As Integer = 16
    '    Dim lDesignationStotalHT As Integer = 17
    '    Dim lDesignationTVA As Integer = 18
    '    Dim lDesignationStotalTTC As Integer = 19

    '    Dim lFraisEntetes As Integer = 22
    '    Dim lFraisStotalHT As Integer = 23
    '    Dim lFraisTVA As Integer = 24
    '    Dim lFraisStotalTTC As Integer = 25

    '    Dim lMontantEntetes As Integer = 29
    '    Dim lReceptionFactureEntetes As Integer = 30

    '    'Colonne

    '    Dim cEntetes As Integer = 0
    '    Dim cNbre As Integer = 2
    '    Dim cPUeuro As Integer = 3
    '    Dim cTotaleuro As Integer = 4

    '    ' maintenant on va remplir les cellules avec les datasets
    '    ' création des datasets
    '    Dim dsClient As New DataSet
    '    'Dim dsProduitsFacturees As New DataSet
    '    Dim dsForfait As New DataSet

    '    Dim dsPourcentageAffaire As New DataSet
    '    Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
    '    Dim dPourcentage As Double = 0
    '    Dim dBudget As Double = 0
    '    Dim iDecalageLigneDesignation As Integer = 0
    '    Dim iDecalageLigneFrais As Integer = 0
    '    Dim iFactureID As Integer = oFacturationAffaireDAO.GetFacturationAffaireIDByRef(sRefFacture)
    '    Dim oAffaire As New CAffaireDAO
    '    Dim dBudgetInitial As Decimal = oAffaire.getAffaireBudgetInitial(iAffaireID)

    '    'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
    '    dsClient = oFacturationAffaireDAO.getInfoPourExportFacture(sRefFacture)

    '    'AffaireBudget, Qte, ProduitAffaireLibelle, MntUnitHT, ProduitAffaireDate, TypeAffaireID
    '    'dsProduitsFacturees = oFacturationAffaireDAO.getProduitAFactureesPourExportFacture(sRefFacture)

    '    dsForfait = oFacturationAffaireDAO.getForfaitPourExportFacture(iAffaireID)

    '    iDecalageLigneDesignation += 1


    '    If oAffaire.GetAffaire(iAffaireID).FraisInclus = 0 Then
    '        Dim dsFraisFactureesContratCadreEtRegie As DataSet = oFacturationAffaireDAO.getFraisPourExportFactureContratCadreEtRegie(sRefFacture)
    '        Try
    '            For Each row As DataRow In dsFraisFactureesContratCadreEtRegie.Tables(0).Rows

    '                Dim dQte As Double = CDbl(row("Qte"))
    '                Dim dMntUnitHT As Double = CDbl(row("MntUnitHT"))


    '                If Not IsDBNull(row("ProduitAffaireLibelle")) Then
    '                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = CStr(row("ProduitAffaireLibelle"))
    '                Else
    '                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = ""
    '                End If

    '                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cNbre).Value = dQte
    '                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cNbre).Style.NumberFormat = "0.00"
    '                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cPUeuro).Value = dMntUnitHT
    '                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cPUeuro).Style.NumberFormat = "0.00"

    '                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Formula = "= C" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2 & "*D" & lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 2

    '                iDecalageLigneFrais += 1

    '                'alignement du texte 
    '                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center

    '                'bordures
    '                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '                ' On ajuste la largeur des colonnes et des lignes
    '                ws.Rows(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation).Height = CInt(1.5 * 256)

    '                ' permet de fusionner les lignes 
    '                ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes, lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation, cEntetes + 1).Merged = True

    '            Next
    '        Catch

    '        End Try

    '        If iDecalageLigneFrais = 0 Then

    '            iDecalageLigneFrais = 1

    '            'bordures
    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '            ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '            ' permet de fusionner les lignes 
    '            ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes, lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True
    '        End If
    '    End If

    '    'on applique le decalage de ligne
    '    'ligne
    '    lDesignationStotalHT = 17 + iDecalageLigneDesignation
    '    lDesignationTVA = 18 + iDecalageLigneDesignation
    '    lDesignationStotalTTC = 19 + iDecalageLigneDesignation

    '    lFraisEntetes = 22 + iDecalageLigneDesignation
    '    lFraisStotalHT = 23 + iDecalageLigneDesignation + iDecalageLigneFrais
    '    lFraisTVA = 24 + iDecalageLigneDesignation + iDecalageLigneFrais
    '    lFraisStotalTTC = 25 + iDecalageLigneDesignation + iDecalageLigneFrais

    '    lMontantEntetes = 29 + iDecalageLigneDesignation + iDecalageLigneFrais
    '    lReceptionFactureEntetes = 30 + iDecalageLigneDesignation + iDecalageLigneFrais

    '    'création des cellules
    '    'colonne A
    '    Dim ecDesignationEntete As ExcelCell = ws.Cells(lDesignationEntetes, cEntetes)
    '    Dim ecDesignation As ExcelCell = ws.Cells(lDesignation, cEntetes)
    '    Dim ecBonCommandeEntete As ExcelCell = ws.Cells(lBonCommandeEntete, cEntetes)
    '    Dim ecFraisEntete As ExcelCell = ws.Cells(lFraisEntetes, cEntetes)
    '    Dim ecMontantEntete As ExcelCell = ws.Cells(lMontantEntetes, cEntetes)
    '    Dim ecReceptionFactureEntete As ExcelCell = ws.Cells(lReceptionFactureEntetes, cEntetes)
    '    Dim ecTVAAXE As ExcelCell = ws.Cells(lTVAAXE, cEntetes)
    '    Dim ecTVAClient As ExcelCell = ws.Cells(lTVAClient, cEntetes)

    '    'colonne C
    '    Dim ecDesignationNbre As ExcelCell = ws.Cells(lDesignationEntetes, cNbre)
    '    Dim ecFraisNbre As ExcelCell = ws.Cells(lFraisEntetes, cNbre)

    '    'colonne D
    '    Dim ecDesignationPUeuro As ExcelCell = ws.Cells(lDesignationEntetes, cPUeuro)
    '    Dim ecFraisPUeuro As ExcelCell = ws.Cells(lFraisEntetes, cPUeuro)

    '    'colonne E
    '    Dim ecRefEtDate As ExcelCell = ws.Cells(lRefEtDate, cTotaleuro)
    '    Dim ecDesignationTotaleuro As ExcelCell = ws.Cells(lDesignationEntetes, cTotaleuro)
    '    Dim ecFraisTotaleuro As ExcelCell = ws.Cells(lFraisEntetes, cTotaleuro)
    '    Dim ecMontantTotaleuro As ExcelCell = ws.Cells(lMontantEntetes, cTotaleuro)


    '    ' Remplissage des cellules
    '    ecRefEtDate.Value = "FACTURE N°" & sRefFacture & " EN DATE DU " & tbFactureDate.Text.Split(CChar("/"))(0) & "/" & tbFactureDate.Text.Split(CChar("/"))(1) & "/" & tbFactureDate.Text.Split(CChar("/"))(2)

    '    ecTVAAXE.Value = "N° TVA intracommunautaire AXE : FR 92 429 489 966"
    '    ecTVAClient.Value = "N° TVA intracommunautaire " & dsClient.Tables(0).Rows(0)("ClientNom").ToString & " : " & dsClient.Tables(0).Rows(0)("ClientFacturationNumTVA").ToString

    '    ecDesignationEntete.Value = "Désignation"
    '    ecDesignationNbre.Value = "Nbre"
    '    ecDesignationPUeuro.Value = "PU €uros"
    '    ecDesignationTotaleuro.Value = "Total €uros"

    '    ecDesignation.Value = dsClient.Tables(0).Rows(0)("ClientNom")

    '    If tbNumBonCommande.Text = "" Then
    '        ecBonCommandeEntete.Value = "BON DE COMMANDE "
    '    Else
    '        ecBonCommandeEntete.Value = "BON DE COMMANDE n° " & tbNumBonCommande.Text
    '    End If

    '    ecFraisEntete.Value = "Frais à TVA déductible"
    '    ecFraisNbre.Value = "Nbre"
    '    ecFraisPUeuro.Value = "PU €uros"
    '    ecFraisTotaleuro.Value = "Total €uros"

    '    ecMontantEntete.Value = "MONTANT NET A PAYER EN €UROS"
    '    ecReceptionFactureEntete.Value = "A RECEPTION DE FACTURE"

    '    ecMontantTotaleuro.Value = "Total €uros"



    '    ' on va maintenant créer les dernières celules 
    '    Dim ecDesignationStotalHT As ExcelCell = ws.Cells(lDesignationStotalHT, cPUeuro)
    '    Dim ecDesignationTVA As ExcelCell = ws.Cells(lDesignationTVA, cPUeuro)
    '    Dim ecDesignationStotalTTC As ExcelCell = ws.Cells(lDesignationStotalTTC, cPUeuro)
    '    Dim ecDesignationStotalHTSom As ExcelCell = ws.Cells(lDesignationStotalHT, cTotaleuro)
    '    Dim ecDesignationTVASom As ExcelCell = ws.Cells(lDesignationTVA, cTotaleuro)
    '    Dim ecDesignationStotalTTCSom As ExcelCell = ws.Cells(lDesignationStotalTTC, cTotaleuro)

    '    Dim ecDesignationNbrePourcentage As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cNbre)
    '    Dim ecDesignationBudgetInitial As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cPUeuro)
    '    Dim ecDesignationTotalHT As ExcelCell = ws.Cells(lBonCommandeEntete + 1, cTotaleuro)

    '    Dim ecFraisStotalHT As ExcelCell = ws.Cells(lFraisStotalHT, cPUeuro)
    '    Dim ecFraisTVA As ExcelCell = ws.Cells(lFraisTVA, cPUeuro)
    '    Dim ecFraisStotalTTC As ExcelCell = ws.Cells(lFraisStotalTTC, cPUeuro)
    '    Dim ecFraisStotalHTSom As ExcelCell = ws.Cells(lFraisStotalHT, cTotaleuro)
    '    Dim ecFraisTVASom As ExcelCell = ws.Cells(lFraisTVA, cTotaleuro)
    '    Dim ecFraisStotalTTCSom As ExcelCell = ws.Cells(lFraisStotalTTC, cTotaleuro)

    '    Dim ecReceptionFactureTotaleuros As ExcelCell = ws.Cells(lReceptionFactureEntetes, cTotaleuro)


    '    ' remplissage 
    '    ecDesignationStotalHT.Value = "S/total HT"
    '    ecDesignationTVA.Value = "TVA 19,60%"
    '    ecDesignationStotalTTC.Value = "S/total TTC"

    '    ecDesignationNbrePourcentage.Value = nbMois / 12
    '    ecDesignationNbrePourcentage.Style.NumberFormat = "#"" ""??/??"
    '    ecDesignationBudgetInitial.Value = "" & dBudgetInitial
    '    ecDesignationBudgetInitial.Style.NumberFormat = "0.00"
    '    ecFraisStotalHT.Value = "S/total HT"
    '    ecFraisTVA.Value = "TVA 19,60%"
    '    ecFraisStotalTTC.Value = "S/total TTC"



    '    ' les formules
    '    ecDesignationTotalHT.Formula = "=C" & lBonCommandeEntete + 2 & " *  D" & lBonCommandeEntete + 2
    '    ecDesignationTotalHT.Style.NumberFormat = "0.00"
    '    ecDesignationStotalHTSom.Formula = "=SUM(E" & lBonCommandeEntete + 2 & ":E" & lBonCommandeEntete + iDecalageLigneDesignation + 1 & ")"
    '    ecDesignationStotalHTSom.Style.NumberFormat = "0.00"
    '    ecDesignationTVASom.Formula = "= E" & lDesignationStotalHT + 1 & "*0.196"
    '    ecDesignationTVASom.Style.NumberFormat = "0.00"
    '    ecDesignationStotalTTCSom.Formula = "=SUM(E" & lDesignationTVA + 1 & ":E" & lDesignationStotalHT + 1 & ")"
    '    ecDesignationStotalTTCSom.Style.NumberFormat = "0.00"
    '    ecFraisStotalHTSom.Formula = "=SUM(E" & lFraisEntetes + 2 & ":E" & lFraisEntetes + iDecalageLigneFrais + 1 & ")"
    '    ecFraisStotalHTSom.Style.NumberFormat = "0.00"
    '    ecFraisTVASom.Formula = "= E" & lFraisStotalHT + 1 & "*0.196"
    '    ecFraisTVASom.Style.NumberFormat = "0.00"
    '    ecFraisStotalTTCSom.Formula = "=SUM(E" & lFraisTVA + 1 & ":E" & lFraisStotalHT + 1 & ")"
    '    ecFraisStotalTTCSom.Style.NumberFormat = "0.00"
    '    ecReceptionFactureTotaleuros.Formula = "= E" & lFraisStotalTTC + 1 & "+E" & lDesignationStotalTTC + 1
    '    ecReceptionFactureTotaleuros.Style.NumberFormat = "0.00"
    '    ' On ajuste la largeur des colonnes et des lignes
    '    ws.Columns(cEntetes).Width = 40 * 256
    '    ws.Columns(cTotaleuro).Width = 13 * 256

    '    ws.Rows(lDesignation).Height = 2 * 256


    '    'permet de fusionner les cellules 
    '    ws.Cells.GetSubrangeAbsolute(lDesignationEntetes, cEntetes, lDesignationEntetes, 1).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lDesignation, cEntetes, lDesignation, cTotaleuro).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete, cEntetes, lBonCommandeEntete, cTotaleuro).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lFraisEntetes, cEntetes, lFraisEntetes, 1).Merged = True

    '    ws.Cells.GetSubrangeAbsolute(lMontantEntetes, cEntetes, lMontantEntetes, cPUeuro).Merged = True
    '    ws.Cells.GetSubrangeAbsolute(lReceptionFactureEntetes, cEntetes, lReceptionFactureEntetes, cPUeuro).Merged = True

    '    ' les couleurs
    '    ecDesignationEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecDesignationNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecDesignationPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecDesignationTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ecDesignation.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.PaleGreen, Drawing.Color.PaleGreen)

    '    ecBonCommandeEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ecFraisEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecFraisNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecFraisPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecFraisTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ecMontantEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecMontantTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
    '    ecReceptionFactureEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

    '    ' alignement du texte 
    '    ecRefEtDate.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right

    '    ecDesignationNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecDesignationPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecDesignationTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

    '    ecDesignation.Style.VerticalAlignment = VerticalAlignmentStyle.Center

    '    ecFraisNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecFraisPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
    '    ecFraisTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

    '    ' les polices
    '    ecRefEtDate.Style.Font.Weight = 1000

    '    ecDesignationEntete.Style.Font.Weight = 1000
    '    ecDesignationNbre.Style.Font.Weight = 1000
    '    ecDesignationPUeuro.Style.Font.Weight = 1000
    '    ecDesignationTotaleuro.Style.Font.Weight = 1000

    '    ecDesignation.Style.Font.Weight = 1000

    '    ecBonCommandeEntete.Style.Font.Weight = 1000

    '    ecFraisEntete.Style.Font.Weight = 1000
    '    ecFraisNbre.Style.Font.Weight = 1000
    '    ecFraisPUeuro.Style.Font.Weight = 1000
    '    ecFraisTotaleuro.Style.Font.Weight = 1000

    '    ecMontantEntete.Style.Font.Weight = 1000
    '    ecMontantTotaleuro.Style.Font.Weight = 1000
    '    ecReceptionFactureEntete.Style.Font.Weight = 1000

    '    ecDesignationStotalHT.Style.Font.Weight = 1000
    '    ecDesignationTVA.Style.Font.Weight = 1000
    '    ecDesignationStotalTTC.Style.Font.Weight = 1000
    '    ecDesignationStotalHTSom.Style.Font.Weight = 1000
    '    ecDesignationTVASom.Style.Font.Weight = 1000
    '    ecDesignationStotalTTCSom.Style.Font.Weight = 1000

    '    ecFraisStotalHT.Style.Font.Weight = 1000
    '    ecFraisTVA.Style.Font.Weight = 1000
    '    ecFraisStotalTTC.Style.Font.Weight = 1000
    '    ecFraisStotalHTSom.Style.Font.Weight = 1000
    '    ecFraisTVASom.Style.Font.Weight = 1000
    '    ecFraisStotalTTCSom.Style.Font.Weight = 1000

    '    ecReceptionFactureTotaleuros.Style.Font.Weight = 1000

    '    ' les bordures
    '    ecDesignationEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecDesignation.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecBonCommandeEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecMontantEntete.SetBorders(MultipleBorders.Top, Drawing.Color.Black, LineStyle.Thin)
    '    ecMontantEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ecMontantTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecReceptionFactureEntete.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '    ecReceptionFactureEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ecReceptionFactureEntete.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)

    '    ecDesignationStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ws.Cells(lBonCommandeEntete + 1, cEntetes).SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '    ws.Cells(lBonCommandeEntete + 1, cEntetes).SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ws.Cells(lBonCommandeEntete + 1, cEntetes + 1).SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationNbrePourcentage.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationNbrePourcentage.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationBudgetInitial.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationBudgetInitial.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTotalHT.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTotalHT.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTotalHT.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)

    '    ecDesignationStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecDesignationStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecFraisStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
    '    ecFraisStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ecReceptionFactureTotaleuros.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

    '    ' haut/pied page 
    '    'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
    '    Try
    '        ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom")) & CStr(dsClient.Tables(0).Rows(0)("ClientAdresse"))
    '    Catch
    '        ws.HeadersFooters.Header = CStr(dsClient.Tables(0).Rows(0)("ClientNom"))
    '    End Try

    '    Dim iRibID As Integer = oFacturationAffaireDAO.getRibIDAvecFacturationID(iFactureID)
    '    If Not iRibID = -1 Then
    '        Dim oRib As CRib = New CRib(CStr(iRibID))

    '        If oRib.Iban Then
    '            ws.HeadersFooters.Footer = "IBAN AXE : " & oRib.Cp & oRib.Cc & " " & oRib.Cb.ToString.Substring(0, 4) & " " _
    '                & oRib.Cb.ToString.Substring(4, 1) & oRib.Cg.ToString.Substring(0, 3) & " " & oRib.Cg.ToString.Substring(3, 2) _
    '                & oRib.Nc.ToString.Substring(0, 2) & " " & oRib.Nc.ToString.Substring(2, 4) & " " & oRib.Nc.ToString.Substring(6, 4) _
    '                & " " & oRib.Nc.ToString.Substring(10, 1) & oRib.Cle & " BRUZ 35170"
    '        Else
    '            ws.HeadersFooters.Footer = "RIB AXE : " & oRib.RibLibelle & "    N° Banque : " & oRib.Cb & "" & vbCrLf & "Compte : " & oRib.Nc & "    Guichet : " & oRib.Cg & "    Clé : " & oRib.Cle & "    BRUZ 35170"

    '        End If
    '    End If

    '    With ws.PrintOptions

    '        .HeaderMargin = 0.5
    '        .FooterMargin = 0.5

    '        .TopMargin = 0.5
    '        .BottomMargin = 0.5
    '        .LeftMargin = 0.25
    '        .RightMargin = .LeftMargin

    '    End With

    '    Response.Clear()
    '    ' Enregistrement du fichier Excel
    '    'Dim sNomFichier = "Facture " & sRefFacture

    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AddHeader("Content-Disposition", "attachment; filename=" & iFactureID & ".xls")
    '    Dim sUpload As String = CConfiguration.UploadPhysicalPath

    '    'Si le répertoire factures n'existe pas on le créer
    '    If Not System.IO.Directory.Exists(sUpload & "\Factures") Then
    '        System.IO.Directory.CreateDirectory(sUpload & "\Factures")
    '    End If
    '    ' Si le répertoire de l'affaire n'existe pas on le créer
    '    If Not System.IO.Directory.Exists(sUpload & "\Factures\" & iAffaireID) Then
    '        System.IO.Directory.CreateDirectory(sUpload & "\Factures\" & iAffaireID)
    '    End If

    '    'save dans le répertoire upload sur le serveur
    '    ef.SaveXls(CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID & "\" & iFactureID & ".xls")

    '    ef.SaveXls(Response.OutputStream)
    '    Response.ClearHeaders()
    '    LoadRef()
    '    ExportTreize(lblFactureRef.Text)

    'End Sub

    Private Sub UpdateMontantFacturationAffaire(ByVal iAffaireID As Integer, ByVal oFacturationAffaire As CFacturationAffaire)
        Dim oFactu As New CFacturationAffaireDAO
        oFactu.UpDateMontantPresta(iAffaireID, oFacturationAffaire)
        oFactu.UpDateMontantFrais(iAffaireID, oFacturationAffaire)
    End Sub


End Class