Imports System.Data
Imports ComptaAna.Business
Imports ComptaAna.net.Droit
Imports GemBox.Spreadsheet
Imports System.IO

Public Class AffaireFactureDetailsV2
    Inherits System.Web.UI.Page

    Dim idFactureAffaire As Integer
    Dim rowCount As Integer
    Dim sommeTot, sommeTVA As Decimal
    Dim dateVisible As Boolean = True

    ''' <summary>
    ''' chargement de la page, chargement des informations concernant la facture
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        idFactureAffaire = CInt(Request.QueryString("id"))


        If Not Page.IsPostBack Then

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesFacture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")

                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try


            Dim oFactureAffaireDAO As New CFacturationAffaireDAO
            Dim factu As CFacturationAffaire = oFactureAffaireDAO.GetFacturationAffaireByFactureID(idFactureAffaire)

            lblClientNom.Text = factu.Affaire.Client.ClientNom
            lblClientAdresse.Text = factu.Affaire.Client.ClientAdresse
            lblClientCPetVille.Text = factu.Affaire.Client.ClientCP.ToString

            Dim bAvoir As Boolean = oFactureAffaireDAO.isAvoirWithID(idFactureAffaire)
            If bAvoir Then
                lblFactureRefetDate.Text = "Avoir n°" & factu.FacturationAffaireRef & " en date du " & factu.FacturationAffaireDate
                lblClientTVA.Text &= "" & factu.Affaire.Client.ClientNom & " : " & factu.Affaire.Client.ClientFacturationNumTVA
                lblNumBC.Visible = False
                tbBC.Visible = False
                chargerMontantAvoir(idFactureAffaire)
                ChargerTotalAvoir()
            Else
                lblFactureRefetDate.Text = "Facture n°" & factu.FacturationAffaireRef & " en date du " & factu.FacturationAffaireDate
                lblClientTVA.Text &= "" & factu.Affaire.Client.ClientNom & " : " & factu.Affaire.Client.ClientFacturationNumTVA

                chargerListeFrais(idFactureAffaire)
                chargerListePrestations(idFactureAffaire)
                ChargerTotauxFacture()
            End If
            tbBC.Text = factu.FacturationAffaireBC
            ChargerCommentaires(idFactureAffaire)
            ChargerRIB(idFactureAffaire)

        End If

    End Sub

    ''' <summary>
    ''' chargement du gridview avec les produits correspondant a la facture
    ''' </summary>
    ''' <param name="idFactureAffaire">id de la facture concernee</param>
    Protected Sub chargerListePrestations(ByVal idFactureAffaire As Integer)
        dateVisible = True
        Dim oFacturationDAO As New CFacturationAffaireDAO
        Dim oFactu As CFacturationAffaire = oFacturationDAO.GetFacturationAffaireByFactureID(idFactureAffaire)
        Dim iAffaireID As Integer = oFactu.Affaire.AffaireID
        Dim oAffaire As New CAffaireDAO
        Dim ds As New DataSet
        Dim iType As Integer = oAffaire.GetAffaire(iAffaireID).TypeAffaire.TypeAffaireID

        If iType = 3 Then
            ds = oFacturationDAO.getPrestationsRegie(idFactureAffaire)
        ElseIf iType = 2 Then
            ds = oFacturationDAO.getPrestationsCC(idFactureAffaire)
        ElseIf iType = 1 Then
            ds = oFacturationDAO.getPrestationsForfait(idFactureAffaire)
        End If


        gvBonCommande.DataSource = ds
        gvBonCommande.DataBind()
    End Sub
    ''' <summary>
    ''' chargement du gridview avec les produits correspondant a la facture
    ''' </summary>
    ''' <param name="idFactureAffaire">id de la facture concernee</param>
    Protected Sub chargerListeFrais(ByVal idFactureAffaire As Integer)
        dateVisible = True
        Dim oFacturationDAO As New CFacturationAffaireDAO
        Dim oFactu As CFacturationAffaire = oFacturationDAO.GetFacturationAffaireByFactureID(idFactureAffaire)
        Dim iAffaireID As Integer = oFactu.Affaire.AffaireID
        Dim oAffaire As New CAffaireDAO
        Dim ds As New DataSet
        Dim iType As Integer = oAffaire.GetAffaire(iAffaireID).TypeAffaire.TypeAffaireID

        If iType = 3 Then
            ds = oFacturationDAO.getFraisRegie(idFactureAffaire)
        ElseIf iType = 2 Then
            ds = oFacturationDAO.getFraisCC(idFactureAffaire)
        ElseIf iType = 1 Then
            ds = oFacturationDAO.getFraisForfait(idFactureAffaire)
        End If


        gvFrais.DataSource = ds
        gvFrais.DataBind()

    End Sub

    Private Sub chargerMontantAvoir(ByVal idFactureAffaire As Integer)
        Dim oFacturationDAO As New CFacturationAffaireDAO

        Dim ds As New DataSet
        ds = oFacturationDAO.getMontantAvoir(idFactureAffaire)

        gvBonCommande.DataSource = ds
        gvBonCommande.DataBind()
    End Sub

    Private Function CalculerSSTotalPresta(ByVal idFacture As Integer) As Double
        Dim oFacturationDAO As New CFacturationAffaireDAO
        Dim oFactu As CFacturationAffaire = oFacturationDAO.GetFacturationAffaireByFactureID(idFactureAffaire)
        Dim iAffaireID As Integer = oFactu.Affaire.AffaireID
        Dim oAffaire As New CAffaireDAO
        Dim ds As New DataSet
        Dim iType As Integer = oAffaire.GetAffaire(iAffaireID).TypeAffaire.TypeAffaireID
        Dim ssTotal As Double = 0

        If iType = 3 Then
            ds = oFacturationDAO.getPrestationsRegie(idFactureAffaire)
        ElseIf iType = 2 Then
            ds = oFacturationDAO.getPrestationsCC(idFactureAffaire)
        ElseIf iType = 1 Then
            ds = oFacturationDAO.getPrestationsForfait(idFactureAffaire)
        End If

        For Each row As DataRow In ds.Tables(0).Rows
            ssTotal += CDbl(row("Total"))
        Next

        Return Math.Round(ssTotal, 2)
    End Function

    Private Function CalculerSSTotalFrais(ByVal idFactureAffaire As Integer) As Double
        Dim oFacturationDAO As New CFacturationAffaireDAO
        Dim oFactu As CFacturationAffaire = oFacturationDAO.GetFacturationAffaireByFactureID(idFactureAffaire)
        Dim iAffaireID As Integer = oFactu.Affaire.AffaireID
        Dim oAffaire As New CAffaireDAO
        Dim ds As New DataSet
        Dim iType As Integer = oAffaire.GetAffaire(iAffaireID).TypeAffaire.TypeAffaireID
        Dim ssTotal As Double = 0

        If iType = 3 Then
            ds = oFacturationDAO.getFraisRegie(idFactureAffaire)
        ElseIf iType = 2 Then
            ds = oFacturationDAO.getFraisCC(idFactureAffaire)
        ElseIf iType = 1 Then
            ds = oFacturationDAO.getFraisForfait(idFactureAffaire)
        End If

        For Each row As DataRow In ds.Tables(0).Rows
            ssTotal += CDbl(row("Total"))
        Next

        Return Math.Round(ssTotal, 2)
    End Function
    Private Sub ChargerCommentaires(ByVal idFactureAffaire As Integer)
        Dim CFacturationAffaireDAO As New CFacturationAffaireDAO

        Dim sComDate As String = CFacturationAffaireDAO.GetCommentaireDateFacture(idFactureAffaire)
        Dim sComClauses As String = CFacturationAffaireDAO.GetCommentaireClausesFacture(idFactureAffaire)

        tbCommentaireFacture.Text = sComDate
        If Not sComDate.Length = 0 Then
            tbCommentaireFacture.Width = sComDate.Length * 6
        End If

        tbCommentaire2.Text = sComClauses
        If Not sComClauses.Length = 0 Then
            tbCommentaire2.Width = sComClauses.Length * 8
        End If

    End Sub


    ''' <summary>
    ''' redirection vers la page AffaireFacture lors du click sur le bouton Retour
    ''' </summary>
    Protected Sub btnRetour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetour.Click
        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim iAffaireID As Integer = oFactureAffaireDAO.GetFactureAffaireByFactureID(idFactureAffaire, True).Affaire.AffaireID

        Response.Redirect("AffaireFacturation.aspx?id=" & iAffaireID)
    End Sub

    Private Sub gvBonCommande_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvBonCommande.DataBound
        If CDbl(gvBonCommande.Rows(gvBonCommande.Rows.Count - 1).Cells(4).Text) = 0 Then
            gvBonCommande.Rows(gvBonCommande.Rows.Count - 1).Visible = False
        End If
    End Sub

    Protected Sub gvBonCommande_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBonCommande.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            'FactureSauvegardeTypeProduitID = -1 pour Forfait et -2 pour CC. Pour régie FactureSauvegardeTypeProduitID = TypeProduitID
            If drv("FactureSauvegardeTypeProduitID").ToString = "-1" Then
                e.Row.Cells(2).Text &= "%"
            ElseIf drv("FactureSauvegardeTypeProduitID").ToString = "-2" Then
                e.Row.Cells(2).Text &= "/12"
            End If
        End If
    End Sub

    Protected Sub gvBonCommande_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvBonCommande.RowEditing
        gvBonCommande.EditIndex = e.NewEditIndex
        chargerListePrestations(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub

    Protected Sub gvBonCommande_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvBonCommande.RowCancelingEdit
        gvBonCommande.EditIndex = -1
        chargerListePrestations(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub

    Protected Sub gvBonCommande_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvBonCommande.RowUpdating

        Dim sCom As String = ""
        If Not CType(gvBonCommande.Rows(e.RowIndex).FindControl("tbDesignation"), TextBox).Text = "" Then
            sCom = CStr(CType(gvBonCommande.Rows(e.RowIndex).FindControl("tbDesignation"), TextBox).Text.Replace("'", "''"))
        End If

        Dim iPdtID As Integer = CInt(gvBonCommande.DataKeys(e.RowIndex).Values(0))

        Dim oFactuSvdDAO As New CFactureSauvegardeDAO
        oFactuSvdDAO.SaveFacturationAffaire(idFactureAffaire, sCom, iPdtID)

        gvBonCommande.EditIndex = -1
        chargerListePrestations(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub

    Private Sub gvFrais_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFrais.RowCommand
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oAffaireDAO As New CAffaireDAO

        If e.CommandName = "DeleteProduitAffaire" Then

            Dim iProduitAffaireID As Integer = CInt(e.CommandArgument.ToString)
            oProduitAffaireDAO.SupprimerProduitAffaire(iProduitAffaireID)

        End If

        gvFrais.EditIndex = -1
        chargerListeFrais(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub


    Protected Sub gvFrais_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFrais.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'If IsDBNull(DataBinder.Eval(e.Row.DataItem, "TypeProduitID")) Then
            '    e.Row.BackColor = Drawing.Color.FromArgb(229, 243, 255)
            '    CType(e.Row.FindControl("btnAfficherDetailsEmploye"), ImageButton).Visible = False
            'End If
        End If
    End Sub

    Private Sub ChargerRIB(ByVal idFactureAffaire As Integer)
        Dim oRibDAO As New CRibDAO
        Dim oFacturationAffaire As New CFacturationAffaireDAO
        Dim oFactu As CFacturationAffaire = oFacturationAffaire.GetFacturationAffaireByFactureID(idFactureAffaire)
        Dim iRibID As Integer = oFactu.RibID
        Dim ds As DataSet = oRibDAO.GetListeCoordonnéeBancaireAvecRibID(iRibID)


        If Not iRibID = -1 Then
            Dim oRib As CRib = New CRib(CStr(iRibID))
            lblRibAxe.Text = ds.Tables(0).Rows(0)("RibLibelle").ToString
            lblIbanNum.Text = "" & oRib.Cp & oRib.Cc & " " & oRib.Cb.ToString.Substring(0, 4) & " " _
                & oRib.Cb.ToString.Substring(4, 1) & oRib.Cg.ToString.Substring(0, 3) & " " & oRib.Cg.ToString.Substring(3, 2) _
                & oRib.Nc.ToString.Substring(0, 2) & " " & oRib.Nc.ToString.Substring(2, 4) & " " & oRib.Nc.ToString.Substring(6, 4) _
                & " " & oRib.Nc.ToString.Substring(10, 1) & oRib.Cle & " BIC : " & oRib.bic

        End If

    End Sub

    Private Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrer.Click
        Dim oFactuDAO As New CFacturationAffaireDAO

        oFactuDAO.saveCom(idFactureAffaire, tbCommentaireFacture.Text, tbCommentaire2.Text)
        oFactuDAO.saveBC(idFactureAffaire, tbBC.Text)
        Response.Redirect("AffaireFactureDetailsV2.aspx?id=" & idFactureAffaire)
    End Sub

    Protected Sub gvFrais_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFrais.RowEditing
        gvFrais.EditIndex = e.NewEditIndex
        chargerListeFrais(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub

    Protected Sub gvFrais_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvFrais.RowCancelingEdit
        gvFrais.EditIndex = -1
        chargerListeFrais(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub

    Protected Sub gvFrais_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvFrais.RowUpdating
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim oProduitAffaire As New CProduitAffaire
        Dim oAffaireDAO As New CAffaireDAO
        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim iAffaireID As Integer = oFactureAffaireDAO.GetFactureAffaireByFactureID(idFactureAffaire, True).Affaire.AffaireID

        Dim iProduitAffaireID As Integer = CInt(gvFrais.DataKeys(e.RowIndex).Values(0))

        Dim sLibelle As String = CType(gvFrais.Rows(e.RowIndex).FindControl("tbEditProduitAffaireLibelle"), TextBox).Text
        Dim decPrix As Decimal = CDec(CType(gvFrais.Rows(e.RowIndex).FindControl("tbEditProduitAffairePU"), TextBox).Text.Replace(".", ","))
        Dim decQte As Decimal = CDec(CType(gvFrais.Rows(e.RowIndex).FindControl("tbEditProduitAffaireNbre"), TextBox).Text.Replace(".", ","))

        Dim oProduit As New CProduit
        Dim oProduitDAO As New CProduitDAO

        oProduitAffaire = New CProduitAffaire(iProduitAffaireID, sLibelle, decPrix, decQte, decPrix * decQte)
        oProduitAffaireDAO.UpdateProduitAffaire(oProduitAffaire)
        oAffaireDAO.UpdateSommeProduitsParAffaire(iAffaireID)

        gvFrais.EditIndex = -1
        chargerListeFrais(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub

    Protected Sub gvFrais_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFrais.RowDeleting
        Dim oProduitAffaireDAO As New CProduitAffaireDAO

        Try
            Dim iProduitAffaireID As Integer = CInt(gvFrais.DataKeys(e.RowIndex).Values(0))
            oProduitAffaireDAO.SupprimerProduitAffaire(iProduitAffaireID)
        Catch ex As InvalidCastException
            ' lblMsg.Text = "Ce produit ne peut pas être supprimer!"
        End Try

        gvFrais.EditIndex = -1
        chargerListeFrais(idFactureAffaire)
        ChargerTotauxFacture()
    End Sub

    Private Sub ChargerTotauxFacture()
        Dim ssTotal As Double = CalculerSSTotalPresta(idFactureAffaire)
        tbSSTotalHT.Text = "" & ssTotal
        tbTVA.Text = "" & Math.Round((ssTotal * 0.196), 2)
        tbSSTotalTTC.Text = "" & Math.Round(ssTotal + (ssTotal * 0.196), 2)

        Dim ssTotalFrais As Double = CalculerSSTotalFrais(idFactureAffaire)
        tbSSTotalFrais.Text = "" & ssTotalFrais
        tbTVAF.Text = "" & Math.Round((ssTotalFrais * 0.196), 2)
        tbTotalFrais.Text = "" & Math.Round((ssTotalFrais + (ssTotalFrais * 0.196)), 2)
        lblToutTotal.Text = "" & Math.Round((ssTotal + (ssTotal * 0.196) + ssTotalFrais + (ssTotalFrais * 0.196)), 2)
    End Sub

    Private Sub ChargerTotalAvoir()
        Dim ssTotal As Double
        Dim oFacturationDAO As New CFacturationAffaireDAO
        Dim ds As New DataSet
        ds = oFacturationDAO.getMontantAvoir(idFactureAffaire)

        ssTotal = CDbl(ds.Tables(0).Rows(0)("Total"))
        tbSSTotalHT.Text = "" & Math.Round(ssTotal, 2)
        tbTVA.Text = "" & Math.Round((ssTotal * 0.196), 2)
        tbSSTotalTTC.Text = "" & Math.Round(ssTotal + (ssTotal * 0.196), 2)
        lblToutTotal.Text = "" & Math.Round(ssTotal + (ssTotal * 0.196), 2)
        panelSSTotalFrais.Visible = False
    End Sub

    Private Sub btnExporter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExporter.Click
        ExportFacture()
    End Sub
    Private Sub ExportFacture()

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Facture Presta et frais")
        Dim iNumPage As Integer = 1

        'ligne
        Dim lRefEtDate As Integer = 6
        Dim lObservation As Integer = 7
        Dim lTVAAXE As Integer = 9
        Dim lTVAClient As Integer = 10
        Dim lBonCommandeEntete As Integer = 11
        Dim lCommentaire As Integer = 14
        Dim lDesignationEntetes As Integer = 16
        Dim lDesignationStotalHT As Integer = 17
        Dim lDesignationTVA As Integer = 18
        Dim lDesignationStotalTTC As Integer = 19
        Dim lFraisEntetes As Integer = 22
        Dim lFraisStotalHT As Integer = 23
        Dim lFraisTVA As Integer = 24
        Dim lFraisStotalTTC As Integer = 25
        Dim lMontantEntetes As Integer = 29
        Dim lReceptionFactureEntetes As Integer = 30
        Dim lConditions As Integer = 32


        'Colonne
        Dim cEntetes As Integer = 0
        Dim cNbre As Integer = 2
        Dim cPUeuro As Integer = 3
        Dim cTotaleuro As Integer = 4

        ' maintenant on va remplir les cellules avec les datasets
        Dim oFactuDAO As New CFacturationAffaireDAO
        Dim oFactu As CFacturationAffaire = oFactuDAO.GetFacturationAffaireByFactureID(idFactureAffaire)
        Dim oClient As New CClient
        oClient = oFactu.Affaire.Client
        Dim dsForfait As New DataSet

        Dim dsPourcentageAffaire As New DataSet
        Dim oFacturationAffaireDAO As New CFacturationAffaireDAO
        Dim dPourcentage As Double = 0
        Dim dBudget As Double = 0
        Dim iDecalageLigneDesignation As Integer = 0
        Dim iDecalageLigneFrais As Integer = 0

        Dim iAffaireID As Integer = oFactu.Affaire.AffaireID
        Dim oAffaire As New CAffaireDAO
        Dim dsPresta As New DataSet
        Dim iType As Integer = oAffaire.GetAffaire(iAffaireID).TypeAffaire.TypeAffaireID
        Dim dsFrais As New DataSet

        Dim bAvoir As Boolean = oFactuDAO.isAvoirWithID(idFactureAffaire)
        If bAvoir Then
            dsPresta = oFactuDAO.getMontantAvoir(idFactureAffaire)
            dsFrais = Nothing
        Else
            If iType = 3 Then
                dsPresta = oFactuDAO.getPrestationsRegie(idFactureAffaire)
                dsFrais = oFactuDAO.getFraisRegie(idFactureAffaire)
            ElseIf iType = 2 Then
                dsPresta = oFactuDAO.getPrestationsCC(idFactureAffaire)
                dsFrais = oFactuDAO.getFraisCC(idFactureAffaire)
            ElseIf iType = 1 Then
                dsPresta = oFactuDAO.getPrestationsForfait(idFactureAffaire)
                dsFrais = oFactuDAO.getFraisForfait(idFactureAffaire)
            End If
        End If

        'ws.PrintOptions.AutomaticPageBreakScalingFactor = 80

        'On rempli le tableau des prestations
        For Each row As DataRow In dsPresta.Tables(0).Rows
            If CDbl(row("Total")) <> 0 Then
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes).Value = row("Designation")
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes).Style.Font.Size = 9 * 20
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cNbre).Value = row("Nbre")
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cNbre).Style.Font.Size = 9 * 20
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cPUeuro).Value = row("PU")
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cPUeuro).Style.Font.Size = 9 * 20
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cTotaleuro).Value = row("Total")
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cTotaleuro).Style.Font.Size = 9 * 20


                'alignement du texte 
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cNbre).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cNbre).Style.VerticalAlignment = VerticalAlignmentStyle.Center

                'bordures
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lDesignationEntetes + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)


                ' On ajuste la largeur des colonnes et des lignes
                ws.Rows(lDesignationEntetes + iDecalageLigneDesignation + 1).Height = CInt(1.5 * 256)

                ' permet de fusionner les lignes 
                ws.Cells.GetSubrangeAbsolute(lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes, lDesignationEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True

                iDecalageLigneDesignation += 1
                'Si on a plus de 38 lignes sur la page iNumPage alors on fait un saut de page
                If iDecalageLigneDesignation = (38 * iNumPage) Then
                    ws.HorizontalPageBreaks.Add(iDecalageLigneDesignation + 1)
                    iNumPage += 1
                    iDecalageLigneDesignation += 6
                End If

            End If
        Next

        If Not dsFrais Is Nothing Then
            'On rempli le tableau des frais
            For Each row As DataRow In dsFrais.Tables(0).Rows

                Dim dQte As Double = CDbl(row("Nbre"))
                Dim dMntUnitHT As Double = CDbl(row("PU"))
                Dim dTotalLigneFrais As Double = CDbl(row("Total"))


                If Not IsDBNull(row("Frais")) Then
                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = CStr(row("Frais"))
                Else
                    ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cEntetes).Value = ""
                End If

               
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cNbre).Value = dQte
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cNbre).Style.Font.Size = 9 * 20
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cPUeuro).Value = dMntUnitHT
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cPUeuro).Style.Font.Size = 9 * 20
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Value = dTotalLigneFrais
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1, cTotaleuro).Style.Font.Size = 9 * 20


                'alignement du texte 
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cEntetes).Style.HorizontalAlignment = HorizontalAlignmentStyle.Left
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cEntetes).Style.VerticalAlignment = VerticalAlignmentStyle.Center
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cNbre).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cNbre).Style.VerticalAlignment = VerticalAlignmentStyle.Center

                'bordures
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

                ' On ajuste la largeur des colonnes et des lignes
                ws.Rows(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1).Height = CInt(1.5 * 256)

                ' permet de fusionner les lignes 
                ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cEntetes, lFraisEntetes + iDecalageLigneFrais + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True

                iDecalageLigneFrais += 1
                'Si on a plus de 38 lignes sur la page iNumPage alors on fait un saut de page
                If (lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais = 38 * iNumPage) Then
                    ws.HorizontalPageBreaks.Add(lFraisEntetes + iDecalageLigneDesignation + iDecalageLigneFrais + 1)
                    iNumPage += 1
                    iDecalageLigneFrais += 6
                End If

            Next
        End If

        If iDecalageLigneFrais = 0 Then
            If Not bAvoir Then
                iDecalageLigneFrais = 1

                'bordures
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cNbre).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cPUeuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
                ws.Cells(lFraisEntetes + iDecalageLigneDesignation + 1, cTotaleuro).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

                ' permet de fusionner les lignes 
                ws.Cells.GetSubrangeAbsolute(lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes, lFraisEntetes + iDecalageLigneDesignation + 1, cEntetes + 1).Merged = True
            End If
        End If


        'on applique le decalage de ligne
        'ligne
        lDesignationStotalHT += iDecalageLigneDesignation
        lDesignationTVA += iDecalageLigneDesignation
        lDesignationStotalTTC += iDecalageLigneDesignation

        lFraisEntetes += iDecalageLigneDesignation
        lFraisStotalHT += iDecalageLigneDesignation + iDecalageLigneFrais
        lFraisTVA += iDecalageLigneDesignation + iDecalageLigneFrais
        lFraisStotalTTC += iDecalageLigneDesignation + iDecalageLigneFrais

        lMontantEntetes += iDecalageLigneDesignation + iDecalageLigneFrais
        lReceptionFactureEntetes += iDecalageLigneDesignation + iDecalageLigneFrais
        lConditions += iDecalageLigneDesignation + iDecalageLigneFrais

        'création des cellules
        'colonne A
        Dim ecDesignationEntete As ExcelCell = ws.Cells(lDesignationEntetes, cEntetes)
        Dim ecCommentaire As ExcelCell = ws.Cells(lCommentaire, cEntetes)

        Dim ecFraisEntete As ExcelCell = ws.Cells(lFraisEntetes, cEntetes)


        If Not bAvoir Then
            ecFraisEntete.Value = "Frais à TVA déductible"
            ecFraisEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
            ecFraisEntete.Style.Font.Weight = 1000
            ws.Cells(lFraisEntetes, cEntetes).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ws.Cells(lFraisEntetes, cEntetes + 1).SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        End If

        Dim ecMontantEntete As ExcelCell = ws.Cells(lMontantEntetes, cEntetes)
        Dim ecReceptionFactureEntete As ExcelCell = ws.Cells(lReceptionFactureEntetes, cEntetes)
        Dim ecCondition As ExcelCell = ws.Cells(lConditions, cEntetes)
        Dim ecTVAAXE As ExcelCell = ws.Cells(lTVAAXE, cEntetes)
        Dim ecTVAClient As ExcelCell = ws.Cells(lTVAClient, cEntetes)
        Dim ecBonCommandeEntete As ExcelCell = ws.Cells(lBonCommandeEntete, cEntetes)

        'colonne C
        Dim ecDesignationNbre As ExcelCell = ws.Cells(lDesignationEntetes, cNbre)
        Dim ecFraisNbre As ExcelCell
        If Not bAvoir Then
            ecFraisNbre = ws.Cells(lFraisEntetes, cNbre)
            ecFraisNbre.Value = "Nbre"
            ecFraisNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
            ecFraisNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ecFraisNbre.Style.Font.Weight = 1000
            ecFraisNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        End If

        'colonne D
        Dim ecDesignationPUeuro As ExcelCell = ws.Cells(lDesignationEntetes, cPUeuro)

        Dim ecFraisPUeuro As ExcelCell
        If Not bAvoir Then
            ecFraisPUeuro = ws.Cells(lFraisEntetes, cPUeuro)
            ecFraisPUeuro.Value = "PU €uros"
            ecFraisPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
            ecFraisPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ecFraisPUeuro.Style.Font.Weight = 1000
            ecFraisPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        End If

        'colonne E
        Dim ecRefEtDate As ExcelCell = ws.Cells(lRefEtDate, cTotaleuro)
        Dim ecObservation As ExcelCell = ws.Cells(lObservation, cTotaleuro)

        Dim ecDesignationTotaleuro As ExcelCell = ws.Cells(lDesignationEntetes, cTotaleuro)
        Dim ecFraisTotaleuro As ExcelCell
        If Not bAvoir Then
            ecFraisTotaleuro = ws.Cells(lFraisEntetes, cTotaleuro)
            ecFraisTotaleuro.Value = "Total €uros"
            ecFraisTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
            ecFraisTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
            ecFraisTotaleuro.Style.Font.Weight = 1000
            ecFraisTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        End If
        Dim ecMontantTotaleuro As ExcelCell = ws.Cells(lMontantEntetes, cTotaleuro)

        Dim sRef As String = oFactu.FacturationAffaireRef

        ' Remplissage des cellules
        If bAvoir Then
            ecRefEtDate.Value = "AVOIR N°" & sRef & " EN DATE DU " & oFactu.FacturationAffaireDate
        Else
            ecRefEtDate.Value = "FACTURE N°" & sRef & " EN DATE DU " & oFactu.FacturationAffaireDate
        End If

        ecTVAAXE.Value = "N° TVA intracommunautaire AXE : FR 92 429 489 966"
        ecTVAClient.Value = "N° TVA intracommunautaire " & oClient.ClientNom & " : " & oClient.ClientFacturationNumTVA

        ecDesignationEntete.Value = "Désignation"
        ecDesignationNbre.Value = "Nbre"
        ecDesignationPUeuro.Value = "PU €uros"
        ecDesignationTotaleuro.Value = "Total €uros"

        ecBonCommandeEntete.Value = "BON DE COMMANDE n° " & oFactu.FacturationAffaireBC

        ecMontantEntete.Value = "MONTANT NET A PAYER EN €UROS"
        ecReceptionFactureEntete.Value = "A RECEPTION DE FACTURE"

        ecMontantTotaleuro.Value = "Total €uros"

        ' on va maintenant créer les dernières celules 
        Dim ecDesignationStotalHT As ExcelCell = ws.Cells(lDesignationStotalHT, cPUeuro)
        Dim ecDesignationTVA As ExcelCell = ws.Cells(lDesignationTVA, cPUeuro)
        Dim ecDesignationStotalTTC As ExcelCell = ws.Cells(lDesignationStotalTTC, cPUeuro)
        Dim ecDesignationStotalHTSom As ExcelCell = ws.Cells(lDesignationStotalHT, cTotaleuro)
        Dim ecDesignationTVASom As ExcelCell = ws.Cells(lDesignationTVA, cTotaleuro)
        Dim ecDesignationStotalTTCSom As ExcelCell = ws.Cells(lDesignationStotalTTC, cTotaleuro)

        If Not bAvoir Then
            Dim ecFraisStotalHT As ExcelCell = ws.Cells(lFraisStotalHT, cPUeuro)
            Dim ecFraisTVA As ExcelCell = ws.Cells(lFraisTVA, cPUeuro)
            Dim ecFraisStotalTTC As ExcelCell = ws.Cells(lFraisStotalTTC, cPUeuro)

            Dim ecFraisStotalHTSom As ExcelCell = ws.Cells(lFraisStotalHT, cTotaleuro)
            Dim ecFraisTVASom As ExcelCell = ws.Cells(lFraisTVA, cTotaleuro)
            Dim ecFraisStotalTTCSom As ExcelCell = ws.Cells(lFraisStotalTTC, cTotaleuro)

            ecFraisStotalHT.Value = "S/total HT"
            ecFraisTVA.Value = "TVA 19,60%"
            ecFraisStotalTTC.Value = "S/total TTC"

            ecFraisStotalHTSom.Formula = "=SUM(E" & lFraisEntetes + 2 & ":E" & lFraisEntetes + iDecalageLigneFrais + 1 & ")"
            ecFraisStotalHTSom.Style.NumberFormat = "0.00"
            ecFraisTVASom.Formula = "= E" & lFraisStotalHT + 1 & "*0.196"
            ecFraisTVASom.Style.NumberFormat = "0.00"
            ecFraisStotalTTCSom.Formula = "=SUM(E" & lFraisTVA + 1 & ":E" & lFraisStotalHT + 1 & ")"
            ecFraisStotalTTCSom.Style.NumberFormat = "0.00"

            ecFraisStotalHT.Style.Font.Weight = 1000
            ecFraisTVA.Style.Font.Weight = 1000
            ecFraisStotalTTC.Style.Font.Weight = 1000
            ecFraisStotalHTSom.Style.Font.Weight = 1000
            ecFraisTVASom.Style.Font.Weight = 1000
            ecFraisStotalTTCSom.Style.Font.Weight = 1000

            ecFraisStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ecFraisTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ecFraisStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ecFraisStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ecFraisTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
            ecFraisStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        End If


        Dim ecReceptionFactureTotaleuros As ExcelCell = ws.Cells(lReceptionFactureEntetes, cTotaleuro)

        ' remplissage 
        ecDesignationStotalHT.Value = "S/total HT"
        ecDesignationTVA.Value = "TVA 19,60%"
        ecDesignationStotalTTC.Value = "S/total TTC"
        ecBonCommandeEntete.Value = oFactu.FacturationAffaireBC
        ecObservation.Value = oFactu.FacturationAffaireCommentaireDateFacture
        ecCommentaire.Value = oFactu.FacturationAffaireCommentaireClausesFacture
        ecBonCommandeEntete.Value = "BON DE COMMANDE N° : " & oFactu.FacturationAffaireBC
        ecCondition.Value = "Pour tout retard de paiement supérieur à 30 jours à compter de la date d'emission de la facture," & vbLf & "des intérêts calculés sur la base de 2 fois le taux légal seront ajoutés à la présente facture."
        'Ajout de l'image des conditions

        'ws.Pictures.Add("App_Themes/Axone/Design/PiedDePageFacture.png", PositioningMode.Move, New AnchorCell(ws.Columns(cEntetes), ws.Rows(lConditions), 100000, 100000), New AnchorCell(ws.Columns(cPUeuro), ws.Rows(lConditions), 50000, 50000))

        ' les formules
        ecDesignationStotalHTSom.Formula = "=SUM(E" & lDesignationEntetes + 2 & ":E" & lDesignationEntetes + iDecalageLigneDesignation + 1 & ")"
        ecDesignationStotalHTSom.Style.NumberFormat = "0.00"
        ecDesignationTVASom.Formula = "= E" & lDesignationStotalHT + 1 & "*0.196"
        ecDesignationTVASom.Style.NumberFormat = "0.00"

        ecDesignationStotalTTCSom.Formula = "=SUM(E" & lDesignationTVA + 1 & ":E" & lDesignationStotalHT + 1 & ")"
        ecDesignationStotalTTCSom.Style.NumberFormat = "0.00"

        ecReceptionFactureTotaleuros.Formula = "= E" & lFraisStotalTTC + 1 & "+E" & lDesignationStotalTTC + 1
        ecReceptionFactureTotaleuros.Style.NumberFormat = "0.00"

        ' On ajuste la largeur des colonnes et des lignes
        ws.Columns(cEntetes).Width = 40 * 256
        ws.Columns(cTotaleuro).Width = 13 * 256

        'permet de fusionner les cellules 
        'ws.Cells.GetSubrangeAbsolute(lDesignationEntetes, cEntetes, lDesignationEntetes, 1).Merged = True

        ws.Cells.GetSubrangeAbsolute(lObservation, cEntetes + 1, lObservation, cTotaleuro).Merged = True

        ws.Cells.GetSubrangeAbsolute(lBonCommandeEntete, cNbre, lBonCommandeEntete, cTotaleuro).Merged = True

        ws.Cells.GetSubrangeAbsolute(lCommentaire, cNbre, lCommentaire, cTotaleuro).Merged = True

        ws.Cells.GetSubrangeAbsolute(lDesignationEntetes, cEntetes, lDesignationEntetes, cTotaleuro).Merged = True

        ws.Cells.GetSubrangeAbsolute(lFraisEntetes, cEntetes, lFraisEntetes, 1).Merged = True

        ws.Cells.GetSubrangeAbsolute(lMontantEntetes, cEntetes, lMontantEntetes, cPUeuro).Merged = True
        ws.Cells.GetSubrangeAbsolute(lReceptionFactureEntetes, cEntetes, lReceptionFactureEntetes, cPUeuro).Merged = True
        ws.Cells.GetSubrangeAbsolute(lConditions, cEntetes, lConditions + 1, cPUeuro + 1).Merged = True


        ' les couleurs
        ecDesignationEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecDesignationNbre.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecDesignationPUeuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecDesignationTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecCommentaire.Style.Font.Color = Drawing.Color.Black
        ecCommentaire.Style.Font.Italic = True
        ' ecCommentaire.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.PaleGreen, Drawing.Color.FloralWhite)

        ' ecBonCommandeEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.LightYellow, Drawing.Color.PaleTurquoise)
        ecCondition.Style.Font.Color = Drawing.Color.DarkBlue
        ecCondition.Style.Font.Italic = True
        ecCondition.Style.Font.Weight = 1000

        ecMontantEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecMontantTotaleuro.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)
        ecReceptionFactureEntete.Style.FillPattern.SetPattern(FillPatternStyle.Solid, Drawing.Color.FromArgb(192, 192, 192), Drawing.Color.Gray)

        ' alignement du texte 
        ecRefEtDate.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right

        ecDesignationNbre.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ecDesignationPUeuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ecDesignationTotaleuro.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center
        ecObservation.Style.HorizontalAlignment = HorizontalAlignmentStyle.Right
        ecCommentaire.Style.WrapText = True


        ' les polices
        ecRefEtDate.Style.Font.Weight = 1000
        ecRefEtDate.Style.Font.Size = 9 * 20

        ecDesignationEntete.Style.Font.Weight = 1000
        ecDesignationEntete.Style.Font.Size = 9 * 20
        ecDesignationNbre.Style.Font.Weight = 1000
        ecDesignationNbre.Style.Font.Size = 9 * 20
        ecDesignationPUeuro.Style.Font.Weight = 1000
        ecDesignationPUeuro.Style.Font.Size = 9 * 20
        ecDesignationTotaleuro.Style.Font.Weight = 1000
        ecDesignationTotaleuro.Style.Font.Size = 9 * 20

        'ecBonCommandeEntete.Style.Font.Weight = 1000
        ecCommentaire.Style.Font.Weight = 1000
        ecCommentaire.Style.Font.Size = 9 * 20
        ecMontantEntete.Style.Font.Weight = 1000
        ecMontantEntete.Style.Font.Size = 9 * 20
        ecMontantTotaleuro.Style.Font.Weight = 1000
        ecMontantTotaleuro.Style.Font.Size = 9 * 20
        ecReceptionFactureEntete.Style.Font.Weight = 1000
        ecReceptionFactureEntete.Style.Font.Size = 9 * 20

        ecDesignationStotalHT.Style.Font.Weight = 1000
        ecDesignationStotalHT.Style.Font.Size = 9 * 20
        ecDesignationTVA.Style.Font.Weight = 1000
        ecDesignationTVA.Style.Font.Size = 9 * 20
        ecDesignationStotalTTC.Style.Font.Weight = 1000
        ecDesignationStotalTTC.Style.Font.Size = 9 * 20
        ecDesignationStotalHTSom.Style.Font.Weight = 1000
        ecDesignationStotalHTSom.Style.Font.Size = 9 * 20
        ecDesignationTVASom.Style.Font.Weight = 1000
        ecDesignationTVASom.Style.Font.Size = 9 * 20
        ecDesignationStotalTTCSom.Style.Font.Weight = 1000
        ecDesignationStotalTTCSom.Style.Font.Size = 9 * 20

        ecReceptionFactureTotaleuros.Style.Font.Weight = 1000
        ecReceptionFactureTotaleuros.Style.Font.Size = 9 * 20

        ' les bordures
        ecDesignationEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationNbre.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationPUeuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ' ecCommentaire.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        'ecBonCommandeEntete.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ecMontantEntete.SetBorders(MultipleBorders.Top, Drawing.Color.Black, LineStyle.Thin)
        ecMontantEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
        ecMontantTotaleuro.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Bottom, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Left, Drawing.Color.Black, LineStyle.Thin)
        ecReceptionFactureEntete.SetBorders(MultipleBorders.Right, Drawing.Color.Black, LineStyle.Thin)

        ecDesignationStotalHT.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationTVA.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationStotalTTC.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationStotalHTSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationTVASom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)
        ecDesignationStotalTTCSom.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ecReceptionFactureTotaleuros.SetBorders(MultipleBorders.Outside, Drawing.Color.Black, LineStyle.Thin)

        ' haut/pied page 
        'Client.ClientFacturationNumTVA, Client.ClientNom, Client.ClientID, Client.ClientAdresse
        Try
            ws.HeadersFooters.Header = oClient.ClientNom & oClient.ClientAdresse
        Catch
            ws.HeadersFooters.Header = oClient.ClientNom
        End Try

        Dim oRibDAO As New CRibDAO
        Dim iRibID As Integer = oFactu.RibID
        If Not iRibID = -1 Then
            Dim ds As DataSet = oRibDAO.GetListeCoordonnéeBancaireAvecRibID(iRibID)
            Dim headerFooter As SheetHeaderFooter = ws.HeadersFooters

            If Not iRibID = -1 Then
                Dim oRib As CRib = New CRib(CStr(iRibID))

                headerFooter.Footer = "Conditions de règlement : chèque ou virement à réception de facture " & vbLf & " Domiciliation bancaire : " & ds.Tables(0).Rows(0)("RibLibelle").ToString & " IBAN : " & oRib.Cp & oRib.Cc & " " & oRib.Cb.ToString.Substring(0, 4) & " " & _
                    oRib.Cb.ToString.Substring(4, 1) & " " & oRib.Cg.ToString.Substring(0, 3) & " " & oRib.Cg.ToString.Substring(3, 2) & " " & _
                    oRib.Nc.ToString.Substring(0, 2) & " " & oRib.Nc.ToString.Substring(2, 4) & " " & oRib.Nc.ToString.Substring(6, 4) & _
                    " " & oRib.Nc.ToString.Substring(10, 1) & oRib.Cle & " BIC : " & oRib.bic
                headerFooter.AlignWithMargins = True
                'headerFooter.FirstPage.Footer.LeftSection.AppendPicture(Path.Combine(pathToResources, "Dices.png"), 40, 40)

            End If

        End If

        With ws.PrintOptions
            ' .FitWorksheetHeightToPages = 1
            .TopMargin = 0.5
            .BottomMargin = 1
            .HeaderMargin = 0.5
            .FooterMargin = 1.3

            .LeftMargin = 0.25
            .RightMargin = .LeftMargin

        End With

        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & oFactu.Affaire.Client.ClientNom & "_" & DateEtHeureCouranteToString() & ".xls")

        ef.SaveXls(Response.OutputStream)


    End Sub
End Class