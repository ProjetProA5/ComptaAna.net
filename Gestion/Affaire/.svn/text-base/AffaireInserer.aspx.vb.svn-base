Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class AffaireInserer
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' chargement de la page, chargement des différentes listes
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesAffaireEcriture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")

                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try

            'chargement des listes
            chargerListeTypeAffaire()
            chargerListeChargeAffaire()
            chargerListeService()
            chargerListeClient()
            lblMntPrevi.Visible = cbFraisInclus.Checked
            tbMntFraisInclus.Visible = cbFraisInclus.Checked
            lblInfoFrais.Visible = cbFraisInclus.Checked
        End If
    End Sub

    ''' <summary>
    ''' chargement de la liste des types d'affaire
    ''' </summary>
    Protected Sub chargerListeTypeAffaire()
        Dim oTypeAffaireDAO As New CTypeAffaireDAO
        Dim ds As DataSet

        If Not IsNothing(Request.QueryString("affaire")) Then
            ' Il s'agit d'une sous-affaire donc on ne charge que le type d'affaire récurrent
            ds = oTypeAffaireDAO.LoadAll("TypeAffaireID=4")

        Else
            ' Il s'agit d'un affaire "classique", donc on charge tous les types d'affaires sauf récurrent
            ds = oTypeAffaireDAO.LoadAll("TypeAffaireID<>4")
        End If

        cbTypeAffaire.DataSource = ds
        cbTypeAffaire.DataBind()
    End Sub

    ''' <summary>
    ''' chargement de la liste des services
    ''' </summary>
    Protected Sub chargerListeService()
        Dim oServiceDAO As New CServiceDAO
        Dim ds As DataSet

        ds = oServiceDAO.GetAllServiceActif

        cbService.DataSource = ds
        cbService.DataBind()

    End Sub

    ''' <summary>
    ''' chargement de la liste des employes
    ''' </summary>
    Protected Sub chargerListeChargeAffaire()
        Dim oEmployeDAO As New CEmployeDAO
        Dim ds As DataSet

        ds = oEmployeDAO.GetNomPrenomEmployeActif

        cbChargeAffaire.DataSource = ds
        cbChargeAffaire.DataBind()

    End Sub

    ''' <summary>
    ''' chargement de la liste des clients
    ''' </summary>
    Protected Sub chargerListeClient()
        Dim oClientDAO As New CClientDAO
        Dim ds As DataSet

        ds = oClientDAO.GetAllClients

        cbClient.DataSource = ds
        cbClient.DataBind()

    End Sub

    ''' <summary>
    ''' insertion de l'affaire dans la base de données lors du click sur le bouton Valider
    ''' </summary>
    Protected Sub btnValider_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnValider.Click
        Dim iAffaireMereID As Integer

        If Not IsNothing(Request.QueryString("affaire")) Then
            iAffaireMereID = CInt(Request.QueryString("affaire"))
        End If

        lblErreurInsertion.Visible = False
        Dim sBudget As String
        Dim sFrais As String = ""

        sBudget = Formater.FormatDecimal(tbBudget.Text)

        If Not tbMntFraisInclus.Text = "" Then
            sFrais = Formater.FormatDecimal(tbMntFraisInclus.Text)
        End If
        Dim dBudget As Decimal
        If cbFraisInclus.Checked Then
            dBudget = CDec(sBudget) - CDec(sFrais)
        Else
            dBudget = CDec(sBudget)
        End If


        ' controle des champs obligatoires
        If (String.IsNullOrEmpty(tbLibelle.Text) _
            Or String.IsNullOrEmpty(tbDate.Text) _
            Or String.IsNullOrEmpty(cbClient.SelectedValue) _
            Or String.IsNullOrEmpty(cbChargeAffaire.SelectedValue) _
            Or String.IsNullOrEmpty(cbTypeAffaire.SelectedValue) _
            Or String.IsNullOrEmpty(cbService.SelectedValue)) Then
            lblErreurInsertion.Visible = True
            lblErreurInsertion.ForeColor = Drawing.Color.Red
            lblErreurInsertion.Text = ("Erreur: Tous les champs avec des * doivent être remplis.")

        ElseIf Not VerifLibelleAffaire(tbLibelle.Text, CInt(cbClient.SelectedValue)) Then
            lblErreurInsertion.Visible = True
            lblErreurInsertion.ForeColor = Drawing.Color.Red
            lblErreurInsertion.Text = "Erreur: Le libellé est déjà utilisé pour une affaire de ce client. Veuillez en choisir un autre."
        Else

            'creation d'un objet de type Affaire pour l'insertion
            Dim oAffaireDAO As New CAffaireDAO

            Dim oClient As New CClient
            oClient.ClientID = CInt(cbClient.SelectedValue)

            Dim oEmploye As New CEmploye
            oEmploye.EmployeID = CInt(cbChargeAffaire.SelectedValue)


            Dim oService As New CService
            oService.ServiceID = CInt(cbService.SelectedValue)

            Dim oTypeAffaire As New CTypeAffaire
            oTypeAffaire.TypeAffaireID = CInt(cbTypeAffaire.SelectedValue)
            Dim iFrais As Integer
            If cbFraisInclus.Checked Then iFrais = 1 Else iFrais = 0

            If IsNothing(Request.QueryString("affaire")) Then
                Dim oAffaire As New CAffaire(oClient, oEmploye, oService, _
                                   oTypeAffaire, tbLibelle.Text, dBudget, _
                                   CDate(tbDate.Text), False, tbCom.Text, 0, iFrais, dBudget)

                oAffaireDAO.InsererAffaire(oAffaire)
            Else
                Dim oAffaire As New CAffaire(oClient, oEmploye, oService, _
                   oTypeAffaire, tbLibelle.Text, dBudget, _
                   CDate(tbDate.Text), False, tbCom.Text, iAffaireMereID, 0, iFrais, dBudget)

                oAffaireDAO.InsererSousAffaire(oAffaire)
            End If

            ' on recupere l'id de l'affaire qu'on vient d'inserer
            Dim oNewAffaire As New CAffaire
            oNewAffaire = oAffaireDAO.GetMaxAffaireID()
            lblIdAff.Text = CStr(oNewAffaire.AffaireID)

            'on ne peut plus changer ce que l'on vient de renseigner mais on peut maintenant ajouter des qualifications
            btnAjouter.Enabled = True
            chargerListeQualif()

            cbClient.Enabled = False
            tbBudget.Enabled = False
            tbLibelle.Enabled = False
            tbDate.Enabled = False
            cbTypeAffaire.Enabled = False
            cbService.Enabled = False
            cbChargeAffaire.Enabled = False
            tbCom.Enabled = False
            btnValider.Enabled = False
            divQualif.Visible = True
            divEtapFactu.Visible = True

            chargerListeAffaireQualif(CInt(lblIdAff.Text))
            chargerChoixEtapeFacturePourcentage(CInt(lblIdAff.Text))
        End If

    End Sub

    ''' <summary>
    ''' chargement du gridview correspondant aux qualifications de l'affaire
    ''' </summary>
    ''' <param name="idAffaire">id de l'affaire concernee</param>
    Protected Sub chargerListeAffaireQualif(ByVal idAffaire As Integer)
        Dim oAffaireQualificationDAO As New CAffaireQualificationDAO
        Dim oPosteDAO As New CPosteDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim ds As DataSet

        If oAffaireDAO.VerifSousAffaire(CInt(lblIdAff.Text)) Then
            ds = oPosteDAO.GetAffairePoste(idAffaire)
        Else
            ds = oAffaireQualificationDAO.GetAffaireQualification(idAffaire)
        End If

        gvQualifAffaire.DataSource = ds
        gvQualifAffaire.DataBind()

        ' calcul de la somme des totaux
        Dim dTotal As Decimal = 0
        Dim iCount As Integer = gvQualifAffaire.Rows.Count
        For i = 0 To iCount - 1
            dTotal = dTotal + CDec(CType(gvQualifAffaire.Rows(i).Cells(3).FindControl("tbPrixTotal"), TextBox).Text)
        Next
        tbTotal.Text = dTotal.ToString

        If Not dTotal = 0 Then
            divTotal.Visible = True
        Else : divTotal.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' chargement de la liste des qualifications
    ''' </summary>
    Protected Sub chargerListeQualif()
        Dim oAffaireDAO As New CAffaireDAO
        Dim ds As DataSet

        ' recupere la listes des qualifications
        If oAffaireDAO.VerifSousAffaire(CInt(lblIdAff.Text)) Then
            ds = oAffaireDAO.GetListePoste(CInt(lblIdAff.Text))
        Else
            ds = oAffaireDAO.GetListeQualif(CInt(lblIdAff.Text))
        End If

        listeQualif.DataSource = ds
        listeQualif.DataBind()

        If Not oAffaireDAO.VerifSousAffaire(CInt(lblIdAff.Text)) Then
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
    Protected Function boolMontantSupBudget() As Boolean
        Return (CDbl(tbPrixHT.Text.Replace(".", ",")) * CDbl(tbNbJours.Text.Replace(".", ",")) + CDbl(tbTotal.Text.Replace(".", ",")) >= CDbl(tbBudget.Text.Replace(".", ",")))
    End Function

    ''' <summary>
    ''' insertion d'une qualification dans la base de données lors du click sur le bouton Ajouter
    ''' </summary>
    Protected Sub btnAjouter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAjouter.Click

        If Not (boolMontantSupBudget()) And Not (CDbl(tbNbJours.Text.Replace(".", ",")) = 0) And Not (CDbl(tbPrixHT.Text.Replace(".", ",")) = 0) Then
            'on verifie que les champs obligatoires soient bien renseignes
            If String.IsNullOrWhiteSpace(tbPrixHT.Text) Or String.IsNullOrWhiteSpace(tbNbJours.Text) Then
                lblerror.Visible = True
                lblerror.ForeColor = Drawing.Color.Red
                lblerror.Text = ("erreur: Le prix HT et le nombre de jours doivent être remplis")
            Else
                Dim oAffaireDAO As New CAffaireDAO
                Dim oPosteDAO As New CPosteDAO
                Dim oAffaireQualificationDAO As New CAffaireQualificationDAO
                Dim sNBjour As String
                If tbNbJours.Text = "" Then
                    sNBjour = "0"
                Else
                    sNBjour = tbNbJours.Text
                End If

                If oAffaireDAO.VerifSousAffaire(CInt(lblIdAff.Text)) Then
                    oPosteDAO.InsertAffairePoste(CInt(lblIdAff.Text), CInt(listeQualif.SelectedValue), tbPrixHT.Text, sNBjour)
                Else
                    oAffaireQualificationDAO.InsertAffaireQualif(CInt(lblIdAff.Text), CInt(listeQualif.SelectedValue), tbPrixHT.Text, sNBjour)
                End If

                chargerListeAffaireQualif(CInt(lblIdAff.Text))
                chargerListeQualif()
                btnRetourFiche.Visible = True

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

    ''' <summary>
    ''' pré rempli le prix HT en fonction de la qualification et du type d'affaire
    ''' </summary>
    Protected Sub listeQualif_changed(ByVal sender As Object, ByVal e As EventArgs) Handles listeQualif.SelectedIndexChanged
        Dim oAffaireDAO As New CAffaireDAO
        If Not oAffaireDAO.VerifSousAffaire(CInt(lblIdAff.Text)) Then
            Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
            Dim oPrixQualifTypeAffaire As New CPrixQualifTypeAffaire

            oPrixQualifTypeAffaire = oPrixQualifTypeAffaireDAO.GetPrixHTQualifTypeAffaire(CInt(listeQualif.SelectedValue), CInt(cbTypeAffaire.SelectedValue))
            tbPrixHT.Text = CStr(oPrixQualifTypeAffaire.PrixHT)
        End If
    End Sub

    ''' <summary>
    ''' redirection vers la page AffaireLister lors du click sur le bouton Retour
    ''' </summary>
    Protected Sub btnRetourListe_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetourListe.Click
        Response.Redirect("AffaireLister.aspx")
    End Sub


    Private Sub btnRetourFiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetourFiche.Click
        Response.Redirect("AffaireModifier.aspx?id=" & CInt(lblIdAff.Text))
    End Sub



    ''' <summary>
    ''' chargement des différents modes de facturation
    ''' </summary>
    ''' <param name="idAffaire">id de l'affaire concernee</param>
    ''' <remarks></remarks>
    Protected Sub chargerChoixEtapeFacturePourcentage(ByVal iAffaireID As Integer)

        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO

        Dim oAffaireDAO As New CAffaireDAO


        'chargement des etapes de facturation

        Dim iTypeAffaire As Integer = oAffaireDAO.GetAffaireTypeByAffaireID(iAffaireID)

        If (iTypeAffaire = eTypeAffaire.Forfait) Then
            'affichage du taux d'avancement
            divChoixFactu.Visible = True
            gvEtapesFactuPourcentageInsertion.Visible = False
            tblMois.Visible = False
        ElseIf iTypeAffaire = eTypeAffaire.ContratCadre Or iTypeAffaire = eTypeAffaire.Regie Or iTypeAffaire = eTypeAffaire.Recurrent Then
            'affichage du mois
            'For i As Integer = 1 To 12
            '    CType(tblMois.FindControl("cbMois" & i), CheckBox).Checked = oAffaireEtapeFactureDAO.IsAffaireFactureMois(idAffaire, i)
            'Next
            'tblMois.Visible = True

            'Ajout en base des mois à facturer

            'Dim idEtape As Integer
            For i As Integer = 1 To 12
                ' oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
                ' idEtape = oAffaireEtapeFactureDAO.GetLastID()
                oAffaireEtapeFactureDAO.UpdateEtapeFactureMois(iAffaireID, i, False)
            Next
            gvEtapesFactuPourcentageInsertion.Visible = False
        End If




    End Sub
    ''' <summary>
    ''' chargement des etapes de facturation pourcentage
    ''' </summary>
    Private Sub chargerListeEtapeFacturePourcentage(ByVal idAffaire As Integer)
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim dsEtape As DataSet

        dsEtape = oAffaireEtapeFactureDAO.GetAffaireEtapeFacturePourcentage(idAffaire)

        gvEtapesFactuPourcentageInsertion.DataSource = dsEtape
        gvEtapesFactuPourcentageInsertion.DataBind()
    End Sub

    ''' <summary>
    ''' Permet l'enregistrement en base des différentes étapes de facturations en fonction du mode choisi
    ''' </summary>
    Private Sub btnSaveEtapeFactu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveEtapeFactu.Click

        Dim iAffaireID As Integer = CInt(lblIdAff.Text)
        Dim oAffaireDAO As New CAffaireDAO
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim idEtape As Integer = oAffaireEtapeFactureDAO.GetLastID()

        If (rbChoixFactu.SelectedItem.Value = "0% - 100%") Then
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape, True, CInt(0))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 1, False, CInt(100))
        ElseIf (rbChoixFactu.SelectedItem.Value = "0% - 50% - 50%") Then
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape, True, CInt(0))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 1, False, CInt(50))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 2, False, CInt(50))
        ElseIf (rbChoixFactu.SelectedItem.Value = "0% - 10% - 30% - 60%") Then
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape, True, CInt(0))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 1, False, CInt(10))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 2, False, CInt(30))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 3, False, CInt(60))
        ElseIf (rbChoixFactu.SelectedItem.Value = "0% - 30% - 70%") Then
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape, True, CInt(0))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 1, False, CInt(30))
            oAffaireEtapeFactureDAO.InsertLigneVideByAffaireID(iAffaireID)
            oAffaireEtapeFactureDAO.UpdateEtapeFactPourcentage(idEtape + 2, False, CInt(70))
        End If

        chargerListeEtapeFacturePourcentage(iAffaireID)
        divChoixFactu.Visible = False
        divEtapFactu.Visible = True
        gvEtapesFactuPourcentageInsertion.Visible = True

    End Sub
    ''' <summary>
    ''' Retourne un booléan disant di le libellé existe déjà.
    ''' Retourne 0 si le dataset n'est pas vide et 1 sinon
    ''' </summary>
    ''' <param name="sLibelle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerifLibelleAffaire(ByVal sLibelle As String, ByVal iClientID As Integer) As Boolean
        Dim oAffaireDAO As New CAffaireDAO
        Dim ds As DataSet = oAffaireDAO.GetAffaireMemeLibelle(sLibelle, iClientID)

        Return ds.Tables(0).Rows.Count = 0
    End Function


    Private Sub cbFraisInclus_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFraisInclus.CheckedChanged
        lblMntPrevi.Visible = cbFraisInclus.Checked
        tbMntFraisInclus.Visible = cbFraisInclus.Checked
        lblInfoFrais.Visible = cbFraisInclus.Checked
        tbCom.Text = ""
    End Sub
End Class
