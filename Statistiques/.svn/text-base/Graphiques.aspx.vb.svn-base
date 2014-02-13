Imports Telerik.Charting
Imports ComptaAna.Business
Imports ComptaAna.net.Droit
Imports System.Drawing
Imports Obout.ComboBox

Public Class Graphiques
    Inherits System.Web.UI.Page
    Dim dsTauxFactu As New DataSet
    Dim bCAServiceTypeGraphe As Boolean
    Dim bMasseSalarialeTypeGraphe As Boolean
    Dim bEBEServiceTypeGraphe As Boolean
    Dim bOutilsServiceTypeGraphe As Boolean
    Dim bNotPremierPassage As Boolean
    Dim oRechercheGraphSave As New RechercheGraphe
    Dim oRechercheGraphPremPge As New RechercheGraphPremPge

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oRechercheGraphPremPge = CType(Session("PremPassage"), RechercheGraphPremPge)
        oRechercheGraphSave = CType(Session("Graphe"), RechercheGraphe)
        Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
        ChargerCBBService()
        If verifDroit(lDroit, eModule.accesGraphiqueGeneral) Then
            chargerCbbEmploye(True)
        Else
            chargerCbbEmploye(False)
        End If

        If Page.IsPostBack Then

        Else
            If Not (oRechercheGraphPremPge Is Nothing) Then
                oRechercheGraphPremPge.RestaureRecherche(bNotPremierPassage)
            End If
            listRadioBoutonCAEmploye_Init()
            listRadioBoutonCAService_Init()
            cbbFiltreStatEmployeService.SelectedValue = "-1"
            cbbFiltreStatEmployeService.SelectedIndex = 0
            cbbFiltreStatGalDetail.SelectedIndex = 0
            cbbFiltreStatGalDetail.SelectedValue = "-1"
            If Not (oRechercheGraphSave Is Nothing) Then
                oRechercheGraphSave.RestaureRecherche(cbn1, cbPourcentageFormation, rcOutilsGraphe, rcOutilsGrapheNMoinsUn, cbOutils, rcRexGraphe, rcRexGrapheNMoinsUn, cbEBEService, rcPanelGraphe3, rcMasseSalarialeNMoinsUn, cbMasseSalariale, cbbFiltreStatEmployeService, cbPourcentageCAParEmploye, CheckBoxListCAEmploye, rcPanelGraphe, rcCAServiceNMoinsUn, cbPourcentageCAService, chkbCAParService, cbbFiltreStatGalDetail, tbDateDeb, tbDateFin)
            End If
            Try
                If (Not verifDroit(lDroit, eModule.accesGraphiqueGeneral)) And (Not verifDroit(lDroit, eModule.accesGraphiqueRespoBU)) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            If tbDateDeb.Text = "" Then
                If DateTime.Now.Month < 10 Then
                    tbDateDeb.Text = "01/0" & DateTime.Now.Month & "/" & DateTime.Now.Year
                Else
                    tbDateDeb.Text = "01/" & DateTime.Now.Month & "/" & DateTime.Now.Year
                End If

            End If

            End If

            If tbDateFin.Text = "" Then

                Dim dat = ReleveActivite.GetLastDayOfThisMonth
                tbDateFin.Text = CStr(dat).Substring(0, 10)

            End If

    End Sub

    Private Sub btnCalcul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcul.Click
        Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
        If Not (oRechercheGraphPremPge Is Nothing) Then
            oRechercheGraphPremPge.RestaureRecherche(bNotPremierPassage)
        End If
        ' Calcul des graphe n-1
        'Dim sAnneeDebN1, sAnneeFinN1 As Integer
        Dim dDateDebN1, dDateFinN1 As Date
        If cbn1.Checked Then
            dDateDebN1 = CDate(tbDateDeb.Text).AddYears(-1)
            dDateFinN1 = CDate(tbDateFin.Text).AddYears(-1)
            'sAnneeDebN1 = DatePart("yyyy", CDate(tbDateDeb.Text)) - 1
            'sAnneeFinN1 = DatePart("yyyy", CDate(tbDateFin.Text)) - 1

            'dDateDebN1 = CDate(sAnneeDebN1 & "/" & DatePart("m", CDate(tbDateDeb.Text)) & "/" & DatePart("d", CDate(tbDateDeb.Text)))
            'dDateFinN1 = CDate(sAnneeFinN1 & "/" & DatePart("m", CDate(tbDateFin.Text)) & "/" & DatePart("d", CDate(tbDateFin.Text)))
            GetGraphTauxFactu(dDateDebN1, dDateFinN1, rcTauxFactuMoinUn, LabTauxFactuMoinUn, True)
            GetGraphCAParService(cbPourcentageCAService.Checked, dDateDebN1, dDateFinN1, rcCAServiceNMoinsUn, LabNMoinsUn, True)
            GetGraphCAparEmploye(dDateDebN1, dDateFinN1, rcCAEmployeNMoinsUn, LabCAEmployeNMoinsUn, True)
            GetGraphMasseSalariale(dDateDebN1, dDateFinN1, rcMasseSalarialeNMoinsUn, LabMasseSalarialeNMoinsUn, True)
            GetGraphEBEParService(dDateDebN1, dDateFinN1, rcRexGrapheNMoinsUn, LabRexGrapheNMoinsUn, True)
            GetGraphOutilsParService(dDateDebN1, dDateFinN1, rcOutilsGrapheNMoinsUn, LabOutilsGrapheNMoinsUN, True)
            GetGraphMontantFormationEmploye(dDateDebN1, dDateFinN1, rcFormationGrapheNMoinsUn, LabFormationNMoinsUn, True)
        End If

        ' calcul des graphes
        GetGraphTauxFactu(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), RadChart1, LabTauxFactu, False)
        GetGraphCAParService(cbPourcentageCAService.Checked, CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcPanelGraphe, lblServiceInsuffisant, False)
        GetGraphCAparEmploye(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcCAEmploye, LabelCAEmploye, False)
        GetGraphMasseSalariale(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcPanelGraphe3, LabMasseSalariale, False)
        GetGraphEBEParService(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcRexGraphe, LabRexGraphe, False)
        GetGraphOutilsParService(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcOutilsGraphe, LabOutilsGraphe, False)
        GetGraphMontantFormationEmploye(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcFormationGraphe, LabelFormation, False)
        If bNotPremierPassage Then
            SaveState(False, False, False, False, True)
        Else
            SaveState(False, False, False, False, False)
        End If
        bNotPremierPassage = True
        oRechercheGraphPremPge = New RechercheGraphPremPge
        oRechercheGraphPremPge.SaveRecherche(bNotPremierPassage)
        Session("PremPassage") = oRechercheGraphPremPge
        ' ont rend visible les fieldset contenant les graphes
        FieldsetTauxFactu.Visible = True
        FieldsetCAParService.Visible = True
        FieldsetMasseSalarialeParService.Visible = True
        FieldEBE.Visible = True
        FieldOutils.Visible = True
        FieldsetCAEmploye.Visible = True
        FieldsetFormation.Visible = True

    End Sub
    Private Sub cbbTauxfact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbbTauxfact.Click

        GetGraphTauxFactu(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcTauxFactuMoinUn, LabTauxFactuMoinUn, True)
        GetGraphTauxFactu(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), RadChart1, LabTauxFactu, False)
        SaveState(False, False, False, False, True)
    End Sub
    Private Sub btnCABU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCABU.Click

        GetGraphCAParService(cbPourcentageCAService.Checked, CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcCAServiceNMoinsUn, LabNMoinsUn, True)
        GetGraphCAParService(cbPourcentageCAService.Checked, CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcPanelGraphe, lblServiceInsuffisant, False)
        SaveState(False, False, False, False, True)
    End Sub
    Private Sub btnMSBU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMSBU.Click

        GetGraphMasseSalariale(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcMasseSalarialeNMoinsUn, LabMasseSalarialeNMoinsUn, True)
        GetGraphMasseSalariale(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcPanelGraphe3, LabMasseSalariale, False)
        SaveState(False, False, False, False, True)
    End Sub

    Private Sub btnEBE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEBE.Click

        GetGraphEBEParService(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcRexGrapheNMoinsUn, LabRexGrapheNMoinsUn, True)
        GetGraphEBEParService(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcRexGraphe, LabRexGraphe, False)
        SaveState(False, False, False, False, True)
    End Sub

    Private Sub btnOutils_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOutils.Click
        

        GetGraphOutilsParService(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcOutilsGrapheNMoinsUn, LabOutilsGrapheNMoinsUN, True)
        GetGraphOutilsParService(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcOutilsGraphe, LabOutilsGraphe, False)
        SaveState(False, False, False, False, True)
    End Sub

    Private Sub btnCAEmploye_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCAEmploye.Click

        GetGraphCAparEmploye(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcCAEmployeNMoinsUn, LabCAEmployeNMoinsUn, True)
        GetGraphCAparEmploye(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcCAEmploye, LabelCAEmploye, False)

        SaveState(False, False, False, False, True)
    End Sub

    Private Sub btnFormation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFormation.Click

        GetGraphMontantFormationEmploye(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcFormationGrapheNMoinsUn, LabFormationNMoinsUn, True)
        GetGraphMontantFormationEmploye(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcFormationGraphe, LabelFormation, False)

        SaveState(False, False, False, False, True)
    End Sub

    Protected Sub rcPanelGraphe_ItemDataBound(ByVal sender As Object, ByVal e As ChartItemDataBoundEventArgs)
        If rcPanelGraphe.Chart.Series.GetSeries(0).Items.Contains(e.SeriesItem) Then 'GetSeries(0)
            e.SeriesItem.Name = CType(e.DataItem, DataRowView)("Objet").ToString()
        ElseIf rcPanelGraphe.Chart.Series.GetSeries(1).Items.Contains(e.SeriesItem) Then
            e.SeriesItem.Name = "Objectif " & CType(e.DataItem, DataRowView)("Objet").ToString() '
        End If
    End Sub
    Protected Sub rcPanelGrapheNMoinsUn_ItemDataBound(ByVal sender As Object, ByVal e As ChartItemDataBoundEventArgs)
        If rcCAServiceNMoinsUn.Chart.Series.GetSeries(0).Items.Contains(e.SeriesItem) Then 'GetSeries(0)
            e.SeriesItem.Name = CType(e.DataItem, DataRowView)("Objet").ToString()
        ElseIf rcCAServiceNMoinsUn.Chart.Series.GetSeries(1).Items.Contains(e.SeriesItem) Then
            e.SeriesItem.Name = "Objectif " & CType(e.DataItem, DataRowView)("Objet").ToString() '
        End If
    End Sub

    Protected Sub rcPanelGraph_ItemDataBound(ByVal sender As Object, ByVal e As ChartItemDataBoundEventArgs)
        e.SeriesItem.Name = CType(e.DataItem, DataRowView)("Service").ToString()
    End Sub

    Public Function MaxDate(ByVal d1 As Object, ByVal d2 As Object) As Date
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
    Public Function MinDate(ByVal d1 As Object, ByVal d2 As Object) As Date
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
    Public Sub GetGraphTauxFactu(ByVal dDateDeb As Date, ByVal dDateFin As Date, ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label, ByVal bNmoinsUn As Boolean)

        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsEmploye As New DataSet
        Dim dsResultat As New DataSet
        Dim dResult As New Double
        'création d'une table
        dsResultat.Tables.Add("Employe")
        'ajout des colone du DataSet
        dsResultat.Tables("Employe").Columns.Add("Nom", GetType(String))
        dsResultat.Tables("Employe").Columns.Add("EmployeID", GetType(Integer))
        dsResultat.Tables("Employe").Columns.Add("TauxFactu", GetType(Double))
        'récupération des employés facturable
        If (cbbFiltreStatGalDetail.Items(cbbFiltreStatGalDetail.SelectedIndex).Value = "") Then
            dsEmploye = oEmployeDAO.GetAllEmployeFacturable(dDateDeb, dDateFin, -1)
        Else
            dsEmploye = oEmployeDAO.GetAllEmployeFacturable(dDateDeb, dDateFin, CInt(cbbFiltreStatGalDetail.Items(cbbFiltreStatGalDetail.SelectedIndex).Value))
        End If

        If dsEmploye.Tables(0).Rows.Count = 1 Or dsEmploye.Tables(0).Rows.Count = 0 Then
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                label.Text = "Les données  sur cette période sont insuffisantes pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            End If
        Else
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                RcGraphe.Visible = False
                label.Font.Bold = True
                label.ForeColor = Color.Red
            Else
                'construction de la table résultat
                For Each row As DataRow In dsEmploye.Tables(0).Rows
                    dResult = oStatistiquesDAO.GetEmployeQteProduiteFacturable(dDateDeb, dDateFin, CInt(row(0)))
                    If dResult > 0 Then
                        'J'ai pas trouver comment savoir si un employé était commercial dans la base.Tous ceux qui sont a 0 on ne les affiches pas.
                        dsResultat.Tables("Employe").Rows.Add(row(1), row(0), dResult)
                    End If
                Next
            LabTauxFactu.Text = ""
            RcGraphe.DataSource = dsResultat
            RcGraphe.DataBind()
            RcGraphe.Height = (dsResultat.Tables(0).Rows.Count * 15) + 100
            'coloration de la serie
            Dim barColors As Color() = New Color(0) {Color.Red}
            For Each item As ChartSeriesItem In RcGraphe.Series(0).Items
                item.Appearance.FillStyle.MainColor = barColors(0)
                item.Appearance.FillStyle.SecondColor = barColors(0)
            Next
            RcGraphe.Visible = True
        End If
        End If

    End Sub

    Public Sub GetGraphCAParService(ByVal bPourcentage As Boolean, ByVal dDateDeb As Date, ByVal dDateFin As Date, ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label, ByVal bNmoinsUn As Boolean)
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim listID As New List(Of String)
        listID.Clear()

        For Each I As ListItem In chkbCAParService.Items

            If I.Selected Then
                listID.Add(I.Value)
            End If

        Next
        Dim ds As DataSet = oStatistiquesDAO.SelectStatGeneralesParServiceGraphe(dDateDeb, dDateFin, False, False, -1, -1, "", listID)
        If ds.Tables(0).Rows.Count = 0 Then
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                label.Text = "Le nombre de service sur cette période est insuffisant pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            End If
        Else
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else


                lblServiceInsuffisant.Text = ""
                'Objectif par service pour n mois
                Dim oServiceDAO As New CServiceDAO
                Dim dsServices As DataSet = oServiceDAO.GetAllServiceCAService(listID)   'GetAllServiceToList()
                ds.Tables(0).Columns.Add("Objectif", GetType(Double))
                Dim lDureePeriode As Long = DateDiff("m", dDateDeb, dDateFin) + 1
                For t = 0 To ds.Tables(0).Rows.Count - 1
                    For j = 0 To ds.Tables(0).Rows.Count - 1
                        If CStr(ds.Tables(0).Rows(t).Item("Objet")) = CStr(dsServices.Tables(0).Rows(j).Item("ServiceLibelle")) Then
                            ds.Tables(0).Rows(t).Item("Objectif") = Format(CDbl(dsServices.Tables(0).Rows(j).Item("ServiceObjectif")) * (lDureePeriode / 12), ".00")
                        End If
                    Next
                Next
                '---------------------------------------------------------------------------------------------------------------------------
                If bPourcentage Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        row("CAHT") = FormatNumber((CDec(row("CAHT")) / CDec(row("Objectif"))) * 100, 2)
                        row("Objectif") = FormatNumber(100, 2)
                    Next
                End If

                RcGraphe.DataSource = ds
                RcGraphe.DataBind()
                RcGraphe.Visible = True

                Dim barColors As Color() = New Color(19) {Color.Red, Color.Aqua, Color.GreenYellow, Color.Coral, Color.CornflowerBlue, Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid, Color.DarkRed, Color.DarkSalmon, Color.DarkSeaGreen, Color.DarkSlateBlue, Color.Cornsilk}
                Dim i As Integer = 0
                'Dim z As Integer = RcGraphe.Series(0).Items.Count
                For Each item As ChartSeriesItem In RcGraphe.Series(0).Items
                    If i = 20 Then
                        i = 0
                    End If
                    item.Appearance.FillStyle.MainColor = barColors(i)
                    item.Appearance.FillStyle.SecondColor = barColors(i)
                    RcGraphe.Series(1).Items(i).Appearance.FillStyle.MainColor = barColors(i)
                    'RcGraphe.Series(1).Items(i).Appearance.FillStyle.SecondColor = barColors(i)
                    i += 1
                    'z += 1

                Next
                'RcGraphe.Width = (ds.Tables(0).Rows.Count * 80) + 400
            End If
        End If
    End Sub
    Public Sub GetGraphMasseSalariale(ByVal dDateDeb As Date, ByVal dDateFin As Date, ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label, ByVal bNmoinsUn As Boolean)
        Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsEmployes As DataSet = oEmployeDAO.GetNomPrenomEmploye(CInt(cbbFiltreStatEmployeService.SelectedValue))
        Dim oServiceDAO As New CServiceDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceMasseSalariale()
        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count
        '---------------------------------------------------------------------------------
        Dim htService As New Hashtable
        Dim dsService As DataSet = oServiceDAO.GetAllServiceID()
        For Each row As DataRow In dsService.Tables(0).Rows
            htService.Add(row(0), 0)
        Next
        '---------------------------------------------------------------------------------
        Dim htCAHTEmployes As Hashtable = oStatistiquesDAO.GetCAHTParEmploye(dDateDeb, dDateFin)
        ' Parcours des services
        Dim lDureePeriode As Long
        ' Parcours des employés
        For Each rowEmploye As DataRow In dsEmployes.Tables(0).Rows
            Dim lEmployeId As Long = CLng(rowEmploye("EmployeId"))
            Dim dCoutPeriode As Double = 0
            Dim dsCouts As DataSet
            Dim dTot As Double = 0
            Dim dTmp As Double = 0
            Dim dCoutAxe As Double
            dsCouts = oEmployeDAO.GetEmployeCoutPeriode(dDateDeb, dDateFin, lEmployeId)
            If dsCouts.Tables(0).Rows.Count > 0 Then
                'on a au moins un cout dans la liste, donc on ajoute les infos de l'employé

                For Each rowCouts As DataRow In dsCouts.Tables(0).Rows
                    With rowCouts
                        ' On calcule la date de la rentabilité pour la période trouvée
                        Dim dDateDebutRent As Date
                        Dim dDateFinRent As Date

                        dDateDebutRent = MaxDate(dDateDeb, .Item("EmployeCoutDateDebut"))
                        dDateFinRent = MinDate(dDateFin, .Item("EmployeCoutDateFin"))

                        ' Calcul du coût de la personne sur la période
                        lDureePeriode = DateDiff(DateInterval.Month, dDateDebutRent, dDateFinRent) + 1                              'ETRANGE
                        dCoutPeriode = CDbl(IIf(IsDBNull(.Item("EmployeCoutCout")), 0, .Item("EmployeCoutCout"))) * lDureePeriode   'PAS SUPER PRECIS ENFIN JE CROIS
                        dTot = CDbl(htCAHTEmployes(lEmployeId))
                        ' TODO : coût Axe, Soufflot et Save à rendre dynamique
                        dCoutAxe = dCoutPeriode * 1

                        If CInt(.Item("EmployeCoutFacturable")) = 0 Then
                            If dTot <> 0 Then
                                ' Calcul du CA par BU
                                For Each rowService As DataRow In dsServices.Tables(0).Rows
                                    dTmp = CDbl(oStatistiquesDAO.SelectStatEmployeService(dDateDebutRent, dDateFinRent, CInt(lEmployeId), CInt(rowService("ServiceId"))))
                                    dTmp = CDbl(Format(dTmp / dTot * dCoutAxe, ".00"))
                                    'tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices) = (CDbl(tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices)) + dTmp).ToString
                                    '------------------------------------------------------------------------------------------------------------------------------------------------------------
                                    htService.Item(CInt(rowService(0))) = CDbl(htService.Item(CInt(rowService(0)))) + dTmp

                                Next

                            End If

                        Else

                            For Each rowService As DataRow In dsServices.Tables(0).Rows

                                Dim dsCoutService As DataSet = oEmployeDAO.GetEmployeCoutService(CLng(.Item("EmployeCoutID")), CLng(rowService("ServiceID")))

                                If dsCoutService.Tables(0).Rows.Count > 0 Then
                                    dTmp = dCoutAxe * CDbl(dsCoutService.Tables(0).Rows(0)("Repartition")) / 100.0#

                                    'tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices) = (CDbl(tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices)) + dTmp).ToString

                                    '------------------------------------------------------------------------------------------------------------------------------------------------------------
                                    htService.Item(CInt(rowService(0))) = CDbl(htService.Item(CInt(rowService(0)))) + dTmp

                                End If
                            Next
                        End If
                    End With
                Next
            End If
        Next

        ' Fermeture des datasets et de la connexion
        dsEmployes.Dispose()
        dsServices.Dispose()
        Dim dsResultat As New DataSet
        'création d'une table
        dsResultat.Tables.Add("Res")
        'ajout des colone du DataSet
        dsResultat.Tables("Res").Columns.Add("Service", GetType(String))
        dsResultat.Tables("Res").Columns.Add("Sum", GetType(Double))
        Dim sum As New Decimal
        For Each key In htService.Keys
            'on remplis une ligne
            sum += CDec(htService.Item(key))
            dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), htService.Item(key))
        Next
        If cbMasseSalariale.Checked Then
            If sum > 0 Then
                For Each row As DataRow In dsResultat.Tables(0).Rows
                    row("Sum") = FormatNumber(CDec(row("Sum")) / sum * 100, 2)
                Next
                label.Text = ""
                label.Font.Bold = True
                label.ForeColor = Color.Red
            End If
        End If

        If bNmoinsUn And Not (cbn1.Checked) Then
            RcGraphe.Visible = False
        Else
            If sum <= 0 Then
                RcGraphe.Visible = False
                label.Text = " donnée insufisantes"
                label.Font.Bold = True
                label.ForeColor = Color.Red
            Else
                label.Text = ""
                RcGraphe.DataSource = dsResultat
                RcGraphe.DataBind()
                'rcPanelGraphe2.Chart.Series.GetSeries(0).Type= ChartSeriesType.Bar
                RcGraphe.Visible = True
                'coloration de la serie
                Dim barColors As Color() = New Color(19) {Color.Red, Color.Aqua, Color.GreenYellow, Color.Coral, Color.CornflowerBlue, Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid, Color.DarkRed, Color.DarkSalmon, Color.DarkSeaGreen, Color.DarkSlateBlue, Color.Cornsilk}
                Dim i As Integer = 0
                For Each item As ChartSeriesItem In RcGraphe.Series(0).Items
                    If i = 20 Then
                        i = 0
                    End If
                    item.Appearance.FillStyle.MainColor = barColors(i)
                    item.Appearance.FillStyle.SecondColor = barColors(i)
                    i += 1
                Next
            End If
        End If
    End Sub
    Public Sub ChargerCBBService()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        cbbFiltreStatGalDetail.Items.Clear()
        Dim dsEmploye As DataSet = oStatistiquesDAO.GetAllServiceBis()
        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les services"
        itemDefaut.Value = "-1"
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
    ''' charger la liste des employes
    ''' </summary> 
    Public Sub chargerCbbEmploye(ByVal Bool As Boolean)
        If Bool Then
            'Cas droit Général
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
            cbbFiltreStatEmployeService.Visible = True
        Else
            cbbFiltreStatEmployeService.Visible = False
            cbbFiltreStatEmployeService.SelectedIndex = 0
            cbbFiltreStatEmployeService.SelectedValue = "-1"
        End If
        cbbFiltreStatEmployeService.DataBind()
    End Sub
    Private Sub listRadioBoutonCAService_Init()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        chkbCAParService.Items.Clear()
        Dim dsEmploye As DataSet = oStatistiquesDAO.GetAllServiceBis()
        For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsEmploye.Tables(0).Rows(i)("ServiceLibelle").ToString
            Dim item As New ListItem
            item.Text = cbbText
            item.Value = dsEmploye.Tables(0).Rows(i)("ServiceID").ToString
            item.Selected = True
            chkbCAParService.Items.Add(item)
        Next
        chkbCAParService.DataBind()
    End Sub
    Private Sub listRadioBoutonCAEmploye_Init()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO
        CheckBoxListCAEmploye.Items.Clear()
        Dim dsEmploye As DataSet = oStatistiquesDAO.GetAllServiceBis()

        For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsEmploye.Tables(0).Rows(i)("ServiceLibelle").ToString
            Dim item As New ListItem
            item.Text = cbbText
            item.Value = dsEmploye.Tables(0).Rows(i)("ServiceID").ToString
            item.Selected = True
            CheckBoxListCAEmploye.Items.Add(item)
        Next
        CheckBoxListCAEmploye.DataBind()
    End Sub

    Private Sub btnRenitialiser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenitialiser.Click
        For Each I As ListItem In chkbCAParService.Items
            I.Selected = True
        Next
    End Sub

    Private Sub btnCheckBoxCAEmploye_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckBoxCAEmploye.Click
        For Each I As ListItem In CheckBoxListCAEmploye.Items
            I.Selected = True
        Next
    End Sub
    Public Sub GetGraphEBEParService(ByVal dDateDeb As Date, ByVal dDateFin As Date, ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label, ByVal bNmoinsUn As Boolean)
        Dim oServiceDAO As New CServiceDAO
        'Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceMasseSalariale()

        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim dsCAServices As New DataSet
        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim htCAServices As New Hashtable
        Dim htProduitsHorsDepassementBudget As New Hashtable

        If dsServices.Tables(0).Rows.Count = 1 Or dsServices.Tables(0).Rows.Count = 0 Then
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                label.ForeColor = Color.Red
                RcGraphe.Visible = False

            Else
                dsServices = New DataSet()
                label.Text = "Le nombre de service sur cette période est insuffisant pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            End If
        Else
            label.Text = ""
            For i As Integer = 0 To iNbServices - 1
                Try
                    With dsServices.Tables(0).Rows(i)
                        dsCAServices = oStatistiquesDAO.SelectStatGeneralesParService(dDateDeb, dDateFin, False, False, CInt(.Item("ServiceId")), 0, -1, "")
                        htCAServices.Add(dsCAServices.Tables(0).Rows(0)("ID"), dsCAServices.Tables(0).Rows(0)("CAHT"))
                        htProduitsHorsDepassementBudget.Add(dsCAServices.Tables(0).Rows(0)("ID"), oProduitAffaireDAO.GetProduitsHorsDepassementBudget(dDateDeb, dDateFin, CLng(.Item("ServiceId"))))
                    End With
                Catch
                    'dsServices = New DataSet()
                    'label.Text = "Le nombre de service sur cette période est insuffisant pour faire un graphique."
                    'label.Font.Bold = True
                    'RcGraphe.Visible = False
                    'label.ForeColor = Color.Red
                End Try

            Next

            Dim htSalaireschargesParBU As Hashtable = SalaireschargesParBU(dDateDeb, dDateFin)
            Dim dsResultat As New DataSet
            'création d'une table
            dsResultat.Tables.Add("Res")
            'ajout des colone du DataSet
            dsResultat.Tables("Res").Columns.Add("Service", GetType(String))
            dsResultat.Tables("Res").Columns.Add("Rex", GetType(Double))
            Dim sum As New Double
            For Each key In htSalaireschargesParBU.Keys
                'on remplis une ligne
                'dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), htService.Item(key))
                Dim CA = CDbl(htProduitsHorsDepassementBudget.Item(key)) + CDbl(htCAServices.Item(key))

                Dim rex As Double = CDbl(FormatNumber(CA - (CA * 2 / 100 + CDbl(htSalaireschargesParBU.Item(key)) + CA * 31 / 100), 2))
                If rex <> 0 Then
                    dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), rex)
                    sum += rex
                End If
            Next
        If dsResultat.Tables("res").Rows.Count > 1 Then
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                If cbEBEService.Checked Then

                    For Each row As DataRow In dsResultat.Tables(0).Rows
                        row("Rex") = FormatNumber(CDec(row("Rex")) / sum * 100, 2)
                    Next
                End If
                RcGraphe.DataSource = dsResultat
                RcGraphe.DataBind()
                RcGraphe.Visible = True

                'coloration de la serie
                Dim barColors As Color() = New Color(19) {Color.Red, Color.Aqua, Color.GreenYellow, Color.Coral, Color.CornflowerBlue, Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid, Color.DarkRed, Color.DarkSalmon, Color.DarkSeaGreen, Color.DarkSlateBlue, Color.Cornsilk}
                Dim i As Integer = 0
                For Each item As ChartSeriesItem In RcGraphe.Series(0).Items
                    If i = 20 Then
                        i = 0
                    End If
                    item.Appearance.FillStyle.MainColor = barColors(i)
                    item.Appearance.FillStyle.SecondColor = barColors(i)
                    i += 1
                Next
            End If
        Else
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
            Else
                dsServices = New DataSet()
                label.Text = "Le nombre de service sur cette période est insuffisant pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            End If
        End If
        End If
    End Sub

    Private Function SalaireschargesParBU(ByVal dDateDeb As Date, ByVal dDateFin As Date) As Hashtable
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsEmployes As DataSet = oEmployeDAO.GetNomPrenomEmploye(-1) ' CInt(cbbFiltreStatEmployeService.SelectedValue)
        Dim oServiceDAO As New CServiceDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        'Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceMasseSalariale()

        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count
        '---------------------------------------------------------------------------------
        Dim htService As New Hashtable
        Dim dsService As DataSet = oServiceDAO.GetAllServiceID()
        For Each row As DataRow In dsService.Tables(0).Rows
            htService.Add(row(0), 0)
        Next
        '---------------------------------------------------------------------------------
        Dim htCAHTEmployes As Hashtable = oStatistiquesDAO.GetCAHTParEmploye(dDateDeb, dDateFin)
        ' Parcours des services
        Dim lDureePeriode As Long
        ' Parcours des employés
        For Each rowEmploye As DataRow In dsEmployes.Tables(0).Rows
            Dim lEmployeId As Long = CLng(rowEmploye("EmployeId"))
            Dim dCoutPeriode As Double = 0
            Dim dsCouts As DataSet
            Dim dTot As Double = 0
            Dim dTmp As Double = 0
            Dim dCoutAxe As Double

            dsCouts = oEmployeDAO.GetEmployeCoutPeriode(dDateDeb, dDateFin, lEmployeId)
            If dsCouts.Tables(0).Rows.Count > 0 Then
                'on a au moins un cout dans la liste, donc on ajoute les infos de l'employé
                For Each rowCouts As DataRow In dsCouts.Tables(0).Rows
                    With rowCouts
                        ' On calcule la date de la rentabilité pour la période trouvée
                        Dim dDateDebutRent As Date
                        Dim dDateFinRent As Date
                        dDateDebutRent = MaxDate(dDateDeb, .Item("EmployeCoutDateDebut"))
                        dDateFinRent = MinDate(dDateFin, .Item("EmployeCoutDateFin"))
                        ' Calcul du coût de la personne sur la période
                        lDureePeriode = DateDiff(DateInterval.Month, dDateDebutRent, dDateFinRent) + 1                              'ETRANGE
                        dCoutPeriode = CDbl(IIf(IsDBNull(.Item("EmployeCoutCout")), 0, .Item("EmployeCoutCout"))) * lDureePeriode   'PAS SUPER PRECIS ENFIN JE CROIS
                        dTot = CDbl(htCAHTEmployes(lEmployeId))
                        ' TODO : coût Axe, Soufflot et Save à rendre dynamique
                        dCoutAxe = dCoutPeriode * 1

                        If CInt(.Item("EmployeCoutFacturable")) = 0 Then
                            If dTot <> 0 Then
                                ' Calcul du CA par BU
                                For Each rowService As DataRow In dsServices.Tables(0).Rows
                                    dTmp = CDbl(oStatistiquesDAO.SelectStatEmployeService(dDateDebutRent, dDateFinRent, CInt(lEmployeId), CInt(rowService("ServiceId"))))

                                    dTmp = CDbl(Format(dTmp / dTot * dCoutAxe, ".00"))

                                    'tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices) = (CDbl(tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices)) + dTmp).ToString
                                    '------------------------------------------------------------------------------------------------------------------------------------------------------------
                                    htService.Item(CInt(rowService(0))) = CDbl(htService.Item(CInt(rowService(0)))) + dTmp

                                Next

                            End If

                        Else

                            For Each rowService As DataRow In dsServices.Tables(0).Rows

                                Dim dsCoutService As DataSet = oEmployeDAO.GetEmployeCoutService(CLng(.Item("EmployeCoutID")), CLng(rowService("ServiceID")))

                                If dsCoutService.Tables(0).Rows.Count > 0 Then
                                    dTmp = dCoutAxe * CDbl(dsCoutService.Tables(0).Rows(0)("Repartition")) / 100.0#

                                    'tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices) = (CDbl(tb(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices)) + dTmp).ToString

                                    '------------------------------------------------------------------------------------------------------------------------------------------------------------
                                    htService.Item(CInt(rowService(0))) = CDbl(htService.Item(CInt(rowService(0)))) + dTmp

                                End If
                            Next

                        End If

                    End With
                Next

            End If

        Next

        ' Fermeture des datasets et de la connexion
        dsEmployes.Dispose()
        dsServices.Dispose()

        Return htService
    End Function

    Protected Sub rcRexGraphe_ItemDataBound(ByVal sender As Object, ByVal e As ChartItemDataBoundEventArgs)
        e.SeriesItem.Name = CType(e.DataItem, DataRowView)("Service").ToString()
    End Sub

    Protected Sub rcOutilsGraphe_ItemDataBound(ByVal sender As Object, ByVal e As ChartItemDataBoundEventArgs)
        e.SeriesItem.Name = CType(e.DataItem, DataRowView)("Service").ToString()
    End Sub
    Public Sub GetGraphOutilsParService(ByVal dDateDeb As Date, ByVal dDateFin As Date, ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label, ByVal bNmoinsUn As Boolean)
        Dim oServiceDAO As New CServiceDAO
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim dsCAServices As New DataSet
        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim htCAServices As New Hashtable
        Dim htProduitsHorsDepassementBudget As New Hashtable

        For i As Integer = 0 To iNbServices - 1
            Try
                With dsServices.Tables(0).Rows(i)
                    dsCAServices = oStatistiquesDAO.SelectStatGeneralesParService(dDateDeb, dDateFin, False, False, CInt(.Item("ServiceId")), 0, -1, "")
                    htCAServices.Add(dsCAServices.Tables(0).Rows(0)("ID"), dsCAServices.Tables(0).Rows(0)("CAHT"))
                    htProduitsHorsDepassementBudget.Add(dsCAServices.Tables(0).Rows(0)("ID"), oProduitAffaireDAO.GetProduitsHorsDepassementBudget(dDateDeb, dDateFin, CLng(.Item("ServiceId"))))
                End With
            Catch
                dsServices = New DataSet()
                label.Text = "Le nombre de service sur cette période est insuffisant pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            End Try
        Next

        Dim htSalaireschargesParBU As Hashtable = SalaireschargesParBU(dDateDeb, dDateFin)

        Dim dsResultat As New DataSet

        'création d'une table
        dsResultat.Tables.Add("Res")
        'ajout des colone du DataSet
        dsResultat.Tables("Res").Columns.Add("Service", GetType(String))
        dsResultat.Tables("Res").Columns.Add("Rex", GetType(Double))

        For Each key In htSalaireschargesParBU.Keys
            'on remplis une ligne

            'dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), htService.Item(key))
            Dim CA = CDbl(htProduitsHorsDepassementBudget.Item(key)) + CDbl(htCAServices.Item(key))
            Dim rex = CA - (CA * 2 / 100 + CDbl(htSalaireschargesParBU.Item(key)) + CA * 31 / 100)
            If rex > 0 Then
                dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), rex)
            End If
        Next
        '-------------------------------------------------- PARTIE OUTILS QUI UTILISE CE QUI EST AU DESSUS---------------------------------------
        Dim dsResultatOutils As New DataSet

        'création d'une table
        dsResultatOutils.Tables.Add("Res")
        'ajout des colone du DataSet
        dsResultatOutils.Tables("Res").Columns.Add("Service", GetType(String))
        dsResultatOutils.Tables("Res").Columns.Add("Outils", GetType(Double))
        Dim Sum As Double = 0
        For Each key In htSalaireschargesParBU.Keys
            'on remplis une ligne

            Dim Outils As Double = CDbl(htProduitsHorsDepassementBudget.Item(key))
            If Outils > 0 Then
                Sum += Outils
                dsResultatOutils.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), Outils)
            End If
        Next
        If dsResultatOutils.Tables("res").Rows.Count > 1 Then
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                If cbOutils.Checked Then

                    For Each row As DataRow In dsResultatOutils.Tables("Res").Rows
                        row("Outils") = FormatNumber(CDec(row("Outils")) / Sum * 100, 2)
                    Next
                End If

                RcGraphe.DataSource = dsResultatOutils
                RcGraphe.DataBind()
                label.Text = ""
                RcGraphe.Visible = True
                'coloration de la serie
                Dim barColors As Color() = New Color(19) {Color.Red, Color.Aqua, Color.GreenYellow, Color.Coral, Color.CornflowerBlue, Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid, Color.DarkRed, Color.DarkSalmon, Color.DarkSeaGreen, Color.DarkSlateBlue, Color.Cornsilk}
                Dim i As Integer = 0
                For Each item As ChartSeriesItem In RcGraphe.Series(0).Items
                    If i = 20 Then
                        i = 0
                    End If
                    item.Appearance.FillStyle.MainColor = barColors(i)
                    item.Appearance.FillStyle.SecondColor = barColors(i)
                    i += 1
                Next
            End If
        Else
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                dsServices = New DataSet()
                label.Text = "Le nombre de service sur cette période est insuffisant pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            End If
        End If
    End Sub
    Private Sub GetGraphCAparEmploye(ByVal dDateDeb As Date, ByVal dDateFin As Date, ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label, ByVal bNmoinsUn As Boolean)
        Dim listID As New List(Of String)
        listID.Clear()
        For Each I As ListItem In CheckBoxListCAEmploye.Items
            If I.Selected Then
                listID.Add(I.Value)
            End If
        Next
        Dim htCAEmploye As New Hashtable
        Dim oServiceDAO As New CServiceDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceToListGraph(listID)
        Dim dsEmployes As DataSet
        Dim htCAHTEmployes As Hashtable = oStatistiquesDAO.GetCAHTParEmploye(dDateDeb, dDateFin)
        Dim iIndexServices As Integer = 0
        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count
        Dim iNbColCout As Integer = 2


        ' Parcours des services
        Dim lDureePeriode As Long = DateDiff("m", dDateDeb, dDateFin) + 1

        ' Parcours des employés 
        dsEmployes = oEmployeDAO.GetNomPrenomEmploye()
        dsEmployes = oEmployeDAO.GetNomPrenomEmployeFacturable(CStr(dDateDeb), CStr(dDateFin))
        Dim iIndexEmploye As Integer = 0

        Dim Sum As Double = 0

        For i As Integer = 0 To dsEmployes.Tables(0).Rows.Count - 1
            Dim lEmployeId As Long = CLng(dsEmployes.Tables(0).Rows(i)("EmployeId"))
            Dim iIndexCout As Integer = 0
            Dim dCoutPeriode As Double = 0
            Dim dsCouts As DataSet
            Dim dTot As Double = 0
            Dim iCellule As Integer = 0
            Dim dTmp As Double = 0
            Dim dCoutAxe As Double
            dsCouts = oEmployeDAO.GetEmployeCoutPeriode(dDateDeb, dDateFin, lEmployeId)

            If dsCouts.Tables(0).Rows.Count > 0 Then
                'on a au moins un cout dans la liste, donc on ajoute les infos de l'employé
                'htCAEmploye.Add(dsEmployes.Tables(0).Rows(i)("PrenomNom"), 0)

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
                        dTot = CDbl(htCAHTEmployes(lEmployeId))

                        ' TODO : coût Axe, Soufflot et Save à rendre dynamique
                        dCoutAxe = dCoutPeriode * 1
                        'dCoutSoufflot = dCoutPeriode * 0
                        'dCoutSave = dCoutPeriode * 0

                        If CInt(.Item("EmployeCoutFacturable")) = 0 Then
                            If dTot <> 0 Then
                                ' Calcul du CA par BU
                                iIndexServices = 0

                                Dim diffDateRent = dDateDebutRent & "-" & dDateFinRent
                                'wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 1).Value = IIf(IsDBNull(.Item("EmployeCoutCout")), "", .Item("EmployeCoutCommentaire"))

                                For k As Integer = 0 To iNbServices - 1
                                    dTmp = CDbl(oStatistiquesDAO.SelectStatEmployeService(dDateDebutRent, dDateFinRent, CInt(lEmployeId), CInt(dsServices.Tables(0).Rows(k)("ServiceId"))))
                                    'iCellule = iColNomEmploye + 1 + iNbServices + (iIndexCout * (iNbColCout + (iNbServices * 3))) + iIndexServices * 3 + iNbColCout
                                    If dTmp > 0 Then
                                        Try
                                            Sum += dTmp
                                            htCAEmploye.Add(dsEmployes.Tables(0).Rows(i)("PrenomNom"), CDbl(htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom"))) + dTmp)
                                        Catch
                                            Sum += dTmp
                                            htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom")) = CDbl(htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom"))) + dTmp
                                        End Try
                                    End If
                                    'wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 1).Value = Format(dTmp / dTot * 100, ".00") & "%"

                                    iIndexServices += 1
                                Next

                            End If

                        Else
                            'iIndexServices = 0
                            'For k As Integer = 0 To iNbServices - 1

                            '    Dim dsCoutService As DataSet = oEmployeDAO.GetEmployeCoutService(CLng(.Item("EmployeCoutID")), CLng(dsServices.Tables(0).Rows(k)("ServiceID")))

                            '    If dsCoutService.Tables(0).Rows.Count > 0 And dCoutAxe > 0 Then
                            '        dTmp = dCoutAxe * CDbl(dsCoutService.Tables(0).Rows(0)("Repartition")) / 100.0#

                            '        Try
                            '            Sum += dTmp
                            '            htCAEmploye.Add(dsEmployes.Tables(0).Rows(i)("PrenomNom"), CDbl(htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom"))) + dTmp)
                            '        Catch
                            '            Sum += dTmp
                            '            htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom")) = CDbl(htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom"))) + dTmp
                            '        End Try

                            '        'wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                            '        ''------------------------------------------------------------------------------------------------------------------------------------------------------------
                            '    End If
                            '    iIndexServices += 1
                            'Next
                        End If

                    End With


                    iIndexCout += 1
                Next

                iIndexEmploye += 1
            End If

        Next
        '-----------------------------------------------------------------------------------------------------------------------------------------------
        Dim dsResultatCAEmploye As New DataSet
        'création d'une table
        dsResultatCAEmploye.Tables.Add("Res")
        'ajout des colone du DataSet
        dsResultatCAEmploye.Tables("Res").Columns.Add("NomPrenom", GetType(String))
        dsResultatCAEmploye.Tables("Res").Columns.Add("CA", GetType(Double))
        Dim l As List(Of String) = New List(Of String)
        For Each key In htCAEmploye.Keys
            l.Add(CStr(key))
        Next

        l.Sort()
        l.Reverse()

        For Each key In l
            'on remplis une ligne
            If CDbl(htCAEmploye.Item(key)) <> 0 Then

                dsResultatCAEmploye.Tables("Res").Rows.Add(key, htCAEmploye.Item(key))
            End If
        Next

        If dsResultatCAEmploye.Tables("res").Rows.Count > 1 Then
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red

            Else
                label.Text = ""
                If cbPourcentageCAParEmploye.Checked Then

                    For Each row As DataRow In dsResultatCAEmploye.Tables(0).Rows
                        row("CA") = FormatNumber(CDec(row("CA")) / Sum * 100, 2)
                    Next
                End If

                RcGraphe.DataSource = dsResultatCAEmploye
                RcGraphe.DataBind()
                'coloration de la serie
                Dim barColors As Color() = New Color(0) {Color.Red}
                For Each item As ChartSeriesItem In RcGraphe.Series(0).Items
                    item.Appearance.FillStyle.MainColor = barColors(0)
                    item.Appearance.FillStyle.SecondColor = barColors(0)
                Next
                RcGraphe.Visible = True
                RcGraphe.Height = (dsEmployes.Tables(0).Rows.Count * 7) + 300
            End If
        Else
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                label.Text = "Le nombre de service sur cette période est insuffisant pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            End If

        End If

    End Sub

    Private Sub btnCAService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCAService.Click


        GetGraphCAParService(cbPourcentageCAService.Checked, CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcCAServiceNMoinsUn, LabNMoinsUn, True)
        GetGraphCAParService(cbPourcentageCAService.Checked, CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcPanelGraphe, lblServiceInsuffisant, False)
        If rcPanelGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar Then
            bCAServiceTypeGraphe = False
            rcPanelGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcPanelGraphe.Chart.Series.GetSeries(1).Type = ChartSeriesType.Pie
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(1).Type = ChartSeriesType.Pie
            SaveState(True, False, False, False, True)
        Else
            bCAServiceTypeGraphe = True
            rcPanelGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcPanelGraphe.Chart.Series.GetSeries(1).Type = ChartSeriesType.Bar
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(1).Type = ChartSeriesType.Bar
            SaveState(True, False, False, False, True)

        End If
    End Sub

    Private Sub btnBarPie_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBarPie.Click
       
        GetGraphMasseSalariale(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcMasseSalarialeNMoinsUn, LabMasseSalarialeNMoinsUn, True)
        GetGraphMasseSalariale(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcPanelGraphe3, LabMasseSalariale, False)
        If rcPanelGraphe3.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar Then
            bMasseSalarialeTypeGraphe = False
            rcPanelGraphe3.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcMasseSalarialeNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            SaveState(False, True, False, False, True)
        Else
            bMasseSalarialeTypeGraphe = True
            rcPanelGraphe3.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcMasseSalarialeNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            SaveState(False, True, False, False, True)

        End If
    End Sub

    Private Sub btnBarPieEBEService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBarPieEBEService.Click
        GetGraphEBEParService(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcRexGrapheNMoinsUn, LabRexGrapheNMoinsUn, True)
        GetGraphEBEParService(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcRexGraphe, LabRexGraphe, False)
        If rcRexGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar Then
            bEBEServiceTypeGraphe = False
            rcRexGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcRexGrapheNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            SaveState(False, False, True, False, True)
        Else
            bEBEServiceTypeGraphe = True
            rcRexGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcRexGrapheNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            SaveState(False, False, True, False, True)
        End If
    End Sub

    Private Sub btnBarPieOutilsService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBarPieOutilsService.Click
        GetGraphOutilsParService(CDate(tbDateDeb.Text).AddYears(-1), CDate(tbDateFin.Text).AddYears(-1), rcOutilsGrapheNMoinsUn, LabOutilsGrapheNMoinsUN, True)
        GetGraphOutilsParService(CDate(tbDateDeb.Text), CDate(tbDateFin.Text), rcOutilsGraphe, LabOutilsGraphe, False)
        If rcOutilsGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar Then
            bOutilsServiceTypeGraphe = False
            rcOutilsGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcOutilsGrapheNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            SaveState(False, False, False, True, True)
        Else
            bOutilsServiceTypeGraphe = True
            rcOutilsGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcOutilsGrapheNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            SaveState(False, False, False, True, True)

        End If
    End Sub
    Private Sub GetGraphMontantFormationEmploye(ByVal dDateDeb As Date, ByVal dDateFin As Date, ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label, ByVal bNmoinsUn As Boolean)
        Dim oFormationDAO As New CFormationDAO


        Dim sDateDeb As String = dDateDeb.ToString.Split(CChar("/"))(2).Substring(0, 4) & "-" & dDateDeb.ToString.Split(CChar("/"))(1) & "-" & dDateDeb.ToString.Split(CChar("/"))(0)
        Dim sDateFin As String = dDateFin.ToString.Split(CChar("/"))(2).Substring(0, 4) & "-" & dDateFin.ToString.Split(CChar("/"))(1) & "-" & dDateFin.ToString.Split(CChar("/"))(0)
        Dim dData As DataSet = oFormationDAO.GetSumFormationCoutParEmploye(sDateDeb, sDateFin)
        If dData.Tables(0).Rows.Count = 0 Then
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                label.Text = "Le nombre de Formation sur cette période est insuffisant pour faire un graphique."
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
                RcGraphe.Visible = False
            End If
        Else
            If bNmoinsUn And Not (cbn1.Checked) Then
                label.Text = ""
                label.Font.Bold = True
                RcGraphe.Visible = False
                label.ForeColor = Color.Red
            Else
                Dim Sum As Decimal = 0
                For Each row As DataRow In dData.Tables(0).Rows
                    Sum += CDec(row("Cout"))
                Next

                If cbPourcentageFormation.Checked Then

                    For Each row As DataRow In dData.Tables(0).Rows
                        row("Cout") = FormatNumber(CDec(row("Cout")) / Sum * 100, 2)
                    Next
                End If

                RcGraphe.DataSource = dData
                RcGraphe.DataBind()
                label.Text = ""
                RcGraphe.Visible = True
                'coloration de la serie
                Dim barColors As Color() = New Color(19) {Color.Red, Color.Aqua, Color.GreenYellow, Color.Coral, Color.CornflowerBlue, Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid, Color.DarkRed, Color.DarkSalmon, Color.DarkSeaGreen, Color.DarkSlateBlue, Color.Cornsilk}

                For Each item As ChartSeriesItem In RcGraphe.Series(0).Items

                    item.Appearance.FillStyle.MainColor = barColors(0)
                    item.Appearance.FillStyle.SecondColor = barColors(0)
                Next
            End If
        End If
        RcGraphe.Height = (dData.Tables(0).Rows.Count * 8) + 100
    End Sub

    Private Sub SaveState(ByVal bTypeGrapheCAService As Boolean, ByVal bTypeGrapheMasseSalariale As Boolean, ByVal bTypeGrapheEBEService As Boolean, ByVal bTypeGrapheOutilsService As Boolean, ByVal bBtn As Boolean)
        oRechercheGraphSave = New RechercheGraphe
        Dim i As Integer = 0
        Dim lsString As New List(Of String)
        Dim lbBoolean As New List(Of Boolean)
        Dim lsStringCAEmploye As New List(Of String)
        Dim lbBooleanCAEmploye As New List(Of Boolean)

        If bTypeGrapheCAService Then ' si le type du Graphe CAService est modifié (Bar,Pie)
            oRechercheGraphSave = CType(Session("Graphe"), RechercheGraphe)
            For i = 0 To chkbCAParService.Items.Count - 1
                lsString.Add(chkbCAParService.Items(i).Value)
                lbBoolean.Add(chkbCAParService.Items(i).Selected)
            Next
            For i = 0 To CheckBoxListCAEmploye.Items.Count - 1
                lsStringCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Value)
                lbBooleanCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Selected)
            Next
            '
            oRechercheGraphSave.SaveRecherche(cbn1.Checked, cbPourcentageFormation.Checked, oRechercheGraphSave.RechercheOutilsServiceTypeGraphe, cbOutils.Checked, oRechercheGraphSave.RechercheEBEServiceTypeGraphe, cbEBEService.Checked, oRechercheGraphSave.RechercheMasseSalarialeTypeGraphe, cbMasseSalariale.Checked, cbbFiltreStatEmployeService.SelectedValue, cbbFiltreStatEmployeService.SelectedIndex, cbPourcentageCAParEmploye.Checked, lsStringCAEmploye, lbBooleanCAEmploye, bCAServiceTypeGraphe, cbPourcentageCAService.Checked, lsString, lbBoolean, cbbFiltreStatGalDetail.SelectedIndex, cbbFiltreStatGalDetail.SelectedValue, tbDateDeb.Text, tbDateFin.Text)
            Session("Graphe") = oRechercheGraphSave

        ElseIf bTypeGrapheMasseSalariale Then ' si le type du Graphe Masse Salariale est modifié (Bar,Pie)
            oRechercheGraphSave = CType(Session("Graphe"), RechercheGraphe)
            For i = 0 To chkbCAParService.Items.Count - 1
                lsString.Add(chkbCAParService.Items(i).Value)
                lbBoolean.Add(chkbCAParService.Items(i).Selected)
            Next
            For i = 0 To CheckBoxListCAEmploye.Items.Count - 1
                lsStringCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Value)
                lbBooleanCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Selected)
            Next
            'bMasseSalarialeTypeGraphe
            oRechercheGraphSave.SaveRecherche(cbn1.Checked, cbPourcentageFormation.Checked, oRechercheGraphSave.RechercheOutilsServiceTypeGraphe, cbOutils.Checked, oRechercheGraphSave.RechercheEBEServiceTypeGraphe, cbEBEService.Checked, bMasseSalarialeTypeGraphe, cbMasseSalariale.Checked, cbbFiltreStatEmployeService.SelectedValue, cbbFiltreStatEmployeService.SelectedIndex, cbPourcentageCAParEmploye.Checked, lsStringCAEmploye, lbBooleanCAEmploye, oRechercheGraphSave.RechercheCAServiceTypeGraphe, cbPourcentageCAService.Checked, lsString, lbBoolean, cbbFiltreStatGalDetail.SelectedIndex, cbbFiltreStatGalDetail.SelectedValue, tbDateDeb.Text, tbDateFin.Text)
            Session("Graphe") = oRechercheGraphSave
        ElseIf bTypeGrapheEBEService Then
            If bBtn Then 'si le type du Graphe EBEService est modifié (Bar,Pie)
                oRechercheGraphSave = CType(Session("Graphe"), RechercheGraphe)
            End If
            For i = 0 To chkbCAParService.Items.Count - 1
                lsString.Add(chkbCAParService.Items(i).Value)
                lbBoolean.Add(chkbCAParService.Items(i).Selected)
            Next
            For i = 0 To CheckBoxListCAEmploye.Items.Count - 1
                lsStringCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Value)
                lbBooleanCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Selected)
            Next '
            oRechercheGraphSave.SaveRecherche(cbn1.Checked, cbPourcentageFormation.Checked, oRechercheGraphSave.RechercheOutilsServiceTypeGraphe, cbOutils.Checked, bEBEServiceTypeGraphe, cbEBEService.Checked, oRechercheGraphSave.RechercheMasseSalarialeTypeGraphe, cbMasseSalariale.Checked, cbbFiltreStatEmployeService.SelectedValue, cbbFiltreStatEmployeService.SelectedIndex, cbPourcentageCAParEmploye.Checked, lsStringCAEmploye, lbBooleanCAEmploye, oRechercheGraphSave.RechercheCAServiceTypeGraphe, cbPourcentageCAService.Checked, lsString, lbBoolean, cbbFiltreStatGalDetail.SelectedIndex, cbbFiltreStatGalDetail.SelectedValue, tbDateDeb.Text, tbDateFin.Text)
            Session("Graphe") = oRechercheGraphSave
        ElseIf bTypeGrapheOutilsService Then 'si le type du Graphe Outils Service est modifié (Bar,Pie)

            oRechercheGraphSave = CType(Session("Graphe"), RechercheGraphe)

            For i = 0 To chkbCAParService.Items.Count - 1
                lsString.Add(chkbCAParService.Items(i).Value)
                lbBoolean.Add(chkbCAParService.Items(i).Selected)
            Next
            For i = 0 To CheckBoxListCAEmploye.Items.Count - 1
                lsStringCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Value)
                lbBooleanCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Selected)
            Next '
            oRechercheGraphSave.SaveRecherche(cbn1.Checked, cbPourcentageFormation.Checked, bOutilsServiceTypeGraphe, cbOutils.Checked, oRechercheGraphSave.RechercheEBEServiceTypeGraphe, cbEBEService.Checked, oRechercheGraphSave.RechercheMasseSalarialeTypeGraphe, cbMasseSalariale.Checked, cbbFiltreStatEmployeService.SelectedValue, cbbFiltreStatEmployeService.SelectedIndex, cbPourcentageCAParEmploye.Checked, lsStringCAEmploye, lbBooleanCAEmploye, oRechercheGraphSave.RechercheCAServiceTypeGraphe, cbPourcentageCAService.Checked, lsString, lbBoolean, cbbFiltreStatGalDetail.SelectedIndex, cbbFiltreStatGalDetail.SelectedValue, tbDateDeb.Text, tbDateFin.Text)
            Session("Graphe") = oRechercheGraphSave
        Else
            If bBtn Then ' le type n'est pas modifié
                oRechercheGraphSave = CType(Session("Graphe"), RechercheGraphe)
            End If
            For i = 0 To chkbCAParService.Items.Count - 1
                lsString.Add(chkbCAParService.Items(i).Value)
                lbBoolean.Add(chkbCAParService.Items(i).Selected)
            Next
            For i = 0 To CheckBoxListCAEmploye.Items.Count - 1
                lsStringCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Value)
                lbBooleanCAEmploye.Add(CheckBoxListCAEmploye.Items(i).Selected)
            Next '
            oRechercheGraphSave.SaveRecherche(cbn1.Checked, cbPourcentageFormation.Checked, oRechercheGraphSave.RechercheOutilsServiceTypeGraphe, cbOutils.Checked, oRechercheGraphSave.RechercheEBEServiceTypeGraphe, cbEBEService.Checked, oRechercheGraphSave.RechercheMasseSalarialeTypeGraphe, cbMasseSalariale.Checked, cbbFiltreStatEmployeService.SelectedValue, cbbFiltreStatEmployeService.SelectedIndex, cbPourcentageCAParEmploye.Checked, lsStringCAEmploye, lbBooleanCAEmploye, oRechercheGraphSave.RechercheCAServiceTypeGraphe, cbPourcentageCAService.Checked, lsString, lbBoolean, cbbFiltreStatGalDetail.SelectedIndex, cbbFiltreStatGalDetail.SelectedValue, tbDateDeb.Text, tbDateFin.Text)
            Session("Graphe") = oRechercheGraphSave

        End If
    End Sub

    Private Sub cbn1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbn1.CheckedChanged
        If Not (cbn1.Checked) Then ' quand on ne veut plus afficher les graphes n-1
            masquer(rcTauxFactuMoinUn, LabTauxFactuMoinUn)
            masquer(rcCAServiceNMoinsUn, LabNMoinsUn)
            masquer(rcCAEmployeNMoinsUn, LabCAEmployeNMoinsUn)
            masquer(rcMasseSalarialeNMoinsUn, LabMasseSalarialeNMoinsUn)
            masquer(rcRexGrapheNMoinsUn, LabRexGrapheNMoinsUn)
            masquer(rcOutilsGrapheNMoinsUn, LabOutilsGrapheNMoinsUN)
            masquer(rcFormationGrapheNMoinsUn, LabFormationNMoinsUn)

        End If
    End Sub
    'permet de masquer un graphe ainsi que son libellé
    Private Sub masquer(ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label)
        label.Text = ""
        RcGraphe.Visible = False
    End Sub
 
End Class