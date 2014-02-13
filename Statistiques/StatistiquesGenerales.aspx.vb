Imports ComptaAna.Business
Imports Obout.ComboBox
Imports GemBox.Spreadsheet
Imports Telerik.Charting
Imports ComptaAna.net.Droit
Imports System.Drawing


Public Class StatitiquesGenerales
    Inherits System.Web.UI.Page

    Dim dDateDeb As Date
    Dim dDateFin As Date
    Dim n1 As Boolean
    Dim n2 As Boolean
    Dim bAfficherRAVides As Boolean
    Dim colorRAIncomplet As Drawing.Color = Drawing.Color.FromArgb(255, 55, 55)
    Dim colorDepassement As Drawing.Color = Drawing.Color.FromArgb(58, 241, 159)
    '  FromArgb(230, 0, 115)

    ''' <summary>
    ''' Chargement de la page, effectue les differentes recherches en fonction des filtres selectionnes
    ''' </summary> 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack Then
            Try
                dDateDeb = CDate(tbDateDeb.Text)
                dDateFin = CDate(tbDateFin.Text)
            Catch ex As InvalidCastException
            End Try

            n1 = cbn1.Checked
            n2 = cbn2.Checked
           
        Else

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesStatsGenerales) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            'cbbChampRechercheAffaire.SelectedIndex = 0
            'cbbChampRechercheAffaire.SelectedValue = "GLOBAL"
            ChargerCBBEmploye()

        End If



        If tbDateFin.Text = "" Then

            Dim dat = ReleveActivite.GetLastDayOfThisMonth
            tbDateFin.Text = CStr(dat).Substring(0, 10)

        End If


        If tbDateDeb.Text = "" Then
            If DateTime.Now.Month < 10 Then
                tbDateDeb.Text = "01/0" & DateTime.Now.Month & "/" & DateTime.Now.Year
            Else
                tbDateDeb.Text = "01/" & DateTime.Now.Month & "/" & DateTime.Now.Year
            End If

        End If




        lblLegendeDepassement.ForeColor = colorDepassement
        lblLegendeRAIncomplet.BackColor = colorRAIncomplet
    End Sub

    Public Sub ChargerCBBEmploye()

        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        '  cbbFiltreStatGeneral.SelectedIndex = 0
        cbbFiltreStatGalDetail.Items.Clear()
        Dim dsEmploye As DataSet = oStatistiquesDAO.GetAllEmploye(-1)
        cbbFiltreStatGalDetail.EmptyText = " --- Employe --- "

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les employés"
        itemDefaut.Value = CStr(-1)
        cbbFiltreStatGalDetail.Items.Insert(0, itemDefaut)

        For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsEmploye.Tables(0).Rows(i)("EmployeName").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = dsEmploye.Tables(0).Rows(i)("EmployeID").ToString
            cbbFiltreStatGalDetail.Items.Add(item)
        Next
        cbbFiltreStatGalDetail.DataBind()
    End Sub
    Public Sub ChargerCBBService()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO

        cbbFiltreStatGalDetail.Items.Clear()
        Dim dsEmploye As DataSet = oStatistiquesDAO.GetAllService()
        cbbFiltreStatGalDetail.EmptyText = " --- Service --- "

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les services"
        itemDefaut.Value = CStr(-1)
        cbbFiltreStatGalDetail.Items.Insert(0, itemDefaut)

        For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsEmploye.Tables(0).Rows(i)("ServiceLibelle").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = dsEmploye.Tables(0).Rows(i)("ServiceID").ToString
            cbbFiltreStatGalDetail.Items.Add(item)
        Next
        cbbFiltreStatGalDetail.DataBind()


    End Sub
    ''' <summary>
    ''' Effectue la recherche des statistiques generales par employe et met a jour le gridview correspondant
    ''' </summary> 
    Public Sub ChargerStatsParEmployes()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim employeId As Integer

        gvStatGenerales.Columns.Item(1).HeaderText = "Employé"
        ' La colonne Outils n'est pas utilisée
        'gvStatGenerales.Columns.Item(12).Visible = False
        gvStatGenerales.Columns.Item(5).Visible = False
        If Not n1 And Not n2 Then
            gvStatGenerales.Columns.Item(2).Visible = False
        Else
            gvStatGenerales.Columns.Item(2).Visible = True
        End If

        If (cbbFiltreStatGalDetail.SelectedText = " --- Employe --- ") Then
            employeId = -1
        Else
            employeId = CInt(cbbFiltreStatGalDetail.SelectedValue)
        End If

        Dim ds As DataSet = oStatistiquesDAO.SelectStatGeneralesParEmploye(dDateDeb, dDateFin, n1, n2, employeId, -1, -1, "", False)
        gvStatGenerales.DataSource = ds
        gvStatGenerales.DataBind()

    End Sub

    ''' <summary>
    ''' Effectue la recherche des statistiques generales par services et met a jour le gridview correspondant
    ''' </summary> 
    Public Sub ChargerStatsParServices()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim serviceId As Integer

        gvStatGenerales.Columns.Item(1).HeaderText = "Service"

        ' La colonne Outils est utilisée
        gvStatGenerales.Columns.Item(5).Visible = True

        If Not n1 And Not n2 Then
            gvStatGenerales.Columns.Item(2).Visible = False
        Else
            gvStatGenerales.Columns.Item(2).Visible = True
        End If

        If (cbbFiltreStatGalDetail.SelectedText = " --- Service --- ") Then
            serviceId = -1
        Else
            serviceId = CInt(cbbFiltreStatGalDetail.SelectedValue)
        End If

        Dim ds As DataSet = oStatistiquesDAO.SelectStatGeneralesParService(dDateDeb, dDateFin, n1, n2, serviceId, -1, -1, "")

        gvStatGenerales.DataSource = ds
        gvStatGenerales.DataBind()

    End Sub

    ''' <summary>
    ''' Control du bouton; lancer la recherche
    ''' </summary> 
    Public Sub btnRechercheStat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRechercheStat.Click
        If cbbFiltreStatGeneral.SelectedValue = "Employe" Then
            ChargerStatsParEmployes()
        ElseIf cbbFiltreStatGeneral.SelectedValue = "Service" Then
            ChargerStatsParServices()
        End If
    End Sub

    ''' <summary>
    ''' control du combobox du premier filtre de recherche
    ''' </summary> 
    Public Sub cbbFiltreStatGeneral_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cbbFiltreStatGeneral.SelectedIndexChanged
        If cbbFiltreStatGeneral.SelectedValue = "Service" Then
            ChargerCBBService()
        Else
            ChargerCBBEmploye()
        End If

    End Sub

    ''' <summary>
    ''' control du combobox dans le header
    ''' </summary> 
    Public Sub cbbFiltreHeader_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim ds As DataSet
        Dim odv As New DataView
        Dim idCouleur As Integer = 0
        Dim employeId, serviceId As Integer

        If cbbFiltreStatGeneral.SelectedValue = "Employe" Then
            Dim myCtrl As Control = gvStatGenerales.HeaderRow.FindControl("cbbFiltreHeader")

            If Not IsNothing(myCtrl) Then
                Dim comboboxHeader As ComboBox = CType(myCtrl, ComboBox)
                Dim produitID As Integer = CInt(comboboxHeader.SelectedValue)

                If (cbbFiltreStatGalDetail.SelectedText = " --- Employe --- ") Then
                    employeId = -1
                Else
                    employeId = CInt(cbbFiltreStatGalDetail.SelectedValue)
                End If

                ' On sauvegarde en session ces 3 éléments pour les récupérer pour l'export Excel
                Session("StatsGeneraleSelectedIndex") = comboboxHeader.SelectedIndex
                Session("StatsGeneraleProduitId") = produitID
                Session("StatsGeneraleSelectedText") = comboboxHeader.SelectedText

                ds = oStatistiquesDAO.SelectStatGeneralesParEmploye(dDateDeb, dDateFin, n1, n2, employeId, comboboxHeader.SelectedIndex, produitID, comboboxHeader.SelectedText, False)

                gvStatGenerales.DataSource = ds
                gvStatGenerales.DataBind()
            End If

        ElseIf cbbFiltreStatGeneral.SelectedValue = "Service" Then
            Dim myCtrl As Control = gvStatGenerales.HeaderRow.FindControl("cbbFiltreHeader")

            If Not IsNothing(myCtrl) Then
                Dim comboboxHeader As ComboBox = CType(myCtrl, ComboBox)
                Dim produitID As Integer = CInt(comboboxHeader.SelectedValue)

                If (cbbFiltreStatGalDetail.SelectedText = " --- Service --- ") Then
                    serviceId = -1
                Else
                    serviceId = CInt(cbbFiltreStatGalDetail.SelectedValue)
                End If

                ' On sauvegarde en session ces 3 éléments pour les récupérer pour l'export Excel
                Session("StatsGeneraleSelectedIndex") = comboboxHeader.SelectedIndex
                Session("StatsGeneraleProduitId") = produitID
                Session("StatsGeneraleSelectedText") = comboboxHeader.SelectedText

                ds = oStatistiquesDAO.SelectStatGeneralesParService(dDateDeb, dDateFin, n1, n2, serviceId, comboboxHeader.SelectedIndex, produitID, comboboxHeader.SelectedText)
                gvStatGenerales.DataSource = ds
                gvStatGenerales.DataBind()
            End If

        End If
    End Sub

    ''' <summary>
    ''' remplir le combobox dans le header et mettre les couleurs
    ''' </summary>
    Protected Sub gvStat_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim oTypeAffaireDAO As CTypeAffaireDAO = New CTypeAffaireDAO

        If (e.Row.RowType = DataControlRowType.Header) Then
            Dim myCtrl As Control = e.Row.FindControl("cbbFiltreHeader")
            If Not IsNothing(myCtrl) Then
                Dim comboboxHeader As ComboBox = CType(myCtrl, ComboBox)

                comboboxHeader.EmptyText = " --- Plus d'info --- "
                Dim itemDefaut As New ComboBoxItem
                itemDefaut.Text = "Aucune précision"
                itemDefaut.Value = CStr(-1)
                comboboxHeader.Items.Add(itemDefaut)

                Dim itemTypeProduit As New ComboBoxItem
                itemTypeProduit.Attributes.Add("style", "color: blue;")
                itemTypeProduit.Text = "Type de produit"
                itemTypeProduit.Value = CStr(-1)
                itemTypeProduit.Enabled = False
                comboboxHeader.Items.Add(itemTypeProduit)

                'charger la liste des types de produits non facturables
                Dim dsTypeProduit As DataSet = oStatistiquesDAO.GetTypeProduitNonFacturable()
                For i = 0 To dsTypeProduit.Tables(0).Rows.Count - 1
                    Dim item As New ComboBoxItem
                    item.Text = dsTypeProduit.Tables(0).Rows(i)("TypeProduitLibelle").ToString
                    item.Value = dsTypeProduit.Tables(0).Rows(i)("TypeProduitID").ToString
                    comboboxHeader.Items.Add(item)
                Next

                Dim itemTypeAffaire As New ComboBoxItem
                itemTypeAffaire.Attributes.Add("style", "color: blue;")
                itemTypeAffaire.Text = "Type d'affaire"
                itemTypeAffaire.Value = CStr(-1)
                itemTypeAffaire.Enabled = False
                comboboxHeader.Items.Add(itemTypeAffaire)

                'charger la liste des types d'affaire
                Dim dsTypeAffaire As DataSet = oTypeAffaireDAO.GetAllTypeAffaire(True)
                For i = 0 To dsTypeAffaire.Tables(0).Rows.Count - 1
                    Dim item As New ComboBoxItem
                    item.Text = dsTypeAffaire.Tables(0).Rows(i)("TypeAffaireLibelle").ToString
                    item.Value = dsTypeAffaire.Tables(0).Rows(i)("TypeAffaireID").ToString
                    comboboxHeader.Items.Add(item)
                Next

                comboboxHeader.DataBind()
            End If
        Else
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim iID As Integer = CInt(drv("ID"))
                Dim iAnnee As Integer = CInt(drv("Annee"))
                Dim iDepassement As Integer = CInt(drv("Depassement"))
                Dim iDepJour As Integer = CInt(drv("NbJoursDep"))
                Dim iNbJoursRA As Integer = CInt(drv("NbJoursRA"))
                Dim iNbJoursTotal As Integer = CInt(drv("lNbJoursOuvres"))

                'alerte en bleu lorsqu'il y a depassement
                If Not (iDepassement = 0) Then
                    e.Row.Cells(e.Row.Cells.Count - 2).ForeColor = colorDepassement
                    e.Row.Cells(e.Row.Cells.Count - 2).Font.Bold = True
                End If

                If Not (iDepJour = 0) Then
                    e.Row.Cells(e.Row.Cells.Count - 1).ForeColor = colorDepassement
                    e.Row.Cells(e.Row.Cells.Count - 1).Font.Bold = True
                End If

                'colorer les lignes
                If cbbFiltreStatGalDetail.SelectedValue = "" Or cbbFiltreStatGalDetail.SelectedValue = CStr(-1) Then
                    If n1 Or n2 Then
                        'si on a cocher la case n1/n2, on met la couleur sur l'annee courante
                        If iAnnee = DatePart("yyyy", dDateFin) Then
                            e.Row.BackColor = Drawing.Color.FromArgb(229, 243, 255)
                        End If

                    Else
                        'If e.Row.RowIndex Mod 2 = 1 Then
                        '    e.Row.BackColor = Drawing.Color.FromArgb(238, 247, 255)
                        'End If

                    End If
                End If

                ' On surligne le nom de l'employé s'il n'a pas rempli entièrement sa compta
                lblLegendeRAIncomplet.BackColor = colorRAIncomplet
                If iNbJoursRA < iNbJoursTotal Then
                    For i As Integer = 1 To e.Row.Cells.Count - 1
                        e.Row.Cells(i).BackColor = colorRAIncomplet
                    Next
                End If

            End If
        End If
    End Sub

    Private Sub btnExporter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExporter.Click
        If cbbFiltreStatGeneral.SelectedValue = "Employe" Then
            ExporterStatsParEmployes()
        ElseIf cbbFiltreStatGeneral.SelectedValue = "Service" Then
            ExporterStatsParServices()
        End If
    End Sub

    Private Sub ExporterStatsParEmployes()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim employeId As Integer
        Dim iColEmploye As Integer = 0
        Dim iColAnnee As Integer = 1
        Dim iColTauxFactu As Integer = 2
        Dim iColCAHT As Integer = 3
        Dim iColNbJoursCA As Integer = 4
        Dim iColNbJoursRA As Integer = 5
        Dim iColNbJoursTotal As Integer = 6
        Dim iColNbJoursAbs As Integer = 7
        Dim iColDetail As Integer = 8
        Dim iColDepassement As Integer = 9
        Dim iColJoursDep As Integer = 10

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Statistiques générales")

        If (cbbFiltreStatGalDetail.SelectedText = " --- Employe --- ") Then
            employeId = -1
        Else
            employeId = CInt(cbbFiltreStatGalDetail.SelectedValue)
        End If

        ' Affichage de l'en-tête
        ws.Cells(0, iColEmploye).Value = "Employé"
        ws.Cells(0, iColAnnee).Value = "Année"
        ws.Cells(0, iColTauxFactu).Value = "Taux de factu"
        ws.Cells(0, iColCAHT).Value = "CA HT"
        ws.Cells(0, iColNbJoursCA).Value = "Nb Jours CA"
        ws.Cells(0, iColNbJoursRA).Value = "Nb Jours RA"
        ws.Cells(0, iColNbJoursTotal).Value = "Nb Jours Total"
        ws.Cells(0, iColNbJoursAbs).Value = "Nb Jours Abs"
        ws.Cells(0, iColDetail).Value = "Détail"
        ws.Cells(0, iColDepassement).Value = "Dépassement"
        ws.Cells(0, iColJoursDep).Value = "Jours Dép"

        Dim iIndex As Integer = -1
        Dim iId As Integer = -1
        Dim sLibelle As String = ""

        If Not IsNothing(Session("StatsGeneraleSelectedIndex")) Then
            iIndex = CInt(Session("StatsGeneraleSelectedIndex"))
            Session("StatsGeneraleSelectedIndex") = Nothing
        End If
        If Not IsNothing(Session("StatsGeneraleProduitId")) Then
            iId = CInt(Session("StatsGeneraleProduitId"))
            Session("StatsGeneraleProduitId") = Nothing
        End If
        If Not IsNothing(Session("StatsGeneraleSelectedText")) Then
            sLibelle = Session("StatsGeneraleSelectedText").ToString()
            Session("StatsGeneraleSelectedText") = Nothing
        End If

        ' Affichage des données
        Dim ds As DataSet = oStatistiquesDAO.SelectStatGeneralesParEmploye(dDateDeb, dDateFin, n1, n2, employeId, iIndex, iId, sLibelle, False)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            With ds.Tables(0).Rows(i)
                ws.Cells(i + 1, iColEmploye).Value = .Item("Objet")
                ws.Cells(i + 1, iColAnnee).Value = .Item("Annee")
                ws.Cells(i + 1, iColTauxFactu).Value = .Item("TauxFactu")
                ws.Cells(i + 1, iColTauxFactu).Style.NumberFormat = "#0.##%"
                ws.Cells(i + 1, iColCAHT).Value = .Item("CAHT")
                ws.Cells(i + 1, iColNbJoursCA).Value = .Item("NbJoursCA")
                ws.Cells(i + 1, iColNbJoursRA).Value = .Item("NbJoursRA")
                ws.Cells(i + 1, iColNbJoursTotal).Value = CInt(.Item("lNbJoursOuvres"))
                ws.Cells(i + 1, iColNbJoursAbs).Value = .Item("NbJoursAbsents")
                ws.Cells(i + 1, iColDetail).Value = .Item("JourDetail")
                ws.Cells(i + 1, iColDepassement).Value = .Item("Depassement")
                ws.Cells(i + 1, iColJoursDep).Value = .Item("NbJoursDep")
            End With

        Next

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "StatsGeneralesParEmploye" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)

    End Sub

    Private Sub ExporterStatsParServices()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim serviceId As Integer
        Dim iColEmploye As Integer = 0
        Dim iColAnnee As Integer = 1
        Dim iColTauxFactu As Integer = 2
        Dim iColCAHT As Integer = 3
        Dim iColNbJoursCA As Integer = 4
        Dim iColNbJoursRA As Integer = 5
        Dim iColNbJoursTotal As Integer = 6
        Dim iColNbJoursAbs As Integer = 7
        Dim iColDetail As Integer = 8
        Dim iColDepassement As Integer = 9
        Dim iColJoursDep As Integer = 10
        Dim iColOutils As Integer = 11

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Statistiques générales")

        If (cbbFiltreStatGalDetail.SelectedText = " --- Service --- ") Then
            serviceId = -1
        Else
            serviceId = CInt(cbbFiltreStatGalDetail.SelectedValue)
        End If

        ' Affichage de l'en-tête
        ws.Cells(0, iColEmploye).Value = "Service"
        ws.Cells(0, iColAnnee).Value = "Année"
        ws.Cells(0, iColTauxFactu).Value = "Taux de factu"
        ws.Cells(0, iColCAHT).Value = "CA HT"
        ws.Cells(0, iColNbJoursCA).Value = "Nb Jours CA"
        ws.Cells(0, iColNbJoursRA).Value = "Nb Jours RA"
        ws.Cells(0, iColNbJoursTotal).Value = "Nb Jours Total"
        ws.Cells(0, iColNbJoursAbs).Value = "Nb Jours Abs"
        ws.Cells(0, iColDetail).Value = "Détail"
        ws.Cells(0, iColDepassement).Value = "Dépassement"
        ws.Cells(0, iColJoursDep).Value = "Jours Dép"
        ws.Cells(0, iColOutils).Value = "Outils"

        Dim iIndex As Integer = -1
        Dim iId As Integer = -1
        Dim sLibelle As String = ""

        If Not IsNothing(Session("StatsGeneraleSelectedIndex")) Then
            iIndex = CInt(Session("StatsGeneraleSelectedIndex"))
            Session("StatsGeneraleSelectedIndex") = Nothing
        End If
        If Not IsNothing(Session("StatsGeneraleProduitId")) Then
            iId = CInt(Session("StatsGeneraleProduitId"))
            Session("StatsGeneraleProduitId") = Nothing
        End If
        If Not IsNothing(Session("StatsGeneraleSelectedText")) Then
            sLibelle = Session("StatsGeneraleSelectedText").ToString()
            Session("StatsGeneraleSelectedText") = Nothing
        End If

        ' Affichage des données
        Dim ds As DataSet = oStatistiquesDAO.SelectStatGeneralesParService(dDateDeb, dDateFin, n1, n2, serviceId, iIndex, iId, sLibelle)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            With ds.Tables(0).Rows(i)
                ws.Cells(i + 1, iColEmploye).Value = .Item("Objet")
                ws.Cells(i + 1, iColAnnee).Value = .Item("Annee")
                ws.Cells(i + 1, iColTauxFactu).Value = .Item("TauxFactu")
                ws.Cells(i + 1, iColTauxFactu).Style.NumberFormat = "#0.##%"
                ws.Cells(i + 1, iColCAHT).Value = .Item("CAHT")
                ws.Cells(i + 1, iColNbJoursCA).Value = .Item("NbJoursCA")
                ws.Cells(i + 1, iColNbJoursRA).Value = .Item("NbJoursRA")
                ws.Cells(i + 1, iColNbJoursTotal).Value = CInt(.Item("lNbJoursOuvres"))
                ws.Cells(i + 1, iColNbJoursAbs).Value = .Item("NbJoursAbsents")
                ws.Cells(i + 1, iColDetail).Value = .Item("JourDetail")
                ws.Cells(i + 1, iColDepassement).Value = .Item("Depassement")
                ws.Cells(i + 1, iColJoursDep).Value = .Item("NbJoursDep")
                ws.Cells(i + 1, iColOutils).Value = .Item("Outils")
            End With

        Next

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "StatsGeneralesParService" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)

    End Sub

End Class