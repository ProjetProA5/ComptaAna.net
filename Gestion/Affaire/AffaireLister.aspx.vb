Imports System.Data
Imports ComptaAna.Business
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit
Imports Obout.ComboBox

Public Class AffaireLister
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' chargement de la page
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oRechercheAffaire As New RechercheAffaire
        oRechercheAffaire = CType(Session("recherche"), RechercheAffaire)

        If Not Page.IsPostBack Then
            
            'If Not IsNothing(Request.QueryString("affaire")) Then
            '    'fs1.Visible = True
            '    'fs2.Visible = True
            '    'fs3.Visible = True
            '    'fs4.Visible = True
            '    'LoadAffaire(CInt(Request.QueryString("affaire")))
            '    'mMenuBouton.Visible = True
            'Else
            '    tblMois.Visible = False
            'End If
                rbAffaireFiltre.SelectedIndex = 4
                cbbChampRechercheAffaire.SelectedIndex = 0
                LoadTreeViewClient(-1)
                chargerChargeAffaire()

                If Not (oRechercheAffaire Is Nothing) Then
                    ' chargerChargeAffaire()
                    oRechercheAffaire.RestaureRecherche(cbbChampRechercheAffaire, rbAffaireFiltre, cbbEmploye, tbDateDeb, tbDateFin)
                    If oRechercheAffaire.RechercheAffaireTri = 0 Then
                        cbbChampRechercheAffaire.SelectedValue = "Client"
                    ElseIf oRechercheAffaire.RechercheAffaireTri = 1 Then
                        cbbChampRechercheAffaire.SelectedValue = "Type"
                    ElseIf oRechercheAffaire.RechercheAffaireTri = 2 Then
                        cbbChampRechercheAffaire.SelectedValue = "BU"
                    End If

                    lblNoAffaire.Text = ""
                    LoadTreeView()
                End If
        Else
            oRechercheAffaire = New RechercheAffaire
            oRechercheAffaire.SaveRecherche(cbbChampRechercheAffaire.SelectedIndex, rbAffaireFiltre.SelectedIndex, cbbEmploye.SelectedIndex, cbbEmploye.SelectedValue, tbDateDeb.Text, tbDateFin.Text)
            Session("recherche") = oRechercheAffaire
            'If Not (oRechercheAffaire Is Nothing) Then
            '    cbbEmploye.SelectedValue = oRechercheAffaire.RechercheAffaireChargeID
            '    cbbEmploye.SelectedIndex = oRechercheAffaire.RechercheAffaireCharge
            'End If
        End If

        'Gestion des droits
        Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
        Try
            If Not verifDroit(lDroit, eModule.AccesAffaireEcriture) Then
                'Pas de création ni de modif ni de suppr
                ibDelete.Visible = False
                ibModifier.Visible = False
                btnInsererAffaire.Visible = False
            End If
            'Pas de facture
            If Not verifDroit(lDroit, eModule.AccesFacture) Then
                mMenuBouton.FindItem("4").Enabled = False
            End If
        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try

        mMenuBouton.Items(1).Enabled = False





    End Sub

    ''' <summary>
    ''' chargement de la liste des employes
    ''' </summary>
    Protected Sub chargerChargeAffaire()

        Dim oEmployeDAO As CEmployeDAO = New CEmployeDAO
        Dim oEmploye As CEmploye = New CEmploye
        Dim ds As DataSet
        cbbEmploye.Items.Clear()
        oEmploye = CType(Session("Employe"), CEmploye)
        ds = oEmployeDAO.GetAllEmployeToList

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les employés"
        itemDefaut.Value = CStr(-1)
        cbbEmploye.Items.Insert(0, itemDefaut)


        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim cbbText As String = ds.Tables(0).Rows(i)("Employe").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = ds.Tables(0).Rows(i)("EmployeID").ToString
            cbbEmploye.Items.Add(item)
        Next
        cbbEmploye.DataBind()
        cbbEmploye.SelectedValue = "-1"

    End Sub
    
    ''' <summary>
    ''' chargement de l'affaire selecionnee
    ''' </summary>
    ''' <param name="idAffaire">id de l'affaire concernee</param>
    Private Sub LoadAffaire(ByVal idAffaire As Integer)
        lblMsg.Text = ""
        ' remplissage des information générales
        Dim oAffaireDAO As New CAffaireDAO
        Dim oAffaire As CAffaire

        oAffaire = oAffaireDAO.GetAffaire(idAffaire)
        lblAffaireID.Text = CStr(oAffaire.AffaireID)
        tbClient.Text = oAffaire.Client.ClientNom
        tbLibelle.Text = oAffaire.AffaireLibelle
        tbDate.Text = CStr(oAffaire.AffaireDateDeb)
        tbTypeAffaire.Text = oAffaire.TypeAffaire.TypeAffaireLibelle
        tbService.Text = oAffaire.Service.ServiceLibelle
        tbChargeDAffaire.Text = oAffaire.Employe.EmployePrenom & " " & oAffaire.Employe.EmployeNom
        tbCom.Text = oAffaire.AffaireRemarques
        cbTerminee.Checked = oAffaire.AffaireTermine

        ' remplissage des informations budgetaires
        Dim decAffaireBudjet As Decimal = CDec(oAffaire.AffaireBudget)
        Dim decSousBudjet As Decimal = oAffaireDAO.GetBudgetTotSousAffaire(idAffaire)
        'Dim decBudjet As Decimal = +decSousBudjet

        ' Si l'affaire possède au moins une sous-affaire, le budget de cette affaire
        ' est égale à la somme des budgets des sous-affaires
        If oAffaireDAO.GetListeSousAffaire(idAffaire).Tables(0).Rows.Count > 0 Then
            tbBudget.Text = CStr(FormatNumber(decSousBudjet, 2))
            tbBudget.Enabled = False
            decAffaireBudjet = decSousBudjet
        Else
            tbBudget.Text = CStr(FormatNumber(decAffaireBudjet, 2))
        End If

        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim decConsoHT As Decimal = CDec(oProduitAffaireDAO.GetProduitAffaireAssocieBudget(idAffaire))
        tbFraisConso.Text = CStr(FormatNumber(oProduitAffaireDAO.GetProduitAffaireFrais(idAffaire), 2))
        tbHTconso.Text = CStr(FormatNumber((decConsoHT), 2))
        tbPrestRest.Text = CStr(FormatNumber((decAffaireBudjet - decConsoHT), 2))
        tbNbJoursPasses.Text = CStr(FormatNumber(oProduitAffaireDAO.GetProduitAffaireNbJours(idAffaire), 3))
        Try
            tbCtMoyen.Text = CStr(FormatNumber(decConsoHT / CDec(tbNbJoursPasses.Text), 3))
        Catch ex As DivideByZeroException
            tbCtMoyen.Text = "0,00"
        End Try

        'chargement la liste des qualifications
        chargerListeAffaireQualif(idAffaire)

        'chargement des etapes de facturation
        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO

        Dim iTypeAffaire As Integer = oAffaireDAO.GetAffaireTypeByAffaireID(idAffaire)

        If (iTypeAffaire = eTypeAffaire.Forfait) Then
            'affichage du taux d'avancement
            chargerListeEtapeFacturePourcentage(idAffaire)
            gvEtapesFactuPourcentage.Visible = True
            tblMois.Visible = False
        ElseIf iTypeAffaire = eTypeAffaire.ContratCadre Or iTypeAffaire = eTypeAffaire.Regie Or iTypeAffaire = eTypeAffaire.Recurrent Then
            'affichage du mois
            For i As Integer = 1 To 12
                CType(tblMois.FindControl("cbMois" & i), CheckBox).Checked = oAffaireEtapeFactureDAO.IsAffaireFactureMois(idAffaire, i)
            Next

            tblMois.Visible = True
            gvEtapesFactuPourcentage.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' chargement du treeview
    ''' </summary>
    Private Sub LoadTreeView()

        tvAffaires.Nodes.Clear()
        Dim oRechercheAffaire As New RechercheAffaire
        oRechercheAffaire = CType(Session("recherche"), RechercheAffaire)
        Dim iEmployeID As Integer
        '-----------------------------------------------------------------------
        If oRechercheAffaire.RechercheAffaireChargeID = "" Then
            oRechercheAffaire.RechercheAffaireChargeID = "-1"
            cbbEmploye.SelectedValue = "-1"
        End If
        '-----------------------------------------------------------------------
        If CInt(oRechercheAffaire.RechercheAffaireChargeID) <> -1 Then
            iEmployeID = CInt(cbbEmploye.SelectedValue)
        Else
            iEmployeID = -1
        End If
        If iEmployeID = 0 Then iEmployeID = -1

        If (cbbChampRechercheAffaire.SelectedValue = "Client") Then
            LoadTreeViewClient(iEmployeID)

        ElseIf (cbbChampRechercheAffaire.SelectedValue = "Type") Then
            LoadTreeViewTypeAffaire(iEmployeID)
        ElseIf (cbbChampRechercheAffaire.SelectedValue = "BU") Then
            LoadTreeViewBU(iEmployeID)
        End If


    End Sub

    ''' <summary>
    ''' charge le treeview en fonction des types d'affaire
    ''' </summary>
    Public Sub LoadTreeViewTypeAffaire(ByVal iEmployeID As Integer)

        Dim oAffaireDAO As New CAffaireDAO
        Dim dateDeb As Date = New Date
        Dim dateFin As Date = New Date
        Dim dateDebSql As String = ""
        Dim dateFinSql As String = ""
        Dim daystr As String = ""
        Dim monthstr As String = ""
        Dim daystrFin As String = ""
        Dim monthstrFin As String = ""

        If tbDateDeb.Text <> "" And tbDateDeb.Text <> "" Then
            dateDeb = CDate(tbDateDeb.Text)
            dateFin = CDate(tbDateFin.Text)

            If dateDeb.Day < 10 Then
                daystr = "0" & dateDeb.Day
            Else
                daystr = "" & dateDeb.Day
            End If

            If dateDeb.Month < 10 Then
                monthstr = "0" & dateDeb.Month
            Else
                monthstr = "" & dateDeb.Month
            End If

            If dateFin.Day < 10 Then
                daystrFin = "0" & dateFin.Day
            Else
                daystrFin = "" & dateFin.Day
            End If

            If dateFin.Month < 10 Then
                monthstrFin = "0" & dateFin.Month
            Else
                monthstrFin = "" & dateFin.Month
            End If

            dateDebSql = "" & dateDeb.Year & "-" & monthstr & "-" & daystr

            dateFinSql = "" & dateFin.Year & "-" & monthstrFin & "-" & daystrFin

        End If

        Dim tnTypeAffaire, tnClient, tnAffaire As New Obout.Ajax.UI.TreeView.Node

        Dim oDataSet As DataSet = oAffaireDAO.GetAffaireParTypeAffaire(rbAffaireFiltre.SelectedIndex, iEmployeID, dateDebSql, dateFinSql)
        Dim idTypeAffaire As Integer = -1
        Dim iClientID As Integer = -1
        Dim iAffaireID As Integer = -1
        Dim bBool As Boolean = False

        If oDataSet.Tables(0).Rows.Count = 0 Then
            lblNoAffaire.Text = "Aucune affaire ne correspond à votre recherche"

        End If
        For i = 0 To oDataSet.Tables(0).Rows.Count - 1
            If Not idTypeAffaire = CInt(oDataSet.Tables(0).Rows(i)("TypeAffaireID")) Then
                tnTypeAffaire = New Obout.Ajax.UI.TreeView.Node
                tnTypeAffaire.Value = CStr(oDataSet.Tables(0).Rows(i)("TypeAffaireID"))
                tnTypeAffaire.Text = CStr(oDataSet.Tables(0).Rows(i)("TypeAffaireLibelle"))
                tnTypeAffaire.ImageUrl = "~/App_Themes/Axone/Design/folder.png"

                tvAffaires.Nodes.Add(tnTypeAffaire)
                bBool = True
                idTypeAffaire = CInt(oDataSet.Tables(0).Rows(i)("TypeAffaireID"))
            End If

            If Not (iClientID = CInt(oDataSet.Tables(0).Rows(i)("ClientID"))) Or bBool Then
                tnClient = New Obout.Ajax.UI.TreeView.Node()
                tnClient.Value = CStr(oDataSet.Tables(0).Rows(i)("ClientID"))
                tnClient.Text = CStr(oDataSet.Tables(0).Rows(i)("ClientNom"))
                tnClient.ImageUrl = "~/App_Themes/Axone/Design/folder.png"

                tnTypeAffaire.ChildNodes.Add(tnClient)
                tnTypeAffaire.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand

                iClientID = CInt(oDataSet.Tables(0).Rows(i)("ClientID"))
            End If

            If Not iAffaireID = CInt(oDataSet.Tables(0).Rows(i)("AffaireID")) Then
                tnAffaire = New Obout.Ajax.UI.TreeView.Node()
                tnAffaire.Value = CStr(oDataSet.Tables(0).Rows(i)("AffaireID"))
                tnAffaire.Text = CStr(oDataSet.Tables(0).Rows(i)("AffaireLibelle"))
                Dim decSommeProduits As Decimal
                If Not IsDBNull(oDataSet.Tables(0).Rows(i)("AffaireSommeProduits")) Then
                    decSommeProduits = CDec(oDataSet.Tables(0).Rows(i)("AffaireSommeProduits"))
                Else
                    decSommeProduits = 0
                End If

                If CBool(oDataSet.Tables(0).Rows(i)("AffaireTermine")) Then
                    If decSommeProduits > CDec(oDataSet.Tables(0).Rows(i)("AffaireBudget")) Then
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_terminee_depassement.png"
                    Else
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_terminee.png"
                    End If
                    tnAffaire.CssClass = "nodeAffaireTerminee"
                Else
                    If decSommeProduits > CDec(oDataSet.Tables(0).Rows(i)("AffaireBudget")) Then
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_Depassement.png"
                    Else
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire.png"
                    End If
                End If

                tnClient.ChildNodes.Add(tnAffaire)
                tnClient.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand

                iAffaireID = CInt(oDataSet.Tables(0).Rows(i)("AffaireID"))
            End If
            bBool = False
        Next

        '-----------------------------------------------------------------------------------------------------------

        lblCompteurAffaire.Text = ""

        For Each nTypeAffaire As Obout.Ajax.UI.TreeView.Node In tvAffaires.Nodes

            Dim nFilsNoeudContratsCadres As Obout.Ajax.UI.TreeView.NodeCollection = nTypeAffaire.ChildNodes
            lblCompteurAffaire.Text &= nFilsNoeudContratsCadres.Count & " client(s)"
            Dim iNbAffaire As Integer = 0

            For Each nodeAffaire As Obout.Ajax.UI.TreeView.Node In nFilsNoeudContratsCadres
                iNbAffaire += nodeAffaire.ChildNodes.Count
            Next

            lblCompteurAffaire.Text &= " et " & iNbAffaire & " affaire(s) trouvé(s) pour les " & nTypeAffaire.Text & " <br />"
        Next
        '-----------------------------------------------------------------------------------------------------------


    End Sub

    ''' <summary>
    ''' charge le treeview en fonction des Services
    ''' </summary>
    Public Sub LoadTreeViewBU(ByVal iEmployeID As Integer)

        Dim oAffaireDAO As New CAffaireDAO
        Dim dateDeb As Date = New Date
        Dim dateFin As Date = New Date
        Dim dateDebSql As String = ""
        Dim dateFinSql As String = ""
        Dim daystr As String = ""
        Dim monthstr As String = ""
        Dim daystrFin As String = ""
        Dim monthstrFin As String = ""

        If tbDateDeb.Text <> "" And tbDateDeb.Text <> "" Then
            dateDeb = CDate(tbDateDeb.Text)
            dateFin = CDate(tbDateFin.Text)

            If dateDeb.Day < 10 Then
                daystr = "0" & dateDeb.Day
            Else
                daystr = "" & dateDeb.Day
            End If


            If dateDeb.Month < 10 Then
                monthstr = "0" & dateDeb.Month
            Else
                monthstr = "" & dateDeb.Month
            End If


            If dateFin.Day < 10 Then
                daystrFin = "0" & dateFin.Day
            Else
                daystrFin = "" & dateFin.Day
            End If


            If dateFin.Month < 10 Then
                monthstrFin = "0" & dateFin.Month
            Else
                monthstrFin = "" & dateFin.Month
            End If

            dateDebSql = "" & dateDeb.Year & "-" & monthstr & "-" & daystr

            dateFinSql = "" & dateFin.Year & "-" & monthstrFin & "-" & daystrFin


        End If

        Dim tnService, tnClient, tnAffaire As New Obout.Ajax.UI.TreeView.Node

        Dim oDataSet As DataSet = oAffaireDAO.GetAffaireParService(rbAffaireFiltre.SelectedIndex, iEmployeID, dateDebSql, dateFinSql)
        Dim idService As Integer = -1
        Dim iClientID As Integer = -1
        Dim iAffaireID As Integer = -1

        If oDataSet.Tables(0).Rows.Count = 0 Then
            lblNoAffaire.Text = "Aucune affaire ne correspond à votre recherche"

        End If

        Dim bBool As Boolean = False

        For i = 0 To oDataSet.Tables(0).Rows.Count - 1
            If Not idService = CInt(oDataSet.Tables(0).Rows(i)("ServiceID")) Then
                tnService = New Obout.Ajax.UI.TreeView.Node
                tnService.Value = CStr(oDataSet.Tables(0).Rows(i)("ServiceID"))
                tnService.Text = CStr(oDataSet.Tables(0).Rows(i)("ServiceLibelle"))
                tnService.ImageUrl = "~/App_Themes/Axone/Design/folder.png"

                tvAffaires.Nodes.Add(tnService)

                idService = CInt(oDataSet.Tables(0).Rows(i)("ServiceID"))
                bBool = True
            End If

            If Not iClientID = CInt(oDataSet.Tables(0).Rows(i)("ClientID")) Or bBool Then
                tnClient = New Obout.Ajax.UI.TreeView.Node()
                tnClient.Value = CStr(oDataSet.Tables(0).Rows(i)("ClientID"))
                tnClient.Text = CStr(oDataSet.Tables(0).Rows(i)("ClientNom"))
                tnClient.ImageUrl = "~/App_Themes/Axone/Design/folder.png"

                tnService.ChildNodes.Add(tnClient)
                tnService.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand

                iClientID = CInt(oDataSet.Tables(0).Rows(i)("ClientID"))
            End If

            If Not iAffaireID = CInt(oDataSet.Tables(0).Rows(i)("AffaireID")) Then
                tnAffaire = New Obout.Ajax.UI.TreeView.Node()
                tnAffaire.Value = CStr(oDataSet.Tables(0).Rows(i)("AffaireID"))
                tnAffaire.Text = CStr(oDataSet.Tables(0).Rows(i)("AffaireLibelle"))
                Dim decSommeProduits As Decimal
                If Not IsDBNull(oDataSet.Tables(0).Rows(i)("AffaireSommeProduits")) Then
                    decSommeProduits = CDec(oDataSet.Tables(0).Rows(i)("AffaireSommeProduits"))
                Else
                    decSommeProduits = 0
                End If

                If CBool(oDataSet.Tables(0).Rows(i)("AffaireTermine")) Then
                    If decSommeProduits > CDec(oDataSet.Tables(0).Rows(i)("AffaireBudget")) Then
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_terminee_depassement.png"
                    Else
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_terminee.png"
                    End If
                    tnAffaire.CssClass = "nodeAffaireTerminee"
                Else
                    If decSommeProduits > CDec(oDataSet.Tables(0).Rows(i)("AffaireBudget")) Then
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_Depassement.png"
                    Else
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire.png"
                    End If
                End If

                tnClient.ChildNodes.Add(tnAffaire)
                tnClient.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand

                iAffaireID = CInt(oDataSet.Tables(0).Rows(i)("AffaireID"))
            End If
            bBool = False
        Next

        '-----------------------------------------------------------------------------------------------------------
        lblCompteurAffaire.Text = ""

        For Each nodeBU As Obout.Ajax.UI.TreeView.Node In tvAffaires.Nodes

            Dim nFilsNoeudContratsCadres As Obout.Ajax.UI.TreeView.NodeCollection = nodeBU.ChildNodes

            lblCompteurAffaire.Text &= nFilsNoeudContratsCadres.Count & " client(s)"
            Dim iNbAffaire As Integer = 0

            For Each nodeAffaire As Obout.Ajax.UI.TreeView.Node In nFilsNoeudContratsCadres
                iNbAffaire += nodeAffaire.ChildNodes.Count
            Next

            lblCompteurAffaire.Text &= " et " & iNbAffaire & " affaire(s) trouvé(s) pour le service " & nodeBU.Text & " <br />"
        Next
    End Sub

    ''' <summary>
    ''' charge le treeview en fonction des clients
    ''' </summary>
    Public Sub LoadTreeViewClient(ByVal iEmployeID As Integer)

        Dim oAffaireDAO As New CAffaireDAO

        Dim tnClient, tnAffaire As New Obout.Ajax.UI.TreeView.Node

        Dim dateDeb As Date = New Date
        Dim dateFin As Date = New Date
        Dim dateDebSql As String = ""
        Dim dateFinSql As String = ""
        Dim daystr As String = ""
        Dim monthstr As String = ""
        Dim daystrFin As String = ""
        Dim monthstrFin As String = ""

        If tbDateDeb.Text <> "" And tbDateDeb.Text <> "" Then
            dateDeb = CDate(tbDateDeb.Text)
            dateFin = CDate(tbDateFin.Text)

            If dateDeb.Day < 10 Then
                daystr = "0" & dateDeb.Day
            Else
                daystr = "" & dateDeb.Day
            End If


            If dateDeb.Month < 10 Then
                monthstr = "0" & dateDeb.Month
            Else
                monthstr = "" & dateDeb.Month
            End If


            If dateFin.Day < 10 Then
                daystrFin = "0" & dateFin.Day
            Else
                daystrFin = "" & dateFin.Day
            End If


            If dateFin.Month < 10 Then
                monthstrFin = "0" & dateFin.Month
            Else
                monthstrFin = "" & dateFin.Month
            End If

            dateDebSql = "" & dateDeb.Year & "-" & monthstr & "-" & daystr

            dateFinSql = "" & dateFin.Year & "-" & monthstrFin & "-" & daystrFin


        End If

        Dim oDataSet As DataSet = oAffaireDAO.GetAffaireParClient(rbAffaireFiltre.SelectedIndex, iEmployeID, dateDebSql, dateFinSql)

        If oDataSet.Tables(0).Rows.Count = 0 And iEmployeID <> -1 And dateDebSql <> "" And dateFinSql <> "" Then
            lblNoAffaire.Text = "Pas d'affaire pour cet employé sur cette période"
        ElseIf oDataSet.Tables(0).Rows.Count = 0 And iEmployeID <> -1 Then
            lblNoAffaire.Text = "Pas d'affaire pour cet employé"
        ElseIf oDataSet.Tables(0).Rows.Count = 0 And iEmployeID = -1 And dateDebSql <> "" And dateFinSql <> "" Then
            lblNoAffaire.Text = "Pas d'affaire sur cette période"
        ElseIf oDataSet.Tables(0).Rows.Count = 0 Then
            lblNoAffaire.Text = "Aucune affaire ne correspond à votre recherche"
        End If

        Dim iClientID As Integer = -1
        Dim iAffaireID As Integer = -1

        For i = 0 To oDataSet.Tables(0).Rows.Count - 1
            If Not iClientID = CInt(oDataSet.Tables(0).Rows(i)("ClientID")) Then
                tnClient = New Obout.Ajax.UI.TreeView.Node()
                tnClient.Value = CStr(oDataSet.Tables(0).Rows(i)("ClientID"))
                tnClient.Text = CStr(oDataSet.Tables(0).Rows(i)("ClientNom"))
                tnClient.ImageUrl = "~/App_Themes/Axone/Design/folder.png"

                tvAffaires.Nodes.Add(tnClient)

                iClientID = CInt(oDataSet.Tables(0).Rows(i)("ClientID"))
            End If

            If Not iAffaireID = CInt(oDataSet.Tables(0).Rows(i)("AffaireID")) Then
                tnAffaire = New Obout.Ajax.UI.TreeView.Node()
                tnAffaire.Value = CStr(oDataSet.Tables(0).Rows(i)("AffaireID"))
                tnAffaire.Text = CStr(oDataSet.Tables(0).Rows(i)("AffaireLibelle"))
                Dim decSommeProduits As Decimal
                If Not IsDBNull(oDataSet.Tables(0).Rows(i)("AffaireSommeProduits")) Then
                    decSommeProduits = CDec(oDataSet.Tables(0).Rows(i)("AffaireSommeProduits"))
                Else
                    decSommeProduits = 0
                End If

                If CBool(oDataSet.Tables(0).Rows(i)("AffaireTermine")) Then
                    If decSommeProduits > CDec(oDataSet.Tables(0).Rows(i)("AffaireBudget")) Then
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_terminee_depassement.png"
                    Else
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_terminee.png"
                    End If
                    tnAffaire.CssClass = "nodeAffaireTerminee"
                Else
                    If decSommeProduits > CDec(oDataSet.Tables(0).Rows(i)("AffaireBudget")) Then
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire_Depassement.png"
                    Else
                        tnAffaire.ImageUrl = "~/App_Themes/ComptaAna/Design/Icon_affaire.png"
                    End If
                End If

                tnClient.ChildNodes.Add(tnAffaire)
                tnClient.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand

                iAffaireID = CInt(oDataSet.Tables(0).Rows(i)("AffaireID"))
            End If

        Next

        lblCompteurAffaire.Text = tvAffaires.Nodes.Count & " client(s)"
        Dim iNbAffaire As Integer = 0

        For Each nodeAffaire As Obout.Ajax.UI.TreeView.Node In tvAffaires.Nodes
            iNbAffaire += nodeAffaire.ChildNodes.Count
        Next

        lblCompteurAffaire.Text &= " et " & iNbAffaire & " affaire(s) trouvé(s)"
    End Sub



    ''' <summary>
    ''' chargement de l'affaire quand elle est selectionnee dans le treeview
    ''' </summary>
    Protected Sub tvAffaires_SelectedTreeNodeChanged(ByVal sender As Object, ByVal e As Obout.Ajax.UI.TreeView.NodeEventArgs) Handles tvAffaires.SelectedTreeNodeChanged

        If Not (tvAffaires.SelectedNode.Value = "") Then
            fs1.Visible = True
            fs2.Visible = True
            fs3.Visible = True
            fs4.Visible = True
            LoadAffaire(CInt(tvAffaires.SelectedNode.Value))
            mMenuBouton.Visible = True

        End If


    End Sub


    ''' <summary>
    ''' chargement des qualifications en fonction de l'affaire selectionnee dans le treeview
    ''' </summary>
    Protected Sub gvQualifAffaire_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        chargerListeAffaireQualif(CInt(tvAffaires.SelectedNode.Value))
    End Sub

    ''' <summary>
    ''' chargement de la liste des qualifications
    ''' </summary>
    ''' <param name="idAffaire">id de l'affaire concernee</param>
    Protected Sub chargerListeAffaireQualif(ByVal idAffaire As Integer)
        ' chargement des qualifications
        Dim oAffaireQualificationDAO As New CAffaireQualificationDAO
        Dim oAffaireDAO As New CAffaireDAO
        Dim ds As DataSet

        ds = oAffaireQualificationDAO.GetAffaireQualification(idAffaire)

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
    ''' chargement des etapes de facturation en fonction de l'affaire selectionnee dans le treeview
    ''' </summary>
    Protected Sub gvEtapesFactu_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        chargerListeEtapeFacturePourcentage(CInt(tvAffaires.SelectedNode.Value))
    End Sub

    ''' <summary>
    ''' chargement des etapes de facturation en pourcentage
    ''' </summary>
    ''' <param name="idAffaire">id de l'affaire concernee</param>
    ''' <remarks></remarks>
    Protected Sub chargerListeEtapeFacturePourcentage(ByVal idAffaire As Integer)

        Dim oAffaireEtapeFactureDAO As New CAffaireEtapeFactureDAO
        Dim ds As DataSet

        ds = oAffaireEtapeFactureDAO.GetAffaireEtapeFacturePourcentage(idAffaire)

        gvEtapesFactuPourcentage.DataSource = ds
        gvEtapesFactuPourcentage.DataBind()
    End Sub

    ' ''' <summary>
    ' ''' charge le treeview en fonction de la selection de la liste
    ' ''' </summary>
    'Protected Sub btnRecherche_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRecherche.Click

    '    lblNoAffaire.Text = ""
    '    LoadTreeView()

    'End Sub

    Private Sub tblMois_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblMois.DataBinding
        Dim oAffaireEtapeFacturationDAO As New CAffaireEtapeFactureDAO
        Dim dsMoisValide As DataSet

        Try
            dsMoisValide = oAffaireEtapeFacturationDAO.GetMoisValidation(CInt(tvAffaires.SelectedNode.Value))
        Catch ex As NullReferenceException
            dsMoisValide = oAffaireEtapeFacturationDAO.GetMoisValidation(CInt(Request.QueryString("affaire")))
        End Try

        Dim iNbRowMois As Integer = dsMoisValide.Tables(0).Rows.Count

        For i = 0 To iNbRowMois - 1
            Try
                Dim iMois As Integer = CInt(dsMoisValide.Tables(0).Rows(i)("EtapeFactureMois"))
                Dim idEtape As Integer = CInt(dsMoisValide.Tables(0).Rows(i)("AffaireEtapeFactureID"))

                Dim casecocher As String = "cbMois" + iMois.ToString
                Dim ctrlCheckbox As Control = tblMois.FindControl(casecocher)
                If Not IsNothing(ctrlCheckbox) Then
                    Dim checkbox As CheckBox = CType(ctrlCheckbox, CheckBox)
                    checkbox.Checked = True
                End If

                Dim lblMois As String = "lblMois" + iMois.ToString
                Dim ctrlLbl As Control = tblMois.FindControl(lblMois)
                If Not IsNothing(ctrlLbl) Then
                    Dim Label As Label = CType(ctrlLbl, Label)
                    Label.Text = idEtape.ToString
                End If
            Catch ex As InvalidCastException

            End Try
        Next
    End Sub

    Private Sub mMenuBouton_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mMenuBouton.MenuItemClick
        'If e.Item.Value = "0" Then
        '    Response.Redirect("AffaireInserer.aspx")
        'Else
        'If e.Item.Value = "1" Then
        '    If Not lblAffaireID.Text = "" Then
        '        Response.Redirect("AffaireModifier.aspx?id=" & lblAffaireID.Text)
        '    End If
        'ElseIf e.Item.Value = "2" Then

        '    pPopupAlerteSuppression.Visible = True


        'Else
        If e.Item.Value = "3" Then
            Try
                Response.Redirect("AffaireProduits.aspx?id=" & CInt(tvAffaires.SelectedNode.Value))
            Catch ex As NullReferenceException
                lblMsg.Text = "Vous n'avez pas encore sélectionné d'affaire."
            End Try
        ElseIf e.Item.Value = "4" Then
            Try
                If Not CConfiguration.NouvelleVersion Then
                    Response.Redirect("~/Gestion/Affaire/AffaireFacture.aspx?id=" & CInt(tvAffaires.SelectedNode.Value))
                Else
                    Response.Redirect("~/Gestion/Affaire/AffaireFacturation.aspx?id=" & CInt(tvAffaires.SelectedNode.Value))
                End If

            Catch ex As NullReferenceException
                lblMsg.Text = "Vous n'avez pas encore sélectionné d'affaire."
            End Try
        ElseIf e.Item.Value = "5" Then
            Try
                Response.Redirect("AffaireSousAffaireListe.aspx?affaire=" & CInt(tvAffaires.SelectedNode.Value))
            Catch ex As NullReferenceException
                lblMsg.Text = "Vous n'avez pas encore sélectionné d'affaire."
            End Try
        End If
    End Sub

    Private Sub validationSuppression() Handles btnContinuer.Click

        pPopupAlerteSuppression.Visible = False

        Try
            Dim oAffaireDAO As New CAffaireDAO
            Dim iSupprimer As Integer = oAffaireDAO.DeleteAffaire(CInt(tvAffaires.SelectedNode.Value))
            If iSupprimer = 0 Then
                lblMsg.Text = "Suppression effectuée!"
                fs1.Visible = False
                fs2.Visible = False
                fs3.Visible = False
                fs4.Visible = False

                tbClient.Text = ""
                tbLibelle.Text = ""
                cbTerminee.Checked = False
                tbDate.Text = ""
                tbTypeAffaire.Text = ""
                tbService.Text = ""
                tbChargeDAffaire.Text = ""
                tbCom.Text = ""
                tbBudget.Text = ""
                tbHTconso.Text = ""
                tbPrestRest.Text = ""
                tbFraisConso.Text = ""
                tbNbJoursPasses.Text = ""
                tbCtMoyen.Text = ""
                gvQualifAffaire.Visible = False
                gvEtapesFactuPourcentage.Visible = False
                tblMois.Visible = False
                LoadTreeView()

                LoadTreeViewClient(-1)
            ElseIf iSupprimer = 1 Then
                lblMsg.Text = "Suppression échouée, l'affaire possède au moins un produit!"
            End If

        Catch ex As NullReferenceException
            lblMsg.Text = "Vous n'avez pas encore sélectionné d'affaire."
        End Try


    End Sub

    Private Sub annulationSuppression() Handles btnAnnuler.Click
        pPopupAlerteSuppression.Visible = False
    End Sub

    Private Sub btnInsererAffaire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsererAffaire.Click
        Response.Redirect("AffaireInserer.aspx")
    End Sub

    ''' <summary>
    ''' permet de réinitialiser les champs de la recherche
    ''' </summary>
    Private Sub ButtonReinit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReinit.Click
        lblNoAffaire.Text = ""
        cbbChampRechercheAffaire.SelectedIndex = 0
        cbbChampRechercheAffaire.SelectedValue = "Client"
        rbAffaireFiltre.SelectedIndex = 4
        chargerChargeAffaire()
        ' cbbEmploye.SelectedIndex = -1
        cbbEmploye.SelectedValue = "-1"
        tbDateDeb.Text = ""
        tbDateFin.Text = ""
        cDateDeb.SelectedDate = Nothing
        cDateFin.SelectedDate = Nothing
        Dim oRechercheAffaire As New RechercheAffaire

        oRechercheAffaire.SaveRecherche(cbbChampRechercheAffaire.SelectedIndex, rbAffaireFiltre.SelectedIndex, cbbEmploye.SelectedIndex, cbbEmploye.SelectedValue, tbDateDeb.Text, tbDateFin.Text)

        Session("recherche") = oRechercheAffaire
        tvAffaires.Nodes.Clear()
        LoadTreeView()
    End Sub

    Private Sub ibValider_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibValider.Click
        If (tbDateDeb.Text = "" And tbDateFin.Text = "") Or Not (tbDateDeb.Text = "" Or tbDateFin.Text = "") Then
            lblNoAffaire.Text = ""
            'Dim oRechercheAffaire As New RechercheAffaire
            'oRechercheAffaire.SaveRecherche(cbbChampRechercheAffaire.SelectedIndex, rbAffaireFiltre.SelectedIndex, cbbEmploye.SelectedIndex, cbbEmploye.SelectedValue, tbDateDeb.Text, tbDateFin.Text)
            'Session("recherche") = oRechercheAffaire
            lblPeriodeIncorrete.Visible = False
            LoadTreeView()
            fs1.Visible = False
            fs2.Visible = False
            fs3.Visible = False
            fs4.Visible = False
            mMenuBouton.Visible = False
        Else
            cDateDeb.SelectedDate = Nothing
            lblPeriodeIncorrete.Visible = True
        End If
    End Sub

    Private Sub ibModifier_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibModifier.Click

        If Not lblAffaireID.Text = "" Then
            Response.Redirect("AffaireModifier.aspx?id=" & lblAffaireID.Text)
        End If


    End Sub

    Private Sub ibDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibDelete.Click
        pPopupAlerteSuppression.Visible = True
    End Sub

    Private Sub ibExporter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExporter.Click
        Dim oAffaire As New CAffaireDAO
        Dim listeAffaires As New ArrayList
        Dim iaffaireID As Integer


        'Colonnes de la feuille excel
        Dim iColLibelle As Integer = 0
        Dim iColClient As Integer = 1
        Dim iColEmploye As Integer = 2
        Dim iColBudget As Integer = 3
        Dim iColSommePdt As Integer = 4
        Dim iColDateDeb As Integer = 5



        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture")

        ws.Cells(0, 1).Value = "Liste des affaires au " & DateCouranteToString()
        ws.Cells(0, 1).Style.Font.Weight = 1000
        ws.Cells(0, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center


        ' Affichage de l'en-tête
        ws.Cells(2, iColLibelle).Value = "Libellé affaire"
        ws.Cells(2, iColLibelle).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(2, iColLibelle).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColClient).Value = "Client"
        ws.Cells(2, iColClient).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(2, iColClient).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColEmploye).Value = "Charger d'affaire"
        ws.Cells(2, iColEmploye).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(2, iColEmploye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColBudget).Value = "Budget affaire"
        ws.Cells(2, iColBudget).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(2, iColBudget).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColSommePdt).Value = "Somme produits"
        ws.Cells(2, iColSommePdt).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(2, iColSommePdt).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDateDeb).Value = "Date début"
        ws.Cells(2, iColDateDeb).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(2, iColDateDeb).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center


        For i As Integer = 0 To tvAffaires.Nodes.Count - 1
            For j As Integer = 0 To tvAffaires.Nodes(i).ChildNodes.Count - 1
                iaffaireID = CInt(tvAffaires.Nodes(i).ChildNodes(j).Value)
                Dim affaire As CAffaire = oAffaire.GetAffaire(iaffaireID)
                listeAffaires.Add(affaire)
            Next
        Next

        For i As Integer = 0 To listeAffaires.Count - 1
            Dim affaire As CAffaire = CType(listeAffaires(i), CAffaire)
            ws.Cells(i + 3, iColLibelle).Value = affaire.AffaireLibelle
            ws.Cells(i + 3, iColLibelle).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColLibelle).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColClient).Value = affaire.Client.ClientNom
            ws.Cells(i + 3, iColClient).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColClient).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColEmploye).Value = affaire.Employe.EmployeNom & " " & affaire.Employe.EmployePrenom
            ws.Cells(i + 3, iColEmploye).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColEmploye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColBudget).Value = affaire.AffaireBudget
            ws.Cells(i + 3, iColBudget).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColBudget).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColSommePdt).Value = affaire.AffaireSommeProduits
            ws.Cells(i + 3, iColSommePdt).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColSommePdt).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColDateDeb).Value = "" & affaire.AffaireDateDeb.Day & "/" & affaire.AffaireDateDeb.Month & "/" & affaire.AffaireDateDeb.Year
            ' ws.Cells(i + 3, iColDateDeb).Formula = "=TEXT(""" & affaire.AffaireDateDeb & """, ""dd/mm/yyyy"")"
            ws.Cells(i + 3, iColDateDeb).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColDateDeb).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        Next

        ws.Columns(iColLibelle).Width = 30 * 256
        ws.Columns(iColLibelle).AutoFit()
        ws.Columns(iColClient).Width = 30 * 256
        ws.Columns(iColClient).AutoFit()
        ws.Columns(iColEmploye).Width = 30 * 256
        ws.Columns(iColEmploye).AutoFit()
        ws.Columns(iColBudget).Width = 30 * 256
        ws.Columns(iColBudget).AutoFit()
        ws.Columns(iColBudget).Style.NumberFormat = "Standard"
        ws.Columns(iColSommePdt).Width = 30 * 256
        ws.Columns(iColSommePdt).AutoFit()
        ws.Columns(iColSommePdt).Style.NumberFormat = "Standard"
        ws.Columns(iColDateDeb).Width = 30 * 256
        ws.Columns(iColDateDeb).AutoFit()

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "Affaires" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)

    End Sub
End Class
