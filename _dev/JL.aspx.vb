Imports GemBox.Spreadsheet
Imports Telerik.Charting
Imports ComptaAna.Business

Public Class JL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oEmployeDAO As New Business.CEmployeDAO
        Dim oEmploye As Business.CEmploye
        Dim oEmployeModule As New Business.CEmployeModuleDAO
        Dim lDroit As Hashtable



        Dim dDateDeb As Date = CDate("01/07/2013") ' A CHANGER
        Dim dDateFin As Date = CDate("30/07/2013") ' A CHANGER

        'Dim oServiceDAO As New CServiceDAO
        'Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()
        'Dim oStatistiquesDAO As New CStatistiquesDAO
        'Dim dsCAServices As New DataSet
        'Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count
        'Dim oProduitAffaireDAO As New CProduitAffaireDAO
        'Dim htCAServices As New Hashtable
        'Dim htProduitsHorsDepassementBudget As New Hashtable

        'For i As Integer = 0 To iNbServices - 1
        '    With dsServices.Tables(0).Rows(i)
        '        dsCAServices = oStatistiquesDAO.SelectStatGeneralesParService(dDateDeb, dDateFin, False, False, CInt(.Item("ServiceId")), 0, -1, "")
        '        htCAServices.Add(dsCAServices.Tables(0).Rows(0)("ID"), dsCAServices.Tables(0).Rows(0)("CAHT"))
        '        htProduitsHorsDepassementBudget.Add(dsCAServices.Tables(0).Rows(0)("ID"), oProduitAffaireDAO.GetProduitsHorsDepassementBudget(dDateDeb, dDateFin, CLng(.Item("ServiceId"))))
        '    End With
        'Next

        'Dim htSalaireschargesParBU As Hashtable = SalaireschargesParBU(dDateDeb, dDateFin)

        'Dim dsResultat As New DataSet

        ''création d'une table
        'dsResultat.Tables.Add("Res")
        ''ajout des colone du DataSet
        'dsResultat.Tables("Res").Columns.Add("Service", GetType(String))
        'dsResultat.Tables("Res").Columns.Add("Rex", GetType(Double))

        'For Each key In htSalaireschargesParBU.Keys
        '    'on remplis une ligne

        '    'dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), htService.Item(key))
        '    Dim CA = CDbl(htProduitsHorsDepassementBudget.Item(key)) + CDbl(htCAServices.Item(key))
        '    Dim rex = CA - (CA * 2 / 100 + CDbl(htSalaireschargesParBU.Item(key)) + CA * 31 / 100)
        '    If rex > 0 Then
        '        dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), rex)
        '    End If
        'Next
        'If dsResultat.Tables("res").Rows.Count > 1 Then
        '    rcRexGraphe.DataSource = dsResultat
        '    rcRexGraphe.DataBind()
        'Else
        '    ' message erreur
        'End If

        ''-------------------------------------------------- PARTIE OUTILS QUI UTILISE CE QUI EST AU DESSUS---------------------------------------
        'Dim dsResultatOutils As New DataSet

        ''création d'une table
        'dsResultatOutils.Tables.Add("Res")
        ''ajout des colone du DataSet
        'dsResultatOutils.Tables("Res").Columns.Add("Service", GetType(String))
        'dsResultatOutils.Tables("Res").Columns.Add("Outils", GetType(Double))

        'For Each key In htSalaireschargesParBU.Keys
        '    'on remplis une ligne

        '    Dim Outils As Double = CDbl(htProduitsHorsDepassementBudget.Item(key))
        '    If Outils > 0 Then
        '        dsResultatOutils.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), Outils)
        '    End If
        'Next
        'If dsResultatOutils.Tables("res").Rows.Count > 1 Then
        '    rcOutilsGraphe.DataSource = dsResultatOutils
        '    rcOutilsGraphe.DataBind()
        'Else
        '    ' message erreur
        'End If

        'grapheCAparEmploye(dDateDeb, dDateFin)

        'grapheMontantFormationEmploye(dDateDeb, dDateFin)

        oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("lamare", "delphps")
        ''oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("bleuzen", "yoann")
        ''oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("gruel", "annie")
        ''oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("piau", "matthieu")
        ''oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("kraeutler", "laurent")
        ''oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("favris", "laurence")

        lDroit = oEmployeModule.GetDroitsEmploye(oEmploye.EmployeID)

        Session("employe") = oEmploye
        Session("droit") = lDroit
        Session("Connecter") = True

        'Response.Redirect("~/Statistiques/StatTypeProduit.aspx")
        Response.Redirect("http://compta.local/Gestion/Employe/EmployeEcheanceAnnuelle.aspx?id=53")
        'Response.Redirect("~/Statistiques/StatEmployeService.aspx")
        'Response.Redirect("~/Gestion/Client/ClientListe.aspx")
        'Response.Redirect("~/Gestion/Catalogue.aspx")
        'Response.Redirect("~/Gestion/Affaire/AffaireProduits.aspx?id=3226")
        'Response.Redirect("~/Gestion/Affaire/AffaireLister.aspx")
        'Response.Redirect("~/Gestion/Affaire/AffaireProduits.aspx?id=2843")
        'Response.Redirect("~/Gestion/Affaire/AffaireFacturation.aspx?id=3607")
        'Response.Redirect("~/Administration/RIB.aspx")
        'Response.Redirect("~/activite/ReleveActivite.aspx")
        'Response.Redirect("~/Statistiques/StatistiquesGenerales.aspx")
        'Response.Redirect("~/Statistiques/StatEmployeServiceV2.aspx")
        'Response.Redirect("~/Gestion/Employe/EmployeEcheanceAnnuelle.aspx?id=53")
        'Response.Redirect("~/Statistiques/StatPrimesAugmentations.aspx")
        'Response.Redirect("~/Statistiques/Graphiques.aspx")
        'Response.Redirect("~/Gestion/Employe/EmployeCout.aspx?id=53")
        'Response.Redirect("~/Gestion/Affaire/AffaireFacturation.aspx?id=3226")
        'Response.Redirect("http://localhost:1668/Gestion/Affaire/AffaireFactureDetailsV2.aspx?id=20")

    End Sub

    Protected Sub rcRexGraphe_ItemDataBound(ByVal sender As Object, ByVal e As ChartItemDataBoundEventArgs)
        e.SeriesItem.Name = CType(e.DataItem, DataRowView)("Service").ToString()
    End Sub

    Protected Sub rcOutilsGraphe_ItemDataBound(ByVal sender As Object, ByVal e As ChartItemDataBoundEventArgs)
        e.SeriesItem.Name = CType(e.DataItem, DataRowView)("Service").ToString()
    End Sub

    Private Function SalaireschargesParBU(dDateDeb As Date, dDateFin As Date) As Hashtable
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsEmployes As DataSet = oEmployeDAO.GetNomPrenomEmploye(-1) ' CInt(cbbFiltreStatEmployeService.SelectedValue)


        Dim oServiceDAO As New CServiceDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()

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

        'Dim i As Double = 0
        'For Each d As Double In htService.Values
        '    i = i + d
        'Next
        'Dim y = i * 0.196 + i
        'y = i * 0.196 + i

        'Dim list As New List(Of Double)

        'For Each row In htService.Values
        '    list.Add(CDbl(row))
        'Next

        'rcPanelGraphe.DataSource = list

        'Dim dsResultat As New DataSet


        ''création d'une table
        'dsResultat.Tables.Add("Res")
        ''ajout des colone du DataSet
        'dsResultat.Tables("Res").Columns.Add("Service", GetType(String))
        'dsResultat.Tables("Res").Columns.Add("Sum", GetType(Double))

        'For Each key In htService.Keys
        '    'on remplis une ligne

        '    dsResultat.Tables("Res").Rows.Add(oServiceDAO.GetServiceLibelleByID(CInt(key)).Tables(0).Rows(0)(0), htService.Item(key))
        'Next

        Return htService
    End Function


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

    Private Sub grapheCAparEmploye(dDateDeb As Date, dDateFin As Date)


        Dim htCAEmploye As New Hashtable





        Dim oServiceDAO As New CServiceDAO
        Dim oEmployeDAO As New CEmployeDAO
        Dim oStatistiquesDAO As New CStatistiquesDAO
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsServices As DataSet = oServiceDAO.GetAllServiceToList()

        Dim dsEmployes As DataSet
        Dim htCAHTEmployes As Hashtable = oStatistiquesDAO.GetCAHTParEmploye(dDateDeb, dDateFin)

        Dim iIndexServices As Integer = 0
        Dim iNbServices As Integer = dsServices.Tables(0).Rows.Count

        Dim iNbColCout As Integer = 2


        ' Parcours des services
        Dim lDureePeriode As Long = DateDiff("m", dDateDeb, dDateFin) + 1

        ' Parcours des employés
        dsEmployes = oEmployeDAO.GetNomPrenomEmploye()
        Dim iIndexEmploye As Integer = 0

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

                                    Try
                                        htCAEmploye.Add(dsEmployes.Tables(0).Rows(i)("PrenomNom"), CDbl(htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom"))) + dTmp)
                                    Catch
                                        htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom")) = CDbl(htCAEmploye.Item(dsEmployes.Tables(0).Rows(i)("PrenomNom"))) + dTmp
                                    End Try

                                    'wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iCellule + 1).Value = Format(dTmp / dTot * 100, ".00") & "%"

                                    iIndexServices += 1
                                Next

                            End If

                            'Else
                            '    iIndexServices = 0
                            '    For k As Integer = 0 To iNbServices - 1

                            '        Dim dsCoutService As DataSet = oEmployeDAO.GetEmployeCoutService(CLng(.Item("EmployeCoutID")), CLng(dsServices.Tables(0).Rows(k)("ServiceID")))

                            '        If dsCoutService.Tables(0).Rows.Count > 0 Then
                            '            dTmp = dCoutAxe * CDbl(dsCoutService.Tables(0).Rows(0)("Repartition")) / 100.0#

                            '            wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSal.Cells(iLigNomEmploye + iIndexEmploye, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                            '            '------------------------------------------------------------------------------------------------------------------------------------------------------------
                            '            wsChargesSal.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value = CDbl(wsChargesSal.Cells(iLigTotauxServices, iColNomEmploye + 1 + iIndexServices).Value) + dTmp
                            '        End If
                            '        iIndexServices += 1
                            '    Next

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

            dsResultatCAEmploye.Tables("Res").Rows.Add(key, htCAEmploye.Item(key))

        Next

        If dsResultatCAEmploye.Tables("res").Rows.Count > 1 Then
            rcCAEmploye.DataSource = dsResultatCAEmploye
            rcCAEmploye.DataBind()
        Else
            ' message erreur
        End If

        rcCAEmploye.Appearance.Dimensions.Height = dsEmployes.Tables(0).Rows.Count * 15

    End Sub

    Private Sub grapheMontantFormationEmploye(ByVal dDateDeb As Date, ByVal dDateFin As Date)
        Dim oFormationDAO As New CFormationDAO

        Dim sDateDeb As String = dDateDeb.ToString.Split(CChar("/"))(2).Substring(0, 4) & "-" & dDateDeb.ToString.Split(CChar("/"))(1) & "-" & dDateDeb.ToString.Split(CChar("/"))(0)
        Dim sDateFin As String = dDateFin.ToString.Split(CChar("/"))(2).Substring(0, 4) & "-" & dDateFin.ToString.Split(CChar("/"))(1) & "-" & dDateFin.ToString.Split(CChar("/"))(0)

        rcFormationGraphe.DataSource = oFormationDAO.GetSumFormationCoutParEmploye(sDateDeb, sDateFin)
        rcFormationGraphe.DataBind()
    End Sub

End Class