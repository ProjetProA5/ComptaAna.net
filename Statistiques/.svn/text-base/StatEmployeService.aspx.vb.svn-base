Imports ComptaAna.Business
Imports Obout.ComboBox
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit
Imports Telerik.Charting
Imports System.Drawing

Public Class StatEmployeService
    Inherits System.Web.UI.Page

    Dim dDateDeb As Date
    Dim dDateFin As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesStatsEmployeServiceRex) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
        Else
            Try
                dDateDeb = CDate(tbDateDeb.Text)
                dDateFin = CDate(tbDateFin.Text)
            Catch ex As InvalidCastException
            End Try
        End If

        If tbDateFin.Text = "" Then

            Dim dat = ReleveActivite.GetLastDayOfThisMonth
            tbDateFin.Text = CStr(dat).Substring(0, 10)

        End If


        If tbDateDeb.Text = "" Then
            tbDateDeb.Text = "01/01/" & DateTime.Now.Year
        End If

        chargerCbbEmploye()

    End Sub

    ''' <summary>
    ''' Effectue la recherche des statistiques sur les employés par service et met a jour le gridview
    ''' </summary> 
    Public Sub ChargerStatsEmployesParService()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        Dim odv As New DataView
        Dim idCouleur As Integer = 0
        Dim dsEmploye As DataSet
        Dim dsService As DataSet = oStatistiquesDAO.GetAllService()

        dsEmploye = oStatistiquesDAO.GetAllEmploye(CInt(cbbFiltreStatEmployeService.SelectedValue))

        gvStatEmployeService.Columns.Clear()

        Dim bndEmployeID As New BoundField
        bndEmployeID.DataField = "EmployeID"
        bndEmployeID.HeaderText = "EmployeID"
        bndEmployeID.Visible = False
        gvStatEmployeService.Columns.Add(bndEmployeID)

        Dim bndEmployeNom As New BoundField
        bndEmployeNom.DataField = "EmployeName"
        bndEmployeNom.HeaderText = "Employé"
        gvStatEmployeService.Columns.Add(bndEmployeNom)

        dsEmploye.Tables(0).Columns.Add("CAtotal", Type.GetType("System.String"))
        Dim bndCAtotal As New BoundField
        bndCAtotal.DataField = "CAtotal"
        bndCAtotal.HeaderText = "CA total"
        gvStatEmployeService.Columns.Add(bndCAtotal)

        For Each odr As DataRow In dsService.Tables(0).Rows
            dsEmploye.Tables(0).Columns.Add(odr("ServiceLibelle").ToString, Type.GetType("System.String"))
            dsEmploye.Tables(0).Columns.Add(odr("ServiceLibelle").ToString & "Taux", Type.GetType("System.String"))

            If (cbbFiltreStatEmployeService.SelectedValue = "-1") Then

                For i = 0 To dsEmploye.Tables(0).Rows.Count - 1


                    dsEmploye.Tables(0).Rows(i)(odr("ServiceLibelle").ToString) = oStatistiquesDAO.SelectStatEmployeService(dDateDeb, dDateFin, CInt(dsEmploye.Tables(0).Rows(i)("EmployeID")), CInt(odr("ServiceID")))

                    Try
                        dsEmploye.Tables(0).Rows(i)("CAtotal") = FormatNumber((CDbl(dsEmploye.Tables(0).Rows(i)("CAtotal")) + CDbl(dsEmploye.Tables(0).Rows(i)(odr("ServiceLibelle").ToString))), 2)
                    Catch ex As InvalidCastException
                        dsEmploye.Tables(0).Rows(i)("CAtotal") = FormatNumber(CDbl(dsEmploye.Tables(0).Rows(i)(odr("ServiceLibelle").ToString)), 2)
                    End Try

                Next
            Else

                dsEmploye.Tables(0).Rows(0)(odr("ServiceLibelle").ToString) = oStatistiquesDAO.SelectStatEmployeService(dDateDeb, dDateFin, CInt(cbbFiltreStatEmployeService.SelectedValue), CInt(odr("ServiceID")))

                Try
                    dsEmploye.Tables(0).Rows(0)("CAtotal") = FormatNumber((CDbl(dsEmploye.Tables(0).Rows(0)("CAtotal")) + CDbl(dsEmploye.Tables(0).Rows(0)(odr("ServiceLibelle").ToString))), 2)
                Catch ex As InvalidCastException
                    dsEmploye.Tables(0).Rows(0)("CAtotal") = FormatNumber(CDbl(dsEmploye.Tables(0).Rows(0)(odr("ServiceLibelle").ToString)), 2)
                End Try
            End If

            Dim bndColonne As New BoundField
            bndColonne.DataField = odr("ServiceLibelle").ToString
            bndColonne.HeaderText = odr("ServiceLibelle").ToString
            gvStatEmployeService.Columns.Add(bndColonne)

            Dim bndPourcentage As New BoundField
            bndPourcentage.DataField = odr("ServiceLibelle").ToString & "Taux"
            bndPourcentage.HeaderText = odr("ServiceLibelle").ToString
            gvStatEmployeService.Columns.Add(bndPourcentage)
        Next

        For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
            For j = 4 To dsEmploye.Tables(0).Columns.Count - 1 Step 2
                If Not dsEmploye.Tables(0).Rows(i)("CAtotal").ToString = "" And Not dsEmploye.Tables(0).Rows(i)("CAtotal").ToString = "0,00" Then
                    dsEmploye.Tables(0).Rows(i)(dsEmploye.Tables(0).Columns(j).ColumnName) = FormatNumber((CDbl(dsEmploye.Tables(0).Rows(i)(dsEmploye.Tables(0).Columns(j - 1).ColumnName))) * 100 / CDbl(dsEmploye.Tables(0).Rows(i)("CAtotal")), 2).ToString & " %"
                Else
                    dsEmploye.Tables(0).Rows(i)(dsEmploye.Tables(0).Columns(j).ColumnName) = "0,00 %"
                End If
            Next
        Next

        gvStatEmployeService.DataSource = dsEmploye
        gvStatEmployeService.DataBind()
    End Sub

    ''' <summary>
    ''' charger la liste des employes
    ''' </summary> 
    Public Sub chargerCbbEmploye()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO

        cbbFiltreStatEmployeService.Items.Clear()
        Dim dsCBBEmploye As DataSet = oStatistiquesDAO.GetAllEmploye(-1)

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les employés actifs"
        itemDefaut.Value = "-1"
        cbbFiltreStatEmployeService.Items.Insert(0, itemDefaut)

        For i = 0 To dsCBBEmploye.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsCBBEmploye.Tables(0).Rows(i)("EmployeName").ToString

            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = dsCBBEmploye.Tables(0).Rows(i)("EmployeID").ToString
            cbbFiltreStatEmployeService.Items.Add(item)
        Next

        cbbFiltreStatEmployeService.SelectedIndex = 0
        cbbFiltreStatEmployeService.DataBind()

    End Sub

    ''' <summary>
    ''' ecouteur du bouton rechercher
    ''' </summary> 
    Private Sub btnRechercheStat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRechercheStat.Click
        ChargerStatsEmployesParService()
    End Sub

    ''' <summary>
    ''' ecouteur du bouton Calculer rentabilité pour exporter les resultats
    ''' </summary>
    Protected Sub btnCalcRent_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCalcRent.Click
        ExporterREX()
    End Sub

    Private Sub ExporterREX()
        Dim oServiceDAO As New CServiceDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()
        Dim dsCAServices As DataSet
        Dim dsEmployes As DataSet
        Dim htCAHTEmployes As Hashtable = oStatistiquesDAO.GetCAHTParEmploye(dDateDeb, dDateFin)

        Dim iIndexServices As Integer = 0
        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count

        Dim iColCoutSave As Integer = 2
        Dim iColCoutSoufflot As Integer = 1
        Dim iColCoutAxe As Integer = 0

        Dim iColNomEmploye As Integer = 3
        Dim iLigNomEmploye As Integer = 9

        Dim iLigTotauxServices As Integer = 7
        Dim iLigEnTeteColonne As Integer = 8

        Dim iNbColCout As Integer = 2
        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim wsMain As ExcelWorksheet = ef.Worksheets.Add("Données AXE")
        Dim wsChargesSal As ExcelWorksheet = ef.Worksheets.Add("chargessal")

        ' Parcours des services
        Dim lDureePeriode As Long = DateDiff("m", dDateDeb, dDateFin) + 1
        For i As Integer = 0 To iNbServices - 1
            With dsServices.Tables(0).Rows(i)
                wsChargesSal.Cells(iLigTotauxServices + 1, iColNomEmploye + 1 + iIndexServices).Value = .Item("ServiceLibelle")
                dsCAServices = oStatistiquesDAO.SelectStatGeneralesParService(dDateDeb, dDateFin, False, False, CInt(.Item("ServiceId")), 0, -1, "")
                wsChargesSal.Cells(iLigTotauxServices - 1, iColNomEmploye + 1 + iIndexServices).Value = dsCAServices.Tables(0).Rows(0)("CAHT")
                wsChargesSal.Cells(iLigTotauxServices - 2, iColNomEmploye + 1 + iIndexServices).Value = oProduitAffaireDAO.GetProduitsHorsDepassementBudget(dDateDeb, dDateFin, CLng(.Item("ServiceId")))
                wsChargesSal.Cells(iLigTotauxServices - 3, iColNomEmploye + 1 + iIndexServices).Value = Format(CDbl(IIf(IsDBNull(.Item("ServiceObjectif")), 0, .Item("ServiceObjectif"))) * CDbl(lDureePeriode) / 12, ".00")
                wsChargesSal.Cells(iLigTotauxServices - 4, iColNomEmploye + 1 + iIndexServices).Value = Format(.Item("ServiceObjectif"), ".00")
            End With

            iIndexServices += 1
        Next

        ' Parcours des employés
        dsEmployes = oEmployeDAO.GetNomPrenomEmploye()
        Dim iIndexEmploye As Integer = 0

        For i As Integer = 0 To dsEmployes.Tables(0).Rows.Count - 1
            Dim lEmployeId As Long = CLng(dsEmployes.Tables(0).Rows(i)("EmployeId"))
            Dim iIndexCout As Integer = 0
            Dim dCoutGlobal As Double = 0
            Dim dCoutPeriode As Double = 0
            Dim dsCouts As DataSet
            Dim dTot As Double = 0
            Dim iCellule As Integer = 0
            Dim dTmp As Double = 0

            Dim dCoutAxe As Double
            Dim dCoutSoufflot As Double
            Dim dCoutSave As Double

            dsCouts = oEmployeDAO.GetEmployeCoutPeriode(dDateDeb, dDateFin, lEmployeId)

            If dsCouts.Tables(0).Rows.Count > 0 Then
                'on a au moins un cout dans la liste, donc on ajoute les infos de l'employé
                wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye).Value = dsEmployes.Tables(0).Rows(i)("PrenomNom")

                For j As Integer = 0 To dsCouts.Tables(0).Rows.Count - 1
                    With dsCouts.Tables(0).Rows(j)
                        ' On calcule la date de la rentabilité pour la période trouvée
                        Dim dDateDebutRent As Date
                        Dim dDateFinRent As Date

                        dDateDebutRent = MaxDate(dDateDeb, .Item("EmployeCoutDateDebut"))
                        dDateFinRent = MinDate(dDateFin, .Item("EmployeCoutDateFin"))

                        ' Calcul du coût de la personne sur la période
                        lDureePeriode = DateDiff(DateInterval.Month, dDateDebutRent, dDateFinRent) + 1
                        dCoutPeriode = CDbl(IIf(IsDBNull(.Item("EmployeCoutCout")), 0, .Item("EmployeCoutCout"))) * lDureePeriode
                        dCoutGlobal += dCoutPeriode

                        dTot = CDbl(htCAHTEmployes(lEmployeId))

                        ' TODO : coût Axe, Soufflot et Save à rendre dynamique
                        dCoutAxe = dCoutPeriode * 1
                        dCoutSoufflot = dCoutPeriode * 0
                        dCoutSave = dCoutPeriode * 0

                        If CInt(.Item("EmployeCoutFacturable")) = 0 Then
                            If dTot <> 0 Then
                                ' Calcul du CA par BU
                                iIndexServices = 0

                                iCellule = iColNomEmploye + 1 + iNbServices + (iIndexCout * (iNbColCout + (iNbServices * 3)))
                                wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule).Value = dDateDebutRent & "-" & dDateFinRent
                                wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 1).Value = IIf(IsDBNull(.Item("EmployeCoutCout")), "", .Item("EmployeCoutCommentaire"))

                                For k As Integer = 0 To iNbServices - 1
                                    dTmp = CDbl(oStatistiquesDAO.SelectStatEmployeService(dDateDebutRent, dDateFinRent, CInt(lEmployeId), CInt(dsServices.Tables(0).Rows(k)("ServiceId"))))
                                    iCellule = iColNomEmploye + 1 + iNbServices + (iIndexCout * (iNbColCout + (iNbServices * 3))) + iIndexServices * 3 + iNbColCout

                                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule).Value = dTmp
                                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 1).Value = Format(dTmp / dTot * 100, ".00") & "%"
                                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 2).Value = Format(dTmp / dTot * dCoutAxe, ".00")
                                    dTmp = CDbl(Format(dTmp / dTot * dCoutAxe, ".00"))

                                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                    '------------------------------------------------------------------------------------------------------------------------------------------------------------
                                    wsChargesSal.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSal.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                    wsChargesSal.Cells(iLigEnTeteColonne, iCellule).Value = IIf(IsDBNull(dsServices.Tables(0).Rows(k)("ServiceLibelle")), "", dsServices.Tables(0).Rows(k)("ServiceLibelle"))

                                    iIndexServices += 1
                                Next

                            End If

                        Else
                            iIndexServices = 0
                            For k As Integer = 0 To iNbServices - 1

                                Dim dsCoutService As DataSet = oEmployeDAO.GetEmployeCoutService(CLng(.Item("EmployeCoutID")), CLng(dsServices.Tables(0).Rows(k)("ServiceID")))

                                If dsCoutService.Tables(0).Rows.Count > 0 Then
                                    dTmp = dCoutAxe * CDbl(dsCoutService.Tables(0).Rows(0)("Repartition")) / 100.0#

                                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                    '------------------------------------------------------------------------------------------------------------------------------------------------------------
                                    wsChargesSal.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSal.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                End If
                                iIndexServices += 1
                            Next

                        End If

                    End With

                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColCoutAxe).Value = CDbl(wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColCoutAxe).Value) + dCoutAxe
                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSoufflot).Value = CDbl(wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSoufflot).Value) + dCoutSoufflot
                    wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSave).Value = CDbl(wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSave).Value) + dCoutSave
                    iIndexCout += 1
                Next

                iIndexEmploye += 1
            End If

        Next

        ' Fermeture des datasets et de la connexion
        dsEmployes.Dispose()
        dsServices.Dispose()

        ' Remplissage de l'onglet "Données Axe"
        Dim iLigPctCAHTPrevu As Integer = 2
        Dim iLigCAAnnuelPrevi As Integer = 3
        ' Colonne à renommer CA global
        Dim iLigCAGlobal As Integer = 4
        Dim iLigImpotTaxe As Integer = 5
        Dim iLigSalaireChargeBU As Integer = 6
        Dim iLigFraisStructure As Integer = 7
        Dim iLigREX As Integer = 8
        Dim iLigParametrage As Integer = 10
        Dim iLigMoisLibelle As Integer = 11
        Dim iLigMoisValeur As Integer = 12
        Dim iLigImpotTaxeLibelle As Integer = 13
        Dim iLigImpotTaxeValeur As Integer = 14
        Dim iLigFraisStructureLibelle As Integer = 15
        Dim iLigFraisStructureValeur As Integer = 16
        Dim iLigRatios As Integer = 18
        Dim iLigMasseSalarialeSurCA As Integer = 19
        Dim iLigResultatExploitation As Integer = 20
        Dim iLigOutilsAnneeN As Integer = 21
        Dim iLigCAHTAnneeN As Integer = 22
        Dim iLigOutilsAnneeN1 As Integer = 23
        Dim iLigCAHTAnneeN1 As Integer = 24
        Dim iLigREXAnneeN1 As Integer = 25
        Dim iLigMasseSalarialeCA As Integer = 46
        Dim iLigRésultatDexploitation As Integer = 47

        Dim iColSTJ As Integer = 1
        Dim iColICPE As Integer = 3
        Dim iColMDP As Integer = 5
        Dim iColPORZO As Integer = 7
        Dim iColTotal As Integer = 9
        Dim iColConso As Integer = 12
        '----------------------------------------------------- pour avoir le nombre de mois de la période
        lDureePeriode = DateDiff("m", dDateDeb, dDateFin) + 1
        '------------------------------------------------------------------------------------------------
        wsMain.Cells(0, 0).Value = "Calcul de centre de profit veille"
        wsMain.Cells(0, iColSTJ).Value = "STJ"
        wsMain.Cells(0, iColICPE).Value = "ICPE"
        wsMain.Cells(0, iColMDP).Value = "MDP"
        wsMain.Cells(0, iColPORZO).Value = "PORZO"
        wsMain.Cells(0, iColTotal).Value = "TOTAL"
        wsMain.Cells(0, iColConso).Value = "CONSOLIDE"

        wsMain.Cells(1, iColSTJ).Value = "Objectif STJ"
        wsMain.Cells(1, iColSTJ + 1).Value = "Réalisé STJ"
        wsMain.Cells(1, iColICPE).Value = "Objectif ICPE"
        wsMain.Cells(1, iColICPE + 1).Value = "Réalisé ICPE"
        wsMain.Cells(1, iColMDP).Value = "Objectif MDP"
        wsMain.Cells(1, iColMDP + 1).Value = "Réalisé MDP"
        wsMain.Cells(1, iColPORZO).Value = "Objectif PORZO"
        wsMain.Cells(1, iColPORZO + 1).Value = "Réalisé PORZO"
        wsMain.Cells(1, iColTotal).Value = "Objectif AXE"
        wsMain.Cells(1, iColTotal + 1).Value = "Réalisé AXE"
        wsMain.Cells(1, iColTotal + 2).Value = "Réalisé AXE N-1"
        wsMain.Cells(1, iColConso).Value = "Obj Consolidé"
        wsMain.Cells(1, iColConso + 1).Value = "Réa Conso"
        wsMain.Cells(1, iColConso + 2).Value = "Réa Conso N-1"

        wsMain.Cells(iLigPctCAHTPrevu, 0).Value = "%age du CAHT Prévu"
        wsMain.Cells(iLigCAAnnuelPrevi, 0).Value = "CA ANNUEL PREVI"
        wsMain.Cells(iLigCAGlobal, 0).Value = "CA Global"
        wsMain.Cells(iLigImpotTaxe, 0).Formula = "=""Impôts et taxes (""&A15&""%)"""
        wsMain.Cells(iLigSalaireChargeBU, 0).Value = "Salaires chargés/BU "
        wsMain.Cells(iLigFraisStructure, 0).Formula = "=""Frais de structure (""&A17&""%)"""
        wsMain.Cells(iLigREX, 0).Value = "EBE"
        wsMain.Cells(iLigParametrage, 0).Value = "Paramétrage"
        wsMain.Cells(iLigMoisLibelle, 0).Value = "MOIS"
        wsMain.Cells(iLigMoisValeur, 0).Value = lDureePeriode ' récuprer lDureePeriode <-------------------------
        wsMain.Cells(iLigImpotTaxeLibelle, 0).Value = "Impôts et taxes (en %)"
        wsMain.Cells(iLigImpotTaxeValeur, 0).Value = 2
        wsMain.Cells(iLigFraisStructureLibelle, 0).Value = "Frais de structure (en %)"
        wsMain.Cells(iLigFraisStructureValeur, 0).Value = 31
        wsMain.Cells(iLigRatios, 0).Value = "Ratios"
        wsMain.Cells(iLigMasseSalarialeSurCA, 0).Value = "Masse salariale/CA HT <42%"
        wsMain.Cells(iLigResultatExploitation, 0).Value = "EBE / CA >25à30%"
        wsMain.Cells(iLigOutilsAnneeN, 0).Value = "OUTILS année en cours"
        wsMain.Cells(iLigCAHTAnneeN, 0).Value = "CAHT hors outils année en cours"
        wsMain.Cells(iLigOutilsAnneeN1, 0).Value = "OUTILS année n-1"
        wsMain.Cells(iLigCAHTAnneeN1, 0).Value = "CAHT hors outils année n-1"
        wsMain.Cells(iLigREXAnneeN1, 0).Value = "EBE " & Now.Year - 1

        wsMain.Cells(iLigSalaireChargeBU, iColSTJ + 1).Value = wsChargesSal.Cells("F8").Value
        wsMain.Cells(iLigSalaireChargeBU, iColICPE + 1).Value = wsChargesSal.Cells("E8").Value
        wsMain.Cells(iLigSalaireChargeBU, iColMDP + 1).Value = wsChargesSal.Cells("G8").Value
        wsMain.Cells(iLigSalaireChargeBU, iColPORZO + 1).Value = wsChargesSal.Cells("I8").Value

        wsMain.Cells(iLigCAGlobal, iColSTJ + 1).Value = CDbl(wsChargesSal.Cells("F6").Value) + CDbl(wsChargesSal.Cells("F7").Value) ' cellule C5
        wsMain.Cells(iLigCAGlobal, iColICPE + 1).Value = CDbl(wsChargesSal.Cells("E6").Value) + CDbl(wsChargesSal.Cells("E7").Value)
        wsMain.Cells(iLigCAGlobal, iColMDP + 1).Value = CDbl(wsChargesSal.Cells("G6").Value) + CDbl(wsChargesSal.Cells("G7").Value)
        wsMain.Cells(iLigCAGlobal, iColPORZO + 1).Value = CDbl(wsChargesSal.Cells("I6").Value) + CDbl(wsChargesSal.Cells("I7").Value)
        ' ------------------------------------------------------------------------------------------------------ montant prévisionnel des service pour l'année
        wsMain.Cells(iLigCAAnnuelPrevi, iColSTJ).Value = CDbl(wsChargesSal.Cells("F4").Value)
        wsMain.Cells(iLigCAAnnuelPrevi, iColICPE).Value = CDbl(wsChargesSal.Cells("E4").Value)
        wsMain.Cells(iLigCAAnnuelPrevi, iColMDP).Value = CDbl(wsChargesSal.Cells("G4").Value)
        wsMain.Cells(iLigCAAnnuelPrevi, iColPORZO).Value = CDbl(wsChargesSal.Cells("I4").Value)
        ' ------------------------------------------------------------------------------------------------------
        wsMain.Cells(iLigOutilsAnneeN, iColSTJ + 1).Value = CDbl(wsChargesSal.Cells("F6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColICPE + 1).Value = CDbl(wsChargesSal.Cells("E6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColMDP + 1).Value = CDbl(wsChargesSal.Cells("G6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColPORZO + 1).Value = CDbl(wsChargesSal.Cells("I6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColTotal + 1).Formula = "=C22+E22+G22"
        wsMain.Cells(iLigOutilsAnneeN, iColConso + 1).Formula = "=K22+I22"
        wsMain.Cells(iLigOutilsAnneeN, iColTotal + 2).Formula = "=K24"

        wsMain.Cells(iLigCAHTAnneeN, iColSTJ + 1).Formula = "=C5-C22"
        wsMain.Cells(iLigCAHTAnneeN, iColICPE + 1).Formula = "=E5-E22"
        wsMain.Cells(iLigCAHTAnneeN, iColMDP + 1).Formula = "=G5-G22"
        wsMain.Cells(iLigCAHTAnneeN, iColPORZO + 1).Formula = "=I5-I22"
        wsMain.Cells(iLigCAHTAnneeN, iColTotal + 1).Formula = "=K5-K22"
        wsMain.Cells(iLigCAHTAnneeN, iColTotal + 2).Formula = "=L22/L5"
        wsMain.Cells(iLigCAHTAnneeN, iColConso + 1).Formula = "=N5-N22"


        wsMain.Cells(iLigMasseSalarialeSurCA, iColTotal + 2).Formula = "=L7/L5"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColConso + 2).Formula = "=O7/O5"
        wsMain.Cells(iLigResultatExploitation, iColTotal + 2).Formula = "=L9/L5"
        wsMain.Cells(iLigResultatExploitation, iColConso + 2).Formula = "=O9/O5"

        wsMain.Cells(iLigOutilsAnneeN1, iColTotal + 1).Formula = "=C24+E24+G24"
        wsMain.Cells(iLigCAHTAnneeN1, iColTotal + 1).Formula = "=C25+E25+G25"
        wsMain.Cells(iLigREXAnneeN1, iColTotal + 1).Formula = "=C26+E26+G26"
        wsMain.Cells(iLigOutilsAnneeN1, iColConso + 1).Formula = "=K24+I24"
        wsMain.Cells(iLigCAHTAnneeN1, iColConso + 1).Formula = "=K25+I25"
        wsMain.Cells(iLigREXAnneeN1, iColConso + 1).Formula = "=K26+I26"

        ' Calculs STJ
        wsMain.Cells(iLigPctCAHTPrevu, iColSTJ + 1).Formula = "=C5/B5"
        wsMain.Cells(iLigCAGlobal, iColSTJ).Formula = "=(B4/12)*$A$13"
        wsMain.Cells(iLigImpotTaxe, iColSTJ).Formula = "=B5*$A$15/100"
        wsMain.Cells(iLigSalaireChargeBU, iColSTJ).Formula = "=B5*42/100"
        wsMain.Cells(iLigFraisStructure, iColSTJ).Formula = "=B5*$A$17/100"
        wsMain.Cells(iLigREX, iColSTJ).Formula = "=B5-(B6+B7+B8)"
        wsMain.Cells(iLigImpotTaxe, iColSTJ + 1).Formula = "=C5*$A$15/100"
        wsMain.Cells(iLigFraisStructure, iColSTJ + 1).Formula = "=C5*$A$17/100"
        wsMain.Cells(iLigREX, iColSTJ + 1).Formula = "=C5-(C6+C7+C8)"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColSTJ).Formula = "=B7/B5"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColSTJ + 1).Formula = "=C7/C5"
        wsMain.Cells(iLigResultatExploitation, iColSTJ).Formula = "=B9/B5"
        wsMain.Cells(iLigResultatExploitation, iColSTJ + 1).Formula = "=C9/C5"

        ' Calculs ICPE
        wsMain.Cells(iLigPctCAHTPrevu, iColICPE + 1).Formula = "=E5/D5"
        wsMain.Cells(iLigCAGlobal, iColICPE).Formula = "=(D4/12)*$A$13"
        wsMain.Cells(iLigImpotTaxe, iColICPE).Formula = "=D5*$A$15/100"
        wsMain.Cells(iLigSalaireChargeBU, iColICPE).Formula = "=D5*42/100"
        wsMain.Cells(iLigFraisStructure, iColICPE).Formula = "=D5*$A$17/100"
        wsMain.Cells(iLigREX, iColICPE).Formula = "=D5-(D6+D7+D8)"
        wsMain.Cells(iLigImpotTaxe, iColICPE + 1).Formula = "=E5*$A$15/100"
        wsMain.Cells(iLigFraisStructure, iColICPE + 1).Formula = "=E5*$A$17/100"
        wsMain.Cells(iLigREX, iColICPE + 1).Formula = "=E5-(E6+E7+E8)"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColICPE).Formula = "=D7/D5"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColICPE + 1).Formula = "=E7/E5"
        wsMain.Cells(iLigResultatExploitation, iColICPE).Formula = "=D9/D5"
        wsMain.Cells(iLigResultatExploitation, iColICPE + 1).Formula = "=E9/E5"

        ' Calculs MDP
        wsMain.Cells(iLigPctCAHTPrevu, iColMDP + 1).Formula = "=G5/F5"
        wsMain.Cells(iLigCAGlobal, iColMDP).Formula = "=(F4/12)*$A$13"
        wsMain.Cells(iLigImpotTaxe, iColMDP).Formula = "=F5*$A$15/100"
        wsMain.Cells(iLigSalaireChargeBU, iColMDP).Formula = "=F5*42/100"
        wsMain.Cells(iLigFraisStructure, iColMDP).Formula = "=F5*$A$17/100"
        wsMain.Cells(iLigREX, iColMDP).Formula = "=F5-(F6+F7+F8)"
        wsMain.Cells(iLigImpotTaxe, iColMDP + 1).Formula = "=G5*$A$15/100"
        wsMain.Cells(iLigFraisStructure, iColMDP + 1).Formula = "=G5*$A$17/100"
        wsMain.Cells(iLigREX, iColMDP + 1).Formula = "=G5-(G6+G7+G8)"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColMDP).Formula = "=F7/F5"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColMDP + 1).Formula = "=G7/G5"
        wsMain.Cells(iLigResultatExploitation, iColMDP).Formula = "=F9/F5"
        wsMain.Cells(iLigResultatExploitation, iColMDP + 1).Formula = "=G9/G5"

        ' Calculs PORZO
        wsMain.Cells(iLigPctCAHTPrevu, iColPORZO + 1).Formula = "=I5/H5"
        wsMain.Cells(iLigCAGlobal, iColPORZO).Formula = "=(H4/12)*$A$13"
        wsMain.Cells(iLigImpotTaxe, iColPORZO).Formula = "=H5*$A$15/100"
        wsMain.Cells(iLigSalaireChargeBU, iColPORZO).Formula = "=H5*42/100"
        wsMain.Cells(iLigFraisStructure, iColPORZO).Formula = "=H5*$A$17/100"
        wsMain.Cells(iLigREX, iColPORZO).Formula = "=H5-(H6+H7+H8)"
        wsMain.Cells(iLigImpotTaxe, iColPORZO + 1).Formula = "=I5*$A$15/100"
        wsMain.Cells(iLigFraisStructure, iColPORZO + 1).Formula = "=I5*$A$17/100"
        wsMain.Cells(iLigREX, iColPORZO + 1).Formula = "=I5-(I6+I7+I8)"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColPORZO).Formula = "=H7/H5"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColPORZO + 1).Formula = "=I7/I5"
        wsMain.Cells(iLigResultatExploitation, iColPORZO).Formula = "=H9/H5"
        wsMain.Cells(iLigResultatExploitation, iColPORZO + 1).Formula = "=I9/I5"

        ' Calcul TOTAL
        wsMain.Cells(iLigPctCAHTPrevu, iColTotal + 1).Formula = "=K5/J5"
        wsMain.Cells(iLigCAAnnuelPrevi, iColTotal).Formula = "=B4+D4+F4"
        wsMain.Cells(iLigCAGlobal, iColTotal).Formula = "=(J4/12)*$A$13"
        wsMain.Cells(iLigCAGlobal, iColTotal + 1).Formula = "=C5+E5+G5"
        wsMain.Cells(iLigImpotTaxe, iColTotal).Formula = "=J5*$A$15/100"
        wsMain.Cells(iLigImpotTaxe, iColTotal + 1).Formula = "=C6+E6+G6"
        wsMain.Cells(iLigSalaireChargeBU, iColTotal).Formula = "=J5*42/100"
        wsMain.Cells(iLigSalaireChargeBU, iColTotal + 1).Formula = "=C7+E7+G7"
        wsMain.Cells(iLigFraisStructure, iColTotal).Formula = "=J5*$A$17/100"
        wsMain.Cells(iLigFraisStructure, iColTotal + 1).Formula = "=C8+E8+G8"
        wsMain.Cells(iLigREX, iColTotal).Formula = "=J5-(J6+J7+J8)"
        wsMain.Cells(iLigREX, iColTotal + 1).Formula = "=C9+E9+G9"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColTotal).Formula = "=J7/J5"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColTotal + 1).Formula = "=K7/K5"
        wsMain.Cells(iLigResultatExploitation, iColTotal).Formula = "=J9/J5"
        wsMain.Cells(iLigResultatExploitation, iColTotal + 1).Formula = "=K9/K5"

        ' Calcul conso
        wsMain.Cells(iLigPctCAHTPrevu, iColConso + 1).Formula = "=N5/M5"
        wsMain.Cells(iLigCAAnnuelPrevi, iColConso).Formula = "=J4+H4"
        wsMain.Cells(iLigCAGlobal, iColConso).Formula = "=J5+H5"
        wsMain.Cells(iLigCAGlobal, iColConso + 1).Formula = "=K5+I5"
        wsMain.Cells(iLigImpotTaxe, iColConso).Formula = "=J6+H6"
        wsMain.Cells(iLigImpotTaxe, iColConso + 1).Formula = "=K6+I6"
        wsMain.Cells(iLigSalaireChargeBU, iColConso).Formula = "=J7+H7"
        wsMain.Cells(iLigSalaireChargeBU, iColConso + 1).Formula = "=K7+I7"
        wsMain.Cells(iLigFraisStructure, iColConso).Formula = "=J8+H8"
        wsMain.Cells(iLigFraisStructure, iColConso + 1).Formula = "=K8+I8"
        wsMain.Cells(iLigREX, iColConso).Formula = "=J9+H9"
        wsMain.Cells(iLigREX, iColConso + 1).Formula = "=K9+I9"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColConso).Formula = "=M7/M5"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColConso + 1).Formula = "=N7/N5"
        wsMain.Cells(iLigResultatExploitation, iColConso).Formula = "=M9/M5"
        wsMain.Cells(iLigResultatExploitation, iColConso + 1).Formula = "=N9/N5"

        wsMain.Cells(iLigOutilsAnneeN1, iColSTJ + 1).Formula = " = C49"
        wsMain.Cells(iLigCAHTAnneeN1, iColSTJ + 1).Formula = " = C50"
        wsMain.Cells(iLigREXAnneeN1, iColSTJ + 1).Formula = " = C36"

        wsMain.Cells(iLigOutilsAnneeN1, iColICPE + 1).Formula = " = E49"
        wsMain.Cells(iLigCAHTAnneeN1, iColICPE + 1).Formula = " = E50"
        wsMain.Cells(iLigREXAnneeN1, iColICPE + 1).Formula = " = E36"

        wsMain.Cells(iLigOutilsAnneeN1, iColMDP + 1).Formula = " = G49"
        wsMain.Cells(iLigCAHTAnneeN1, iColMDP + 1).Formula = " = G50"
        wsMain.Cells(iLigREXAnneeN1, iColMDP + 1).Formula = " = G36"

        wsMain.Cells(iLigOutilsAnneeN1, iColPORZO + 1).Formula = " = I49"
        wsMain.Cells(iLigCAHTAnneeN1, iColPORZO + 1).Formula = " = I50"
        wsMain.Cells(iLigREXAnneeN1, iColPORZO + 1).Formula = " = I36"

        wsMain.Cells(iLigOutilsAnneeN1, iColTotal + 1).Formula = " = K49"
        wsMain.Cells(iLigCAHTAnneeN1, iColTotal + 1).Formula = " = K50"
        wsMain.Cells(iLigREXAnneeN1, iColTotal + 1).Formula = " = K36"

        wsMain.Cells(iLigOutilsAnneeN1, iColConso + 1).Formula = " = SUM(C" & iLigOutilsAnneeN1 + 1 & ":L" & iLigOutilsAnneeN1 + 1 & ")"
        wsMain.Cells(iLigCAHTAnneeN1, iColConso + 1).Formula = " = SUM(C" & iLigCAHTAnneeN1 + 1 & ":L" & iLigCAHTAnneeN1 + 1 & ")"
        wsMain.Cells(iLigREXAnneeN1, iColConso + 1).Formula = " = SUM(C" & iLigREXAnneeN1 + 1 & ":L" & iLigREXAnneeN1 + 1 & ")"


        For i As Integer = 0 To 6
            wsMain.Cells(iLigPctCAHTPrevu + i, iColTotal + 2).Formula = " = K" & 30 + i
            wsMain.Cells(iLigPctCAHTPrevu + i, iColConso + 2).Formula = " = N" & 30 + i


        Next

        ' Mise en forme de la feuille Excel
        ' 1) Format des cellules 
        ' 1-a) Pourcentage
        wsMain.Cells(iLigPctCAHTPrevu, iColSTJ + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColICPE + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColMDP + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColPORZO + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColTotal + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColTotal + 2).Style.NumberFormat = "0.00%"
        wsMain.Cells(iLigPctCAHTPrevu, iColConso + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColConso + 2).Style.NumberFormat = "0.00%"
        wsMain.Rows(iLigMasseSalarialeSurCA).Style.NumberFormat = "0.00%"
        wsMain.Rows(iLigResultatExploitation).Style.NumberFormat = "0.00%"
        wsMain.Rows(iLigOutilsAnneeN).Style.NumberFormat = "0"
        wsMain.Rows(iLigCAHTAnneeN).Style.NumberFormat = "0"
        wsMain.Rows(iLigOutilsAnneeN1).Style.NumberFormat = "0"
        wsMain.Rows(iLigCAHTAnneeN1).Style.NumberFormat = "0"
        wsMain.Rows(iLigREXAnneeN1).Style.NumberFormat = "0"
        For j = 1 To 15

            '1-b) On arrondit à l'entier le plus proche
            For i As Integer = 3 To 8
                wsMain.Cells(i, j).Style.NumberFormat = "0"
            Next
        Next

        ' 2) Alternance par ligne de 2 niveaux de gris
        For i As Integer = 2 To 8

            If i Mod 2 = 0 Then
                wsMain.Cells(i, 0).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(242, 242, 242), Drawing.Color.Beige)
            Else
                wsMain.Cells(i, 0).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(216, 216, 216), Drawing.Color.Beige)
            End If
        Next

        For i As Integer = 19 To 25
            For j As Integer = 0 To iColConso + 2
                If i Mod 2 = 0 Then
                    wsMain.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(216, 216, 216), Drawing.Color.Beige)
                Else
                    wsMain.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(242, 242, 242), Drawing.Color.Beige)
                End If
            Next
        Next
        ' Mise en couleur de stj, ICPE...
        For i As Integer = 0 To 8
            For j As Integer = 1 To 2
                wsMain.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(230, 184, 183), Drawing.Color.Beige)
                wsMain.Cells(i, j + 2).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(149, 179, 215), Drawing.Color.Beige)
                wsMain.Cells(i, j + 4).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(146, 208, 80), Drawing.Color.Beige)
                wsMain.Cells(i, j + 6).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(191, 191, 191), Drawing.Color.Beige)
                wsMain.Cells(i, j + 8).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 102), Drawing.Color.Beige)
                wsMain.Cells(i, j + 11).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(242, 220, 219), Drawing.Color.Beige)
            Next
        Next
        wsMain.Cells(1, 11).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 0), Drawing.Color.Beige)
        wsMain.Cells(1, 14).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(230, 184, 183), Drawing.Color.Beige)
        For i As Integer = 4 To 8
            wsMain.Cells(i, 11).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 204, 102), Drawing.Color.Beige)
            wsMain.Cells(i, 14).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(230, 184, 183), Drawing.Color.Beige)
        Next
        ' 3) Mise en place des bordures pour plus de lisibilité (entre auutres séparation des BU)
        wsMain.Cells.GetSubrange("A3", "A9").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("B1", "C2").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("B3", "C9").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("D1", "E2").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("D3", "E9").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("F1", "G2").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("F3", "G9").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("H1", "I2").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("H3", "I9").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("J1", "L2").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("J3", "L9").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("M1", "O2").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("M3", "O9").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)

        wsMain.Cells.GetSubrange("A12", "A17").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("A20", "A26").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)

        wsMain.Cells.GetSubrange("B20", "C26").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("D20", "E26").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("F20", "G26").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("H20", "I26").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("J20", "L26").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("M20", "O26").SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)

        ' On ajuste la largeur des colonnes et des lignes
        For j As Integer = 0 To 14
            wsMain.Columns(j).AutoFitAdvanced(1)
        Next

        ExporterREXNmoins1(ef, wsMain)

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "REX_" & Year(dDateDeb) & "_De_" & GetLibelleMois(Month(dDateDeb)) & "_A_" & GetLibelleMois(Month(dDateFin))

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)

    End Sub

    Public Function MaxDate(d1 As Object, d2 As Object) As Date
        If IsDBNull(d1) And IsDBNull(d2) Then
            Return CDate("00:00:00")
        ElseIf IsDBNull(d1) And Not IsDBNull(d2) Then
            Return CDate(d2)
        ElseIf Not IsDBNull(d1) And IsDBNull(d2) Then
            Return CDate(d1)
        Else
            ' Dans ce cas, les 2 dates sont non vides
            If Date.Compare(CDate(d1), CDate(d2)) >= 0 Then
                Return CDate(d1)
            Else
                Return CDate(d2)
            End If
        End If
    End Function

    Public Function MinDate(d1 As Object, d2 As Object) As Date
        If IsDBNull(d1) And IsDBNull(d2) Then
            Return CDate("00:00:00")
        ElseIf IsDBNull(d1) And Not IsDBNull(d2) Then
            Return CDate(d2)
        ElseIf Not IsDBNull(d1) And IsDBNull(d2) Then
            Return CDate(d1)
        Else
            ' Dans ce cas, les 2 dates sont non vides
            If Date.Compare(CDate(d1), CDate(d2)) <= 0 Then
                Return CDate(d1)
            Else
                Return CDate(d2)
            End If
        End If
    End Function

    Public Function GetLibelleMois(iIndexMois As Integer) As String
        Select Case iIndexMois
            Case 1
                Return "Janvier"
            Case 2
                Return "Fevrier"
            Case 3
                Return "Mars"
            Case 4
                Return "Avril"
            Case 5
                Return "Mai"
            Case 6
                Return "Juin"
            Case 7
                Return "Juillet"
            Case 8
                Return "Aout"
            Case 9
                Return "Septembre"
            Case 10
                Return "Octobre"
            Case 11
                Return "Novembre"
            Case 12
                Return "Decembre"
            Case Else
                Return "-1"
        End Select
    End Function


    Public Function IsBisextile(madate As Date) As Boolean
        IsBisextile = Day(DateSerial(Year(madate), 3, 0)) = 29
    End Function

    Private Sub ExporterREXNmoins1(ByRef ef As ExcelFile, ByRef wsMain As ExcelWorksheet)
        Dim oServiceDAO As New CServiceDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()
        Dim dsCAServices As DataSet
        Dim dsEmployes As DataSet

        Dim wsChargesSalNmoins1 As ExcelWorksheet = ef.Worksheets.Add("chargessalN-1")

        'Dim sAnneeDebN1, sAnneeFinN1 As New Integer
        Dim dDateDebN1, dDateFinN1 As New Date

        'sAnneeDebN1 = DatePart("yyyy", dDateDeb) - 1
        'sAnneeFinN1 = DatePart("yyyy", dDateFin) - 1

        dDateDebN1 = CDate(dDateDeb).AddYears(-1)
        dDateFinN1 = CDate(dDateFin).AddYears(-1)

        'If IsBisextile(dDateDeb) And CStr(DatePart("m", dDateDeb)) = "2" And CStr(DatePart("d", dDateDeb)) = "29" Then '
        '    dDateDebN1 = CDate(sAnneeDebN1 & "/" & DatePart("m", dDateDeb) & "/" & DatePart("d", dDateDeb) - 1)
        'Else
        '    dDateDebN1 = CDate(sAnneeDebN1 & "/" & DatePart("m", dDateDeb) & "/" & DatePart("d", dDateDeb))
        'End If

        'If IsBisextile(dDateFin) And CStr(DatePart("m", dDateFin)) = "2" And CStr(DatePart("d", dDateFin)) = "29" Then '
        '    dDateFinN1 = CDate(sAnneeFinN1 & "/" & DatePart("m", dDateFin) & "/" & DatePart("d", dDateFin) - 1)
        'Else
        '    dDateFinN1 = CDate(sAnneeFinN1 & "/" & DatePart("m", dDateFin) & "/" & DatePart("d", dDateFin))
        'End If

        Dim htCAHTEmployes As Hashtable = oStatistiquesDAO.GetCAHTParEmploye(dDateDebN1, dDateFinN1)


        Dim iIndexServices As Integer = 0
        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count

        Dim iColCoutSave As Integer = 2
        Dim iColCoutSoufflot As Integer = 1
        Dim iColCoutAxe As Integer = 0

        Dim iColNomEmploye As Integer = 3
        Dim iLigNomEmploye As Integer = 9

        Dim iLigTotauxServices As Integer = 7
        Dim iLigEnTeteColonne As Integer = 8

        Dim iNbColCout As Integer = 2

        ' Parcours des services
        Dim lDureePeriode As Long = DateDiff("m", dDateDebN1, dDateFinN1) + 1

        For i As Integer = 0 To iNbServices - 1
            With dsServices.Tables(0).Rows(i)
                wsChargesSalNmoins1.Cells(iLigTotauxServices + 1, iColNomEmploye + 1 + iIndexServices).Value = .Item("ServiceLibelle")
                dsCAServices = oStatistiquesDAO.SelectStatGeneralesParService(dDateDebN1, dDateFinN1, False, False, CInt(.Item("ServiceId")), 0, -1, "")
                wsChargesSalNmoins1.Cells(iLigTotauxServices - 1, iColNomEmploye + 1 + iIndexServices).Value = dsCAServices.Tables(0).Rows(0)("CAHT")
                wsChargesSalNmoins1.Cells(iLigTotauxServices - 2, iColNomEmploye + 1 + iIndexServices).Value = oProduitAffaireDAO.GetProduitsHorsDepassementBudget(dDateDebN1, dDateFinN1, CLng(.Item("ServiceId")))
                wsChargesSalNmoins1.Cells(iLigTotauxServices - 3, iColNomEmploye + 1 + iIndexServices).Value = Format(CDbl(IIf(IsDBNull(.Item("ServiceObjectif")), 0, .Item("ServiceObjectif"))) * CDbl(lDureePeriode) / 12, ".00")
                wsChargesSalNmoins1.Cells(iLigTotauxServices - 4, iColNomEmploye + 1 + iIndexServices).Value = Format(.Item("ServiceObjectif"), ".00")
            End With

            iIndexServices += 1
        Next

        ' Parcours des employés
        dsEmployes = oEmployeDAO.GetNomPrenomEmploye()
        Dim iIndexEmploye As Integer = 0

        For i As Integer = 0 To dsEmployes.Tables(0).Rows.Count - 1
            Dim lEmployeId As Long = CLng(dsEmployes.Tables(0).Rows(i)("EmployeId"))
            Dim iIndexCout As Integer = 0
            Dim dCoutGlobal As Double = 0
            Dim dCoutPeriode As Double = 0
            Dim dsCouts As DataSet
            Dim dTot As Double = 0
            Dim iCellule As Integer = 0
            Dim dTmp As Double = 0

            Dim dCoutAxe As Double
            Dim dCoutSoufflot As Double
            Dim dCoutSave As Double

            dsCouts = oEmployeDAO.GetEmployeCoutPeriode(dDateDebN1, dDateFinN1, lEmployeId)

            If dsCouts.Tables(0).Rows.Count > 0 Then
                'on a au moins un cout dans la liste, donc on ajoute les infos de l'employé
                wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye).Value = dsEmployes.Tables(0).Rows(i)("PrenomNom")

                For j As Integer = 0 To dsCouts.Tables(0).Rows.Count - 1
                    With dsCouts.Tables(0).Rows(j)
                        ' On calcule la date de la rentabilité pour la période trouvée
                        Dim dDateDebutRent As Date
                        Dim dDateFinRent As Date

                        dDateDebutRent = MaxDate(dDateDebN1, .Item("EmployeCoutDateDebut"))
                        dDateFinRent = MinDate(dDateFinN1, .Item("EmployeCoutDateFin"))

                        ' Calcul du coût de la personne sur la période
                        lDureePeriode = DateDiff(DateInterval.Month, dDateDebutRent, dDateFinRent) + 1
                        dCoutPeriode = CDbl(IIf(IsDBNull(.Item("EmployeCoutCout")), 0, .Item("EmployeCoutCout"))) * lDureePeriode
                        dCoutGlobal += dCoutPeriode

                        dTot = CDbl(htCAHTEmployes(lEmployeId))

                        ' TODO : coût Axe, Soufflot et Save à rendre dynamique
                        dCoutAxe = dCoutPeriode * 1
                        dCoutSoufflot = dCoutPeriode * 0
                        dCoutSave = dCoutPeriode * 0

                        If CInt(.Item("EmployeCoutFacturable")) = 0 Then
                            If dTot <> 0 Then
                                ' Calcul du CA par BU
                                iIndexServices = 0

                                iCellule = iColNomEmploye + 1 + iNbServices + (iIndexCout * (iNbColCout + (iNbServices * 3)))
                                wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iCellule).Value = dDateDebutRent & "-" & dDateFinRent
                                wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 1).Value = IIf(IsDBNull(.Item("EmployeCoutCout")), "", .Item("EmployeCoutCommentaire"))

                                For k As Integer = 0 To iNbServices - 1
                                    dTmp = CDbl(oStatistiquesDAO.SelectStatEmployeService(dDateDebutRent, dDateFinRent, CInt(lEmployeId), CInt(dsServices.Tables(0).Rows(k)("ServiceId"))))
                                    iCellule = iColNomEmploye + 1 + iNbServices + (iIndexCout * (iNbColCout + (iNbServices * 3))) + iIndexServices * 3 + iNbColCout

                                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iCellule).Value = dTmp
                                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 1).Value = Format(dTmp / dTot * 100, ".00") & "%"
                                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 2).Value = Format(dTmp / dTot * dCoutAxe, ".00")
                                    dTmp = CDbl(Format(dTmp / dTot * dCoutAxe, ".00"))

                                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                    wsChargesSalNmoins1.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSalNmoins1.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                    wsChargesSalNmoins1.Cells(iLigEnTeteColonne, iCellule).Value = IIf(IsDBNull(dsServices.Tables(0).Rows(k)("ServiceLibelle")), "", dsServices.Tables(0).Rows(k)("ServiceLibelle"))

                                    iIndexServices += 1
                                Next

                            End If

                        Else
                            iIndexServices = 0
                            For k As Integer = 0 To iNbServices - 1

                                Dim dsCoutService As DataSet = oEmployeDAO.GetEmployeCoutService(CLng(.Item("EmployeCoutID")), CLng(dsServices.Tables(0).Rows(k)("ServiceID")))

                                If dsCoutService.Tables(0).Rows.Count > 0 Then
                                    dTmp = dCoutAxe * CDbl(dsCoutService.Tables(0).Rows(0)("Repartition")) / 100.0#

                                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                    wsChargesSalNmoins1.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSalNmoins1.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                                End If
                                iIndexServices += 1
                            Next

                        End If

                    End With

                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColCoutAxe).Value = CDbl(wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColCoutAxe).Value) + dCoutAxe
                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSoufflot).Value = CDbl(wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSoufflot).Value) + dCoutSoufflot
                    wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSave).Value = CDbl(wsChargesSalNmoins1.Cells(iLigNomEmploye + iIndexEmploye, iColCoutSave).Value) + dCoutSave
                    iIndexCout += 1
                Next

                iIndexEmploye += 1
            End If

        Next

        ' Fermeture des datasets et de la connexion
        dsEmployes.Dispose()
        dsServices.Dispose()
        '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ' Remplissage de l'onglet "Données Axe"
        Dim idecalageDesLignes As Integer = 27
        Dim iLigPctCAHTPrevu As Integer = 2 + idecalageDesLignes
        Dim iLigCAAnnuelPrevi As Integer = 3 + idecalageDesLignes
        ' Colonne à renommer CA global
        Dim iLigCAGlobal As Integer = 4 + idecalageDesLignes
        Dim iLigImpotTaxe As Integer = 5 + idecalageDesLignes
        Dim iLigSalaireChargeBU As Integer = 6 + idecalageDesLignes
        Dim iLigFraisStructure As Integer = 7 + idecalageDesLignes
        Dim iLigREX As Integer = 8 + idecalageDesLignes
        Dim iLigParametrage As Integer = 10 + idecalageDesLignes
        Dim iLigMoisLibelle As Integer = 11 + idecalageDesLignes
        Dim iLigMoisValeur As Integer = 12 + idecalageDesLignes
        Dim iLigImpotTaxeLibelle As Integer = 13 + idecalageDesLignes
        Dim iLigImpotTaxeValeur As Integer = 14 + idecalageDesLignes
        Dim iLigFraisStructureLibelle As Integer = 15 + idecalageDesLignes
        Dim iLigFraisStructureValeur As Integer = 16 + idecalageDesLignes
        Dim iLigRatios As Integer = 18 + idecalageDesLignes
        Dim iLigMasseSalarialeSurCA As Integer = 19 + idecalageDesLignes
        Dim iLigResultatExploitation As Integer = 20 + idecalageDesLignes
        Dim iLigOutilsAnneeN As Integer = 21 + idecalageDesLignes
        Dim iLigCAHTAnneeN As Integer = 22 + idecalageDesLignes
        'Dim iLigOutilsAnneeN1 As Integer = 23 + idecalageDesLignes
        'Dim iLigCAHTAnneeN1 As Integer = 24 + idecalageDesLignes
        'Dim iLigREXAnneeN1 As Integer = 25 + idecalageDesLignes

        Dim iColSTJ As Integer = 1
        Dim iColICPE As Integer = 3
        Dim iColMDP As Integer = 5
        Dim iColPORZO As Integer = 7
        Dim iColTotal As Integer = 9
        Dim iColConso As Integer = 12

        lDureePeriode = DateDiff("m", dDateDebN1, dDateFinN1) + 1

        wsMain.Cells(0 + idecalageDesLignes, 0).Value = "Calcul de centre de profit veille"
        wsMain.Cells(0 + idecalageDesLignes, iColSTJ).Value = "STJ"
        wsMain.Cells(0 + idecalageDesLignes, iColICPE).Value = "ICPE"
        wsMain.Cells(0 + idecalageDesLignes, iColMDP).Value = "MDP"
        wsMain.Cells(0 + idecalageDesLignes, iColPORZO).Value = "PORZO"
        wsMain.Cells(0 + idecalageDesLignes, iColTotal).Value = "TOTAL"
        wsMain.Cells(0 + idecalageDesLignes, iColConso).Value = "CONSOLIDE"

        wsMain.Cells(1 + idecalageDesLignes, iColSTJ).Value = "Objectif STJ"
        wsMain.Cells(1 + idecalageDesLignes, iColSTJ + 1).Value = "Réalisé STJ"
        wsMain.Cells(1 + idecalageDesLignes, iColICPE).Value = "Objectif ICPE"
        wsMain.Cells(1 + idecalageDesLignes, iColICPE + 1).Value = "Réalisé ICPE"
        wsMain.Cells(1 + idecalageDesLignes, iColMDP).Value = "Objectif MDP"
        wsMain.Cells(1 + idecalageDesLignes, iColMDP + 1).Value = "Réalisé MDP"
        wsMain.Cells(1 + idecalageDesLignes, iColPORZO).Value = "Objectif PORZO"
        wsMain.Cells(1 + idecalageDesLignes, iColPORZO + 1).Value = "Réalisé PORZO"
        wsMain.Cells(1 + idecalageDesLignes, iColTotal).Value = "Objectif AXE"
        wsMain.Cells(1 + idecalageDesLignes, iColTotal + 1).Value = "Réalisé AXE"
        wsMain.Cells(1 + idecalageDesLignes, iColTotal + 2).Value = "Réalisé AXE N-2"
        wsMain.Cells(1 + idecalageDesLignes, iColConso).Value = "Obj Consolidé"
        wsMain.Cells(1 + idecalageDesLignes, iColConso + 1).Value = "Réa Conso"
        wsMain.Cells(1 + idecalageDesLignes, iColConso + 2).Value = "Réa Conso N-2"

        wsMain.Cells(iLigPctCAHTPrevu, 0).Value = "%age du CAHT Prévu"
        wsMain.Cells(iLigCAAnnuelPrevi, 0).Value = "CA ANNUEL PREVI"
        wsMain.Cells(iLigCAGlobal, 0).Value = "CA Global"
        wsMain.Cells(iLigImpotTaxe, 0).Formula = "=""Impôts et taxes (""&A" & 15 + idecalageDesLignes & "&""%)"""
        wsMain.Cells(iLigSalaireChargeBU, 0).Value = "Salaires chargés/BU "
        wsMain.Cells(iLigFraisStructure, 0).Formula = "=""Frais de structure (""&A" & 17 + idecalageDesLignes & "&""%)"""
        wsMain.Cells(iLigREX, 0).Value = "EBE"
        wsMain.Cells(iLigParametrage, 0).Value = "Paramétrage"
        wsMain.Cells(iLigMoisLibelle, 0).Value = "MOIS"
        wsMain.Cells(iLigMoisValeur, 0).Value = lDureePeriode
        wsMain.Cells(iLigImpotTaxeLibelle, 0).Value = "Impôts et taxes (en %)"
        wsMain.Cells(iLigImpotTaxeValeur, 0).Value = 2
        wsMain.Cells(iLigFraisStructureLibelle, 0).Value = "Frais de structure (en %)"
        wsMain.Cells(iLigFraisStructureValeur, 0).Value = 31
        wsMain.Cells(iLigRatios, 0).Value = "Ratios N-1"
        wsMain.Cells(iLigMasseSalarialeSurCA, 0).Value = "Masse salariale/CA HT <42%"
        wsMain.Cells(iLigResultatExploitation, 0).Value = "EBE / CA HT >25à30%"
        wsMain.Cells(iLigOutilsAnneeN, 0).Value = "OUTILS année en cours"
        wsMain.Cells(iLigCAHTAnneeN, 0).Value = "CAHT hors outils année en cours"
        'wsMain.Cells(iLigOutilsAnneeN1, 0).Value = "OUTILS année n-1"
        'wsMain.Cells(iLigCAHTAnneeN1, 0).Value = "CAHT hors outils année n-1"
        'wsMain.Cells(iLigREXAnneeN1, 0).Value = "REX 2011"

        wsMain.Cells(iLigSalaireChargeBU, iColSTJ + 1).Value = wsChargesSalNmoins1.Cells("F8").Value
        wsMain.Cells(iLigSalaireChargeBU, iColICPE + 1).Value = wsChargesSalNmoins1.Cells("E8").Value
        wsMain.Cells(iLigSalaireChargeBU, iColMDP + 1).Value = wsChargesSalNmoins1.Cells("G8").Value
        wsMain.Cells(iLigSalaireChargeBU, iColPORZO + 1).Value = wsChargesSalNmoins1.Cells("I8").Value

        wsMain.Cells(iLigCAGlobal, iColSTJ + 1).Value = CDbl(wsChargesSalNmoins1.Cells("F6").Value) + CDbl(wsChargesSalNmoins1.Cells("F7").Value)
        wsMain.Cells(iLigCAGlobal, iColICPE + 1).Value = CDbl(wsChargesSalNmoins1.Cells("E6").Value) + CDbl(wsChargesSalNmoins1.Cells("E7").Value)
        wsMain.Cells(iLigCAGlobal, iColMDP + 1).Value = CDbl(wsChargesSalNmoins1.Cells("G6").Value) + CDbl(wsChargesSalNmoins1.Cells("G7").Value)
        wsMain.Cells(iLigCAGlobal, iColPORZO + 1).Value = CDbl(wsChargesSalNmoins1.Cells("I6").Value) + CDbl(wsChargesSalNmoins1.Cells("I7").Value)

        wsMain.Cells(iLigCAAnnuelPrevi, iColSTJ).Value = CDbl(wsChargesSalNmoins1.Cells("F4").Value)
        wsMain.Cells(iLigCAAnnuelPrevi, iColICPE).Value = CDbl(wsChargesSalNmoins1.Cells("E4").Value)
        wsMain.Cells(iLigCAAnnuelPrevi, iColMDP).Value = CDbl(wsChargesSalNmoins1.Cells("G4").Value)
        wsMain.Cells(iLigCAAnnuelPrevi, iColPORZO).Value = CDbl(wsChargesSalNmoins1.Cells("I4").Value)

        wsMain.Cells(iLigOutilsAnneeN, iColSTJ + 1).Value = CDbl(wsChargesSalNmoins1.Cells("F6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColICPE + 1).Value = CDbl(wsChargesSalNmoins1.Cells("E6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColMDP + 1).Value = CDbl(wsChargesSalNmoins1.Cells("G6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColPORZO + 1).Value = CDbl(wsChargesSalNmoins1.Cells("I6").Value)
        wsMain.Cells(iLigOutilsAnneeN, iColTotal + 1).Formula = "=C" & 22 + idecalageDesLignes & "+E" & 22 + idecalageDesLignes & "+G" & 22 + idecalageDesLignes
        wsMain.Cells(iLigOutilsAnneeN, iColConso + 1).Formula = "=K" & 22 + idecalageDesLignes & "+I" & 22 + idecalageDesLignes
        wsMain.Cells(iLigOutilsAnneeN, iColTotal + 2).Formula = "=K24" & +idecalageDesLignes

        wsMain.Cells(iLigCAHTAnneeN, iColSTJ + 1).Formula = "=C" & 5 + idecalageDesLignes & "-C" & 22 + idecalageDesLignes
        wsMain.Cells(iLigCAHTAnneeN, iColICPE + 1).Formula = "=E" & 5 + idecalageDesLignes & "-E" & 22 + idecalageDesLignes
        wsMain.Cells(iLigCAHTAnneeN, iColMDP + 1).Formula = "=G" & 5 + idecalageDesLignes & "-G" & 22 + idecalageDesLignes
        wsMain.Cells(iLigCAHTAnneeN, iColPORZO + 1).Formula = "=I" & 5 + idecalageDesLignes & "-I" & 22 + idecalageDesLignes
        wsMain.Cells(iLigCAHTAnneeN, iColTotal + 1).Formula = "=K" & 5 + idecalageDesLignes & "-K" & 22 + idecalageDesLignes
        wsMain.Cells(iLigCAHTAnneeN, iColTotal + 2).Formula = "=L" & 22 + idecalageDesLignes & "/L" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAHTAnneeN, iColConso + 1).Formula = "=N" & 5 + idecalageDesLignes & "-N" & 22 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColTotal + 2).Formula = "=L" & 7 + idecalageDesLignes & "/L" & 5 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColConso + 2).Formula = "=O" & 7 + idecalageDesLignes & "/O" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColTotal + 2).Formula = "=L" & 9 + idecalageDesLignes & "/L" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColConso + 2).Formula = "=O" & 9 + idecalageDesLignes & "/O" & 5 + idecalageDesLignes
        ' wsMain.Cells(iLigOutilsAnneeN1, iColTotal + 1).Formula = "=C" & 24 + idecalageDesLignes & "+E" & 24 + idecalageDesLignes & "+G" & 24 + idecalageDesLignes
        'wsMain.Cells(iLigCAHTAnneeN1, iColTotal + 1).Formula = "=C" & 25 + idecalageDesLignes & "+E" & 25 + idecalageDesLignes & "+G" & 25 + idecalageDesLignes
        'wsMain.Cells(iLigREXAnneeN1, iColTotal + 1).Formula = "=C" & 26 + idecalageDesLignes & "+E" & 26 + idecalageDesLignes & "+G" & 26 + idecalageDesLignes
        'wsMain.Cells(iLigOutilsAnneeN1, iColConso + 1).Formula = "=K" & 24 + idecalageDesLignes & "+I" & 24 + idecalageDesLignes
        'wsMain.Cells(iLigCAHTAnneeN1, iColConso + 1).Formula = "=K" & 25 + idecalageDesLignes & "+I" & 25 + idecalageDesLignes
        'wsMain.Cells(iLigREXAnneeN1, iColConso + 1).Formula = "=K" & 26 + idecalageDesLignes & "+I" & 26 + idecalageDesLignes

        ' Calculs STJ
        wsMain.Cells(iLigPctCAHTPrevu, iColSTJ + 1).Formula = "=C" & 5 + idecalageDesLignes & "/B" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColSTJ).Formula = "=(B" & 4 + idecalageDesLignes & "/12)*$A$" & 13 + idecalageDesLignes
        wsMain.Cells(iLigImpotTaxe, iColSTJ).Formula = "=B" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigSalaireChargeBU, iColSTJ).Formula = "=B" & 5 + idecalageDesLignes & "*42/100"
        wsMain.Cells(iLigFraisStructure, iColSTJ).Formula = "=B" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColSTJ).Formula = "=B" & 5 + idecalageDesLignes & "-(B" & 6 + idecalageDesLignes & "+B" & 7 + idecalageDesLignes & "+B" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigImpotTaxe, iColSTJ + 1).Formula = "=C" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigFraisStructure, iColSTJ + 1).Formula = "=C" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColSTJ + 1).Formula = "=C" & 5 + idecalageDesLignes & "-(C" & 6 + idecalageDesLignes & "+C" & 7 + idecalageDesLignes & "+C" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColSTJ).Formula = "=B" & 7 + idecalageDesLignes & "/B" & 5 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColSTJ + 1).Formula = "=C" & 7 + idecalageDesLignes & "/C" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColSTJ).Formula = "=B" & 9 + idecalageDesLignes & "/B" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColSTJ + 1).Formula = "=C" & 9 + idecalageDesLignes & "/C" & 5 + idecalageDesLignes

        ' Calculs ICPE
        wsMain.Cells(iLigPctCAHTPrevu, iColICPE + 1).Formula = "=E" & 5 + idecalageDesLignes & "/D" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColICPE).Formula = "=(D" & 4 + idecalageDesLignes & "/12)*$A$" & 13 + idecalageDesLignes
        wsMain.Cells(iLigImpotTaxe, iColICPE).Formula = "=D" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigSalaireChargeBU, iColICPE).Formula = "=D" & 5 + idecalageDesLignes & "*42/100"
        wsMain.Cells(iLigFraisStructure, iColICPE).Formula = "=D" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColICPE).Formula = "=D" & 5 + idecalageDesLignes & "-(D" & 6 + idecalageDesLignes & "+D" & 7 + idecalageDesLignes & "+D" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigImpotTaxe, iColICPE + 1).Formula = "=E" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigFraisStructure, iColICPE + 1).Formula = "=E" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColICPE + 1).Formula = "=E" & 5 + idecalageDesLignes & "-(E" & 6 + idecalageDesLignes & "+E" & 7 + idecalageDesLignes & "+E" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColICPE).Formula = "=D" & 7 + idecalageDesLignes & "/D" & 5 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColICPE + 1).Formula = "=E" & 7 + idecalageDesLignes & "/E" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColICPE).Formula = "=D" & 9 + idecalageDesLignes & "/D" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColICPE + 1).Formula = "=E" & 9 + idecalageDesLignes & "/E" & 5 + idecalageDesLignes

        ' Calculs MDP
        wsMain.Cells(iLigPctCAHTPrevu, iColMDP + 1).Formula = "=G" & 5 + idecalageDesLignes & "/F" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColMDP).Formula = "=(F" & 4 + idecalageDesLignes & "/12)*$A$" & 13 + idecalageDesLignes
        wsMain.Cells(iLigImpotTaxe, iColMDP).Formula = "=F" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigSalaireChargeBU, iColMDP).Formula = "=F" & 5 + idecalageDesLignes & "*42/100"
        wsMain.Cells(iLigFraisStructure, iColMDP).Formula = "=F" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColMDP).Formula = "=F" & 5 + idecalageDesLignes & "-(F" & 6 + idecalageDesLignes & "+F" & 7 + idecalageDesLignes & "+F" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigImpotTaxe, iColMDP + 1).Formula = "=G" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigFraisStructure, iColMDP + 1).Formula = "=G" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColMDP + 1).Formula = "=G" & 5 + idecalageDesLignes & "-(G" & 6 + idecalageDesLignes & "+G" & 7 + idecalageDesLignes & "+G" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColMDP).Formula = "=F" & 7 + idecalageDesLignes & "/F" & 5 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColMDP + 1).Formula = "=G" & 7 + idecalageDesLignes & "/G" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColMDP).Formula = "=F" & 9 + idecalageDesLignes & "/F" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColMDP + 1).Formula = "=G" & 9 + idecalageDesLignes & "/G" & 5 + idecalageDesLignes

        ' Calculs PORZO
        wsMain.Cells(iLigPctCAHTPrevu, iColPORZO + 1).Formula = "=I" & 5 + idecalageDesLignes & "/H" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColPORZO).Formula = "=(H" & 4 + idecalageDesLignes & "/12)*$A$" & 13 + idecalageDesLignes
        wsMain.Cells(iLigImpotTaxe, iColPORZO).Formula = "=H" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigSalaireChargeBU, iColPORZO).Formula = "=H" & 5 + idecalageDesLignes & "*42/100"
        wsMain.Cells(iLigFraisStructure, iColPORZO).Formula = "=H" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColPORZO).Formula = "=H" & 5 + idecalageDesLignes & "-(H" & 6 + idecalageDesLignes & "+H" & 7 + idecalageDesLignes & "+H" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigImpotTaxe, iColPORZO + 1).Formula = "=I" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigFraisStructure, iColPORZO + 1).Formula = "=I" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigREX, iColPORZO + 1).Formula = "=I" & 5 + idecalageDesLignes & "-(I" & 6 + idecalageDesLignes & "+I" & 7 + idecalageDesLignes & "+I" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigMasseSalarialeSurCA, iColPORZO).Formula = "=H" & 7 + idecalageDesLignes & "/H" & 5 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColPORZO + 1).Formula = "=I" & 7 + idecalageDesLignes & "/I" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColPORZO).Formula = "=H" & 9 + idecalageDesLignes & "/H" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColPORZO + 1).Formula = "=I" & 9 + idecalageDesLignes & "/I" & 5 + idecalageDesLignes

        ' Calcul TOTAL
        wsMain.Cells(iLigPctCAHTPrevu, iColTotal + 1).Formula = "=K" & 5 + idecalageDesLignes & "/J" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAAnnuelPrevi, iColTotal).Formula = "=B" & 4 + idecalageDesLignes & "+D" & 4 + idecalageDesLignes & "+F" & 4 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColTotal).Formula = "=(J" & 4 + idecalageDesLignes & "/12)*$A$" & 13 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColTotal + 1).Formula = "=C" & 5 + idecalageDesLignes & "+E" & 5 + idecalageDesLignes & "+G" & 5 + idecalageDesLignes
        wsMain.Cells(iLigImpotTaxe, iColTotal).Formula = "=J" & 5 + idecalageDesLignes & "*$A$" & 15 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigImpotTaxe, iColTotal + 1).Formula = "=C" & 6 + idecalageDesLignes & "+E" & 6 + idecalageDesLignes & "+G" & 6 + idecalageDesLignes
        wsMain.Cells(iLigSalaireChargeBU, iColTotal).Formula = "=J" & 5 + idecalageDesLignes & "*42/100"
        wsMain.Cells(iLigSalaireChargeBU, iColTotal + 1).Formula = "=C" & 7 + idecalageDesLignes & "+E" & 7 + idecalageDesLignes & "+G" & 7 + idecalageDesLignes
        wsMain.Cells(iLigFraisStructure, iColTotal).Formula = "=J" & 5 + idecalageDesLignes & "*$A$" & 17 + idecalageDesLignes & "/100"
        wsMain.Cells(iLigFraisStructure, iColTotal + 1).Formula = "=C" & 8 + idecalageDesLignes & "+E" & 8 + idecalageDesLignes & "+G" & 8 + idecalageDesLignes
        wsMain.Cells(iLigREX, iColTotal).Formula = "=J" & 5 + idecalageDesLignes & "-(J" & 6 + idecalageDesLignes & "+J" & 7 + idecalageDesLignes & "+J" & 8 + idecalageDesLignes & ")"
        wsMain.Cells(iLigREX, iColTotal + 1).Formula = "=C" & 9 + idecalageDesLignes & "+E" & 9 + idecalageDesLignes & "+G" & 9 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColTotal).Formula = "=J" & 7 + idecalageDesLignes & "/J" & 5 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColTotal + 1).Formula = "=K" & 7 + idecalageDesLignes & "/K" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColTotal).Formula = "=J" & 9 + idecalageDesLignes & "/J" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColTotal + 1).Formula = "=K" & 9 + idecalageDesLignes & "/K" & 5 + idecalageDesLignes

        ' Calcul conso
        wsMain.Cells(iLigPctCAHTPrevu, iColConso + 1).Formula = "=N" & 5 + idecalageDesLignes & "/M" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAAnnuelPrevi, iColConso).Formula = "=J" & 4 + idecalageDesLignes & "+H" & 4 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColConso).Formula = "=J" & 5 + idecalageDesLignes & "+H" & 5 + idecalageDesLignes
        wsMain.Cells(iLigCAGlobal, iColConso + 1).Formula = "=K" & 5 + idecalageDesLignes & "+I" & 5 + idecalageDesLignes
        wsMain.Cells(iLigImpotTaxe, iColConso).Formula = "=J" & 6 + idecalageDesLignes & "+H" & 6 + idecalageDesLignes
        wsMain.Cells(iLigImpotTaxe, iColConso + 1).Formula = "=K" & 6 + idecalageDesLignes & "+I" & 6 + idecalageDesLignes
        wsMain.Cells(iLigSalaireChargeBU, iColConso).Formula = "=J" & 7 + idecalageDesLignes & "+H" & 7 + idecalageDesLignes
        wsMain.Cells(iLigSalaireChargeBU, iColConso + 1).Formula = "=K" & 7 + idecalageDesLignes & "+I" & 7 + idecalageDesLignes
        wsMain.Cells(iLigFraisStructure, iColConso).Formula = "=J" & 8 + idecalageDesLignes & "+H" & 8 + idecalageDesLignes
        wsMain.Cells(iLigFraisStructure, iColConso + 1).Formula = "=K" & 8 + idecalageDesLignes & "+I" & 8 + idecalageDesLignes
        wsMain.Cells(iLigREX, iColConso).Formula = "=J" & 9 + idecalageDesLignes & "+H" & 9 + idecalageDesLignes
        wsMain.Cells(iLigREX, iColConso + 1).Formula = "=K" & 9 + idecalageDesLignes & "+I" & 9 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColConso).Formula = "=M" & 7 + idecalageDesLignes & "/M" & 5 + idecalageDesLignes
        wsMain.Cells(iLigMasseSalarialeSurCA, iColConso + 1).Formula = "=N" & 7 + idecalageDesLignes & "/N" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColConso).Formula = "=M" & 9 + idecalageDesLignes & "/M" & 5 + idecalageDesLignes
        wsMain.Cells(iLigResultatExploitation, iColConso + 1).Formula = "=N" & 9 + idecalageDesLignes & "/N" & 5 + idecalageDesLignes


        ' Mise en forme de la feuille Excel
        ' 1) Format des cellules 
        ' 1-a) Pourcentage
        wsMain.Cells(iLigPctCAHTPrevu, iColSTJ + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColICPE + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColMDP + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColPORZO + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColTotal + 1).Style.NumberFormat = "#0.##%"
        wsMain.Cells(iLigPctCAHTPrevu, iColConso + 1).Style.NumberFormat = "#0.##%"

        wsMain.Rows(iLigMasseSalarialeSurCA).Style.NumberFormat = "0.00%"
        wsMain.Rows(iLigResultatExploitation).Style.NumberFormat = "0.00%"
        wsMain.Rows(iLigOutilsAnneeN).Style.NumberFormat = "0"
        wsMain.Rows(iLigCAHTAnneeN).Style.NumberFormat = "0"
        For j = 1 To 15

            '1-b) On arrondit à l'entier le plus proche
            For i As Integer = 3 + idecalageDesLignes To 8 + idecalageDesLignes
                wsMain.Cells(i, j).Style.NumberFormat = "0"
            Next
        Next

        ' 2) Alternance par ligne de 2 niveaux de gris
        For i As Integer = 2 + idecalageDesLignes To 8 + idecalageDesLignes

            If i Mod 2 = 0 Then
                wsMain.Cells(i, 0).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(242, 242, 242), Drawing.Color.Beige)
            Else
                wsMain.Cells(i, 0).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(216, 216, 216), Drawing.Color.Beige)
            End If
        Next

        For i As Integer = 19 + idecalageDesLignes To 22 + idecalageDesLignes
            For j As Integer = 0 To iColConso + 2
                If i Mod 2 = 0 Then
                    wsMain.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(216, 216, 216), Drawing.Color.Beige)
                Else
                    wsMain.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(242, 242, 242), Drawing.Color.Beige)
                End If
            Next
        Next
        ' Mise en couleur de stj, ICPE...
        For i As Integer = 0 + idecalageDesLignes To 8 + idecalageDesLignes
            For j As Integer = 1 To 2
                wsMain.Cells(i, j).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(230, 184, 183), Drawing.Color.Beige)
                wsMain.Cells(i, j + 2).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(149, 179, 215), Drawing.Color.Beige)
                wsMain.Cells(i, j + 4).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(146, 208, 80), Drawing.Color.Beige)
                wsMain.Cells(i, j + 6).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(191, 191, 191), Drawing.Color.Beige)
                wsMain.Cells(i, j + 8).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 102), Drawing.Color.Beige)
                wsMain.Cells(i, j + 11).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(242, 220, 219), Drawing.Color.Beige)
            Next
        Next
        wsMain.Cells(1 + idecalageDesLignes, 11).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 0), Drawing.Color.Beige)
        wsMain.Cells(1 + idecalageDesLignes, 14).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(230, 184, 183), Drawing.Color.Beige)
        For i As Integer = 4 + idecalageDesLignes To 8 + idecalageDesLignes
            wsMain.Cells(i, 11).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 204, 102), Drawing.Color.Beige)
            wsMain.Cells(i, 14).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(230, 184, 183), Drawing.Color.Beige)
        Next
        ' 3) Mise en place des bordures pour plus de lisibilité (entre auutres séparation des BU)
        wsMain.Cells.GetSubrange("A" & 3 + idecalageDesLignes, "A" & 9 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("B" & 1 + idecalageDesLignes, "C" & 2 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("B" & 3 + idecalageDesLignes, "C" & 9 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("D" & 1 + idecalageDesLignes, "E" & 2 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("D" & 3 + idecalageDesLignes, "E" & 9 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("F" & 1 + idecalageDesLignes, "G" & 2 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("F" & 3 + idecalageDesLignes, "G" & 9 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("H" & 1 + idecalageDesLignes, "I" & 2 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("H" & 3 + idecalageDesLignes, "I" & 9 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("J" & 1 + idecalageDesLignes, "L" & 2 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("J" & 3 + idecalageDesLignes, "L" & 9 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("M" & 1 + idecalageDesLignes, "O" & 2 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("M" & 3 + idecalageDesLignes, "O" & 9 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)

        wsMain.Cells.GetSubrange("A" & 12 + idecalageDesLignes, "A" & 17 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("A" & 20 + idecalageDesLignes, "A" & 23 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)

        wsMain.Cells.GetSubrange("B" & 20 + idecalageDesLignes, "C" & 23 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("D" & 20 + idecalageDesLignes, "E" & 23 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("F" & 20 + idecalageDesLignes, "G" & 23 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("H" & 20 + idecalageDesLignes, "I" & 23 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("J" & 20 + idecalageDesLignes, "L" & 23 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)
        wsMain.Cells.GetSubrange("M" & 20 + idecalageDesLignes, "O" & 23 + idecalageDesLignes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Medium)

        ' On ajuste la largeur des colonnes et des lignes
        For j As Integer = 0 To 14
            wsMain.Columns(j).AutoFitAdvanced(1)
        Next

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

    Private Sub BtnExportBis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportBis.Click

        Dim oRechercheExportRex As New RechercheExportRex

        oRechercheExportRex.SaveRecherche(CDate(tbDateDeb.Text), CDate(tbDateFin.Text))
        Session("ExportRex") = oRechercheExportRex
        Response.Redirect("StatEmployeServiceV2.aspx")
    End Sub
End Class