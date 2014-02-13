Imports ComptaAna.Business
Imports System.Data
Imports GemBox.Spreadsheet
Imports ComptaAna.net.Droit

Partial Public Class ClientListe
    Inherits System.Web.UI.Page

    Dim oClientDAO As CClientDAO = New CClientDAO
    Dim ds As DataSet = oClientDAO.GetAllClients()

    ''' <summary>
    ''' Chargement de la page de la liste des clients
    ''' </summary>
    Protected Sub PageLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            gvClient.DataSource = ds
            gvClient.DataBind()
        End If

       
        Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
        Try
            If Not verifDroit(lDroit, eModule.AccesClientEcriture) Then
                btInsererClient.Visible = False
                gvClient.Columns(2).Visible = False
                gvClient.Columns(3).Visible = True

            End If
            If Not verifDroit(lDroit, eModule.AccesClientLecture) Then
                gvClient.Columns(1).Visible = False
            End If
        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try
    End Sub

    ''' <summary>
    ''' control pour modifier client/ajouter client site/supprimer client
    ''' </summary>
    Protected Sub gvClient_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "ModifierClient" Then
            Response.Redirect("ClientModification.aspx?updateClient=" & e.CommandArgument.ToString)
        ElseIf e.CommandName = "ConsulterClient" Then
            Response.Redirect("ClientModification.aspx?updateClient=" & e.CommandArgument.ToString)
        ElseIf e.CommandName = "SupprimerClient" Then
            Dim oClientDAO As CClientDAO = New CClientDAO
            Dim oClient As CClient = oClientDAO.GetClientById(CInt(e.CommandArgument.ToString))
            Dim iDelete As Integer = oClientDAO.DeleteClient(oClient)

            ds = oClientDAO.GetAllClients
            gvClient.DataSource = ds
            gvClient.DataBind()

            If iDelete = 0 Then
                lMessage.Text = "Suppression effectuée!"
            ElseIf iDelete = 1 Then
                lMessage.Text = "Suppression echouée, ce client est lié à au moins une affaire ou à au moins un produit. "
            End If
        End If
    End Sub

    ''' <summary>
    ''' Differencier les clients actif/inactif par couleur
    ''' </summary>
    Protected Sub gvClient_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim isActif As Boolean
            isActif = CBool(DataBinder.Eval(e.Row.DataItem, "ClientActif"))
            If Not isActif Then
                e.Row.BackColor = Drawing.Color.SkyBlue
            End If
        End If
    End Sub

    ''' <summary>
    ''' control du bouton insererClient
    ''' </summary>
    Protected Sub btInsererClient_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btInsererClient.Click
        Response.Redirect("ClientModification.aspx")
    End Sub

    ''' <summary>
    ''' Rechercher un client par filtrage
    ''' </summary>
    Protected Sub btRechercheClient_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRechercheClient.Click
        Dim odv As New DataView
        odv.Table = ds.Tables(0)
        odv.RowFilter = "ClientNom LIKE '%" & tbRechercheClient.Text() & "%'"
        odv.Sort = "ClientActif desc, ClientNom asc"

        gvClient.DataSource = odv
        gvClient.DataBind()

    End Sub

    Private Sub ibExporter_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibExporter.Click
        Dim oClient As New CClientDAO
        Dim dsClient As DataSet = oClient.GetAllClients()

        Dim iColNom As Integer = 0
        Dim iColAdresse As Integer = 1
        Dim iColCodePostal As Integer = 2
        Dim iColVille As Integer = 3

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Client")

        ' Affichage de l'en-tête
        ws.Cells(0, iColNom).Value = "Dénomination sociale"
        ws.Cells(0, iColAdresse).Value = "Adresse"
        ws.Cells(0, iColCodePostal).Value = "Code postal"
        ws.Cells(0, iColVille).Value = "Ville"

        ' Parcours du dataset
        For i As Integer = 0 To dsClient.Tables(0).Rows.Count - 1  
            ws.Cells(i + 1, iColNom).Value = dsClient.Tables(0).Rows(i)("ClientNom")
            ws.Cells(i + 1, iColAdresse).Value = dsClient.Tables(0).Rows(i)("ClientAdresse")
            ws.Cells(i + 1, iColCodePostal).Value = dsClient.Tables(0).Rows(i)("ClientCP")
            ws.Cells(i + 1, iColVille).Value = dsClient.Tables(0).Rows(i)("ClientVille")
        Next

        ws.Columns(iColNom).Width = 80 * 256
        ws.Columns(iColAdresse).Width = 80 * 256
        ws.Columns(iColCodePostal).Width = 12 * 256
        ws.Columns(iColVille).Width = 40 * 256

        Response.Clear()

        ' Enregistrement du fichier Excel
        Dim sNomFichier = "Client" & DateEtHeureCouranteToString()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sNomFichier & ".xls")

        ef.SaveXls(Response.OutputStream)

    End Sub

End Class