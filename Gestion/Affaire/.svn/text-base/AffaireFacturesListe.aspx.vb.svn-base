
Imports ComptaAna.Business
Imports System.Data
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit
Imports Obout.ComboBox

Public Class AffaireFacturesListe
    Inherits System.Web.UI.Page

    Dim oFactureDAO As CFacturationAffaireDAO = New CFacturationAffaireDAO
    Dim ds As DataSet = oFactureDAO.GetSuiviFactures()

    ''' <summary>
    ''' Chargement de la page de la liste des Factures
    ''' </summary>
    Protected Sub PageLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
        Try
            If Not verifDroit(lDroit, eModule.AccesSuiviFactures) Then
                Response.Redirect("~/Login.aspx?Erreur=403")
            End If

        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try


        If Not Page.IsPostBack Then
            gvFacture.DataSource = ds
            gvFacture.DataBind()
            chargerListeClient()
            chargerListeService()
            chargerListeTypeAffaire()
        End If


    End Sub

    ''' <summary>
    ''' control pour modifier Facture/ajouter Facture site/supprimer Facture
    ''' </summary>
    Protected Sub gvFacture_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)

    End Sub

    Private Sub gvFacture_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFacture.RowDataBound

    End Sub


    Protected Sub gvFacture_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFacture.RowEditing
        gvFacture.EditIndex = e.NewEditIndex
        ds = oFactureDAO.ChargerFactureSuivantRecherche(cbbFiltreClient.SelectedValue, cbbFiltreService.SelectedValue, cbbFiltreType.SelectedValue, tbMontant.Text, rbPaye.SelectedValue)
        gvFacture.DataSource = ds
        gvFacture.DataBind()
    End Sub

    Protected Sub gvFacture_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFacture.RowCancelingEdit
        gvFacture.EditIndex = -1
        ds = oFactureDAO.ChargerFactureSuivantRecherche(cbbFiltreClient.SelectedValue, cbbFiltreService.SelectedValue, cbbFiltreType.SelectedValue, tbMontant.Text, rbPaye.SelectedValue)
        gvFacture.DataSource = ds
        gvFacture.DataBind()
    End Sub

    Protected Sub gvFacture_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFacture.RowUpdating


        'Dim sLibelle As String = CType(gvFacture.Rows(e.RowIndex).FindControl("tbEditProduitAffaireLibelle"), TextBox).Text
        Dim iFactureID As Integer = CInt(gvFacture.DataKeys(e.RowIndex).Values(0))

        Dim sCom As String = ""
        If Not CType(gvFacture.Rows(e.RowIndex).FindControl("tbCom"), TextBox).Text = "" Then
            sCom = CStr(CType(gvFacture.Rows(e.RowIndex).FindControl("tbCom"), TextBox).Text.Replace("'", "''"))
        End If

        Dim dateDemandeFacture As Date
        If Not CType(gvFacture.Rows(e.RowIndex).FindControl("tbDateDemandeFacture"), TextBox).Text = "" Then
            dateDemandeFacture = CDate(CType(gvFacture.Rows(e.RowIndex).FindControl("tbDateDemandeFacture"), TextBox).Text)
        End If

        Dim dateEnvoi As Date
        If Not CType(gvFacture.Rows(e.RowIndex).FindControl("tbDateEnvoi"), TextBox).Text = "" Then
            dateEnvoi = CDate(CType(gvFacture.Rows(e.RowIndex).FindControl("tbDateEnvoi"), TextBox).Text)
        End If

        Dim datePaiement As Date
        If Not CType(gvFacture.Rows(e.RowIndex).FindControl("tbDatePaiement"), TextBox).Text = "" Then
            datePaiement = CDate(CType(gvFacture.Rows(e.RowIndex).FindControl("tbDatePaiement"), TextBox).Text)
        End If

        Dim paye As Boolean

        paye = CType(gvFacture.Rows(e.RowIndex).FindControl("cbPaye"), CheckBox).Checked


        Dim oFactuDAO As New CFacturationAffaireDAO
        oFactuDAO.UpDateFacturationAffaire(iFactureID, sCom, dateDemandeFacture, dateEnvoi, datePaiement, paye)

        gvFacture.EditIndex = -1
        ds = oFactureDAO.ChargerFactureSuivantRecherche(cbbFiltreClient.SelectedValue, cbbFiltreService.SelectedValue, cbbFiltreType.SelectedValue, tbMontant.Text, rbPaye.SelectedValue)
        gvFacture.DataSource = ds
        gvFacture.DataBind()
    End Sub
    ''' <summary>
    ''' Rechercher un Facture par filtrage
    ''' </summary>
    Protected Sub btRechercheFacture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRechercheFacture.Click
        Dim ds As DataSet
        Dim oFactuDAO As New CFacturationAffaireDAO
        ds = oFactuDAO.ChargerFactureSuivantRecherche(cbbFiltreClient.SelectedValue, cbbFiltreService.SelectedValue, cbbFiltreType.SelectedValue, tbMontant.Text, rbPaye.SelectedValue)
        gvFacture.DataSource = ds
        gvFacture.DataBind()
    End Sub

    Private Sub ibExporter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibExporter.Click
        Dim oFacture As New CFacturationAffaireDAO
        Dim dsFacture As DataSet = oFacture.ChargerFactureSuivantRecherche(cbbFiltreClient.SelectedValue, cbbFiltreService.SelectedValue, cbbFiltreType.SelectedValue, tbMontant.Text, rbPaye.SelectedValue)

        Dim iColRef As Integer = 0
        Dim iColDesignation As Integer = 1
        Dim iColTypeAffaire As Integer = 2
        Dim iColService As Integer = 3
        Dim iColPrestas As Integer = 4
        Dim iColFrais As Integer = 5
        Dim iColMontantTTC As Integer = 6
        Dim iColDateDemande As Integer = 7
        Dim iColDateEnvoi As Integer = 8
        Dim iColPaye As Integer = 9
        Dim iColDatePaiement As Integer = 10
        Dim iColCompteAuto As Integer = 11



        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Suivi factures")

        ws.Cells(0, 1).Value = "Tableau de suivi des factures exporté le  " & DateCouranteToString()
        ws.Cells(0, 1).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Medium)
        ws.Cells(0, 1).Style.Font.Weight = 1000
        ws.Cells(0, 1).Style.Font.Size = 300
        ws.Cells(0, 1).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(147, 201, 255), Drawing.Color.FromArgb(147, 201, 255))
        ws.Cells(0, 1).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells.GetSubrangeAbsolute(0, 1, 0, 5).Merged = True

        ' Affichage de l'en-tête
        ws.Cells(2, iColRef).Value = "Référence facture"
        ws.Cells(2, iColRef).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColRef).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColRef).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColRef).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColRef).Style.Font.Weight = 1000
        ws.Cells(2, iColDesignation).Value = "Désignation"
        ws.Cells(2, iColDesignation).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColDesignation).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDesignation).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColDesignation).Style.Font.Weight = 1000
        ws.Cells(2, iColDesignation).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColTypeAffaire).Value = "Type d'affaire"
        ws.Cells(2, iColTypeAffaire).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColTypeAffaire).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColTypeAffaire).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColTypeAffaire).Style.Font.Weight = 1000
        ws.Cells(2, iColTypeAffaire).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColService).Value = "Service"
        ws.Cells(2, iColService).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColService).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColService).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColService).Style.Font.Weight = 1000
        ws.Cells(2, iColService).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColPrestas).Value = "Prestations HT"
        ws.Cells(2, iColPrestas).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColPrestas).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColPrestas).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColPrestas).Style.Font.Weight = 1000
        ws.Cells(2, iColPrestas).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColFrais).Value = "Frais HT"
        ws.Cells(2, iColFrais).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColFrais).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColFrais).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColFrais).Style.Font.Weight = 1000
        ws.Cells(2, iColFrais).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColMontantTTC).Value = "Montant TTC"
        ws.Cells(2, iColMontantTTC).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColMontantTTC).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColMontantTTC).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColMontantTTC).Style.Font.Weight = 1000
        ws.Cells(2, iColMontantTTC).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColDateDemande).Value = "Date demande facture"
        ws.Cells(2, iColDateDemande).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColDateDemande).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDateDemande).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColDateDemande).Style.Font.Weight = 1000
        ws.Cells(2, iColDateDemande).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColDateEnvoi).Value = "Envoyé le"
        ws.Cells(2, iColDateEnvoi).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColDateEnvoi).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDateEnvoi).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColDateEnvoi).Style.Font.Weight = 1000
        ws.Cells(2, iColDateEnvoi).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColPaye).Value = "Payé"
        ws.Cells(2, iColPaye).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColPaye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColPaye).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColPaye).Style.Font.Weight = 1000
        ws.Cells(2, iColPaye).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColDatePaiement).Value = "Date paiement"
        ws.Cells(2, iColDatePaiement).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColDatePaiement).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColDatePaiement).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColDatePaiement).Style.Font.Weight = 1000
        ws.Cells(2, iColDatePaiement).Style.VerticalAlignment = VerticalAlignmentStyle.Center
        ws.Cells(2, iColCompteAuto).Value = "Délai de paiement"
        ws.Cells(2, iColCompteAuto).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Double)
        ws.Cells(2, iColCompteAuto).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ws.Cells(2, iColCompteAuto).Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(196, 255, 196), Drawing.Color.FromArgb(196, 255, 196))
        ws.Cells(2, iColCompteAuto).Style.Font.Weight = 1000
        ws.Cells(2, iColCompteAuto).Style.VerticalAlignment = VerticalAlignmentStyle.Center


        ws.Rows(2).Height = 2 * 256

        ' Parcours du dataset
        For i As Integer = 0 To dsFacture.Tables(0).Rows.Count - 1
            Dim cColorJ As Drawing.Color = Drawing.Color.FromArgb(255, 255, 202)
            Dim cColorB As Drawing.Color = Drawing.Color.White
            Dim cColor As Drawing.Color
            If i Mod (2) = 0 Then
                cColor = cColorJ
            Else
                cColor = cColorB
            End If
            ws.Rows(i + 3).Height = 2 * 150
            ws.Cells(i + 3, iColRef).Value = dsFacture.Tables(0).Rows(i)("FacturationAffaireRef")
            ws.Cells(i + 3, iColRef).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColRef).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColRef).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColRef).Style.Font.Weight = 500

            ws.Cells(i + 3, iColDesignation).Value = dsFacture.Tables(0).Rows(i)("Designation")
            ws.Cells(i + 3, iColDesignation).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColDesignation).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
            ws.Cells(i + 3, iColDesignation).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColDesignation).Style.Font.Weight = 500

            ws.Cells(i + 3, iColTypeAffaire).Value = dsFacture.Tables(0).Rows(i)("TypeAffaire")
            ws.Cells(i + 3, iColTypeAffaire).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColTypeAffaire).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColTypeAffaire).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColTypeAffaire).Style.Font.Weight = 500

            ws.Cells(i + 3, iColService).Value = dsFacture.Tables(0).Rows(i)("ServiceAffaire")
            ws.Cells(i + 3, iColService).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColService).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColService).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColService).Style.Font.Weight = 500

            ws.Cells(i + 3, iColPrestas).Value = dsFacture.Tables(0).Rows(i)("MontantPrestas")
            ws.Cells(i + 3, iColPrestas).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColPrestas).Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
            ws.Cells(i + 3, iColPrestas).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColPrestas).Style.Font.Weight = 500

            ws.Cells(i + 3, iColFrais).Value = dsFacture.Tables(0).Rows(i)("MontantFrais")
            ws.Cells(i + 3, iColFrais).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColFrais).Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
            ws.Cells(i + 3, iColFrais).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColFrais).Style.Font.Weight = 500

            ws.Cells(i + 3, iColMontantTTC).Value = dsFacture.Tables(0).Rows(i)("MontantTTC")
            ws.Cells(i + 3, iColMontantTTC).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColMontantTTC).Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
            ws.Cells(i + 3, iColMontantTTC).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColMontantTTC).Style.Font.Weight = 500

            ws.Cells(i + 3, iColDateDemande).Value = dsFacture.Tables(0).Rows(i)("DateDemandeFacture")
            ws.Cells(i + 3, iColDateDemande).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColDateDemande).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColDateDemande).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColDateDemande).Style.Font.Weight = 500

            ws.Cells(i + 3, iColDateEnvoi).Value = dsFacture.Tables(0).Rows(i)("DateEnvoi")
            ws.Cells(i + 3, iColDateEnvoi).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColDateEnvoi).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColDateEnvoi).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColDateEnvoi).Style.Font.Weight = 500
            ws.Cells(i + 3, iColPaye).Value = dsFacture.Tables(0).Rows(i)("Paye")
            ws.Cells(i + 3, iColPaye).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColPaye).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColPaye).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColPaye).Style.Font.Weight = 500
            ws.Cells(i + 3, iColDatePaiement).Value = dsFacture.Tables(0).Rows(i)("DatePaiement")
            ws.Cells(i + 3, iColDatePaiement).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColDatePaiement).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColDatePaiement).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColDatePaiement).Style.Font.Weight = 500

            ws.Cells(i + 3, iColCompteAuto).Value = dsFacture.Tables(0).Rows(i)("CompteAuto")
            ws.Cells(i + 3, iColCompteAuto).SetBorders(MultipleBorders.Outside, Drawing.Color.DarkBlue, LineStyle.Thin)
            ws.Cells(i + 3, iColCompteAuto).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ws.Cells(i + 3, iColCompteAuto).Style.FillPattern.SetPattern(FillPatternStyle.Solid, cColor, cColor)
            ws.Cells(i + 3, iColCompteAuto).Style.Font.Weight = 500



        Next

        ws.Columns(iColRef).Width = 30 * 256
        ws.Columns(iColRef).AutoFit()
        ws.Columns(iColDesignation).Width = 30 * 256
        ws.Columns(iColDesignation).AutoFit()
        ws.Columns(iColTypeAffaire).Width = 30 * 256
        ws.Columns(iColTypeAffaire).AutoFit()
        ws.Columns(iColService).Width = 30 * 256
        ws.Columns(iColService).AutoFit()
        ws.Columns(iColPrestas).Width = 30 * 256
        ws.Columns(iColPrestas).AutoFit()
        ws.Columns(iColFrais).Width = 30 * 256
        ws.Columns(iColFrais).AutoFit()
        ws.Columns(iColMontantTTC).Width = 30 * 256
        ws.Columns(iColMontantTTC).AutoFit()
        ws.Columns(iColDateDemande).Width = 30 * 256
        ws.Columns(iColDateDemande).AutoFit()
        ws.Columns(iColDateEnvoi).Width = 30 * 256
        ws.Columns(iColDateEnvoi).AutoFit()
        ws.Columns(iColPaye).Width = 30 * 256
        ws.Columns(iColPaye).AutoFit()
        ws.Columns(iColDatePaiement).Width = 30 * 256
        ws.Columns(iColDatePaiement).AutoFit()
        ws.Columns(iColCompteAuto).Width = 30 * 256
        ws.Columns(iColCompteAuto).AutoFit()
        
        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "Factures" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)

    End Sub

    ''' <summary>
    ''' chargement de la liste des clients
    ''' </summary>
    Protected Sub chargerListeClient()
        Dim oClientDAO As New CClientDAO

        cbbFiltreClient.Items.Clear()
        Dim dsClient As DataSet = oClientDAO.GetAllClientToListe()


        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les clients"
        itemDefaut.Value = CStr(-1)
        cbbFiltreClient.Items.Insert(0, itemDefaut)

        For i = 0 To dsClient.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsClient.Tables(0).Rows(i)("ClientNom").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = dsClient.Tables(0).Rows(i)("ClientID").ToString
            cbbFiltreClient.Items.Add(item)
        Next
        cbbFiltreClient.DataBind()
        cbbFiltreClient.SelectedValue = "-1"
    End Sub
    ''' <summary>
    ''' chargement de la liste des services
    ''' </summary>
    Protected Sub chargerListeService()

        Dim oStatistiquesDAO As CStatistiquesDAO = New CStatistiquesDAO

        cbbFiltreService.Items.Clear()
        Dim dsService As DataSet = oStatistiquesDAO.GetAllService()


        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les services"
        itemDefaut.Value = CStr(-1)
        cbbFiltreService.Items.Insert(0, itemDefaut)

        For i = 0 To dsService.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsService.Tables(0).Rows(i)("ServiceLibelle").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = dsService.Tables(0).Rows(i)("ServiceID").ToString
            cbbFiltreService.Items.Add(item)
        Next
        cbbFiltreService.DataBind()
        cbbFiltreService.SelectedValue = "-1"


    End Sub

    ''' <summary>
    ''' chargement de la liste des types d'affaire
    ''' </summary>
    Protected Sub chargerListeTypeAffaire()
        Dim oTypeAffaireDAO As New CTypeAffaireDAO
        cbbFiltreType.Items.Clear()
        Dim dsEmploye As DataSet = oTypeAffaireDAO.GetAllTypeAffaire(True)

        Dim itemDefaut As New ComboBoxItem
        itemDefaut.Attributes.Add("style", "color: blue;")
        itemDefaut.Text = "Tous les types"
        itemDefaut.Value = CStr(-1)
        cbbFiltreType.Items.Insert(0, itemDefaut)

        For i = 0 To dsEmploye.Tables(0).Rows.Count - 1
            Dim cbbText As String = dsEmploye.Tables(0).Rows(i)("TypeAffaireLibelle").ToString
            Dim item As New ComboBoxItem
            item.Text = cbbText
            item.Value = dsEmploye.Tables(0).Rows(i)("TypeAffaireID").ToString
            cbbFiltreType.Items.Add(item)
        Next
        cbbFiltreType.DataBind()
        cbbFiltreType.SelectedValue = "-1"
    End Sub


End Class
