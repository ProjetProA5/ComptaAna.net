Imports ComptaAna.Business
Imports Obout.ComboBox
Imports System.Drawing
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit
''' <summary>
''' Classe permettant de gerer l'ecoute de la page ReleveActivite.aspx
''' </summary>
''' <remarks></remarks>
Public Class ReleveActivite
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Au chargement de la page
    ''' </summary>
    ''' <remarks>Si l'id = 0, on affiche les champs vide. Si on a selectionne un id dans le treeview et que l'id est initialise, on charge le produitaffaire correspondant</remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If tbDateFin.Text = "" Then

            Dim dat = GetLastDayOfThisMonth()
            tbDateFin.Text = CStr(dat).Substring(0, 10)

        End If

        If Not Page.IsPostBack Then
            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)

            If Not verifDroit(lDroit, eModule.AccesReleveActiviteVisu) Then
                mMenuInsertionVisualision.FindItem("1").Enabled = False
            End If

            'charge tout les liste du relevé d'activité
            chargerListeTypeProduit(True)
            chargerListeService()
            chargerListeTVA()

            chargerListeAffaire()
            chargerListeQualification()

            chargerListeEmployeVisualisation()
        Else
        End If

        ibExporter.ImageUrl = "~/App_Themes/" & Page.Theme & "/Design/Icon_Excel.png"

        lblMsg.Text = ""
        lblConfirmation.Text = ""
    End Sub

    ''' <summary>
    ''' Charge la liste des affaires
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeAffaire()
        Dim oAffaireDAO As CAffaireDAO = New CAffaireDAO
        Dim ds As DataSet

        ds = oAffaireDAO.GetAllAffairetoList()
        'Dans insertion
        ddlAffaire.DataSource = ds
        ddlAffaire.DataBind()

        ddlAffaire.Items.Insert(0, New ListItem("-- Affaire --", "0"))

        'Dans Visualisation

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Toutes les affaires"
        itemDefaut.Value = "default"
        cbbAffaire1.Items.Insert(0, itemDefaut)
        cbbAffaire1.SelectedValue = "default"
        cbbAffaire1.DataSource = ds
        cbbAffaire1.DataBind()

        'Dans Visualisation employé

        Dim itemDefaut2 As New ComboBoxItem
        itemDefaut2.Attributes.Add("style", "color: blue;")
        itemDefaut2.Text = "Toutes les affaires"
        itemDefaut2.Value = "default"
        cbbAffaires2.Items.Insert(0, itemDefaut2)
        cbbAffaires2.SelectedValue = "default"
        cbbAffaires2.DataSource = ds
        cbbAffaires2.DataBind()

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
    '''  charge la liste des services
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeService()
        Dim oServiceDAO As CServiceDAO = New CServiceDAO
        Dim ds As DataSet
        Dim oEmploye = New CEmploye

        oEmploye = CType(Session("Employe"), CEmploye)
        ds = oServiceDAO.GetAllServiceToList

        ddlService.DataSource = ds
        ddlService.DataBind()

        'Auto Select le service de la personne connecter
        ddlService.SelectedValue = CStr(oEmploye.ServiceID)

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

        If oProduit.ProduitID = 13 Then
            'Formation suivie
            lblNbHeures.Visible = True
            tbNbHeures.Visible = True
            lblOrganisme.Visible = True
            tbOrga.Visible = True
            lblCout.Visible = True
            tbCout.Visible = True
            lblNbParticipants.Visible = False
            tbNbParticipants.Visible = False
            lblLibelle.Text = "Libellé * :"
            rbIntExt.Visible = True
        ElseIf oProduit.ProduitID = 47 Then
            'Formation dispensée
            lblNbHeures.Visible = True
            tbNbHeures.Visible = True
            lblOrganisme.Visible = False
            tbOrga.Visible = False
            lblCout.Visible = False
            tbCout.Visible = False
            lblNbParticipants.Visible = True
            tbNbParticipants.Visible = True
            lblLibelle.Text = "Libellé * :"
            rbIntExt.Visible = False
        Else
            lblNbHeures.Visible = False
            tbNbHeures.Visible = False
            lblOrganisme.Visible = False
            tbOrga.Visible = False
            lblCout.Visible = False
            tbCout.Visible = False
            lblNbParticipants.Visible = False
            tbNbParticipants.Visible = False
            lblLibelle.Text = "Libellé:"
            rbIntExt.Visible = False
        End If
        ' On ajoute une astérisque avec une info-bulle pour avertir l'utilisateur que le produit en question
        ' sera facturé en TTC
        If oProduit.ProduitRefac Then
            lblWarningTTC.Visible = True
        Else
            lblWarningTTC.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Switch entre le relevé Non associé et associé à une affaire
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tblReleve_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblReleve.SelectedIndexChanged

        If (tblReleve.SelectedIndex = 1) Then
            'Non associé
            chargerListeTypeProduit(False)
            chargerListeService()
            rfvAffaire.Enabled = False
            rfvSite.Enabled = False
            rfvPU.Enabled = False

            tbDate.Text = tbDateDeb.Text

            tbLibelle.Text = ""

            lblAffaire.Visible = False
            lblSite.Visible = False
            lblQualification.Visible = False

            ddlAffaire.Visible = False
            ddlSite.Visible = False
            ddlQualification.Visible = False

            lblDonneurOrdre.Visible = False
            tbDonneurOrdre.Visible = False
            tdDonneurOrdreLabel.Visible = False
            tdDonneurOrdreTextBox.Visible = False


            ' On met à 0 la quantité et le PU, et on grise le PU
            tbQuantite.Text = "0"
            tbPrixUnit.Text = "0"
            tbPrixUnit.Enabled = False

            ddlProduit.Items.Clear()
            ddlProduit.Items.Add(New ListItem("-- Produit -- ", "default"))

            ddlSite.Items.Clear()
            ddlSite.Items.Add(New ListItem("-- Site -- ", "default"))

            tbLibelle.Text = ""
            tbDonneurOrdre.Text = ""

            cCalendrier.SelectedDate = New Date(0)
        Else
            'Associé
            rbIntExt.Visible = False
            chargerListeTypeProduit(True)
            chargerListeService()
            rfvAffaire.Enabled = True
            rfvSite.Enabled = True
            rfvPU.Enabled = True

            lblAffaire.Visible = True
            lblSite.Visible = True
            lblQualification.Visible = True

            ddlAffaire.Visible = True
            ddlSite.Visible = True
            ddlQualification.Visible = True

            lblDonneurOrdre.Visible = True
            tbDonneurOrdre.Visible = True
            tdDonneurOrdreLabel.Visible = True
            tdDonneurOrdreTextBox.Visible = True

            ' On met à 0 la quantité et le PU, et on réactive le PU
            tbQuantite.Text = "0"
            tbPrixUnit.Text = "0"
            tbPrixUnit.Enabled = True

            ddlProduit.Items.Clear()
            ddlProduit.Items.Add(New ListItem("-- Produit -- ", "default"))

            tbLibelle.Text = ""
            chargerListeAffaire()

            cCalendrier.SelectedDate = New Date(0)
        End If
    End Sub

    ''' <summary>
    ''' Insere un produit dans son relevé d'activité 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub bNouveau_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bNouveau.Click
        'Associé à une affaire
        lblMsg.Text = ""
        If tblReleve.SelectedItem.Value = "Associé à une affaire" Then
            'Si on saisit des frais: pas de dépassement
            If ddlType.SelectedItem.Text = "Frais" Then
                CreationProduit()
            Else
                'Sinon possibilité de dépassement
                If ProduitEnDepassement() Then
                    pDepassement.Visible = True
                Else
                    CreationProduit()
                End If
            End If

        Else
            
                CreationProduit()
            End If

    End Sub

    Private Function CreationProduit() As Integer
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim oProduitAffaire As CProduitAffaire
        Dim oProduitAffaireAssocie As CProduitAffaireAssocie
        Dim sQuantite As String
        Dim sPrix As String
        Dim oEmploye As New CEmploye
        Dim sPU As String
        Dim oProduit As New CProduit
        Dim oProduitDAO As New CProduitDAO
        Dim oTvaDAO As New CTVADAO
        oEmploye = CType(Session("Employe"), CEmploye)
        Dim iRes As Integer = 0

        sQuantite = Formater.FormatDecimal(tbQuantite.Text)
        sPrix = Formater.FormatDecimal(tbPrixUnit.Text)

        lblQteNulle.Visible = False

        lblPrixNull.Visible = False

        lbQteErreur.Visible = False

        If CDbl(sQuantite) = 0 Then
            lblQteNulle.Visible = True
            lblMsg.Visible = False
        Else

            oProduit = oProduitDAO.GetProduit(CInt(ddlProduit.SelectedValue))
            If oProduit.ProduitRefac Then
                sPU = GetMontantTTC(CDbl(tbPrixUnit.Text), oTvaDAO.GetMontantTva(CInt(ddlTVA.SelectedValue))).ToString()
            Else
                sPU = Formater.FormatDecimal(tbPrixUnit.Text)
            End If


            If Not (oProduitAffaireDAO.EqualOne(oEmploye.EmployeID, tbDate.Text, CDec(sQuantite), CInt(ddlType.SelectedValue), False)) Then
                lbQteErreur.Visible = True
                lblMsg.Visible = False
            Else
                '----------------------------------------------------------------------------------------------------------------------
                'Non associé à une affaire: Pas de dépassement
                'Formation
                If ddlProduit.SelectedValue = "13" Then
                    'Formation suivie
                    If rbIntExt.SelectedIndex = 1 And tbCout.Text = "" Then
                        lblMsg.Visible = True
                        lblMsg.ForeColor = Color.Red
                        lblMsg.Text = "Erreur: Tous les champs avec une * doivent être remplis"
                    ElseIf rbIntExt.SelectedIndex = 0 Then

                        If Not tbLibelle.Text = "" And Not tbNbHeures.Text = "" And Not tbOrga.Text = "" Then

                            'Dim oProduitDAO As New CProduitDAO
                            Dim iLastID As Integer = oProduitDAO.GetLastID()
                            Dim oFormation As CFormation
                            Dim oFormationDAO As New CFormationDAO
                            Dim slibelle As String = tbLibelle.Text

                            Dim dDate As Date = CDate(tbDate.Text)
                            ' Dim oEmploye As CEmploye = CType(Session("Employe"), CEmploye)
                            Dim iEmployeID As Integer = oEmploye.EmployeID
                            Dim iType As Integer
                            Dim dNbHeure As Decimal
                            dNbHeure = CDec(tbNbHeures.Text)
                            Dim sOrganisme As String
                            sOrganisme = tbOrga.Text
                            iType = 0
                            Dim dCout As Double
                            If tbCout.Text = "" Then
                                dCout = 0
                            Else
                                dCout = CDbl(tbCout.Text)
                            End If

                            oFormation = New CFormation(slibelle, iLastID, iEmployeID, dNbHeure, iType, sOrganisme, 0, dCout, dDate)
                            Dim iInsert As Integer = oFormationDAO.InsertFormationAssocie(oFormation)
                            
                        Else
                            lblMsg.Visible = True
                            lblMsg.ForeColor = Color.Red
                            lblMsg.Text = "Erreur: Tous les champs avec une * doivent être remplis"
                        End If
                    ElseIf rbIntExt.SelectedIndex = 1 And Not tbCout.Text = "" Then
                        If Not tbLibelle.Text = "" And Not tbNbHeures.Text = "" And Not tbOrga.Text = "" Then

                            ' Dim oProduitDAO As New CProduitDAO
                            Dim iLastID As Integer = oProduitDAO.GetLastID()
                            Dim oFormation As CFormation
                            Dim oFormationDAO As New CFormationDAO
                            Dim slibelle As String = tbLibelle.Text

                            Dim dDate As Date = CDate(tbDate.Text)
                            '  Dim oEmploye As CEmploye = CType(Session("Employe"), CEmploye)
                            Dim iEmployeID As Integer = oEmploye.EmployeID
                            Dim iType As Integer
                            Dim dNbHeure As Decimal
                            dNbHeure = CDec(tbNbHeures.Text)
                            Dim sOrganisme As String
                            sOrganisme = tbOrga.Text
                            iType = 0
                            Dim dCout As Double
                            If tbCout.Text = "" Then
                                dCout = 0
                            Else
                                dCout = CDbl(tbCout.Text)
                            End If

                            oFormation = New CFormation(slibelle, iLastID, iEmployeID, dNbHeure, iType, sOrganisme, 0, dCout, dDate)
                            Dim iInsert As Integer = oFormationDAO.InsertFormationAssocie(oFormation)
                            
                        Else
                            lblMsg.Visible = True
                            lblMsg.ForeColor = Color.Red
                            lblMsg.Text = "Erreur: Tous les champs avec une * doivent être remplis"
                        End If

                    End If



                ElseIf ddlProduit.SelectedValue = "47" Then
                    'Formation dispensée
                    If Not tbLibelle.Text = "" And Not tbNbHeures.Text = "" And Not tbNbParticipants.Text = "" Then

                        ' Dim oProduitDAO As New CProduitDAO
                        Dim iLastID As Integer = oProduitDAO.GetLastID()
                        Dim oFormation As CFormation
                        Dim oFormationDAO As New CFormationDAO
                        Dim slibelle As String = tbLibelle.Text

                        Dim dDate As Date = CDate(tbDate.Text)
                        ' Dim oEmploye As CEmploye = CType(Session("Employe"), CEmploye)
                        Dim iEmployeID As Integer = oEmploye.EmployeID
                        Dim iType As Integer
                        Dim dNbHeure As Decimal
                        dNbHeure = CDec(tbNbHeures.Text)
                        iType = 1
                        Dim iNbParticipants As Integer = CInt(tbNbParticipants.Text)


                        oFormation = New CFormation(slibelle, iLastID, iEmployeID, dNbHeure, iType, "", iNbParticipants, 0, dDate)
                        Dim iInsert As Integer = oFormationDAO.InsertFormationAssocie(oFormation)
                        
                    Else
                        lblMsg.Visible = True
                        lblMsg.ForeColor = Color.Red
                        lblMsg.Text = "Erreur: Tous les champs avec une * doivent être remplis"

                    End If

                End If
                '----------------------------------------------------------------------------------------------------------------------
                lbQteErreur.Visible = False
                lblMsg.Visible = True
                If (tblReleve.SelectedIndex = 1) Then
                    ' Produit non associé à une affaire
                    oProduitAffaire = New CProduitAffaire(CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                                 oEmploye.EmployeID, CInt(ddlService.SelectedValue),
                                                 CDec(sQuantite), CDate(tbDate.Text), CDec(sPU), CInt(ddlTVA.SelectedValue), New Decimal, tbLibelle.Text)

                    oProduitAffaireDAO.InsertProduitAffaireNonAssocie(oProduitAffaire)
                Else
                    ' Produit associé à une affaire
                    If CDbl(sPrix) = 0 Then
                        lblPrixNull.Visible = True
                        lblMsg.Visible = False
                    Else
                        ' On vérifie d'abord si l'affaire est déjà en dépassement
                        If oProduitAffaireDAO.SiAffaireEnDepassement(CInt(ddlAffaire.SelectedValue)) Then
                            '-----------------------------------------------------------------------------------------------------------------------------
                            'Dim iAffaireID As Integer = CInt(ddlAffaire.SelectedValue)
                            ' & Not oAffaireDAO.GetAffaire(iAffaireID).FraisInclus
                            If ddlType.SelectedItem.Text = "Frais" Then
                                oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(ddlAffaire.SelectedValue), CInt(ddlQualification.SelectedValue),
                                        CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                        CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                        oEmploye.EmployeID, CInt(ddlService.SelectedValue),
                                        CDec(sQuantite), CInt(ddlTVA.SelectedValue),
                                        CDate(tbDate.Text),
                                        CDec(sPU), New Decimal, tbLibelle.Text)
                            Else
                                ' L'affaire est en depassement
                                oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(ddlAffaire.SelectedValue), CInt(ddlQualification.SelectedValue),
                                            CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                            CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                            oEmploye.EmployeID, CInt(ddlService.SelectedValue),
                                            CDec(sQuantite), CInt(ddlTVA.SelectedValue),
                                            CDate(tbDate.Text),
                                            CDec(sPU), CDec(Formater.FormatDecimal(tbQuantite.Text)) * CDec(Formater.FormatDecimal(tbPrixUnit.Text)), tbLibelle.Text)
                            End If

                            oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)
                        Else
                            ' L'affaire n'est pas en depassement

                            ' On vérifie d'abord si ce produit lié à une affaire ferait dépasser le budget de l'affaire
                            Dim decConsoHT As Decimal = oProduitAffaireDAO.GetProduitAffaireAssocieBudget(CInt(ddlAffaire.SelectedValue))
                            Dim decNewConsoHT As Decimal = CDec(Formater.FormatDecimal(tbQuantite.Text)) * CDec(Formater.FormatDecimal(tbPrixUnit.Text))
                            'Dim decConsoTotal As Decimal = decConsoHT + decNewConsoHT
                            Dim decConsoTotal As Decimal
                            '-----------------------------------------------------------------------------------------------------------------------------
                            'Dim iAffaireID As Integer = CInt(ddlAffaire.SelectedValue)
                            ' & Not oAffaireDAO.GetAffaire(iAffaireID).FraisInclus

                            If ddlType.SelectedItem.Text = "Frais" Then
                                decConsoTotal = decConsoHT
                            Else
                                decConsoTotal = decConsoHT + decNewConsoHT
                            End If

                            '-----------------------------------------------------------------------------------------------------------------------------

                            Dim decDepassement As Decimal = decConsoTotal - oAffaireDAO.GetAffaireBudgetByAffaireID(CInt(ddlAffaire.SelectedValue))

                            If decDepassement <= 0 Then
                                ' Le produit ne fait pas dépasser le budget
                                oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(ddlAffaire.SelectedValue), CInt(ddlQualification.SelectedValue),
                                                                                    CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                                                                    CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                                                                    oEmploye.EmployeID, CInt(ddlService.SelectedValue),
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
                                    oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(ddlAffaire.SelectedValue), CInt(ddlQualification.SelectedValue),
                                                                CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                                                CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                                                oEmploye.EmployeID, CInt(ddlService.SelectedValue),
                                                                CDec(decJoursDansBudget), CInt(ddlTVA.SelectedValue),
                                                                CDate(tbDate.Text),
                                                                CDec(sPU), New Decimal, tbLibelle.Text)
                                    oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)
                                End If

                                ' 2) Produit avec dépassement
                                oProduitAffaireAssocie = New CProduitAffaireAssocie(CInt(ddlAffaire.SelectedValue), CInt(ddlQualification.SelectedValue),
                                                                CInt(ddlSite.SelectedValue), tbLibelle.Text, tbDonneurOrdre.Text,
                                                                CInt(ddlType.SelectedValue), CInt(ddlProduit.SelectedValue),
                                                                oEmploye.EmployeID, CInt(ddlService.SelectedValue),
                                                                CDec(decJoursHorsBudget), CInt(ddlTVA.SelectedValue),
                                                                CDate(tbDate.Text),
                                                                CDec(sPU), decMontantHorsBudget, tbLibelle.Text)
                                oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)
                            End If

                        End If

                    End If

                End If
                gvProduitAffaire.EditIndex = -1
                LoadGridView()


                oAffaireDAO.UpdateSommeProduitsParAffaire(CInt(ddlAffaire.SelectedValue))
                oAffaireDAO.UpdateProduitsDepassement(CInt(ddlAffaire.SelectedValue))

                lblMsg.ForeColor = Color.Blue
                lblMsg.Text = vbLf & "Enregistrement réussi : vous venez de rentrer " & tbQuantite.Text & " unité(s) de " & ddlProduit.SelectedItem.Text & " pour le " & tbDate.Text & "."
                iRes = 1
                End If
                'End If
            End If

            chargerResumeSurPeriode(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), oEmploye.EmployeID)
            Return iRes
    End Function


    ''' <summary>
    '''  charge le gridview
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadGridView()
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduitAffaire As DataSet
        Dim oEmploye As CEmploye

        oEmploye = CType(Session("Employe"), CEmploye)
        dsProduitAffaire = oProduitAffaireDAO.GetAllProduitAffaireEmployeIDAndDate(True, oEmploye.EmployeID, CDate(tbDateDeb.Text), CDate(tbDateFin.Text))

        gvProduitAffaire.DataSource = dsProduitAffaire
        gvProduitAffaire.DataBind()
    End Sub

    ''' <summary>
    ''' charge le gridview de la partit visualisation employe
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadGridViewVisualisation(Optional ByVal idAffaire As Integer = -1)
        Dim oEmployeDAO As New CEmployeDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsEmploye, dsProduitAffaire As New DataSet

        If (ddlEmployeVisualisation.SelectedValue = "default") Then
            dsProduitAffaire.Tables.Add(New DataTable)
            dsProduitAffaire.Tables(0).Columns.Add("ProduitAffaireID", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("EmployeID", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("Employe", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("ProduitAffaireDate", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("ClientNom", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("ProduitRef", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("ProduitAffaireLibelle", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("ProduitAffaireDonneurOrdre", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("ProduitAffairePrixUnit", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("ProduitAffaireQte", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("totalHT", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("TvaTaux", Type.GetType("System.String"))
            dsProduitAffaire.Tables(0).Columns.Add("TotalTTC", Type.GetType("System.String"))

            dsEmploye = oEmployeDAO.GetAllEmployeToList()

            For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
                Dim dtr As DataRow = dsProduitAffaire.Tables(0).NewRow
                dtr("Employe") = dsEmploye.Tables(0).Rows(i)("Employe")
                dsProduitAffaire.Tables(0).Rows.Add(dtr)

                If oProduitAffaireDAO.SiEmployeProduitAffaire(CInt(dsEmploye.Tables(0).Rows(i)("EmployeID"))) Then
                    Dim dsInter As DataSet = oProduitAffaireDAO.GetAllProduitAffaireEmployeIDAndDate(CBool(cbNonAssocie.Checked), CInt(dsEmploye.Tables(0).Rows(i)("EmployeID")), CDate(tbDateDebVisualisation.Text), CDate(tbDateFinVisualisation.Text), idAffaire)
                    For k = 0 To dsInter.Tables(0).Rows.Count - 1
                        dsProduitAffaire.Tables(0).ImportRow(dsInter.Tables(0).Rows(k))
                    Next
                End If
            Next
        Else
            dsProduitAffaire = oProduitAffaireDAO.GetAllProduitAffaireEmployeIDAndDate(CBool(cbNonAssocie.Checked), CInt(ddlEmployeVisualisation.SelectedValue), CDate(tbDateDebVisualisation.Text), CDate(tbDateFinVisualisation.Text), idAffaire)
        End If

        gvProduitAffaireVisualisation.DataSource = dsProduitAffaire
        gvProduitAffaireVisualisation.DataBind()
    End Sub

    ''' <summary>
    ''' charge le gridview de la partit visualisation employe
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadGridViewVisualisationEmploye(Optional ByVal idEmploye As Integer = -1, Optional ByVal idAffaire As Integer = -1)
        Dim oEmployeDAO As New CEmployeDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsEmploye, dsProduitAffaire As New DataSet

        dsProduitAffaire = oProduitAffaireDAO.GetAllProduitAffaireEmployeIDAndDate(CBool(cbNonAssocie.Checked), idEmploye, CDate(tbDateDebVisuEmploye.Text), CDate(tbDateFinVisuEmploye.Text), idAffaire)


        gvVisuEmploye.DataSource = dsProduitAffaire
        gvVisuEmploye.DataBind()
    End Sub
    ''' <summary>
    ''' Supprime un produit du gridview
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub gvProduitAffaire_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvProduitAffaire.RowCommand
        If e.CommandName = "DeleteProduitAffaire" Then
            Dim oProduitAffaireDAO As New CProduitAffaireDAO
            Dim oAffaireDAO As New CAffaireDAO
            Dim oFormationDAO As New CFormationDAO
            Try
                Dim iProduitAffaireID As Integer = CInt(e.CommandArgument.ToString)
                Dim oEmploye As New CEmploye
                oEmploye = CType(Session("Employe"), CEmploye)

                Dim iProduitID As Integer = oProduitAffaireDAO.GetProduitIDFromProduitAffaireID(iProduitAffaireID)
                Dim iAffaireId As Integer = oProduitAffaireDAO.GetAffaireIdFromProduitAffaireId(iProduitAffaireID)
                Dim iFormationID As Integer

                If iProduitID = 13 Or iProduitID = 47 Then
                    'Delete Formation avant ProduitAffaire car FK
                    iFormationID = oFormationDAO.GetFormationIDFromProduitAffaireID(iProduitAffaireID)
                    oFormationDAO.DeleteFormation(iFormationID)
                End If
                oProduitAffaireDAO.SupprimerProduitAffaire(iProduitAffaireID)

                LoadGridView()

                chargerResumeSurPeriode(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), oEmploye.EmployeID)

                oAffaireDAO.UpdateSommeProduitsParAffaire(iAffaireId)
                oAffaireDAO.UpdateProduitsDepassement(iAffaireId)

                lblConfirmation.Visible = True
                lblConfirmation.ForeColor = Color.Blue
                lblConfirmation.Text = vbLf & "Suppression réussie"

                gvProduitAffaire.EditIndex = -1
                LoadGridView()
            Catch ex As InvalidCastException
                lblConfirmation.Visible = True
                lblConfirmation.ForeColor = Color.Red
                lblConfirmation.Text = "Ce produit ne peut pas être supprimé !"
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Lorsque que 2 dates conformes sont choisies le relevé d'activité s'affiche
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ibDates_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibDates.Click
        Dim oEmploye As CEmploye = CType(Session("Employe"), CEmploye)
        chargerResumeSurPeriode(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), oEmploye.EmployeID)

        divReleve.Visible = True
        btnPeriode.Visible = True
        tbDateDeb.Enabled = False
        tbDateFin.Enabled = False
        cDateDeb.Visible = False
        cDateFin.Visible = False
        ibDates.Visible = False
        ibPourcentageRemplissage.Visible = True
        lblResume.Visible = True
        tbDate.Text = tbDateDeb.Text
        gvProduitAffaire.Visible = True
        LoadGridView()
    End Sub

    ''' <summary>
    '''  Permet de changer la période
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub btnPeriode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPeriode.Click
        divReleve.Visible = False
        btnPeriode.Visible = False
        tbDateDeb.Enabled = True
        tbDateFin.Enabled = True
        cDateDeb.Visible = True
        cDateFin.Visible = True
        ibDates.Visible = True
        lblResume.Visible = False
        ibPourcentageRemplissage.Visible = False

        gvProduitAffaire.Visible = False

        lblMsg.Text = ""
    End Sub

    ''' <summary>
    ''' Charge la liste des site
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargerListeSite(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAffaire.SelectedIndexChanged
        lblMsg.Text = ""
        tbLibelle.Text = ""

        Dim oClientSiteDAO As CClientDAO = New CClientDAO
        Dim oAffaireDAO As CAffaireDAO = New CAffaireDAO
        Dim oAffaire As CAffaire = New CAffaire
        Dim ds As DataSet

        If Not (ddlAffaire.SelectedItem.Value = "default") Then
            oAffaire = oAffaireDAO.GetAffaire(CInt(ddlAffaire.SelectedItem.Value))
            ds = oClientSiteDAO.GetAllClientSiteByAffaireIdToListe(CInt(ddlAffaire.SelectedItem.Value), oAffaire.Client.ClientID)

            ddlSite.DataSource = ds
            ddlSite.DataBind()
            ddlSite.SelectedIndex = 0
            ddlService.SelectedValue = CStr(oAffaire.Service.ServiceID)
        Else
            ddlSite.Items.Clear()
            ddlSite.Items.Insert(0, New ListItem("-- Site --", "default"))
        End If

        If tblReleve.SelectedIndex = 0 Then

            Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
            tbPrixUnit.Text = oPrixQualifTypeAffaireDAO.GetPrixHTQualifAffaire(CInt(ddlQualification.SelectedValue), CInt(ddlAffaire.SelectedValue), oAffaire.TypeAffaire.TypeAffaireID).ToString
        End If

    End Sub

    ''' <summary>
    ''' lance le chargement de la gridview de visualisation
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ibDatesVisualisation_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibDatesVisualisation.Click
        If cbbAffaire1.SelectedValue = "default" Then
            LoadGridViewVisualisation()
        Else
            LoadGridViewVisualisation(CInt(cbbAffaire1.SelectedValue))
        End If

    End Sub

    Protected Sub mMenuInformationCout_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mMenuInsertionVisualision.MenuItemClick
        mvInsertion.ActiveViewIndex = Int32.Parse(e.Item.Value)

    End Sub

    ''' <summary>
    ''' Charge la liste des employe dans la partie visualisation
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeEmployeVisualisation()
        Dim oEmployeDAO As CEmployeDAO = New CEmployeDAO
        Dim oEmploye As CEmploye = New CEmploye
        Dim ds As DataSet

        oEmploye = CType(Session("Employe"), CEmploye)
        ds = oEmployeDAO.GetAllEmployeToList

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les employés"
        itemDefaut.Value = "default"
        ddlEmployeVisualisation.Items.Insert(0, itemDefaut)
        ddlEmployeVisualisation.SelectedValue = "default"

        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim cbbText As String = ds.Tables(0).Rows(i)("Employe").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = ds.Tables(0).Rows(i)("EmployeID").ToString
            ddlEmployeVisualisation.Items.Add(item)
        Next
        ddlEmployeVisualisation.DataBind()

    End Sub

    ''' <summary>
    ''' Donne un résumé des RA sur la période sélectionnée
    ''' </summary>
    Protected Sub chargerResumeSurPeriode(ByVal dDateDeb As Date, ByVal dDateFin As Date, ByVal idEmploye As Integer)
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim decNbJoursSaisi, decNbJoursCA, decNbJoursNonCA, decNbJoursDep, decTauxFactu, decCAHT As Decimal

        Dim iNbJourOuvres As Integer = oStatistiquesDAO.NbJoursOuvres(dDateDeb, dDateFin)

        Dim dResult() As Decimal = oProduitAffaireDAO.GetResumeEmployePeriode(dDateDeb, dDateFin, idEmploye)
        decNbJoursDep = dResult(0)
        decNbJoursCA = dResult(1)
        decNbJoursSaisi = dResult(2)
        decTauxFactu = dResult(3)
        decCAHT = dResult(4)
        decNbJoursNonCA = decNbJoursSaisi - decNbJoursCA - decNbJoursDep

        lblResume.Text = "Résumé sur la période: " & decNbJoursSaisi & " jours saisis (dont " & decNbJoursCA & " jours CA /  " & decNbJoursNonCA & " jours non CA / " & decNbJoursDep & " jours Dépassement) sur " & iNbJourOuvres & " jours ouvrés."
        lblResume.Text &= "<br>Taux de facturation moyen : " & FormatNumber(decTauxFactu, 2) & " %, CA HT de la société : " & FormatNumber(decCAHT, 2) & " €"

        Dim decPourcentageJourRemplis = decNbJoursSaisi / iNbJourOuvres

        If decPourcentageJourRemplis < 0.25 Then
            ibPourcentageRemplissage.ImageUrl = "~/App_Themes/ComptaAna/Design/barre0.png"
        ElseIf decPourcentageJourRemplis < 0.5 Then
            ibPourcentageRemplissage.ImageUrl = "~/App_Themes/ComptaAna/Design/barre25.png"
        ElseIf decPourcentageJourRemplis < 0.75 Then
            ibPourcentageRemplissage.ImageUrl = "~/App_Themes/ComptaAna/Design/barre50.png"
        ElseIf decPourcentageJourRemplis < 1 Then
            ibPourcentageRemplissage.ImageUrl = "~/App_Themes/ComptaAna/Design/barre75.png"
        Else
            ibPourcentageRemplissage.ImageUrl = "~/App_Themes/ComptaAna/Design/barre100.png"
        End If

        ibPourcentageRemplissage.ToolTip = "Jours non remplis à 100%"

        Session("idEmployeCourant") = idEmploye

    End Sub

    Private Sub gvProduitAffaire_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvProduitAffaire.RowCancelingEdit
        gvProduitAffaire.EditIndex = -1
        LoadGridView()
    End Sub

    ''' <summary>
    ''' Colore le gridview gvProduitAffaire
    ''' </summary>
    Protected Sub gvProduitAffaire_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvProduitAffaire.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)

            If drv("ProduitAffaireFacture").ToString = "1" Then
                e.Row.BackColor = Drawing.Color.FromArgb(36, 255, 146)
                e.Row.Enabled = False
            End If
            If drv("ProduitAffaireDate").ToString = "" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 254, 204)

                ' On masque les boutons de mise à jour / suppression
                CType(e.Row.Cells(gvProduitAffaire.Columns.Count - 2).Controls(0), ImageButton).Visible = False
                CType(e.Row.FindControl("btnDelete"), ImageButton).Visible = False

            Else
                If Not IsDBNull(drv("ProduitAffaireDepassementMnt")) Then
                    For i As Integer = 0 To gvProduitAffaire.Columns.Count - 1
                        e.Row.Cells(i).ForeColor = Drawing.Color.FromArgb(230, 0, 115)
                    Next
                End If

                Dim iProduitAffaireID As Integer = CInt(drv(ProduitAffaire.ProduitAffaireID))
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
                        'drv("ProduitAffairePrixUnit") = GetMontantHT(CDbl(drv("ProduitAffairePrixUnit")), dTvaMontant).ToString
                        If Not IsNothing(e.Row.Cells(11).FindControl("tbEditProduitAffaireMntUnitHT")) Then
                            CType(e.Row.Cells(11).FindControl("tbEditProduitAffaireMntUnitHT"), TextBox).Text = GetMontantHT(CDbl(drv("ProduitAffairePrixUnit")), dTvaMontant).ToString
                        End If
                    End If
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' colorer le gridview gvProduitAffaireVisualisation
    ''' </summary>
    Protected Sub gvProduitAffaireVisualisation_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)

            If drv("ProduitRef").ToString = "" Then
                Dim tempCell As New TableCell()
                tempCell.Text = drv("Employe").ToString

                e.Row.Cells.Clear()
                e.Row.Cells.Add(tempCell)
                e.Row.Cells(0).ColumnSpan = 11

                e.Row.BackColor = Drawing.Color.FromArgb(175, 228, 201)
            End If

            If drv("Employe").ToString = "" Then
                Dim cellProduit As New TableCell()
                cellProduit.Text = drv("ProduitRef").ToString
                Dim cellQte As New TableCell()
                cellQte.Text = drv("ProduitAffaireQte").ToString
                Dim cellTot As New TableCell()
                cellTot.Text = drv("totalHT").ToString

                e.Row.Cells.Clear()
                e.Row.Cells.Add(New TableCell())
                e.Row.Cells(0).ColumnSpan = 3

                e.Row.Cells.Add(cellProduit)
                e.Row.Cells(1).ColumnSpan = 4
                e.Row.Cells.Add(cellQte)

                e.Row.Cells.Add(cellTot)
                e.Row.Cells(3).ColumnSpan = 3

                e.Row.BackColor = Drawing.Color.FromArgb(255, 255, 204)
            End If

        End If
    End Sub

    Private Sub ddlQualification_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQualification.SelectedIndexChanged
        Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
        Dim oAffaireDAO As CAffaireDAO = New CAffaireDAO
        Dim oAffaire As CAffaire = New CAffaire

        oAffaire = oAffaireDAO.GetAffaire(CInt(ddlAffaire.SelectedItem.Value))

        tbPrixUnit.Text = oPrixQualifTypeAffaireDAO.GetPrixHTQualifAffaire(CInt(ddlQualification.SelectedValue), CInt(ddlAffaire.SelectedValue), oAffaire.TypeAffaire.TypeAffaireID).ToString
    End Sub

    Private Sub ExportExcel()
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduitAffaire As DataSet
        Dim oEmploye As CEmploye
        Dim k As Integer = 0

        Dim iColNomEmploye As Integer = 0
        Dim iColDate As Integer = 1
        Dim iColClientSite As Integer = 2
        Dim iColProduitLibelle As Integer = 3
        Dim iColProduitReference As Integer = 4
        Dim iColProduitAffaireLibelle As Integer = 5
        Dim iColDonneurOrdre As Integer = 6
        Dim iColPrixUnitaire As Integer = 7
        Dim iColQuantite As Integer = 8
        Dim iColTotalHT As Integer = 9
        Dim iColTVA As Integer = 10
        Dim iColTotalTTC As Integer = 11

        oEmploye = CType(Session("Employe"), CEmploye)
        dsProduitAffaire = oProduitAffaireDAO.GetAllProduitAffaireEmployeIDAndDate(True, oEmploye.EmployeID, CDate(tbDateDeb.Text), CDate(tbDateFin.Text))

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("ReleveActivite")

        ' Affichage de l'en-tête
        ws.Cells(0, iColNomEmploye).Value = "Nom"
        ws.Cells(0, iColDate).Value = "Date"
        ws.Cells(0, iColClientSite).Value = "Client-Site"
        ws.Cells(0, iColProduitLibelle).Value = "Produit"
        ws.Cells(0, iColProduitReference).Value = "Référence"
        ws.Cells(0, iColProduitAffaireLibelle).Value = "Libellé"
        ws.Cells(0, iColDonneurOrdre).Value = "Donneur d'ordre"
        ws.Cells(0, iColPrixUnitaire).Value = "Prix Unitaire"
        ws.Cells(0, iColQuantite).Value = "Quantité"
        ws.Cells(0, iColTotalHT).Value = "Total HT"
        ws.Cells(0, iColTVA).Value = "TVA"
        ws.Cells(0, iColTotalTTC).Value = "Total TTC"

        ' Parcours du dataset
        For i As Integer = 0 To dsProduitAffaire.Tables(0).Rows.Count - 1
            If dsProduitAffaire.Tables(0).Rows(i)(0).ToString() <> "" Then
                k += 1
                For j As Integer = 1 To dsProduitAffaire.Tables(0).Columns.Count - 1
                    ws.Cells(k, iColNomEmploye).Value = dsProduitAffaire.Tables(0).Rows(i)("Employe")
                    ws.Cells(k, iColDate).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffaireDate")
                    ws.Cells(k, iColClientSite).Value = dsProduitAffaire.Tables(0).Rows(i)("ClientNom")
                    ws.Cells(k, iColProduitLibelle).Value = dsProduitAffaire.Tables(0).Rows(i)("TypeProduitLibelle")
                    ws.Cells(k, iColProduitReference).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffaireLibelle")
                    ws.Cells(k, iColProduitAffaireLibelle).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitRef")
                    ws.Cells(k, iColDonneurOrdre).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffaireDonneurOrdre")
                    ws.Cells(k, iColPrixUnitaire).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffairePrixUnit")
                    ws.Cells(k, iColQuantite).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffaireQte")
                    ws.Cells(k, iColTotalHT).Value = dsProduitAffaire.Tables(0).Rows(i)("totalHT")
                    ws.Cells(k, iColTVA).Value = dsProduitAffaire.Tables(0).Rows(i)("TvaTaux")
                    ws.Cells(k, iColTotalTTC).Value = dsProduitAffaire.Tables(0).Rows(i)("totalTTC")
                Next
            End If
        Next

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "ReleveActivite" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)
    End Sub
    Private Sub ibExporter_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibExporter.Click
        ExportExcel()
    End Sub

    Private Sub gvProduitAffaire_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvProduitAffaire.RowDeleting
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Try
            Dim iProduitAffaireID As Integer = CInt(gvProduitAffaire.DataKeys(e.RowIndex).Values(0))
            Dim oEmploye As New CEmploye
            oEmploye = CType(Session("Employe"), CEmploye)

            oProduitAffaireDAO.SupprimerProduitAffaire(iProduitAffaireID)

            LoadGridView()

            chargerResumeSurPeriode(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), oEmploye.EmployeID)
            lblConfirmation.Visible = True
            lblConfirmation.ForeColor = Color.Blue
            lblConfirmation.Text = vbLf & "Suppression réussie"
        Catch ex As InvalidCastException
            lblConfirmation.Visible = True
            lblConfirmation.ForeColor = Color.Red
            lblConfirmation.Text = "Ce produit ne peut pas être supprimé !"
        End Try
    End Sub

    Private Sub gvProduitAffaire_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvProduitAffaire.RowEditing
        gvProduitAffaire.EditIndex = e.NewEditIndex
        LoadGridView()
    End Sub

    Private Sub gvProduitAffaire_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvProduitAffaire.RowUpdating
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oProduitAffaire As New CProduitAffaire
        Dim oAffaireDAO As New CAffaireDAO
        Dim oFormationDAO As New CFormationDAO


        Dim iProduitAffaireID As Integer = CInt(gvProduitAffaire.DataKeys(e.RowIndex).Values(0))
        Dim iTypeProduitID As Integer = CInt(gvProduitAffaire.DataKeys(e.RowIndex).Values(1))
        Dim sDate As String = gvProduitAffaire.DataKeys(e.RowIndex).Values(2).ToString
        Dim iEmployeID As Integer = CType(Session("Employe"), CEmploye).EmployeID
        Dim iAffaireId As Integer = oProduitAffaireDAO.GetAffaireIdFromProduitAffaireId(iProduitAffaireID)

        Dim sLibelle As String = CType(gvProduitAffaire.Rows(e.RowIndex).FindControl("tbEditProduitAffaireLibelle"), TextBox).Text
        Dim decPrix As Decimal = CDec(CType(gvProduitAffaire.Rows(e.RowIndex).FindControl("tbEditProduitAffaireMntUnitHT"), TextBox).Text.Replace(".", ","))
        Dim decQte As Decimal = CDec(CType(gvProduitAffaire.Rows(e.RowIndex).FindControl("tbEditProduitAffaireQte"), TextBox).Text.Replace(".", ","))
        Dim iProduitID As Integer

        ' On en enregistre la valeur en TTC pour les produits désignés dans le catalogue en Refac. = TTC
        Dim dsProduitAffaire As DataSet = oProduitAffaireDAO.LoadOne(iProduitAffaireID)
        Dim iProduitIDCourant As Integer = 0
        Dim dTvaMontant As Double = CDbl(gvProduitAffaire.Rows(e.RowIndex).Cells(12).Text)

        If dsProduitAffaire.Tables(0).Rows.Count > 0 Then
            iProduitIDCourant = CInt(dsProduitAffaire.Tables(0).Rows(0)(ProduitAffaire.ProduitID))
        End If

        Dim oProduit As New CProduit
        Dim oProduitDAO As New CProduitDAO

        oProduit = oProduitDAO.GetProduit(iProduitIDCourant)
        If oProduit.ProduitRefac Then
            decPrix = CDec(GetMontantTTC(CDbl(decPrix), dTvaMontant))
        End If


        iProduitID = oProduitAffaireDAO.GetProduitIDFromProduitAffaireID(iProduitAffaireID)
        If iProduitID = 13 Or iProduitID = 47 Then
            If sLibelle = "" Then
                lblConfirmation.Visible = True
                lblConfirmation.ForeColor = Color.Red
                lblConfirmation.Text = "Modification impossible: Le libellé d'une formation ne doit pas être null"
            Else

                If oProduitAffaireDAO.EqualOne(iEmployeID, sDate, decQte, iTypeProduitID, True, iProduitAffaireID) Then
                    If oProduitAffaireDAO.SiAffaireEnDepassement(iAffaireId) Then
                        oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, decPrix * decQte)
                        oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
                    Else
                        Dim decConsoHT As Decimal = oProduitAffaireDAO.GetProduitAffaireAssocieBudget(iAffaireId, iProduitAffaireID)
                        Dim decNewConsoHT As Decimal = decPrix * decQte
                        Dim decConsoTotal As Decimal = decConsoHT + decNewConsoHT
                        Dim decDepassement As Decimal = decConsoTotal - oAffaireDAO.GetAffaireBudgetByAffaireID(iAffaireId)

                        If decDepassement <= 0 Then
                            ' Le produit ne fait pas dépasser le budget
                            oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, New Decimal)
                            oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
                        Else
                            ' Le produit fait dépasser le budget

                            Dim decMontantDansBudget As Decimal = decNewConsoHT - decDepassement
                            Dim decJoursDansBudget As Decimal = decMontantDansBudget / decNewConsoHT
                            ' On arrondit cette valeur à la 3ème décimale (vers 0)
                            'decJoursDansBudget = 0 + Int(decJoursDansBudget * 1000) / 1000

                            Dim decMontantHorsBudget As Decimal = decDepassement
                            Dim decJoursHorsBudget As Decimal = CDec(decQte) - decJoursDansBudget

                            ' On modifie le produit déjà entré de manière à atteindre le budget, donc dépassement=0
                            oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decJoursDansBudget, New Decimal)
                            oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)

                            ' On ajoute un nouveau produit avec le dépassement
                            'Dim oProduitAffaireAssocieDAO As New CProduitAffaireDAO
                            Dim iQualificationId As Integer = -1
                            Dim iClientId As Integer = -1
                            Dim sDonneurOrdre As String = ""
                            iProduitID = -1
                            Dim iServiceId As Integer = -1
                            Dim iTvaId As Integer = -1

                            Dim ds As DataSet = oProduitAffaireDAO.LoadAllProduitAffaire(iProduitAffaireID)
                            If ds.Tables(0).Rows.Count > 0 Then
                                iQualificationId = CInt(ds.Tables(0).Rows(0)("QualificationID"))
                                iClientId = CInt(ds.Tables(0).Rows(0)("ClientID"))
                                sDonneurOrdre = ds.Tables(0).Rows(0)("ProduitAffaireDonneurOrdre").ToString
                                iTypeProduitID = CInt(ds.Tables(0).Rows(0)("TypeProduitID"))
                                iProduitID = CInt(ds.Tables(0).Rows(0)("ProduitID"))
                                iEmployeID = CInt(ds.Tables(0).Rows(0)("EmployeID"))
                                iServiceId = CInt(ds.Tables(0).Rows(0)("ServiceID"))
                                iTvaId = CInt(ds.Tables(0).Rows(0)("TvaID"))
                            End If

                            Dim oProduitAffaireAssocie As CProduitAffaireAssocie
                            oProduitAffaireAssocie = New CProduitAffaireAssocie(iAffaireId, iQualificationId,
                                                                iClientId, sLibelle, sDonneurOrdre,
                                                                iTypeProduitID, iProduitID,
                                                                iEmployeID, iServiceId,
                                                                CDec(decJoursHorsBudget), iTvaId,
                                                                CDate(sDate),
                                                                decPrix, decMontantHorsBudget, sLibelle)
                            oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)

                        End If
                    End If
                    lbQteErreur.Visible = False
                    lblConfirmation.Visible = True
                    lblConfirmation.Text = "Modification réussie"

                    'La modification du ProduitAffaire doit entraîner la modif de la formation (Libellé)
                    Dim iFormationID As Integer

                    iProduitID = oProduitAffaireDAO.GetProduitIDFromProduitAffaireID(iProduitAffaireID)

                    If iProduitID = 13 Or iProduitID = 47 Then
                        'Delete Formation avant ProduitAffaire car FK
                        iFormationID = oFormationDAO.GetFormationIDFromProduitAffaireID(iProduitAffaireID)
                        oFormationDAO.UpDateFormation(iFormationID, sLibelle)
                    End If

                    oAffaireDAO.UpdateSommeProduitsParAffaire(iAffaireId)
                    oAffaireDAO.UpdateProduitsDepassement(iAffaireId)

                    chargerResumeSurPeriode(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), iEmployeID)
                Else
                    lblConfirmation.Visible = True
                    lblConfirmation.Text = "Modification échouée : la quantité journalière est supérieure à 1 !"
                    lblMsg.Visible = False
                End If
            End If

        Else
            If oProduitAffaireDAO.EqualOne(iEmployeID, sDate, decQte, iTypeProduitID, True, iProduitAffaireID) Then
                If oProduitAffaireDAO.SiAffaireEnDepassement(iAffaireId) Then
                    oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, decPrix * decQte)
                    oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
                Else
                    Dim decConsoHT As Decimal = oProduitAffaireDAO.GetProduitAffaireAssocieBudget(iAffaireId, iProduitAffaireID)
                    Dim decNewConsoHT As Decimal = decPrix * decQte
                    Dim decConsoTotal As Decimal = decConsoHT + decNewConsoHT
                    Dim decDepassement As Decimal = decConsoTotal - oAffaireDAO.GetAffaireBudgetByAffaireID(iAffaireId)

                    If decDepassement <= 0 Then
                        ' Le produit ne fait pas dépasser le budget
                        oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, New Decimal)
                        oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
                    Else
                        ' Le produit fait dépasser le budget

                        Dim decMontantDansBudget As Decimal = decNewConsoHT - decDepassement
                        Dim decJoursDansBudget As Decimal = decMontantDansBudget / decNewConsoHT
                        ' On arrondit cette valeur à la 3ème décimale (vers 0)
                        'decJoursDansBudget = 0 + Int(decJoursDansBudget * 1000) / 1000

                        Dim decMontantHorsBudget As Decimal = decDepassement
                        Dim decJoursHorsBudget As Decimal = CDec(decQte) - decJoursDansBudget

                        ' On modifie le produit déjà entré de manière à atteindre le budget, donc dépassement=0
                        oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decJoursDansBudget, New Decimal)
                        oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)

                        ' On ajoute un nouveau produit avec le dépassement
                        'Dim oProduitAffaireAssocieDAO As New CProduitAffaireDAO
                        Dim iQualificationId As Integer = -1
                        Dim iClientId As Integer = -1
                        Dim sDonneurOrdre As String = ""
                        iProduitID = -1
                        Dim iServiceId As Integer = -1
                        Dim iTvaId As Integer = -1

                        Dim ds As DataSet = oProduitAffaireDAO.LoadAllProduitAffaire(iProduitAffaireID)
                        If ds.Tables(0).Rows.Count > 0 Then
                            iQualificationId = CInt(ds.Tables(0).Rows(0)("QualificationID"))
                            iClientId = CInt(ds.Tables(0).Rows(0)("ClientID"))
                            sDonneurOrdre = ds.Tables(0).Rows(0)("ProduitAffaireDonneurOrdre").ToString
                            iTypeProduitID = CInt(ds.Tables(0).Rows(0)("TypeProduitID"))
                            iProduitID = CInt(ds.Tables(0).Rows(0)("ProduitID"))
                            iEmployeID = CInt(ds.Tables(0).Rows(0)("EmployeID"))
                            iServiceId = CInt(ds.Tables(0).Rows(0)("ServiceID"))
                            iTvaId = CInt(ds.Tables(0).Rows(0)("TvaID"))
                        End If

                        Dim oProduitAffaireAssocie As CProduitAffaireAssocie
                        oProduitAffaireAssocie = New CProduitAffaireAssocie(iAffaireId, iQualificationId,
                                                            iClientId, sLibelle, sDonneurOrdre,
                                                            iTypeProduitID, iProduitID,
                                                            iEmployeID, iServiceId,
                                                            CDec(decJoursHorsBudget), iTvaId,
                                                            CDate(sDate),
                                                            decPrix, decMontantHorsBudget, sLibelle)
                        oProduitAffaireDAO.InsertProduitAffaireAssocie(oProduitAffaireAssocie)

                    End If
                End If
                lbQteErreur.Visible = False
                lblConfirmation.Visible = True
                lblConfirmation.Text = "Modification réussie"

                'La modification du ProduitAffaire doit entraîner la modif de la formation (Libellé)
                Dim iFormationID As Integer

                iProduitID = oProduitAffaireDAO.GetProduitIDFromProduitAffaireID(iProduitAffaireID)

                If iProduitID = 13 Or iProduitID = 47 Then
                    'Delete Formation avant ProduitAffaire car FK
                    iFormationID = oFormationDAO.GetFormationIDFromProduitAffaireID(iProduitAffaireID)
                    oFormationDAO.UpDateFormation(iFormationID, sLibelle)
                End If

                oAffaireDAO.UpdateSommeProduitsParAffaire(iAffaireId)
                oAffaireDAO.UpdateProduitsDepassement(iAffaireId)

                chargerResumeSurPeriode(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), iEmployeID)
            Else
                lblConfirmation.Visible = True
                lblConfirmation.Text = "Modification échouée : la quantité journalière est supérieure à 1 !"
                lblMsg.Visible = False
            End If

        End If

        gvProduitAffaire.EditIndex = -1
        LoadGridView()
    End Sub

    Private Sub ddlProduit_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProduit.SelectedIndexChanged

        Dim oProduit As New CProduit
        Dim oProduitDAO As New CProduitDAO

        oProduit = oProduitDAO.GetProduit(CInt(ddlProduit.SelectedValue))


        If oProduit.ProduitID = 13 Then
            'Formation suivie
            lblNbHeures.Visible = True
            tbNbHeures.Visible = True
            lblOrganisme.Visible = True
            tbOrga.Visible = True
            lblCout.Visible = True
            tbCout.Visible = True
            lblNbParticipants.Visible = False
            tbNbParticipants.Visible = False
            lblLibelle.Text = "Libellé * :"
            rbIntExt.Visible = True
        ElseIf oProduit.ProduitID = 47 Then
            'Formation dispensée
            lblNbHeures.Visible = True
            tbNbHeures.Visible = True
            lblOrganisme.Visible = False
            tbOrga.Visible = False
            lblCout.Visible = False
            tbCout.Visible = False
            lblNbParticipants.Visible = True
            tbNbParticipants.Visible = True
            lblLibelle.Text = "Libellé * :"
            rbIntExt.Visible = False
        End If
        ' On ajoute un astérisque avec une info-bulle pour avertir l'utilisateur que le produit en question
        ' sera facturé en TTC
        If oProduit.ProduitRefac Then
            lblWarningTTC.Visible = True
        Else
            lblWarningTTC.Visible = False
        End If

    End Sub

    Private Sub ibPourcentageRemplissage_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibPourcentageRemplissage.Click

        Dim oProduitAffaireDAO As New CProduitAffaireDAO

        Dim lDatesJoursSaisis As List(Of Tuple(Of Date, String)) = oProduitAffaireDAO.GetDateSaisies(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), CSession.EmployeID)

        Dim lDatesJoursOuvres = datesJoursOuvres(CDate(tbDateDeb.Text), CDate(tbDateFin.Text))
        Dim i As Integer = 0

        For Each tuple As Tuple(Of Date, String) In lDatesJoursSaisis
            Dim dateEnCours As Date = tuple.Item1

            Dim sQu As String = tuple.Item2

            If sQu = "1" Then
                Dim tupleEqui As Tuple(Of Date, String) = New Tuple(Of Date, String)(dateEnCours, "0")
                lDatesJoursOuvres.Remove(tupleEqui)
            Else
                Dim tuplenew As Tuple(Of Date, String) = New Tuple(Of Date, String)(dateEnCours, sQu)


                While Not (lDatesJoursOuvres(i).Item1 = tuplenew.Item1)
                    i = i + 1
                End While

                lDatesJoursOuvres(i) = tuplenew
            End If
        Next

        Dim dsJoursNonSaisis As DataSet = New DataSet

        dsJoursNonSaisis.Tables.Add("JoursNonSaisis")

        Dim dcColonneAnnee As New DataColumn
        dcColonneAnnee.ColumnName = "Année"
        dcColonneAnnee.DataType = GetType(String)
        dsJoursNonSaisis.Tables("JoursNonSaisis").Columns.Add(dcColonneAnnee)

        Dim dcColonneMois As New DataColumn
        dcColonneMois.ColumnName = "Mois"
        dcColonneMois.DataType = GetType(String)
        dsJoursNonSaisis.Tables("JoursNonSaisis").Columns.Add(dcColonneMois)

        Dim dcColonneJour As New DataColumn
        dcColonneJour.ColumnName = "Jour"
        dcColonneJour.DataType = GetType(String)
        dsJoursNonSaisis.Tables("JoursNonSaisis").Columns.Add(dcColonneJour)

        Dim dcColonneQuantité As New DataColumn
        dcColonneQuantité.ColumnName = "Quantité"
        dcColonneQuantité.DataType = GetType(String)
        dsJoursNonSaisis.Tables("JoursNonSaisis").Columns.Add(dcColonneQuantité)

        For Each tuple As Tuple(Of Date, String) In lDatesJoursOuvres
            Dim dateEnCours As Date = tuple.Item1
            Dim sQu As String = tuple.Item2

            dsJoursNonSaisis.Tables("JoursNonSaisis").Rows.Add(dateEnCours.Year, dateEnCours.Month, dateEnCours.Day, sQu)

        Next

        gvInfosRemplissage.DataSource = dsJoursNonSaisis
        gvInfosRemplissage.DataBind()

        pPopupInfosRemplissage.Visible = True
    End Sub

    ''' <summary>
    ''' Permet de recuperer une liste des dates correspondantes aux jours ouvres
    ''' </summary>
    ''' <param name="dDateDeb"></param>
    ''' <param name="dDateFin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function datesJoursOuvres(ByVal dDateDeb As Date, ByVal dDateFin As Date) As List(Of Tuple(Of Date, String))
        Dim lDeDates As List(Of Tuple(Of Date, String)) = New List(Of Tuple(Of Date, String))
        Dim dDate As Date = dDateDeb

        Do Until dDate > dDateFin
            Select Case Weekday(dDate)
                Case vbMonday, vbTuesday, vbWednesday, vbThursday, vbFriday
                    Dim tuple As Tuple(Of Date, String) = New Tuple(Of Date, String)(dDate, "0")
                    lDeDates.Add(tuple)
            End Select
            dDate = DateAdd("d", 1, dDate)
        Loop
        Return lDeDates
    End Function

    Private Sub btnQuitter_Click(sender As Object, e As System.EventArgs) Handles btnQuitter.Click
        pPopupInfosRemplissage.Visible = False
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
                oAffaire = oAffaireDAO.GetAffaire(CInt(ddlAffaire.SelectedItem.Value))
                If tblReleve.SelectedIndex = 1 Then
                    tbPrixUnit.Text = "0"
                Else
                    tbPrixUnit.Text = oPrixQualifTypeAffaireDAO.GetPrixHTQualifAffaire(CInt(ddlQualification.SelectedValue), CInt(ddlAffaire.SelectedValue), oAffaire.TypeAffaire.TypeAffaireID).ToString
                End If


                tbLibelle.Text = ""
                tbQuantite.Text = "0"
        End Select
    End Sub

    Public Shared Function GetLastDayOfThisMonth() As DateTime
        Dim now As DateTime = DateTime.Now
        Dim nbDays As Integer = DateTime.DaysInMonth(now.Year, now.Month)

        now = New DateTime(now.Year, now.Month, nbDays, 23, 59, 59, 999)

        Dim b = True

        While b
            Select Case Weekday(now)
                Case vbSaturday, vbSunday
                    nbDays = nbDays - 1
                    now = New DateTime(now.Year, now.Month, nbDays, 23, 59, 59, 999)

                Case Else
                    b = False
            End Select
        End While

        Return now
    End Function


    Private Sub cbNonAssocie_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbNonAssocie.CheckedChanged
        tbPrixUnit.Enabled = False
        tbPrixUnit.BackColor = Color.LightGray
    End Sub

    Private Sub btnRechercheVisuEmploye_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRechercheVisuEmploye.Click
        Dim oEmploye As CEmploye = CType(Session("Employe"), CEmploye)
        Dim iAffaireID As Integer
        If Not (cbbAffaires2.SelectedValue) = "default" Then
            iAffaireID = CInt(cbbAffaires2.SelectedValue)
            LoadGridViewVisualisationEmploye(oEmploye.EmployeID, iAffaireID)

        Else
            LoadGridViewVisualisationEmploye(oEmploye.EmployeID)
        End If



    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        CreationProduit()

        pDepassement.Visible = False
    End Sub

    Private Sub btnAnnuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
        pDepassement.Visible = False
    End Sub

    Private Function ProduitEnDepassement() As Boolean

        Dim oAffaireDAO As New CAffaireDAO
        Dim affaire As CAffaire = oAffaireDAO.GetAffaire(CInt(ddlAffaire.SelectedValue))
        Dim dSommePdt As Double = CDbl(affaire.AffaireSommeProduits)
        Dim dBudget As Double = CDbl(affaire.AffaireBudget)
        Dim dPrixPdt As Double

        dPrixPdt = (CDbl(tbQuantite.Text.Replace(CChar("."), CChar(",")))) * CDbl(tbPrixUnit.Text.Replace(CChar("."), CChar(","))) * CDbl(ddlTVA.SelectedItem.Text.Replace(CChar("."), CChar(","))) / 100 + CDbl(tbQuantite.Text.Replace(CChar("."), CChar(","))) * CDbl(tbPrixUnit.Text.Replace(CChar("."), CChar(",")))


        Return dSommePdt + dPrixPdt > dBudget

    End Function
    Private Sub ExportExcelVisu()
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduitAffaire As DataSet
        Dim oEmployeDAO As New CEmployeDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim k As Integer = 0

        Dim iColNomEmploye As Integer = 0
        Dim iColDate As Integer = 1
        Dim iColClientSite As Integer = 2
        Dim iColProduitLibelle As Integer = 3
        Dim iColProduitReference As Integer = 4
        Dim iColProduitAffaireLibelle As Integer = 5
        Dim iColPrixUnitaire As Integer = 6
        Dim iColQuantite As Integer = 7
        Dim iColTotalHT As Integer = 8

        Dim iEmployeID As Integer
        If ddlEmployeVisualisation.SelectedValue = "default" Then
            iEmployeID = -1
        Else
            iEmployeID = oEmployeDAO.GetEmployeById(CInt(ddlEmployeVisualisation.SelectedValue)).EmployeID
        End If
        Dim iAffaireID As Integer
        If cbbAffaire1.SelectedValue = "default" Then
            iAffaireID = -1
        Else
            iAffaireID = oAffaireDAO.GetAffaire(CInt(cbbAffaire1.SelectedValue)).AffaireID
        End If

        dsProduitAffaire = oProduitAffaireDAO.GetAllProduitAffaireEmployeIDAndDate(True, iEmployeID, CDate(tbDateDebVisualisation.Text), CDate(tbDateFinVisualisation.Text), iAffaireID)

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("ReleveActivite")

        ' Affichage de l'en-tête
        ws.Cells(2, iColNomEmploye).Value = "Nom"
        ws.Cells(2, iColNomEmploye).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColNomEmploye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColNomEmploye).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColNomEmploye).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColNomEmploye).Style.Font.Weight = 1000
        ws.Cells(2, iColDate).Value = "Date"
        ws.Cells(2, iColDate).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColDate).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDate).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColDate).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColDate).Style.Font.Weight = 1000
        ws.Cells(2, iColClientSite).Value = "Client-Site"
        ws.Cells(2, iColClientSite).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColClientSite).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColClientSite).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColClientSite).Style.Font.Weight = 1000
        ws.Cells(2, iColClientSite).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColProduitLibelle).Value = "Produit"
        ws.Cells(2, iColProduitLibelle).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColProduitLibelle).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColProduitLibelle).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColProduitLibelle).Style.Font.Weight = 1000
        ws.Cells(2, iColProduitLibelle).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColProduitReference).Value = "Référence"
        ws.Cells(2, iColProduitReference).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColProduitReference).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColProduitReference).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColProduitReference).Style.Font.Weight = 1000
        ws.Cells(2, iColProduitReference).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColProduitAffaireLibelle).Value = "Libellé"
        ws.Cells(2, iColProduitAffaireLibelle).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColProduitAffaireLibelle).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColProduitAffaireLibelle).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColProduitAffaireLibelle).Style.Font.Weight = 1000
        ws.Cells(2, iColProduitAffaireLibelle).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColPrixUnitaire).Value = "Prix Unitaire"
        ws.Cells(2, iColPrixUnitaire).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColPrixUnitaire).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColPrixUnitaire).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColPrixUnitaire).Style.Font.Weight = 1000
        ws.Cells(2, iColPrixUnitaire).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColQuantite).Value = "Quantité"
        ws.Cells(2, iColQuantite).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColQuantite).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColQuantite).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColQuantite).Style.Font.Weight = 1000
        ws.Cells(2, iColQuantite).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColTotalHT).Value = "Total HT"
        ws.Cells(2, iColTotalHT).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColTotalHT).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColTotalHT).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColTotalHT).Style.Font.Weight = 1000
        ws.Cells(2, iColTotalHT).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Rows(2).Height = 2 * 256

        ' Parcours du dataset
        For i As Integer = 0 To dsProduitAffaire.Tables(0).Rows.Count - 1

            If dsProduitAffaire.Tables(0).Rows(i)(0).ToString() <> "" Then
                k += 1
                For j As Integer = 1 To dsProduitAffaire.Tables(0).Columns.Count - 1
                    ws.Rows(k + 3).Height = 2 * 150
                    ws.Cells(k + 3, iColNomEmploye).Value = dsProduitAffaire.Tables(0).Rows(i)("Employe")
                    ws.Cells(k + 3, iColDate).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffaireDate")
                    ws.Cells(k + 3, iColClientSite).Value = dsProduitAffaire.Tables(0).Rows(i)("ClientNom")
                    ws.Cells(k + 3, iColProduitLibelle).Value = dsProduitAffaire.Tables(0).Rows(i)("TypeProduitLibelle")
                    ws.Cells(k + 3, iColProduitReference).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffaireLibelle")
                    ws.Cells(k + 3, iColProduitAffaireLibelle).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitRef")
                    ws.Cells(k + 3, iColPrixUnitaire).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffairePrixUnit")
                    ws.Cells(k + 3, iColQuantite).Value = dsProduitAffaire.Tables(0).Rows(i)("ProduitAffaireQte")
                    ws.Cells(k + 3, iColTotalHT).Value = dsProduitAffaire.Tables(0).Rows(i)("totalHT")

                Next
            End If
        Next

        ws.Columns(iColNomEmploye).Width = 30 * 256
        ws.Columns(iColNomEmploye).AutoFit()
        ws.Columns(iColDate).Width = 30 * 256
        ws.Columns(iColDate).AutoFit()
        ws.Columns(iColClientSite).Width = 60 * 256
        ' ws.Columns(iColClientSite).AutoFit()
        ws.Columns(iColProduitLibelle).Width = 30 * 256
        ws.Columns(iColProduitLibelle).AutoFit()
        ws.Columns(iColProduitReference).Width = 50 * 256
        ' ws.Columns(iColProduitReference).AutoFit()
        ws.Columns(iColProduitAffaireLibelle).Width = 30 * 256
        ' ws.Columns(iColProduitAffaireLibelle).AutoFit()
        ws.Columns(iColPrixUnitaire).Width = 30 * 256
        ws.Columns(iColPrixUnitaire).AutoFit()
        ws.Columns(iColQuantite).Width = 30 * 256
        ws.Columns(iColQuantite).AutoFit()
        ws.Columns(iColTotalHT).Width = 30 * 256
        ws.Columns(iColTotalHT).AutoFit()

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "ReleveActivite" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)
    End Sub
    Private Sub btnExportExcelVisu_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportExcelVisu.Click
        ExportExcelVisu()
    End Sub

    Private Sub rbIntExt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbIntExt.SelectedIndexChanged
        If rbIntExt.SelectedIndex = 0 Then
            lblCout.Text = "Coût horaire: "
            lblOrganisme.Text = "Lieu de formation:"
            tbOrga.Text = "Nos Locaux"
        ElseIf rbIntExt.SelectedIndex = 1 Then
            lblCout.Text = "Coût horaire*: "
            lblOrganisme.Text = "Organisme et lieu de formation*:"
            tbOrga.Text = ""
        End If

    End Sub
End Class