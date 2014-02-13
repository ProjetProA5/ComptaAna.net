Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class AffaireModifier
    Inherits System.Web.UI.Page

    Dim iAffaireID As Integer

    ''' <summary>
    ''' chargement de la page, chargement des informations concernant l'affaire
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oAffaireDAO As New CAffaireDAO
        Dim oAffaire As CAffaire
        iAffaireID = CInt(Request.QueryString("id"))

        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim iTypeAffaire As Integer = oAffaireDAO.GetAffaireTypeByAffaireID(iAffaireID)

        If Not Page.IsPostBack Then

            'gestion des droits

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesFacture) Then
                    mMenuAffaireModif.FindItem("2").Enabled = False
                End If
                If Not verifDroit(lDroit, eModule.AccesAffaireLecture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
                If Not verifDroit(lDroit, eModule.AccesAffaireEcriture) Then
                    btnValider.Visible = False
                    btnAjouter.Visible = False
                    btnEnregistrerEtape.Visible = False
                    gvEtapesFactuPourcentage.Enabled = False
                    gvEtapesFactuPourcentage.Columns(3).Visible = False
                    tblMois.Enabled = False
                    btnEnregistrerEtape.Visible = False
                    gvQualifAffaire.Columns(4).Visible = False
                    gvQualifAffaire.Columns(5).Visible = False
                    gvQualifAffaire.Enabled = False
                    lblNonModif.Visible = True
                    lblMsgQualif.Visible = False
                    tableAjoutQualif.Visible = False
                    tbBudget.Enabled = False
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
          
            If (CConfiguration.NouvelleVersion) Then
                tblMois.Enabled = False
            End If

            ' chargement de l'affaire
            oAffaire = oAffaireDAO.GetAffaire(iAffaireID)
            ' remplissage des information générales
            lblAffaireID.Text = CStr(oAffaire.AffaireID)
            tbClient.Text = oAffaire.Client.ClientNom
            tbLibelle.Text = oAffaire.AffaireLibelle
            tbDate.Text = CStr(oAffaire.AffaireDateDeb)
            tbCom.Text = oAffaire.AffaireRemarques
            cbTypeAffaire.SelectedValue = CStr(oAffaire.TypeAffaire.TypeAffaireID)
            cbChargeAffaire.SelectedValue = CStr(oAffaire.Employe.EmployeID)
            cbService.SelectedValue = CStr(oAffaire.Service.ServiceID)

            ' si l'affaire est terminee, on ne peut plus ajouter de qualifications
            cbTerminee.Checked = CBool(oAffaire.AffaireTermine)
            If cbTerminee.Checked Then
                btnAjouter.Visible = False
                listeQualif.Visible = False
                tbPrixHT.Visible = False
                tbNbJours.Visible = False
            End If

            ' Si l'affaire possède au moins une sous-affaire, le budget de cette affaire
            ' est égale à la somme des budgets des sous-affaires
            If oAffaireDAO.GetListeSousAffaire(iAffaireID).Tables(0).Rows.Count > 0 Then
                tbBudget.Text = oAffaireDAO.GetBudgetTotSousAffaire(iAffaireID).ToString
                tbBudget.Enabled = False
            Else
                tbBudget.Text = CStr(oAffaire.AffaireBudget)
            End If

            ' remplissage des informations budgetaires
            Dim oProduitAffaireDAO As New CProduitAffaireDAO
            tbHTconso.Text = CStr(FormatNumber(oProduitAffaireDAO.GetProduitAffaireAssocieBudget(iAffaireID), 2))
            tbFraisConso.Text = CStr(FormatNumber(oProduitAffaireDAO.GetProduitAffaireFrais(iAffaireID), 2))
            tbPrestRest.Text = CStr(FormatNumber(CSng(tbBudget.Text) - CSng(tbHTconso.Text), 2))
            tbNbJoursPasses.Text = CStr(FormatNumber(oProduitAffaireDAO.GetProduitAffaireNbJours(iAffaireID), 3))
            Try
                tbCtMoyen.Text = CStr(FormatNumber(CDbl(tbHTconso.Text) / CDec(tbNbJoursPasses.Text), 3))
            Catch ex As DivideByZeroException
                tbCtMoyen.Text = "0,00"
            End Try
            ' chargement des listes
            chargerListeTypeAffaire()
            chargerChargeAffaire()
            chargerService()
            chargerListeQualif()

            If (iTypeAffaire = eTypeAffaire.ContratCadre Or iTypeAffaire = eTypeAffaire.Regie Or iTypeAffaire = eTypeAffaire.Recurrent) Then
                'affichage du mois
                gvEtapesFactuPourcentage.Visible = False
                For i As Integer = 1 To 12
                    CType(tblMois.FindControl("cbMois" & i), CheckBox).Checked = oAffaireEtapeFactureDAO.IsAffaireFactureMois(iAffaireID, i)
                Next
                tblMois.Visible = True
            End If

        Else

        End If

        chargerListeAffaireQualif()
        
        If (iTypeAffaire = eTypeAffaire.Forfait) Then
            'affichage du taux d'avancement
            chargerListeEtapeFacturePourcentage()
            
            gvEtapesFactuPourcentage.Visible = True
            tblMois.Visible = False
        End If

        'If oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
        '    mMenuAffaireModif.Visible = False
        'End If
        mMenuAffaireModif.Items(1).Selected = True
    End Sub

    ''' <summary>
    ''' chargement de la liste des types d'affaire
    ''' </summary>
    Protected Sub chargerListeTypeAffaire()
        Dim oTypeAffaireDAO As New CTypeAffaireDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim ds As DataSet

        If oAffaireDAO.EstSousAffaire(CInt(Request.QueryString("id"))) Then
            ds = oTypeAffaireDAO.LoadAll("TypeAffaireID=4")
        Else
            ds = oTypeAffaireDAO.LoadAll("TypeAffaireID<>4")
        End If

        cbTypeAffaire.DataSource = ds
        cbTypeAffaire.DataBind()
    End Sub

    ''' <summary>
    ''' chargement de la liste des services
    ''' </summary>
    Protected Sub chargerService()
        Dim oService As New CServiceDAO
        Dim ds As DataSet

        ds = oService.GetAllService

        cbService.DataSource = ds
        cbService.DataBind()

    End Sub

    ''' <summary>
    ''' chargement de la liste des employes
    ''' </summary>
    Protected Sub chargerChargeAffaire()
        Dim oEmploye As New CEmployeDAO
        Dim ds As DataSet

        ds = oEmploye.GetNomPrenomEmployeActif


        Dim oAffaireDAO As New CAffaireDAO
        Dim oAffaire As CAffaire
        iAffaireID = CInt(Request.QueryString("id"))
        ' chargement de l'affaire
        oAffaire = oAffaireDAO.GetAffaire(iAffaireID)

        ' permet de rajouter, à la liste des employés actif, l'employé qui était chargé à l'époque de l'affaire
        If Not oEmploye.isEmployeActif(oAffaire.Employe.EmployeID) Then


            Dim dataRow As DataRow = ds.Tables(0).NewRow()
            ds.Tables(0).Rows.Add(dataRow)

            dataRow("EmployeID") = cbChargeAffaire.SelectedValue
            dataRow("PrenomNom") = oEmploye.GetNomPrenomFromId(oAffaire.Employe.EmployeID)

        End If

        '------------------------------------------------------------------------------------------------------------------------------------------------------------------
        cbChargeAffaire.DataSource = ds
        cbChargeAffaire.DataBind()
    End Sub

    ''' <summary>
    ''' modification de l'affaire dans la base de données lors du click sur le bouton Valider
    ''' </summary>
    Private Sub btnValider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValider.Click

        lblErreurInsertion.Visible = False

        ' controle pour éviter les erreurs de conversion dû à des champs null ou vide
        Dim sBudget As String
        If (String.IsNullOrWhiteSpace(tbBudget.Text) Or String.IsNullOrEmpty(tbBudget.Text)) Then
            sBudget = "0"
        Else
            sBudget = tbBudget.Text
            sBudget = Replace(sBudget, ".", ",").ToString
        End If

        ' controle des champs obligatoires
        If (String.IsNullOrEmpty(tbLibelle.Text) _
            Or String.IsNullOrEmpty(tbDate.Text) _
            Or String.IsNullOrEmpty(cbChargeAffaire.SelectedValue) _
            Or String.IsNullOrEmpty(cbTypeAffaire.SelectedValue) _
            Or String.IsNullOrEmpty(cbService.SelectedValue)) Then
            lblErreurInsertion.Visible = True
            lblErreurInsertion.ForeColor = Drawing.Color.Red
            lblErreurInsertion.Text = ("erreur: Tous les champs avec des * doivent être remplis.")

        Else

            Dim oEmploye As New CEmploye
            oEmploye.EmployeID = CInt(cbChargeAffaire.SelectedValue)

            Dim oService As New CService
            oService.ServiceID = CInt(cbService.SelectedValue)

            Dim oTypeAffaire As New CTypeAffaire
            oTypeAffaire.TypeAffaireID = CInt(cbTypeAffaire.SelectedValue)

            ' creation d'un objet de type Affaire pour la mise a jour
            Dim oAffaire As New CAffaire
            oAffaire.AffaireID = CInt(lblAffaireID.Text)
            oAffaire.Employe = oEmploye
            oAffaire.Service = oService
            oAffaire.TypeAffaire = oTypeAffaire
            oAffaire.AffaireLibelle = tbLibelle.Text
            oAffaire.AffaireBudget = Convert.ToDecimal(sBudget)
            oAffaire.AffaireDateDeb = CDate(tbDate.Text)
            oAffaire.AffaireTermine = cbTerminee.Checked
            oAffaire.AffaireRemarques = tbCom.Text
            
            Dim oAffaireDAO As New CAffaireDAO
            oAffaireDAO.UpdateAffaire(oAffaire)
            'tbPrestRest.Text = CStr(CDbl(tbBudget.Text) - CDbl(tbHTconso.Text))


            ' Mise à jour de la somme des produits dans la colonne SommeProduits
            oAffaireDAO.UpdateSommeProduitsParAffaire(oAffaire.AffaireID)

            ' Mise à jour de tous les produits associés au niveau du dépassement
            oAffaireDAO.UpdateProduitsDepassement(oAffaire.AffaireID)

            ' Mise à jour qualif
            chargerListeAffaireQualif()

            ' Mise à jour étape factu
            Dim sType As String = cbTypeAffaire.SelectedText
            If (sType = "Forfait") Then
                'affichage du taux d'avancement
                chargerListeEtapeFacturePourcentage()
                gvEtapesFactuPourcentage.Visible = True
                tblMois.Visible = False
            ElseIf (sType = "Contrat Cadre" Or sType = "Regie" Or sType = "recurrent ") Then
                'affichage du mois
                gvEtapesFactuPourcentage.Visible = False

                Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
                For i As Integer = 1 To 12
                    CType(tblMois.FindControl("cbMois" & i), CheckBox).Checked = oAffaireEtapeFactureDAO.IsAffaireFactureMois(iAffaireID, i)
                Next

                tblMois.Visible = True
            End If

        End If

        ' on controle si l'affaire est terminee ou pas pour permettre l'ajout de qualifications ou non
        If cbTerminee.Checked Then
            btnAjouter.Visible = False
            listeQualif.Visible = False
            tbPrixHT.Visible = False
            tbNbJours.Visible = False
        Else
            btnAjouter.Visible = True
            listeQualif.Visible = True
            tbPrixHT.Visible = True
            tbNbJours.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' modification d'une ligne du gridview
    ''' mise a jour des qualification dans la base de données
    ''' </summary>
    Protected Sub gvQualifAffaire_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim oAffaireDAO As New CAffaireDAO
        Dim oPosteDAO As New CPosteDAO
        Dim oAffairePoste As New CAffairePoste
        Dim oAffaireQualificationDAO As New CAffaireQualificationDAO
        Dim oAffaireQualification As New CAffaireQualification

        If e.CommandName = "EnregistrerAffaireQualif" Then
            If oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
                oAffairePoste.AffairePosteID = CInt((CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lblAffaireQualificationID"), Label).Text))
                oAffairePoste.AffaireID = iAffaireID
                oAffairePoste.PosteID = CInt(CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("ddlQualif"), DropDownList).SelectedValue)
                oAffairePoste.QualifNBJours = CDec(CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("tbNbreJours"), TextBox).Text)
                oAffairePoste.QualifMntUnitHT = CDec(CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("tbPrixHT"), TextBox).Text)

                oPosteDAO.UpdateAffairePoste(oAffairePoste)
            Else
                oAffaireQualification.Qualification.QualificationId = CInt(CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("ddlQualif"), DropDownList).SelectedValue)
                oAffaireQualification.QualifNbJours = CDec(CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("tbNbreJours"), TextBox).Text)
                oAffaireQualification.QualifMntUnitHT = CDec(CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("tbPrixHT"), TextBox).Text)
                oAffaireQualification.Affaire.AffaireID = iAffaireID
                oAffaireQualification.AffaireQualificationID = CInt((CType(gvQualifAffaire.Rows(Convert.ToInt32(e.CommandArgument)).FindControl("lblAffaireQualificationID"), Label).Text))

                oAffaireQualificationDAO.UpdateAffaireQualification(oAffaireQualification)
            End If

            chargerListeAffaireQualif()
            lblMsgQualif.ForeColor = Drawing.Color.Red
            lblMsgQualif.Text = "Modification effectuée!"
        End If
    End Sub

    ''' <summary>
    ''' chargement de la liste des qualifications dans le gridview
    ''' </summary>
    Protected Sub chargerListeAffaireQualif()
        Dim oAffaireQualificationDAO As New CAffaireQualificationDAO
        Dim oPosteDAO As New CPosteDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim ds As DataSet

        If oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
            ds = oPosteDAO.GetAffairePoste(iAffaireID)
        Else
            ds = oAffaireQualificationDAO.GetAffaireQualification(iAffaireID)
        End If

        gvQualifAffaire.DataSource = ds
        gvQualifAffaire.DataBind()

        ' calcul de la somme des totaux
        Dim dTotal As Decimal = 0
        Dim iCount As Integer = gvQualifAffaire.Rows.Count
        For i = 0 To iCount - 1
            dTotal = dTotal + CDec(CType(gvQualifAffaire.Rows(i).Cells(4).FindControl("tbPrixTotal"), TextBox).Text)
        Next
        tbTotal.Text = dTotal.ToString

        If Not dTotal = 0 Then
            divTotal.Visible = True
        Else : divTotal.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' affiche la liste déroulante des qualifications dans le gridview
    ''' </summary>
    Protected Sub gvQualifAffaire_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvQualifAffaire.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim DropDownList = e.Row.FindControl("ddlQualif")
            Dim idRow = e.Row.RowIndex
            Dim ddlQualif As New DropDownList

            ddlQualif = CType(DropDownList, DropDownList)

            Dim oAffaireDAO As New CAffaireDAO
            Dim oPosteDAO As New CPosteDAO
            Dim oQualificationDAO As New CQualificationDAO
            Dim dsQualif As DataSet

            If oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
                dsQualif = oPosteDAO.GetAllPoste()
            Else
                dsQualif = oQualificationDAO.GetAllQualification()
            End If

            Dim selectedQualification As Integer
            selectedQualification = (CInt(CType(e.Row.FindControl("lblQualificationID"), Label).Text))

            ddlQualif.DataSource = dsQualif
            ddlQualif.DataTextField = "Libelle"
            ddlQualif.DataValueField = "ID"
            ddlQualif.SelectedIndex = selectedQualification - 2
            ddlQualif.DataBind()
        End If

    End Sub

    ''' <summary>
    ''' affiche la liste des qualifications sous le gridview pour pouvoir en ajouter
    ''' </summary>
    Protected Sub chargerListeQualif()

        Dim oAffaireDAO As New CAffaireDAO
        Dim ds As DataSet

        ' recupere la listes des qualifications
        If oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
            ds = oAffaireDAO.GetListePoste(iAffaireID)
        Else
            ds = oAffaireDAO.GetListeQualif(iAffaireID)
        End If

        listeQualif.DataSource = ds
        listeQualif.DataBind()

        If Not oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
            ' pré rempli le prix HT en fonction de la qualification selectionnee et le type de l'affaire
            Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
            Dim oPrixQualifTypeAffaire As New CPrixQualifTypeAffaire

            ' s'il n'y a plus de qualification non ajoutée
            If Not (listeQualif.SelectedValue = "") Then
                oPrixQualifTypeAffaire = oPrixQualifTypeAffaireDAO.GetPrixHTQualifTypeAffaire(CInt(listeQualif.SelectedValue), CInt(cbTypeAffaire.SelectedValue))
                tbPrixHT.Text = CStr(oPrixQualifTypeAffaire.PrixHT)
            End If
        End If

    End Sub

    ''' <summary>
    ''' ajout de la nouvelle qualification lors du click sur le bouton Ajouter
    ''' </summary>
    Protected Sub btnAjouter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAjouter.Click
            If Not (boolMontantSupBudget()) And Not (CDbl(tbNbJours.Text.Replace(".", ",")) = 0) And Not (CDbl(tbPrixHT.Text.Replace(".", ",")) = 0) Then
                'on verifie que les champs obligatoires soient bien renseignes
                If String.IsNullOrWhiteSpace(tbPrixHT.Text) Then
                    lblerror.Visible = True
                    lblerror.ForeColor = Drawing.Color.Red
                    lblerror.Text = ("erreur: Le prix HT doit être rempli.")
                Else
                    ' insertion de la qualification
                    Dim oAffaireDAO As New CAffaireDAO
                    Dim oPosteDAO As New CPosteDAO
                    Dim oAffaireQualificationDAO As New CAffaireQualificationDAO
                    Dim sNBjour As String
                    If tbNbJours.Text = "" Then
                        sNBjour = "0"
                    Else
                        sNBjour = tbNbJours.Text
                    End If

                    If oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
                        oPosteDAO.InsertAffairePoste(CInt(lblAffaireID.Text), CInt(listeQualif.SelectedValue), tbPrixHT.Text, sNBjour)
                    Else
                        oAffaireQualificationDAO.InsertAffaireQualif(CInt(lblAffaireID.Text), CInt(listeQualif.SelectedValue), tbPrixHT.Text, sNBjour)
                    End If

                    chargerListeAffaireQualif()
                    chargerListeQualif()
                End If

                lblerror.Visible = False
                tbNbJours.Text = ""
            ElseIf CInt(tbPrixHT.Text) = 0 Then
                lblerror.Text = "Prix HT null"
                lblerror.Visible = True
            ElseIf CInt(tbNbJours.Text) = 0 Then
                lblerror.Text = "Nombre de jours null"
                lblerror.Visible = True
            ElseIf boolMontantSupBudget() Then
                lblerror.Text = "Le total dépasse le budget de l'affaire"
                lblerror.Visible = True
                'lblMontantSupBudget.Visible = True
            End If

    End Sub
    Protected Function boolMontantSupBudget() As Boolean
        Return (CDbl(tbPrixHT.Text.Replace(".", ",")) * CDbl(tbNbJours.Text.Replace(".", ",")) + CDbl(tbTotal.Text.Replace(".", ",")) >= CDbl(tbBudget.Text.Replace(".", ",")))
    End Function
    ''' <summary>
    ''' prérempli le prix HT en fonction de la qualification et du type d'affaire
    ''' </summary>
    Protected Sub listeQualif_changed(ByVal sender As Object, ByVal e As EventArgs) Handles listeQualif.SelectedIndexChanged
        Dim oAffaireDAO As New CAffaireDAO
        If Not oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
            Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
            Dim oPrixQualifTypeAffaire As New CPrixQualifTypeAffaire

            oPrixQualifTypeAffaire = oPrixQualifTypeAffaireDAO.GetPrixHTQualifTypeAffaire(CInt(listeQualif.SelectedValue), CInt(cbTypeAffaire.SelectedValue))

            tbPrixHT.Text = CStr(oPrixQualifTypeAffaire.PrixHT)
        End If
    End Sub

    ''' <summary>
    ''' supression d'une qualification
    ''' </summary>
    Private Sub gvQualifAffaire_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvQualifAffaire.RowDeleting
        Dim oAffaireDAO As New CAffaireDAO
        Dim oAffaireQualificationDAO As New CAffaireQualificationDAO
        Dim oPosteDAO As New CPosteDAO

        'id de la facture de la ligne
        Dim iQualificationID As Integer = CType(CType(gvQualifAffaire.Rows(e.RowIndex).Cells(4).FindControl("lblAffaireQualificationID"), Label).Text, Integer)

        'suppression de la qualification
        If oAffaireDAO.VerifSousAffaire(CInt(Request.QueryString("id"))) Then
            oPosteDAO.DeleteAffairePoste(iQualificationID)
        Else
            oAffaireQualificationDAO.DeleteAffaireQualification(iQualificationID)
        End If


        'chargement du gridview avec la nouvelle liste des qualifications pour l'affaire
        chargerListeAffaireQualif()

        'chargement de la nouvelle liste de qualifications disponibles pour l'ajout
        chargerListeQualif()

    End Sub

    ''' <summary>
    ''' chargement des etapes de facturation pourcentage
    ''' </summary>
    Private Sub chargerListeEtapeFacturePourcentage()
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim dsEtape As DataSet

        dsEtape = oAffaireEtapeFactureDAO.GetAffaireEtapeFacturePourcentage(iAffaireID)

        gvEtapesFactuPourcentage.DataSource = dsEtape
        gvEtapesFactuPourcentage.DataBind()
    End Sub

    ''' <summary>
    ''' rechargement d'etape de factu lorsqu'on choisit un type affaire
    ''' </summary>
    Private Sub cbTypeAffaire_changed(ByVal sender As Object, ByVal e As EventArgs) Handles cbTypeAffaire.SelectedIndexChanged
        If (cbTypeAffaire.SelectedText = "Forfait") Then
            'affichage du taux d'avancement
            gvEtapesFactuPourcentage.Visible = True
            tblMois.Visible = False
        ElseIf (cbTypeAffaire.SelectedText = "Contrat Cadre" Or cbTypeAffaire.SelectedText = "Regie" Or cbTypeAffaire.SelectedText = "recurrent ") Then
            'affichage du mois
            gvEtapesFactuPourcentage.Visible = False
            tblMois.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' redirection vers la page AffaireLister lors du click sur le bouton Retour
    ''' </summary>
    Protected Sub btnRetourListe_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetourListe.Click
        Response.Redirect("AffaireLister.aspx?affaire=" & Request.QueryString("id"))
    End Sub



    ''' <summary>
    ''' modification des etapes de facturation dans la base de données lors du click sur le bouton Enregistrer
    ''' </summary>
    Protected Sub btnEnregistrerEtape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnregistrerEtape.Click
        iAffaireID = CInt(Request.QueryString("id"))

        Dim oAffaireDAO As New CAffaireDAO
        Dim iTypeAffaire As Integer = oAffaireDAO.GetAffaireTypeByAffaireID(iAffaireID)

        Dim oAffaireEtapeFacturationDAO As New CAffaireEtapeFactureDAO
        Dim dsMoisValide As DataSet = oAffaireEtapeFacturationDAO.GetMoisValidation(CInt(Request.QueryString("id")))
        Dim iNbRowMois As Integer = dsMoisValide.Tables(0).Rows.Count

        If (iTypeAffaire = eTypeAffaire.ContratCadre) Or (iTypeAffaire = eTypeAffaire.Regie) Then
            For i As Integer = 1 To 12
                oAffaireEtapeFacturationDAO.UpdateEtapeFactureMois(iAffaireID, i, CType(tblMois.FindControl("cbMois" & i), CheckBox).Checked)
            Next

        ElseIf (iTypeAffaire = eTypeAffaire.Forfait) Then

            If verifTaux() Then
                ErreurPourcentage.Visible = False
                'onClick sur le bouton doit modifier le pourcentage
                Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO

                Try
                    For Each gvRow As GridViewRow In gvEtapesFactuPourcentage.Rows
                        Dim bValide As Boolean = CType(gvRow.Cells(1).FindControl("cbValidation"), CheckBox).Checked
                        Dim iTaux As Integer = CInt(CType(gvRow.Cells(1).FindControl("tbEtapeFacturePourcentage"), TextBox).Text)
                        Dim idEtape As Integer = CInt(CType(gvRow.Cells(1).FindControl("idAffaireEtapeFacture"), Label).Text)

                        oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape, bValide, iTaux)
                    Next
                Catch ex As InvalidCastException
                    'onClick lorsqu'il n'y a rien qui a été changé
                    'On ne fait rien
                End Try
            Else
                ErreurPourcentage.Visible = True

            End If



            End If

    End Sub
    Protected Function verifTaux() As Boolean
        Dim bool As Boolean
        Dim iSum As Integer = 0

        For Each gvRow As GridViewRow In gvEtapesFactuPourcentage.Rows
            Dim iTaux As Integer = CInt(CType(gvRow.Cells(1).FindControl("tbEtapeFacturePourcentage"), TextBox).Text)
            iSum += iTaux
        Next

        bool = (iSum <= 100)

        Return bool
    End Function

    ''' <summary>
    ''' control pour ajouter/supprimer un taux de facturation
    ''' </summary>
    Protected Sub gvEtapesFactuPourcentage_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        iAffaireID = CInt(Request.QueryString("id"))
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO

        If e.CommandName = "AjouterTauxEtapFactu" Then
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            Response.Redirect("AffaireModifier.aspx?id=" & iAffaireID)

        ElseIf e.CommandName = "SupprimerTauxEtapFactu" Then
            oAffaireEtapeFactureDAO.DeleteByAffaireEtapeFactuID(CInt(e.CommandArgument))
            Response.Redirect("AffaireModifier.aspx?id=" & iAffaireID)
        End If
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


 
    Private Sub gvEtapesFactuPourcentage_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEtapesFactuPourcentage.RowDataBound
        If (CConfiguration.NouvelleVersion) Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cbEtape = e.Row.FindControl("cbValidation")
                Dim cbEtape1 As New CheckBox

                cbEtape1 = CType(cbEtape, CheckBox)
                cbEtape1.Enabled = False
            End If
        End If
        
    End Sub
End Class