Imports ComptaAna.Business
Imports Obout.ComboBox
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit

Public Class StatTypeProduit
    Inherits System.Web.UI.Page

    Dim dDateDeb As Date
    Dim dDateFin As Date
    Dim n1 As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
         
            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesStatsProduits) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            chargerCbbTypeProduit()
            chargerCbbBU()
        Else
            Try
                dDateDeb = CDate(tbDateDeb.Text)
                dDateFin = CDate(tbDateFin.Text)
            Catch ex As InvalidCastException
            End Try
        End If

        n1 = cbn1.Checked

        'chargerCbbTypeProduit()
        'chargerCbbBU()

    End Sub

    ''' <summary>
    ''' charger la liste des types de produit
    ''' </summary> 
    Public Sub chargerCbbTypeProduit()
        cbbFiltreStatTypeProduit.Items.Clear()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO

        ' cbbFiltreStatTypeProduit.EmptyText = " --- Type de produit --- "
        cbbFiltreStatTypeProduit.DataValueField = "TypeProduitID"
        cbbFiltreStatTypeProduit.DataTextField = "TypeProduitLibelle"

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les types de produit"
        itemDefaut.Value = CStr(-1)
        cbbFiltreStatTypeProduit.Items.Insert(0, itemDefaut)

        cbbFiltreStatTypeProduit.SelectedIndex = 0
        cbbFiltreStatTypeProduit.DataSource = oStatistiquesDAO.GetAllTypeProduit()
        cbbFiltreStatTypeProduit.DataBind()
    End Sub

    ''' <summary>
    ''' charger la liste des BU
    ''' </summary> 
    Public Sub chargerCbbBU()
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO

        cbbFiltreStatBU.Items.Clear()
        Dim dsBU As DataSet = oStatistiquesDAO.GetAllService()
        ' cbbFiltreStatBU.EmptyText = " --- Service --- "

        cbbFiltreStatBU.DataValueField = "ServiceID"
        cbbFiltreStatBU.DataTextField = "ServiceLibelle"

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les services"
        itemDefaut.Value = CStr(-1)
        cbbFiltreStatBU.Items.Insert(0, itemDefaut)

        'For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
        '    Dim cbbText As String = dsEmploye.Tables(0).Rows(i)("ServiceLibelle").ToString
        '    Dim item As New ComboBoxItem
        '    item.Text = cbbText
        '    item.Value = dsEmploye.Tables(0).Rows(i)("ServiceID").ToString
        '    cbbFiltreStatBU.Items.Add(item)
        'Next
        cbbFiltreStatBU.SelectedIndex = 0
        cbbFiltreStatBU.DataSource = dsBU
        cbbFiltreStatBU.DataBind()
    End Sub

    ''' <summary>
    ''' ecouteur du bouton rechercher
    ''' </summary> 
    Private Sub btnRechercheStat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRechercheStat.Click
        ChargerStatTypeProduit_v2()
    End Sub

    ''' <summary>
    ''' Effectue la recherche des statistiques sur les charges externes et les produits et met a jour le gridview
    ''' </summary> 
    Private Function ChargerStatTypeProduit() As DataSet
        Dim oConnection As New CConnexion
        oConnection.GetConnexion()
        Dim oProduitDAO As CProduitDAO = New CProduitDAO
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO

        Dim dsResult, dsType, dsProduit As New DataSet
        Dim dtTable As New DataTable("Result")
        Dim dtRow, dtNewRow As DataRow
        Dim somme, sommeN1 As New Decimal

        gvStatTypeProduit.Columns.Clear()

        dtTable.Columns.Add("TypeColor", Type.GetType("System.String"))
        Dim bndColor As New BoundField
        bndColor.HeaderText = "ID"
        bndColor.Visible = False
        gvStatTypeProduit.Columns.Add(bndColor)

        dtTable.Columns.Add("ID", Type.GetType("System.String"))
        Dim bndID As New BoundField
        bndID.DataField = "ID"
        bndID.HeaderText = "ID"
        bndID.SortExpression = "ID"
        bndID.Visible = False
        gvStatTypeProduit.Columns.Add(bndID)

        dtTable.Columns.Add("ProduitRef", Type.GetType("System.String"))
        Dim bndProduitRef As New BoundField
        bndProduitRef.DataField = "ProduitRef"
        bndProduitRef.HeaderText = "Référence"
        bndProduitRef.SortExpression = "ProduitRef"
        gvStatTypeProduit.Columns.Add(bndProduitRef)

        dtTable.Columns.Add("ProduitLibelle", Type.GetType("System.String"))
        Dim bndProduitLibelle As New BoundField
        bndProduitLibelle.DataField = "ProduitLibelle"
        bndProduitLibelle.HeaderText = "Libelle"
        bndProduitLibelle.SortExpression = "ProduitLibelle"
        gvStatTypeProduit.Columns.Add(bndProduitLibelle)

        dtTable.Columns.Add("ProduitCA", Type.GetType("System.String"))
        Dim bndProduitCA As New BoundField
        bndProduitCA.DataField = "ProduitCA"
        bndProduitCA.HeaderText = "CA"
        bndProduitCA.SortExpression = "ProduitCA"
        gvStatTypeProduit.Columns.Add(bndProduitCA)

        dtTable.Columns.Add("ProduitN1", Type.GetType("System.String"))
        Dim bndProduitN1 As New BoundField
        bndProduitN1.DataField = "ProduitN1"
        bndProduitN1.HeaderText = "N - 1"
        bndProduitN1.SortExpression = "ProduitN1"
        gvStatTypeProduit.Columns.Add(bndProduitN1)

        dsResult.Tables.Add(dtTable)

        Try
            If CInt(cbbFiltreStatTypeProduit.SelectedValue) = -1 Then
                dsType = oStatistiquesDAO.GetAllTypeProduit()
            Else
                dsType = oConnection.ExecuteQuery("SELECT TypeProduitID, TypeProduitLibelle FROM TypeProduit WHERE TypeProduitID =" & CInt(cbbFiltreStatTypeProduit.SelectedValue))
                oConnection.Close()
            End If
        Catch ex As InvalidCastException
            dsType = oStatistiquesDAO.GetAllTypeProduit()
        End Try

        For Each row As DataRow In dsType.Tables(0).Rows
            dtRow = dsResult.Tables(0).NewRow
            dtRow("TypeColor") = 1
            dtRow("ID") = row("TypeProduitID").ToString
            dtRow("ProduitRef") = row("TypeProduitLibelle").ToString

            dsResult.Tables(0).Rows.Add(dtRow)

            somme = 0
            sommeN1 = 0
            dsProduit = oStatistiquesDAO.SelectStatTypeProduit(dDateDeb, dDateFin, CInt(row("TypeProduitID")), n1)

            If dsProduit.Tables(0).Rows.Count > 0 Then

                For Each rowProduit As DataRow In dsProduit.Tables(0).Rows
                    dtNewRow = dsResult.Tables(0).NewRow
                    dtNewRow("TypeColor") = 2
                    dtNewRow("ID") = rowProduit("ProduitID")
                    dtNewRow("ProduitRef") = rowProduit("ProduitRef")
                    dtNewRow("ProduitLibelle") = rowProduit("ProduitLibelle")
                    dtNewRow("ProduitCA") = rowProduit("ProduitCA")
                    somme += CDec(rowProduit("ProduitCA"))
                    If n1 Then
                        dtNewRow("ProduitN1") = rowProduit("ProduitN1")
                        sommeN1 += CDec(rowProduit("ProduitN1"))
                    Else
                        dtNewRow("ProduitN1") = ""
                    End If

                    dsResult.Tables(0).Rows.Add(dtNewRow)
                Next

                If n1 Then
                    dtRow("ProduitN1") = FormatNumber(sommeN1, 3)
                End If

                dtRow("ProduitCA") = FormatNumber(somme, 3)
            End If
        Next

        gvStatTypeProduit.DataSource = dsResult
        gvStatTypeProduit.DataBind()

        Return dsResult
    End Function

    Private Sub ChargerStatTypeProduit_v2()
        Dim oProduitDAO As CProduitDAO = New CProduitDAO
        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO

        Dim dsResult, dsProduit As New DataSet
        Dim dtTable As New DataTable("Result")
        Dim somme, sommeN1 As New Decimal
        Dim stypeProduit As String = cbbFiltreStatTypeProduit.SelectedText
        Dim itypeProdID As Integer


        Dim iServiceID As Integer
        Try
            iServiceID = CInt(cbbFiltreStatBU.SelectedValue)
        Catch ex As Exception
            iServiceID = -1
        End Try



        Try
            itypeProdID = CInt(oStatistiquesDAO.GetTypeProduitIDWithTypeProduitLibelle(stypeProduit).Tables(0).Rows(0).Item(0))
        Catch ex As Exception
            itypeProdID = -1
        End Try

        somme = 0
        sommeN1 = 0

        dsProduit = oStatistiquesDAO.SelectStatTypeProduit_v2(dDateDeb, dDateFin, itypeProdID, n1, iServiceID)

        Dim iTypeProduitID As Integer
        Dim iTypeProduitIDPrec As Integer
        Dim dProduitCA As Decimal
        Dim dProduitN1 As Decimal

        If n1 Then
            gvStatTypeProduit.Columns(6).Visible = True

            For i As Integer = dsProduit.Tables(0).Rows.Count - 1 To 0 Step -1
                iTypeProduitID = CInt(dsProduit.Tables(0).Rows(i)("TypeProduitID"))
                If i > 0 Then
                    iTypeProduitIDPrec = CInt(dsProduit.Tables(0).Rows(i - 1)("TypeProduitID"))
                Else
                    iTypeProduitIDPrec = -1
                End If


                dProduitCA = CDec(dsProduit.Tables(0).Rows(i)("ProduitCA"))
                dProduitN1 = CDec(dsProduit.Tables(0).Rows(i)("ProduitN1"))

                If iTypeProduitID = iTypeProduitIDPrec Then
                    somme += dProduitCA
                    sommeN1 += dProduitN1

                Else
                    somme += dProduitCA
                    sommeN1 += dProduitN1

                    Dim dr As DataRow = dsProduit.Tables(0).NewRow
                    dr("ProduitRef") = dsProduit.Tables(0).Rows(i)("TypeProduitLibelle")
                    dr("ProduitCA") = somme
                    dr("ProduitN1") = sommeN1

                    dsProduit.Tables(0).Rows.InsertAt(dr, i)
                    somme = 0
                    sommeN1 = 0
                End If

            Next
        Else
            gvStatTypeProduit.Columns(6).Visible = False

            For i As Integer = dsProduit.Tables(0).Rows.Count - 1 To 0 Step -1
                iTypeProduitID = CInt(dsProduit.Tables(0).Rows(i)("TypeProduitID"))
                If i > 0 Then
                    iTypeProduitIDPrec = CInt(dsProduit.Tables(0).Rows(i - 1)("TypeProduitID"))
                Else
                    iTypeProduitIDPrec = -1
                End If


                dProduitCA = CDec(dsProduit.Tables(0).Rows(i)("ProduitCA"))

                If iTypeProduitID = iTypeProduitIDPrec Then
                    somme += dProduitCA


                Else
                    somme += dProduitCA

                    Dim dr As DataRow = dsProduit.Tables(0).NewRow
                    dr("ProduitRef") = dsProduit.Tables(0).Rows(i)("TypeProduitLibelle")
                    dr("ProduitCA") = somme

                    dsProduit.Tables(0).Rows.InsertAt(dr, i)
                    somme = 0
                End If

            Next
        End If

        gvStatTypeProduit.DataSource = dsProduit
        gvStatTypeProduit.DataBind()
    End Sub


    'Protected Sub gvStatTypeProduit_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvStatTypeProduit.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        If Not IsNothing(DataBinder.Eval(e.Row.DataItem, "TypeColor").ToString) Then
    '            Dim idCouleur As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "TypeColor"))
    '            If idCouleur = 1 Then
    '                e.Row.BackColor = Drawing.Color.FromArgb(229, 243, 255)
    '            End If
    '        End If
    '    End If
    'End Sub TypeProduitID

    Protected Sub gvStatTypeProduit_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvStatTypeProduit.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then '  ai changer le type TYPEPRODUITID EN TYPECOLOR
            If IsDBNull(DataBinder.Eval(e.Row.DataItem, "TypeColor")) Then
                e.Row.BackColor = Drawing.Color.FromArgb(229, 243, 255)
                CType(e.Row.FindControl("btnAfficherDetailsEmploye"), ImageButton).Visible = False
            End If
        End If
    End Sub

    Private Sub gvStatTypeProduit_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvStatTypeProduit.RowCommand
        If e.CommandName = "AfficherDetailsEmploye" Then
            pPopupInfosDetailsEmployes.Visible = True
            pPopupInfosDetailsAffaire.Visible = False
            Dim iProduitID As Integer = CInt(e.CommandArgument.ToString)
            Dim oStat As New CStatistiquesDAO
            gvInfosDetailsEmployes.DataSource = oStat.getAllEmployeWithProduitID(iProduitID, getDateFormatSql(CDate(tbDateDeb.Text)), getDateFormatSql(CDate(tbDateFin.Text)))
            gvInfosDetailsEmployes.DataBind()

        End If
    End Sub

    Private Function getDateFormatSql(dateFin As Date) As String

        Dim daystrFin, monthstrFin As String

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

        Return "" & dateFin.Year & "-" & monthstrFin & "-" & daystrFin
    End Function


    Private Sub btnExporter_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExporter.Click
        Dim ds As DataSet = ChargerStatTypeProduit()

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("StatsChargesExternesProduits")

        Dim iColProduitLibelle As Integer = 0
        Dim iColProduitReference As Integer = 1
        Dim iColProduitAffaireLibelle As Integer = 2
        Dim iColProduitCA As Integer = 3
        Dim iColProduitN1 As Integer = 4

        Dim sProduitLibelle As String = ""
        Dim k As Integer = 0

        ' Affichage de l'en-tête
        ws.Cells(0, iColProduitLibelle).Value = "Produit"
        ws.Cells(0, iColProduitReference).Value = "Référence"
        ws.Cells(0, iColProduitAffaireLibelle).Value = "Libellé"
        ws.Cells(0, iColProduitCA).Value = "CA"
        ws.Cells(0, iColProduitN1).Value = "N-1"

        ' Parcours du dataset
        For i As Integer = 1 To ds.Tables(0).Rows.Count - 2
            Dim iTypeColor As Integer = CInt(ds.Tables(0).Rows(i)("TypeColor"))
            Dim iTypeColorPrec As Integer = CInt(ds.Tables(0).Rows(i - 1)("TypeColor"))
            Dim iTypeColorSuiv As Integer = CInt(ds.Tables(0).Rows(i + 1)("TypeColor"))

            k += 1

            If iTypeColor = 1 Then
                If iTypeColorSuiv = 1 Then
                    ws.Cells(k, iColProduitLibelle).Value = ds.Tables(0).Rows(i)("ProduitRef").ToString()
                Else
                    ' On ne fait rien
                    k -= 1
                End If
            Else
                ' iTypeColor=2
                If iTypeColorPrec = 1 Then
                    sProduitLibelle = ds.Tables(0).Rows(i - 1)("ProduitRef").ToString()
                End If
                ws.Cells(k, iColProduitLibelle).Value = sProduitLibelle
                ws.Cells(k, iColProduitReference).Value = ds.Tables(0).Rows(i)("ProduitRef").ToString()
                ws.Cells(k, iColProduitAffaireLibelle).Value = ds.Tables(0).Rows(i)("ProduitLibelle").ToString()
                ws.Cells(k, iColProduitCA).Value = ds.Tables(0).Rows(i)("ProduitCA").ToString()
                ws.Cells(k, iColProduitN1).Value = ds.Tables(0).Rows(i)("ProduitN1").ToString()
            End If

        Next

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "StatsChargesExternesProduits" & DateEtHeureCouranteToString

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)
    End Sub

    Private Sub btnQuitterDetailsEmployes_Click(sender As Object, e As System.EventArgs) Handles btnQuitterDetailsEmployes.Click
        pPopupInfosDetailsEmployes.Visible = False
    End Sub

    Private Sub btnQuitterDetailsAffaire_Click(sender As Object, e As System.EventArgs) Handles btnQuitterDetailsAffaire.Click
        pPopupInfosDetailsEmployes.Visible = True
        pPopupInfosDetailsAffaire.Visible = False
    End Sub

    Private Sub gvInfosDetailsEmployes_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvInfosDetailsEmployes.RowCommand
        If e.CommandName = "AfficherDetailsAffaires" Then

            pPopupInfosDetailsEmployes.Visible = False
            pPopupInfosDetailsAffaire.Visible = True

            Dim sCommandArgument As String = e.CommandArgument.ToString
            Dim t() As String = Split(sCommandArgument, ";")

            Dim iEmployeID As Integer = CInt(t(0))
            Dim iProduitAffaireID As Integer = CInt(t(1))

            Dim oStat As New CStatistiquesDAO
            gvInfosDetailsAffaire.DataSource = oStat.getAllAffaireWithProduitIDAndEmployeID(iProduitAffaireID)
            gvInfosDetailsAffaire.DataBind()

        End If
    End Sub

End Class