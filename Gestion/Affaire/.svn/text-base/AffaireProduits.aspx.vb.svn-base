Imports ComptaAna.Business
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit
Imports System.Drawing

Public Class AffaireProduits
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' chargement de la page, chargement des listes
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            chargerListeEmployes()
            chargerListeTypesProduits()
            chargerListeService()
            chargerListeAffaireProduit()

            Dim oAffaire As New CAffaireDAO
            lblNomAffaire.Text = oAffaire.GetAffaireLibelle(CInt(Request.QueryString("id")))
            tbAffaire.Text = lblNomAffaire.Text
            tbAffaireID.Text = Request.QueryString("id")

            'charge tout les liste du relevé d'activité
            chargerListeTypeProduit(True)
            chargerListeService()
            chargerListeQualification()
            chargerListeSite()
            chargerListeTVA()


            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesFacture) Then
                    mMenuAffaireModif.FindItem("2").Enabled = False
                End If
                If Not verifDroit(lDroit, eModule.AccesAffaireEcriture) Then
                    ' mMenuAffaireModif.FindItem("0").Enabled = False
                End If

            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            mMenuAffaireModif.Items(2).Selected = True
        End If

        lblMsg.Text = ""

    End Sub

    ''' <summary>
    ''' Charge la liste des qualification
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeQualification()
        Dim oQualificationDAO As CQualificationDAO = New CQualificationDAO
        Dim oEmploye As CEmploye = New CEmploye
        Dim ds As DataSet

        oEmploye = CType(Session("Employe"), CEmploye)
        ds = oQualificationDAO.GetAllQualificationToList

        ddlQualification.DataSource = ds
        ddlQualification.DataBind()

        'Auto Select la qualification de la personne connectée
        ddlQualification.SelectedValue = CStr(oEmploye.QualificationID)

    End Sub

    ''' <summary>
    ''' charge la liste des types de produit
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeTypeProduit(ByVal estAssocie As Boolean)
        Dim oTypeProduitDAO As CTypeProduitDAO = New CTypeProduitDAO
        Dim ds As DataSet

        ds = oTypeProduitDAO.GetAllTypeProduitFiltreParAssociation(estAssocie)

        ddlType.DataSource = ds
        ddlType.DataBind()

        ddlType.Items.Insert(0, New ListItem("-- Type --", "default"))
    End Sub

    ''' <summary>
    ''' charge la liste de TVA
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeTVA()
        Dim oTVADAO As CTVADAO = New CTVADAO
        Dim ds As DataSet

        ds = oTVADAO.GetAllTvaActif()

        ddlTVA.DataSource = ds
        ddlTVA.DataBind()
    End Sub


    ''' <summary>
    ''' Charge la liste des produits
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargerListeProduit(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        Dim oProduitDAO As CProduitDAO = New CProduitDAO
        Dim ds As DataSet


        'remise à zero des champs libelle, qut, Pu ht, tva
        remiseAZeroChamps()

        If Not (ddlType.SelectedItem.Value = "default") Then
            ds = oProduitDAO.GetAllProduitByTypeProduitID(CInt(ddlType.SelectedItem.Value))

            If (ds.Tables(0).Rows.Count > 0) Then
                ddlProduit.DataSource = ds
                ddlProduit.DataBind()
            Else
                ddlProduit.Items.Clear()
                ddlProduit.Items.Insert(0, New ListItem("-- Produit --", "default"))
            End If
        Else
            ddlProduit.Items.Clear()
            ddlProduit.Items.Insert(0, New ListItem("-- Produit --", "default"))
        End If

        Dim oProduit As New CProduit
        oProduit = oProduitDAO.GetProduit(CInt(ddlProduit.SelectedValue))

        ' On ajoute un astérisque avec une info-bulle pour avertir l'utilisateur que le produit en question
        ' sera facturé en TTC
        If oProduit.ProduitRefac Then
            lblWarningTTC.Visible = True
        Else
            lblWarningTTC.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Permet de remettre les champs libelle, qut, Pu ht, tva
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub remiseAZeroChamps()
        Select Case ddlType.SelectedItem.Text
            Case "Frais", "Outils"
                tbLibelle.Text = ""
                tbQuantite.Text = "0"
                tbPrixUnit.Text = "0"
            Case Else
                Dim oAffaire As CAffaire = New CAffaire
                Dim oPrixQualifTypeAffaireDAO As CPrixQualifTypeAffaireDAO = New CPrixQualifTypeAffaireDAO
                Dim oAffaireDAO As CAffaireDAO = New CAffaireDAO

                ' Pour rétablir le prix unit ...
                oAffaire = oAffaireDAO.GetAffaire(CInt(tbAffaireID.Text))
                tbPrixUnit.Text = oPrixQualifTypeAffaireDAO.GetPrixHTQualifAffaire(CInt(ddlQualification.SelectedValue), CInt(tbAffaireID.Text), oAffaire.TypeAffaire.TypeAffaireID).ToString
                tbLibelle.Text = ""
                tbQuantite.Text = "0"
        End Select
    End Sub

    ''' <summary>
    ''' Charge la liste des site
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargerListeSite()
        lblMsg.Text = ""

        Dim oClientSiteDAO As CClientDAO = New CClientDAO
        Dim oAffaireDAO As CAffaireDAO = New CAffaireDAO
        Dim oAffaire As CAffaire = New CAffaire
        Dim ds As DataSet


        oAffaire = oAffaireDAO.GetAffaire(CInt(tbAffaireID.Text))
        ds = oClientSiteDAO.GetAllClientSiteByAffaireIdToListe(CInt(tbAffaireID.Text), oAffaire.Client.ClientID)

        ddlSite.DataSource = ds
        ddlSite.DataBind()
        ddlSite.SelectedIndex = 0
        ddlService.SelectedValue = CStr(oAffaire.Service.ServiceID)




            Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
            tbPrixUnit.Text = oPrixQualifTypeAffaireDAO.GetPrixHTQualifAffaire(CInt(ddlQualification.SelectedValue), CInt(tbAffaireID.Text), oAffaire.TypeAffaire.TypeAffaireID).ToString



    End Sub

    ''' <summary>
    ''' charge la liste des employes pour les restrictions
    ''' </summary>
    Protected Sub chargerListeEmployes()

        Dim oEmployeDAO As New CEmployeDAO
        Dim dsEmployes As DataSet

        dsEmployes = oEmployeDAO.GetNomPrenomEmployeActif()

        'Pour la fenêtre
        ddlEmployes.DataSource = dsEmployes
        ddlEmployes.DataBind()

        ddlEmployes.Items.Insert(0, New ListItem("-- Tous les employés actifs --", "-1"))

        'Pour la popup
        ddlEmployeConcerne.DataSource = dsEmployes
        ddlEmployeConcerne.DataBind()

        ddlEmployeConcerne.Items.Insert(0, New ListItem("-- Tous les employés actifs --", "-1"))
    End Sub
    ''' <summary>
    ''' charge la liste des employes pour les restrictions
    ''' </summary>
    Protected Sub chargerListeService()

        Dim oServiceDAO As New CServiceDAO
        Dim dsServices As DataSet

        dsServices = oServiceDAO.GetAllServiceToList()

        ddlBU.DataSource = dsServices
        ddlBU.DataBind()

        ddlBU.Items.Insert(0, New ListItem("-- Tous les services --", "-1"))

        'Pour la popup
        ddlService.DataSource = dsServices
        ddlService.DataBind()

        ddlService.Items.Insert(0, New ListItem("-- Tous les services --", "-1"))
    End Sub

    ''' <summary>
    ''' charge la liste des types de produits pour les restrictions
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeTypesProduits()

        Dim oTypeProduitDAO As New CTypeProduitDAO
        Dim dsTypes As DataSet

        dsTypes = oTypeProduitDAO.GetAllTypeProduit

        ddlTypes.DataSource = dsTypes
        ddlTypes.DataBind()

        ddlTypes.Items.Insert(0, New ListItem("-- Tous les types de produits --", "-1"))
    End Sub

    ''' <summary>
    ''' chargement du gridview avec les produits qui correspondent a l'affaire
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeAffaireProduit()

        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduitAffaire As New DataSet

        dsProduitAffaire = oProduitAffaireDAO.GetProduitAffaireAssocieByAffaireID(CInt(Request.QueryString("id")), -1, -1, -1, "", "")

        gvProduitAffaire.DataSource = dsProduitAffaire
        gvProduitAffaire.DataBind()

    End Sub

    Private Sub gvProduitAffaire_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduitAffaire.RowCommand
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oAffaireDAO As New CAffaireDAO

        If e.CommandName = "DeleteProduitAffaire" Then

            Try
                Dim iProduitAffaireID As Integer = CInt(e.CommandArgument.ToString)
                Dim oEmploye As New CEmploye
                oEmploye = CType(Session("Employe"), CEmploye)

                oProduitAffaireDAO.SupprimerProduitAffaire(iProduitAffaireID)

                chargerListeEmployes()
                chargerListeTypesProduits()

                chargerListeAffaireProduit()

                oAffaireDAO.UpdateSommeProduitsParAffaire(CInt(Request.QueryString("id")))
                oAffaireDAO.UpdateProduitsDepassement(CInt(Request.QueryString("id")))

                gvProduitAffaire.EditIndex = -1
                chargerListeAffaireProduit()

                lblMsg.Text = vbLf & "Suppression réussie"
            Catch ex As InvalidCastException
                lblMsg.Text = "Ce produit ne peut pas être supprimé !"
            End Try

        ElseIf e.CommandName = "AjouterProduit" Then

            ' Dim iProduitAffaireID As Integer
            Dim oEmploye As New CEmploye


            'oEmploye = CType(Session("Employe"), CEmploye)
            Dim iAffaireId As Integer = CInt(Request.QueryString("id"))
            Dim sAffaireLibelle As String = lblNomAffaire.Text
            tbAffaire.Text = sAffaireLibelle

            pPopupAjoutProduit.Visible = True
            gvProduitAffaire.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' colore les lignes du gridview 
    ''' </summary>
    Protected Sub gvProduitAffaire_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvProduitAffaire.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oEmploye As New CEmployeDAO

            If drv("ProduitAffaireFacture").ToString = "1" Then
                e.Row.BackColor = Drawing.Color.FromArgb(36, 255, 146)
                'e.Row.Enabled = False
                e.Row.Cells(12).Visible = False
                e.Row.Cells(13).Visible = False

                    End If

                    If drv("ProduitAffaireDate").ToString = "" Then
                        Dim RefCell As New TableCell()
                        RefCell.Text = drv("ProduitAffaireLibelle").ToString
                        e.Row.Cells.Clear()
                        e.Row.Cells.Add(RefCell)
                        e.Row.Cells(0).ColumnSpan = 5

                        Dim QteCell As New TableCell()
                        QteCell.Text = drv("ProduitAffaireQte").ToString
                        e.Row.Cells.Add(QteCell)

                        Dim TotCell As New TableCell()
                        TotCell.Text = drv("TotalHT").ToString
                        e.Row.Cells.Add(TotCell)
                        e.Row.Cells(2).ColumnSpan = 6

                        e.Row.BackColor = Drawing.Color.FromArgb(229, 243, 255)
                    End If

                    If Not drv("ProduitAffaireDate").ToString = "" And Not drv("FactureAffaireID").ToString = "" Then
                        e.Row.BackColor = Drawing.Color.FromArgb(230, 255, 204)
                    End If

                    If drv("EmployeId").ToString <> "" Then
                        e.Row.Cells(3).Text = drv("ProduitRef").ToString & " - " & oEmploye.GetNomPrenomFromId(CLng(drv("EmployeId"))).ToString

                        Dim iProduitAffaireID As Integer = CInt(drv("ProduitAffaireID"))
                        Dim oProduitAffaireDAO As New CProduitAffaireDAO
                        Dim ds As DataSet = oProduitAffaireDAO.LoadOne(iProduitAffaireID)
                        Dim iProduitID As Integer = 0
                        Dim dTvaMontant As Double = CDbl(drv("TvaTaux"))

                        If ds.Tables(0).Rows.Count > 0 Then
                            iProduitID = CInt(ds.Tables(0).Rows(0)(ProduitAffaire.ProduitID))
                        End If

                        Dim oProduit As New CProduit
                        Dim oProduitDAO As New CProduitDAO

                        oProduit = oProduitDAO.GetProduit(iProduitID)

                        If oProduit.ProduitRefac Then
                            If gvProduitAffaire.EditIndex > 0 Then
                                If Not IsNothing(e.Row.Cells(11).FindControl("tbEditProduitAffaireMntUnitHT")) Then
                                    CType(e.Row.Cells(11).FindControl("tbEditProduitAffaireMntUnitHT"), TextBox).Text = GetMontantHT(CDbl(drv("ProduitAffaireMntUnitHT")), dTvaMontant).ToString
                                End If
                            End If
                        End If
                    End If

                    If Not IsDBNull(drv("ProduitAffaireDepassementMnt")) Then
                        For i As Integer = 0 To gvProduitAffaire.Columns.Count - 1
                            e.Row.Cells(i).ForeColor = Drawing.Color.FromArgb(230, 0, 115)
                        Next
                    End If

                End If
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


    ''' <summary>
    '''  filtrage en fonction de l'employé selectionne et/ou du type de produit selectionne
    ''' </summary>
    Protected Sub ddlEmployesAndTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEmployes.SelectedIndexChanged, ddlTypes.SelectedIndexChanged, ddlBU.SelectedIndexChanged

        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduitAffaire As New DataSet

        dsProduitAffaire = oProduitAffaireDAO.GetProduitAffaireAssocieByAffaireID(CInt(Request.QueryString("id")), CInt(ddlEmployes.SelectedValue), CInt(ddlTypes.SelectedValue), CInt(ddlBU.SelectedValue), tbDateDeb.Text, tbDateFin.Text)

        gvProduitAffaire.DataSource = dsProduitAffaire
        gvProduitAffaire.DataBind()

    End Sub

    ''' <summary>
    ''' filtrage avec les dates lors du click sur le bouton valider
    ''' </summary>
    Protected Sub ibValider_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibValider.Click
        ChargerGvAffaireProduit()

    End Sub

    Protected Sub ChargerGvAffaireProduit()
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduitAffaire As New DataSet

        dsProduitAffaire = oProduitAffaireDAO.GetProduitAffaireAssocieByAffaireID(CInt(Request.QueryString("id")), CInt(ddlEmployes.SelectedValue), CInt(ddlTypes.SelectedValue), CInt(ddlBU.SelectedValue), tbDateDeb.Text, tbDateFin.Text)

        gvProduitAffaire.DataSource = dsProduitAffaire
        gvProduitAffaire.DataBind()
    End Sub

    Protected Sub gvProduitAffaire_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvProduitAffaire.RowEditing
        gvProduitAffaire.EditIndex = e.NewEditIndex
        ChargerGvAffaireProduit()
    End Sub

    Protected Sub gvProduitAffaire_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvProduitAffaire.RowCancelingEdit
        gvProduitAffaire.EditIndex = -1
        ChargerGvAffaireProduit()
    End Sub

    Protected Sub gvProduitAffaire_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvProduitAffaire.RowUpdating
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oProduitAffaire As New CProduitAffaire
        Dim oAffaireDAO As New CAffaireDAO

        Dim iProduitAffaireID As Integer = CInt(gvProduitAffaire.DataKeys(e.RowIndex).Values(0))
        Dim iEmployeID As Integer = CInt(gvProduitAffaire.DataKeys(e.RowIndex).Values(1))
        Dim iTypeProduitID As Integer = CInt(gvProduitAffaire.DataKeys(e.RowIndex).Values(2))
        Dim sDate As String = gvProduitAffaire.DataKeys(e.RowIndex).Values(3).ToString

        Dim sLibelle As String = CType(gvProduitAffaire.Rows(e.RowIndex).FindControl("tbEditProduitAffaireLibelle"), TextBox).Text
        Dim decPrix As Decimal = CDec(CType(gvProduitAffaire.Rows(e.RowIndex).FindControl("tbEditProduitAffaireMntUnitHT"), TextBox).Text.Replace(".", ","))
        Dim decQte As Decimal = CDec(CType(gvProduitAffaire.Rows(e.RowIndex).FindControl("tbEditProduitAffaireQte"), TextBox).Text.Replace(".", ","))

        ' On en enregistre la valeur en TTC pour les produits désignés dans le catalogue en Refac. = TTC
        Dim dsProduitAffaire As DataSet = oProduitAffaireDAO.LoadOne(iProduitAffaireID)
        Dim iProduitIDCourant As Integer = 0
        Dim dTvaMontant As Double = CDbl(gvProduitAffaire.Rows(e.RowIndex).Cells(5).Text)

        If dsProduitAffaire.Tables(0).Rows.Count > 0 Then
            iProduitIDCourant = CInt(dsProduitAffaire.Tables(0).Rows(0)(ProduitAffaire.ProduitID))
        End If

        Dim oProduit As New CProduit
        Dim oProduitDAO As New CProduitDAO

        oProduit = oProduitDAO.GetProduit(iProduitIDCourant)
        If oProduit.ProduitRefac Then
            decPrix = CDec(GetMontantTTC(CDbl(decPrix), dTvaMontant))
        End If

        If oProduitAffaireDAO.EqualOne(iEmployeID, sDate, decQte, iTypeProduitID, True, iProduitAffaireID) Then
            If oProduitAffaireDAO.SiAffaireEnDepassement(CInt(Request.QueryString("id"))) Then
                oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, decPrix * decQte)
                oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
            Else
                Dim decConsoHT As Decimal = oProduitAffaireDAO.GetProduitAffaireAssocieBudget(CInt(Request.QueryString("id")), iProduitAffaireID)
                Dim decNewConsoHT As Decimal = decPrix * decQte
                Dim decConsoTotal As Decimal = decConsoHT + decNewConsoHT
                Dim decDepassement As Decimal = decConsoTotal - oAffaireDAO.GetAffaireBudgetByAffaireID(CInt(Request.QueryString("id")))

                If decDepassement <= 0 Then
                    oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, New Decimal)
                    oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
                Else
                    oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, decDepassement)
                    oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
                End If
            End If

            oAffaireDAO.UpdateSommeProduitsParAffaire(CInt(Request.QueryString("id")))
            oAffaireDAO.UpdateProduitsDepassement(CInt(Request.QueryString("id")))
            lblMsg.Text = ""
        Else
            lblMsg.Text = "Modification échouée : la quantité journalière est supérieure à 1 !"
        End If


        gvProduitAffaire.EditIndex = -1
        ChargerGvAffaireProduit()
    End Sub

    Protected Sub gvProduitAffaire_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvProduitAffaire.RowDeleting
        Dim oProduitAffaireDAO As New CProduitAffaireDAO

        Try
            Dim iProduitAffaireID As Integer = CInt(gvProduitAffaire.DataKeys(e.RowIndex).Values(0))
            oProduitAffaireDAO.SupprimerProduitAffaire(iProduitAffaireID)
        Catch ex As InvalidCastException
            lblMsg.Text = "Ce produit ne peut pas être supprimer!"
        End Try

        gvProduitAffaire.EditIndex = -1
        ChargerGvAffaireProduit()
    End Sub

    Private Sub ibExporter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExporter.Click
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduitAffaire As DataSet

        dsProduitAffaire = oProduitAffaireDAO.GetProduitAffaireExcel(CInt(Request.QueryString("id")), CInt(ddlEmployes.SelectedValue), CInt(ddlTypes.SelectedValue), tbDateDeb.Text, tbDateFin.Text, True)

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim wsDonnes As ExcelWorksheet = ef.Worksheets.Add("ReleveActivite")
        Dim wsRegroupementSite As ExcelWorksheet = ef.Worksheets.Add("ReleveActiviteParSite")

        ' 1er onglet : export des données brutes
        For i As Integer = 0 To dsProduitAffaire.Tables(0).Rows.Count - 1
            For j As Integer = 0 To dsProduitAffaire.Tables(0).Columns.Count - 1
                wsDonnes.Cells(i + 1, j).Value = dsProduitAffaire.Tables(0).Rows(i)(j)
            Next
        Next

        ' Nom des colonnes
        For j As Integer = 0 To dsProduitAffaire.Tables(0).Columns.Count - 1
            wsDonnes.Cells(0, j).Value = dsProduitAffaire.Tables(0).Columns(j).ColumnName

            Select Case LCase(dsProduitAffaire.Tables(0).Columns(j).ColumnName)
                Case "date"
                    wsDonnes.Columns(j).Width = 12 * 256
                Case "libellé"
                    wsDonnes.Columns(j).Style.WrapText = True
                    wsDonnes.Columns(j).Width = 40 * 256
                Case "site"
                    wsDonnes.Columns(j).Style.WrapText = True
                    wsDonnes.Columns(j).Width = 35 * 256
                Case "référence"
                    wsDonnes.Columns(j).Style.WrapText = True
                    wsDonnes.Columns(j).Width = 25 * 256
                Case "donneur d'ordre"
                    wsDonnes.Columns(j).Style.WrapText = True
                    wsDonnes.Columns(j).Width = 25 * 256
                Case "employé"
                    wsDonnes.Columns(j).Style.WrapText = True
                    wsDonnes.Columns(j).Width = 25 * 256
                Case "type de produit"
                    wsDonnes.Columns(j).Style.WrapText = True
                    wsDonnes.Columns(j).Width = 25 * 256
                Case "référence produit"
                    wsDonnes.Columns(j).Style.WrapText = True
                    wsDonnes.Columns(j).Width = 25 * 256
            End Select

        Next

        dsProduitAffaire = oProduitAffaireDAO.GetProduitAffaireExcel(CInt(Request.QueryString("id")), CInt(ddlEmployes.SelectedValue), CInt(ddlTypes.SelectedValue), tbDateDeb.Text, tbDateFin.Text, False)

        ' 2ème onglet : export des données formatées par site puis par type de produit
        Dim sSiteCourant As String = ""
        Dim sTypeProduitCourant As String = ""
        Dim iLigneExcel As Integer = 0

        For k As Integer = 0 To dsProduitAffaire.Tables(0).Rows.Count - 1
            If sSiteCourant <> dsProduitAffaire.Tables(0).Rows(k)("Site").ToString() Then
                ' Regroupement
                wsRegroupementSite.Cells(iLigneExcel, 0).Value = "Site " & dsProduitAffaire.Tables(0).Rows(k)("Site").ToString()
                wsRegroupementSite.Cells(iLigneExcel + 1, 0).Value = dsProduitAffaire.Tables(0).Rows(k)("Type de produit").ToString()

                ' Application des couleurs
                For kk As Integer = 0 To 6
                    wsRegroupementSite.Cells(iLigneExcel, kk).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 192, 0), Drawing.Color.Beige)
                    wsRegroupementSite.Cells(iLigneExcel + 1, kk).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(146, 208, 80), Drawing.Color.Beige)
                Next

                ' Données
                wsRegroupementSite.Cells(iLigneExcel + 2, 0).Value = dsProduitAffaire.Tables(0).Rows(k)("Date").ToString()
                wsRegroupementSite.Cells(iLigneExcel + 2, 1).Value = dsProduitAffaire.Tables(0).Rows(k)("Employé").ToString()
                wsRegroupementSite.Cells(iLigneExcel + 2, 2).Value = dsProduitAffaire.Tables(0).Rows(k)("Libellé").ToString()
                wsRegroupementSite.Cells(iLigneExcel + 2, 3).Value = CDbl(dsProduitAffaire.Tables(0).Rows(k)("Prix HT").ToString())
                wsRegroupementSite.Cells(iLigneExcel + 2, 4).Value = CDbl(dsProduitAffaire.Tables(0).Rows(k)("Quantité").ToString())
                wsRegroupementSite.Cells(iLigneExcel + 2, 5).Value = CDbl(dsProduitAffaire.Tables(0).Rows(k)("Total HT").ToString())
                wsRegroupementSite.Cells(iLigneExcel + 2, 6).Value = dsProduitAffaire.Tables(0).Rows(k)("Donneur d'ordre").ToString()

                iLigneExcel += 2

                sTypeProduitCourant = ""
            Else
                ' Regroupement
                If sTypeProduitCourant <> dsProduitAffaire.Tables(0).Rows(k)("Type de produit").ToString() Then
                    wsRegroupementSite.Cells(iLigneExcel, 0).Value = dsProduitAffaire.Tables(0).Rows(k)("Type de produit").ToString()

                    ' Application des couleurs
                    For kk As Integer = 0 To 6
                        wsRegroupementSite.Cells(iLigneExcel, kk).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(146, 208, 80), Drawing.Color.Beige)
                    Next

                    iLigneExcel += 1
                End If

                ' Données
                wsRegroupementSite.Cells(iLigneExcel, 0).Value = dsProduitAffaire.Tables(0).Rows(k)("Date").ToString()
                wsRegroupementSite.Cells(iLigneExcel, 1).Value = dsProduitAffaire.Tables(0).Rows(k)("Employé").ToString()
                wsRegroupementSite.Cells(iLigneExcel, 2).Value = dsProduitAffaire.Tables(0).Rows(k)("Libellé").ToString()
                wsRegroupementSite.Cells(iLigneExcel, 3).Value = CDbl(dsProduitAffaire.Tables(0).Rows(k)("Prix HT").ToString())
                wsRegroupementSite.Cells(iLigneExcel, 4).Value = CDbl(dsProduitAffaire.Tables(0).Rows(k)("Quantité").ToString())
                wsRegroupementSite.Cells(iLigneExcel, 5).Value = CDbl(dsProduitAffaire.Tables(0).Rows(k)("Total HT").ToString())
                wsRegroupementSite.Cells(iLigneExcel, 6).Value = dsProduitAffaire.Tables(0).Rows(k)("Donneur d'ordre").ToString()

            End If

            sSiteCourant = dsProduitAffaire.Tables(0).Rows(k)("Site").ToString()
            sTypeProduitCourant = dsProduitAffaire.Tables(0).Rows(k)("Type de produit").ToString()
            iLigneExcel += 1
        Next

        ' Bordure des cellules
        For j As Integer = 0 To iLigneExcel - 1
            For k As Integer = 0 To 6
                wsRegroupementSite.Cells(j, k).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            Next
        Next

        ' Intitulé des colonnes
        wsRegroupementSite.Cells(0, 1).Value = "Employé"
        wsRegroupementSite.Cells(0, 2).Value = "Libellé"
        wsRegroupementSite.Cells(0, 3).Value = "Prix HT"
        wsRegroupementSite.Cells(0, 4).Value = "Quantité"
        wsRegroupementSite.Cells(0, 5).Value = "Total HT"
        wsRegroupementSite.Cells(0, 6).Value = "Donneur d'ordre"

        ' Adaptation des largeurs de colonnes au texte
        For i As Integer = 0 To 6
            wsRegroupementSite.Columns(i).AutoFitAdvanced(1)
        Next

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "ProduitAffaire" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)
    End Sub

    Private Sub bNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bNouveau.Click
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim oProduitAffaireAssocie As CProduitAffaireAssocie
        Dim sQuantite As String
        Dim sPrix As String
        Dim oEmploye As New CEmploye
        Dim sPU As String
        Dim oProduit As New CProduit
        Dim oProduitDAO As New CProduitDAO
        Dim oTvaDAO As New CTVADAO
        If (ddlEmployeConcerne.SelectedValue = "-1") Then
            lblErreur.Visible = True
            lblErreur.Text = "Veuillez saisir un employé"
        Else

            Dim iEmployeID As Integer = CInt(ddlEmployeConcerne.SelectedValue)

            sQuantite = Formater.FormatDecimal(tbQuantite.Text)
            sPrix = Formater.FormatDecimal(tbPrixUnit.Text)

            lblQteNulle.Visible = False

            lblPrixNull.Visible = False

            lbQteErreur.Visible = False

            If CDbl(sQuantite) = 0 Then
                lblQteNulle.Visible = True
            Else
                If CDbl(sPrix) = 0 Then
                    lblPrixNull.Visible = True
                Else

                    oProduit = oProduitDAO.GetProduit(CInt(ddlProduit.SelectedValue))
                    If oProduit.ProduitRefac Then
                        sPU = GetMontantTTC(CDbl(tbPrixUnit.Text), oTvaDAO.GetMontantTva(CInt(ddlTVA.SelectedValue))).ToString()
                    Else
                        sPU = Formater.FormatDecimal(tbPrixUnit.Text)
                    End If


                    If Not (oProduitAffaireDAO.EqualOne(iEmployeID, tbDate.Text, CDec(sQuantite), CInt(ddlType.SelectedValue), False)) Then
                        lbQteErreur.Visible = True
                        lblMsg.Visible = False
                    Else
                        lbQteErreur.Visible = False
                        lblMsg.Visible = True

                        ' Produit associé à une affaire

                        ' On vérifie d'abord si l'affaire est déjà en dépassement
                        If oProduitAffaireDAO.SiAffaireEnDepassement(CInt(tbAffaireID.Text)) Then
                            ' L'affaire est en depassement
                            oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(tbAffaireID.Text), CInt(ddlQualification.SelectedValue),
                                        CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                        CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                        oEmploye.EmployeID, CInt(ddlService.SelectedValue),
                                        CDec(sQuantite), CInt(ddlTVA.SelectedValue),
                                        CDate(tbDate.Text),
                                        CDec(sPU), CDec(Formater.FormatDecimal(tbQuantite.Text)) * CDec(Formater.FormatDecimal(tbPrixUnit.Text)), tbLibelle.Text)
                            oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)
                        Else
                            ' L'affaire n'est pas en depassement

                            ' On vérifie d'abord si ce produit lié à une affaire ferait dépasser le budget de l'affaire
                            Dim decConsoHT As Decimal = oProduitAffaireDAO.GetProduitAffaireAssocieBudget(CInt(tbAffaireID.Text))
                            Dim decNewConsoHT As Decimal = CDec(Formater.FormatDecimal(tbQuantite.Text)) * CDec(Formater.FormatDecimal(tbPrixUnit.Text))
                            Dim decConsoTotal As Decimal = decConsoHT + decNewConsoHT
                            Dim decDepassement As Decimal = decConsoTotal - oAffaireDAO.GetAffaireBudgetByAffaireID(CInt(tbAffaireID.Text))

                            If ddlType.SelectedItem.Text = "Frais" Then
                                decConsoTotal = decConsoHT
                            Else
                                decConsoTotal = decConsoHT + decNewConsoHT
                            End If

                            If decDepassement <= 0 Then
                                ' Le produit ne fait pas dépasser le budget
                                oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(tbAffaireID.Text), CInt(ddlQualification.SelectedValue),
                                                                                    CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                                                                    CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                                                                    iEmployeID, CInt(ddlService.SelectedValue),
                                                                                    CDec(sQuantite), CInt(ddlTVA.SelectedValue),
                                                                                    CDate(tbDate.Text),
                                                                                    CDec(sPU), New Decimal, tbLibelle.Text)
                                oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)
                            Else
                                ' Le produit fait dépasser le budget

                                ' On ajoute un produit sans dépassement et un produit avec dépassement
                                Dim decMontantDansBudget As Decimal = decNewConsoHT - decDepassement
                                Dim decJoursDansBudget As Decimal = decMontantDansBudget / decNewConsoHT
                                Dim decMontantHorsBudget As Decimal = decDepassement
                                Dim decJoursHorsBudget As Decimal = CDec(sQuantite) - decJoursDansBudget

                                ' 1) Produit sans dépassement
                                If decJoursDansBudget > 0 Then
                                    oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(tbAffaireID.Text), CInt(ddlQualification.SelectedValue),
                                                                CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                                                CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                                                iEmployeID, CInt(ddlService.SelectedValue),
                                                                CDec(decJoursDansBudget), CInt(ddlTVA.SelectedValue),
                                                                CDate(tbDate.Text),
                                                                CDec(sPU), New Decimal, tbLibelle.Text)
                                    oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)
                                End If

                                ' 2) Produit avec dépassement
                                oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(tbAffaireID.Text), CInt(ddlQualification.SelectedValue),
                                                                CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                                                CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                                                iEmployeID, CInt(ddlService.SelectedValue),
                                                                CDec(decJoursHorsBudget), CInt(ddlTVA.SelectedValue),
                                                                CDate(tbDate.Text),
                                                                CDec(sPU), decMontantHorsBudget, tbLibelle.Text)
                                oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)
                            End If


                        End If
                        gvProduitAffaire.EditIndex = -1


                        oAffaireDAO.UpdateSommeProduitsParAffaire(CInt(tbAffaireID.Text))
                        oAffaireDAO.UpdateProduitsDepassement(CInt(tbAffaireID.Text))

                        lblMsg.ForeColor = Color.Blue
                        lblMsg.Text = vbLf & "Enregistrement réussi : vous venez de rentrer " & tbQuantite.Text & " unité(s) de " & ddlProduit.SelectedItem.Text & " pour le " & tbDate.Text & "."

                    End If

                End If
            End If

            pPopupAjoutProduit.Visible = False
            gvProduitAffaire.Visible = True
            chargerListeAffaireProduit()
        End If
    End Sub

    Private Sub btnQuitter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitter.Click
        pPopupAjoutProduit.Visible = False
        gvProduitAffaire.Visible = True
        chargerListeAffaireProduit()
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