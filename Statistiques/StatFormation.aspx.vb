Imports ComptaAna.Business
Imports Obout.Ajax.UI
Imports ComptaAna.net.Droit
Imports Obout.ComboBox
Imports GemBox.Spreadsheet

Public Class StatFormation
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesEmployeEcriture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try

            chargerListeEmploye()
            LoadTreeView()
            tbTotal.Text = "" & CalculerTotalHeures()
            lblConfirmation.Visible = False
        End If
        lblConfirmation.Visible = False

    End Sub

    ''' <summary>
    ''' Permet de charger le tableau des formations de tous les employés
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTreeView()
        Dim oFormationDAO As New CFormationDAO
        Dim ds As DataSet = oFormationDAO.GetAllFormations()
        gvFormation.DataSource = ds
        gvFormation.DataBind()
    End Sub


    ''' <summary>
    ''' Permet de valider la recherche
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ibValider_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibValider.Click
        LoadTreeViewRecherche()
        tbTotal.Text = "" & CalculerTotalHeures()

        cDateDeb.SelectedDate = Nothing
        cDateFin.SelectedDate = Nothing
    End Sub
    ''' <summary>
    ''' Charge la liste des employés
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeEmploye()
        Dim oEmployeDAO As CEmployeDAO = New CEmployeDAO
        Dim oEmploye As CEmploye = New CEmploye
        Dim ds As DataSet

        oEmploye = CType(Session("Employe"), CEmploye)
        ds = oEmployeDAO.GetAllEmployeToList

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les employés"
        itemDefaut.Value = "default"
        cbbEmploye.Items.Insert(0, itemDefaut)
        cbbEmploye.SelectedValue = "default"

        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim cbbText As String = ds.Tables(0).Rows(i)("Employe").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = ds.Tables(0).Rows(i)("EmployeID").ToString
            cbbEmploye.Items.Add(item)
        Next
        cbbEmploye.DataBind()
    End Sub

    ''' <summary>
    ''' Charge le tableaau en fonction de la recherche
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTreeViewRecherche()
        Dim oFormationDAO As New CFormationDAO
        Dim iEmployeID As Integer
        If Not cbbEmploye.SelectedValue = "default" Then
            iEmployeID = CInt(cbbEmploye.SelectedValue)
        Else
            iEmployeID = -1
        End If

        Dim ds As DataSet = oFormationDAO.GetFormationEmployeParTypePeriode(cbbTypeFormation.SelectedValue, tbDateDeb.Text, tbDateFin.Text, iEmployeID, rbStatut.SelectedIndex, rbSexe.SelectedIndex)
        gvFormation.DataSource = ds
        gvFormation.DataBind()
    End Sub

    Private Sub gvFormation_DataBound1(sender As Object, e As System.EventArgs) Handles gvFormation.DataBound
        gvFormation.Columns(11).Visible = False
    End Sub

    Private Sub gvFormation_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFormation.RowCommand
        If e.CommandName = "SupprimerFormation" Then
            Dim oFormationDAO As New CFormationDAO
            Dim iFormationID As Integer = CInt(e.CommandArgument.ToString)
            Dim iRes As Integer = oFormationDAO.DeleteFormation(iFormationID)
            If iRes = 1 Then
                lblConfirmation.Visible = True
                lblConfirmation.ForeColor = Drawing.Color.Blue
                lblConfirmation.Text = "Formation supprimée"
            Else
                lblConfirmation.Visible = True
                lblConfirmation.ForeColor = Drawing.Color.Red
                lblConfirmation.Text = "Formation non supprimée"
            End If
            LoadTreeView()
        End If
    End Sub



    Private Sub gvFormation_DataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvFormation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim iType As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "FormationType"))
            Select Case iType
                Case 0
                    e.Row.Cells(5).Text = "Suivie"
                    e.Row.Cells(7).Text = "Non renseigné"
                    e.Row.Cells(9).Text = e.Row.Cells(11).Text
                    e.Row.Cells(10).Text = e.Row.Cells(11).Text
                Case 1
                    e.Row.Cells(5).Text = "Dispensée"
                    e.Row.Cells(9).Text = e.Row.Cells(11).Text
                    e.Row.Cells(10).Text = e.Row.Cells(11).Text
                Case 2
                    e.Row.Cells(5).Text = "Prévue"
                    e.Row.Cells(6).Text = "Non renseigné"
                    e.Row.Cells(7).Text = "Non renseigné"
                    e.Row.Cells(12).Visible = True

            End Select

        End If
    End Sub

    Private Function CalculerTotalHeures() As Double
        Dim dTotalH As Double = 0

        For i As Integer = 0 To gvFormation.Rows.Count - 1
            Dim sHeures As String = gvFormation.Rows(i).Cells(4).Text
            dTotalH += CDbl(sHeures)
        Next

        Return dTotalH
    End Function


    Private Sub btnExporter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExporter.Click
        ' Dim ds As DataSet = CType(gvFormation.DataSource, DataSet)
        Dim oFormationDAO As New CFormationDAO
        Dim iEmployeID As Integer
        If Not cbbEmploye.SelectedValue = "default" Then
            iEmployeID = CInt(cbbEmploye.SelectedValue)
        Else
            iEmployeID = -1
        End If

        Dim ds As DataSet = oFormationDAO.GetFormationEmployeParTypePeriode(cbbTypeFormation.SelectedValue, tbDateDeb.Text, tbDateFin.Text, iEmployeID, rbStatut.SelectedIndex, rbSexe.SelectedIndex)

        Dim iColNom As Integer = 0
        Dim iColPrenom As Integer = 1
        Dim iColLibelle As Integer = 2
        Dim iColNbHeures As Integer = 3
        Dim iColType As Integer = 4
        Dim iColOrganisme As Integer = 5
        Dim iColNbParticipants As Integer = 6
        Dim iColCout As Integer = 7
        Dim iColDateDeb As Integer = 8
        Dim iColDateFin As Integer = 9

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture")

        ws.Cells(0, 1).Value = "Liste des formations"
        ws.Cells(0, 1).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(0, 1).Style.Font.Weight = 1000
        ws.Cells(0, 1).Style.Font.Size = 300
        ws.Cells(0, 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(147, 201, 255), Drawing.Color.FromArgb(147, 201, 255))
        ws.Cells(0, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center

        ws.Cells.GetSubrangeAbsolute(0, 0, 0, 9).Merged = True

        ' Affichage de l'en-tête
        ws.Cells(2, iColNom).Value = "Nom"
        ws.Cells(2, iColNom).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColNom).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColNom).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColNom).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColNom).Style.Font.Weight = 1000
        ws.Cells(2, iColPrenom).Value = "Prénom"
        ws.Cells(2, iColPrenom).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColPrenom).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColPrenom).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColPrenom).Style.Font.Weight = 1000
        ws.Cells(2, iColPrenom).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColLibelle).Value = "Libellé formation"
        ws.Cells(2, iColLibelle).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColLibelle).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColLibelle).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColLibelle).Style.Font.Weight = 1000
        ws.Cells(2, iColLibelle).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColNbHeures).Value = "Nombre d'heures de formation"
        ws.Cells(2, iColNbHeures).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColNbHeures).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColNbHeures).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColNbHeures).Style.Font.Weight = 1000
        ws.Cells(2, iColNbHeures).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColType).Value = "Type formation"
        ws.Cells(2, iColType).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColType).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColType).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColType).Style.Font.Weight = 1000
        ws.Cells(2, iColType).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColOrganisme).Value = "Lieu de formation"
        ws.Cells(2, iColOrganisme).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColOrganisme).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColOrganisme).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColOrganisme).Style.Font.Weight = 1000
        ws.Cells(2, iColOrganisme).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColNbParticipants).Value = "Nombre de participants"
        ws.Cells(2, iColNbParticipants).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColNbParticipants).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColNbParticipants).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColNbParticipants).Style.Font.Weight = 1000
        ws.Cells(2, iColNbParticipants).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColCout).Value = "Coût formation"
        ws.Cells(2, iColCout).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColCout).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColCout).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColCout).Style.Font.Weight = 1000
        ws.Cells(2, iColCout).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColDateDeb).Value = "Date de début de formation"
        ws.Cells(2, iColDateDeb).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColDateDeb).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDateDeb).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColDateDeb).Style.Font.Weight = 1000
        ws.Cells(2, iColDateDeb).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColDateFin).Value = "Date de fin de formation"
        ws.Cells(2, iColDateFin).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColDateFin).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDateFin).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColDateFin).Style.Font.Weight = 1000
        ws.Cells(2, iColDateFin).Style.VerticalAlignment = VerticalAlignmentStyle.Center

        ws.Rows(2).Height = 2 * 256
        Dim iType As Integer
        ' Parcours du dataset
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            ws.Rows(i + 3).Height = 2 * 150
            ws.Cells(i + 3, iColNom).Value = ds.Tables(0).Rows(i)("EmployeNom")
            ws.Cells(i + 3, iColNom).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColNom).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColNom).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColNom).Style.Font.Weight = 500
            ws.Cells(i + 3, iColPrenom).Value = ds.Tables(0).Rows(i)("EmployePrenom")
            ws.Cells(i + 3, iColPrenom).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColPrenom).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColPrenom).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColPrenom).Style.Font.Weight = 500
            ws.Cells(i + 3, iColLibelle).Value = ds.Tables(0).Rows(i)("FormationLibelle")
            ws.Cells(i + 3, iColLibelle).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColLibelle).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColLibelle).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColLibelle).Style.Font.Weight = 500
            ws.Cells(i + 3, iColNbHeures).Value = ds.Tables(0).Rows(i)("FormationNbHeure")
            ws.Cells(i + 3, iColNbHeures).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColNbHeures).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColNbHeures).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColNbHeures).Style.Font.Weight = 500

            iType = CInt(ds.Tables(0).Rows(i)("FormationType"))
            Select Case iType
                Case 0
                    ws.Cells(i + 3, iColType).Value = "Suivie"
                Case 1
                    ws.Cells(i + 3, iColType).Value = "Dispensee"
                Case 2
                    ws.Cells(i + 3, iColType).Value = "Prevue"

            End Select
            ws.Cells(i + 3, iColType).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColType).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColType).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColType).Style.Font.Weight = 500
            ws.Cells(i + 3, iColOrganisme).Value = ds.Tables(0).Rows(i)("FormationOrganisme")
            ws.Cells(i + 3, iColOrganisme).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColOrganisme).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColOrganisme).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColOrganisme).Style.Font.Weight = 500
            ws.Cells(i + 3, iColNbParticipants).Value = ds.Tables(0).Rows(i)("FormationNbParticipants")
            ws.Cells(i + 3, iColNbParticipants).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColNbParticipants).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColNbParticipants).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColNbParticipants).Style.Font.Weight = 500
            ws.Cells(i + 3, iColCout).Value = ds.Tables(0).Rows(i)("FormationCout")
            ws.Cells(i + 3, iColCout).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColCout).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColCout).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
            ws.Cells(i + 3, iColCout).Style.Font.Weight = 500
            ' si la date de début et la date de fin ne sont pas renseigné.
            If IsDBNull(ds.Tables(0).Rows(i)("FormationDateDeb")) Then

                ws.Cells(i + 3, iColDateDeb).Value = ds.Tables(0).Rows(i)("FormationDate")
                ws.Cells(i + 3, iColDateDeb).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
                ws.Cells(i + 3, iColDateDeb).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                ws.Cells(i + 3, iColDateDeb).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
                ws.Cells(i + 3, iColDateDeb).Style.Font.Weight = 500
                ws.Cells(i + 3, iColDateFin).Value = ds.Tables(0).Rows(i)("FormationDate")
                ws.Cells(i + 3, iColDateFin).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
                ws.Cells(i + 3, iColDateFin).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                ws.Cells(i + 3, iColDateFin).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
                ws.Cells(i + 3, iColDateFin).Style.Font.Weight = 500
                ' si la date est renseigné
            Else
                ws.Cells(i + 3, iColDateDeb).Value = ds.Tables(0).Rows(i)("FormationDateDeb")
                ws.Cells(i + 3, iColDateDeb).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
                ws.Cells(i + 3, iColDateDeb).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                ws.Cells(i + 3, iColDateDeb).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
                ws.Cells(i + 3, iColDateDeb).Style.Font.Weight = 500
                ws.Cells(i + 3, iColDateFin).Value = ds.Tables(0).Rows(i)("FormationDateFin")
                ws.Cells(i + 3, iColDateFin).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
                ws.Cells(i + 3, iColDateFin).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                ws.Cells(i + 3, iColDateFin).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(255, 255, 202), Drawing.Color.FromArgb(255, 255, 202))
                ws.Cells(i + 3, iColDateFin).Style.Font.Weight = 500
            End If
        Next

        ws.Columns(iColNom).Width = 30 * 256
        ws.Columns(iColNom).AutoFit()
        ws.Columns(iColPrenom).Width = 30 * 256
        ws.Columns(iColPrenom).AutoFit()
        ws.Columns(iColLibelle).Width = 25 * 256
        'ws.Columns(iColLibelle).AutoFit()
        ws.Columns(iColNbHeures).Width = 30 * 256
        'ws.Columns(iColNbHeures).AutoFit()
        ws.Columns(iColType).Width = 30 * 256
        ws.Columns(iColType).AutoFit()
        ws.Columns(iColOrganisme).Width = 30 * 256
        ws.Columns(iColOrganisme).AutoFit()
        ws.Columns(iColNbParticipants).Width = 25 * 256
        'ws.Columns(iColNbParticipants).AutoFit()
        ws.Columns(iColCout).Width = 25 * 256
        'ws.Columns(iColCout).AutoFit()
        ws.Columns(iColDateDeb).Width = 30 * 256
        'ws.Columns(iColDateDeb).AutoFit()
        ws.Columns(iColDateFin).Width = 25 * 256
        ' ws.Columns(iColDateFin).AutoFit()

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "Formations" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)
    End Sub
End Class