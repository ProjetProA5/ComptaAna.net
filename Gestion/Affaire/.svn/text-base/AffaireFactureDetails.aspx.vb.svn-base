Imports System.Data
Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class AffaireFactureDetails
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


            Dim oFactureAffaireDAO As New CFactureAffaireDAO
            Dim iTypeFacture As Integer = oFactureAffaireDAO.GetFactureTypeByFactureID(idFactureAffaire)

            If iTypeFacture = 1 Then
                chargerListeProduitSelected(idFactureAffaire)
                lblToutTotal.Text = CStr(sommeTot + sommeTVA)
            ElseIf iTypeFacture = 2 Or iTypeFacture = 3 Then
                chargerListeProduitEdited(idFactureAffaire)
                lblToutTotal.Text = CStr(sommeTot + sommeTVA)
            End If

        End If

    End Sub

    ''' <summary>
    ''' chargement du gridview avec les produits correspondant a la facture
    ''' </summary>
    ''' <param name="idFactureAffaire">id de la facture concernee</param>
    Protected Sub chargerListeProduitSelected(ByVal idFactureAffaire As Integer)
        dateVisible = True
        Dim oProduitAffaireDAO As New CProduitAffaireDAO
        Dim dsProduit As DataSet

        dsProduit = oProduitAffaireDAO.GetProduitAffaireSelectedByFactureID(idFactureAffaire)

        rowCount = dsProduit.Tables(0).Rows.Count

        Dim drHT As DataRow = dsProduit.Tables(0).NewRow()
        dsProduit.Tables(0).Rows.Add(drHT)
        Dim drTVA As DataRow = dsProduit.Tables(0).NewRow()
        dsProduit.Tables(0).Rows.Add(drTVA)
        Dim drTTC As DataRow = dsProduit.Tables(0).NewRow()
        dsProduit.Tables(0).Rows.Add(drTTC)

        drHT("ProduitAffaireMntUnitHT") = "S/total HT"
        drTVA("ProduitAffaireMntUnitHT") = "TVA"
        drTTC("ProduitAffaireMntUnitHT") = "S/total TTC"

        gvProduitFacture.DataSource = dsProduit
        gvProduitFacture.DataBind()

        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim oClient As CClient = oFactureAffaireDAO.GetClientInfoByFactureID(idFactureAffaire)

        lblClientNom.Text = oClient.ClientNom
        lblClientAdresse.Text = oClient.ClientAdresse
        If oClient.ClientCP = -1 Then
            lblClientCPetVille.Text = "CP non enregistré " & oClient.ClientVille
        Else
            lblClientCPetVille.Text = oClient.ClientCP & " " & oClient.ClientVille
        End If
        Dim sFacture As String() = oFactureAffaireDAO.GetRefetDateByFactureID(idFactureAffaire)
        lblFactureRefetDate.Text = "FACTURE N°" & sFacture(0) & " EN DATE DU " & sFacture(1)
        lblClientTVA.Text = "N°TVA intracommunautaire " & oClient.ClientNom & " : " & oClient.ClientFacturationNumTVA

    End Sub

    ''' <summary>
    ''' chargement du gridiview avec les produits pourcentage ou manuel
    ''' </summary>
    ''' <param name="idFactureAffaire"></param>
    Protected Sub chargerListeProduitEdited(ByVal idFactureAffaire As Integer)
        dateVisible = False
        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim dsProduit As DataSet

        dsProduit = oFactureAffaireDAO.GetProduitAffaireEditedByFactureID(idFactureAffaire)

        rowCount = dsProduit.Tables(0).Rows.Count

        Dim drHT As DataRow = dsProduit.Tables(0).NewRow()
        dsProduit.Tables(0).Rows.Add(drHT)
        Dim drTVA As DataRow = dsProduit.Tables(0).NewRow()
        dsProduit.Tables(0).Rows.Add(drTVA)
        Dim drTTC As DataRow = dsProduit.Tables(0).NewRow()
        dsProduit.Tables(0).Rows.Add(drTTC)

        drHT("ProduitAffaireMntUnitHT") = "S/total HT"
        drTVA("ProduitAffaireMntUnitHT") = "TVA"
        drTTC("ProduitAffaireMntUnitHT") = "S/total TTC"

        gvProduitFacture.DataSource = dsProduit
        gvProduitFacture.DataBind()

        gvProduitFacture.Columns.Item(0).Visible = False

        Dim oClient As CClient = oFactureAffaireDAO.GetClientInfoByFactureID(idFactureAffaire)
        lblClientNom.Text = oClient.ClientNom
        lblClientAdresse.Text = oClient.ClientAdresse
        If oClient.ClientCP = -1 Then
            lblClientCPetVille.Text = "CP non enregistré " & oClient.ClientVille
        Else
            lblClientCPetVille.Text = oClient.ClientCP & " " & oClient.ClientVille
        End If
        Dim sFacture As String() = oFactureAffaireDAO.GetRefetDateByFactureID(idFactureAffaire)
        lblFactureRefetDate.Text = "FACTURE N°" & sFacture(0) & " EN DATE DU " & sFacture(1)
        lblClientTVA.Text = "N°TVA intracommunautaire " & oClient.ClientNom & " : " & oClient.ClientFacturationNumTVA
    End Sub

    Protected Sub gvProduitFacture_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)

            If drv("ProduitAffaireDate").ToString = "" And Not drv("ProduitAffaireLibelle").ToString = "" Then
                e.Row.BackColor = Drawing.Color.PaleGoldenrod
            End If

            If e.Row.RowIndex < rowCount And Not drv("ProduitAffaireMntUnitHT").ToString = "" Then
                sommeTot += CDec(drv("TotalHT").ToString)
                sommeTVA += CDec(gvProduitFacture.DataKeys(e.Row.RowIndex).Values(1))
            Else
                If drv("ProduitAffaireMntUnitHT").ToString = "S/total HT" Then
                    e.Row.Cells.Clear()

                    Dim blank As New TableCell()
                    If dateVisible = False Then
                        blank.ColumnSpan = gvProduitFacture.Columns.Count - 3
                    Else
                        blank.ColumnSpan = gvProduitFacture.Columns.Count - 2
                    End If
                    blank.Attributes.Add("style", "border: 0px; border-right: solid 1px #DBDBDB;")

                    Dim totalHT As New TableCell()
                    totalHT.Attributes.Add("style", "color: red;")
                    totalHT.Text = "S/total HT"

                    Dim total As New TableCell()
                    total.Attributes.Add("style", "color: red;")
                    total.Text = CStr(sommeTot)

                    e.Row.Cells.Add(blank)
                    e.Row.Cells.Add(totalHT)
                    e.Row.Cells.Add(total)

                ElseIf drv("ProduitAffaireMntUnitHT").ToString = "TVA" Then
                    e.Row.Cells.Clear()

                    Dim blank As New TableCell()
                    If dateVisible = False Then
                        blank.ColumnSpan = gvProduitFacture.Columns.Count - 3
                    Else
                        blank.ColumnSpan = gvProduitFacture.Columns.Count - 2
                    End If
                    blank.Attributes.Add("style", "border: 0px; border-right: solid 1px #DBDBDB;")

                    Dim totalHT As New TableCell()
                    totalHT.Attributes.Add("style", "color: red;")
                    Try
                        totalHT.Text = "TVA " & CDec(gvProduitFacture.DataKeys(0).Values(0)) & "%"
                    Catch ex As InvalidCastException
                        totalHT.Text = "TVA"
                    End Try


                    Dim total As New TableCell()
                    total.Attributes.Add("style", "color: red;")
                    total.Text = CStr(sommeTVA)

                    e.Row.Cells.Add(blank)
                    e.Row.Cells.Add(totalHT)
                    e.Row.Cells.Add(total)

                ElseIf drv("ProduitAffaireMntUnitHT").ToString = "S/total TTC" Then
                    e.Row.Cells.Clear()

                    Dim blank As New TableCell()
                    If dateVisible = False Then
                        blank.ColumnSpan = gvProduitFacture.Columns.Count - 3
                    Else
                        blank.ColumnSpan = gvProduitFacture.Columns.Count - 2
                    End If
                    blank.Attributes.Add("style", "border: 0px; border-right: solid 1px #DBDBDB; ")

                    Dim totalHT As New TableCell()
                    totalHT.Attributes.Add("style", "color: red;")
                    totalHT.Text = "S/total TTC"

                    Dim total As New TableCell()
                    total.Attributes.Add("style", "color: red;")
                    total.Text = CStr(sommeTot + sommeTVA)

                    e.Row.Cells.Add(blank)
                    e.Row.Cells.Add(totalHT)
                    e.Row.Cells.Add(total)

                End If

            End If

        End If
    End Sub

    ''' <summary>
    ''' redirection vers la page AffaireFacture lors du click sur le bouton Retour
    ''' </summary>
    Protected Sub btnRetour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRetour.Click
        Dim oFactureAffaireDAO As New CFactureAffaireDAO
        Dim iAffaireID As Integer = oFactureAffaireDAO.GetFactureAffaireByFactureID(idFactureAffaire, False).Affaire.AffaireID

        Response.Redirect("AffaireFacture.aspx?id=" & iAffaireID)
    End Sub

End Class