
Imports ComptaAna.Business
Imports Obout.ComboBox
Imports GemBox.Spreadsheet
Imports Telerik.Charting
Imports ComptaAna.net.Droit
Imports System.Drawing


Public Class StatPrimesAugmentations
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

        Else

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesStatsCoutSalaires) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
           
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
    ''' Control du bouton; lancer la recherche
    ''' </summary> 
    Public Sub btnRechercheStat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRechercheStat.Click
        If cbbFiltreStatGeneral.SelectedValue = "Employe" Then
            ChargerStatsPrimeAugmentationParEmploye()
        ElseIf cbbFiltreStatGeneral.SelectedValue = "Service" Then
            ChargerStatsPrimeAugmentationParService()
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
    ''' remplir le combobox dans le header et mettre les couleurs
    ''' </summary>
    Protected Sub gvStat_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim oTypeAffaireDAO As CTypeAffaireDAO = New CTypeAffaireDAO

        If (e.Row.RowType = DataControlRowType.Header) Then
            
        Else
            If e.Row.RowType = DataControlRowType.DataRow Then
              
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

    Private Sub ChargerStatsPrimeAugmentationParEmploye()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim employeId As Integer = -1
        Dim iServiceID As Integer = -1

        If Not (cbbFiltreStatGalDetail.SelectedText = " --- Employe --- ") Then
            employeId = CInt(cbbFiltreStatGalDetail.SelectedValue)
        End If

        Dim ds As DataSet = oStatistiquesDAO.GetStatPrime(dDateDeb, dDateFin, employeId, iServiceID)
        Dim dsAug As DataSet = oStatistiquesDAO.GetStatAugmentation(dDateDeb, dDateFin, employeId, iServiceID)
        gvPrime.DataSource = ds
        gvPrime.DataBind()
        gvPrime.Visible = True
        lblPrime.Visible = True
        lblTotalPrimes.Visible = True
        tbTotalPrimes.Visible = True
        tbTotalPrimes.Text = CStr(CalculerTotalPrimes())
        gvAugmentation.DataSource = dsAug
        gvAugmentation.DataBind()
        gvAugmentation.Visible = True
        lblAug.Visible = True
        lblTxMoyen.Visible = True
        tbTxMoyen.Visible = True
        tbTxMoyen.Text = CStr(CalculerTauxMoyen(dsAug))
    End Sub

    Private Sub ChargerStatsPrimeAugmentationParService()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim employeId As Integer = -1
        Dim iServiceID As Integer = -1

        If Not (cbbFiltreStatGalDetail.SelectedText = " --- Service --- ") Then
            iServiceID = CInt(cbbFiltreStatGalDetail.SelectedValue)
        End If

        Dim ds As DataSet = oStatistiquesDAO.GetStatPrime(dDateDeb, dDateFin, employeId, iServiceID)
        Dim dsAug As DataSet = oStatistiquesDAO.GetStatAugmentation(dDateDeb, dDateFin, employeId, iServiceID)
        gvPrime.DataSource = ds
        gvPrime.DataBind()
        gvPrime.Visible = True
        lblPrime.Visible = True
        lblTotalPrimes.Visible = True
        tbTotalPrimes.Visible = True
        tbTotalPrimes.Text = CStr(CalculerTotalPrimes())
        gvAugmentation.DataSource = dsAug
        gvAugmentation.DataBind()
        gvAugmentation.Visible = True
        lblAug.Visible = True
        lblTxMoyen.Visible = True
        tbTxMoyen.Visible = True
        tbTxMoyen.Text = CStr(CalculerTauxMoyen(dsAug))
    End Sub


    Private Function CalculerTotalPrimes() As Decimal
        Dim dTotalPrime As Decimal = 0
        If Not gvPrime.Rows.Count = 0 Then
            For i As Integer = 0 To gvPrime.Rows.Count - 1
                Dim sPrime As String = gvPrime.Rows(i).Cells(4).Text
                dTotalPrime += CDec(sPrime)
            Next
        End If

        Return dTotalPrime
    End Function

    Private Function CalculerTauxMoyen(ByVal dsDataset As DataSet) As Double
        Dim dSommeTx As Decimal = 0
        Dim dTxMoyen As Decimal = 0
        Dim Somme As Double = 0
        Dim dsAug As DataSet = dsDataset
        Dim dsData As New DataSet
        'Ajout de la table taux d'augmentation
        dsData.Tables.Add("TauxAugementation")
        'Ajout des colonnes
        dsData.Tables("TauxAugementation").Columns.Add("Nom", GetType(String))
        dsData.Tables("TauxAugementation").Columns.Add("Somme", GetType(Double))
        dsData.Tables("TauxAugementation").Columns.Add("NbAugmentation", GetType(Double))
        For Each row As DataRow In dsDataset.Tables(0).Rows
            If Contient(dsData, CStr(row(2))) Then
                Dim i As Integer = indice(dsData, CStr(row(2)))
                dsData.Tables("TauxAugementation").Rows(i)(1) = CDbl(dsData.Tables("TauxAugementation").Rows(i)(1)) * (CDbl(row(5)))
                dsData.Tables("TauxAugementation").Rows(i)(2) = CInt(dsData.Tables("TauxAugementation").Rows(i)(2)) + 1
                'dsData.Tables("TauxAugementation").Rows(i)(0) = row(2)
                'Dim monNombreCoupe() As String
                'monNombreCoupe = Split(CStr(row(5)), ",", 2)
                'If CInt(dsData.Tables("TauxAugementation").Rows(i)(1)) < 1 Then
                '    dsData.Tables("TauxAugementation").Rows(i)(1) = CDbl(dsData.Tables("TauxAugementation").Rows(i)(1)) + 1
                'End If
                'dsData.Tables("TauxAugementation").Rows(i)(1) = CDbl(dsData.Tables("TauxAugementation").Rows(i)(1)) + (CType("0," + monNombreCoupe(1), Double))
                '


            Else
                dsData.Tables("TauxAugementation").Rows.Add(row(2), CType(row(5), Double), 1)
            End If
        Next

        If Not dsData.Tables(0).Rows.Count = 0 Then
            For Each row As DataRow In dsData.Tables(0).Rows
                Somme += CDbl(row(1))
            Next
            Somme = Somme - dsData.Tables(0).Rows.Count
            'For i As Integer = 0 To gvAugmentation.Rows.Count - 1
            '    Dim sPrime As String = gvAugmentation.Rows(i).Cells(5).Text
            '    dSommeTx += CDec(sPrime)
            'Next

            'dTxMoyen = CDec(dSommeTx / gvAugmentation.Rows.Count)
        End If
        Return CDbl(FormatNumber(((Somme / dsData.Tables(0).Rows.Count) + 1), 3))
        'Return dTxMoyen
    End Function
 
    Private Function Contient(ByVal dsData As DataSet, ByVal str As String) As Boolean
        For Each row As DataRow In dsData.Tables(0).Rows
            If CStr(row(0)) = str Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function indice(ByVal dsData As DataSet, ByVal str As String) As Integer
        Dim I As Integer = 0
        For Each row As DataRow In dsData.Tables(0).Rows
            If CStr(row(0)) = str Then
                Return I
            End If
            I = I + 1
        Next
        Return I
    End Function

    Private Sub btnEXportPrimeAugmentation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEXportPrimeAugmentation.Click
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim employeId As Integer = -1
        Dim iServiceID As Integer = -1

        If cbbFiltreStatGeneral.SelectedValue = "Employe" Then

            If Not (cbbFiltreStatGalDetail.SelectedText = " --- Employe --- ") Then
                employeId = CInt(cbbFiltreStatGalDetail.SelectedValue)
            End If

        ElseIf cbbFiltreStatGeneral.SelectedValue = "Service" Then

            If Not (cbbFiltreStatGalDetail.SelectedText = " --- Service --- ") Then
                iServiceID = CInt(cbbFiltreStatGalDetail.SelectedValue)
            End If
        End If

        Dim ds As DataSet = oStatistiquesDAO.GetStatPrime(dDateDeb, dDateFin, employeId, iServiceID)
        Dim dsAug As DataSet = oStatistiquesDAO.GetStatAugmentation(dDateDeb, dDateFin, employeId, iServiceID)
        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("PrimeAugmentation")
        'colonne augmentation
        Dim iColEmploye As Integer = 0
        Dim iColMontant As Integer = 1
        Dim iColTauxAugmentation As Integer = 2
        Dim iColDate As Integer = 4
        'colonne Prime
        Dim iColEmployePrime As Integer = 0
        Dim iColMontantPrime As Integer = 1
        Dim iColAvantageNature As Integer = 2
        Dim iColModelVoiture As Integer = 3
        Dim iColDatePrime As Integer = 4
        'ligne
        Dim iLigEntete = 4
        Dim iLigEspacement1 = 1
        Dim iLigEspacement2 = 2
        Dim iLigEspacement4 = 4

        ' titre
        ws.Cells(0, 1).Value = "Liste des augmentations"
        ws.Cells(0, 1).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(0, 1).Style.Font.Weight = 1000
        ws.Cells(0, 1).Style.Font.Size = 200
        ws.Cells(0, 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(147, 201, 255), Drawing.Color.FromArgb(147, 201, 255))
        ws.Cells(0, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        ws.Cells.GetSubrangeAbsolute(0, 0, 0, 4).Merged = True
        ' période afficher
        ws.Cells(2, 1).Value = "du" & tbDateDeb.Text & " au " & tbDateFin.Text
        ws.Cells(2, 1).Style.Font.Size = 200
        ws.Cells(2, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, 1).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Cells.GetSubrangeAbsolute(2, 0, 2, 4).Merged = True

        ' Affichage de l'en-tête
        ws.Cells(iLigEntete, iColEmploye).Value = "Nom et Prénom"
        ws.Cells(iLigEntete, iColEmploye).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete, iColEmploye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColEmploye).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete, iColEmploye).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColEmploye).Style.Font.Weight = 1000
        ws.Cells(iLigEntete, iColMontant).Value = "Montant"
        ws.Cells(iLigEntete, iColMontant).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete, iColMontant).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColMontant).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete, iColMontant).Style.Font.Weight = 1000
        ws.Cells(iLigEntete, iColMontant).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColTauxAugmentation).Value = "Taux d'augmentation"
        ws.Cells(iLigEntete, iColTauxAugmentation).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete, iColTauxAugmentation).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColTauxAugmentation).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete, iColTauxAugmentation).Style.Font.Weight = 1000
        ws.Cells(iLigEntete, iColTauxAugmentation).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColDate - 1).Value = ""
        ws.Cells(iLigEntete, iColDate - 1).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete, iColDate - 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColDate - 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete, iColDate - 1).Style.Font.Weight = 1000
        ws.Cells(iLigEntete, iColDate - 1).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColDate).Value = "Date"
        ws.Cells(iLigEntete, iColDate).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete, iColDate).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete, iColDate).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete, iColDate).Style.Font.Weight = 1000
        ws.Cells(iLigEntete, iColDate).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Rows(2).Height = 2 * 256

        'Parcours du dataset concernant les augmentations
        Dim iLigDecalage As Integer = 0
        For i As Integer = 0 To dsAug.Tables(0).Rows.Count - 1

            ' Alternance par ligne de 2 niveaux de jaunes
            If i Mod 2 = 0 Then
                ws.Cells(i + iLigEntete + iLigEspacement1, iColEmploye).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, iColMontant).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, iColTauxAugmentation).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, 3).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, iColDate).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
            Else
                ws.Cells(i + iLigEntete + iLigEspacement1, iColEmploye).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, iColMontant).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, iColTauxAugmentation).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, 3).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(i + iLigEntete + iLigEspacement1, iColDate).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
            End If
            'valeur des Cellules
            ws.Cells(i + iLigEntete + iLigEspacement1, iColEmploye).Value = dsAug.Tables(0).Rows(i)("Employe")
            ws.Cells(i + iLigEntete + iLigEspacement1, iColMontant).Value = FormatNumber(dsAug.Tables(0).Rows(i)("EmployeCoutCout"), 2)
            ws.Cells(i + iLigEntete + iLigEspacement1, iColTauxAugmentation).Value = FormatNumber(dsAug.Tables(0).Rows(i)("EmployeCoutTaux"), 2)
            ws.Cells(i + iLigEntete + iLigEspacement1, iColDate).Value = dsAug.Tables(0).Rows(i)("EmployeCoutDateDebut")
            'mise en forme
            ws.Cells(i + iLigEntete + iLigEspacement1, iColEmploye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + iLigEntete + iLigEspacement1, iColMontant).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + iLigEntete + iLigEspacement1, iColTauxAugmentation).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + iLigEntete + iLigEspacement1, iColDate).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

            ws.Cells(i + iLigEntete + iLigEspacement1, iColEmploye).SetBorders(MultipleBorders.Left, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + iLigEntete + iLigEspacement1, iColDate).SetBorders(MultipleBorders.Right, Drawing.Color.DarkBlue, LineStyle.Thin)
            iLigDecalage += 1
        Next
        'bordure du bas de tableau
        ws.Cells(iLigDecalage + iLigEntete, iColEmploye).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage + iLigEntete, iColMontant).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage + iLigEntete, iColTauxAugmentation).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage + iLigEntete, 3).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage + iLigEntete, iColDate).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)

        iLigDecalage += 1
        'Affichage du taux moyen d'augmentation
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement1, iColEmploye).Value = "Taux Moyen d'Augmentation"
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement1, iColEmploye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement1, iColEmploye).Style.Font.Weight = 1000

        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement1, iColMontant).Value = CStr(CalculerTauxMoyen(dsAug))
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement1, iColMontant).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.DashDot)
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement1, iColMontant).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        iLigDecalage += 1
        'Deuxième tableau
        ' titre
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement2, 1).Value = "Liste des Primes"
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement2, 1).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement2, 1).Style.Font.Weight = 1000
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement2, 1).Style.Font.Size = 200
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement2, 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(147, 201, 255), Drawing.Color.FromArgb(147, 201, 255))
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement2, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        ws.Cells.GetSubrangeAbsolute(iLigEntete + iLigDecalage + iLigEspacement2, 0, iLigEntete + iLigDecalage + iLigEspacement2, 4).Merged = True

        ' Affichage de l'en-tête
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColEmployePrime).Value = "Nom et Prénom"
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColEmployePrime).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColEmployePrime).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColEmployePrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColEmployePrime).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColEmployePrime).Style.Font.Weight = 1000
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColMontantPrime).Value = "Montant"
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColMontantPrime).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColMontantPrime).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColMontantPrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColMontantPrime).Style.Font.Weight = 1000
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColMontantPrime).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColAvantageNature).Value = "Avantage Nature"
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColAvantageNature).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColAvantageNature).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColAvantageNature).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColAvantageNature).Style.Font.Weight = 1000
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColAvantageNature).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColModelVoiture).Value = "Modèle"
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColModelVoiture).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColModelVoiture).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColModelVoiture).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColModelVoiture).Style.Font.Weight = 1000
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColModelVoiture).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColDatePrime).Value = "Date"
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColDatePrime).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColDatePrime).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColDatePrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColDatePrime).Style.Font.Weight = 1000
        ws.Cells(iLigEntete + iLigDecalage + iLigEspacement4, iColDatePrime).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        iLigDecalage += 1

        'parcours du dataset concernant les primes
        Dim iLigDecalage2 As Integer = iLigDecalage

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

            ' Alternance par ligne de 2 niveaux de jaunes
            If i Mod 2 = 0 Then
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColEmployePrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColMontantPrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColAvantageNature).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColModelVoiture).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColDatePrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 190), Drawing.Color.Beige)
            Else
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColEmployePrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColMontantPrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColAvantageNature).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColModelVoiture).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColDatePrime).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 104), Drawing.Color.Beige)
            End If
            'valeur des cellules
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColEmployePrime).Value = ds.Tables(0).Rows(i)("Employe")
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColMontantPrime).Value = FormatNumber(ds.Tables(0).Rows(i)("EmployePrimeMontant"), 2)
            If CInt(ds.Tables(0).Rows(i)("EmployePrimeTypeAvNature")) = 1 Then
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColAvantageNature).Value = "Oui"
            Else
                ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColAvantageNature).Value = "Non"
            End If
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColModelVoiture).Value = ds.Tables(0).Rows(i)("EmployePrimeModeleAvNature")
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColDatePrime).Value = ds.Tables(0).Rows(i)("EmployePrimeDate")

            ' mise en forme
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColEmployePrime).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColMontantPrime).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColAvantageNature).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColModelVoiture).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColDatePrime).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColEmployePrime).SetBorders(MultipleBorders.Left, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(iLigDecalage + i + iLigEntete + iLigEspacement4, iColDatePrime).SetBorders(MultipleBorders.Right, Drawing.Color.DarkBlue, LineStyle.Thin)
            iLigDecalage2 += 1
        Next
        iLigDecalage2 += 1
        'bordure du bas de tableau
        ws.Cells(iLigDecalage2 + iLigEntete + 2, iColEmployePrime).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage2 + iLigEntete + 2, iColMontantPrime).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage2 + iLigEntete + 2, iColAvantageNature).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage2 + iLigEntete + 2, iColModelVoiture).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)
        ws.Cells(iLigDecalage2 + iLigEntete + 2, iColDatePrime).SetBorders(MultipleBorders.Bottom, Drawing.Color.DarkBlue, LineStyle.Thin)

        'Affichage du total des primes
        ws.Cells(iLigEntete + iLigDecalage2 + iLigEspacement4, iColEmploye).Value = "Total Des Primes"
        ws.Cells(iLigEntete + iLigDecalage2 + iLigEspacement4, iColEmploye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(iLigEntete + iLigDecalage2 + iLigEspacement4, iColEmploye).Style.Font.Weight = 1000

        ws.Cells(iLigEntete + iLigDecalage2 + iLigEspacement4, iColMontant).Value = CalculerTotalPrimesDataSet(ds)
        ws.Cells(iLigEntete + iLigDecalage2 + iLigEspacement4, iColMontant).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.DashDot)
        ws.Cells(iLigEntete + iLigDecalage2 + iLigEspacement4, iColMontant).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        'Agrandissement des cellules pour qu'elles s'ajustent à la taille des données qu'elle contiennent
        ws.Columns(iColEmployePrime).Width = 30 * 256
        ws.Columns(iColEmployePrime).AutoFit()
        ws.Columns(iColMontantPrime).Width = 30 * 256
        ws.Columns(iColMontantPrime).AutoFit()
        ws.Columns(iColAvantageNature).Width = 25 * 256
        ws.Columns(iColAvantageNature).AutoFit()
        ws.Columns(iColModelVoiture).Width = 30 * 256
        ws.Columns(iColModelVoiture).AutoFit()
        ws.Columns(iColDatePrime).Width = 30 * 256
        ws.Columns(iColDatePrime).AutoFit()

        'Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "Prime_Augmentation" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)
    End Sub
    Private Function CalculerTotalPrimesDataSet(ByVal ds As DataSet) As Decimal

        Dim dTotalPrime As Decimal = 0
        If Not ds.Tables(0).Rows.Count = 0 Then

            For Each Row As DataRow In ds.Tables(0).Rows

                dTotalPrime += CDec(Row("EmployePrimeMontant"))
            Next
        End If

        Return dTotalPrime
    End Function
End Class